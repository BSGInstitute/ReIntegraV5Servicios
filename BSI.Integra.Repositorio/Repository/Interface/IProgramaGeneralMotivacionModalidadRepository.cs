using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralMotivacionModalidadRepository : IGenericRepository<TProgramaGeneralMotivacionModalidad>
    {
        #region Metodos Base
        TProgramaGeneralMotivacionModalidad Add(ProgramaGeneralMotivacionModalidad entidad);
        TProgramaGeneralMotivacionModalidad Update(ProgramaGeneralMotivacionModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralMotivacionModalidad> Add(IEnumerable<ProgramaGeneralMotivacionModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralMotivacionModalidad> Update(IEnumerable<ProgramaGeneralMotivacionModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralMotivacionModalidad? ObtenerPorId(int id);
    }
}