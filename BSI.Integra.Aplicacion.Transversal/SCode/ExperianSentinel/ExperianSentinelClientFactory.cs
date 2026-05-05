using BSI.Integra.Aplicacion.DTO.ExperianSentinel;
using BSI.Integra.Aplicacion.Servicios.ExperianSentinel;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;

namespace BSI.Integra.Aplicacion.Transversal.ExperianSentinel
{
    /// <summary>
    /// Factory que crea el cliente Experian Sentinel adecuado segun el tipo indicado.
    /// Usa instancias estaticas de HttpClient y IMemoryCache para evitar agotamiento
    /// de sockets y asegurar que el cache de token se comparta entre llamadas.
    /// </summary>
    public static class ExperianSentinelClientFactory
    {
        private static readonly HttpClient _httpRest = new HttpClient();
        private static readonly IMemoryCache _cacheRest =
            new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// Crea el cliente Experian Sentinel para el tipo indicado.
        /// </summary>
        /// <param name="tipo">Tipo de cliente: "SOAP" o "REST" (insensible a mayusculas)</param>
        /// <param name="unitOfWork">Unit of work para acceso a credenciales</param>
        public static IExperianSentinelClient Crear(string tipo, IUnitOfWork unitOfWork)
        {
            return tipo?.ToUpperInvariant() switch
            {
                "REST" => new ExperianSentinelRestClient(_httpRest, unitOfWork, _cacheRest),
                _      => new ExperianSentinelSoapClient(unitOfWork)
            };
        }
    }
}
