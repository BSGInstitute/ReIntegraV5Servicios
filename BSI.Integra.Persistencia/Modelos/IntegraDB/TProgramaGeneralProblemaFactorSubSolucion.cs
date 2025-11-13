using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla registra Soluciones asociadas a una solucion de los problemas de los clientes de la empresa
    /// </summary>
    public partial class TProgramaGeneralProblemaFactorSubSolucion
    {
        public TProgramaGeneralProblemaFactorSubSolucion()
        {
            TProgramaGeneralProblemaFactorSubSolucionAsignada = new HashSet<TProgramaGeneralProblemaFactorSubSolucionAsignadum>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreign key que referencia a la tabla T_ProgramaGeneralProblemaFactorSolucion
        /// </summary>
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        /// <summary>
        /// Nombre Solucion asignada a una solucion especifica del cliente
        /// </summary>
        public string? Solucion { get; set; }
        /// <summary>
        /// Orden de las soluciones presentadas
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Nivel al que ira la Solucion
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
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
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TProgramaGeneralProblemaFactorSolucion IdProgramaGeneralProblemaFactorSolucionNavigation { get; set; } = null!;
        public virtual ICollection<TProgramaGeneralProblemaFactorSubSolucionAsignadum> TProgramaGeneralProblemaFactorSubSolucionAsignada { get; set; }
    }
}
