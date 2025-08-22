using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoCronogramaGrupo : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int IdPais { get; set; }
        public string UrlDocumentoCronogramaGrupo { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
