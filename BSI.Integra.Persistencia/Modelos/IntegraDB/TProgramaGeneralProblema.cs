using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralProblema
    {
        public TProgramaGeneralProblema()
        {
            TProgramaGeneralProblemaDetalleSolucions = new HashSet<TProgramaGeneralProblemaDetalleSolucion>();
            TProgramaGeneralProblemaModalidads = new HashSet<TProgramaGeneralProblemaModalidad>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre de factor de motivacion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Valida si sera visible en agenda
        /// </summary>
        public bool EsVisibleAgenda { get; set; }
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
        /// <summary>
        /// Descripcion del Problema
        /// </summary>
        public string? Descripcion { get; set; }

        public virtual ICollection<TProgramaGeneralProblemaDetalleSolucion> TProgramaGeneralProblemaDetalleSolucions { get; set; }
        public virtual ICollection<TProgramaGeneralProblemaModalidad> TProgramaGeneralProblemaModalidads { get; set; }
    }
}
