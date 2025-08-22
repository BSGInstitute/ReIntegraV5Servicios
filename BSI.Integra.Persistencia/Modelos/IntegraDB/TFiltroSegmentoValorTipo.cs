using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFiltroSegmentoValorTipo
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_FiltroSegmento
        /// </summary>
        public int IdFiltroSegmento { get; set; }
        /// <summary>
        /// Varlor
        /// </summary>
        public int Valor { get; set; }
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
        /// <summary>
        /// Es llave foranea de mkt.T_CategoriaObjetoFiltro
        /// </summary>
        public int IdCategoriaObjetoFiltro { get; set; }

        public virtual TFiltroSegmento IdFiltroSegmentoNavigation { get; set; } = null!;
    }
}
