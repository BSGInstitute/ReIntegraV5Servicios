using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ICabeceraConfiguracionLlamadaAutomaticaService
    {
        #region Metodos Base
        CabeceraConfiguracionLlamadaAutomatica Add(CabeceraConfiguracionLlamadaAutomaticaDTO data, string Usuario);
        CabeceraConfiguracionLlamadaAutomatica Update(CabeceraConfiguracionLlamadaAutomaticaDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<CabeceraConfiguracionLlamadaAutomatica> Add(List<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad);
        List<CabeceraConfiguracionLlamadaAutomatica> Update(List<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<LlamadaAutomaticaConfiguracionDTO> ObtenerCabeceraConfiguracionLlamadaAutomatica();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
