using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IRegionCiudadService
    {

        RegionCiudad Add(RegionCiudad entidad);
        RegionCiudad Update(RegionCiudad entidad);
        bool Delete(int id, string usuario);

        List<RegionCiudad> Add(List<RegionCiudad> listadoEntidad);
        List<RegionCiudad> Update(List<RegionCiudad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);

        IEnumerable<ComboRegionCiudadDTO> ObtenerCombo();
        IEnumerable<RegionCiudadPanelDTO> ObtenerRegionCiudad();
        IEnumerable<RegionCiudadPanelDTO2> filtroPaisCiudad(int idPais, int idCiudad);
        List<RegionCiudadComboDTO> ObtenerPorEstado();
    }
}
