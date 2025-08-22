using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPartnerPw
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre completo del partner
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Imagen o logo original
        /// </summary>
        public string? ImgPrincipal { get; set; }
        /// <summary>
        /// Descripcion breve para el atributo alt de la imagen
        /// </summary>
        public string? ImgPrincipalAlf { get; set; }
        /// <summary>
        /// Imagen secundaria, la cual no es segun los estandares del partner
        /// </summary>
        public string? ImgSecundaria { get; set; }
        /// <summary>
        /// Descripcion breve para el atributo alt de la imagen
        /// </summary>
        public string? ImgSecundariaAlf { get; set; }
        /// <summary>
        /// Descripcion completa
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Descripcion breve o corta
        /// </summary>
        public string? DescripcionCorta { get; set; }
        /// <summary>
        /// Texto adicional para el partner
        /// </summary>
        public string? Preguntas { get; set; }
        /// <summary>
        /// La pocision de como se visualizara el listado de partner
        /// </summary>
        public int Posicion { get; set; }
        /// <summary>
        /// Identificador secundario dado por el sistema antiguo de la pagina web
        /// </summary>
        public short? IdPartner { get; set; }
        /// <summary>
        /// encabezado del correo
        /// </summary>
        public string? EncabezadoCorreoPartner { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
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
    }
}
