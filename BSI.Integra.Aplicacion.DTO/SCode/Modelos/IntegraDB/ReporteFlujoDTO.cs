namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteFlujoDTO
    {
        public string codigo { get; set; }
        public string EstadoP { get; set; }
        public string EstadoFinanzas { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public DateTime fechavencimiento { get; set; }
        public decimal cuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal montopagado { get; set; }
        public decimal saldopendiente { get; set; }
        public decimal mora { get; set; }
        public string nrocuota { get; set; }
        public string moneda { get; set; }
        public decimal TotalCuotaD { get; set; }
        public decimal RealPagoD { get; set; }
        public decimal SaldoPendienteD { get; set; }
        public string OrigenPrograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string Email { get; set; }
        public string TelFijo { get; set; }
        public string TelCel { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string DocumentoPago { get; set; }
        public string RazonSocial { get; set; }
        public string Coordinadoraacademica { get; set; }
        public string Coordinadoracobranza { get; set; }
        public string monedaPago { get; set; }
        public DateTime? fechavencimientoOriginal { get; set; }
        public decimal? cuotaOriginal { get; set; }
        public decimal? Modificacion { get; set; }
        public string EstadoMatriculaOperaciones { get; set; }
        public string PaisAlumno { get; set; }
        public string Paquete { get; set; }
    }

    public class FiltroFechaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? CodigoMatricula { get; set; } 
        public int? IdCodigoPais { get; set; }     

    }
  
    public class CongelarFlujoDTO
    {
        public DateTime? FechaCongelamiento { get; set; } = null;
        public int? IdPeriodo { get; set; } = null;
    }

    public class ReporteFlujoFinalDTO
    {
        public List<ReporteFlujoDTO> reporteFlujo { get; set; }
        public byte[] dataExport { get; set; }
    }
}
