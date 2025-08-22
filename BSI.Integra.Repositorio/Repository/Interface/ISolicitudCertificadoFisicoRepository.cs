using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudCertificadoFisicoRepository : IGenericRepository<TSolicitudCertificadoFisico>
    {
        #region Metodos Base
        TSolicitudCertificadoFisico Add(SolicitudCertificadoFisico entidad);
        TSolicitudCertificadoFisico Update(SolicitudCertificadoFisico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSolicitudCertificadoFisico> Add(IEnumerable<SolicitudCertificadoFisico> listadoEntidad);
        IEnumerable<TSolicitudCertificadoFisico> Update(IEnumerable<SolicitudCertificadoFisico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        string ObtenerCourierPorNombre(int idSolicitudCertificado);
        SolicitudCertificadoFisico ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera);
        List<DatosReporteEnvioCertificadoFisicoDTO> DatosReporteCertificadoEnvioFisicoPorId(string codigoMatricula); //bingo

        List<DatosEnvioAlumnoDTO> obtenerDatosEnvio(int idmatricula);
        SolicitudCertificadoFisico obtenerSolicitudCertificado(int IdMatriculaCabecera, int IdCertificadoGeneradoAutomatico);
        List<DataSolicitudCertificadoFisicoDTO> ObtenerSolicitudesCertificadoFisico(filtroSolicitudCertificadoFisicoDTO json);
        DatosEnvioAlumnoDTO InsertarDatosEnviosOperaciones(DatosEnvioAlumnoDTO filtro);
        DatosRegistroEnvioFisicoDTO DatosRegistroEnvioFisico(int IdSolicitudCertificadoFisico);

    }
}
