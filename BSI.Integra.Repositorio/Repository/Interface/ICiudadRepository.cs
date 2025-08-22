using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICiudadRepository : IGenericRepository<TCiudad>
    {
        #region Metodos Base
        TCiudad Add(Ciudad entidad);
        TCiudad Update(Ciudad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCiudad> Add(IEnumerable<Ciudad> listadoEntidad);
        IEnumerable<TCiudad> Update(IEnumerable<Ciudad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CiudadComboDTO> ObtenerCombo();
        IEnumerable<CiudadDTO> ObtenerCiudad();
        StringDTO ObtenerNombreCiudadPorId(int idCiudad);
        IEnumerable<RegionCiudadComboDTO> ObtenerComboRegionCiudad();
        IEnumerable<ComboDTO> ObtenerCiudadesDeSedesExistentes();
        IEnumerable<CiudadEnvioDTO> ObtenerNombreCiudadPorIdPais(int idPais);
        bool LongitudCelularPorPaisCorrecta(int? idPais, string numeroCelular);
        List<ComboDTO> ObtenerCiudadesPorPais(string idPais);
        IEnumerable<CiudadAlternoDTO> ObtenerTodoCiudades();
        Ciudad ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCiudadFiltro();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        List<Ciudad> ObtenerPorIds(string ids);
        List<FiltroDTO> ObtenerListaCiudadesBs();
        IEnumerable<CiudadAlternoDTO> Obtener();
        Task<IEnumerable<CiudadAlternoDTO>> ObtenerAsync();
        void insertarColonia(CiudadColoniaDTO dto);
        ComboDTO? ObtenerMunicipioById(int idMunicipioMexico);
        ComboDTO? ObtenerAsentammientoById(int idAsentamientoMexico);
        CodigoPostalAsentammiento? ObtenerCodigoPostalPorIdAsentamiento(int idAsentamientoMexico);
        ComboDTO? ObtenerCiudadMexicoById(int idCiudadMexico);
        List<ComboDTO> ObtenerCiudadMexicoByIdEstadoMexico(int idCiudadRef);
        #region
        List<CiudadDatosDTO> ObtenerCiudadesPorPaisByFichaDato();
        #endregion
    }
}

