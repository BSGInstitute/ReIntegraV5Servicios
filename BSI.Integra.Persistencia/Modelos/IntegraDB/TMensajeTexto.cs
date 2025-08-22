using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMensajeTexto
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK con la tabla T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// FK con la tabla T_MatriculaCabecera
        /// </summary>
        public string? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Mensaje enviado
        /// </summary>
        public string Mensaje { get; set; } = null!;
        /// <summary>
        /// Numero de Destino
        /// </summary>
        public string Numero { get; set; } = null!;
        /// <summary>
        /// Codigo de Pais
        /// </summary>
        public int CodigoPais { get; set; }
        /// <summary>
        /// Id del envio en Twilio
        /// </summary>
        public string IdSeguimientoTwilio { get; set; } = null!;
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
