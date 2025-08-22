using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Origen : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdTipodato { get; set; }
        public int Prioridad { get; set; }
        public int IdCategoriaOrigen { get; set; }
    }
}
