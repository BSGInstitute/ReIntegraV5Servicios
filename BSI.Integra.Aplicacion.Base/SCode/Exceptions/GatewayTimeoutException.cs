namespace BSI.Integra.Aplicacion.Base.Exceptions
{
    /// <summary>
    /// Excepcion de upstream: el servicio externo no respondio dentro del timeout.
    /// Mapea a HTTP 504 Gateway Timeout via GlobalExceptionHandlingMiddleware.
    /// </summary>
    public class GatewayTimeoutException : Exception
    {
        public GatewayTimeoutException(string msg) : base(msg) { }
    }
}
