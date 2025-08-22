using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Ciudad : BaseIntegraEntity
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
        public int? LongCelularAlterno { get; set; }

    }
}
