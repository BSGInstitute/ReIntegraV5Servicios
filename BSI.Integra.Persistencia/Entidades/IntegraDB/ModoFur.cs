using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ModoFurE : BaseIntegraEntity
    {
        public int ModoFur { get; set; }
        public string Descripcion { get; set; } = null!;
    }
}
