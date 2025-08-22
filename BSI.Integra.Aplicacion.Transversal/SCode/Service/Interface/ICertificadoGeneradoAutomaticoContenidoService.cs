using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICertificadoGeneradoAutomaticoContenidoService
    {
        #region Metodos Base
        CertificadoGeneradoAutomaticoContenido Add(CertificadoGeneradoAutomaticoContenido entidad);
        CertificadoGeneradoAutomaticoContenido Update(CertificadoGeneradoAutomaticoContenido entidad);
        bool Delete(int id, string usuario);

        List<CertificadoGeneradoAutomaticoContenido> Add(List<CertificadoGeneradoAutomaticoContenido> listadoEntidad);
        List<CertificadoGeneradoAutomaticoContenido> Update(List<CertificadoGeneradoAutomaticoContenido> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        List<ContenidoCertificadoSinFondoDTO> ObtenerDatosParaCertificadoFisico(int IdCertificadoGeneradoAutomatico);

    }
}
