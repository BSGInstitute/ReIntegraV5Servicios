using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFiltroSegmentoDetalle
    {
        /// <summary>
        /// Clave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_FiltroSegmento
        /// </summary>
        public int IdFiltroSegmento { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_TiempoFrecuencia
        /// </summary>
        public int IdOperadorComparacion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion
        /// </summary>
        public int IdTiempoFrecuencia { get; set; }
        /// <summary>
        /// Indica la cantidad de tiempo, en relacion al campo IdTiempoFrecuencia
        /// </summary>
        public int CantidadTiempoFrecuencia { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_CategoriaObjetoFiltro
        /// </summary>
        public int IdCategoriaObjetoFiltro { get; set; }
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
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Es valor en referencia al campo IdCategoriaObjetoFiltro
        /// </summary>
        public int Valor { get; set; }

        public virtual TFiltroSegmento IdFiltroSegmentoNavigation { get; set; } = null!;
    }
}
