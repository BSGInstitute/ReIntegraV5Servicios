using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialTipo: BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; } = " ";
        public virtual List<MaterialAsociacionAccion> MaterialAsociacionAccions { get; set; }
        public virtual List<MaterialAsociacionCriterioVerificacion> MaterialAsociacionCriterioVerificacions { get; set; }
        public virtual List<MaterialAsociacionVersion> MaterialAsociacionVersions { get; set; }
    }
}
