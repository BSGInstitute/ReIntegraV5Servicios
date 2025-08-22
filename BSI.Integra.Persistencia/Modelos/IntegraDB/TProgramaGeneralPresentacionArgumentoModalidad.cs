using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la conexion entre tablas ProgramaGeneralPresentacionArgumento con ModalidaCurso
    /// </summary>
    public partial class TProgramaGeneralPresentacionArgumentoModalidad
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_ProgramaGeneralProblema
        /// </summary>
        public int IdProgramaGeneralPresentacionArgumento { get; set; }
        /// <summary>
        /// FK de T_ModalidadCurso
        /// </summary>
        public int IdModalidadCurso { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string? Nombre { get; set; }
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

        public virtual TProgramaGeneralPresentacionArgumento IdProgramaGeneralPresentacionArgumentoNavigation { get; set; } = null!;
    }
}
