namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ComprobantePagoOportunidadDTO
    {
        public int IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string? Dni { get; set; }
        public string Correo { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public string? NombreCiudad { get; set; }
        public string TipoComprobante { get; set; }
        public string? NroDocumento { get; set; }
        public string? NombreRazonSocial { get; set; }
        public string? Direccion { get; set; }
        public int BitComprobante { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdAsesor { get; set; }
        public int? IdOportunidad { get; set; }
        public string? Comentario { get; set; }
    }
    public class ComprobantePagoOportunidadBaseObjectDTO
    {
        public int Id { get; set; }
        public int? IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public string NombreCiudad { get; set; }
        public string TipoComprobante { get; set; }
        public string NroDocumento { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Direccion { get; set; }
        public int BitComprobante { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdAsesor { get; set; }
        public int? IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }

    public class ComprobantePagoAlumnoDTO
    {
        public string MedioPago { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string ConceptoPago { get; set; }
        public string Moneda { get; set; }
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? TipoComprobante { get; set; }
        public string NroDocumento { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Observacion { get; set; }
        public int Id { get; set; }
    }

    public class filtroReporteComprobanteDTO
    {
        public string IdFormaPago { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string Comprobante { get; set; }
        public int? IdPeriodo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public int TipoFechaPago { get; set; }
    }

    public class ActualizarComprobantePagoAlumnoDTO
    {
        public int Id { get; set; }
        public int TipoComprobante { get; set; }
        public string NroDocumento { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Observacion { get; set; }
        public string Usuario { get; set; }
    }
}
