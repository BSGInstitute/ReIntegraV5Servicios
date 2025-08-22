using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IProgramaGeneralCertificacionRespuestaService
    {
        #region Metodos Base
        ProgramaGeneralCertificacionRespuesta Add(ProgramaGeneralCertificacionRespuesta entidad);
        ProgramaGeneralCertificacionRespuesta Update(ProgramaGeneralCertificacionRespuesta entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralCertificacionRespuesta> Add(List<ProgramaGeneralCertificacionRespuesta> listadoEntidad);
        List<ProgramaGeneralCertificacionRespuesta> Update(List<ProgramaGeneralCertificacionRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralCertificacionRespuestaDTO> ObtenerProgramaGeneralCertificacionRespuesta();
        ProgramaGeneralCertificacionRespuesta ObtenerCertificacionRespuesta(int idOportunidad, int idCertificacion);
    }
}
