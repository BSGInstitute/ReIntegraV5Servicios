using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralCriterioEvaluacionHijo
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key pla.T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Valida para considerar nota para la evaluacion
        /// </summary>
        public bool ConsiderarNota { get; set; }
        /// <summary>
        /// El porcentaje sobre la nota del curso
        /// </summary>
        public int? Porcentaje { get; set; }
        /// <summary>
        /// Es foreing key de T_ModalidadCurso
        /// </summary>
        public int IdModalidadCurso { get; set; }
        /// <summary>
        /// Es foreing key de T_TipoPromedio
        /// </summary>
        public int IdTipoPromedio { get; set; }
        /// <summary>
        /// Es llave foranea con la tabla T_PGeneral
        /// </summary>
        public int IdPgeneralHijo { get; set; }
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
        /// Es foreing key de T_CriterioEvaluacion
        /// </summary>
        public int? IdCriterioEvaluacion { get; set; }
    }
}
