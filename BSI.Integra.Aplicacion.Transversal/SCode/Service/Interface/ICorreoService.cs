using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICorreoService
    {
        bool EnvioEmailSinCopia(string email, string displayname, string subject, string mensaje);
        bool EnvioEmail(string email, string displayname, string subject, string mensaje, List<string> correos);
        string ObtenerMensajeCorreoProcesoPagoPeru(string email, string password, string url, AlumnoDTO contacto, List<MontoPagoCronogramaDetalleDTO> listaCronogama, string pgeneral, string codigoMatricula, string pais, string simboloMoneda);
        string ObtenerMensajeCorreoFinanzas(AlumnoDTO contacto, List<MontoPagoCronogramaDetalleDTO> listaCronogama, string codigoMatricula, string simboloMoneda, CentroCosto centroCosto);
        bool EnvioEmailBlobAdjunto(string email, string displayname, string subject, string mensaje, string nombreDocumentos, byte[] archivoBytes, List<string> correos);
        bool EsCorreoValido(string email);
        byte[] Descargar(int idCorreo, string nombreArchivo, int idAsesor, string folder);
    }
}
