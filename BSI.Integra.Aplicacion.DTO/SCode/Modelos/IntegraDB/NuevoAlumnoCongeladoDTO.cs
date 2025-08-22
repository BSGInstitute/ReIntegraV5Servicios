namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class NuevoAlumnoCongeladoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public decimal Mora { get; set; }
        public decimal MontoPagado { get; set; }
        public bool Cancelado { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public int IdPeriodo { get; set; }
        public string Periodo { get; set; }
        public string Usuario { get; set; }
    }
    public class NuevoAlumnoCongeladoExcelDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public decimal Mora { get; set; }
        public decimal MontoPagado { get; set; }
        public bool Cancelado { get; set; }
        public string? TipoCuota { get; set; } 
        public DateTime FechaPago { get; set; }
    }

    public class FiltroNuevoAlumnoCongeladoExcelDTO
    {
        public DateTime FechaCongelamiento { get; set; }
        public int IdPeriodo { get; set; }
        public List<NuevoAlumnoCongeladoExcelDTO> ListaAlumnoCongelado { get; set; }
        public string Usuario { get; set; }
    }
}

