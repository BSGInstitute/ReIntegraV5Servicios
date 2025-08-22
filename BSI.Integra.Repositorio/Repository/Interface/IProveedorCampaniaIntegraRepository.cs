using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorCampaniaIntegraRepository : IGenericRepository<TProveedorCampaniaIntegra>
    {
        #region Metodos Base
        TProveedorCampaniaIntegra Add(ProveedorCampaniaIntegra entidad);
        TProveedorCampaniaIntegra Update(ProveedorCampaniaIntegra entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedorCampaniaIntegra> Add(IEnumerable<ProveedorCampaniaIntegra> listadoEntidad);
        IEnumerable<TProveedorCampaniaIntegra> Update(IEnumerable<ProveedorCampaniaIntegra> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ProveedorCampaniaIntegraDTO> ObtenerProveedorCampaniaIntegra();
        IEnumerable<ProveedorCampaniaIntegraFiltroDTO> ObtenerProveedorCampaniaIntegraFiltro();
    }
}
