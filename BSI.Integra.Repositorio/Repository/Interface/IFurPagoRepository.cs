using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFurPagoRepository : IGenericRepository<TFurPago>
    {
        #region Metodos Base
        TFurPago Add(FurPago entidad);
        TFurPago Update(FurPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFurPago> Add(IEnumerable<FurPago> listadoEntidad);
        IEnumerable<TFurPago> Update(IEnumerable<FurPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        int obtenerNumeroPagoByFur(int idFur);
        IEnumerable<FurPagoDTO> BuscarListaFurPagos(int? area, int? ciudad, int? anio, int? semana, int? moneda, bool? estado);
        IEnumerable<FurPagoRealizadoDTO> ObtenerPagosRealizadosPorFur(int IdFur);
        int ObtenerFurPago(int? IdFur);
        IEnumerable<ComboDTO> ObtenerListaFormaPago();

        List<ReporteEstadoCuentaProveedorDTO> GenerarReporteEstadoCuentaProveedor(string? Empresa, string? Ciudad, int? Proveedor, string Comprobante, string FechaInicio, string FechaFin,string CuentaContable);

        /// Autor: Miguel Valdivia
        /// Fecha: 24/01/2026
        /// Version: 1.0
        /// <summary>
        /// Convierte un monto de una moneda origen a una moneda destino usando el tipo de cambio mas reciente
        /// </summary>
        /// <param name="idMonedaOrigen">Id de la moneda de origen</param>
        /// <param name="idMonedaDestino">Id de la moneda de destino</param>
        /// <param name="monto">Monto a convertir</param>
        /// <returns>ConversionMonedaDTO con el resultado de la conversion</returns>
        ConversionMonedaDTO ConvertirMoneda(int idMonedaOrigen, int idMonedaDestino, decimal monto);
    }
}
