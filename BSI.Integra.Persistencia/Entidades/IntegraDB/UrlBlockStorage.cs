using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class UrlBlockStorage : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Ruta { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdProveedorNube { get; set; }
        public bool EsVisibleModuloArchivos { get; set; }
        public bool AplicaSubcontenedores { get; set; }
        public bool AplicaSubidaMultiple { get; set; }
        public int Subdominio { get; set; }
        public int IdMigracion { get; set; }


    }
}
