namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class InformacionMatriculaCabeceraDTO
    {
        public int Id { get; set; }
        public string? CodigoMatricula { get; set; }
        public int IdPaquete { get; set; }
        public int IdCronograma { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
    public class DetalleMatriculaDTO
    {
        public int IdPGeneral { get; set; }
        public int IdAlumno { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdPaquete { get; set; }
    }
    public class DatosGenerarCertificadoDTO
    {
        public int Id { get; set; }
        public int IdDetalle { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int IdSubEstadoMatricula { get; set; }
    }
    public class FiltroMatriculaAlumnoDTO
    {
        public string? IdMatricula { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? DNI { get; set; }
        public string? NombreAlumno { get; set; }
        public string? PersonalAsignado { get; set; }
        public string? CentroCosto { get; set; }
        public string? EstadoMatricula { get; set; }

    }
    public class DocumentoMatriculaDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdCriterioDocs { get; set; }
        public string NombreDocumento { get; set; }
        public bool Estado { get; set; }
        public int EstadoEntero { get; set; }
    }

    public class EscrituraCrepDTO
    {
        public string? CodigoMatricula { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPrograma { get; set; }
        public int Tipo { get; set; }
    }

    public class DatosMatriculaManualDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public int? IdAlumno { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaIniPago { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalPagar { get; set; }
        public int? NroCuotas { get; set; }
        public string Periodo { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdPEspecifico { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public string TituloAcuerdoPago { get; set; }
    }
}
