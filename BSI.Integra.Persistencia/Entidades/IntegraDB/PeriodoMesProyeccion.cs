using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PeriodoMesProyeccion : BaseIntegraEntity
    {
        public string Periodo { get; set; } = null!;
        
        public int Cantidad { get; set; }
        
    }
}
