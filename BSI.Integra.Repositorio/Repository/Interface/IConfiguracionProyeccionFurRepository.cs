using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionProyeccionFurRepository : IGenericRepository<TConfiguracionProyeccionFur>
    {
        #region Metodos Base
        TConfiguracionProyeccionFur Add(ConfiguracionProyeccionFur entidad);
        TConfiguracionProyeccionFur Update(ConfiguracionProyeccionFur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionProyeccionFur> Add(IEnumerable<ConfiguracionProyeccionFur> listadoEntidad);
        IEnumerable<TConfiguracionProyeccionFur> Update(IEnumerable<ConfiguracionProyeccionFur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<ConfiguracionProyeccionFurDTO> ObtenerConfiguracionProyeccionFur();
        ConfiguracionProyeccionFur ObtenerConfiguracionProyeccionFurById(int Id);

        List<ConfiguracionProyeccionFur> ObtenerConfiguracionProyeccionFurActivos();
    }
}
