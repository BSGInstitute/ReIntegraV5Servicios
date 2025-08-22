using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacebookAudienciaAlumnoRepository : IGenericRepository<TFacebookAudienciaAlumno>
    {
        #region Metodos Base
        TFacebookAudienciaAlumno Add(FacebookAudienciaAlumno entidad);
        TFacebookAudienciaAlumno Update(FacebookAudienciaAlumno entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFacebookAudienciaAlumno> Add(IEnumerable<FacebookAudienciaAlumno> listadoEntidad);
        IEnumerable<TFacebookAudienciaAlumno> Update(IEnumerable<FacebookAudienciaAlumno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
       
    }
}