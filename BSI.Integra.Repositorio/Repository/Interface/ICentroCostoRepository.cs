using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICentroCostoRepository : IGenericRepository<TCentroCosto>
    {
        #region Metodos Base
        TCentroCosto Add(CentroCosto entidad);
        TCentroCosto Update(CentroCosto entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCentroCosto> Add(IEnumerable<CentroCosto> listadoEntidad);
        IEnumerable<TCentroCosto> Update(IEnumerable<CentroCosto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();

        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ComboDTO> ObtenerRecientesAutocomplete(string nombreParcial);
        IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string nombreParcial);
        IEnumerable<ComboDTO> ObtenerAutocompletePorTipoProgramaCarrera(string valor, int? tipoProgramaCarrera);
        IEnumerable<ComboDTO> ObtenerAutocompletePorTipoProgramaCarreraV3(string valor, int? tipoProgramaCarrera);
        IEnumerable<ComboDTO> ObtenerAutocompleteCentroCosto(string nombreParcial);
        CentroCosto ObtenerPorId(int idCentroCosto);
        IEnumerable<ComboDTO> ObtenerAutocompleteConPGeneral(string nombreParcial);
        PlantillaCentroCostoDTO ObtenerCentroCostoParaPEspecifico(int idCentroCosto);
        List<ComboDTO> ObtenerCentroCostoPorAsesores(List<int> listaAsesores);
        PlantillaCentroCostoDTO ObtenerCentroCostoParaPlantillaWhatsApp(int idCentroCosto);
        List<CentroCostoPadreCentroCostoIndividualDTO> ObtenerCentroCostoPadreCentroCostoIndividual();
        List<DatosCentroCostoDTO> ObtenerDatosCentroCostos(int idPEspecifico);
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorCampania(string Nombre);
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorNombreIdConjuntoAnuncio(int IdConjuntoAnuncio, string NombreCampania);
        public PlantillaCentroCostoDTO ObtenerRemplazoPlantilla(int IdPgeneral);
        public List<CentroCostoPEspecificoDTO> ObtenerListaCentrosCostoPorNombre(string nombreCentroCosto);
        public List<FiltroDTO> ObtenerCentroCostoParaFiltro();
        IEnumerable<CentroCostoPEspecificoFiltroDTO> ObtenerFiltroPorTipo(bool aplicaTipo);
        Task<IEnumerable<CentroCostoPEspecificoFiltroDTO>> ObtenerFiltroPorTipoAsync(bool aplicaTipo);
        List<ComboDTO> ObtenerCentroCostoWebinar();
        IEnumerable<CentroCostoDTO> ObtenerCentroCostoParaPEspecifico(string codigo, string condicion, string anio, string nombreCiudad);
        int ObtenerUltimoIdCentroCosto();
        string? ObtenerUltimoCentroCostoPorCodigo(string codigo);
        public List<CentroCostoDTO> Obtener();
        public List<CentroCostoUsuariosDTO> ObtenerCcDatosUsuarios();
        public CentroCostoMasAdicionalesDTO ObtenerMasAdicionales(int id);
        public List<CentroCostoProgramaEspecificoFiltroDTO> ObtenerCentroCostoPadres(int? tipo);
        public CentroCostoPEspecificoDTO ObtenerCentrosCostoPorNombre(string nombreCentroCosto);

        IEnumerable<ComboDTO> ObtenerCentroCostoAgenda();
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompleteInstituto(string valor);
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor);

        int? ObtenerIdPorNombre(string nombre);


    }
}