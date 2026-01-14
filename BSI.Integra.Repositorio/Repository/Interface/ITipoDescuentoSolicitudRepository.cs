using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoDescuentoSolicitudRepository
    {
        void InsertarSolicitud(
            int idTipoDescuento,
            int idOportunidad,
            int idPersonalSolicitante,
            string? comentarioSolicitud,
            string? nombreArchivoSolicitud,
            string? contentTypeSolicitud,
            string usuario);

        IEnumerable<TipoDescuentoSolicitudListadoDTO> ObtenerTodasSolicitudes();

        void AprobarSolicitudCoordinador(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario);

        void RechazarSolicitudCoordinador(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario);

        void AprobarSolicitudGerencia(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario);

        void RechazarSolicitudGerencia(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario);

        TipoDescuentoSolicitudPaginadoDTO ListarSolicitudes(TipoDescuentoSolicitudFiltroDTO filtro);
    }
}
