using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaV2Seccion : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdPlantillaV2 { get; set; }
        public int IdSeccion { get; set; }
        public string? Uauario { get; set; }

    }
}
