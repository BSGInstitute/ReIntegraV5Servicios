using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAlumnoLogRepository : IGenericRepository<TAlumnoLog>
    {
        #region Metodos Base
        TAlumnoLog Add(AlumnoLog entidad);
        TAlumnoLog Update(AlumnoLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAlumnoLog> Add(IEnumerable<AlumnoLog> listadoEntidad);
        IEnumerable<TAlumnoLog> Update(IEnumerable<AlumnoLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AlumnoLogAgendaDTO> ObtenerAlumnoLogParaAgendaPorIdAlumno(int idAlumno);
    }
}