namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class FlujoCongelamientoPeriodoDTO
    {
        public int idPeriodo { get; set; }
        public string periodo { get; set; }
        public int idMatriculaCabecera { get; set; }
        public int idCoordAcademico { get; set; }
        public string coordinadorAcademico { get; set; }
        public int idPespecifico { get; set; }
        public string programa { get; set; }
        public string codigoMatricula { get; set; }
        public string alumno { get; set; }
        public DateTime fechaCuota { get; set; }
        public decimal montoCuota { get; set; }
        public DateTime? fechaPago { get; set; }
        public decimal pago { get; set; }
        public decimal saldoPendiente { get; set; }
        public decimal mora { get; set; }
        public int nroCuota { get; set; }
        public int nroSubCuota { get; set; }
        public string moneda { get; set; }
        public decimal totalUSD { get; set; }
        public decimal realUSD { get; set; }
        public decimal penUSD { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }


}
