using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstadoCertificadoFisico : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
