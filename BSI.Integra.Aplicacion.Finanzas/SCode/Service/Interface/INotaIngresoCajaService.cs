using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface INotaIngresoCajaService
    {
        #region Metodos Base
        NotaIngresoCaja Add(NotaIngresoEnvioDTO entidad);
        NotaIngresoCaja Update(NotaIngresoEnvioDTO entidad);
        bool Delete(int id, string usuario);

        List<NotaIngresoCaja> Add(List<NotaIngresoCaja> listadoEntidad);
        List<NotaIngresoCaja> Update(List<NotaIngresoCaja> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<NotaIngresoCajaDTO> ObtenerNotaIngresoCaja(int id);
        IEnumerable<NotaIngresoCajaComboDTO> ObtenerCombo();
        IEnumerable<NotaIngresoCajaDTO> ObtenerCajaIngresoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja);

        IEnumerable<NotaIngresoCajaDatosPdfDTO> ObtenerDatosCajaIngreso(int[] IdIngresoCaja);
    }
}
