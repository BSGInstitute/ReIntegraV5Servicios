using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICiudadService
    {

        #region Metodos Base
        Ciudad Insertar(CiudadEnvioDTO entidad, string usuario);
        Ciudad Actualizar(CiudadEnvioDTO entidad, string usuario);
        bool ActualizarCiudadMultiple(CiudadMultipleDTO entidades, string usuario);
        bool Eliminar(int id, string usuario);

        List<Ciudad> Insertar(List<Ciudad> listadoEntidad);
        List<Ciudad> Actualizar(List<Ciudad> listadoEntidad);
        bool Eliminar(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CiudadComboDTO> ObtenerCombo();
        IEnumerable<CiudadDTO> ObtenerCiudad();
        StringDTO ObtenerNombreCiudadPorId(int idCiudad);
        IEnumerable<CiudadEnvioDTO> ObtenerNombreCiudadPorIdPais(int idPais);
        IEnumerable<RegionCiudadComboDTO> ObtenerComboRegionCiudad();
        IEnumerable<DTO.ComboDTO> ObtenerCiudadesDeSedesExistentes();
        List<DTO.ComboDTO> ObtenerCiudadesPorPais(string idPais);
        IEnumerable<CiudadAlternoDTO> ObtenerTodoCiudades();
        List<ComboDTO> ObtenerMunicipioPorCiudad(int idCiudadRef);
        List<ComboDTO> ObtenerMunicipioPorEstadoyCiudad(int idCiudadRef, int? idCiudadMexico);
        List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipio(int idCiudadRef, int idMunicipioMexico);
        List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipioyCiudadMexico(int idCiudadRef, int idMunicipioMexico, int? idCiudadMexico);
        List<DatosCodigoPostalDTO> BusquedaPorCodigoPostal(string codigoPostal);
        List<ComboDTO> ObtenerCiudadMexicoByIdEstadoMexico(int idCiudadRef);
    }
}
