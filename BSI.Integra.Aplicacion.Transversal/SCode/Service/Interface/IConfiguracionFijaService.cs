using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionFijaService
    {
        #region Metodos Base
        ConfiguracionFija Add(ConfiguracionFija entidad);
        ConfiguracionFija Update(ConfiguracionFija entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionFija> Add(List<ConfiguracionFija> listadoEntidad);
        List<ConfiguracionFija> Update(List<ConfiguracionFija> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ConfiguracionFijaDTO> ObtenerConfiguracionFija();
        IEnumerable<ConfiguracionFijaComboDTO> ObtenerCombo();
        List<ValorEstaticoDTO> ObtenerTodosLosRegistros();
    }
}
