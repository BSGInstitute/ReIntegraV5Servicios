using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRegistroCertificadoFisicoGenerado
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// clave foranea de la tabla mkt.T_SolicitudCertificadoFisico
        /// </summary>
        public int IdSolicitudCertificadoFisico { get; set; }
        /// <summary>
        /// clave foranea de la tabla ope.T_UrlBlockStorage
        /// </summary>
        public int IdUrlBlockStorage { get; set; }
        /// <summary>
        /// Formato del archivo generado
        /// </summary>
        public string FormatoArchivo { get; set; } = null!;
        /// <summary>
        /// nombre de archivo generado
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// ultima fecha en la que se realizo la descarga de documento
        /// </summary>
        public DateTime UltimaFechaGeneracion { get; set; }
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

        public virtual TSolicitudCertificadoFisico IdSolicitudCertificadoFisicoNavigation { get; set; } = null!;
        public virtual TUrlBlockStorage IdUrlBlockStorageNavigation { get; set; } = null!;
    }
}
