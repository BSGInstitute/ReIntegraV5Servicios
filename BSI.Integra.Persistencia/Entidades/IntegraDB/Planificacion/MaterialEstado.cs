using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialEstado : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
