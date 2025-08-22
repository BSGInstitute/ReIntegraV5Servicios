using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface ICampaniasMailingWhatsappRepository
    {
        List<CampaniaMailingDTO> ObtenerListaCampaniaMailing();
        List<CategoriaOrigenFiltroDTO> ObtenerListaCategoriaOrigen();
        List<CategoriaOrigenFiltroDTO> ObtenerListaCampaniaMailingDetalle(int Id);
        List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor();
        public List<FiltroDTO> ObtenerParaFiltro();
        List<ComboDTO> ObtenerTodoFiltro();
        List<PrioridadesDTO> ObtenerListaCampaniaMailingDetalleConProgramas(int idCampaniaMailing);
    }
}
