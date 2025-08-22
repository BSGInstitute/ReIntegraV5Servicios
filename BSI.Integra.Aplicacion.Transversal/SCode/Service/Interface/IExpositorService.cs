using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IExpositorService
    {
        ExpositorDTO Actualizar(ExpositorDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        (string? UrlArchivo, string? NombreArchivo) RegistrarArchivoFotoExpositor(IFormFile files);
        ExpositorDTO Insertar(ExpositorDTO dto, string usuario);
        Task<ComboModuloExpositorDTO> ObtenerCombosModulo();
        IEnumerable<ExpositorDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
