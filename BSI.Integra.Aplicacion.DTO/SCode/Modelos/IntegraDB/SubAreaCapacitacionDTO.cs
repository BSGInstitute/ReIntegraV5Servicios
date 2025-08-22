using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SubAreaCapacitacionDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdSubArea { get; set; }
        public string DescripcionHtml { get; set; }
        public List<SubAreaParametroSeoPwDTO> ListaParametro { get; set; }
    }
    public class SubAreaCapacitacionFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdAreaCapacitacion { get; set; }
    }
    public class SubAreaCapacitacionAlternoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdSubArea { get; set; }
        public string DescripcionHtml { get; set; }
        public string NombreAreaCapacitacion { get; set; }
    }
}
