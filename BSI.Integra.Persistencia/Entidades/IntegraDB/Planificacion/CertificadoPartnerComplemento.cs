using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CertificadoPartnerComplemento : BaseIntegraEntity
    {
   
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? FrontalCentral { get; set; }
        public string? FrontalInferiorIzquierda { get; set; }
        public string? PosteriorCentral { get; set; }
        public string? PosteriorInferiorIzquierda { get; set; }
        public string? MencionEnCertificado { get; set; }
        public int? IdMigracion { get; set; }
        public bool Estado { get; set; }
    }
}
