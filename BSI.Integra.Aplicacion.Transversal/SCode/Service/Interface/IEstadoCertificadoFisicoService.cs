using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstadoCertificadoFisicoService
    {
        List<EstadoCertificadoFisicoDTO> ObtenerEstadParaFiltro();
    }
}
