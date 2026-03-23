using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPartnerPwService
    {
        IEnumerable<PartnerPwDTO> Obtener();
        (IEnumerable<PartnerBeneficioPwDTO> Beneficios, IEnumerable<PartnerContactoPwDTO> Contactos) ObtenerBeneficioContactoPorId(int idPartner);
        PartnerPwDTO Insertar(PartnerPwDTO dto, string usuario);
        PartnerPwDTO Actualizar(PartnerPwDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        IEnumerable<ComboDTO> ObtenerCombo();

        // Métodos para manejo de archivos en Blob Storage
        string SubirCertificadoLogo(IFormFile archivo, string nombreArchivo);
        string SubirCertificadoBSG(IFormFile archivo, string nombreArchivo);
        string GenerarNombreArchivo(string nombreOriginal);
        string ObtenerUrlCertificadoLogo(string? nombreArchivo);
        string ObtenerUrlCertificadoBSG(string? nombreArchivo);
    }
}
