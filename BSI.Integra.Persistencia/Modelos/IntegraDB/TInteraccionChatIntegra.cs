using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TInteraccionChatIntegra
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la tabla TCRM_ChatIntegra_HistorialAsesor
        /// </summary>
        public int? IdChatIntegraHistorialAsesor { get; set; }
        /// <summary>
        /// Id del alumno de la tabla talumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Cookie del alumno de la tabla TCRM_CONTACTOPORTALSEGMENTO en la bd del portal
        /// </summary>
        public Guid? IdContactoPortalSegmento { get; set; }
        /// <summary>
        /// Id de la tabla  TMK_TipoInteraccion
        /// </summary>
        public int? IdTipoInteraccion { get; set; }
        /// <summary>
        /// Id de la tabla TPLA_Pgeneral (int)
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Id de la tabla TPW_SubAreaCapacitacion
        /// </summary>
        public int? IdSubAreaCapacitacion { get; set; }
        /// <summary>
        /// Id de la tabla TPW_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacion { get; set; }
        /// <summary>
        /// Ip del visitante
        /// </summary>
        public string? Ip { get; set; }
        /// <summary>
        /// Nombre del pais del visitante
        /// </summary>
        public string? Pais { get; set; }
        /// <summary>
        /// Nombre de la region del visitante
        /// </summary>
        public string? Region { get; set; }
        /// <summary>
        /// Nombre de la ciudad del visitante
        /// </summary>
        public string? Ciudad { get; set; }
        /// <summary>
        /// Duracion total de la conversacion en segundos
        /// </summary>
        public int? Duracion { get; set; }
        /// <summary>
        /// Nro de mensajes enviados en total en la conversacion
        /// </summary>
        public int? NroMensajes { get; set; }
        /// <summary>
        /// Nro de mensajes enviados en total por el visitante
        /// </summary>
        public int? NroPalabrasVisitor { get; set; }
        /// <summary>
        /// Nro de mensajes enviados en total por el asesor
        /// </summary>
        public int? NroPalabrasAgente { get; set; }
        /// <summary>
        /// Tiempo maximo en responder el asesor (en seg)
        /// </summary>
        public int? UsuarioTiempoRespuestaMaximo { get; set; }
        /// <summary>
        /// Tiempo promedio en responder el asesor (en seg)
        /// </summary>
        public int? UsuarioTiempoRespuestaPromedio { get; set; }
        /// <summary>
        /// Fecha y hora de inicio del chat
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha y hora de fin del chat
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Flag que valida si ya esta leido el mensaje
        /// </summary>
        public bool? Leido { get; set; }
        /// <summary>
        /// Plataforma del visitante
        /// </summary>
        public string? Plataforma { get; set; }
        /// <summary>
        /// Navegador del visitante
        /// </summary>
        public string? Navegador { get; set; }
        /// <summary>
        /// Url de donde viene el visitante
        /// </summary>
        public string? UrlFrom { get; set; }
        /// <summary>
        /// Url de donde esta el visitante
        /// </summary>
        public string? UrlTo { get; set; }
        /// <summary>
        /// Llave foranea a la tabla com.T_TipoInteraccionChatIntegra
        /// </summary>
        public int IdEstadoChat { get; set; }
        /// <summary>
        /// Id de la campania asociada de la tabla TFM_ConjuntoAnuncios
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// Id de la sesion de chat que genera el signalR
        /// </summary>
        public Guid IdChatSession { get; set; }
        /// <summary>
        /// Id de la tabla TCRM_FaseOportunidadPortal de la bd del portal
        /// </summary>
        public Guid? IdFaseOportunidadPortalWeb { get; set; }
        /// <summary>
        /// Estado del registro activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
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
        /// Tiempo que el cliente espera antes de desconectarse del chat
        /// </summary>
        public decimal? ClienteTiempoEspera { get; set; }
        /// <summary>
        /// Contador que ayudara a promediar los tiempos de respuesta de acuerdo al grupo de mensajes &apos;asesor&apos;, &apos;visitante&apos;
        /// </summary>
        public int? ContadorUsuarioPromedioRespuesta { get; set; }
        /// <summary>
        /// Tiempo total de respuesta del asesor en el chat
        /// </summary>
        public decimal? TiempoRespuestaTotal { get; set; }
        /// <summary>
        /// Cantidad de mensajes sin lectura
        /// </summary>
        public int? NroMensajesSinLeer { get; set; }
    }
}
