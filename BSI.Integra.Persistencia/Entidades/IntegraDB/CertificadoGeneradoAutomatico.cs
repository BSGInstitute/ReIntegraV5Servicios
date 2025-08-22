using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CertificadoGeneradoAutomatico : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
        public string NombreArchivo { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdCronogramaPagoTarifario { get; set; }
        public int? IdMigracion { get; set; }
    }
}
