using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FlujoOcurrencia : BaseIntegraEntity
    {
        public int IdFlujoActividad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public bool CerrarSeguimiento { get; set; }
        public int? IdFaseDestino { get; set; }
        public int? IdFlujoActividadSiguiente { get; set; }
        public int? IdMigracion { get; set; }
    }
}
