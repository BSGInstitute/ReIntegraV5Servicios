using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialAsociacionAccion: BaseIntegraEntity
    {
        public int IdMaterialTipo { get; set; }
        public int IdMaterialAccion { get; set; }
    }
}
