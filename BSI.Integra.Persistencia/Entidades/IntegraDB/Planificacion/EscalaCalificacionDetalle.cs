using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class EscalaCalificacionDetalle : BaseIntegraEntity
    {
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
