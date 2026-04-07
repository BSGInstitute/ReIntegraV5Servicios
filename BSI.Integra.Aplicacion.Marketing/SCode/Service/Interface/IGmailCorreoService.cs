using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IGmailCorreoService
    {
        #region Metodos Base
        GmailCorreo Add(GmailCorreo entidad);
        GmailCorreo Update(GmailCorreo entidad);
        bool Delete(int id, string usuario);

        List<GmailCorreo> Add(List<GmailCorreo> listadoEntidad);
        List<GmailCorreo> Update(List<GmailCorreo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GmailCorreoDTO> ObtenerGmailCorreo();
        IEnumerable<GmailCorreoComboDTO> ObtenerCombo();
        BandejaCorreoEnviadoPorPersonalDTO ObtenerCorreosEnviadosPorFiltroBandeja(FiltroBandejaCorreoDTO filtroBandejaCorreo);
        string SubirArchivo(byte[] archivo, string tipo, string nombreArchivo);
        Task<bool> EnviarMensajeCorreo(ParametrosEnviarMensajeDTO informacionCorreo, IList<IFormFile> Files, string usuario);
        CorreoBodyDTO ObtenerCorreoEnviadoPorId(int idGmailCorreo, string Usuario);
        GmailCorreo ObtenerCorreoPorId(int idCorreo);
        List<CorreoAlumnoVentasDTO> ObtenerCorreosAlumnosSoloVentas(string emailAlumno);
        Task<bool> EnviarMensajeCorreoPla(ParametrosEnviarMensajePlaDTO informacionCorreo, IList<IFormFile> Files, string usuario);
        PreviewMensajePlaResponseDTO PreviewMensajePla(PreviewMensajePlaRequestDTO request);
    }
}
