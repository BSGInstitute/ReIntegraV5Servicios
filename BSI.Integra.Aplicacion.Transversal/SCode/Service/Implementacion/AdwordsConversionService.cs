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

                // 4. Obtener todas las subcuentas activas
                var subcuentas = await _repository.ObtenerSubcuentasActivas();

                // 5. Enviar en lotes agrupados por subcuenta y tipo de conversión
                int exitosas = 0;
                int errores = 0;

                // Separar conversiones con subcuenta asignada y sin subcuenta
                var conversionesConSubcuenta = conversiones.Where(c => c.IdSubcuentaGoogle.HasValue).ToList();
                var conversionesSinSubcuenta = conversiones.Where(c => !c.IdSubcuentaGoogle.HasValue).ToList();

                // Procesar conversiones con subcuenta asignada
                var gruposConSubcuenta = conversionesConSubcuenta
                    .GroupBy(c => new { c.IdSubcuentaGoogle, c.TipoConversion });

                foreach (var grupo in gruposConSubcuenta)
                {
                    try
                    {
                        var subcuenta = subcuentas.FirstOrDefault(s => s.Id == grupo.Key.IdSubcuentaGoogle);
                        if (subcuenta == null)
                        {
                            await _repository.RegistrarLog($"Subcuenta {grupo.Key.IdSubcuentaGoogle} no encontrada", false);
                            errores += grupo.Count();
                            continue;
                        }

                        var resultado = await EnviarLoteAGoogleAds(
                            grupo.ToList(),
                            accessToken,
                            credenciales,
                            subcuenta
                        );

                        exitosas += resultado.Exitosas;
                        errores += resultado.Errores;
                    }
                    catch (Exception ex)
                    {
                        await _repository.RegistrarLog($"Error al enviar lote de {grupo.Key.TipoConversion} para subcuenta {grupo.Key.IdSubcuentaGoogle}: {ex.Message}", false);
                        errores += grupo.Count();

                        foreach (var conv in grupo)
                        {
                            await _repository.ActualizarEstadoConversion(conv.Id, "Error", null, ex.Message);
                        }
                    }
                }

                // Procesar conversiones sin subcuenta: enviar a TODAS las subcuentas activas
                if (conversionesSinSubcuenta.Any())
                {
                    await _repository.RegistrarLog($"Procesando {conversionesSinSubcuenta.Count} conversiones sin subcuenta asignada - Se enviarán a todas las subcuentas", true);

                    var gruposSinSubcuenta = conversionesSinSubcuenta.GroupBy(c => c.TipoConversion);

                    foreach (var grupo in gruposSinSubcuenta)
                    {
                        // Enviar a cada subcuenta activa
                        foreach (var subcuenta in subcuentas)
                        {
                            try
                            {
                                var resultado = await EnviarLoteAGoogleAds(
                                    grupo.ToList(),
                                    accessToken,
                                    credenciales,
                                    subcuenta
                                );

                                exitosas += resultado.Exitosas;
                                errores += resultado.Errores;
                            }
                            catch (Exception ex)
                            {
                                await _repository.RegistrarLog($"Error al enviar lote de {grupo.Key} a subcuenta {subcuenta.NombreSubcuenta}: {ex.Message}", false);
                            }
                        }

                        // Marcar las conversiones como enviadas si al menos una subcuenta fue exitosa
                        foreach (var conv in grupo)
                        {
                            await _repository.ActualizarEstadoConversion(conv.Id, "Enviado", "Enviado a todas las subcuentas por no tener asignación específica", null);
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

        public async Task<GoogleAdsSubcuentaDTO?> ObtenerSubcuentaAPI(string campaignId)
        {
            try
            {
                // 1. Obtener credenciales
                var credenciales = await _repository.ObtenerCredenciales();
                if (credenciales == null)
                {
                    throw new Exception("No se encontraron credenciales activas");
                }

                // 2. Obtener access token
                var accessToken = await ObtenerAccessToken(credenciales);

                // 3. Consultar la API de Google Ads para obtener el customer_id de la campaña
                var customerId = await ConsultarSubcuentaDeCampania(campaignId, accessToken, credenciales);

                if (string.IsNullOrEmpty(customerId))
                {
                    return null;
                }

                // 4. Buscar la subcuenta en la base de datos por CustomerId
                return await _repository.ObtenerSubcuentaPorCustomerId(customerId);
            }
            catch (Exception ex)
            {
                await _repository.RegistrarLog($"Error al obtener subcuenta de campaña {campaignId}: {ex.Message}", false);
                return null;
            }
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
            AdwordsCredencialesDTO credenciales,
            GoogleAdsSubcuentaDTO subcuenta)
        {
            // Preparar payload
            var conversionesPayload = conversiones.Select(c =>
            {
                // Calcular el ConversionActionId dinámicamente desde la subcuenta
                string conversionActionId;
                if (!string.IsNullOrEmpty(c.ConversionActionId))
                {
                    // Si ya tiene ConversionActionId desde la BD, usarlo
                    conversionActionId = c.ConversionActionId;
                }
                else
                {
                    // Calcularlo dinámicamente desde la subcuenta según el TipoConversion
                    conversionActionId = c.TipoConversion switch
                    {
                        "Conversion IT" => subcuenta.ConversionActionIdIT ?? string.Empty,
                        "Conversion IP, PF" => subcuenta.ConversionActionIdIPPF ?? string.Empty,
                        "Conversion IC, IS y M" => subcuenta.ConversionActionIdICISM ?? string.Empty,
                        _ => string.Empty
                    };
                }

                var conversion = new Dictionary<string, object>
                {
                    ["gclid"] = c.Gclid,
                    ["conversionAction"] = conversionActionId,
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

                // URL con el Customer ID de la subcuenta específica
                var customerId = subcuenta.CustomerId.Replace("-", "");
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

        private async Task<string?> ConsultarSubcuentaDeCampania(string campaignId, string accessToken, AdwordsCredencialesDTO credenciales)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                // Usar el Manager Account para buscar la campaña
                var managerAccountId = credenciales.ManagerAccountId?.Replace("-", "") ?? credenciales.CustomerId.Replace("-", "");
                var url = $"https://googleads.googleapis.com/{credenciales.ApiVersion}/customers/{managerAccountId}/googleAds:searchStream";

                var gaqlQuery = new
                {
                    query = $@"
                        SELECT
                            campaign.id,
                            campaign.name,
                            customer.id
                        FROM campaign
                        WHERE campaign.id = {campaignId}
                        LIMIT 1"
                };

                var jsonPayload = JsonConvert.SerializeObject(gaqlQuery);

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
                request.Headers.Add("developer-token", credenciales.DeveloperToken);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    await _repository.RegistrarLog($"Error al consultar campaña {campaignId}: {responseContent}", false);
                    return null;
                }

                // Parsear respuesta
                dynamic? resultado = JsonConvert.DeserializeObject(responseContent);

                if (resultado == null || resultado.results == null || resultado.results.Count == 0)
                {
                    return null;
                }

                // Extraer el customer.id del primer resultado
                var customerResourceName = resultado.results[0].customer?.id?.ToString();

                return customerResourceName;
            }
        }
    }
}