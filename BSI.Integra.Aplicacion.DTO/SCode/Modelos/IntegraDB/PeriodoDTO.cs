namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PeriodoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicialFinanzas { get; set; }
        public DateTime FechaFinFinanzas { get; set; }
        public DateTime? FechaInicialRepIngresos { get; set; }
        public DateTime? FechaFinRepIngresos { get; set; }
        public string Usuario { get; set; } = null;
    }
    public class PeriodoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
    }

    public class PeriodoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
    }


}
