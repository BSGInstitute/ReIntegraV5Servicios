using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMaterialPespecificoDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialPEspecifico
        /// </summary>
        public int IdMaterialPespecifico { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialVersion
        /// </summary>
        public int IdMaterialVersion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialEstado
        /// </summary>
        public int IdMaterialEstado { get; set; }
        /// <summary>
        /// Almacena el nombre archivo
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// Almacena la url del archivo en el repositorio web
        /// </summary>
        public string UrlArchivo { get; set; } = null!;
        /// <summary>
        /// Almacena la fecha en que se subio el archivo
        /// </summary>
        public DateTime? FechaSubida { get; set; }
        /// <summary>
        /// Almacena el comentario de subida del archivo
        /// </summary>
        public string? ComentarioSubida { get; set; }
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
        /// Usuario de creacion del registro
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
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla fin.T_Fur
        /// </summary>
        public int? IdFur { get; set; }
        /// <summary>
        /// Almacena la fecha de entrega del material
        /// </summary>
        public DateTime? FechaEntrega { get; set; }
        /// <summary>
        /// Almacena la direccion de entrega del material
        /// </summary>
        public string? DireccionEntrega { get; set; }
        /// <summary>
        /// Almacena el usuario que aprobo el material
        /// </summary>
        public string? UsuarioAprobacion { get; set; }
        /// <summary>
        /// Almacena la fecha en que se aprobo el material
        /// </summary>
        public DateTime? FechaAprobacion { get; set; }
        /// <summary>
        /// FK T_EstadoRegistroMaterial
        /// </summary>
        public int? IdEstadoRegistroMaterial { get; set; }
        /// <summary>
        /// Almacena el usuario que envio el material
        /// </summary>
        public string? UsuarioEnvio { get; set; }
        /// <summary>
        /// Almacena la fecha en que se envio el material
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// Almacena el usuario que subio el archivo
        /// </summary>
        public string? UsuarioSubida { get; set; }

        public virtual TFur? IdFurNavigation { get; set; }
        public virtual TMaterialEstado IdMaterialEstadoNavigation { get; set; } = null!;
        public virtual TMaterialPespecifico IdMaterialPespecificoNavigation { get; set; } = null!;
        public virtual TMaterialVersion IdMaterialVersionNavigation { get; set; } = null!;
    }
}
