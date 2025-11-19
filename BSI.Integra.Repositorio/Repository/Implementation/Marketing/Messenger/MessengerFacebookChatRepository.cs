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
                    DireccionMensaje = tipo?.ToLower() ?? "all"
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

                if (string.IsNullOrWhiteSpace(jsonResult))
                    return new List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO>();

                var listaMensajes = JsonSerializer.Deserialize<List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO>>(jsonResult);

                return listaMensajes.OrderBy(m => m.Fecha).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool GuardarAlumnoOportunidadRegistro(string identificadorAmbitoPagina, int idOportunidad, int idAlumno, string usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(identificadorAmbitoPagina))
                    return false;

                var SP_Insertar = "[mkt].[SP_TMessengerAlumnoRegistro_Insertar]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP_Insertar, new { IdentificadorAmbitoPagina = identificadorAmbitoPagina, IdAlumno = idAlumno, IdOportunidad  = idOportunidad, UsuarioCreacion = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
