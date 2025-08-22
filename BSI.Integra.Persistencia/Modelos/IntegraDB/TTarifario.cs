using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTarifario
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del tarifario
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fecha inicial para considerar el tarifario
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha final para considerar el tarifario
        /// </summary>
        public DateTime FechaFin { get; set; }
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
        /// Identificador de la tabla en integraV3
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Campo del estado visible o no visible del portal web
        /// </summary>
        public bool? VisiblePortalWeb { get; set; }
    }
}
