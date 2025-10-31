using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampoContacto : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? NombreLabel {get; set;}
        public string TipoControl { get; set; } = null!;
        public int ValoresPreEstablecidos { get; set; }
        public string Procedimiento { get; set; } = null!;

    }
}
