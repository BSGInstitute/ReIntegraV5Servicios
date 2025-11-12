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

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<List<ResumenMessengerFacebookChatDTO>>(jsonResult, options);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
