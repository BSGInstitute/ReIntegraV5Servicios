using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteFurPorPagarRepository
    {
        public List<FurPorPagarDTO> ObtenerFurPorPagarByFecha(DateTime? FechaInicial, DateTime? FechaFinal);

    }
}
