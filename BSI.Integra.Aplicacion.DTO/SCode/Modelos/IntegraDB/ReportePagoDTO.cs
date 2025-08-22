namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroReportePagoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdCambio { get; set; }
        public string CodigoMatricula { get; set; }
        
    }
    public class PagoAlumnoDTO
    {
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public string Alumno { get; set; }
        public string CodigoAlumno { get; set; }
        public DateTime FechaPagoOriginal { get; set; }
        public DateTime FechaPago { get; set; }
        public string DiaPago { get; set; }
        public DateTime FechaPagoReal { get; set; }
        public int DiasDeposito { get; set; }
        public int DiasDisponible { get; set; }
        public bool CuentaFeriados { get; set; }
        public bool ConsideraVSD { get; set; }
        public bool ConsiderarDiasHabilesLV { get; set; }
        public bool ConsiderarDiasHabilesLS { get; set; }
        public DateTime? FechaDepositaron { get; set; }
        public DateTime? FechaDisponible { get; set; }
        public string EstadoEfectivo { get; set; }
        public string Cuota_SubCuota { get; set; }
        public string FechaCuota { get; set; }
        public string FormaPago { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public DateTime? FechaProcesoPagoReal { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public int? IdCiudad { get; set; }
        public decimal Cuota { get; set; }
        public string MonedaCuota { get; set; }
    }


}

