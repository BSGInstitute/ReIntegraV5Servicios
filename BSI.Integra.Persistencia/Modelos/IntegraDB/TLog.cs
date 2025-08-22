using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLog
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Direccion IP
        /// </summary>
        public string Ip { get; set; } = null!;
        /// <summary>
        /// Nombre de usuario 
        /// </summary>
        public string Usuario { get; set; } = null!;
        /// <summary>
        /// Descripcion de la maquina
        /// </summary>
        public string Maquina { get; set; } = null!;
        /// <summary>
        /// Ruta del acceso
        /// </summary>
        public string Ruta { get; set; } = null!;
        /// <summary>
        /// Parametros del trafigo
        /// </summary>
        public string Parametros { get; set; } = null!;
        /// <summary>
        /// Mensaje de ruta
        /// </summary>
        public string Mensaje { get; set; } = null!;
        /// <summary>
        /// Excepciones
        /// </summary>
        public string? Excepcion { get; set; }
        /// <summary>
        /// Tipo 
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Llave primaria de Padre
        /// </summary>
        public int? IdPadre { get; set; }
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
    }
}
