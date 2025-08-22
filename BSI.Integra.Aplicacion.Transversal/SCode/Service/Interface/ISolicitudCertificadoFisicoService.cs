using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISolicitudCertificadoFisicoService
    {
        #region Metodos Base
        SolicitudCertificadoFisico Add(SolicitudCertificadoFisico entidad);
        SolicitudCertificadoFisico Update(SolicitudCertificadoFisico entidad);
        bool Delete(int id, string usuario);

        List<SolicitudCertificadoFisico> Add(List<SolicitudCertificadoFisico> listadoEntidad);
        List<SolicitudCertificadoFisico> Update(List<SolicitudCertificadoFisico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        string ObtenerCourierPorNombre(int idSolicitudCertificado);
        SolicitudCertificadoFisico ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera);
        List<DatosReporteEnvioCertificadoFisicoDTO> DatosReporteCertificadoEnvioFisicoPorId(string codigoMatricula);
        List<DataSolicitudCertificadoFisicoDTO> ObtenerSolicitudesCertificadoFisico(filtroSolicitudCertificadoFisicoDTO json);
        SolicitudCertificadoFisico ObtenerPorId(int id);
        DatosRegistroEnvioFisicoDTO DatosRegistroEnvioFisico(int IdSolicitudCertificadoFisico);

    }
}
