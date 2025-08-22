using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoPadreFrecuencia : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int IdFrecuencia { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public string Nota { get; set; } = null!;
        public ICollection<PespecificoPadreFrecuenciaSesion> PespecificoPadreFrecuenciaSesions { get; set; }
    }
}
