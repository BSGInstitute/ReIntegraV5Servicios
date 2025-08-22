using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModeloPredictivoProbabilidad
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ModeloPredictivoTipo
        /// </summary>
        public int IdModeloPredictivoTipo { get; set; }
        /// <summary>
        /// Indica el tipo de entrenamiento del Modelo Predictivo asociado
        /// </summary>
        public int Tipo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Probabilidad calculada
        /// </summary>
        public decimal Probabilidad { get; set; }
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

        public virtual TModeloPredictivoTipo IdModeloPredictivoTipoNavigation { get; set; } = null!;
        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
    }
}
