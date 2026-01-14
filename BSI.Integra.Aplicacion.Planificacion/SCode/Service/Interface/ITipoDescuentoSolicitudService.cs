using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ITipoDescuentoSolicitudService
    {
        void InsertarSolicitud(TipoDescuentoSolicitudEntradaDTO solicitud);
        IEnumerable<TipoDescuentoSolicitudListadoDTO> ObtenerTodasSolicitudes();
        void AprobarSolicitudCoordinador(TipoDescuentoSolicitudRespuestaEntradaDTO dto);
        void RechazarSolicitudCoordinador(TipoDescuentoSolicitudRespuestaEntradaDTO dto);
        void AprobarSolicitudGerencia(TipoDescuentoSolicitudRespuestaEntradaDTO dto);
        void RechazarSolicitudGerencia(TipoDescuentoSolicitudRespuestaEntradaDTO dto);
        TipoDescuentoSolicitudPaginadoDTO ListarSolicitudes(TipoDescuentoSolicitudFiltroDTO filtro);
        string SlugNombreArchivo(string textoOriginal);
        byte[] ConvertToByte(IFormFile file);
        string SubirArchivoSolicitudTipoDescuentoRepositorio(IFormFile archivoEntrada, string tipo, string nombreArchivo);
    }
}
