using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoContribuyenteService
    {
        #region Metodos Base
        TipoContribuyente Add(TipoContribuyente entidad);
        TipoContribuyente Update(TipoContribuyente entidad);
        bool Delete(int id, string usuario);

        List<TipoContribuyente> Add(List<TipoContribuyente> listadoEntidad);
        List<TipoContribuyente> Update(List<TipoContribuyente> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerTipoContribuyente();
    }
}
