namespace BSI.Integra.Aplicacion.DTO.ExperianSentinel
{
    /// <summary>
    /// Configuracion global del tipo de proveedor activo para consultas Sentinel.
    /// Se mantiene en DTO para que tanto Comercial como Servicios y la API principal
    /// puedan leer y cambiar el estado sin dependencias cruzadas.
    /// </summary>
    public static class SentinelTipoServicioConfig
    {
        private static string _tipo = "REST";

        /// <summary>
        /// Tipo de proveedor actualmente activo: "REST" o "SOAP".
        /// </summary>
        public static string TipoActual => _tipo;

        /// <summary>
        /// Cambia el proveedor activo en runtime. Valores válidos: REST, SOAP.
        /// </summary>
        /// <param name="tipo">Tipo de proveedor: "REST" o "SOAP"</param>
        /// <exception cref="ArgumentException">Si el tipo no es REST ni SOAP</exception>
        public static void Cambiar(string tipo)
        {
            tipo = tipo?.Trim().ToUpperInvariant() ?? string.Empty;
            if (tipo != "REST" && tipo != "SOAP")
                throw new ArgumentException($"Tipo de servicio inválido: '{tipo}'. Valores válidos: REST, SOAP");
            _tipo = tipo;
        }
    }
}
