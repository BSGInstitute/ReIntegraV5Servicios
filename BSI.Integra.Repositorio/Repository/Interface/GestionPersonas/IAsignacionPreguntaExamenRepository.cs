using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IAsignacionPreguntaExamenRepository : IGenericRepository<TCentil>
    {
        #region Metodos Base
        TCentil Add(AsignacionPreguntaExamen entidad);
        TCentil Update(AsignacionPreguntaExamen entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCentil> Add(IEnumerable<AsignacionPreguntaExamen> listadoEntidad);
        IEnumerable<TCentil> Update(IEnumerable<AsignacionPreguntaExamen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        int ObtenerCantidadPreguntaExamen(int idExamen);
    }
}
