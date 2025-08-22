using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoCuentaBancoService
    {
        #region Metodos Base
        TipoCuentaBanco Add(TipoCuentaBancoDTO entidad, string Usuario);
        TipoCuentaBanco Update(TipoCuentaBancoDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<TipoCuentaBanco> Add(List<TipoCuentaBanco> listadoEntidad);
        List<TipoCuentaBanco> Update(List<TipoCuentaBanco> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoCuentaBancoDTO> ObtenerTipoCuentaBanco();
        IEnumerable<TipoCuentaBancoComboDTO> ObtenerCombo();
    }
}
