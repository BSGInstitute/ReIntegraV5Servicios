using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAdwordsRepository
    {

        public CampaniaAdwordsDTO ObtenerDatosCampaniaAdwords(string idcampania);
        public List<CampaniaAdwordsTodoDTO> ObtenerTodoCampaniaAdwords();
        public CampaniaAdwordsTodoDTO ObtenerCampaniaAdwordsPorId(int idcampania);
        public bool InsertarCampaniaAdwords(CampaniaAdwordsDTO datos);
        public bool ActualizarCampaniaAdwords(ActualzarCampaniaAdwordsDTO datos);
        public bool EliminarCampaniaAdwords(int id);

    }
}
