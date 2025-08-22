using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFeedbackConfigurarDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_FeedbackConfigurar
        /// </summary>
        public int IdFeedbackConfigurar { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.IdSexo
        /// </summary>
        public int IdSexo { get; set; }
        /// <summary>
        /// El puntaje a registrar en al configuracion
        /// </summary>
        public int Puntaje { get; set; }
        /// <summary>
        /// Nombre del video para visualizar el feedback
        /// </summary>
        public string NombreVideo { get; set; } = null!;
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
        /// <summary>
        /// Se ingresa el orden en el que se visua el video segun las respuestas de sus respectivas evaluaciones
        /// </summary>
        public int? OrdenVideo { get; set; }

        public virtual TFeedbackConfigurar IdFeedbackConfigurarNavigation { get; set; } = null!;
        public virtual TSexo IdSexoNavigation { get; set; } = null!;
    }
}
