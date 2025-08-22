using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion
{
    public partial class TRegistroInicioSesionEstado
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador principal de la tabla lgv.T_RegistroInicioSesion
        /// </summary>
        public int IdRegistroInicioSesion { get; set; }
        /// <summary>
        /// Token generada en el inicio de sesión
        /// </summary>
        public string TokenGenerada { get; set; } = null!;
        /// <summary>
        /// Estado de inicio de sesión correcto o no
        /// </summary>
        public bool InicioSesionCorrecta { get; set; }
        /// <summary>
        /// Descripción del inicio de sesión generada en el logueo
        /// </summary>
        public string? Descripcion { get; set; }
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
