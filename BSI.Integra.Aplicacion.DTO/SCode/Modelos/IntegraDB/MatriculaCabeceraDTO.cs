namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MatriculaCabeceraDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; } = null!;
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public int IdEstadoPagoMatricula { get; set; }
        public string? EstadoMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public string? EmpresaRuc { get; set; }
        public string? EmpresaNombre { get; set; }
        public string? EmpresaContacto { get; set; }
        public string? EmpresaEmail { get; set; }
        public string? EmpresaPaga { get; set; }
        public string? EmpresaObservaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public int? IdCoordinador { get; set; }
        public int? IdAsesor { get; set; }
        public int? IdEstado_Matricula { get; set; }
        public string? FechaSuspendido { get; set; }
        public string? UsuarioCoordinadorAcademico { get; set; }
        public string? ObservacionGeneralOperaciones { get; set; }
        public string? UsuarioCoordinadorSupervision { get; set; }
        public int? IdCronograma { get; set; }
        public int? IdPeriodo { get; set; }
        public string? UsuarioCoordinadorPreAsignacion { get; set; }
        public bool? VerificacionConforme { get; set; }
        public bool? FechaMatriculaValidada { get; set; }
        public bool? FechaPagoValidada { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public int? GrupoCurso { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? IdPaquete { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? IdEstadoMatriculaCertificado { get; set; }
        public int? IdSubEstadoMatriculaCertificado { get; set; }
        public bool? EsInhouse { get; set; }
        public DateTime? FechaPorMatricularMatriculado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class MatriculaCabeceraComboDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; } = null!;
    }
    public class MatriculaCabeceraCodigoFechaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; } = null!;
        public DateTime? FechaMatricula { get; set; }
    }
    public class DetalleCursoActualAulaVirtualDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string NombreCurso { get; set; }
    }
    public class DetalleAccesoAulaVirtualDTO
    {
        public string UsuarioMoodle { get; set; } = "";
        public string ClaveMoodle { get; set; } = "";
    }
    public class DetalleAccesoPortalWebDTO
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
    public class InformacionBeneficioSolicitadoDTO
    {
        public int Id { get; set; }
        public string Beneficio { get; set; }
        public string Programa { get; set; }
        public string CentroCosto { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public string Coordinador { get; set; }
        public string EstadoSolicitud { get; set; }
        public DateTime? FechaEntregaBeneficio { get; set; }
    }
    public class IdentificadorMatriculaComboDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdOportunidad { get; set; }
        public string TipoModalidadPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public string VersionPrograma { get; set; }
    }
    public class MatriculaTemporalDTO
    {
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
    }
    public class NuevoCompromisoAlumnoDTO
    {
        public int Id { get; set; }
        public int? NroSubCuota { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public string Usuario { get; set; }
        public decimal? MontoCompromiso { get; set; }
        public int? Version { get; set; }
        public int? IdMoneda { get; set; }
        public bool? Flag { get; set; }
    }
    public class DatosAlumnoCoordinadorMatriculaCabeceraDTO
    {
        public int IdAlumno { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
    }
    public class DatosMatriculaDTO
    {
        public string Id { get; set; }
        public int IdPEspecifico { get; set; }
        public string Moneda { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal TotalAPagar { get; set; }
        public int NroCuotas { get; set; }
        public string EstadoMatricula { get; set; }
        public int? Periodo { get; set; }
        public string Programa { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public int? Paquete { get; set; }
        public string Titulo { get; set; }
        public string Observaciones { get; set; }
        public string EmpresaPaga { get; set; }
        public string EmpresaNombre { get; set; }
        public int IdCoordinador { get; set; }
        public int IdAsesor { get; set; }
    }
    public class BeneficiosCodigoMatriculaDTO
    {
        public string Titulo { get; set; }
        public string CodigoMatricula { get; set; }
    }

    public class MatriculaActualizarDTO
    {
        public string Codigomatricula { get; set; }
        public string Estado { get; set; }
        public Nullable<int> Periodo { get; set; }
        public Nullable<int> Programa { get; set; }
        public Nullable<int> Asesor { get; set; }
        public Nullable<int> Coordinador { get; set; }
        public string? Observaciones { get; set; }
        public bool EmpresaPaga { get; set; }
        public string EmpresaNombre { get; set; }
        public string usuario { get; set; }
    }


    public class MatriculaPespecificoAlumnoDTO
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
        public string? Duracion { get; set; }
        public string Tipo { get; set; }
    }
    public class MatriculaCronogramaDTO
    {

        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }//Centrocosto 
        public int IdCoordinador { get; set; }
        public int IdAsesor { get; set; }
        public string Codigobanco { get; set; }
        public List<int> ListaIdDocumento { get; set; }
        public string Periodo { get; set; }
        public int IdMoneda { get; set; }
        public string AcuerdoPago { get; set; }
        public double TipoCambio { get; set; }
        public double TotalPagar { get; set; }
        public int NroCuotas { get; set; }
        public DateTime FechaInicioPago { get; set; }
        public bool OpcionPagoNDias { get; set; }
        public int? Ndias { get; set; }
        public List<int> CursosMatriculados { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class IdMatriculaDniDTO
    {
        public string DNI { get; set; }
        public int idMatriculaCabecera { get; set; }
    }
    public class IdMatriculaCorreoDTO
    {
        public string correo { get; set; }
        public int idMatriculaCabecera { get; set; }
    }
    public class BeneficioDatosAdicionalesDTO
    {
        public int IdPgeneral { get; set; }
        public int IdConfiguracionBeneficio { get; set; }
        public int IdDatoAdicional { get; set; }
    }
    public class CodigoMatriculaStringDTO
    {
        public string CodigoMatricula { get; set; }
    }
    public partial class MatriculaCabeceraDetallesDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public string? PGeneral { get; set; }
        public int IdBusqueda { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
    }
    public class ListaCuotaPagoDTO
    {
        public int IdCuota { get; set; }
        public int NroCuota { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MontoPagado { get; set; }
        public string? TipoCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public bool Cancelado { get; set; }
        public string? Moneda { get; set; }
        public string? WebMoneda { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string? Simbolo { get; set; }
        public string? NombreMoneda { get; set; }
        public decimal? MoraCalculada { get; set; }
        public int Version { get; set; }
        public bool NextCuota { get; set; }
    }
    public partial class ResumenCronogramaPagoDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdPGeneral { get; set; }
        public string? PGeneral { get; set; }
        public int NumeroCuota { get; set; }
        public int CuotasPagadas { get; set; }
        public int CuotasPendientes { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
    public class IdMatriculaCelularDTO
    {
        public string celular { get; set; }
        public int idMatriculaCabecera { get; set; }
    }

    public class PaisMatriculaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
    }

}