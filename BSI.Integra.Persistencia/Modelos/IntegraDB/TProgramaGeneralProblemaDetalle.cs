using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Detalle de los problemas generales en el programa.
    /// </summary>
    public partial class TProgramaGeneralProblemaDetalle
    {
        public TProgramaGeneralProblemaDetalle()
        {
            TProgramaGeneralProblemaFactorSubSolucionAsignada = new HashSet<TProgramaGeneralProblemaFactorSubSolucionAsignadum>();
        }

        /// <summary>
        /// Identificador único de la tabla.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador general del problema.
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Identificador del factor asociado al problema.
        /// </summary>
        public int? IdProgramaGeneralProblemaFactor { get; set; }
        /// <summary>
        /// Identificador del detalle del factor del problema.
        /// </summary>
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        /// <summary>
        /// Indica si aplica título en el detalle.
        /// </summary>
        public bool AplicaTituloDetalle { get; set; }
        /// <summary>
        /// Indica si aplica nombre en el detalle.
        /// </summary>
        public bool AplicaNombreDetalle { get; set; }
        /// <summary>
        /// Indica si aplica pie de página en el detalle.
        /// </summary>
        public bool AplicaPieDePagina { get; set; }
        /// <summary>
        /// Indica si aplica descripción en la solución.
        /// </summary>
        public bool AplicaDescripcionSolucion { get; set; }
        /// <summary>
        /// Indica si aplica título en la solución.
        /// </summary>
        public bool AplicaTituloSolucion { get; set; }
        /// <summary>
        /// Indica si aplica subtítulo en la solución.
        /// </summary>
        public bool AplicaSubTituloSolucion { get; set; }
        /// <summary>
        /// Estado del registro.
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión de la fila para control de concurrencia.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual TProgramaGeneralProblemaFactorDetalle? IdProgramaGeneralProblemaFactorDetalleNavigation { get; set; }
        public virtual TProgramaGeneralProblemaFactor? IdProgramaGeneralProblemaFactorNavigation { get; set; }
        public virtual ICollection<TProgramaGeneralProblemaFactorSubSolucionAsignadum> TProgramaGeneralProblemaFactorSubSolucionAsignada { get; set; }
    }
}
