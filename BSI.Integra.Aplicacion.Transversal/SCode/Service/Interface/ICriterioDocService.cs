using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICriterioDocService
    {
        #region Metodos Base
        CriterioDoc Add(CriterioDoc entidad);
        CriterioDoc Update(CriterioDoc entidad);
        bool Delete(int id, string usuario);

        List<CriterioDoc> Add(List<CriterioDoc> listadoEntidad);
        List<CriterioDoc> Update(List<CriterioDoc> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion



        List<DTO.ComboDTO> ObtenerTodoSeleccionar();
        List<DTO.ComboDTO> ObtenerCriterioModalidad(List<int> idModalidades);
        }
}
