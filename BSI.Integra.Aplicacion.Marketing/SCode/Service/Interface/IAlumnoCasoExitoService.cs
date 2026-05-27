using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IAlumnoCasoExitoService
    {
        IEnumerable<AlumnoCasoExitoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        AlumnoCasoExitoDTO? ObtenerPorId(int id);
        AlumnoCasoExitoDTO Insertar(AlumnoCasoExitoEntradaDTO dto, string usuario);
        AlumnoCasoExitoDTO Actualizar(AlumnoCasoExitoEntradaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        bool ActualizarVisibilidad(int id, bool estadoVisibilidad, string usuario);
        bool ActualizarPosiciones(List<AlumnoCasoExitoPosicionDTO> posiciones, string usuario);
        string SubirFotoPerfil(IFormFile archivo, string nombreArchivo);
        string GenerarNombreArchivo(string nombreOriginal);
        string ObtenerUrlFotoPerfil(string? nombreArchivo);
    }
}
