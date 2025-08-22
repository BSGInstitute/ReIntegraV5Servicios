using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DetallesProgramasDTO
    {
        public List<PgeneralParametroSeoPwDTO> ParametrosSeo { get; set; }
        //public List<PGeneralDescripcionDTO> DescripcionesGenerales { get; set; }
        //public List<AdicionalProgramaGeneralDTO> DescripcionesAdicionales { get; set; }
        public List<int> Expositores { get; set; }
        public List<int> PreRequisitos { get; set; }
        public List<int> Modalidad { get; set; }
        public List<int> Docentes { get; set; }
        //public List<int> Proveedor { get; set; }
        //public List<int> ProgramaAreasRelacionadas { get; set; }
        //public List<SuscripcionProgramaGeneralDTO> Suscripciones { get; set; }
        public List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficio { get; set; }
        public List<PgeneralConfiguracionPlantillaDTO> ConfiguracionPlantilla { get; set; }
        public List<PgeneralConfiguracionPlantillaDTO> ConfiguracionPlantillaConstancia { get; set; }
        public List<MontoPagoDTO> MontoPago { get; set; }
        public List<PGeneralVersionProgramaDetalleDTO> PgeneralVersionPrograma { get; set; }
        public List<PgeneralCodigoPartnerAlternoDTO> PgeneralCodigoPartner { get; set; }
        public List<PespecificoCodigoPartnerDTO> PespecificoCodigoPartner { get; set; }
        public List<PgeneralProyectoAplicacionAlternoDTO> PgeneralProyectoAplicacion { get; set; }
        public List<PgeneralForoAsignacionProveedorAlternoDTO> PgeneralForoAsignacionProveedor { get; set; }
        public List<PgeneralFechaOnlineDTO>? PgeneralFechaInicioOnline { get; set; }
        public List<PgeneralFechaAonlineDTO>? PgeneralFechaInicioAonline { get; set; }
        public List<PgeneralFechaPresencialDTO>? PgeneralFechaInicioPresencial { get; set; }
        public bool? CambioPGeneralForoAsignacion { get; set; }
    }
    public class PgeneralCodigoPartnerVersionProgramaDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneralCodigoPartner { get; set; }
        public int? IdVersionPrograma { get; set; }
        public string Usuario { get; set; }
    }
    public class PgeneralCodigoPartnerModalidadCursoDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneralCodigoPartner { get; set; }
        public int? IdModalidadCurso { get; set; }
        public string UsuarioCreacion { get; set; }
    }
    public class CuotasProgramaDTO
    {
        public int? IdBusqueda { get; set; }
        public string NombreCurso { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCuotaSeleccionada { get; set; }
        public string WebMoneda { get; set; }
        public decimal? WebTipoCambio { get; set; }
        public string NombrePespecifico { get; set; }
        public string DuracionPespecifico { get; set; }
        public string DuracionPGeneral { get; set; }
        public string TipoPrograma { get; set; }
        public int? NumeroCuotas { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? WebTotalPagar { get; set; }
        public bool EstadoCronogramaMod { get; set; }
        public List<ProgramaListaCuotaDTO> ListaCuotas { get; set; }
    }
    public class ProgramaListaCuotaDTO
    {
        public DateTime? FechaVencimiento { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Mora { get; set; }
        public int? NroCuota { get; set; }
        public string Moneda { get; set; }
        public bool? Cancelado { get; set; }
        public decimal? MontoCuotaDescuento { get; set; }
    }
}