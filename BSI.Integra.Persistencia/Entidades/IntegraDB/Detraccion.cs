using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Detraccion : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Valor { get; set; }
        public int? IdPais { get; set; }
    }
}
