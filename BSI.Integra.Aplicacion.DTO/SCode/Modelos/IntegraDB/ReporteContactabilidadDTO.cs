namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteContactabilidadDTO
    {
        public int Hora { get; set; }
        public string Clave { get; set; }
        public int Valor { get; set; }
        public int Tipo { get; set; }
        public int? TotalLlamadas { get; set; }
        public string Troncal { get; set; }
        public string Sede { get; set; }
        public int? CantidadIntentos { get; set; }
        public int? CantidadIntentoEjecutadoUno { get; set; }
        public int? CantidadIntentoEjecutadoDos { get; set; }
        public int? CantidadIntentoEjecutadoTres { get; set; }
        public int? CantidadIntentoEjecutadoMasTres { get; set; }
        public int? CantidadIntentoNoEjecutadoUno { get; set; }
        public int? CantidadIntentoNoEjecutadoDos { get; set; }
        public int? CantidadIntentoNoEjecutadoTres { get; set; }
        public int? CantidadIntentoNoEjecutadoMasTres { get; set; }
        public int? CantidadWhatsappEnviados { get; set; }
        public int? CantidadWhatsappRecibidos { get; set; }
        public int? DuracionIntentoEjecutadoUno { get; set; }
        public int? DuracionIntentoEjecutadoDos { get; set; }
        public int? DuracionIntentoEjecutadoTres { get; set; }
        public int? DuracionIntentoEjecutadoMasTres { get; set; }
        public int? DuracionIntentoNoEjecutadoUno { get; set; }
        public int? DuracionIntentoNoEjecutadoDos { get; set; }
        public int? DuracionIntentoNoEjecutadoTres { get; set; }
        public int? DuracionIntentoNoEjecutadoMasTres { get; set; }
    }
    public class ReporteContactabilidadCombosDTO
    {
        public List<ReportePersonalDTO> Asesores { get; set; }

        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
    }
    public class ReporteContactabilidadFiltroDTO
    {
        public List<int> Asesores { get; set; }
        public List<int> AsesoresComparativos { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Tipo { get; set; }
    }
    public class ReporteContactabilidadFiltroAlternoDTO
    {
        public List<int> Asesores { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Tipo { get; set; }
    }
    public class ReporteContactabilidadFiltroFinalDTO
    {
        public string Asesores { get; set; }
        public string AsesoresComparativo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Tipo { get; set; }
    }
    public class FiltroReporteLlamadaEntranteDTO
    {
        public string? Asesores { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public class TasaContactabilidadDTO
    {
        public double TC { get; set; }
        public int Hora { get; set; }
        public string Clave { get; set; }
        public int AT { get; set; }
        public int TE { get; set; }
    }
    public class ReporteContactabilidadAsesorAgrupadoDTO
    {
        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public List<TasaContactabilidadDTO> Lista { get; set; }
    }
    public class ReporteContactabilidadDataDTO
    {
        public List<ReporteContactabilidadAsesorAgrupadoDTO> ComparativoAsesor { get; set; }
        public List<ReporteContactabilidadDTO> AsesorContactabilidad { get; set; }
    }
    public class ReporteContactabilidadDataV2DTO
    {
        public List<ReporteContactabilidadAgrupadoDTO> ComparativoAsesor { get; set; }
        public List<ReporteContactabilidadDTO> AsesorContactabilidad { get; set; }
        public List<ReporteContactabilidadMinutosDTO> MinutosContactabilidad { get; set; }
    }
    public class ReporteContactabilidadAlternoDTO
    {
        public List<ReporteContactabilidadDTO> AsesorContactabilidad { get; set; }
        public List<ReporteContactabilidadMinutosDTO> MinutosContactabilidad { get; set; }
    }
    public class ReporteContactabilidad3cxAlternoDTO
    {
        public int Hora { get; set; }
        public string Clave { get; set; }
        public int ValorGeneral { get; set; }
        public int ValorIntegra { get; set; }
        public int Valor3cx { get; set; }
        public int Tipo { get; set; }
        public int? TotalGeneral { get; set; }
        public int? TotalIntegra { get; set; }
        public int? Total3cx { get; set; }
        public int? TotalIntegra3cx { get; set; }
        public int? TotalSinLlamada { get; set; }
        public int? CantidadIntentos { get; set; }
        public int? CantidadIntentosIntegra { get; set; }
        public int? CantidadIntentos3cx { get; set; }
        public int? CantidadIntentoEjecutadoUno { get; set; }
        public int? CantidadIntentoEjecutadoUnoIntegra { get; set; }
        public int? CantidadIntentoEjecutadoUno3cx { get; set; }
        public int? CantidadIntentoEjecutadoDos { get; set; }
        public int? CantidadIntentoEjecutadoDosIntegra { get; set; }
        public int? CantidadIntentoEjecutadoDos3cx { get; set; }
        public int? CantidadIntentoEjecutadoTres { get; set; }
        public int? CantidadIntentoEjecutadoTresIntegra { get; set; }
        public int? CantidadIntentoEjecutadoTres3cx { get; set; }
        public int? CantidadIntentoEjecutadoMasTres { get; set; }
        public int? CantidadIntentoEjecutadoMasTresIntegra { get; set; }
        public int? CantidadIntentoEjecutadoMasTres3cx { get; set; }
        public int? CantidadIntentoNoEjecutadoUno { get; set; }
        public int? CantidadIntentoNoEjecutadoUnoIntegra { get; set; }
        public int? CantidadIntentoNoEjecutadoUno3cx { get; set; }
        public int? CantidadIntentoNoEjecutadoDos { get; set; }
        public int? CantidadIntentoNoEjecutadoDosIntegra { get; set; }
        public int? CantidadIntentoNoEjecutadoDos3cx { get; set; }
        public int? CantidadIntentoNoEjecutadoTres { get; set; }
        public int? CantidadIntentoNoEjecutadoTresIntegra { get; set; }
        public int? CantidadIntentoNoEjecutadoTres3cx { get; set; }
        public int? CantidadIntentoNoEjecutadoMasTres { get; set; }
        public int? CantidadIntentoNoEjecutadoMasTresIntegra { get; set; }
        public int? CantidadIntentoNoEjecutadoMasTres3cx { get; set; }
        public int? DuracionIntentoEjecutadoUno { get; set; }
        public int? DuracionIntentoEjecutadoUnoIntegra { get; set; }
        public int? DuracionIntentoEjecutadoUno3cx { get; set; }
        public int? DuracionIntentoEjecutadoDos { get; set; }
        public int? DuracionIntentoEjecutadoDosIntegra { get; set; }
        public int? DuracionIntentoEjecutadoDos3cx { get; set; }
        public int? DuracionIntentoEjecutadoTres { get; set; }
        public int? DuracionIntentoEjecutadoTresIntegra { get; set; }
        public int? DuracionIntentoEjecutadoTres3cx { get; set; }
        public int? DuracionIntentoEjecutadoMasTres { get; set; }
        public int? DuracionIntentoEjecutadoMasTresIntegra { get; set; }
        public int? DuracionIntentoEjecutadoMasTres3cx { get; set; }
        public int? DuracionIntentoNoEjecutadoUno { get; set; }
        public int? DuracionIntentoNoEjecutadoUnoIntegra { get; set; }
        public int? DuracionIntentoNoEjecutadoUno3cx { get; set; }
        public int? DuracionIntentoNoEjecutadoDos { get; set; }
        public int? DuracionIntentoNoEjecutadoDosIntegra { get; set; }
        public int? DuracionIntentoNoEjecutadoDos3cx { get; set; }
        public int? DuracionIntentoNoEjecutadoTres { get; set; }
        public int? DuracionIntentoNoEjecutadoTresIntegra { get; set; }
        public int? DuracionIntentoNoEjecutadoTres3cx { get; set; }
        public int? DuracionIntentoNoEjecutadoMasTres { get; set; }
        public int? DuracionIntentoNoEjecutadoMasTresIntegra { get; set; }
        public int? DuracionIntentoNoEjecutadoMasTres3cx { get; set; }
    }
    public class ReporteContactabilidadAgrupadoDTO
    {
        public int IdAsesor { get; set; }
        public List<ReporteContactabilidadAsesorIndicadoresDTO> Lista { get; set; }
    }
    public class ReporteContactabilidadAsesorIndicadoresDTO
    {
        public int IdAsesor { get; set; }
        public int Hora { get; set; }
        public string Clave { get; set; }
        public int Valor { get; set; }
        public int Tipo { get; set; }
        public int? TotalLlamadas { get; set; }
    }
    public class ReporteContactabilidadMinutosDTO
    {
        public string Pais { get; set; }
        public string Troncal { get; set; }
        public int Segundos { get; set; }
        public int Minutos { get; set; }
        public decimal Costominuto { get; set; }
    }
    public class ReporteLlamadaEntranteDTO
    {
        public string HoraReporte { get; set; }
        public int IdPais { get; set; }
        public string Origen { get; set; }
        public int Total { get; set; }
        public int LlamadaContestada { get; set; }
        public int NoContestoDisponible { get; set; }
        public int NoContestoOcupado { get; set; }
        public int LlamadaDevuelta { get; set; }
        public int LlamadaFallida { get; set; }
    }
}
