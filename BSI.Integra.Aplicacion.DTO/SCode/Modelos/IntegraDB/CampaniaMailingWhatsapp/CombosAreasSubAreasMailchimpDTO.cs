using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class CombosAreasSubAreasMailchimpDTO
    {
        public List<ComboDTO> Areas { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> SubAreas { get; set; }
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ProgramaGeneral { get; set; }
    }
}
