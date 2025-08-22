using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPaisRepository : IGenericRepository<TPai>
    {
        #region Metodos Base
        TPai Add(Pais entidad);
        TPai Update(Pais entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPai> Add(IEnumerable<Pais> listadoEntidad);
        IEnumerable<TPai> Update(IEnumerable<Pais> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Pais? ObtenerPorId(int id);
        IEnumerable<PaisDTO> ObtenerPais();
        Task<IEnumerable<PaisDTO>> ObtenerPaisAsync();
        IEnumerable<PaisComboDTO> ObtenerPaisCombo();
        IEnumerable<PaisZonaHorariaDTO> ObtenerPaisZonaHoraria();
        StringDTO ObtenerNombrePaisPorId(int idPais);
        IEnumerable<PaisMonedaComboDTO> ObtenerComboConMoneda();
        Task<IEnumerable<PaisMonedaComboDTO>> ObtenerComboConMonedaAsync();
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboConZonaHoraria();
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboZonaHorarioActivo();
        List<ComboDTO> ObtenerCombo();
        Task<List<ComboDTO>> ObtenerComboAsync();
        List<int> ObtenerTodoCodigoPais();
        IEnumerable<PlantillaPaisFiltroDTO> ObtenerPaisesPorIdPlantillaPw(int idPlantilla);
        IEnumerable<ComboDTO> ObtenerListaPais();
        Task<IEnumerable<ComboDTO>> ObtenerListaPaisAsync();
    }
}