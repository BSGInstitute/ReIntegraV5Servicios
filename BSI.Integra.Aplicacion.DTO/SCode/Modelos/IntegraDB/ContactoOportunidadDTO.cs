namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ContactoOportunidadDTO
    {
        public List<PaisZonaHorariaDTO> Paises { get; set; }
        public List<CiudadComboDTO> Ciudades { get; set; }
        public List<ComboDTO> TipoDatoChats { get; set; }
        public List<FaseOportunidadComboDTO> FaseOportunidades { get; set; }
        public List<ComboFiltroDTO> CategoriaOrigenes { get; set; }
        public List<ComboDTO> Cargos { get; set; }
        public List<ComboDTO> AreasFormacion { get; set; }
        public List<ComboDTO> AreasTrabajo { get; set; }
        public List<ComboDTO> Industrias { get; set; }
        public List<ComboFiltroDTO> Origenes { get; set; }

    }
}
