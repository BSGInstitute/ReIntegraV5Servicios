using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfigurarWebinarRepository : IGenericRepository<TConfigurarWebinar>
    {
        #region Metodos Base
        TConfigurarWebinar Add(ConfigurarWebinar entidad);
        TConfigurarWebinar Update(ConfigurarWebinar entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfigurarWebinar> Add(IEnumerable<ConfigurarWebinar> listadoEntidad);
        IEnumerable<TConfigurarWebinar> Update(IEnumerable<ConfigurarWebinar> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfigurarWebinar? ObtenerPorId(int id);
        IEnumerable<ConfigurarWebinar> ObtenerPorIds(IEnumerable<int> ids);
        IEnumerable<ConfigurarWebinarDTO> ObtenerPorIdPespecificoPadre(int idPEspecificoPadre);
    }
}
