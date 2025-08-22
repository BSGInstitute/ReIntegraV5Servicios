using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class UrlSubContenedor : BaseIntegraEntity
    {
        public int Id { get; set; }

        public int IdUrlBlockStorage { get; set; }

        public string Nombre { get; set; } = null!;

        public string Ruta { get; set; } = null!;

        public bool Estado { get; set; }

        public int? IdMigracion { get; set; }
    }
}
