using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los argumentos  de presenteacion de Programa
    /// </summary>
    public partial class TProgramaGeneralPresentacionArgumento
    {
        public TProgramaGeneralPresentacionArgumento()
        {
            TProgramaGeneralPresentacionArgumentoDetalleSolucions = new HashSet<TProgramaGeneralPresentacionArgumentoDetalleSolucion>();
            TProgramaGeneralPresentacionArgumentoModalidads = new HashSet<TProgramaGeneralPresentacionArgumentoModalidad>();
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
        /// Nombre de factor de Argumento
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Descripcion del Argumento
        /// </summary>
        public string? Descripcion { get; set; }
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

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TProgramaGeneralPresentacionArgumentoDetalleSolucion> TProgramaGeneralPresentacionArgumentoDetalleSolucions { get; set; }
        public virtual ICollection<TProgramaGeneralPresentacionArgumentoModalidad> TProgramaGeneralPresentacionArgumentoModalidads { get; set; }
    }
}
