using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoLandingPage : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Uauario { get; set; }

    }
}
