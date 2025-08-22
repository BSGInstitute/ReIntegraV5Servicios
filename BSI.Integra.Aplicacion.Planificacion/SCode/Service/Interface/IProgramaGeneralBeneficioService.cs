using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralBeneficioService
    {
        #region Metodos Base
        ProgramaGeneralBeneficio Add(ProgramaGeneralBeneficio entidad);
        ProgramaGeneralBeneficio Update(ProgramaGeneralBeneficio entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralBeneficio> Add(List<ProgramaGeneralBeneficio> listadoEntidad);
        List<ProgramaGeneralBeneficio> Update(List<ProgramaGeneralBeneficio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProgramaGeneralBeneficioDTO> ObtenerProgramaGeneralBeneficio();
        IEnumerable<ProgramaGeneralBeneficioComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralBeneficioOportunidadDTO> ObtenerProgramaGeneralBeneficioPorIdOportunidad(int idOportunidad);
    }
}
