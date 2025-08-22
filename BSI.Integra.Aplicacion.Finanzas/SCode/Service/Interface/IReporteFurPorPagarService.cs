using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteFurPorPagarService
    {
        public List<FurPorPagarDTO> ObtenerFurPorPagarByFecha(FiltroFurPorPagarDTO filtro);
    }
}
