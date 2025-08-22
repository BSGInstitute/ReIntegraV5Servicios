using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp
{
    internal interface ICampaniasMailingWhatsappService
    {
        List<CampaniaMailingDTO> ObtenerCampaniaMailingGrid();
        List<CategoriaOrigenFiltroDTO> ObtenerListaCategoriaOrigen();
        List<CategoriaOrigenFiltroDTO> ObtenerListaCampaniaMailingDetalle(int Id);
        List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor();
        List<ComboDTO> ObtenerTodoFiltro();
    }
}
