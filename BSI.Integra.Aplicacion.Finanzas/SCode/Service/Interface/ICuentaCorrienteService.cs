using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICuentaCorrienteService
    {
        #region Metodos Base
        CuentaCorriente Add(CuentaBancariaRecibidoDTO entidad, string Usuario);
        CuentaCorriente Update(CuentaBancariaRecibidoDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<CuentaCorriente> Add(List<CuentaCorriente> listadoEntidad);
        List<CuentaCorriente> Update(List<CuentaCorriente> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CuentaCorrientesDTO> ObtenerCuentaCorriente();
        IEnumerable<CuentaCorrienteComboDTO> ObtenerCombo();
        IEnumerable<CuentaBancariaDTO> ObtenerCuentaBancaria();
        IEnumerable<CuentaCorrienteEntidadCiudadDTO> ObtenerCuentaCorrienteConEntidad();
        string ObtenerCuentaCorrienteById(int Id);
    }
}
