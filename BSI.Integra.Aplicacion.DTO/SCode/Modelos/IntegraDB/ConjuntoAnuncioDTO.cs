using BSI.Integra.Aplicacion.DTO.SCode;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConjuntoAnuncioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int? IdCategoriaOrigen { get; set; }

        public string? Origen { get; set; }

        public string? IdConjuntoAnuncioFacebook { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public Guid? IdMigracion { get; set; }

        public int? IdConjuntoAnuncioFuente { get; set; }

        public int? IdCentroCosto { get; set; }

        public int? IdConjuntoAnuncioTipoObjetivo { get; set; }

        public int? IdFormularioPlantilla { get; set; }

        public string? Adicional { get; set; }

        public string? EnlaceFormulario { get; set; }

        public bool? EsGrupal { get; set; }

        public bool? EsPaginaWeb { get; set; }

        public bool? EsPreLanzamiento { get; set; }

        public int? IdConjuntoAnuncioSegmento { get; set; }

        public int? IdConjuntoAnuncioTipoGenero { get; set; }

        public int? IdPais { get; set; }
        public string? Propietario { get; set; }
        public string? NumeroAnuncio { get; set; }
        public string? NumeroSemana { get; set; }
        public string? DiaEnvio { get; set; }

        public string? NombreInicial { get; set; }

    }

    public class CompuestoConjuntoAnuncioDTO
    {
        public ConjuntoAnuncioPanelDTO ConjuntoAnuncio { get; set; }
        public string Usuario { get; set; }

    }
    public class ConjuntoAnuncioFiltroCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Codigo { get; set; }
    }
    public class ConjuntoAnuncioPanelDTO
    {
        public int Id { get; set; }
        public string? IdConjuntoAnuncio_Facebook { get; set; }
        public int? IdProveedor { get; set; }
        public string nombreCategoria { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }
        public string Nombre { get; set; }
        public string Total { get; set; }

    }

    public class ConjuntoAnuncioEnvioDTO
    {
        public int Id { get; set; }
        public string? IdConjuntoAnuncio_Facebook { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; } = null!;

    }


    public class FiltroPaginadorDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public GridFiltersDTO? FiltroKendo { get; set; }
    }



    public class CoonjuntoAnuncioUrl
    {
        public int IdProgramaGeneral { get; set; }
        public int DireccionUrl { get; set; }


    }

    public class ConjuntoAnuncioFacebookDTO
    {
        public string IdAnuncioFacebook { get; set; }
        public int? AttributionWindowDays { get; set; }
        public int? BidAmount { get; set; }
        public string BillinEevent { get; set; }
        public double? BudgetRemaining { get; set; }
        public string CampaignId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? DailyBudget { get; set; }
        public int? DailyImps { get; set; }
        public string EffectiveStatus { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsAutobid { get; set; }
        public bool? IsAveragePricePacing { get; set; }
        public int? LifetimeBudget { get; set; }
        public int? LifetimeImps { get; set; }
        public string Name { get; set; }
        public string OptimizationGoal { get; set; }
        public DateTime? StartTime { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? TieneInsights { get; set; }
        public bool? EsValidado { get; set; }
        public bool? EsIntegra { get; set; }
        public bool? EsPublicado { get; set; }
        public bool? ActivoActualizado { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public bool? EsRelacionado { get; set; }
        public bool? EsOtros { get; set; }
        public int? CuentaPublicitaria { get; set; }
        public string NombreCampania { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCampaniaFacebook { get; set; }
        public string ConfiguredStatus { get; set; }
        public Guid? IdMigracion { get; set; }
         public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }


    }





}
