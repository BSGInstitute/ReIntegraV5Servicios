using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla registra Solucion de los problemas de los clientes de la empresa
    /// </summary>
    public partial class TProgramaGeneralProblemaFactorSolucion
    {
        public TProgramaGeneralProblemaFactorSolucion()
        {
            TProgramaGeneralProblemaFactorSubSolucions = new HashSet<TProgramaGeneralProblemaFactorSubSolucion>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre Solucion del Problea del cliente
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Titulo Solucion del problema del cliente
        /// </summary>
        public string? Titulo { get; set; }
        /// <summary>
        /// SubTitulo Solucion del problema del cliente
        /// </summary>
        public string? SubTitulo { get; set; }
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

        public virtual ICollection<TProgramaGeneralProblemaFactorSubSolucion> TProgramaGeneralProblemaFactorSubSolucions { get; set; }
    }
}
