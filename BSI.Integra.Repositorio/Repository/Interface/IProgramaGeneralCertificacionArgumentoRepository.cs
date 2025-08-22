using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralCertificacionArgumentoRepository : IGenericRepository<TProgramaGeneralCertificacionArgumento>
    {
        #region Metodos Base
        TProgramaGeneralCertificacionArgumento Add(ProgramaGeneralCertificacionArgumento entidad);
        TProgramaGeneralCertificacionArgumento Update(ProgramaGeneralCertificacionArgumento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralCertificacionArgumento> Add(IEnumerable<ProgramaGeneralCertificacionArgumento> listadoEntidad);
        IEnumerable<TProgramaGeneralCertificacionArgumento> Update(IEnumerable<ProgramaGeneralCertificacionArgumento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralCertificacionArgumentoDTO> ObtenerProgramaGeneralCertificacionArgumento();
        IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(int idCertificacion);
        ProgramaGeneralCertificacionArgumento? ObtenerPorId(int id);
    }
}