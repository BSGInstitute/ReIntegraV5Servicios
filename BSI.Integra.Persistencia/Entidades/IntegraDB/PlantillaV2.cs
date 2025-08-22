using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaV2 : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string Codigo { get; set; }
        public string? Uauario { get; set; }

    }
}
