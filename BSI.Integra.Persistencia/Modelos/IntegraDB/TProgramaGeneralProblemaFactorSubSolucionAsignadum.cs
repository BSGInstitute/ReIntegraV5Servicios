using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que asigna subsoluciones de factores a los detalles de problemas generales.
    /// </summary>
    public partial class TProgramaGeneralProblemaFactorSubSolucionAsignadum
    {
        /// <summary>
        /// Identificador único de la asignación.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Referencia al detalle del problema general.
        /// </summary>
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        /// <summary>
        /// Referencia a la subsolución del factor del problema.
        /// </summary>
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
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

        public virtual TProgramaGeneralProblemaDetalle IdProgramaGeneralProblemaDetalleNavigation { get; set; } = null!;
        public virtual TProgramaGeneralProblemaFactorSubSolucion IdProgramaGeneralProblemaFactorSubSolucionNavigation { get; set; } = null!;
    }
}
