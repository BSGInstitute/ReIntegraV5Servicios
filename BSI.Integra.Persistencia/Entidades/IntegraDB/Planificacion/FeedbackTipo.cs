using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FeedbackTipo : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
