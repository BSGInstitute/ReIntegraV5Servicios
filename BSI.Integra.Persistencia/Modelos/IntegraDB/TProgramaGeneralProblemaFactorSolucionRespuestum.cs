using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla muestra el detalle de la solucion propuesta
    /// </summary>
    public partial class TProgramaGeneralProblemaFactorSolucionRespuestum
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// FK de T_ProgramaGeneralProblemaFactorSolucion
        /// </summary>
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        /// <summary>
        /// Indica si esta solucionado el problema
        /// </summary>
        public bool EsSolucionado { get; set; }
        /// <summary>
        /// Estado del registro
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
        /// RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
        public virtual TProgramaGeneralProblemaFactorSolucion IdProgramaGeneralProblemaFactorSolucionNavigation { get; set; } = null!;
    }
}
