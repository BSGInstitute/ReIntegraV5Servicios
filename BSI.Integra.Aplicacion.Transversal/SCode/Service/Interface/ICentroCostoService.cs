using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICentroCostoService
    {
        IEnumerable<ComboDTO> ObtenerCombo();
        public Task<CentroCostoCombosPadreDTO> ObtenerCombosModulo();
        public Task<IEnumerable<ComboDTO>> ObtenerSubNivelCCPorAreaCCAsync(int idAreaCC);
        public Task<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>> ObtenerPGeneralPorIdSubAreaAsync(int idSubArea);
        public Task<IEnumerable<ComboDTO>> ObtenerSubAreaPorIdAreaAsync(int idArea);
        IEnumerable<ComboDTO> ObtenerRecientesAutocomplete(string nombreParcial);
        IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string valor);
        IEnumerable<ComboDTO> ObtenerAutocompleteV2(string valor, string usuario);
        IEnumerable<ComboDTO> ObtenerAutocompleteCentroCosto(string nombreParcial);
        IEnumerable<ComboDTO> ObtenerAutocompleteConPGeneral(string nombreParcial);
        PlantillaCentroCostoDTO ObtenerCentroCostoParaPEspecifico(int idCentroCosto);
        List<ComboDTO> ObtenerCentroCostoPorAsesores(List<int> listaAsesores);
        public List<CentroCostoDTO> Obtener();
        public List<CentroCostoUsuariosDTO> ObtenerCcDatosUsuarios();
        public CentroCostoMasAdicionalesDTO ObtenerMasAdicionales(int id);
        PlantillaCentroCostoDTO ObtenerCentroCostoParaPlantillaWhatsApp(int idCentroCosto);
        List<CentroCostoPadreCentroCostoIndividualDTO> ObtenerCentroCostoPadreCentroCostoIndividual();
        List<DatosCentroCostoDTO> ObtenerDatosCentroCostos(int idPEspecifico);
        bool ExistePorId(int idPlantilla);
        public List<CentroCostoPEspecificoDTO> ObtenerListaCentrosCostoPorNombre(string nombreCentroCosto);
        public object ObtenerDatosDelCentrodeCosto(int idPEspecifico);
        public CentroCostoDTO Insertar(CentroCostoDTO dto, string UserName);
        public CentroCostoDTO Actualizar(CentroCostoDTO dto, string UserName);
        public bool Eliminar(int idCentroCosto, string usuario);
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor);
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompleteInstituto(string valor);


    }
}
