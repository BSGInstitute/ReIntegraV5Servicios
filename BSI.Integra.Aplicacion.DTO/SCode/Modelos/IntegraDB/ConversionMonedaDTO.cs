namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// Autor: Miguel Valdivia
    /// Fecha: 24/01/2026
    /// Version: 1.0
    /// <summary>
    /// DTO para retornar el resultado de una conversion de moneda
    /// </summary>
    public class ConversionMonedaDTO
    {
        /// <summary>
        /// Monto convertido a la moneda destino
        /// </summary>
        public decimal MontoConvertido { get; set; }

        /// <summary>
        /// Tipo de cambio utilizado para la conversion
        /// </summary>
        public decimal TipoCambioUtilizado { get; set; }

        /// <summary>
        /// Fecha del tipo de cambio utilizado
        /// </summary>
        public DateTime FechaTipoCambio { get; set; }

        /// <summary>
        /// Nombre de la moneda de origen
        /// </summary>
        public string MonedaOrigenNombre { get; set; }

        /// <summary>
        /// Nombre de la moneda de destino
        /// </summary>
        public string MonedaDestinoNombre { get; set; }
    }

    /// <summary>
    /// DTO interno para deserializar tipo de cambio desde la base de datos
    /// </summary>
    public class TipoCambioMonedaDTO
    {
        public decimal DolarAMoneda { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreMoneda { get; set; }
    }
}
