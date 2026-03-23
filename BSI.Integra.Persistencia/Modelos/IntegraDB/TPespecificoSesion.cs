using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoSesion
    {
        public TPespecificoSesion()
        {
            TGestionContactoActividadSesionCongelada = new HashSet<TGestionContactoActividadSesionCongeladum>();
            TRecuperacionSesions = new HashSet<TRecuperacionSesion>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es  foreing key T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Fecha y hora de inicio de la sesion
        /// </summary>
        public DateTime FechaHoraInicio { get; set; }
        /// <summary>
        /// Duracion en horas de la sesion
        /// </summary>
        public decimal Duracion { get; set; }
        /// <summary>
        /// Identificador del docente de la tabla T_Expositor
        /// </summary>
        public int? IdExpositor { get; set; }
        /// <summary>
        /// Descripcion de un comentario
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Indica si la sesion fue autogenerada por el sistema o no (1,0)
        /// </summary>
        public bool SesionAutoGenerada { get; set; }
        /// <summary>
        /// Es foreing key T_Ambiente
        /// </summary>
        public int? IdAmbiente { get; set; }
        /// <summary>
        /// predeterminado (1,0)
        /// </summary>
        public bool? Predeterminado { get; set; }
        /// <summary>
        /// Numero de grupo
        /// </summary>
        public int Grupo { get; set; }
        /// <summary>
        /// Indica si es una sesion de inicio
        /// </summary>
        public bool EsSesionInicio { get; set; }
        /// <summary>
        /// indica la version del cronograma
        /// </summary>
        public int Version { get; set; }
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
        /// <summary>
        /// Indica el grupo de sesion al cual pertenece.
        /// </summary>
        public int? GrupoSesion { get; set; }
        /// <summary>
        /// Indica el Id del la seison en el registro academico
        /// </summary>
        public int? IdSesionRa { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla fin.T_Proveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// URL de la plataforma Webex generada
        /// </summary>
        public string? UrlWebex { get; set; }
        /// <summary>
        /// Cuenta a utilizar en la plataforma Webex
        /// </summary>
        public int? CuentaWebex { get; set; }
        /// <summary>
        /// FK de T_ModalidadCurso
        /// </summary>
        public int? IdModalidadCurso { get; set; }
        /// <summary>
        /// Fecha de la Cancelación del Webinar
        /// </summary>
        public DateTime? FechaCancelacionWebinar { get; set; }
        /// <summary>
        /// Motivo de la cancelación del Webinar
        /// </summary>
        public string? ComentarioCancelacionWebinar { get; set; }
        /// <summary>
        /// 1 Webinar confirmado - 0 Webinar cancelado
        /// </summary>
        public bool? EsWebinarConfirmado { get; set; }
        /// <summary>
        /// Check que valida si la sesion se mostrara en el portal web
        /// </summary>
        public bool? MostrarPortalWeb { get; set; }
        /// <summary>
        /// Fk T_EstadoEnvioCorreo
        /// </summary>
        public int? IdEstadoEnvioCorreo { get; set; }
        /// <summary>
        /// Flag Envio Sesion Correo
        /// </summary>
        public bool? EnvioSesionCorreo { get; set; }
        /// <summary>
        /// Flag Envio Sesion Correo Regularizacion
        /// </summary>
        public bool? EnvioSesionCorreoRegularizacion { get; set; }
        /// <summary>
        /// Fecha Hora Regularizacion Correo
        /// </summary>
        public DateTime? FechaHoraRegularizacion { get; set; }
        /// <summary>
        /// Flag de envio automatico de correos para webinar
        /// </summary>
        public bool? EnvioAutomaticoCorreoWebinar { get; set; }
        /// <summary>
        /// Flag de envio automatico de whatssapp para webinar
        /// </summary>
        public bool? EnvioAutomaticoWhatsAppWebinar { get; set; }
        /// <summary>
        /// Flag de regularizacion de correo para webinar
        /// </summary>
        public bool? RegularizacionCorreoWebinar { get; set; }
        /// <summary>
        /// Flag de regularizacion de WhatsApp para webinar
        /// </summary>
        public bool? RegularizacionWhatsAppWebinar { get; set; }
        /// <summary>
        /// Usuario de envio de correos Webinar
        /// </summary>
        public string? UsuarioEnvioCorreoWebinar { get; set; }
        /// <summary>
        /// Usuario de envio de WhatsApp Webinar
        /// </summary>
        public string? UsuarioEnvioWhatsAppWebinar { get; set; }
        /// <summary>
        /// Fecha de regularizacion para el envio de correos
        /// </summary>
        public DateTime? FechaRegularizacionCorreoWebinar { get; set; }
        /// <summary>
        /// Fecha de regularizacion para el envio de Whatsapp
        /// </summary>
        public DateTime? FechaRegularizacionWhatsAppWebinar { get; set; }
        /// <summary>
        /// Foreign Key con la tabla de estados de sesion del programa especifico.
        /// </summary>
        public int? IdPespecificoSesionEstado { get; set; }
        /// <summary>
        /// Foreign Key con la tabla de detalle de observaciones del estado de la sesion.
        /// </summary>
        public int? IdPespecificoSesionEstadoObservacionDetalle { get; set; }
        /// <summary>
        /// Indica si la sesion fue reprogramada.
        /// </summary>
        public bool? Reprogramacion { get; set; }

        public virtual TPespecificoSesionEstado? IdPespecificoSesionEstadoNavigation { get; set; }
        public virtual TPespecificoSesionEstadoObservacionDetalle? IdPespecificoSesionEstadoObservacionDetalleNavigation { get; set; }
        public virtual ICollection<TGestionContactoActividadSesionCongeladum> TGestionContactoActividadSesionCongelada { get; set; }
        public virtual ICollection<TRecuperacionSesion> TRecuperacionSesions { get; set; }
    }
}
