using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SeguimientoPreProcesoListaWhatsApp : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdEstadoSeguimientoPreProcesoListaWhatsApp { get; set; }
        public int IdConjuntoLista { get; set; }

        public List<ConjuntoLista> IdConjuntoListaNavigation { get; set; } = null!;
        public List<EstadoSeguimientoPreProcesoListaWhatsApp> IdEstadoSeguimientoPreProcesoListaWhatsAppNavigation { get; set; }

    }
}
