using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TActividadDetalle
    {
        public TActividadDetalle()
        {
            TLlamadaActividads = new HashSet<TLlamadaActividad>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadesCabecera
        /// </summary>
        public int? IdActividadCabecera { get; set; }
        /// <summary>
        /// Fecha de programacion
        /// </summary>
        public DateTime? FechaProgramada { get; set; }
        /// <summary>
        /// Fecha real
        /// </summary>
        public DateTime? FechaReal { get; set; }
        /// <summary>
        /// Duracion real
        /// </summary>
        public int? DuracionReal { get; set; }
        /// <summary>
        /// Ocurrencia en la actividad [dbo].[TCRM_Ocurrencia]
        /// </summary>
        public int? IdOcurrencia { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_EstadoActividadDetalle
        /// </summary>
        public int IdEstadoActividadDetalle { get; set; }
        /// <summary>
        /// Comentario al detalle realizado
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Nomenclatura del actor
        /// </summary>
        public string? Actor { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Estado de la actividad Es Foreing Key  con tcentrallamadas
        /// </summary>
        public int? IdCentralLlamada { get; set; }
        /// <summary>
        /// Referencia de la llamada
        /// </summary>
        public string? RefLlamada { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OcurrenciaActividad
        /// </summary>
        public int? IdOcurrenciaActividad { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Fecha para que no se muestre whatsapp en la agenda Operaciones
        /// </summary>
        public DateTime? FechaOcultarWhatsapp { get; set; }
        /// <summary>
        /// FK de T_OcurrenciaAlterno
        /// </summary>
        public int? IdOcurrenciaAlterno { get; set; }
        /// <summary>
        /// FK de T_OcurrenciaActividadAlterno
        /// </summary>
        public int? IdOcurrenciaActividadAlterno { get; set; }
        /// <summary>
        /// Fecha para que no se muestre el chat en el portal
        /// </summary>
        public DateTime? FechaOcultarChatPortal { get; set; }

        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
        public virtual ICollection<TLlamadaActividad> TLlamadaActividads { get; set; }
    }
}
