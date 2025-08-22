using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFeedbackConfigurar
    {
        public TFeedbackConfigurar()
        {
            TFeedbackConfigurarDetalles = new HashSet<TFeedbackConfigurarDetalle>();
            TFeedbackConfigurarGrupoPregunta = new HashSet<TFeedbackConfigurarGrupoPreguntum>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_FeedbackTipo
        /// </summary>
        public int IdFeedbackTipo { get; set; }
        /// <summary>
        /// Nombre del registro de la configuracion del feedback
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Creado o eliminado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico  Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Identificador para migracion
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TFeedbackTipo IdFeedbackTipoNavigation { get; set; } = null!;
        public virtual ICollection<TFeedbackConfigurarDetalle> TFeedbackConfigurarDetalles { get; set; }
        public virtual ICollection<TFeedbackConfigurarGrupoPreguntum> TFeedbackConfigurarGrupoPregunta { get; set; }
    }
}
