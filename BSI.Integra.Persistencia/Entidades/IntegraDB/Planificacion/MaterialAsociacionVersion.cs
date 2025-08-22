using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialAsociacionVersion: BaseIntegraEntity
    {
        public int IdMaterialTipo { get; set; }
        public int IdMaterialVersion { get; set; }
    }
}
