using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionProyeccionFurService
    {
        #region Metodos Base
        ConfiguracionProyeccionFur Add(ConfiguracionProyeccionFur entidad);
        ConfiguracionProyeccionFur Update(ConfiguracionProyeccionFur entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionProyeccionFur> Add(List<ConfiguracionProyeccionFur> listadoEntidad);
        List<ConfiguracionProyeccionFur> Update(List<ConfiguracionProyeccionFur> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        public ConfiguracionProyeccionFur InsertarConfiguracionProyeccionFur(ConfiguracionProyeccionFurDTO entidad, string Usuario);
        public ConfiguracionProyeccionFur ActualizarConfiguracionProyeccionFur(ConfiguracionProyeccionFurDTO entidad, string Usuario);

        bool desactivarConfiguracion(int Id, string Usuario);
        public bool CambiarActivoConfiguracion(List<int> IdActual, int IdNuevo, string Usuario);

    }
}
