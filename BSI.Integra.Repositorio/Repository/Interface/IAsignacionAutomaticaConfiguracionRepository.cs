using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsignacionAutomaticaConfiguracionRepository : IGenericRepository<TAsignacionAutomaticaConfiguracion>
    {
        #region Metodos Base
        TAsignacionAutomaticaConfiguracion Add(AsignacionAutomaticaConfiguracion entidad);
        TAsignacionAutomaticaConfiguracion Update(AsignacionAutomaticaConfiguracion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsignacionAutomaticaConfiguracion> Add(IEnumerable<AsignacionAutomaticaConfiguracion> listadoEntidad);
        IEnumerable<TAsignacionAutomaticaConfiguracion> Update(IEnumerable<AsignacionAutomaticaConfiguracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<AsignacionAutomaticaConfiguracionDTO> ObtenerConfiguracionAsignacionAutomatica();


    }
}
