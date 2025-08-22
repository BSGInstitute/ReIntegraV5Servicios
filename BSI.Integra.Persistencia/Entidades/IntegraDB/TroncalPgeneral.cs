using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TroncalPgeneral : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int? IdTroncalPartner { get; set; }
        public int Duracion { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdMigracion { get; set; }
    }
}
