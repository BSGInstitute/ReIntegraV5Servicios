using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionFijaRepository : IGenericRepository<TConfiguracionFija>
    {
        #region Metodos Base
        TConfiguracionFija Add(ConfiguracionFija entidad);
        TConfiguracionFija Update(ConfiguracionFija entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionFija> Add(IEnumerable<ConfiguracionFija> listadoEntidad);
        IEnumerable<TConfiguracionFija> Update(IEnumerable<ConfiguracionFija> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ConfiguracionFijaDTO> ObtenerConfiguracionFija();
        IEnumerable<ConfiguracionFijaComboDTO> ObtenerCombo();
        List<ValorEstaticoDTO> ObtenerTodosLosRegistros();
    }
}