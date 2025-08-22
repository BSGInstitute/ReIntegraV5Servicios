using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MandrilEnvioCorreo : BaseIntegraEntity
    {

        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int IdMandrilTipoAsignacion { get; set; }
        public int? EstadoEnvio { get; set; }
        public int IdMandrilTipoEnvio { get; set; }
        public string? Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string? FkMandril { get; set; }
        public bool EsEnvioMasivo { get; set; }
    }
}
