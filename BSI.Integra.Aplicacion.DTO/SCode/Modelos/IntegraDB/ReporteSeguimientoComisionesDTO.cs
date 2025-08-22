namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteSeguimientoComisionesDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string FechaMatricula { get; set; }
        public string NombrePrograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public string Supervisor { get; set; }
        public string Asistente { get; set; }
        public int IdAsesor { get; set; }
        public int? IdCoordinador { get; set; }
        public int IdSupervisor { get; set; }
        public int IdAsistente { get; set; }
        public decimal? ComisionAsesor { get; set; }
        public decimal? ComisionCoordinador { get; set; }
        public decimal? ComisionSupervisor { get; set; }
        public decimal? ComisionAsistente { get; set; }
        public string Pais { get; set; }
        public decimal PrecioPrograma { get; set; }
        public string MonedaPrecioPrograma { get; set; }
        public decimal TotalPagar { get; set; }
        public string MonedaTotalPagar { get; set; }
        public decimal TotalPagarDolares { get; set; }
        public decimal? PrecioMatriculaPrograma { get; set; }
        public string MonedaPrecioMatriculaPrograma { get; set; }
        public decimal MontoPagadoHastaAhora { get; set; }
        public string MonedaMontoPagadoHastaAhora { get; set; }
        public string SiglaDocumento { get; set; }
        public string FechaEntregaDocumento { get; set; }
        public string ObservacionDocumento { get; set; }
        public bool TieneDocumento { get; set; }
        public bool TieneMatriculaPagada { get; set; }
        public bool TieneAsistencia { get; set; }
        public string NombreTieneDocumento { get; set; }
        public string NombreTieneMatriculaPagada { get; set; }
        public string NombreTieneAsistencia { get; set; }
        public int IdEstadoComision { get; set; }
        public string NombreEstadoComision { get; set; }
        public string Observacion { get; set; }
        public decimal? TipoCambio { get; set; }
        public int? IdSubEstadoComision { get; set; }
        public int IdMonedaTotalPagar { get; set; }
        public int diferencia { get; set; }
        public string VersionMatricula { get; set; }
    }

    public class FiltroReporteSeguimientoComisionesDTO
    {
        public string ListaAsesores { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int IdEstadoComision { get; set; }
        public int IdSubEstadoComision { get; set; }

    }
}
