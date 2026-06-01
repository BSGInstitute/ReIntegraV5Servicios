using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los tokens de las cuentas Webex
    /// </summary>
    public partial class TTokenWebex
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Informacion de la asociada al Token de Webex
        /// </summary>
        public string? Cuenta { get; set; }
        /// <summary>
        /// Guarda contrasenas utilizadas por los usuarios en el sistema
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// Almacenamiento de codigos de acceso para la plataforma Webex
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// refresh_token generado a partir de la última actualización de Tokens
        /// </summary>
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Objeto Json obtenido en la última actualización de Tokens
        /// </summary>
        public string? AccessTokenJson { get; set; }
        /// <summary>
        /// Cantidad maxima de clases simultaneas permitidas en esta cuenta Webex. Default 2.
        /// </summary>
        public int? CapacidadSimultanea { get; set; }
    }
}
