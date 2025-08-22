namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MontoPagoCronogramaDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public int? IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string? NombrePlural { get; set; } = null!;
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? Usuario { get; set; }
        public string? UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        // Extras
        public string? SimboloMoneda { get; set; }
        public string? CodigoBancario { get; set; }
        public int IdMedioPago { get; set; }
        public List<MontoPagoCronogramaDetalleDTO> ListaDetalleCuotas { get; set; } = new List<MontoPagoCronogramaDetalleDTO>();
    }
    public class MontoPagoCronogramaComboDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
    }
    public class MontoPagoCronogramaOportunidadDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public int? IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; } = null!;
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string? CodigoMatricula { get; set; }
    }
    public class MontoPagoCronogramaOportunidadDetalleDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public int? IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; } = null!;
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? TipoPersonal { get; set; }
        public List<MontoPagoCronogramaDetalleDTO> Detalle { get; set; } = new List<MontoPagoCronogramaDetalleDTO>();
        public List<MontoPagoOportunidadDTO> MontosPago { get; set; } = new List<MontoPagoOportunidadDTO>();
        public List<TipoDescuentoOportunidadDTO> TiposDescuento { get; set; } = new List<TipoDescuentoOportunidadDTO>();
    }
    public class MontoPagoCronogramaDocumentoDTO
    {
        public int? IdMontoPago { get; set; }
        public double? PrecioDescuento { get; set; }
        public int? IdMoneda { get; set; }
    }
    public class ResumenCronogramaDTO
    {
        public decimal MontoTotal { get; set; }
        public string NombrePluralMoneda { get; set; }
        public string SimboloMoneda { get; set; }
    }
    public class MontoPagadoDTO
    {
        public int Id { get; set; }
        public int IdMontoPago { get; set; }
        public string Moneda { get; set; }
        public decimal CostoOriginal { get; set; }
        public decimal Descuento { get; set; }
        public string PorcentajeDescuento { get; set; }
        public decimal CostoFinal { get; set; }
    }

    public class SesionesDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Tipo { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
    }
    public class CalcularCodigoMatriculaRespuestaDTO
    {
        public MontoPagoCronogramaDTO Cronograma { get; set; }
        public AlumnoDTO Alumno { get; set; }
        public PEspecificoInformacionDTO PEspecificoInformacion { get; set; }
    }
    public class DatosUsuarioPortalDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdAlumno { get; set; }
    }
    public class DetalleMontoPagoDTO
    {
        public string? Titulo { get; set; }
        public int OrdenBeneficio { get; set; }
    }
    public class MontoPagoCronogramaInterfazDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public int? IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; } = null!;
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string? CodigoMatricula { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        // Extras
        public string? SimboloMoneda { get; set; }
        public string? CodigoBancario { get; set; }
        public int IdMedioPago { get; set; }
        public List<MontoPagoCronogramaDetalleInterfazDTO> ListaDetalleCuotas { get; set; } = new List<MontoPagoCronogramaDetalleInterfazDTO>();
    }
    public class DatosMontosComplementariosDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public decimal Matricula { get; set; }
        public decimal Cuotas { get; set; }
        public int NroCuotas { get; set; }
        public string Version { get; set; }
        public string NombreCorto { get; set; }
    }
    public class MontoPagoCronogramaV2DTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdMontoPago { get; set; }
        public int IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; }
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string CodigoMatricula { get; set; }
        public Guid? IdMigracion { get; set; }

        //Extras
        public string TipoPersonal { get; set; }
        public string Usuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdAlumnoPortal { get; set; }
        public string CodigoBancario { get; set; }
        public String SimboloMoneda { get; set; }
        public int IdMedioPago { get; set; }
        public MontoPagoDTO MontoPago { get; set; } = new MontoPagoDTO();
        public List<TipoDescuentoOportunidadDTO> ListaTipoDescuento { get; set; } = new List<TipoDescuentoOportunidadDTO>();
        public List<MontoPagoOportunidadDTO> ListaMontosPagosVentas { get; set; } = new List<MontoPagoOportunidadDTO>();
        public List<MontoPagoCronogramaDetalleDTO> ListaDetalleCuotas { get; set; } = new List<MontoPagoCronogramaDetalleDTO>();
        //COMPLEMENTARIOS
        public List<DatosMontosComplementariosDTO> ListaMontosComplementarios { get; set; } = new List<DatosMontosComplementariosDTO>();
    }
    public class ResumenCronogramaMontoPagoDTO
    {
        public MontoPagoCronogramaOportunidadDetalleDTO Cronograma { get; set; } = new MontoPagoCronogramaOportunidadDetalleDTO();
        public MontoPagoCronogramaOportunidadDTO MontoPago { get; set; } = new MontoPagoCronogramaOportunidadDTO();
        public string VistaPortalWeb { get; set; }
        public string EstadoMatricula { get; set; }
    }

}
