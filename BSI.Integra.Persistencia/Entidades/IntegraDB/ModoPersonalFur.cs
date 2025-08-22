using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ModoPersonalFur : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdModoFur { get; set; }
        public bool FurVencido { get; set; }
    }
}
