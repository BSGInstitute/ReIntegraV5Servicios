using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CertificadoDetalle : BaseIntegraEntity
    {
        public int? IdCertificadoBrochure { get; set; }
        public int IdCertificadoSolicitud { get; set; }
        public int IdCertificadoTipo { get; set; }
        public bool EsDiploma { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoCertificado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public decimal? Nota { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public decimal? EscalaCalificacion { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int? TamanioFuenteNombreAlumno { get; set; }
        public int? TamanioFuenteNombrePrograma { get; set; }
        public string NombreArchivoFrontal { get; set; }
        public string NombreArchivoReverso { get; set; }
        public string NombreArchivoFrontalImpresion { get; set; }
        public string NombreArchivoReversoImpresion { get; set; }
        public string RutaArchivo { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime? FechaUltimoEnvioAlumno { get; set; }
        public int? IdCertificadoTipoPrograma { get; set; }
        public bool? EsAsistenciaPartner { get; set; }
        public bool AplicaPartner { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdMigracion { get; set; }
    }
}
