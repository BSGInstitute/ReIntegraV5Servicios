using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena Campañas de LinkedIn recibidos a travez de la Api de LeadsLinkedIn
    /// </summary>
    public partial class TLinkedInCampaign
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Id del Grupo Campaña recibida por LinkedIn
        /// </summary>
        public long? IdLinkedInGroupCampaign { get; set; }
        /// <summary>
        /// Nombre de la Campaña recibida por LinkedIn
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Estado de la Campaña recibida por LinkedIn
        /// </summary>
        public string? EstadoCampaign { get; set; }
        public string? ObjectiveType { get; set; }
        /// <summary>
        /// Informacion de la Campaña de Linkedin
        /// </summary>
        public string? PacingStrategy { get; set; }
        /// <summary>
        /// Tipo de Campaña recibida por LinkedIn
        /// </summary>
        public string? TypeCampaign { get; set; }
        /// <summary>
        /// Fecha de creacion de la  Campaña recibida por LinkedIn
        /// </summary>
        public DateTime? UltimaModificacion { get; set; }
        /// <summary>
        /// Estado de la Camapaña
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion de la Campaña
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion de la Campaña
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion de la Campaña
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion de la Campaña
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
