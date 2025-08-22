using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;

namespace BSI.Integra.Repositorio.Repository.Interface.Configuracion
{
    public interface IMunicipioRepository
    {
        List<ComboDTO> ObtenerMunicipioPorCiudad(int idCiudadRef);
        List<ComboDTO> ObtenerMunicipioPorEstadoyCiudad(int idCiudadRef, int? idCiudadMexico);
    }
}
