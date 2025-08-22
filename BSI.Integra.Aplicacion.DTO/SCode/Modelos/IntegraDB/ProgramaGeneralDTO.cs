using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralComboDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string urlVersion { get; set; }
    }

    public class PGeneralComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public int? IdTipoPrograma { get; set; }
    }
    public class PGeneralComboModuloDTO
    {
        public IEnumerable<AreaCapacitacionFiltroDTO> AreaCapacitacion { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> SubAreaCapacitacion { get; set; }
        public IEnumerable<ParametroSeoPwDTO> ParametroSeo { get; set; }
        public IEnumerable<ComboDTO> PartnerPw { get; set; }
        public IEnumerable<ComboDTO> Expositor { get; set; }
        public IEnumerable<ComboDTO> CategoriaPrograma { get; set; }
        public IEnumerable<ComboDTO> VisualizacionBsPlay { get; set; }
        public IEnumerable<ComboDTO> Titulo { get; set; }
        public IEnumerable<ComboDTO> Cargo { get; set; }
        public IEnumerable<ComboDTO> AreaFormacion { get; set; }
        public IEnumerable<ComboDTO> AreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> Industria { get; set; }
        public IEnumerable<ComboDTO> Ciudad { get; set; }
        public IEnumerable<CompuestoCategoriaOrigenConHijosDTO> CategoriaOrigenConHijos { get; set; }
        public IEnumerable<ComboDTO> TipoDato { get; set; }
        public IEnumerable<ComboDTO> PGeneral { get; set; }
        public IEnumerable<ComboDTO> PerfilContactoProgramaColumna { get; set; }
        public IEnumerable<ComboDTO> ModalidadCurso { get; set; }
        public IEnumerable<ComboDTO> PaginaWebPw { get; set; }
        public IEnumerable<VersionProgramaDTO> VersionPrograma { get; set; }
        public IEnumerable<ComboDTO> ModuloPrograma { get; set; }
        public IEnumerable<ComboDTO> CicloPrograma { get; set; }
        public IEnumerable<ComboDTO> TipoPrograma { get; set; }
        public IEnumerable<ComboDTO> Proveedor { get; set; }
        public IEnumerable<PGeneralPeriodoAsincronicoDTO> PGeneralPeriodoAsincronico { get; set; }
    }

    public class PGeneralComboMontoPagoModuloDTO
    {
        public IEnumerable<AreaCapacitacionFiltroDTO> AreaCapacitacion { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> SubAreaCapacitacion { get; set; }
        public IEnumerable<TipoDescuentoComboDTO> TipoDescuento { get; set; }
        public IEnumerable<PaisMonedaComboDTO> Pais { get; set; }
        public IEnumerable<MonedaComboDTO> Moneda { get; set; }
        public IEnumerable<ComboDTO> CategoriaPrograma { get; set; }
        public IEnumerable<ComboDTO> SuscripcionProgramaGeneral { get; set; }
        public IEnumerable<TipoPagoComboDTO> TipoPago { get; set; }
        public IEnumerable<ComboDTO> PlataformaPago { get; set; }
    }
    public class PGeneralComboConfiguracionPlantillaModuloDTO
    {
        public IEnumerable<PlantilaCertificadoConstanciaDTO> PlantilaCertificadoConstancia { get; set; }
        public IEnumerable<ComboDTO> ModalidadCurso { get; set; }
        public IEnumerable<ComboDTO> EstadoMatricula { get; set; }
        public IEnumerable<ComboDTO> OperadorComparacion { get; set; }
        public IEnumerable<SubEstadoMatriculaFiltroDTO> SubEstadoMatricula { get; set; }
        public IEnumerable<ComboDTO> Pais { get; set; }
        public IEnumerable<ComboDTO> BeneficioDatoAdicional { get; set; }
        public IEnumerable<ComboDTO> PGeneralVersionPrograma { get; set; }
    }
    public class PgeneralFechaOnlineDTO
    {
        public int IdProgramaGeneral { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
    }
    public class PgeneralFechaAonlineDTO
    {
        public int IdPGeneral { get; set; }
        public int ActulizarFechaInicioAonline { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public DateTime? FechaInicioAsincronico { get; set; }
    }
    public class PgeneralFechaPresencialDTO
    {
        public int IdPGeneral { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
    }
    public class PGeneralComboModuloConfigurarVideoProgramaDTO
    {
        public List<AreaCapacitacionFiltroDTO> AreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> SubAreaCapacitacion { get; set; }
        public List<ComboDTO> PartnerPws { get; set; }
        public List<ComboDTO> PGenerals { get; set; }
        public List<ComboDTO> TipoVista { get; set; }
        public List<ComboDTO> TipoEvaluacionTrabajo { get; set; }
        public List<ComboDTO> TipoMarcador { get; set; }
    }
    public class PerfilContactoProgramaDTO
    {
        public CoeficienteScoringCiudadDTO CoeficienteScoringCiudad { get; set; }
        public CoeficienteScoringModalidadDTO CoeficienteScoringModalidad { get; set; }
        public CoeficienteScoringAFormacionDTO CoeficienteScoringAFormacion { get; set; }
        public CoeficienteScoringIndustriaDTO CoeficienteScoringIndustria { get; set; }
        public CoeficienteScoringCargoDTO CoeficienteScoringCargo { get; set; }
        public CoeficienteScoringATrabajoDTO CoeficienteScoringATrabajo { get; set; }
        public CoeficienteScoringCategoriaDTO CoeficienteScoringCategoria { get; set; }
        public List<EscalaProbabilidadDTO> EscalaProbabilidads { get; set; }
        public ProgramaGeneralPerfilInterceptoDTO ProgramaGeneralPerfilIntercepto { get; set; }
        public List<TipoDatoPerFilContactoProgramaDTO> TipoDatoPerFilContactoProgramas { get; set; }
    }
    public class BeneficioPreRequisitoProgramaDTO
    {
        public List<CompuestoPreRequisitoModalidadDTO> PreRequisitos { get; set; }
        public List<CompuestoBeneficioModalidadDTO> Beneficios { get; set; }
        public int IdPGeneral { get; set; }
    }
}
