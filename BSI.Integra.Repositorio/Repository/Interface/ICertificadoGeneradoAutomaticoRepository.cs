using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICertificadoGeneradoAutomaticoRepository : IGenericRepository<TCertificadoGeneradoAutomatico>
    {
        #region Metodos Base
        TCertificadoGeneradoAutomatico Add(CertificadoGeneradoAutomatico entidad);
        TCertificadoGeneradoAutomatico Update(CertificadoGeneradoAutomatico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCertificadoGeneradoAutomatico> Add(IEnumerable<CertificadoGeneradoAutomatico> listadoEntidad);
        IEnumerable<TCertificadoGeneradoAutomatico> Update(IEnumerable<CertificadoGeneradoAutomatico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        int ObtenerCorrelativoCertificado();
        void ActualizarNombreArchivo(string NombreArchivo, int IdCertificado);
    }
}
