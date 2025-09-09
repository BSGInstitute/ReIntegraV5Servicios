using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfigurarVideoPrograma
    {
        public TConfigurarVideoPrograma()
        {
            TSesionConfigurarVideos = new HashSet<TSesionConfigurarVideo>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador  del programa general
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Identificador de la seccion del documento registrado en la tabla pla.T_DocumentoSeccion_PW
        /// </summary>
        public int IdDocumentoSeccionPw { get; set; }
        /// <summary>
        /// Codigo o identificador del video del cual nos da un proveedor
        /// </summary>
        public string VideoId { get; set; } = null!;
        /// <summary>
        /// Total de los minutos del video
        /// </summary>
        public string? TotalMinutos { get; set; }
        /// <summary>
        /// Documento que se asicia al video en diapositivas u hojas
        /// </summary>
        public string? Archivo { get; set; }
        /// <summary>
        /// Numero de diapositivas u hojas
        /// </summary>
        public string? NroDiapositivas { get; set; }
        /// <summary>
        /// Si el registro tiene configuracion
        /// </summary>
        public bool Configurado { get; set; }
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
        /// Establece si se inserta una imagen como sello en el video
        /// </summary>
        public bool? ConImagenVideo { get; set; }
        /// <summary>
        /// Registrar el nombre del archivo con la extion
        /// </summary>
        public string? ImagenVideoNombre { get; set; }
        /// <summary>
        /// El ancho de la imagen
        /// </summary>
        public string? ImagenVideoAncho { get; set; }
        /// <summary>
        /// El largo de la imagen
        /// </summary>
        public string? ImagenVideoAlto { get; set; }
        /// <summary>
        /// Establece si se inserta una imagen como sello en el diapositiva
        /// </summary>
        public bool? ConImagenDiapositiva { get; set; }
        /// <summary>
        /// Registrar el nombre del archivo con la extion
        /// </summary>
        public string? ImagenDiapositivaNombre { get; set; }
        /// <summary>
        /// El ancho de la imagen
        /// </summary>
        public string? ImagenDiapositivaAncho { get; set; }
        /// <summary>
        /// El largo de la imagen
        /// </summary>
        public string? ImagenDiapositivaAlto { get; set; }
        /// <summary>
        /// El numero de la fila que se registra el registro capitulo, seccion o subsesion
        /// </summary>
        public int? NumeroFila { get; set; }
        /// <summary>
        /// Token de autenticacion
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// Posicion de la imagen que se inserta en el video en la coordenada x
        /// </summary>
        public int? ImagenVideoPosicionX { get; set; }
        /// <summary>
        /// Posicion de la imagen que se inserta en el video en la coordenada y
        /// </summary>
        public int? ImagenVideoPosicionY { get; set; }
        /// <summary>
        /// Posicion de la imagen que se inserta en la diapositiva en la coordenada x
        /// </summary>
        public int? ImagenDiapositivaPosicionX { get; set; }
        /// <summary>
        /// Posicion de la imagen que se inserta en la diapositiva en la coordenada y
        /// </summary>
        public int? ImagenDiapositivaPosicionY { get; set; }
        /// <summary>
        /// Se registra el Id de video del proveedor brightcove
        /// </summary>
        public string? VideoIdBrightcove { get; set; }
        /// <summary>
        /// indica el estado actual de la configuracion del video 
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Se registra el Id de video del proveedor Vimeo
        /// </summary>
        public string? VideoIdVimeo { get; set; }
        /// <summary>
        /// Se registra el Id para reproduccion de Videos sea de Vimeo y/o brightcove
        /// </summary>
        public int? ReproduccionVideo { get; set; }
        /// <summary>
        /// Se registra el Id para descarga de Videos sea de Vimeo y/o brightcove
        /// </summary>
        public int? DescargaVideo { get; set; }

        public virtual TDocumentoSeccionPw IdDocumentoSeccionPwNavigation { get; set; } = null!;
        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TSesionConfigurarVideo> TSesionConfigurarVideos { get; set; }
    }
}
