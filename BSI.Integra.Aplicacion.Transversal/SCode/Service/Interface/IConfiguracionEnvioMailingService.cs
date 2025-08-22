using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionEnvioMailingService
    {
        List<CorreoDTO> ObtenerEnviosMasivos(string email);
        CorreoDTO ObtenerEnvioMasivo(int id);
    }
}
