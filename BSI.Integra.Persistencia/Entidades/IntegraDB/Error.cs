using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Error : BaseIntegraEntity
    {
        public int Codigo { get; set; }
        public int IdErrorTipo { get; set; }
        public string Descripcion { get; set; }
    }
}
