namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class CongelamientoReporteFlujoDTO
    {
        public DateTime FechaCierre { get; set; }
        public string NombrePersonalAsesor { get; set; }
        public string NombrePersonalCoordinador { get; set; }
        public int NumeroIS { get; set; }
        public int ContratoVoz { get; set; }
        public int ContratoFirmado { get; set; }
        public int Empresa { get; set; }
        public int SinDocumentacion { get; set; }
        public decimal Convenio { get; set; }
        public decimal SinDocumentacionP { get; set; }
        public int Observacion { get; set; }
        public int PagoContado { get; set; }
        public int Deuda { get; set; }

    }

    public class FlujoCongelamientoDTO
    {
        public string fechaCongelamiento { get; set; }
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

    public class RecibirDatosReporteFlujoMaestroDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public Nullable<DateTime> FechaVencimiento { get; set; }
        public Nullable<DateTime> FechaCongelamiento { get; set; }
        public decimal MontoCuota { get; set; }
        public string EstadoMatricula { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string CoordinadorCobranza { get; set; }

    }

    public class ReporteFlujoMaestroFiltroDTO
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaCongelamiento { get; set; }
        public string EstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
    }

    public class RecibirDatosCoordinadores
    {
        public string Coordinadores { get; set; }
    }

    public class MatriculaInHouseDTO
    {
        public string CodigoMatricula { get; set; }
        public string Cuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }

    public class ListaCambiosPorPeriodoPruebaDTO
    {
        public string CodigoMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public float MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public DateTime PeriodoCambio { get; set; }
    }

    public class EditarReporteFlujoMaestroFiltroDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public Nullable<DateTime> FechaVencimiento { get; set; }
        public Nullable<DateTime> FechaCongelamiento { get; set; }
        public decimal MontoCuota { get; set; }
        public string EstadoMatricula { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string CoordinadorCobranza { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }


    }

    public class SubidaExcelDTO
    {
        public string CodigoMatricula { get; set; }
        public DateTime FechaVencimientoCambio { get; set; }
        public float MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public DateTime PeriodoCambio { get; set; }
    }

    public class ActualizarInhouseDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int EsInhouse { get; set; }
    }

    public class ActualizarInhouseCodigoDTO
    {
        public string CodigoMatricula { get; set; }
        public int EsInhouse { get; set; }
    }

    public class ActualizarCronogramaCongeladoOriginalesDTO
    {
        public string CoordinadoraAcademica { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
    }

    public class CongeladosDTO
    {
        public string EstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public string PaisAlumno { get; set; }
        public int MatId { get; set; }
        public float Cuota { get; set; }
        public int NroCuota { get; set; }
        public float NroSubCuota { get; set; }
        public string Moneda { get; set; }
        public float CuotaDolarea { get; set; }
        public string CodigoMatricula { get; set; }
        public string PeriodoPorFechaVencimiento { get; set; }
        public string CoordinadoraAcademica { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string FechaVencimiento { get; set; }
    }
}
