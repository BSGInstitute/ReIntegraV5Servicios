using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ContadorBicLog : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int SinContactoManhana { get; set; }
        public int SinContactoTarde { get; set; }
        public int IdFaseOportunidad { get; set; }
        public DateTime FechaCalculo { get; set; }
        public ICollection<ContadorBicLogDetalle> ContadorBicLogDetalles { get; set; }
    }
}
