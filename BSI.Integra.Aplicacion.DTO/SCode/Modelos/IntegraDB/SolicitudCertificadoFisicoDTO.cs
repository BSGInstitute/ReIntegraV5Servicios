namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DatosReporteEnvioCertificadoFisicoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Courier { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string CodigoSeguimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Url { get; set; }
        public DateTime? FechaEntregaCourier { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public string EstadoCourier { get; set; }
        public string Usuario { get; set; }
    }
    public class DatosRegistroEnvioFisicoDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public string Referencia { get; set; }
        public string CodigoPostal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Pais { get; set; }
        public int? IdPersonal { get; set; }
        public string Courier { get; set; }
        public string CodigoSeguimiento { get; set; }
    }

    public class DatosEnvioAlumnoDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public string Region { get; set; }
        public string Distrito { get; set; }
        public string Ciudad { get; set; }
        public string Referencia { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int IdSolicitudCertificadoFisico { get; set; }
        public string Usuario { get; set; }
    }

    public class SolicitudCertificadoFisicoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonal { get; set; }
        public int? IdFur { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCourier { get; set; }
        public DateTime? FechaEntregaCourier { get; set; }
        public string? EstadoCourier { get; set; }
        public int? IdPaisConsignado { get; set; }
        public int? IdCiudadConsignada { get; set; }
        public string? CodigoSeguimiento { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public string? CodigoSeguimientoEnvio { get; set; }
        public int IdEstadoCertificadoFisico { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public string Usuario { get; set; }
    }

    public class filtroSolicitudCertificadoFisicoDTO
    {
        public List<int>? ListaCoordinador { get; set; } = null;
        public string? CodigoSeguimiento { get; set; } = null;
        public int? IdMatriculaCabecera { get; set; } = null;
        public int? IdEstadoCertificadoFisico { get; set; } = null;
        public string? IdPersonal { get; set; } = null;
        public DateTime? FechaInicio { get; set; } = null;
        public DateTime? FechaFin { get; set; } = null;
        public int ConFiltros { get; set; }
    }

    public class DataSolicitudCertificadoFisicoDTO
    {
        public int Id { get; set; }
        public string NombreCertificado { get; set; }
        public string NombreAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CentroCosto { get; set; }
        public string EstadoMatricula { get; set; }
        public string Version { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaRegistroSolicitud { get; set; }
        public int? IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public DateTime? FechaEntregaEsperada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public DateTime? FechaEntregaCourier { get; set; }
        public string CodigoSeguimientoEnvio { get; set; }
        public int IdEstadoCertificadoFisico { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public string DireccionEnvio { get; set; }
        public int Impreso { get; set; }
        public string EstadoCourier { get; set; }
        public int? IdPaisConsignado { get; set; }
        public string Pais { get; set; }
        public int? IdCiudadConsignada { get; set; }
        public string Ciudad { get; set; }
        public string CodigoSeguimiento { get; set; }
        public int? IdCourier { get; set; }
        public string Courier { get; set; }
        public string NombreArchivo { get; set; }
    }

}
