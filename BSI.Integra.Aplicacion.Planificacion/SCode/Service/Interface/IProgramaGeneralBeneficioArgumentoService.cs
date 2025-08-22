using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralBeneficioArgumentoService
    {
        #region Metodos Base
        ProgramaGeneralBeneficioArgumento Add(ProgramaGeneralBeneficioArgumento entidad);
        ProgramaGeneralBeneficioArgumento Update(ProgramaGeneralBeneficioArgumento entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralBeneficioArgumento> Add(List<ProgramaGeneralBeneficioArgumento> listadoEntidad);
        List<ProgramaGeneralBeneficioArgumento> Update(List<ProgramaGeneralBeneficioArgumento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProgramaGeneralBeneficioArgumentoDTO> ObtenerProgramaGeneralBeneficioArgumento();
        IEnumerable<ProgramaGeneralBeneficioArgumentoComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralBeneficioArgumentoAgendaDTO> ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(int idBeneficio);
    }
}
