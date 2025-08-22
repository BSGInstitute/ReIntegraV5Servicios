using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFeedbackConfigurarGrupoPreguntum
    {
        public TFeedbackConfigurarGrupoPreguntum()
        {
            TFeedbackGrupoPreguntaProgramaEspecificos = new HashSet<TFeedbackGrupoPreguntaProgramaEspecifico>();
            TFeedbackGrupoPreguntaProgramaGenerals = new HashSet<TFeedbackGrupoPreguntaProgramaGeneral>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Itentificador de la tabla pla.T_FeedbackConfiguracion
        /// </summary>
        public int IdFeedbackConfigurar { get; set; }
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

        public virtual TFeedbackConfigurar IdFeedbackConfigurarNavigation { get; set; } = null!;
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaEspecifico> TFeedbackGrupoPreguntaProgramaEspecificos { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaGeneral> TFeedbackGrupoPreguntaProgramaGenerals { get; set; }
    }
}
