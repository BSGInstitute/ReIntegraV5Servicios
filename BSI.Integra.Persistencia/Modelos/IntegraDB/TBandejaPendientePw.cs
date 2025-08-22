using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TBandejaPendientePw
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Documento_PW
        /// </summary>
        public int? IdDocumentoPw { get; set; }
        /// <summary>
        /// Es Foreing Key T_RevisionNivel_PW
        /// </summary>
        public int IdRevisionNivelPw { get; set; }
        /// <summary>
        /// Es el tipo de secuencia que sigue
        /// </summary>
        public int Secuencia { get; set; }
        /// <summary>
        /// Si es final ya esta aprobado
        /// </summary>
        public int EsFinal { get; set; }
        /// <summary>
        /// Recien entra para aprobar
        /// </summary>
        public int EsInicio { get; set; }
        /// <summary>
        /// Quie usuario lo ha creado, Llave foranea con la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Estado del documento
        /// </summary>
        public int EstadoRevisar { get; set; }
        /// <summary>
        /// Comentario en el documento
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Comentario por el cual se rechaza el documento
        /// </summary>
        public string? ComentarioRechazar { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }

        public virtual TDocumentoPw? IdDocumentoPwNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TRevisionNivelPw IdRevisionNivelPwNavigation { get; set; } = null!;
    }
}
