using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionDatoRemarketing : BaseIntegraEntity
    {
        public int IdAgendaTab { get; set; }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

    }
}
