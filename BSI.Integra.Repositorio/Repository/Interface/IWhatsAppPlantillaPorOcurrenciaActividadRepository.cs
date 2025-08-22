using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppPlantillaPorOcurrenciaActividadRepository : IGenericRepository<TWhatsAppPlantillaPorOcurrenciaActividad>
    {
        #region Metodos Base
        TWhatsAppPlantillaPorOcurrenciaActividad Add(WhatsAppPlantillaPorOcurrenciaActividad entidad);
        TWhatsAppPlantillaPorOcurrenciaActividad Update(WhatsAppPlantillaPorOcurrenciaActividad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppPlantillaPorOcurrenciaActividad> Add(IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad);
        IEnumerable<TWhatsAppPlantillaPorOcurrenciaActividad> Update(IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> ObtenerWhatsAppPlantillaPorOcurrenciaActividad();
        IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> ObtenerCombo();
        List<WhatsAppPlantillaPorOcurrenciaActividadDTO> ObtenerPorIdOcurrenciaActividad(int idActividadOcurrencia);
    }
}