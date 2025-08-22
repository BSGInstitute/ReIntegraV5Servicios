using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SolicitudCertificadoFisico : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonal { get; set; }
        public int? IdFur { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCourier { get; set; }
        public DateTime? FechaEntregaCourier { get; set; }
        public string EstadoCourier { get; set; }
        public int? IdPaisConsignado { get; set; }
        public int? IdCiudadConsignada { get; set; }
        public string CodigoSeguimiento { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public string CodigoSeguimientoEnvio { get; set; }
        public int IdEstadoCertificadoFisico { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public int? IdMigracion { get; set; }
    }
}
