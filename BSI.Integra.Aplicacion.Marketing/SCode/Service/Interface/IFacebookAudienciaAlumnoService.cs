using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFacebookAudienciaAlumnoService
    {
        #region Metodos Base
        FacebookAudienciaAlumno Add(FacebookAudienciaAlumno entidad);
        FacebookAudienciaAlumno Update(FacebookAudienciaAlumno entidad);
        bool Delete(int id, string usuario);

        List<FacebookAudienciaAlumno> Add(List<FacebookAudienciaAlumno> listadoEntidad);
        List<FacebookAudienciaAlumno> Update(List<FacebookAudienciaAlumno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        }
}
