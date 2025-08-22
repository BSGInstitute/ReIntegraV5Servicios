using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAlumnoLogService
    {
        #region Metodos Base
        AlumnoLog Add(AlumnoLog entidad);
        AlumnoLog Update(AlumnoLog entidad);
        bool Delete(int id, string usuario);

        List<AlumnoLog> Add(List<AlumnoLog> listadoEntidad);
        List<AlumnoLog> Update(List<AlumnoLog> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<AlumnoLogAgendaFechaStringDTO> ObtenerAlumnoLogParaAgendaPorIdAlumno(int idAlumno);
        AlumnoLog ConstruirEntidadAlumnoLog(int idAlumno, string campoActualizado, string valorAnterior, string valorNuevo, string usuario);
    }
}
