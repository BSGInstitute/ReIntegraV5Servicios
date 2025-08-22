using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAnuncioFacebook
    {
        public TAnuncioFacebook()
        {
            TAnuncioFacebookMetricas = new HashSet<TAnuncioFacebookMetrica>();
            TOportunidads = new HashSet<TOportunidad>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id original de Facebook del anuncio
        /// </summary>
        public string? FacebookIdAnuncio { get; set; }
        /// <summary>
        /// Nombre original de Facebook del anuncio
        /// </summary>
        public string? FacebookNombreAnuncio { get; set; }
        /// <summary>
        /// Id original de Facebook del conjunto del anuncio
        /// </summary>
        public string? FacebookIdConjuntoAnuncio { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_ConjuntoAnuncioFacebook
        /// </summary>
        public int? IdConjuntoAnuncioFacebook { get; set; }
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
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de V3
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TConjuntoAnuncioFacebook? IdConjuntoAnuncioFacebookNavigation { get; set; }
        public virtual ICollection<TAnuncioFacebookMetrica> TAnuncioFacebookMetricas { get; set; }
        public virtual ICollection<TOportunidad> TOportunidads { get; set; }
    }
}
