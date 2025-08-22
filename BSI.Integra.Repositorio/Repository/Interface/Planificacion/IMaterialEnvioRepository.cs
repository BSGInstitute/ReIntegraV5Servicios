using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialEnvioRepository : IGenericRepository<TMaterialEnvio>
    {
        #region Metodos Base
        TMaterialEnvio Add(MaterialEnvio entidad);
        TMaterialEnvio Update(MaterialEnvio entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialEnvio> Add(IEnumerable<MaterialEnvio> listadoEntidad);
        IEnumerable<TMaterialEnvio> Update(IEnumerable<MaterialEnvio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MaterialEnvio? ObtenerPorId(int id);
    }
}
