using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProbabilidadRegistroPwRepository : IGenericRepository<TProbabilidadRegistroPw>
    {
        #region Metodos Base
        TProbabilidadRegistroPw Add(ProbabilidadRegistroPw entidad);
        TProbabilidadRegistroPw Update(ProbabilidadRegistroPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProbabilidadRegistroPw> Add(IEnumerable<ProbabilidadRegistroPw> listadoEntidad);
        IEnumerable<TProbabilidadRegistroPw> Update(IEnumerable<ProbabilidadRegistroPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ProbabilidadRegistroPwDTO> ObtenerProbabilidadRegistroPw();
        public IEnumerable<ComboDTO> ObtenerTodoFiltro();
        ProbabilidadRegistroPwDTO ObtenerPorId(int id);
    }
}
