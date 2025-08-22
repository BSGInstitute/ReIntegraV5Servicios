using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Seccion : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EstadoTexto { get; set; }
        public string? Uauario { get; set; }

    }
}
