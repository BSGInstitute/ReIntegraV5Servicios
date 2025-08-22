using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppEstadoMensajeEnviado
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador unico del mensaje asociado que se envio a un determinado numero celular
        /// </summary>
        public string? WaId { get; set; }
        /// <summary>
        /// Numero del celular del al cual se tenvio el mensaje
        /// </summary>
        public string? WaRecipientId { get; set; }
        /// <summary>
        /// Nombre de los estados del mensaje enviado los cuales son read, delivered y sent
        /// </summary>
        public string? WaStatus { get; set; }
        /// <summary>
        /// tiempo del estado del mesaje segun cada estado
        /// </summary>
        public string? WaTimeStamp { get; set; }
        /// <summary>
        /// codigo de pais asociado al numero
        /// </summary>
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
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
    }
}
