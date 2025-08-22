using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAdwordsService
    {
        public IdDTO ProcesarGoogleLeads(GoogleFormularioLeadgenDTO leads);
        public bool CrearOportunidadWebhookAdwords(string idAsignacionAutomatica);
        public List<CampaniaAdwordsTodoDTO> ObtenerTodoCampaniaAdwords();
        public CampaniaAdwordsTodoDTO ObtenerCampaniaAdwords(int id);
        public bool InsertarCampaniaAdwords(CampaniaAdwordsDTO datos);
        public bool ActualizarCampaniaAdwords(ActualzarCampaniaAdwordsDTO datos);
        public bool EliminarCampaniaAdwords(int id);

    }
}
