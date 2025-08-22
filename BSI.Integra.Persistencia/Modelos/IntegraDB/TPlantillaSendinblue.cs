using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPlantillaSendinblue
    {
        /// <summary>
        /// Identificador unico de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la plantilla
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// HtmlContent
        /// </summary>
        public string? HtmlContenido { get; set; }
        /// <summary>
        /// HtmlEditado
        /// </summary>
        public string? HtmlEditado { get; set; }
        /// <summary>
        /// Id relacion con la tabla mkt.T_PlantillaSendinblueBase
        /// </summary>
        public int IdPlantillaSendinblueBase { get; set; }
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
