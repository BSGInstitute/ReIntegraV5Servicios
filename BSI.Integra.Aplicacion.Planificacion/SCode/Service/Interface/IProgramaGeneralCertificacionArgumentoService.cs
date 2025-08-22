using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralCertificacionArgumentoService
    {
        #region Metodos Base
        ProgramaGeneralCertificacionArgumento Add(ProgramaGeneralCertificacionArgumento entidad);
        ProgramaGeneralCertificacionArgumento Update(ProgramaGeneralCertificacionArgumento entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralCertificacionArgumento> Add(List<ProgramaGeneralCertificacionArgumento> listadoEntidad);
        List<ProgramaGeneralCertificacionArgumento> Update(List<ProgramaGeneralCertificacionArgumento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProgramaGeneralCertificacionArgumentoDTO> ObtenerProgramaGeneralCertificacionArgumento();
        IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(int idCertificacion);
    }
}
