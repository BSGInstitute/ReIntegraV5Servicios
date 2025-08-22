using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppConfiguracionRepository : IGenericRepository<TWhatsAppConfiguracion>
    {
        WhatsAppHostDatosDTO ObtenerCredencialHost(int idPais);
        List<WhatsAppHostDatosDTO> ObtenerCredencialHostGeneral();
        List<HoraDTO> ObtenerConfiguracionDeHorariosDeEnvioParaCombo(int intervalo);
    }
}
