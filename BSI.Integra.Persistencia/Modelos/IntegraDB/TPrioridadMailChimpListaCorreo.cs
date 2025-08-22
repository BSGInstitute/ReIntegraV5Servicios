using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPrioridadMailChimpListaCorreo
    {
        public TPrioridadMailChimpListaCorreo()
        {
            TWhatsAppConfiguracionEnvioDetalles = new HashSet<TWhatsAppConfiguracionEnvioDetalle>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public int IdPrioridadMailChimpLista { get; set; }
        public int? IdAlumno { get; set; }
        public string Email1 { get; set; } = null!;
        /// <summary>
        /// Nombre del alumno
        /// </summary>
        public string Nombre1 { get; set; } = null!;
        /// <summary>
        /// Apellido Paterno del alumno
        /// </summary>
        public string ApellidoPaterno { get; set; } = null!;
        /// <summary>
        /// Id del codigo pais
        /// </summary>
        public int IdCodigoPais { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniamailing
        /// </summary>
        public int IdCampaniaMailing { get; set; }
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
        /// Indica si el correo se subio correctamente al proveedor
        /// </summary>
        public bool? EsSubidoCorrectamente { get; set; }
        /// <summary>
        /// Almacena todos los detalles de mailchimp 
        /// </summary>
        public string? ObjetoSerializado { get; set; }
        /// <summary>
        /// Almacena el estado de suscripcion del cliente en mailchimp
        /// </summary>
        public string? EstadoSuscripcionMailChimp { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniageneral
        /// </summary>
        public int? IdCampaniaGeneral { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; } = null!;
        public virtual ICollection<TWhatsAppConfiguracionEnvioDetalle> TWhatsAppConfiguracionEnvioDetalles { get; set; }
    }
}
