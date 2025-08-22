namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CentroCostoPorAsesorDTO
    {
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string PGeneral { get; set; }
        public string PEspecifico { get; set; }
        public string Modalidades { get; set; }
        public string Ciudades { get; set; }
        public bool Fecha { get; set; }
        public string Coordinadores { get; set; }
        public string Asesores { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReporteDevolucionesFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaInicioCronograma { get; set; } = null;
        public DateTime? FechaFinCronograma { get; set; } = null;
        public int? IdCentroCosto { get; set; } = null;
        public int? IdAlumno { get; set; } = null;
        public int? IdMatricula { get; set; } = null;
        public string? EstadoPago { get; set; } = null;
    }


    public class ReporteDevolucionesCompuestoDTO
    {
        public List<ReporteDevolucionDTO> ReporteDevoluciones { get; set; }
        public bool Cronograma { get; set; }
        public List<ReporteDevolucion> ReporteDevolucionAgrupado { get; set; }

    }

    public class ReporteDevolucion
    {
        public string g { get; set; }
        public List<ReporteDevolucionDTO> l { get; set; }
    }
}
