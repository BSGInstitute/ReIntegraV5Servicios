using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAreaCapacitacion
    {
        public TAreaCapacitacion()
        {
            TAreaCampaniaMailingDetalles = new HashSet<TAreaCampaniaMailingDetalle>();
            TAreaParametroSeoPws = new HashSet<TAreaParametroSeoPw>();
            TCampaniaGeneralDetalleAreas = new HashSet<TCampaniaGeneralDetalleArea>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del area
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion o resumen del area
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Imagen principal
        /// </summary>
        public string? ImgPortada { get; set; }
        /// <summary>
        /// Imagen modificada
        /// </summary>
        public string? ImgSecundaria { get; set; }
        /// <summary>
        /// Descripcion seo de la imagen para SEO
        /// </summary>
        public string? ImgPortadaAlt { get; set; }
        /// <summary>
        /// Descripcion seo de la imagen para SEO
        /// </summary>
        public string? ImgSecundariaAlt { get; set; }
        /// <summary>
        /// Creado o eliminado
        /// </summary>
        public bool EsVisibleWeb { get; set; }
        /// <summary>
        /// Identificador del area de la web
        /// </summary>
        public int? IdArea { get; set; }
        /// <summary>
        /// Publicar en la web
        /// </summary>
        public bool EsWeb { get; set; }
        public string? DescripcionHtml { get; set; }
        /// <summary>
        /// FK T_AreaCapacitacionFacebook
        /// </summary>
        public int? IdAreaCapacitacionFacebook { get; set; }
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Color del area capacitacion
        /// </summary>
        public string? ColorArea { get; set; }
        /// <summary>
        /// Url del Icono del Area capacitacion
        /// </summary>
        public string? UrlIconoArea { get; set; }

        public virtual ICollection<TAreaCampaniaMailingDetalle> TAreaCampaniaMailingDetalles { get; set; }
        public virtual ICollection<TAreaParametroSeoPw> TAreaParametroSeoPws { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleArea> TCampaniaGeneralDetalleAreas { get; set; }
    }
}
