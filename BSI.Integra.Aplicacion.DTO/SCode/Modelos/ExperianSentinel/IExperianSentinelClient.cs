namespace BSI.Integra.Aplicacion.DTO.ExperianSentinel
{
    /// <summary>
    /// Contrato (Strategy) para los clientes de consulta Experian Sentinel.
    /// Permite intercambiar SOAP y REST via configuracion sin modificar SentinelService.
    /// Se ubica en BSI.Integra.Aplicacion.DTO porque tanto Comercial como Servicios
    /// referencian este proyecto.
    /// </summary>
    public interface IExperianSentinelClient
    {
        /// <summary>
        /// Consulta el historial crediticio de una persona en Experian Sentinel.
        /// </summary>
        /// <param name="dniConsulta">Numero de DNI del alumno a consultar</param>
        /// <param name="tipoDocumento">Tipo de documento (ej: "D" para DNI)</param>
        /// <returns>Respuesta unificada con los 7 bloques de datos de Sentinel</returns>
        Task<ExperianSentinelRespuestaDTO> ConsultarAsync(string dniConsulta, string tipoDocumento);

        /// <summary>
        /// Retorna la respuesta cruda del proveedor sin conversiones al DTO unificado.
        /// SOAP: objeto anonimo con los 7 arrays nativos SDT_IC_*.
        /// REST: ExperianRespuestaRaiz deserializado directamente desde el JSON.
        /// </summary>
        Task<object> ConsultarAsyncCrudo(string dniConsulta, string tipoDocumento);
    }
}
