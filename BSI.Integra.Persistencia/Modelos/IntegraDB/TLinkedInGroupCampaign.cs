using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena Grupos de Campaña de LinkedIn recibidos a travez de la Api de LeadsLinkedIn
    /// </summary>
    public partial class TLinkedInGroupCampaign
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nombre del Grupo Campaña
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Informacion del Grupo Campaña
        /// </summary>
        public string? ServingStatuses { get; set; }
        /// <summary>
        /// Estado el Grupo Campaña
        /// </summary>
        public string? EstadoGroupCampaign { get; set; }
        /// <summary>
        /// Cuando fue creado o modificado el Grupo Campaña
        /// </summary>
        public DateTime? UltimaModificacion { get; set; }
        /// <summary>
        /// Estado del Grupo Campaña
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Grupo Campaña
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Grupo Campaña
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Grupo Campaña
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Grupo Campaña
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
