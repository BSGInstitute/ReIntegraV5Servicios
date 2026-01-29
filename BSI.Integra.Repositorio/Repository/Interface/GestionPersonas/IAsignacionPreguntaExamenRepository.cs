using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IAsignacionPreguntaExamenRepository : IGenericRepository<TAsignacionPreguntaExaman>
    {
        #region Metodos Base
        /*TCentil Add(AsignacionPreguntaExamen entidad);
        TCentil Update(AsignacionPreguntaExamen entidad);
        IEnumerable<TCentil> Add(IEnumerable<AsignacionPreguntaExamen> listadoEntidad);
        IEnumerable<TCentil> Update(IEnumerable<AsignacionPreguntaExamen> listadoEntidad);*/

        #endregion
        TAsignacionPreguntaExaman Add(AsignacionPreguntaExamen entidad);
        TAsignacionPreguntaExaman Update(AsignacionPreguntaExamen entidad);
        IEnumerable<TAsignacionPreguntaExaman> Add(IEnumerable<AsignacionPreguntaExamen> listadoEntidad);
        IEnumerable<TAsignacionPreguntaExaman> Update(IEnumerable<AsignacionPreguntaExamen> listadoEntidad);
        int ObtenerCantidadPreguntaExamen(int idExamen);
        List<AsignacionPreguntaExamen> ObtenerAsignacionesPreguntaExamenbyId(int idPregunta);

        AsignacionPreguntaExamen ObtenerPorId(int idExamen);
        List<AsignacionPreguntaExamen> ObtenerPorIdPregunta(int idPregunta);

        AsignacionPreguntaExamen ObtenerExamenPregunta(Expression<Func<TAsignacionPreguntaExaman, bool>> filter);
    }
}
