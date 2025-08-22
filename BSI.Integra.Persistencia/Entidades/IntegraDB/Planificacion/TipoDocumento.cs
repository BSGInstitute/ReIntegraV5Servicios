using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoDocumento : BaseIntegraEntity
    {
        public int Clave { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
