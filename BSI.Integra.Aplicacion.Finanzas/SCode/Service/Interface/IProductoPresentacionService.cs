using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IProductoPresentacionService
    {
        #region Metodos Base
        ProductoPresentacion Add(ProductoPresentacion entidad);
        ProductoPresentacion Update(ProductoPresentacion entidad);
        bool Delete(int id, string usuario);

        List<ProductoPresentacion> Add(List<ProductoPresentacion> listadoEntidad);
        List<ProductoPresentacion> Update(List<ProductoPresentacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
