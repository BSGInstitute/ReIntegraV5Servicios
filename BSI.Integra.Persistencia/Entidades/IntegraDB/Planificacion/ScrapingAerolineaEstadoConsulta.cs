using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ScrapingAerolineaEstadoConsulta : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
