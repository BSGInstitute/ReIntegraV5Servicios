
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralParametroSeoPwDTO
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? IdPgeneral { get; set; }
        public int IdParametroSeo { get; set; }
        public string NombreParametroSeo { get; set; }
    }
}
