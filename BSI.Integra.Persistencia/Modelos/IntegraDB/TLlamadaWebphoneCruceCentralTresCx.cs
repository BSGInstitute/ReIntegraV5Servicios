using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLlamadaWebphoneCruceCentralTresCx
    {
        public TLlamadaWebphoneCruceCentralTresCx()
        {
            TTranscripcionLlamada = new HashSet<TTranscripcionLlamadum>();
        }

        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// campo IdLlamada de CentralLlamada.dbo.Segmento
        /// </summary>
        public int IdLlamadaCentral { get; set; }
        /// <summary>
        /// Fecha inicio de llamada
        /// </summary>
        public DateTime FechaInicioLlamadaCentral { get; set; }
        /// <summary>
        /// Fecha fin de llamada
        /// </summary>
        public DateTime FechaFinLlamadaCentral { get; set; }
        /// <summary>
        /// Anexo que realiza llamada
        /// </summary>
        public string? AnexoCentral { get; set; }
        /// <summary>
        /// Duraction timbrado de llamada
        /// </summary>
        public int DuracionTimbradoCentral { get; set; }
        /// <summary>
        /// Duracion contestado de llamada
        /// </summary>
        public int DuracionContestoCentral { get; set; }
        /// <summary>
        /// (PK) de mkt.T_Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// (PK) de com.T_ActividadDetalle
        /// </summary>
        public int? IdActividadDetalle { get; set; }
        public string? TelefonoDestinoReal { get; set; }
        public string? TelefonoDestino { get; set; }
        /// <summary>
        /// Estado de llamada
        /// </summary>
        public string EstadoLlamadaCentral { get; set; } = null!;
        /// <summary>
        /// Sub Estado de llamada
        /// </summary>
        public string SubEstadoLlamadaCentral { get; set; } = null!;
        /// <summary>
        /// Url de reproduccion del audio
        /// </summary>
        public string? UrlAudio { get; set; }
        /// <summary>
        /// Troncal de llamada
        /// </summary>
        public string? Troncal { get; set; }
        /// <summary>
        /// Flag llamada realizada desde 3cx
        /// </summary>
        public bool? LlamadaDesdeTresCx { get; set; }
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
        public string? NombreGrabacion { get; set; }
        public bool? GrabacionContrato { get; set; }
        public int? NroBytes { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string? Origen { get; set; }
        public int? IdPersonal { get; set; }
        public string? CodigoLlamada { get; set; }

        public virtual ICollection<TTranscripcionLlamadum> TTranscripcionLlamada { get; set; }
    }
}
