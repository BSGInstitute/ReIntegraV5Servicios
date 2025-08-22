namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsesorChatFiltroDTO
    {
        public IEnumerable<AsesorNombreFiltroDTO> listaAsesores { get; set; }
        public IEnumerable<ComboDTO> listaAreas { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> listaSubAreas { get; set; }
        public IEnumerable<PGeneralSubAreaCapacitacionFiltroDTO> listaProgramas { get; set; }
        public IEnumerable<PaisComboDTO> listaPaises { get; set; }
    }
}
