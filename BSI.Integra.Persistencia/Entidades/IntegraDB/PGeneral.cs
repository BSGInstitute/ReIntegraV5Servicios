using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PGeneral : BaseIntegraEntity
    {
        public int? IdPgeneral { get; set; }
        public string? Nombre { get; set; }
        public string? PwImgPortada { get; set; }
        public string? PwImgPortadaAlf { get; set; }
        public string? PwImgSecundaria { get; set; }
        public string? PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        public string? PwEstado { get; set; }
        public string? PwMostrarBsplay { get; set; }
        public string? PwDuracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string? PgTitulo { get; set; }
        public string? Codigo { get; set; }
        public string? UrlImagenPortadaFr { get; set; }
        public string? UrlBrochurePrograma { get; set; }
        public string? UrlPartner { get; set; }
        public string? UrlVersion { get; set; }
        public string? PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string? NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
        public bool? TutorVirtualActivo { get; set; } = false;
        public Guid? IdMigracion { get; set; }
        public string? PwDescripcionGeneral { get; set; }
        public bool? TieneProyectoDeAplicacion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public string? CodigoPartner { get; set; }
        public string? LogoPrograma { get; set; }
        public string? UrlLogoPrograma { get; set; }
        public DateTime? FechaInicioAsincronico { get; set; }
        public bool? AsignaVenta { get; set; }
        public bool? TieneCertificadoModular { get; set; }
        public bool? CertificadoRequierePago { get; set; }
        public int? IdPgeneralBase { get; set; }
        public int? IdPgeneralPeriodoAsincronico { get; set; }

        //Instituto
        public double? CreditosTeoricos { get; set; }
        public double? CreditosPracticos { get; set; }
        public double? CreditosTotales { get; set; }
        public int? HorasTeoricas { get; set; }
        public int? HorasPracticas { get; set; }
        public int? HorasTotales { get; set; }
        public int? IdTipoProgramaCarrera { get; set; }

        //Hijos 
        public List<PgeneralParametroSeoPw> PGeneralParametroSeoPw { get; set; }
        public List<PgeneralDescripcion> PgeneralDescripcion { get; set; }
        public List<AdicionalProgramaGeneral> AdicionalProgramaGeneral { get; set; }
        public List<ProgramaAreaRelacionadum> ProgramaAreaRelacionada { get; set; }
        public List<PGeneralExpositor> PGeneralExpositor { get; set; }
        public List<CarreraPreRequisitoPgeneral> CarreraPreRequisitoPgeneral { get; set; }
        public List<SuscripcionProgramaGeneral> SuscripcionProgramaGeneral { get; set; }
        public List<PgeneralModalidad> PgeneralModalidad { get; set; }
        public List<PgeneralVersionPrograma> PgeneralVersionPrograma { get; set; }

        public List<ProgramaGeneralPerfilScoringCiudad> ProgramaGeneralPerfilScoringCiudad;
        public List<ProgramaGeneralPerfilScoringModalidad> ProgramaGeneralPerfilScoringModalidad;
        public List<ProgramaGeneralPerfilScoringAformacion> ProgramaGeneralPerfilScoringAformacion;
        public List<ProgramaGeneralPerfilScoringIndustria> ProgramaGeneralPerfilScoringIndustria;
        public List<ProgramaGeneralPerfilScoringCargo> ProgramaGeneralPerfilScoringCargo;
        public List<ProgramaGeneralPerfilScoringAtrabajo> ProgramaGeneralPerfilScoringAtrabajo;
        public List<ProgramaGeneralPerfilScoringCategoria> ProgramaGeneralPerfilScoringCategoria;
        public List<ProgramaGeneralPerfilCiudadCoeficiente> ProgramaGeneralPerfilCiudadCoeficiente;
        public List<ProgramaGeneralPerfilModalidadCoeficiente> ProgramaGeneralPerfilModalidadCoeficiente;
        public List<ProgramaGeneralPerfilCategoriaCoeficiente> ProgramaGeneralPerfilCategoriaCoeficiente;
        public List<ProgramaGeneralPerfilCargoCoeficiente> ProgramaGeneralPerfilCargoCoeficiente;
        public List<ProgramaGeneralPerfilIndustriaCoeficiente> ProgramaGeneralPerfilIndustriaCoeficiente;
        public List<ProgramaGeneralPerfilAformacionCoeficiente> ProgramaGeneralPerfilAformacionCoeficiente;
        public List<ProgramaGeneralPerfilAtrabajoCoeficiente> ProgramaGeneralPerfilAtrabajoCoeficiente;
        public List<ProgramaGeneralPerfilTipoDato> ProgramaGeneralPerfilTipoDato;
        public List<ProgramaGeneralPerfilEscalaProbabilidad> ProgramaGeneralPerfilEscalaProbabilidad;
        public List<ConfiguracionBeneficioProgramaGeneral> ConfiguracionBeneficioProgramaGenerals { get; set; }
        public ProgramaGeneralPerfilIntercepto ProgramaGeneralPerfilIntercepto;

        public List<ModeloPredictivoIndustria> ModeloPredictivoIndustria;
        public List<ModeloPredictivoCargo> ModeloPredictivoCargo;
        public List<ModeloPredictivoFormacion> ModeloPredictivoFormacion;
        public List<ModeloPredictivoTrabajo> ModeloPredictivoTrabajo;
        public List<ModeloPredictivoCategoriaDato> ModeloPredictivoCategoriaDato;
        public List<ModeloPredictivoTipoDato> ModeloPredictivoTipoDato;
        public List<ModeloPredictivoEscalaProbabilidad> ModeloPredictivoEscalaProbabilidad;

        public ModeloPredictivo ModeloPredictivo;
    }
}
