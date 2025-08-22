using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRegionCiudadRepository : IGenericRepository<TRegionCiudad>

    {
        IEnumerable<TRegionCiudad> Add(IEnumerable<RegionCiudad> listadoEntidad);
        IEnumerable<TRegionCiudad> Update(IEnumerable<RegionCiudad> listadoEntidad);
        TRegionCiudad Add(RegionCiudad entidad);
        TRegionCiudad Update(RegionCiudad entidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        IEnumerable<ComboRegionCiudadDTO> ObtenerComboCiudad();
        IEnumerable<ComboDTO> ObtenerCiudadBs();
        Task<IEnumerable<ComboDTO>> ObtenerCiudadBsAsync();
        IEnumerable<RegionCiudadPanelDTO> ObtenerRegionCiudad();
        IEnumerable<RegionCiudadPanelDTO2> filtroPaisCiudad(int idPais, int idCiudad);
        IEnumerable<RegionCiudadComboDTO> ObtenerPorEstado();
        IEnumerable<ComboDTO> ObtenerCiudadBsCombo();


    }
}
