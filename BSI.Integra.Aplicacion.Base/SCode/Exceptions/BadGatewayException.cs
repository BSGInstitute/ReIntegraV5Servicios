namespace BSI.Integra.Aplicacion.Base.Exceptions
{
    /// <summary>
    /// Excepcion de upstream: el servicio externo respondio 5xx o error de red.
    /// Mapea a HTTP 502 Bad Gateway via GlobalExceptionHandlingMiddleware.
    /// </summary>
    public class BadGatewayException : Exception
    {
        public BadGatewayException(string msg) : base(msg) { }
    }
}
