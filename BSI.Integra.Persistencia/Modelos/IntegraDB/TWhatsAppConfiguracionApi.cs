using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppConfiguracionApi
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero celular registrado y vinculado con el servidor en la cuenta de WhatsApp
        /// </summary>
        public string? Numero { get; set; }
        public string? Vname { get; set; }
        /// <summary>
        /// Codigo del pais al que pertenece elservidor al que debe de responder
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Bearer token de acceso del api
        /// </summary>
        public string? Bearer { get; set; }
        /// <summary>
        /// Numero identifiacdor del api
        /// </summary>
        public string? NumeroIndentificador { get; set; }
        /// <summary>
        /// Version del api
        /// </summary>
        public string? VersionApi { get; set; }
        /// <summary>
        /// Fecha de expiracion del api
        /// </summary>
        public DateTime FechaExpiracion { get; set; }
        /// <summary>
        /// El dato es migrado
        /// </summary>
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
        public string? NumeroWhatsApp { get; set; }
        /// <summary>
        /// Cuenta Id del whatsapp bussiness
        /// </summary>
        public string? CuentaIdentificadorWhatsApp { get; set; }
        /// <summary>
        /// Id relacion con la tabla T_PersonalAreaTrabajo
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// Area a la cual corresponde el numero
        /// </summary>
        public string? CodigoArea { get; set; }
        public int? IdPersonalAsignado { get; set; }
    }
}
