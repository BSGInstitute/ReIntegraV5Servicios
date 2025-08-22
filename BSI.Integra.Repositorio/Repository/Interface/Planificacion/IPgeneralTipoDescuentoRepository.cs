using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPgeneralTipoDescuentoRepository : IGenericRepository<TPgeneralTipoDescuento>
    {
        #region Metodos Base
        TPgeneralTipoDescuento Add(PgeneralTipoDescuento entidad);
        TPgeneralTipoDescuento Update(PgeneralTipoDescuento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralTipoDescuento> Add(IEnumerable<PgeneralTipoDescuento> listadoEntidad);
        IEnumerable<TPgeneralTipoDescuento> Update(IEnumerable<PgeneralTipoDescuento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralTipoDescuento ObtenerPorId(int id);
        List<PgeneralTipoDescuento> ObtenerPorIds(List<int> id);
    }
}
