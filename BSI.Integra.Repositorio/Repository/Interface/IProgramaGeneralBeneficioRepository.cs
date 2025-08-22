using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralBeneficioRepository : IGenericRepository<TProgramaGeneralBeneficio>
    {
        #region Metodos Base
        TProgramaGeneralBeneficio Add(ProgramaGeneralBeneficio entidad);
        TProgramaGeneralBeneficio Update(ProgramaGeneralBeneficio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralBeneficio> Add(IEnumerable<ProgramaGeneralBeneficio> listadoEntidad);
        IEnumerable<TProgramaGeneralBeneficio> Update(IEnumerable<ProgramaGeneralBeneficio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralBeneficioDTO> ObtenerProgramaGeneralBeneficio();
        IEnumerable<ProgramaGeneralBeneficioComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralBeneficioOportunidadDTO> ObtenerProgramaGeneralBeneficioPorIdOportunidad(int idOportunidad);
        ProgramaGeneralBeneficio? ObtenerPorId(int id);
        List<CompuestoBeneficioModalidadAlternoDTO> ObteneBeneficiosPorModalidades(int idPGeneral);
    }
}