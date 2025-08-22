using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IEscalaCalificacionService
    {
        EscalaCalificacionDTO Actualizar(EscalaCalificacionDTO dto, string usuario);
        List<EscalaCalificacionDTO> ActualizarLista(List<EscalaCalificacionDTO> dtos, string usuario);
        bool Eliminar(int id, string usuario);
        bool EliminarLista(List<int> ids, string usuario);
        EscalaCalificacionDTO Insertar(EscalaCalificacionDTO dto, string usuario);
        List<EscalaCalificacionDTO> InsertarLista(List<EscalaCalificacionDTO> dtos, string usuario);
        IEnumerable<EscalaCalificacionDTO> Obtener();
        EscalaCalificacionDTO ObtenerPorId(int id);
    }
}
