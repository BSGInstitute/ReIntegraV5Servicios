using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TChatbotUsuarioContacto
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// guarda el IdPGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// guarda el IdContactoPortalSegmento
        /// </summary>
        public Guid? IdContactoPortalSegmento { get; set; }
        /// <summary>
        /// guarda el IdAlumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// si el Chat esta Finalizado
        /// </summary>
        public bool? ChatFinalizado { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
