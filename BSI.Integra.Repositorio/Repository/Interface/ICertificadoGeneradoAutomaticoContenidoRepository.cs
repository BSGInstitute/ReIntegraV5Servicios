using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICertificadoGeneradoAutomaticoContenidoRepository : IGenericRepository<TCertificadoGeneradoAutomaticoContenido>
    {
        #region Metodos Base
        TCertificadoGeneradoAutomaticoContenido Add(CertificadoGeneradoAutomaticoContenido entidad);
        TCertificadoGeneradoAutomaticoContenido Update(CertificadoGeneradoAutomaticoContenido entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCertificadoGeneradoAutomaticoContenido> Add(IEnumerable<CertificadoGeneradoAutomaticoContenido> listadoEntidad);
        IEnumerable<TCertificadoGeneradoAutomaticoContenido> Update(IEnumerable<CertificadoGeneradoAutomaticoContenido> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ContenidoCertificadoSinFondoDTO> ObtenerDatosParaCertificadoFisico(int IdCertificadoGeneradoAutomatico);
    }
}
