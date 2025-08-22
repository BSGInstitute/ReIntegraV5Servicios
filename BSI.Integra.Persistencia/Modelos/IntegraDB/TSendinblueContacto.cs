using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSendinblueContacto
    {
        /// <summary>
        /// Identificador unico de tabla
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Correo de contacto
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Indica si el correo se encuentra en una lista negra  
        /// </summary>
        public bool ListaNegraCorreo { get; set; }
        /// <summary>
        /// Indica si el sms se exuentra en lista negra
        /// </summary>
        public bool ListaNegroMensaje { get; set; }
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public string FechaCreacionSendinblue { get; set; } = null!;
        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public string FechaModificacionSendinblue { get; set; } = null!;
        /// <summary>
        /// Json de ids de lista
        /// </summary>
        public string? IdLista { get; set; }
        /// <summary>
        /// Json de atributos
        /// </summary>
        public string Atributo { get; set; } = null!;
        /// <summary>
        /// Respuesta de Sendinblue
        /// </summary>
        public string Respuesta { get; set; } = null!;
        /// <summary>
        /// Saber si se guardo o no en Sendinblue
        /// </summary>
        public bool EstadoGuardado { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
