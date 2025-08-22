using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ExamenFeedback : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string? Url { get; set; }
    }
}
