using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICuentaCorrienteRepository : IGenericRepository<TCuentaCorriente>
    {
        #region Metodos Base
        TCuentaCorriente Add(CuentaCorriente entidad);
        TCuentaCorriente Update(CuentaCorriente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCuentaCorriente> Add(IEnumerable<CuentaCorriente> listadoEntidad);
        IEnumerable<TCuentaCorriente> Update(IEnumerable<CuentaCorriente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CuentaCorrientesDTO> ObtenerCuentaCorriente();
        IEnumerable<CuentaCorrienteComboDTO> ObtenerCombo();
        IEnumerable<CuentaBancariaDTO> ObtenerCuentaBancaria();
        IEnumerable<CuentaCorrienteEntidadCiudadDTO> ObtenerCuentaCorrienteConEntidad();
        string ObtenerCuentaCorrienteById(int Id);
        List<CuentasCorrienteDTO> ObtenerCuentasCorrientes();
     


    }
}
