using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaV2SeccionEstilo : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdPlanitillav2Seccion { get; set; }
        public int IdEstilo { get; set; }
        public string Valor { get; set; }
        public string? Uauario { get; set; }

    }
}
