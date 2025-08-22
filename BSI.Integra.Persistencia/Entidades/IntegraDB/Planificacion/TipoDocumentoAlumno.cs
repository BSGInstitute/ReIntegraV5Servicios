using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoDocumentoAlumno: BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
    }
}
