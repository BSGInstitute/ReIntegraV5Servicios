using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFiltroBandejaCorreoService
    {
        bool envioEmailAdjunto(string Correo, string Clave, TMKMailDataDTO mailData, IList<IFormFile> Files);
        BandejaCorreoDTO ObtenerBandejaEntradaMailInboxSpeech(FiltroBandejaCorreoDTO filtroBandejaCorreoDTO);
        ListaCorreosGrupoDTO ObtenerCorreosGrupos(int idCentroCosto, int idPaquete, List<int> estado, List<int> subEstado);
    }
}
