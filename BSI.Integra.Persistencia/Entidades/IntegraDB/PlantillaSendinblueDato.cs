using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaSendinblueDato : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Etiqueta { get; set; }
        public int IdPlantillaSendinblue { get; set; }
        public int IdPlantillaSendinblueTipoEtiqueta { get; set; }

    }
}
