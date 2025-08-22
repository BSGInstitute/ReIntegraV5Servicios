using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialAsociacionCriterioVerificacion: BaseIntegraEntity
    {
        public int IdMaterialTipo { get; set; }
        public int IdMaterialCriterioVerificacion { get; set; }
    }
}
