using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFormularioSolicitud
    {
        public TFormularioSolicitud()
        {
            TFormularioPlantillas = new HashSet<TFormularioPlantilla>();
        }

        /// <summary>
        /// Clave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla T_FormularioRespuesta
        /// </summary>
        public int? IdFormularioRespuesta { get; set; }
        /// <summary>
        /// Nombre formulario solicitud
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo formulario solicitud
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Nombre campaña
        /// </summary>
        public string Campanha { get; set; } = null!;
        /// <summary>
        /// Clave Foranea de la Tabla T_ConjuntoAnuncio
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// Nombre proveedor
        /// </summary>
        public string Proveedor { get; set; } = null!;
        /// <summary>
        /// Clave Foranea de la Tabla T_FormularioSolicitudTextoBoton
        /// </summary>
        public int IdFormularioSolicitudTextoBoton { get; set; }
        /// <summary>
        /// Tipo segmento
        /// </summary>
        public int TipoSegmento { get; set; }
        /// <summary>
        /// Codigo segmento
        /// </summary>
        public string CodigoSegmento { get; set; } = null!;
        /// <summary>
        /// Tipo de evento
        /// </summary>
        public int TipoEvento { get; set; }
        /// <summary>
        /// Url boton de invitacion
        /// </summary>
        public string? UrlbotonInvitacionPagina { get; set; }
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
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TFormularioPlantilla> TFormularioPlantillas { get; set; }
    }
}
