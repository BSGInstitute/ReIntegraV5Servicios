using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICategoriaEvaluacionRepository : IGenericRepository<TEvaluacionCategorium>
    {
        #region Metodos Base
        TEvaluacionCategorium Add(CategoriaEvaluacion entidad);
        TEvaluacionCategorium Update(CategoriaEvaluacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEvaluacionCategorium> Add(IEnumerable<CategoriaEvaluacion> listadoEntidad);
        IEnumerable<TEvaluacionCategorium> Update(IEnumerable<CategoriaEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CategoriaEvaluacionDTO> Obtener();
        CategoriaEvaluacion? ObtenerPorId(int id);
    }
}
