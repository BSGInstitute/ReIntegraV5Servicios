using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSendinBlueDataDeEvento
    {
        /// <summary>
        /// Identificador unico del evento de webhook de sendingblue
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador unico de la campania creada pro sendinblue
        /// </summary>
        public int IdSendinBlueCampania { get; set; }
        /// <summary>
        /// Email del contacto que realizo el evento
        /// </summary>
        public string EmailContacto { get; set; } = null!;
        /// <summary>
        /// identificador unico del evento de sendinblue
        /// </summary>
        public int IdSendinBlueEventoWebHook { get; set; }
        /// <summary>
        /// Fecha que se realizo el envio
        /// </summary>
        public DateTime FechaEnvio { get; set; }
        /// <summary>
        /// fecha en la que se origino el evento
        /// </summary>
        public DateTime FechaDeEvento { get; set; }
        /// <summary>
        /// Url de evento campo que puede ser vacio
        /// </summary>
        public string? UrlEvento { get; set; }
        /// <summary>
        /// Respuesta completa obtenida
        /// </summary>
        public string JsonResponse { get; set; } = null!;
        /// <summary>
        /// Estado del registro (activo o eliminado)
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

        public virtual TSendinBlueEventoWebHook IdSendinBlueEventoWebHookNavigation { get; set; } = null!;
    }
}
