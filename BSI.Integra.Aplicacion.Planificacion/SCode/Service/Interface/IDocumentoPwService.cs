
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IDocumentoPwService
    {
        IEnumerable<DocumentoPwDTO> Obtener();
        DocumentoPw InsertarDocumento(CompuestoDocumentoDTO dto, string usuario);
        DocumentoPw ActualizarDocumento(CompuestoDocumentoPwDTO dto, string usuario);
        bool EliminarDocumento(int id, string usuario);
        IEnumerable<DocumentoPwVersionesDTO> ObtenerIntroduccionVersionDocumento(int idDocumentoPW);
        IEnumerable<ModalidadPortalDTO> ObtenerModalidadPortal();
        IEnumerable<ComboDTO> ObtenerModoFechaInicio();
        IEnumerable<ComboDTO> ObtenerNotasTipo();
        SeccionModalidadHorarioResponseDTO? ObtenerDocumentoPWModalidad(int idDocumentoPW);
    }
}
