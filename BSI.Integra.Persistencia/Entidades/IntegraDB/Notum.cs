using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Notum : BaseIntegraEntity
    {
        public int IdEvaluacion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public decimal Nota { get; set; }
    }
}
