using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Log de creacion de oportunidades (ejecucion del filtro segmento)
    /// </summary>
    public partial class TLogFiltroSegmentoEjecutado
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
        /// Llave foranea a la tabla pla.T_CentroCosto
        /// </summary>
        public int IdCentroCosto { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_TipoDato
        /// </summary>
        public int IdTipoDato { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_Origen
        /// </summary>
        public int IdOrigen { get; set; }
        /// <summary>
        /// Llave foranea a la tabla pla.T_FaseOportunidad
        /// </summary>
        public int IdFaseOportunidad { get; set; }
        /// <summary>
        /// Indica la cantidad de oportunidades creadas al ejecutar el filtro
        /// </summary>
        public int TotalOportunidadesCreadas { get; set; }
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
    }
}
