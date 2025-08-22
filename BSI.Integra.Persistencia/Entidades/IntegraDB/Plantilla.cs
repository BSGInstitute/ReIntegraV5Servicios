using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Plantilla : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public int Documento { get; set; }
        public bool? EstadoPlantillaIntegra { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }

        public List<PlantillaClaveValor> PlantillaClaveValor { get; set; }
        public List<FaseByPlantilla> FaseByPlantilla { get; set; }
        public List<PlantillaAsociacionModuloSistema> PlantillaAsociacionModuloSistema { get; set; }
        public Plantilla()
        {
            PlantillaClaveValor = new List<PlantillaClaveValor>();
            FaseByPlantilla = new List<FaseByPlantilla>();
            PlantillaAsociacionModuloSistema = new List<PlantillaAsociacionModuloSistema>();
        }

    }

}
