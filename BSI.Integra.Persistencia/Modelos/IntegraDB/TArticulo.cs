using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TArticulo
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador para la busqueda en el portal web
        /// </summary>
        public int IdWeb { get; set; }
        /// <summary>
        /// Nombre del articulo
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Titulo del articulo
        /// </summary>
        public string Titulo { get; set; } = null!;
        /// <summary>
        /// Imagen principal del articulo para la portada
        /// </summary>
        public string? ImgPortada { get; set; }
        /// <summary>
        /// Desccripcion de la imagen principal para SEO de etiqueta alt
        /// </summary>
        public string? ImgPortadaAlt { get; set; }
        /// <summary>
        /// Imagen secundaria del articulo
        /// </summary>
        public string? ImgSecundaria { get; set; }
        /// <summary>
        /// Desccripcion de la imagen secundario para SEO de etiqueta alt
        /// </summary>
        public string? ImgSecundariaAlt { get; set; }
        /// <summary>
        /// Autor del articulo
        /// </summary>
        public string? Autor { get; set; }
        /// <summary>
        /// Identificador del tipo de articulo
        /// </summary>
        public int? IdTipoArticulo { get; set; }
        /// <summary>
        /// Contenido del articulo en html
        /// </summary>
        public string? Contenido { get; set; }
        /// <summary>
        /// Identificador del Area Capacitacion
        /// </summary>
        public int? IdArea { get; set; }
        /// <summary>
        /// Identificador de la SubArea Capacitacion
        /// </summary>
        public int? IdSubArea { get; set; }
        /// <summary>
        /// Identificador del Expositor
        /// </summary>
        public int? IdExpositor { get; set; }
        /// <summary>
        /// Identificador de la Categoria
        /// </summary>
        public int? IdCategoria { get; set; }
        /// <summary>
        /// Url amigable para la direccion web del articulo
        /// </summary>
        public string? UrlWeb { get; set; }
        /// <summary>
        /// Creado o eliminado
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
        /// <summary>
        /// Url para direccionar a documentos en la nube
        /// </summary>
        public string? UrlDocumento { get; set; }
        /// <summary>
        /// Descripcion del articulo
        /// </summary>
        public string? DescripcionGeneral { get; set; }
    }
}
