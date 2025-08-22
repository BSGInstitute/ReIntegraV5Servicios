using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionFija
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo del Objeto
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Nombre de la tabla de donde viene el valor
        /// </summary>
        public string NombreTabla { get; set; } = null!;
        /// <summary>
        /// Id de la tabla de donde viene el valor
        /// </summary>
        public int IdTabla { get; set; }
        /// <summary>
        /// Nombre de la columna de la tabla de donde viene el valor
        /// </summary>
        public string NombreColumna { get; set; } = null!;
        /// <summary>
        /// TIpo de dato de la columna de la tabla de donde viene el valor
        /// </summary>
        public string TipoDato { get; set; } = null!;
        /// <summary>
        /// Valor a de la columna indicada
        /// </summary>
        public string Valor { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
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
