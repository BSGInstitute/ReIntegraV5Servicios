using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralMaterialEstudioAdicional
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del Programa General de la tabla pla.T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre del Archivo o enlace que se visualizara
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// Es Enlace lo que se va ha poner dento de la columna EnlaceArchivo para decidir redirigir o descargar desde nuestro servidor
        /// </summary>
        public bool EsEnlace { get; set; }
        /// <summary>
        /// Enlace del Archivo de otra pagina o si es el mismo de nuestro servidor el nombre del archivo
        /// </summary>
        public string? EnlaceArchivo { get; set; }
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
        /// Identificador de migracion
        /// </summary>
        public int? IdMigracion { get; set; }
        public string? NombreConfiguracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
    }
}
