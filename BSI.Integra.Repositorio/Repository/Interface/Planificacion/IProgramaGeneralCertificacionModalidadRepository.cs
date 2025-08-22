using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralCertificacionModalidadRepository : IGenericRepository<TProgramaGeneralCertificacionModalidad>
    {
        #region Metodos Base
        TProgramaGeneralCertificacionModalidad Add(ProgramaGeneralCertificacionModalidad entidad);
        TProgramaGeneralCertificacionModalidad Update(ProgramaGeneralCertificacionModalidad entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProgramaGeneralCertificacionModalidad> Add(IEnumerable<ProgramaGeneralCertificacionModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralCertificacionModalidad> Update(IEnumerable<ProgramaGeneralCertificacionModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralCertificacionModalidad? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralCertificacionModalidad> ObtenerPorIds(List<int> id);
    }
}
