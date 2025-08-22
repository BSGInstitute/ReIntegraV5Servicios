using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoCronograma : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int IdPais { get; set; }
        public string UrlDocumentoCronograma { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
