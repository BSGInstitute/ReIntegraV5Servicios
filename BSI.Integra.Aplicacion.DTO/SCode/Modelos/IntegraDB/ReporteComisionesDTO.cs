namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteComisionesDTO
    {
        public int IdPersonal { get; set; }
        public string Nombre { get; set; }
        public decimal MontoComisionSoles { get; set; }
        public decimal MontoComisionDolares { get; set; }
    }
    public class FiltroReporteComisionDTO
    {
        public string? IdsAsesores { get; set; } =null;
        public DateTime FechaInicio { get; set; } 
        public DateTime FechaFin { get; set; }
        public  int IdSubEstado { get; set; }
    }

    public class FiltroGenerarCierreDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

}
