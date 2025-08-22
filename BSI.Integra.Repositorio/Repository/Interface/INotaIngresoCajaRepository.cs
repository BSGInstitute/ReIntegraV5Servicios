using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface INotaIngresoCajaRepository : IGenericRepository<TNotaIngresoCaja>
    {
        #region Metodos Base
        TNotaIngresoCaja Add(NotaIngresoCaja entidad);
        TNotaIngresoCaja Update(NotaIngresoCaja entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TNotaIngresoCaja> Add(IEnumerable<NotaIngresoCaja> listadoEntidad);
        IEnumerable<TNotaIngresoCaja> Update(IEnumerable<NotaIngresoCaja> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<NotaIngresoCajaComboDTO> ObtenerCombo();
        IEnumerable<NotaIngresoCajaDTO> ObtenerNotaIngresoCaja(int id);

        IEnumerable<NotaIngresoCajaDTO> ObtenerCajaIngresoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja);

        IEnumerable<NotaIngresoCajaDatosPdfDTO> ObtenerDatosCajaIngreso(int[] IdIngresoCaja);
    }
}
