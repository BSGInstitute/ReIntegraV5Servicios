using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRegistroMarcadorFecha
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Ciudad
        /// </summary>
        public int IdCiudad { get; set; }
        /// <summary>
        /// LLave foranea de la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Indica el documento de identidad del personal
        /// </summary>
        public string Pin { get; set; } = null!;
        /// <summary>
        /// Indica la fecha de marcacion del registro
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Indica la marcacion numero 1 del registro
        /// </summary>
        public TimeSpan? M1 { get; set; }
        /// <summary>
        /// Indica la marcacion numero 2 del registro
        /// </summary>
        public TimeSpan? M2 { get; set; }
        /// <summary>
        /// Indica la marcacion numero 3 del registro
        /// </summary>
        public TimeSpan? M3 { get; set; }
        /// <summary>
        /// Indica la marcacion numero 4 del registro
        /// </summary>
        public TimeSpan? M4 { get; set; }
        /// <summary>
        /// Indica la marcacion numero 5 del registro
        /// </summary>
        public TimeSpan? M5 { get; set; }
        /// <summary>
        /// Indica la marcacion numero 6 del registro
        /// </summary>
        public TimeSpan? M6 { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Indica el id que le pertenecia a la tabla migrada
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
