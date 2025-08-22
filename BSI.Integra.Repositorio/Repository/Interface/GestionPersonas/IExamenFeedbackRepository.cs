using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IExamenFeedbackRepository : IGenericRepository<TExamenFeedback>
    {
        #region Metodos Base
        TExamenFeedback Add(ExamenFeedback entidad);
        TExamenFeedback Update(ExamenFeedback entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TExamenFeedback> Add(IEnumerable<ExamenFeedback> listadoEntidad);
        IEnumerable<TExamenFeedback> Update(IEnumerable<ExamenFeedback> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ExamenFeedbackDTO> Obtener();
        ExamenFeedback? ObtenerPorId(int id);
    }
}
