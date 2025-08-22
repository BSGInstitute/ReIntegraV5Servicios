using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaGeneralDetalle
    {
        public TCampaniaGeneralDetalle()
        {
            TCampaniaGeneralDetalleAreas = new HashSet<TCampaniaGeneralDetalleArea>();
            TCampaniaGeneralDetalleProgramas = new HashSet<TCampaniaGeneralDetallePrograma>();
            TCampaniaGeneralDetalleResponsables = new HashSet<TCampaniaGeneralDetalleResponsable>();
            TCampaniaGeneralDetalleSubAreas = new HashSet<TCampaniaGeneralDetalleSubArea>();
            TConfiguracionDeEnvioParaWhatsApps = new HashSet<TConfiguracionDeEnvioParaWhatsApp>();
            TFiltradoDeDatosPorPrioridadWhatsApps = new HashSet<TFiltradoDeDatosPorPrioridadWhatsApp>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_CampaniaGeneral
        /// </summary>
        public int IdCampaniaGeneral { get; set; }
        /// <summary>
        /// Nombre  del registro
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Numero de prioridad (1,2,3,...)
        /// </summary>
        public int Prioridad { get; set; }
        /// <summary>
        /// Asunto del mensaje
        /// </summary>
        public string Asunto { get; set; } = null!;
        /// <summary>
        /// Llave foranea de la tabla gp.T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Llave foranea de la tabla pla.T_CentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Cantidad de contactos calculados para envio de mailing
        /// </summary>
        public int? CantidadContactosMailing { get; set; }
        /// <summary>
        /// Cantidad de contactos calculados para envio de whatsapp
        /// </summary>
        public int? CantidadContactosWhatsapp { get; set; }
        /// <summary>
        /// Flag que indca si incluye o no envio de whatsapp
        /// </summary>
        public bool? NoIncluyeWhatsaap { get; set; }
        /// <summary>
        /// Flag que indica el estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
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
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Id de la tabla mkt.T_ConjuntoAnuncio
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// Flag para evitar doble ejecucion
        /// </summary>
        public bool EnEjecucion { get; set; }
        /// <summary>
        /// Este parametro contiene la url del formulario
        /// </summary>
        public string? UrlFormulario { get; set; }

        public virtual TCampaniaGeneral IdCampaniaGeneralNavigation { get; set; } = null!;
        public virtual TCentroCosto? IdCentroCostoNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual ICollection<TCampaniaGeneralDetalleArea> TCampaniaGeneralDetalleAreas { get; set; }
        public virtual ICollection<TCampaniaGeneralDetallePrograma> TCampaniaGeneralDetalleProgramas { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleResponsable> TCampaniaGeneralDetalleResponsables { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleSubArea> TCampaniaGeneralDetalleSubAreas { get; set; }
        public virtual ICollection<TConfiguracionDeEnvioParaWhatsApp> TConfiguracionDeEnvioParaWhatsApps { get; set; }
        public virtual ICollection<TFiltradoDeDatosPorPrioridadWhatsApp> TFiltradoDeDatosPorPrioridadWhatsApps { get; set; }
    }
}
