using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaAsociacionModuloSistema : BaseIntegraEntity
    {
        public int IdPlantilla { get; set; }
        public int IdModuloSistema { get; set; }
        public string NombreModuloSistem { get; set; }
    }
}
