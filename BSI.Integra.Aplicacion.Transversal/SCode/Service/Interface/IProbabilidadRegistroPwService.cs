using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProbabilidadRegistroPwService
    {
        #region Metodos Base
        ProbabilidadRegistroPw Add(ProbabilidadRegistroPw entidad);
        ProbabilidadRegistroPw Update(ProbabilidadRegistroPw entidad);
        bool Delete(int id, string usuario);

        List<ProbabilidadRegistroPw> Add(List<ProbabilidadRegistroPw> listadoEntidad);
        List<ProbabilidadRegistroPw> Update(List<ProbabilidadRegistroPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProbabilidadRegistroPwDTO> ObtenerProbabilidadRegistroPw();
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        public IEnumerable<ComboDTO> ObtenerTodoFiltro();
        ProbabilidadRegistroPwDTO ObtenerPorId(int id);
    }
}
