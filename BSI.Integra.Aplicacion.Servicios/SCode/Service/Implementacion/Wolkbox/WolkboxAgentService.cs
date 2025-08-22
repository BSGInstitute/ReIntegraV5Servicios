
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wolkbox.WolkboxAgent;
using BSI.Integra.Aplicacion.Servicios.Service.Interface.Wolkbox;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace BSI.Integra.Aplicacion.Servicios.Service.Implementacion.Wolkbox
{
    public class WolkboxAgentService : IWolkboxAgentService
    {
        private IUnitOfWork _unitOfWork;
        private const string API_WOLKBOX = "agentbox.php";
        private WolkboxTokenDTO _wolkvoxToken = new();
        private WolkboxTokenLogDTO _wolkboxTokenLog = new();
        private bool _tokenValido = false;
        public WolkboxAgentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Cambia el estado del agente
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <param name="status">Estado Agente</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> CambiarEstadoAgente(int idPersonal, string status)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"status={status}",
                    $"api=change_status",
                    $"agent_id={_wolkvoxToken.AgentId}"
                };

                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// WolkboxTipificarDTO
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <param name="model">Objeto WolkboxTipificarDTO</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> ColgarTipificarReady(int idPersonal, WolkboxTipificarDTO model)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"api=hang_type_ready",
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"cod_act={model.cod_act}",
                    $"comments={model.comments}"
                };

                if (model.cod_act != null)
                {
                    parametros.Add($"cod_act={model.cod_act_2}");
                }
                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Cuelga la llamada en el agente
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> Colgar(int idPersonal)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"api=hang"
                };

                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Silencia el microfono del agente
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> SilenciarMicrofono(int idPersonal)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"api=mute"
                };
                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza la MarcacionDTMF
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <param name="dtmf_tones">Cadena Numerica</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> MarcacionDTMF(int idPersonal, string dtmf_tones)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"api=dtmf",
                    $"tones={dtmf_tones}"
                };
                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza la llamada en el agente
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <param name="model">Objeto WolkboxMarcarDTO</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> Marcar(int idPersonal, WolkboxMarcarDTO model)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"api=dial",
                    $"customer_phone={model.customer_phone.Trim()}",
                };
                if (model.customer_id != null && model.customer_id.Trim() != "")
                    parametros.Add($"customer_id={model.customer_id.Trim()}");
                if (model.customer_name != null && model.customer_name.Trim() != "")
                    parametros.Add($"customer_name={model.customer_name.Trim()}");

                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Muesta y Oculta los Botones del agente
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <param name="model">Objecto DisplayButtonDTO</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> MostrarOcultarBotones(int idPersonal, DisplayButtonDTO model)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"api=agent_display",
                    $"display={model.display}",
                    $"button={model.button}",
                };

                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Pausa la llamada
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        public async Task<(object resultado, HttpStatusCode statusCode)> PausarLlamada(int idPersonal)
        {
            try
            {
                ValidarWolboxToken(idPersonal);

                var parametros = new List<string>()
                {
                    $"agent_id={_wolkvoxToken.AgentId}",
                    $"api=hold"
                };

                var resultado = await SolicitudHttpWolkbox(parametros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza la solicitud Http Wolkbox Api
        /// </summary>
        /// <param name="parametros">Parametros</param>
        /// <returns>Resultado y StatusCode de la solicitud</returns>
        private async Task<(object resultado, HttpStatusCode statusCode)> SolicitudHttpWolkbox(List<string>? parametros)
        {
            try
            {
                var queryParams = string.Empty;
                if (parametros != null && parametros.Count() > 0)
                {
                    queryParams = "?" + string.Join("&", parametros);
                }
                var urlBase = $"https://wv{_wolkvoxToken.WolkvoxServer}.wolkvox.com/api/v2/{API_WOLKBOX}";
                var urlApi = $"{urlBase}{queryParams}";

                object resultado;
                HttpStatusCode statusCode;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("ContentType", "application/json");
                    client.DefaultRequestHeaders.Add("wolkvox-token", _wolkvoxToken.Token);
                    client.DefaultRequestHeaders.Add("wolkvox_server", _wolkvoxToken.WolkvoxServer);

                    _wolkboxTokenLog = new WolkboxTokenLogDTO();
                    _wolkboxTokenLog.Method = "POST";
                    _wolkboxTokenLog.UrlApi = urlBase;
                    _wolkboxTokenLog.QueryParams = queryParams;
                    _wolkboxTokenLog.Body = "";

                    HttpResponseMessage response;
                    response = await client.PostAsync(urlApi, null);
                    statusCode = response.StatusCode;
                    _wolkboxTokenLog.StatusCode = $"{(int)statusCode}";
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        _wolkboxTokenLog.Response = responseContent;
                        resultado = JsonConvert.DeserializeObject<ResponseWolkboxDTO>(responseContent)!;
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        _wolkboxTokenLog.Response = responseContent;
                        resultado = JsonConvert.DeserializeObject<ErrorWolboxDTO>(responseContent)!;
                    }
                    InsertarWolkboxTokenLog();
                }
                return (resultado, statusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo registro de WolkboxTokenLog
        /// </summary>
        private void InsertarWolkboxTokenLog()
        {
            try
            {
                _wolkboxTokenLog.IdWolkboxToken = _wolkvoxToken.Id;
                _wolkboxTokenLog.IdPersonal = _wolkvoxToken.IdPersonal;
                _wolkboxTokenLog.AgentId = _wolkvoxToken.AgentId;
                _unitOfWork.WolkboxRepository.InsertarWolkboxTokenLog(_wolkboxTokenLog);
            }
            catch (Exception ex)
            {
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Validar el WolkboxToken por personal
        /// </summary>
        /// <param name="idPersonal">Identificador Personal</param>
        private void ValidarWolboxToken(int idPersonal)
        {
            try
            {
                var informacionToken = _unitOfWork.WolkboxRepository.ObtenerWolkboxTokenPorIdPersonal(idPersonal);
                if (informacionToken == null)
                {
                    throw new BadRequestException($"El Personal {idPersonal} no tiene token configurado");
                }
                if (informacionToken.ContadorDia + 1 >= informacionToken.Limite)
                {
                    _tokenValido = false;
                    HabilitarNuevoToken(informacionToken.Id, informacionToken.IdPersonal);
                }
                else
                {
                    _wolkvoxToken = informacionToken;
                    _tokenValido = true;
                }
                if (_tokenValido == false)
                    throw new BadRequestException($"Error: El personal {idPersonal} no tiene configurado un token valido");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Habilita un nuevo token para el personal
        /// </summary>
        /// <param name="idWolkboxToken">Identificador WolkboxToken</param>
        /// <param name="idPersonal">Identificador Personal</param>
        private void HabilitarNuevoToken(int idWolkboxToken, int idPersonal)
        {
            try
            {
                _unitOfWork.WolkboxRepository.ReasignarTokenWolkboxPersonal(idWolkboxToken);
                var nuevoToken = _unitOfWork.WolkboxRepository.ObtenerWolkboxTokenPorIdPersonal(idPersonal);
                if (nuevoToken == null)
                {
                    throw new BadRequestException($"El Personal {idPersonal} no tiene token configurado");
                }
                else
                {
                    if (nuevoToken.ContadorDia + 1 < nuevoToken.Limite)
                    {
                        _wolkvoxToken = nuevoToken;
                        _tokenValido = true;
                    }
                    else
                    {
                        _tokenValido = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
