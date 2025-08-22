using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TroncalCiudad : BaseIntegraEntity
    {

        public string Nombre { get; set; } = null!;

        public int IdTroncalPais { get; set; }

    }
}
