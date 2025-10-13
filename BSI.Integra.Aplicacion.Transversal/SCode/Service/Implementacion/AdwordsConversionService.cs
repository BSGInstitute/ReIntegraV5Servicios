using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    // Clase auxiliar para el resultado del envío
    public class EnvioLoteResultado
    {
        public int Exitosas { get; set; }
        public int Errores { get; set; }
    }

    /// <summary>
    /// Servicio: AdwordsConversionService
    /// Autor: Miguel Valdivia
    /// Fecha: 2025-10-04
    /// Descripción: Lógica de negocio para el envío de conversiones offline a Google Ads
    /// </summary>
    public class AdwordsConversionService : IAdwordsConversionService
    {
        private readonly IAdwordsConversionRepository _repository;

        public AdwordsConversionService(IAdwordsConversionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConversionResultDTO> EnviarConversionesPendientes()
        {
            try
            {
                await _repository.RegistrarLog("INICIO: Proceso de envío de conversiones", true);

                // 1. Obtener credenciales
                var credenciales = await _repository.ObtenerCredenciales();
                if (credenciales == null)
                {
                    throw new Exception("No se encontraron credenciales activas en T_AdworkCredencialApi");
                }

                if (!credenciales.ProcesoActivo)
                {
                    throw new Exception("El proceso de conversiones está desactivado");
                }

                // 2. Obtener conversiones pendientes
                var conversiones = await _repository.ObtenerConversionesPendientes(100);

                if (conversiones.Count == 0)
                {
                    await _repository.RegistrarLog("No hay conversiones pendientes para enviar", true);
                    return new ConversionResultDTO
                    {
                        Mensaje = "No hay conversiones pendientes",
                        Total = 0,
                        Exitosas = 0,
                        Errores = 0,
                        Timestamp = DateTime.Now
                    };
                }

                // 3. Obtener access token
                var accessToken = await ObtenerAccessToken(credenciales);

                // 4. Enviar en lotes agrupados por tipo
                int exitosas = 0;
                int errores = 0;

                var grupos = conversiones.GroupBy(c => c.TipoConversion);

                foreach (var grupo in grupos)
                {
                    try
                    {
                        var resultado = await EnviarLoteAGoogleAds(
                            grupo.ToList(),
                            accessToken,
                            credenciales
                        );

                        exitosas += resultado.Exitosas;
                        errores += resultado.Errores;
                    }
                    catch (Exception ex)
                    {
                        await _repository.RegistrarLog($"Error al enviar lote de {grupo.Key}: {ex.Message}", false);
                        errores += grupo.Count();

                        // Marcar todas como error
                        foreach (var conv in grupo)
                        {
                            await _repository.ActualizarEstadoConversion(conv.Id, "Error", null, ex.Message);
                        }
                    }
                }

                // 5. Log final
                var mensajeFinal = $"Proceso completado: {exitosas} exitosas, {errores} con errores";
                await _repository.RegistrarLog(mensajeFinal, true);

                return new ConversionResultDTO
                {
                    Mensaje = "Proceso completado",
                    Total = conversiones.Count,
                    Exitosas = exitosas,
                    Errores = errores,
                    Timestamp = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                await _repository.RegistrarLog($"ERROR CRÍTICO: {ex.Message}\n{ex.StackTrace}", false);
                throw;
            }
        }

        public async Task<List<ConversionEstadoDTO>> ObtenerEstadoConversiones()
        {
            return await _repository.ObtenerEstadoConversiones();
        }

        // ==========================================
        // MÉTODOS PRIVADOS
        // ==========================================

        private async Task<string> ObtenerAccessToken(AdwordsCredencialesDTO credenciales)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                      new KeyValuePair<string, string>("client_id", credenciales.ClientId),
                      new KeyValuePair<string, string>("client_secret", credenciales.ClientSecret),
                      new KeyValuePair<string, string>("refresh_token", credenciales.RefreshToken),
                      new KeyValuePair<string, string>("grant_type", "refresh_token")
                  });

                var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener access token: {error}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? tokenData = JsonConvert.DeserializeObject(jsonResponse);

                if (tokenData == null || tokenData.access_token == null)
                {
                    throw new Exception("No se pudo obtener el access_token de la respuesta");
                }

                return tokenData.access_token.ToString();
            }
        }

        private async Task<EnvioLoteResultado> EnviarLoteAGoogleAds(
            List<ConversionQueueDTO> conversiones,
            string accessToken,
            AdwordsCredencialesDTO credenciales)
        {
            // Preparar payload
            var conversionesPayload = conversiones.Select(c =>
            {
                var conversion = new Dictionary<string, object>
                {
                    ["gclid"] = c.Gclid,
                    ["conversionAction"] = c.ConversionActionId,
                    ["conversionDateTime"] = c.FechaHoraConversionFormatoGoogle,
                    ["currencyCode"] = "USD"
                };

                if (c.ValorConversion.HasValue && c.ValorConversion.Value > 0)
                {
                    conversion["conversionValue"] = c.ValorConversion.Value;
                }

                var userIdentifiers = new List<Dictionary<string, string>>();

                if (!string.IsNullOrEmpty(c.EmailHasheado))
                {
                    userIdentifiers.Add(new Dictionary<string, string>
                    {
                        ["hashedEmail"] = c.EmailHasheado
                    });
                }

                if (!string.IsNullOrEmpty(c.CelularHasheado))
                {
                    userIdentifiers.Add(new Dictionary<string, string>
                    {
                        ["hashedPhoneNumber"] = c.CelularHasheado
                    });
                }

                if (userIdentifiers.Any())
                {
                    conversion["userIdentifiers"] = userIdentifiers;
                }

                return conversion;
            }).ToList();

            var payload = new
            {
                conversions = conversionesPayload,
                partialFailure = true
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);

            // Enviar a Google Ads API
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);

                // URL con el Customer ID de la sub-cuenta donde están las conversiones
                var customerId = credenciales.CustomerId.Replace("-", "");
                var url = $"https://googleads.googleapis.com/{credenciales.ApiVersion}/customers/{customerId}:uploadClickConversions";

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
                request.Headers.Add("developer-token", credenciales.DeveloperToken);

                // Si hay Manager Account, usar login-customer-id (requerido para sub-cuentas)
                if (!string.IsNullOrEmpty(credenciales.ManagerAccountId))
                {
                    var managerAccountId = credenciales.ManagerAccountId.Replace("-", "");
                    request.Headers.Add("login-customer-id", managerAccountId);
                }

                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                int exitosas = 0;
                int errores = 0;

                if (response.IsSuccessStatusCode)
                {
                    // Todas exitosas
                    foreach (var conv in conversiones)
                    {
                        await _repository.ActualizarEstadoConversion(conv.Id, "Enviado", responseContent, null);
                        exitosas++;
                    }
                }
                else
                {
                    // Error
                    var mensajeError = $"HTTP {(int)response.StatusCode}: {responseContent}";
                    foreach (var conv in conversiones)
                    {
                        await _repository.ActualizarEstadoConversion(conv.Id, "Error", responseContent, mensajeError);
                        errores++;
                    }
                }

                return new EnvioLoteResultado { Exitosas = exitosas, Errores = errores };
            }
        }
    }
}