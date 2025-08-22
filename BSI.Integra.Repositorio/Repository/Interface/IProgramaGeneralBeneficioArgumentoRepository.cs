using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralBeneficioArgumentoRepository : IGenericRepository<TProgramaGeneralBeneficioArgumento>
    {
        #region Metodos Base
        TProgramaGeneralBeneficioArgumento Add(ProgramaGeneralBeneficioArgumento entidad);
        TProgramaGeneralBeneficioArgumento Update(ProgramaGeneralBeneficioArgumento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralBeneficioArgumento> Add(IEnumerable<ProgramaGeneralBeneficioArgumento> listadoEntidad);
        IEnumerable<TProgramaGeneralBeneficioArgumento> Update(IEnumerable<ProgramaGeneralBeneficioArgumento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralBeneficioArgumentoDTO> ObtenerProgramaGeneralBeneficioArgumento();
        IEnumerable<ProgramaGeneralBeneficioArgumentoComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralBeneficioArgumentoAgendaDTO> ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(int idBeneficio);
        ProgramaGeneralBeneficioArgumento? ObtenerPorId(int id);
    }
}