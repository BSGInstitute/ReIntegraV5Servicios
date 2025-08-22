using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las conexiones entre tablas oportunidad y programaGeneralPresentacionArgumentoDetalleSolucion
    /// </summary>
    public partial class TProgramaGeneralPresentacionArgumentoDetalleSolucionRespuestum
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
        /// FK de T_ProgramaGeneralProblemaDetalleSolucion
        /// </summary>
        public int IdProgramaGeneraPresentacionArgumentoDetalleSolucion { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string? UsuarioModificacion { get; set; }
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
    }
}
