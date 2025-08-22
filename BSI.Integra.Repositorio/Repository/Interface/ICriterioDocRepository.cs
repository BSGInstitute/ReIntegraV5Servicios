using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICriterioDocRepository : IGenericRepository<TCriterioDoc>
    {
        #region Metodos Base
        TCriterioDoc Add(CriterioDoc entidad);
        TCriterioDoc Update(CriterioDoc entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCriterioDoc> Add(IEnumerable<CriterioDoc> listadoEntidad);
        IEnumerable<TCriterioDoc> Update(IEnumerable<CriterioDoc> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
     
        List<ComboDTO> ObtenerTodoSeleccionar();
        List<ComboDTO> ObtenerCriterioModalidad(List<int> idModalidades);

    }
}
