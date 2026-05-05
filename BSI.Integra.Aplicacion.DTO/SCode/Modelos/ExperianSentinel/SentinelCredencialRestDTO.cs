namespace BSI.Integra.Aplicacion.DTO.ExperianSentinel
{
    /// <summary>
    /// Credenciales para el servicio REST de Experian Sentinel.
    /// Se obtienen desde la vista fin.V_ObtenerSentinelCredencialRest.
    /// Son completamente diferentes a las credenciales SOAP (SentinelCredencialDTO).
    /// </summary>
    public class SentinelCredencialRestDTO
    {
        /// <summary>Client ID del proveedor OAuth2 de Experian</summary>
        public string ClientId { get; set; } = null!;

        /// <summary>Client Secret del proveedor OAuth2 de Experian</summary>
        public string ClientSecret { get; set; } = null!;

        /// <summary>Usuario (email) para obtener el token OAuth2</summary>
        public string Username { get; set; } = null!;

        /// <summary>Contrasena para obtener el token OAuth2</summary>
        public string Password { get; set; } = null!;

        /// <summary>Header Gx_email requerido en la consulta de historial crediticio</summary>
        public string GxEmail { get; set; } = null!;

        /// <summary>Header Gx_Key requerido en la consulta de historial crediticio</summary>
        public string GxKey { get; set; } = null!;

        /// <summary>Header Gx_usuario requerido en la consulta de historial crediticio</summary>
        public string GxUsuario { get; set; } = null!;

        /// <summary>Tipo de documento a usar en la consulta (ej: "D" para DNI)</summary>
        public string TipDocConsulta { get; set; } = null!;

        /// <summary>URL del endpoint de token OAuth2</summary>
        public string UrlToken { get; set; } = null!;

        /// <summary>URL del endpoint de consulta de historial crediticio</summary>
        public string UrlConsulta { get; set; } = null!;
    }
}
