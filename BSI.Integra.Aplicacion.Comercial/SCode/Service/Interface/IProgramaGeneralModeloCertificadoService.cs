using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IProgramaGeneralModeloCertificadoService
    {
        List<PGeneralModeloCertificadoDTO> ObtenerModeloCertificadoPrograma(int idOportunidad);
    }
}
