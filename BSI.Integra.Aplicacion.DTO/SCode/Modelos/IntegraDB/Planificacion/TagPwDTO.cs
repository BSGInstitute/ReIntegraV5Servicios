
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class TagPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? TagWebId { get; set; }
        public string Codigo { get; set; }
    }
    public class TagEntidadPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? TagWebId { get; set; }
        public string? Codigo { get; set; }
        public List<TagParametroSeoPwDTO>? ParametroSeoAsociados { get; set; }
    }
    public class ParametroSeoPortalWebDTO
    {
        public int Id { get; set; }
        public int IdParametroSeo { get; set; }
        public string NombreParametroSeo { get; set; }
        public string Descripcion { get; set; }
    }
    public class DatosTagPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
