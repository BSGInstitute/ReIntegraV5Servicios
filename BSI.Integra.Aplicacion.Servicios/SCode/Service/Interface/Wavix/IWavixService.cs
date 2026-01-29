using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.SCode.Service.Interface.Wolkbox
{

    public interface IWavixService
    {

        WavixPersonalDTO GetUserAccess(int idPersonal);
        Task<SipTrunkListResponseDTO> ListarSipTrunks(string apiKey, int? page = null, int? perPage = null);
        Task<SipTrunkConfigDTO> ObtenerSipTrunkPorId(string apiKey, string idSipTrunk);
        Task<GenerarTokenWidgetResponseDTO> GenerarTokenWidget(string apiKey, GenerarTokenWidgetRequestDTO request);
        Task<ConfiguracionCompletaWavixDTO> ObtenerConfiguracionCompletaWavix(int idPersonal);
    }
}
