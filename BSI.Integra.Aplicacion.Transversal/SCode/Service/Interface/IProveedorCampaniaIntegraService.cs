using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProveedorCampaniaIntegraService
    {
        #region Metodos Base
        public ProveedorCampaniaIntegra Add(ProveedorCampaniaDTO entidad, string Usuario);
        public ProveedorCampaniaIntegra Update(ProveedorCampaniaDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<ProveedorCampaniaIntegra> Add(List<ProveedorCampaniaIntegra> listadoEntidad);
        List<ProveedorCampaniaIntegra> Update(List<ProveedorCampaniaIntegra> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<ProveedorCampaniaIntegraDTO> ObtenerProveedorCampaniaIntegra();
        IEnumerable<ProveedorCampaniaIntegraFiltroDTO> ObtenerProveedorCampaniaIntegraFiltro();
    }
}
