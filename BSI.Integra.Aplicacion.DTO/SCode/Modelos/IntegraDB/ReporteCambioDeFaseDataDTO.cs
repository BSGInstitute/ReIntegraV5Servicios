namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteCambioDeFaseDataDTO
    {
        public IEnumerable<EjecutadasSinCambiodeFaseAlternoDTO> EjecutadasSinCambiodeFase { get; set; }
        public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ActividadVencidaporTab { get; set; }
        public ReporteTasaDeCambioDTO ReporteTasaDeCambio { get; set; }
        public ReporteTasaDeCambioPredictivoDTO ReporteTasaDeCambioPredictivo { get; set; }
    }
    public class ReporteTasaDeCambioDTO
    {
        public List<TCRM_CambioDeFaseDTO> ReporteTasaDeCambioSemanal { get; set; }
        public List<TCRM_CambioDeFaseDTO> ReporteTasaDeCambioMensual { get; set; }
    }
    public class ReporteTasaDeCambioPredictivoDTO
    {
        public List<TCRM_CambioDeFasePredictivoDTO> ReporteTasaDeCambioSemanal { get; set; }
        public List<TCRM_CambioDeFasePredictivoDTO> ReporteTasaDeCambioMensual { get; set; }
    }
    public class ReporteCambioDeFaseDataV2DTO
    {
        //public ReporteTasaContactoDTO ReporteTasaContactoRn2 { get; set; }
        //public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamadaRn2 { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidad { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadPredictivo { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadConLlamada { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadSinLlamada { get; set; }
        //public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlRN1yRN2 { get; set; }
        public List<ControlCambioDeFaseV2DTO> ControlCambiodeFase { get; set; }
        public List<ControlOtroMedioDTO> ReporteOtroMedio { get; set; }
        //public List<EjecutadasSinCambiodeFaseDTO> EjecutadasSinCambiodeFase { get; set; }
        //public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ActividadVencidaporTab { get; set; }
        public int ReporteMetasObtenerTotalIS { get; set; }
    }
    public class ReporteCalidadCambioDeFaseDTO
    {
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento { get; set; }
        public List<DiferenciaLlamadasBloqueDTO> DiferenciaLlamadasBloque { get; set; }
        public List<ConteoDatosFaseDTO> ConteoDatosFase { get; set; }
        public DateTime? FechaConteoInicio { get; set; }
        public DateTime? FechaConteoMomento { get; set; }
    }
    public class ReporteCalidadCambioDeFaseAlternoDTO
    {
        public List<ReporteCalidadProcesamientoAlternoDTO> ReporteCalidadProcesamiento { get; set; }
        public List<DiferenciaLlamadasBloqueDTO> DiferenciaLlamadasBloque { get; set; }
        //public List<ConteoDatosFaseDTO> ConteoDatosFase { get; set; }
        //public DateTime? FechaConteoInicio { get; set; }
        //public DateTime? FechaConteoMomento { get; set; }
    }
    public class ReporteConteoDatosFaseDTO
    {
        public List<ConteoDatosFaseDTO> ConteoDatosFase { get; set; }
        public DateTime? FechaConteoInicio { get; set; }
        public DateTime? FechaConteoMomento { get; set; }
    }
    public class ReporteCambioDeFaseTasaContactoDTO
    {
        public ReporteTasaContactoDTO ReporteTasaContacto { get; set; }
        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamada { get; set; }
    }
}
