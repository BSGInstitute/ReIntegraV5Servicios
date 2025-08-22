using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IWhatsAppPlantillaPorOcurrenciaActividadService
    {
        #region Metodos Base
        WhatsAppPlantillaPorOcurrenciaActividad Add(WhatsAppPlantillaPorOcurrenciaActividad entidad);
        WhatsAppPlantillaPorOcurrenciaActividad Update(WhatsAppPlantillaPorOcurrenciaActividad entidad);
        bool Delete(int id, string usuario);

        List<WhatsAppPlantillaPorOcurrenciaActividad> Add(List<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad);
        List<WhatsAppPlantillaPorOcurrenciaActividad> Update(List<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> ObtenerWhatsAppPlantillaPorOcurrenciaActividad();
        IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> ObtenerCombo();
        List<WhatsAppPlantillaPorOcurrenciaActividadDTO> ObtenerPorIdOcurrenciaActividad(int idActividadOcurrencia);
    }
}
