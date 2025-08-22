using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion
{
    public partial class TInteraccionModulo
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador principal de la tabla conf.T_Usuario
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Fecha de registro de la interacción en integra
        /// </summary>
        public DateTime FechaInteraccion { get; set; }
        /// <summary>
        /// Fecha de registro de la interacción en formato entero
        /// </summary>
        public int FechaInteraccionEntera { get; set; }
        /// <summary>
        /// Hora de registro de la interacción en formato entero
        /// </summary>
        public int HoraInteraccionEntera { get; set; }
        /// <summary>
        /// Url anterior de navegación en Integra
        /// </summary>
        public string? UrlAnterior { get; set; }
        /// <summary>
        /// Url actual de navegación en Integra
        /// </summary>
        public string? UrlActual { get; set; }
        /// <summary>
        /// Ip Publica del equipo de origen
        /// </summary>
        public string IpPublica { get; set; } = null!;
        /// <summary>
        /// Ip Local de equipo de origen
        /// </summary>
        public string? IpLocal { get; set; }
        /// <summary>
        /// Dirección MAC del equipo de origen
        /// </summary>
        public string? DireccionMac { get; set; }
        /// <summary>
        /// Dirección MAC del equipo de origen
        /// </summary>
        public string? ControlTipo { get; set; }
        /// <summary>
        /// Dirección MAC del equipo de origen
        /// </summary>
        public string? ControlNombre { get; set; }
        /// <summary>
        /// Contenido de la interacción en caso de input
        /// </summary>
        public string? Contenido { get; set; }
        /// <summary>
        /// Dirección MAC del equipo de origen
        /// </summary>
        public string? NombreModulo { get; set; }
        /// <summary>
        /// Dirección MAC del equipo de origen
        /// </summary>
        public string? InteraccionJson { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
