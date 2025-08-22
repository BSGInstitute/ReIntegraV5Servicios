using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaSendinblue : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string HtmlContenido { get; set; }
        public string HtmlEditado { get; set; }
        public int IdPlantillaSendinblueBase { get; set; }


    }
}
