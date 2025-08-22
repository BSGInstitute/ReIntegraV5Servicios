using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSolicitudOperacione
    {
        public TSolicitudOperacione()
        {
            TSolicitudOperacionesAccesoTemporalDetalles = new HashSet<TSolicitudOperacionesAccesoTemporalDetalle>();
        }

        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// llave foranea de la tabla com.T_Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_TipoSolicitudOperaciones
        /// </summary>
        public int IdTipoSolicitudOperaciones { get; set; }
        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime FechaSolicitud { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_personal, indica el usuario Solicitante
        /// </summary>
        public int IdPersonalSolicitante { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_personal, indica el usuario de Aprobacion
        /// </summary>
        public int IdPersonalAprobacion { get; set; }
        /// <summary>
        /// valor que va a ser modificado
        /// </summary>
        public string ValorAnterior { get; set; } = null!;
        /// <summary>
        /// valor a la que va a ser modificado
        /// </summary>
        public string ValorNuevo { get; set; } = null!;
        /// <summary>
        /// Indica si ha sido aprobado
        /// </summary>
        public bool Aprobado { get; set; }
        /// <summary>
        /// Indica si es Cancelado
        /// </summary>
        public bool EsCancelado { get; set; }
        /// <summary>
        /// Comentarios de solicitud
        /// </summary>
        public string? ComentarioSolicitante { get; set; }
        /// <summary>
        /// Observacion de Personal Encargado
        /// </summary>
        public string? Observacion { get; set; }
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
        /// <summary>
        /// Llave foranea de la tabla ope.T_UrlBlockStorage
        /// </summary>
        public int? IdUrlBlockStorage { get; set; }
        /// <summary>
        /// Nombre del archivo adjunto
        /// </summary>
        public string? NombreArchivo { get; set; }
        /// <summary>
        /// Extension del archivo
        /// </summary>
        public string? ContentType { get; set; }
        /// <summary>
        /// Indica si se realizo
        /// </summary>
        public bool? Realizado { get; set; }
        /// <summary>
        /// Observacion por el encargado de realizar la solicitud
        /// </summary>
        public string? ObservacionEncargado { get; set; }
        /// <summary>
        /// Fecha en la que fue aprobado
        /// </summary>
        public DateTime? FechaAprobacion { get; set; }
        /// <summary>
        /// Relacion de cambio de Estado con Subestado mediante replica de Id en cambios de Estado
        /// </summary>
        public int? RelacionEstadoSubEstado { get; set; }
        /// <summary>
        /// Indica Envio Automatico
        /// </summary>
        public bool? EnvioAutomatico { get; set; }

        public virtual ICollection<TSolicitudOperacionesAccesoTemporalDetalle> TSolicitudOperacionesAccesoTemporalDetalles { get; set; }
    }
}
