namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    public class AgendaActividadDTO
    {
    }
    public class ValorEtiquetaDTO
    {
        public ObjetoValorEtiquetaDTO Objeto { get; set; }
        public OportunidadCompuestoDTO DatosOportunidad { get; set; }
        public string FechaInicioPrograma { get; set; }
    }
    public class ObjetoValorEtiquetaDTO
    {
        public string CronogramaPagos { get; set; }
        public OportunidadAlumnoDTO DatosOportunidadAlumno { get; set; }
        public string EtiquetaMontosPagoPaquetes { get; set; }
        public List<ProblemaCausaDTO> ListaProblemasCausa { get; set; }
        public List<ClaveValorDTO> ListaTemplateV2ReemplazoEtiqueta { get; set; }
        public List<PGeneralCursoRelacionadoDTO> UrlCursosRelacionados { get; set; }
    }
    public class ObtenerConfiguracionesDTO
    {
        public IEnumerable<DTO.ComboDTO> AreasCapacitacion { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> SubAreasCapacitacion { get; set; }
        public IEnumerable<PGeneralComboDTO> ProgramasGenerales { get; set; }
    }
    public class ActualizarSentinelAlumnoDTO
    {
        public bool Respuesta { get; set; }
        public int IdSentinel { get; set; }
        public bool Estado { get; set; }
    }
    public class ReporteIncidenciaDTO
    {
        public bool ValidarRN2 { get; set; }
        public WhatsAppPlantillaPorOcurrenciaActividadDTO plantillaAutomaticaWhatsapp { get; set; }
        public List<ArbolOcurenciaAlternoDTO> ArbolOcurrencia { get; set; }
    }
    public class FiltroReporteIncidenciaDTO
    {
        public int IdContacto { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdOcurrenciaReporte { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdActividadOcurrencia { get; set; }
        public int DiasSinContactoOportunidad { get; set; }
        public int IdActividadCabecera { get; set; }
        public bool TieneOcurrencias { get; set; }
    }
    public class SolciitudBeneficioDTO
    {
        public bool Respuesta { get; set; }
        public string Mensaje { get; set; }
    }
}
