using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface ICourierDetalleService
    {
        CourierDetalleDTO Actualizar(CourierDetalleDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        bool EliminarLista(List<int> ids, string usuario);
        CourierDetalleDTO Insertar(CourierDetalleDTO dto, string usuario);
        List<CourierDetalleDTO> ObtenerPorIdCourier(int idCourier);
        CourierDetalleDTO ObtenerPorId(int id);
        CourierDetalleDTO ObtenerCourierDetallePorIdCourierYIdCiudad(int idCourier, int idCiudad);

    }
}
