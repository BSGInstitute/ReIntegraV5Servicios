using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla alamacena las encuestas ONline
    /// </summary>
    public partial class TEncuestaOnline
    {
        /// <summary>
        /// pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// nombre de encuesta
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// codigo de encuesta
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// descripcion de encuesta
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion de registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion de registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion de registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion de registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// rowversion de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// version de encuesta
        /// </summary>
        public int? Version { get; set; }
        /// <summary>
        /// Es FK T_TipoEncuesta
        /// </summary>
        public int? IdTipoEncuesta { get; set; }
        /// <summary>
        /// Es FK T_ModalidadCurso
        /// </summary>
        public int? IdModalidadCurso { get; set; }

        public virtual TModalidadCurso? IdModalidadCursoNavigation { get; set; }
        public virtual TTipoEncuestum? IdTipoEncuestaNavigation { get; set; }
    }
}
