using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConjuntoAnuncioFacebook
    {
        public TConjuntoAnuncioFacebook()
        {
            TAnuncioFacebooks = new HashSet<TAnuncioFacebook>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del anuncio en facebook
        /// </summary>
        public string? IdAnuncioFacebook { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public int? AttributionWindowDays { get; set; }
        /// <summary>
        /// Cantidad de anuncios
        /// </summary>
        public int? BidAmount { get; set; }
        /// <summary>
        /// Facturacion por el evento
        /// </summary>
        public string? BillinEevent { get; set; }
        /// <summary>
        /// Presupuesto restante
        /// </summary>
        public double? BudgetRemaining { get; set; }
        /// <summary>
        /// Identificador de la campaña en facebook
        /// </summary>
        public string? CampaignId { get; set; }
        /// <summary>
        /// Fecha de creacion del anuncio
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// Presupuesto diario
        /// </summary>
        public int? DailyBudget { get; set; }
        /// <summary>
        /// Numero de impresiones (vistas)
        /// </summary>
        public int? DailyImps { get; set; }
        /// <summary>
        /// Estado efectivo del anuncio
        /// </summary>
        public string? EffectiveStatus { get; set; }
        /// <summary>
        /// Fecha de finalizacion del anuncio
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Indica si es auto anunciado
        /// </summary>
        public bool? IsAutobid { get; set; }
        /// <summary>
        /// Indica si el precio del anuncio esta entre el promedio 
        /// </summary>
        public bool? IsAveragePricePacing { get; set; }
        /// <summary>
        /// duracion del presupuesto
        /// </summary>
        public int? LifetimeBudget { get; set; }
        /// <summary>
        /// Duracion de las vistas del anuncio
        /// </summary>
        public int? LifetimeImps { get; set; }
        /// <summary>
        /// Nombre del anuncio
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public string? OptimizationGoal { get; set; }
        /// <summary>
        /// Fecha de inicio del anuncio
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Estado del anuncio
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Fecha de actualizacion del anuncio
        /// </summary>
        public DateTime? UpdatedTime { get; set; }
        /// <summary>
        /// Indica si tiene ideas
        /// </summary>
        public bool? TieneInsights { get; set; }
        /// <summary>
        /// Indica si es valido o no (1,0)
        /// </summary>
        public bool? EsValidado { get; set; }
        /// <summary>
        /// Indica si es un anuncio de integra o no (1,0)
        /// </summary>
        public bool? EsIntegra { get; set; }
        /// <summary>
        /// Indica si es un anuncio publicitario (1,0)
        /// </summary>
        public bool? EsPublicado { get; set; }
        /// <summary>
        /// Indica el estado del anuncio (1:activo, 0:actualiado)
        /// </summary>
        public bool? ActivoActualizado { get; set; }
        /// <summary>
        /// Es foreinf key TFM_ConjuntoAnuncios
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public bool? EsRelacionado { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public bool? EsOtros { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public int? CuentaPublicitaria { get; set; }
        /// <summary>
        /// Nombre de la campania
        /// </summary>
        public string? NombreCampania { get; set; }
        /// <summary>
        /// Nombre del centro de costo
        /// </summary>
        public string? CentroCosto { get; set; }
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
        /// Llave foranea de la tabla mkt.T_CampaniaFacebook
        /// </summary>
        public int? IdCampaniaFacebook { get; set; }
        /// <summary>
        /// Estado efectivo del anuncio
        /// </summary>
        public string? ConfiguredStatus { get; set; }

        public virtual TCampaniaFacebook? IdCampaniaFacebookNavigation { get; set; }
        public virtual ICollection<TAnuncioFacebook> TAnuncioFacebooks { get; set; }
    }
}
