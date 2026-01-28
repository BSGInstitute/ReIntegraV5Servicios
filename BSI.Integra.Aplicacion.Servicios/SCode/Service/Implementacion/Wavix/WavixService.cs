using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Aplicacion.Servicios.SCode.Service.Interface.Wolkbox;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Apis.Auth.OAuth2.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.SCode.Service.Implementacion.Wavix
{
    public class WavixService : IWavixService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private readonly HttpClient _httpClient;
        public WavixService(IUnitOfWork unitOfWork, HttpClient? httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }

        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion wavix del personal
        /// </summary> 
        /// <returns> IEnumerable<WavixPersonalDTO> </returns>
        public WavixPersonalDTO GetUserAccess(int idPersonal)
        {
            try
            {
                return _unitOfWork.WavixRepository.GetUserAccess(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de numero por usuario
        /// </summary> 
        /// <returns> IEnumerable<WavixPersonalDTO> </returns>
        public List<NumeroAsesorWavixDTO>? GetNumberByUser(int idPersonal)
        {
            try
            {
                return _unitOfWork.WavixRepository.GetNumberByUser(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Carlos Crispin
        /// Fecha: 20/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de la ultima llamada realizada en wavix
        /// </summary> 
        /// <returns> IEnumerable<WavixPersonalDTO> </returns>
        public EstadoLlamadaDTO ObtenerEstadoUltimaLlamada(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada)
        {
            try
            {
                return _unitOfWork.WavixRepository.ObtenerEstadoUltimaLlamada(idPersonal, idOportunidad, idActividadDetalle, idAlumno, nroIntentoLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista los SIP trunks desde la API de Wavix
        /// </summary>
        /// <param name="apiKey">API Key para autenticación</param>
        /// <param name="page">Número de página (opcional)</param>
        /// <param name="perPage">Registros por página (opcional)</param>
        /// <returns>SipTrunkListResponseDTO</returns>
        public async Task<SipTrunkListResponseDTO> ListarSipTrunks(string apiKey, int? page = null, int? perPage = null)
        {
            try
            {
                string baseUrl = "https://api.wavix.com/v1/trunks";
                string requestUrl = $"{baseUrl}?appid={apiKey}";

                if (page.HasValue)
                {
                    requestUrl += $"&page={page.Value}";
                }

                if (perPage.HasValue)
                {
                    requestUrl += $"&per_page={perPage.Value}";
                }

                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SipTrunkListResponseDTO>(responseData);
                    return result;
                }
                else
                {
                    throw new Exception($"Error al consultar SIP trunks de Wavix: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar SIP trunks: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene la configuración de un SIP trunk específico por su ID
        /// </summary>
        /// <param name="apiKey">API Key para autenticación</param>
        /// <param name="idSipTrunk">ID del SIP trunk</param>
        /// <returns>SipTrunkConfigDTO</returns>
        public async Task<SipTrunkConfigDTO> ObtenerSipTrunkPorId(string apiKey, string idSipTrunk)
        {
            try
            {
                string baseUrl = $"https://api.wavix.com/v1/trunks/{idSipTrunk}";
                string requestUrl = $"{baseUrl}?appid={apiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SipTrunkConfigDTO>(responseData);
                    return result;
                }
                else
                {
                    throw new Exception($"Error al consultar configuración del SIP trunk {idSipTrunk}: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener SIP trunk por ID: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Genera un token para el widget embebible de Wavix
        /// </summary>
        /// <param name="apiKey">API Key para autenticación</param>
        /// <param name="request">Datos para generar el token (sip_trunk, payload, ttl)</param>
        /// <returns>GenerarTokenWidgetResponseDTO</returns>
        public async Task<GenerarTokenWidgetResponseDTO> GenerarTokenWidget(string apiKey, GenerarTokenWidgetRequestDTO request)
        {
            try
            {
                string baseUrl = "https://api.wavix.com/v2/webrtc/tokens";
                string requestUrl = $"{baseUrl}?appid={apiKey}";

                string jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<GenerarTokenWidgetResponseDTO>(responseData);
                    return result;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al generar token del widget Wavix: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar token del widget: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene la configuración completa de Wavix para un personal (SIP trunk config + token generado)
        /// Este método: 1) Obtiene config personal, 2) Consulta SIP trunk, 3) Genera token, 4) Guarda en BD
        /// </summary>
        /// <param name="idPersonal">ID del personal</param>
        /// <returns>ConfiguracionCompletaWavixDTO con toda la información necesaria</returns>
        public async Task<ConfiguracionCompletaWavixDTO> ObtenerConfiguracionCompletaWavix(int idPersonal)
        {
            string tokenUuid = null;

            try
            {
                // 1. Obtener configuración del personal (IdSipTrunk, UrlServer, IdCredencial)
                var personalConfig = _unitOfWork.WavixRepository.GetUserAccess(idPersonal);
                if (personalConfig == null)
                {
                    throw new Exception($"No se encontró configuración de Wavix para el personal {idPersonal}");
                }

                // 2. Obtener API Key asociado al personal
                var apiKey = _unitOfWork.WavixRepository.ObtenerApiKeyPorPersonal(idPersonal);

                // 2.2.  Validar que exista un token diario
                var tokenVigente = _unitOfWork.WavixRepository.ObtenerTokenVigente(personalConfig.Id);

                // 2.1 Obtener lista de troncales 
                var troncales = await ListarSipTrunks(apiKey,1,100);
                var troncalEncontrada = troncales.sip_trunks.FirstOrDefault(x=>x.name == personalConfig.IdSipTrunk);

                // 3. Obtener configuración del SIP trunk desde Wavix API
                var sipTrunkConfig = await ObtenerSipTrunkPorId(apiKey, troncalEncontrada.id);

                // 4. Generar token del widget con TTL de 12 horas (43200 segundos)
                var tokenRequest = new GenerarTokenWidgetRequestDTO
                {
                    sip_trunk = personalConfig.IdSipTrunk,
                    payload = new { },
                    ttl = 36000// 10 horas
                };


                var configuracionCompleta = new ConfiguracionCompletaWavixDTO();


                // Validar que se tenga un token diario 

                if (tokenVigente == null ) { 
                     var tokenResponse = await GenerarTokenWidget(apiKey, tokenRequest);
                     tokenUuid = tokenResponse.uuid;

                // 5. Guardar token en la base de datos
                try
                {
                    var fechaExpiracion = DateTime.Now.AddSeconds(tokenResponse.ttl ?? 36000);

                    _unitOfWork.WavixRepository.GuardarTokenDiario(
                        idPersonalWavix: personalConfig.Id,
                        tokenUuid: tokenResponse.uuid,
                        token: tokenResponse.token,
                        fechaExpiracion: fechaExpiracion,
                        usuario: "SYSTEM" 
                    );
                }
                catch (Exception exBD)
                {
                    // Log del error pero NO interrumpir el flujo
                    // El token ya fue generado en Wavix y es válido
                    Console.WriteLine($"⚠️ ADVERTENCIA: Token generado pero no guardado en BD. UUID: {tokenUuid}, Error: {exBD.Message}");

                    // No lanzamos excepción para no interrumpir el flujo
                    // El frontend recibirá el token válido aunque no esté en BD
                }

                    // 6. Construir y retornar respuesta completa
                     configuracionCompleta = new ConfiguracionCompletaWavixDTO
                    {
                        id = sipTrunkConfig.id,
                        name = sipTrunkConfig.name,
                        urlServer = personalConfig.UrlServer,
                        callerid = sipTrunkConfig.callerid,
                        token = tokenResponse.token,
                        uuid = tokenResponse.uuid,
                        accessToken = troncalEncontrada.access_token,
                         //ttl = tokenResponse.ttl
                        versionWavix = personalConfig.versionWavix
                     };
                    return configuracionCompleta;

                }

                configuracionCompleta = new ConfiguracionCompletaWavixDTO
                {
                    id = sipTrunkConfig.id,
                    name = sipTrunkConfig.name,
                    urlServer = personalConfig.UrlServer,
                    callerid = sipTrunkConfig.callerid,
                    token = tokenVigente.token,
                    uuid = tokenVigente.tokenUuid,
                    accessToken = troncalEncontrada.access_token,
                    //ttl = tokenResponse.ttl
                    versionWavix = personalConfig.versionWavix
                };
                

                return configuracionCompleta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener configuración completa de Wavix: {ex.Message}", ex);
            }
        }

        public string ObtenerTokenActivo(int idPersonal)
        {
            try {
                var tokenDirarioActivo = _unitOfWork.WavixRepository.ObtenerTokenActivo(idPersonal);

                if (tokenDirarioActivo != null) {
                    return tokenDirarioActivo;
                }
                return string.Empty;

            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la lista de tokens activos de un personal
        /// </summary>
        /// <param name="idPersonal">ID del personal</param>
        /// <returns>Lista de tokens activos</returns>
        public List<TokenActivoListDTO> ObtenerTokensActivos(int idPersonal)
        {
            try
            {
                return _unitOfWork.WavixRepository.ObtenerTokensActivos(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un token específico por su UUID
        /// </summary>
        /// <param name="tokenUuid">UUID del token</param>
        /// <returns>Token encontrado o null</returns>
        public TokenActivoListDTO ObtenerTokenPorUuid(string tokenUuid)
        {
            try
            {
                return _unitOfWork.WavixRepository.ObtenerTokenPorUuid(tokenUuid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Invalida (elimina lógicamente) un token por su UUID
        /// </summary>
        /// <param name="tokenUuid">UUID del token a invalidar</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>Respuesta de la operación</returns>
        public TokenOperacionResponseDTO InvalidarToken(string tokenUuid, string usuario)
        {
            try
            {
                var resultado = _unitOfWork.WavixRepository.InvalidarToken(tokenUuid, usuario);
                return new TokenOperacionResponseDTO
                {
                    Exito = resultado,
                    Mensaje = resultado ? "Token invalidado correctamente" : "No se encontró el token o ya estaba inactivo",
                    TokenUuid = tokenUuid
                };
            }
            catch (Exception ex)
            {
                return new TokenOperacionResponseDTO
                {
                    Exito = false,
                    Mensaje = $"Error al invalidar token: {ex.Message}",
                    TokenUuid = tokenUuid
                };
            }
        }

        /// <summary>
        /// Actualiza el payload de un token en la API de Wavix
        /// PUT /v2/webrtc/tokens/{uuid}
        /// </summary>
        /// <param name="apiKey">API Key para autenticación</param>
        /// <param name="tokenUuid">UUID del token a actualizar</param>
        /// <param name="payload">Nuevo payload</param>
        /// <returns>Respuesta de la operación</returns>
        public async Task<TokenOperacionResponseDTO> ActualizarTokenPayload(string apiKey, string tokenUuid, object payload)
        {
            try
            {
                string baseUrl = $"https://api.wavix.com/v2/webrtc/tokens/{tokenUuid}";
                string requestUrl = $"{baseUrl}?appid={apiKey}";

                var requestBody = new { payload = payload };
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return new TokenOperacionResponseDTO
                    {
                        Exito = true,
                        Mensaje = "Payload actualizado correctamente",
                        TokenUuid = tokenUuid
                    };
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return new TokenOperacionResponseDTO
                    {
                        Exito = false,
                        Mensaje = $"Error al actualizar payload: {response.StatusCode} - {errorContent}",
                        TokenUuid = tokenUuid
                    };
                }
            }
            catch (Exception ex)
            {
                return new TokenOperacionResponseDTO
                {
                    Exito = false,
                    Mensaje = $"Error al actualizar payload: {ex.Message}",
                    TokenUuid = tokenUuid
                };
            }
        }
    }

}
