using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookAudiencium : BaseIntegraEntity
    {
     
        public int IdFiltroSegmento { get; set; }
    
        public string FacebookIdAudiencia { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string Subtipo { get; set; } = null!;

        public string RecursoArchivoCliente { get; set; } = null!;
        public Guid? IdMigracion { get; set; }



    }
}
