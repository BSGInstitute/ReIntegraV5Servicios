using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralBeneficioModalidadRepository : IGenericRepository<TProgramaGeneralBeneficioModalidad>
    {
        #region Metodos Base
        TProgramaGeneralBeneficioModalidad Add(ProgramaGeneralBeneficioModalidad entidad);
        TProgramaGeneralBeneficioModalidad Update(ProgramaGeneralBeneficioModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralBeneficioModalidad> Add(IEnumerable<ProgramaGeneralBeneficioModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralBeneficioModalidad> Update(IEnumerable<ProgramaGeneralBeneficioModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralBeneficioModalidad? ObtenerPorId(int id);
    }
}