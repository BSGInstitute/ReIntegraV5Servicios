using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IInformacionWebinarService
    {
        public (IEnumerable<PgeneralWebinarDTO> PGenerals, List<ComboDTO> Pespecificos, List<ComboDTO> CentroCostos) ObtenerCombosModulo();
        List<WebinarDetalleSesionDTO> ObtenerWebinarPorFiltro(WebinarReporteFiltroDTO filtro);
        List<ComboDTO> ObtenerPEspecificoWebinar(int idPGeneral);
    }
}
