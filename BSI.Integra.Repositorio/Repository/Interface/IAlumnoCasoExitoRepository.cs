using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAlumnoCasoExitoRepository
    {
        IEnumerable<AlumnoCasoExitoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        AlumnoCasoExitoDTO? ObtenerPorId(int id);
        int Insertar(AlumnoCasoExitoDTO dto, string usuario);
        bool Actualizar(AlumnoCasoExitoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        bool ActualizarVisibilidad(int id, bool estadoVisibilidad, string usuario);
        bool ActualizarPosiciones(string jsonPosiciones, string usuario);
    }
}
