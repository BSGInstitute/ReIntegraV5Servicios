using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralProblemaDetalleSolucion
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk de T_ProgramaGeneralProblema
        /// </summary>
        public int IdProgramaGeneralProblema { get; set; }
        /// <summary>
        /// Detalle del problema
        /// </summary>
        public string Detalle { get; set; } = null!;
        /// <summary>
        /// Solucion del problema
        /// </summary>
        public string Solucion { get; set; } = null!;
        /// <summary>
        /// FK de T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
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
        /// <summary>
        /// Id de migracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TProgramaGeneralProblema IdProgramaGeneralProblemaNavigation { get; set; } = null!;
    }
}
