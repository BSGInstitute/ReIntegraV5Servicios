using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppConfiguracionEnvio
    {
        public TWhatsAppConfiguracionEnvio()
        {
            TWhatsAppMensajePublicidads = new HashSet<TWhatsAppMensajePublicidad>();
        }

        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de Publicidad Whatsapp
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de Publicidad Whatsapp
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Llave foranea de la tabla gp.T_personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_Plantilla
        /// </summary>
        public int IdPlantilla { get; set; }
        /// <summary>
        /// clave foranea de la tabla mkt.T_ConjuntoListaDetalle
        /// </summary>
        public int IdConjuntoListaDetalle { get; set; }
        /// <summary>
        /// Fecha en la que quedo inactivo el registro
        /// </summary>
        public DateTime? FechaDesactivacion { get; set; }
        /// <summary>
        /// si esta activo o inactivo la configuracion
        /// </summary>
        public bool Activo { get; set; }
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
        public int? IdCampaniaGeneralDetalle { get; set; }

        public virtual TConjuntoListaDetalle IdConjuntoListaDetalleNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TPlantilla IdPlantillaNavigation { get; set; } = null!;
        public virtual ICollection<TWhatsAppMensajePublicidad> TWhatsAppMensajePublicidads { get; set; }
    }
}
