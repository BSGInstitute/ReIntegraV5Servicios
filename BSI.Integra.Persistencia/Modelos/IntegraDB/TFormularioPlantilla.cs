using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFormularioPlantilla
    {
        public TFormularioPlantilla()
        {
            TConjuntoAnuncios = new HashSet<TConjuntoAnuncio>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la plantilla del Formulario
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// LLave foranea con la tabla T_FormularioSolicitud
        /// </summary>
        public int? IdFormularioSolicitud { get; set; }
        /// <summary>
        /// LLave foranea con la tabla T_FormularioLandingPage
        /// </summary>
        public int? IdFormularioLandingPage { get; set; }
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
        public int? IdMigracion { get; set; }

        public virtual TFormularioLandingPage? IdFormularioLandingPageNavigation { get; set; }
        public virtual TFormularioSolicitud? IdFormularioSolicitudNavigation { get; set; }
        public virtual ICollection<TConjuntoAnuncio> TConjuntoAnuncios { get; set; }
    }
}
