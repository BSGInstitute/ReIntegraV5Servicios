using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFurPagoService
    {
        #region Metodos Base
        FurPago Add(FurPago entidad);
        FurPago Update(FurPago entidad);
        bool Delete(int id, string usuario);

        List<FurPago> Add(List<FurPago> listadoEntidad);
        List<FurPago> Update(List<FurPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        int obtenerNumeroPagoByFur(int idFur);
        IEnumerable<FurPagoRealizadoDTO> ObtenerPagosRealizadosPorFur(int IdFur);
        bool InsertarFurPago(RegistrarFurPagoDTO Json);
        public bool ActualizarFurPago(RegistrarFurPagoDTO Json);

        /// Autor: Miguel Valdivia
        /// Fecha: 24/01/2026
        /// Version: 1.0
        /// <summary>
        /// Convierte un monto de una moneda origen a una moneda destino
        /// </summary>
        /// <param name="idMonedaOrigen">Id de la moneda de origen</param>
        /// <param name="idMonedaDestino">Id de la moneda de destino</param>
        /// <param name="monto">Monto a convertir</param>
        /// <returns>ConversionMonedaDTO con el resultado de la conversion</returns>
        ConversionMonedaDTO ConvertirMoneda(int idMonedaOrigen, int idMonedaDestino, decimal monto);
    }
}
