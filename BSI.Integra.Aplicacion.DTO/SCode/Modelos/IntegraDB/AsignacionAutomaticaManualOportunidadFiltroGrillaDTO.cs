using BSI.Integra.Aplicacion.DTO.SCode;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignacionAutomaticaManualOportunidadFiltroGrillaDTO
    {
        //public PaginadorDTO paginador { get; set; }
        //public List<GridFilterDTO>? filter { get; set; }

        //Filtro,sort,skip,take
        public FiltroKendoGridDTO? filter { get; set; }
        public AsignacionManualOportunidadFiltroDTO filtro { get; set; }
    }
}
