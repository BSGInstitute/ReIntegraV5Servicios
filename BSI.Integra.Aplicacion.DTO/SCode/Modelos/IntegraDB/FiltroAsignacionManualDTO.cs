namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroAsignacionManualDTO
    {
        public IEnumerable<ComboFiltroDTO> filtroCategoriaOrigen { get; set; }
        public IEnumerable<PaisZonaHorariaComboDTO> filtroPais { get; set; }
        public IEnumerable<ComboDTO> filtroProbabilidad { get; set; }
        public IEnumerable<ComboDTO> filtroIndustria { get; set; }
        public IEnumerable<ComboDTO> filtroCargo { get; set; }
        public IEnumerable<ComboDTO> filtroAreaFormacion { get; set; }
        public IEnumerable<ComboDTO> filtroAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> filtroTipoDato { get; set; }
        public IEnumerable<ComboDTO> filtroPgeneral { get; set; }
        public IEnumerable<ComboDTO> filtroAreaCapacitacion { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> filtroSubAreaCapacitacion { get; set; }
        public IEnumerable<FaseOportunidadComboDTO> filtroFaseOportunidad { get; set; }
        public IEnumerable<ComboDTO> filtroCentroCosto { get; set; }
        public IEnumerable<PersonalAutocompleteDTO> filtroPersonal { get; set; }
        public IEnumerable<ComboDTO> filtroOrigen { get; set; }
        public IEnumerable<FiltroDTO> filtroTipoCategoriaOrigen { get; set; }
        public IEnumerable<ComboDTO> filtroOperadorComparacion { get; set; }
    }

}
