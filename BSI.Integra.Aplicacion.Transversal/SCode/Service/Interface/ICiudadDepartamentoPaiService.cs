using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ICiudadDepartamentoPaiService
    {
        IEnumerable<CiudadDepartamentoPai> ObtenerPorId(int idDepartamentoPais);
        IEnumerable<ComboDTO> ObtenerCombo();
        CodigoCiudadDTO ObtenerCodigoPorId(int id);
    }
}
