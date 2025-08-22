using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaSendinblueImagen : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string Extension { get; set; }

    }
}
