using BSI.Integra.Aplicacion.DTO.SCode;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RevertirFaseFiltroGrillaDTO
    {
        public PaginadorDTO? paginador { get; set; }
        public RevertirFaseFiltroDTO? filtro { get; set; }
        public List<GridFilterDTO>?  filter { get; set; }
}
}
