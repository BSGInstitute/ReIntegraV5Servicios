using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICertificadoGeneradoAutomaticoService
    {
        #region Metodos Base
        CertificadoGeneradoAutomatico Add(CertificadoGeneradoAutomatico entidad);
        CertificadoGeneradoAutomatico Update(CertificadoGeneradoAutomatico entidad);
        bool Delete(int id, string usuario);
        List<CertificadoGeneradoAutomatico> Add(List<CertificadoGeneradoAutomatico> listadoEntidad);
        List<CertificadoGeneradoAutomatico> Update(List<CertificadoGeneradoAutomatico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        int ObtenerCorrelativoCertificado();
    }
}
