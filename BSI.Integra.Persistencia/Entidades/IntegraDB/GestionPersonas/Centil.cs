using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class Centil : BaseIntegraEntity
    {
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public int? IdSexo { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public int? IdMigracion { get; set; }
        public decimal? CentilAdicional { get; set; }
        public int? Version { get; set; }
        public bool? EsVigente { get; set; }
    }
}
