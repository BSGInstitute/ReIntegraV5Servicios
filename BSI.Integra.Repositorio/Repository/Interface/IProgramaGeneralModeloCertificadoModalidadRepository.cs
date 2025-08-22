using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralModeloCertificadoModalidadRepository : IGenericRepository<TProgramaGeneralModeloCertificadoModalidad>
    {
        #region Metodos Base
        TProgramaGeneralModeloCertificadoModalidad Add(ProgramaGeneralModeloCertificadoModalidad entidad);
        TProgramaGeneralModeloCertificadoModalidad Update(ProgramaGeneralModeloCertificadoModalidad entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProgramaGeneralModeloCertificadoModalidad> Add(IEnumerable<ProgramaGeneralModeloCertificadoModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralModeloCertificadoModalidad> Update(IEnumerable<ProgramaGeneralModeloCertificadoModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        ProgramaGeneralModeloCertificadoModalidad? ObtenerPorId(int id);
        ProgramaGeneralModeloCertificadoModalidad? ObtenerPorIdModeloCertificadoIdModalidadCurso(int idModeloCertificado, int idModalidadCurso);
    }
}
