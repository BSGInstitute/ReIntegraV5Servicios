using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaSendinblueEtiqueta : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        public int IdPlantillaSendinblueTipoEtiqueta { get; set; }

    }
}
