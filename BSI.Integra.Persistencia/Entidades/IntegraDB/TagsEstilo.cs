using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TagsEstilo : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdTag { get; set; }
        public int IdEstilo { get; set; }
        public string Valor { get; set; }

    }

}
