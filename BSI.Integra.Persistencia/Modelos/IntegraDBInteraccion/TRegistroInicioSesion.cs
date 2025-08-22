using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion
{
    public partial class TRegistroInicioSesion
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador principal de la tabla gp.T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Usuario ingresado para inicio de sesión en Integra
        /// </summary>
        public string Usuario { get; set; } = null!;
        /// <summary>
        /// Clave ingresada para inicio de sesión en Integra
        /// </summary>
        public string Clave { get; set; } = null!;
        /// <summary>
        /// Fecha de registro de la interacción
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Fecha de registro de la interacción en formato entero
        /// </summary>
        public int FechaInteraccionEntera { get; set; }
        /// <summary>
        /// Hora de registro de la interacción en formato entero
        /// </summary>
        public int HoraInteraccionEntera { get; set; }
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
