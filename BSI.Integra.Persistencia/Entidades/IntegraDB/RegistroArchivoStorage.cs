using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RegistroArchivoStorage : BaseIntegraEntity
    {
        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string Ruta { get; set; } = null!;
    }
}
