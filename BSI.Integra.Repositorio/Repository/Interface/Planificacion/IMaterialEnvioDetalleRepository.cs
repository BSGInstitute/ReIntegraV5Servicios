using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialEnvioDetalleRepository : IGenericRepository<TMaterialEnvioDetalle>
    {
        #region Metodos Base
        TMaterialEnvioDetalle Add(MaterialEnvioDetalle entidad);
        TMaterialEnvioDetalle Update(MaterialEnvioDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialEnvioDetalle> Add(IEnumerable<MaterialEnvioDetalle> listadoEntidad);
        IEnumerable<TMaterialEnvioDetalle> Update(IEnumerable<MaterialEnvioDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MaterialEnvioDetalle? ObtenerPorId(int id);
    }
}
