namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReportePagosPorPeriodoFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMatricula { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdModalidad { get; set; }
    }
}
