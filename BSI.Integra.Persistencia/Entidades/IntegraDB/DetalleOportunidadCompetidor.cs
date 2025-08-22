using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DetalleOportunidadCompetidor : BaseIntegraEntity
    {
        public int IdOportunidadCompetidor { get; set; }
        public int IdCompetidor { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
