
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IDocumentoPwService
    {
        IEnumerable<DocumentoPwDTO> Obtener();
        Task<DocumentoPw> InsertarDocumento(CompuestoDocumentoDTO dto, IFormFile? archivoInstruccion, IFormFile? archivoCalificacion, string usuario);
        Task<DocumentoPw> ActualizarDocumento(CompuestoDocumentoPwDTO dto, IFormFile? archivoInstruccion, IFormFile? archivoCalificacion, string usuario);
        bool EliminarDocumento(int id, string usuario);
        IEnumerable<DocumentoPwVersionesDTO> ObtenerIntroduccionVersionDocumento(int idDocumentoPW);
        IEnumerable<ModalidadPortalDTO> ObtenerModalidadPortal();
        IEnumerable<ComboDTO> ObtenerModoFechaInicio();
        IEnumerable<ComboDTO> ObtenerNotasTipo();
        SeccionModalidadHorarioResponseDTO? ObtenerDocumentoPWModalidad(int idDocumentoPW);
        SeccionDuracionDTO? ObtenerDocumentoPWDuracion(int idDocumentoPW);
        SeccionFechaInicioDTO ObtenerDocumentoPWFechaInicio(int idDocumentoPw);
        SeccionNotasDTO ObtenerDocumentoPWNotas(int idDocumentoPW);
        void ActualizarSeccionDuracion(SeccionDuracionDTO? dto, int idDocumentoPw, string usuario);
        Task<string> SubirArchivoDocumentoPw(int id, IFormFile archivo, string campo, string usuario);
    }
}
