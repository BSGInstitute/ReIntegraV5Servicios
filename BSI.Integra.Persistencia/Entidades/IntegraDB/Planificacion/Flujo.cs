using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class Flujo : BaseIntegraEntity
    {
        public int IdModalidadCurso { get; set; }
        public int IdClasificacionUbicacionDocente { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
