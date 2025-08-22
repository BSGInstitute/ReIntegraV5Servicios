using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoFrecuenciaDetalleRepository : IGenericRepository<TPespecificoFrecuenciaDetalle>
    {
        #region Metodos Base
        TPespecificoFrecuenciaDetalle Add(PespecificoFrecuenciaDetalle entidad);
        TPespecificoFrecuenciaDetalle Update(PespecificoFrecuenciaDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPespecificoFrecuenciaDetalle> Add(IEnumerable<PespecificoFrecuenciaDetalle> listadoEntidad);
        IEnumerable<TPespecificoFrecuenciaDetalle> Update(IEnumerable<PespecificoFrecuenciaDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoFrecuenciaDetalle? ObtenerPorId(int id);
        IEnumerable<PespecificoFrecuenciaDetalle>? ObtenerPorIds(List<int> id);
        IEnumerable<PespecificoFrecuenciaDetalleDTO> ObtenerPorIdPespecificoFrecuencia(int idPespecificoFrecuencia);
    }
}
