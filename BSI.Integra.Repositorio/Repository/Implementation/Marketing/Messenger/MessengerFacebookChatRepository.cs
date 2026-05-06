using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;
using System.Text.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Messenger
{
    public class MessengerFacebookChatRepository : IMessengerFacebookChatRepository
    {
        IDapperRepository _dapperRepository;

        public MessengerFacebookChatRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo)
        {
            try
            {
                var parametros = new
                {
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    DireccionMensaje = tipo?.ToLower() ?? "todas"
                };

                var SP_Obtener = "[mkt].[SP_ObtenerGrillaMessengerFacebookChat]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP_Obtener, parametros);

                if (string.IsNullOrWhiteSpace(jsonResult))
                    return new List<ResumenMessengerFacebookChatDTO>();

                var listaMensajes = JsonSerializer.Deserialize<List<ResumenMessengerFacebookChatDTO>>(jsonResult);

                return listaMensajes.OrderByDescending(m => m.FechaMensaje).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request)
        {
            try
            {
                var SP_Obtener = "[mkt].[SP_ObtenerHistorialMessengerFacebookChat]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP_Obtener, request);

                if (string.IsNullOrWhiteSpace(jsonResult))
                    return new List<ChatMessengerFacebookDTO>();

                var listaMensajes = JsonSerializer.Deserialize<List<ChatMessengerFacebookDTO>>(jsonResult);

                return listaMensajes.OrderBy(m => m.FechaEvento).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO> ObtenerDatosGeneralesAlumnosPorPSID(ObtenerDatosGeneralesAlumnosPorPSIDRequestDTO request)
        {
            try
            {
                var SP_Obtener = "[mkt].[SP_ObtenerAlumnosPorIdentificadorPaginaMessengerChat]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP_Obtener, request);

                if (string.IsNullOrWhiteSpace(jsonResult) || jsonResult.Trim() == "[]")
                    return new List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO>();

                var listaMensajes = JsonSerializer.Deserialize<List<ObtenerDatosGeneralesAlumnosPorPSIDTemp>>(jsonResult);

                var listaAgrupada = listaMensajes.GroupBy(x => new { x.IdAlumno, x.Email })
                    .Select(g => new ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO
                    {
                        IdAlumno = g.Key.IdAlumno,
                        Email = g.Key.Email,
                        FechaModificacionAlumno = g.Max(x => x.FechaModificacionAlumno),
                        Oportunidades = g.Select(o => new OportunidadDatoGeneralDTO
                        {
                            IdOportunidad = o.IdOportunidad,
                            FechaModificacionOportunidad = o.FechaModificacionOportunidad
                        })
                        .OrderBy(o => o.FechaModificacionOportunidad)
                        .ToList()
                    })
                    .OrderBy(a => a.IdAlumno)
                    .ToList();
                return listaAgrupada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarAlumnoOportunidadRegistro(string identificadorAmbitoPagina, int idOportunidad, string usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(identificadorAmbitoPagina))
                    return false;

                var SP_Insertar = "[mkt].[SP_TMessengerAlumnoRegistro_Insertar]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP_Insertar, new { IdentificadorAmbitoPagina = identificadorAmbitoPagina, IdOportunidad = idOportunidad, UsuarioCreacion = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 22/04/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene chats de para Messenger por PSID y rango de fecha
        /// </summary>
        /// <param name="IdentificadorAmbitoPagina">PSID del alumno</param>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>Listadode chats asociados al PSID</returns>
        public List<MensajeExtraccionRegistroDTO> ObtenerChatsMessengerPorIdentificadorAmbitoPagina(string IdentificadorAmbitoPagina, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<MensajeExtraccionRegistroDTO> ChatsMessenger = new List<MensajeExtraccionRegistroDTO>();

                var resultado = _dapperRepository.QuerySPDapper("[mkt].[SP_MessengerMensajeEntrante_PorIdentificadorRangoFecha]", new { IdentificadorAmbitoPagina, FechaMensaje_Inicio = fechaInicio, FechaMensaje_Fin = fechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    // El SP retorna Id como int; la conversión a string se realiza aquí en lugar de en el SP
                    var registrosRaw = JsonSerializer.Deserialize<List<MensajeExtraccionRegistroRawDTO>>(resultado);
                    ChatsMessenger = registrosRaw
                        .Select(r => new MensajeExtraccionRegistroDTO
                        {
                            Id = r.Id.ToString(),
                            Contenido = r.Contenido,
                            Remitente = r.Remitente,
                            Timestamp = r.Timestamp
                        })
                        .OrderBy(x => DateTime.Parse(x.Timestamp))
                        .ToList();
                }

                return ChatsMessenger;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    /// <summary>
    /// DTO interno de deserialización: refleja la forma exacta que retorna el SP,
    /// donde Id es int (sin CAST). Solo se usa dentro de este repositorio.
    /// </summary>
    internal class MensajeExtraccionRegistroRawDTO
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string Remitente { get; set; }
        public string Timestamp { get; set; }
    }
}
