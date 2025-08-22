namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AgendaFiltroDTO
    {
        public IEnumerable<DTO.ComboDTO> listaEstadoOcurrencia { get; set; }
        public IEnumerable<FaseOportunidadComboDTO> listaFaseOportunidad { get; set; }
        public IEnumerable<DTO.ComboDTO> listaTipoDato { get; set; }
        public IEnumerable<DTO.ComboDTO> listaOrigen { get; set; }
        public IEnumerable<DTO.ComboDTO> listaProbabilidadRegistro { get; set; }
        public IEnumerable<DTO.ComboDTO> listaCategoriaOrigen { get; set; }
    }
    public class GridFiltroDTO
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
    public class GridFiltrosDTO
    {
        public List<GridFiltroDTO> Filters { get; set; }
        public string Logic { get; set; }
    }
    public class FiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public bool? ConsiderarEnvioAutomatico { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class CompuestoAgendaFiltroDTO
    {
        public string? idOportunidad { get; set; }
        public string IdEstado { get; set; }
        public string IdAlumno { get; set; }
        public string IdsAsesores { get; set; }
        public string IdFaseOportunidad { get; set; }
        public string IdTipoDato { get; set; }
        public string IdOrigen { get; set; }
        public string Fecha { get; set; }
        public string IdCentroCosto { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public GridFiltrosDTO? filter { get; set; }
        public int IdPersonal { get; set; }
        public string categoria { get; set; }
        public string IdProbabilidadActual { get; set; }
    }

    public class ObtenerActividadFichaChatDTO
    {
        public int IdTab { get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public int IdAsesor { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
}
