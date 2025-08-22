using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstadoSeguimientoPreProcesoListaWhatsApp : BaseIntegraEntity
    {

        public string Nombre { get; set; } = null!;


        public List<SeguimientoPreProcesoListaWhatsApp> TSeguimientoPreProcesoListaWhatsApps { get; set; }

    }
}
