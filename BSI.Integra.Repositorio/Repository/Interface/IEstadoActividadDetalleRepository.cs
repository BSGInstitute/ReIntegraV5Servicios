using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoActividadDetalleRepository : IGenericRepository<TEstadoActividadDetalle>
    {
        #region Metodos Base
        TEstadoActividadDetalle Add(EstadoActividadDetalle entidad);
        TEstadoActividadDetalle Update(EstadoActividadDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoActividadDetalle> Add(IEnumerable<EstadoActividadDetalle> listadoEntidad);
        IEnumerable<TEstadoActividadDetalle> Update(IEnumerable<EstadoActividadDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ComboDTO> ObtenerDetalleActividadFiltroCodigo();
    }
}
