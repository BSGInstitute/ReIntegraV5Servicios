namespace BSI.Integra.Aplicacion.DTO.SCode
{
    public partial class GridFiltersDTO
    {
        public List<GridFilterDTO> Filters { get; set; }
        public string Logic { get; set; }//"and"

        public GridFiltersDTO()
        {
            Filters = new List<GridFilterDTO>();
            Logic = string.Empty;
        }
    }

    public class GridFilterDTO
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }

    public class GridSortDTO
    {
        public string Field { get; set; }
        public string? Dir { get; set; }
    }
    public class FiltroKendoGridDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<GridSortDTO>? Sort { get; set; }
        public GridFiltersDTO? Filter { get; set; }
    }
}
