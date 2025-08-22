namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    //public class ReporteTasaConversionConsolidadaDTO
    //{
    //    public List<AsesorFiltroDTO> Asesores { get; set; }
    //    public List<CoordinadorFiltroDTO> Coordinadores { get; set; }

    //}
    public class ReporteTasaConversionConsolidadaGeneralDTO
    {
        public List<ReportePersonalDTO> Coordinadores { get; set; }
        public List<ReportePersonalDTO> Asesores { get; set; }
        public List<PeriodoFiltroDTO> Periodos { get; set; }
    }
    public class ReporteTasaConversionConsolidadasComboDTO
    {
        public List<ReportePersonalDTO> Coordinadores { get; set; }
        public List<ReportePersonalDTO> Asesores { get; set; }
        public List<DTO.ComboDTO> AreasCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> SubAreasCapacitacion { get; set; }
        public List<PGeneralComboDTO> ProgramasGenerales { get; set; }
        public List<PEspecificoPGeneralFiltroDTO> ProgramasEspecificos { get; set; }
    }
    public class ReporteTasaConversionConsolidadaGraficaFiltroDTO
    {
        public List<int> Coordinadores { get; set; }
        public List<int>? Asesores { get; set; }
        public string PeriodoInicio { get; set; }
        public string PeriodoFin { get; set; }
        public int? TipoPeriodo { get; set; }
    }
    public class ReporteTasaConversionConsolidadaGraficasVistaDTO
    {
        public List<TCRM_ConsolidadTCAsesoresGraficas> Consolidado { get; set; }
    }
    public class ReporteTasaConversionConsolidadaMensualGraficasVistaDTO
    {
        public List<TCRM_ConsolidadTCAsesoresMensualGraficas> Consolidado { get; set; }
    }
    public class TCRM_ConsolidadTCAsesoresGraficas
    {
        public int IdAsesor { get; set; }
        public decimal IR { get; set; }
        public decimal Ingreso_en_el_mes_USD { get; set; }
        public decimal PrecioSinDesc { get; set; }
        public decimal PrecioListaFinal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Descuento_Promedio { get; set; }
        public decimal PP_OC_USD { get; set; }
        public decimal IS { get; set; }
        public decimal OC { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public string Semana { get; set; }
        public string Mes { get; set; }
        public decimal TCMeta { get; set; }
        public decimal TC_Real { get; set; }
        public decimal TC_Meta { get; set; }
        public decimal TCReal_TCMeta { get; set; }
        public decimal PP_IM_USD { get; set; }
        public decimal PP_IM_PP_OC { get; set; }
        public decimal Porcentaje_ingreso_mes { get; set; }
        public decimal IM { get; set; }
        public decimal IR_IM { get; set; }
        public int NroSemana { get; set; }
        public int Ano { get; set; }
    }
    public class TCRM_ConsolidadTCAsesoresMensualGraficas
    {
        public int IdAsesor { get; set; }
        public decimal IR { get; set; }
        public decimal Ingreso_en_el_mes_USD { get; set; }
        public decimal PrecioSinDesc { get; set; }
        public decimal PrecioListaFinal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Descuento_Promedio { get; set; }
        public decimal PP_OC_USD { get; set; }
        public decimal IS { get; set; }
        public decimal OC { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public int? MesNumero { get; set; }
        public string Mes { get; set; }
        public decimal TCMeta { get; set; }
        public decimal TC_Real { get; set; }
        public decimal TC_Meta { get; set; }
        public decimal TCReal_TCMeta { get; set; }
        public decimal PP_IM_USD { get; set; }
        public decimal PP_IM_PP_OC { get; set; }
        public decimal Porcentaje_ingreso_mes { get; set; }
        public decimal IM { get; set; }
        public decimal IR_IM { get; set; }
        public int Ano { get; set; }
    }

    public class ReporteTasaConversionConsolidadaFiltroDTO
    {
        public List<int>? Areas { get; set; }
        public List<int>? SubAreas { get; set; }
        public List<int>? PGeneral { get; set; }
        public List<int>? PEspecifico { get; set; }
        public List<string>? Modalidades { get; set; }
        public List<string>? Ciudades { get; set; }
        public bool Fecha { get; set; }
        public List<int>? Coordinadores { get; set; }
        public List<int>? Asesores { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        //Para el reporte de asignacion detalle alumnos operaciones
        public DateTime? FechaInicioMatricula { get; set; }
        public DateTime? FechaFinMatricula { get; set; }
        public DateTime? FechaInicioAsignacion { get; set; }
        public DateTime? FechaFinAsignacion { get; set; }
        public int? CheckFecha { get; set; }
        public int? Personal { get; set; }
    }

    public class TCCReporteDTO
    {
        public List<ConsolidadoDTO> Consolidados { get; set; }
        public List<DesagregadoDTO> Desagregados { get; set; }
        public List<TCRM_CentroCostoPorAsesorAgrupadoDTO> CentroCostoAsesorAgrupados { get; set; }
        public List<TCRM_CentroCostoPorAsesorDTO> CentroCostoAsesor { get; set; }
    }

    public class ConsolidadoDTO
    {
        public string Grupo { get; set; }
        public IGrouping<string, TCRM_ConsolidadTCAsesores> Data { get; set; }
    }
    public class DesagregadoDTO
    {
        public string Grupo { get; set; }
        public IGrouping<string, TCRM_TasaConversionPorCategoriaDatoPaisDTO> Data { get; set; }
    }
    public class ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO
    {
        public string Coordinadora { get; set; }
        public string Dia { get; set; }
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> Detalle { get; set; }
    }
    public class ReporteIndicadoresOperativosPorDiaCoorinadorDTO
    {
        public string Dia { get; set; }
        public string Coordinadora { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }
    }
    public class ReporteIndicadoresOperativosFinalDTO
    {
        public IEnumerable<ReporteIndicadoresOperativosAgrupadoDTO> ReporteIndicadoresOperativos { get; set; }
        public List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO> ReporteIndicadoresOperativosAgrupadoPorDia { get; set; }
        public List<String> Coordinadoras { get; set; }
    }

    public class ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO
    {
        public string Dia { get; set; }
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> Detalle { get; set; }
    }
    public class ReporteIndicadoresOperativosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteIndicadoresOperativosDTO> Detalle { get; set; }
    }
    public class ReporteIndicadoresOperativosDTO
    {
        public string Coordinadora { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }
    }

}