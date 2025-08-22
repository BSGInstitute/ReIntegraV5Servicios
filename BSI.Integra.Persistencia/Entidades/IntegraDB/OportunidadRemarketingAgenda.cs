using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadRemarketingAgenda : BaseIntegraEntity
    {
        public int? IdOportunidad { get; set; }
        public int IdAgendaTab { get; set; }
        public bool AplicaRedireccion { get; set; }
    }
}
