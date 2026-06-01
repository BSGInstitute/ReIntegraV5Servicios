using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena tokens OAuth emitidos por Webex. AccessToken y RefreshToken con fechas de expiracion y estado del ciclo de vida.
    /// </summary>
    public partial class TWebexToken
    {
        /// <summary>
        /// Identificador unico del registro de token Webex.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Token de acceso OAuth emitido por Webex para autenticacion de API.
        /// </summary>
        public string AccessToken { get; set; } = null!;
        /// <summary>
        /// Token de renovacion OAuth para obtener nuevo AccessToken sin reautenticacion.
        /// </summary>
        public string RefreshToken { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en que expira el AccessToken.
        /// </summary>
        public DateTime FechaExpiracionAccess { get; set; }
        /// <summary>
        /// Fecha y hora en que expira el RefreshToken.
        /// </summary>
        public DateTime FechaExpiracionRefresh { get; set; }
        /// <summary>
        /// FK a T_TokenEstado. Estado del ciclo de vida del token (1=Activo, 2=Renovado, 3=Revocado, 4=Expirado).
        /// </summary>
        public int IdTokenEstado { get; set; }
        /// <summary>
        /// Permisos (scopes) OAuth asociados al token, separados por espacio.
        /// </summary>
        public string? Scopes { get; set; }
        /// <summary>
        /// Mensaje de error capturado en caso de fallo durante renovacion o uso del token.
        /// </summary>
        public string? MensajeError { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima renovacion exitosa del token.
        /// </summary>
        public DateTime? FechaUltimaRenovacion { get; set; }
        /// <summary>
        /// Estado logico del registro. 1=Activo, 0=Inactivo.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creacion del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Se actualiza automaticamente en cada modificacion.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TTokenEstado IdTokenEstadoNavigation { get; set; } = null!;
    }
}
