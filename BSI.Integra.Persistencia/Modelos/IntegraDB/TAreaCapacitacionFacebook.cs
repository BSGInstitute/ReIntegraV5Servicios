using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAreaCapacitacionFacebook
    {
        /// <summary>
        /// Llava primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción o nombre del área para facebook messenger
        /// </summary>
        public string Descripcion { get; set; } = null!;
        public int Orden { get; set; }
        /// <summary>
        /// campo de auditoría del estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// campo de auditoría para la fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// campo de auditoría para la fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// campo de auditoría para el usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// campo de auditoría para el usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
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
