using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class EscalaCalificacion : BaseIntegraEntity
    {

        public string Nombre { get; set; } = null!;
        public List<EscalaCalificacionDetalle> EscalaCalificacionDetalles { get; set; }
    }
}
