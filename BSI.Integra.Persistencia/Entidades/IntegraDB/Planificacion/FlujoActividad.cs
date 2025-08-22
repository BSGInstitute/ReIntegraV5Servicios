using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FlujoActividad : BaseIntegraEntity
    {
        public int IdFlujoFase { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
