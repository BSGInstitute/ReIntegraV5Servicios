using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface ICourierService
    {
        CourierDTO Actualizar(CourierDTO dto, string usuario);
        List<CourierDTO> ActualizarLista(List<CourierDTO> dtos, string usuario);
        bool Eliminar(int id, string usuario);
        bool EliminarLista(List<int> ids, string usuario);
        CourierDTO Insertar(CourierDTO dto, string usuario);
        List<CourierDTO> InsertarLista(List<CourierDTO> dtos, string usuario);
        List<CourierDTO> Obtener();
        CourierDTO ObtenerPorId(int id);
    }
}
