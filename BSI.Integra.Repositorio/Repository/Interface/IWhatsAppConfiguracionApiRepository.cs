using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppConfiguracionApiRepository : IGenericRepository<TWhatsAppConfiguracionApi>
    {
        #region Metodos Base
        TWhatsAppConfiguracionApi Add(WhatsAppConfiguracionApi entidad);
        TWhatsAppConfiguracionApi Update(WhatsAppConfiguracionApi entidad);
        bool Delete(int id, string usuario);
        #endregion
        List<WhatsAppConfiguracionApiListaGrillaDTO> ObtenerCredencialesUsuarios();
        WhatsAppConfiguracionApi ObtenerPorId(int id);
        WhatsAppConfiguracionApiNumeroIdentificadorDto ObtenerNumeroIdentificadorWhatsAppPorID(int id, int idCodigoPais);
    }
}