using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MontoPagoSuscripcion : BaseIntegraEntity
    {
        public int IdMontoPago { get; set; }

        public int IdSuscripcionProgramaGeneral { get; set; }

    }
}
