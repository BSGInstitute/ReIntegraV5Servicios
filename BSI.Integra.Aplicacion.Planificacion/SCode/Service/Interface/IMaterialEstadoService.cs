using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IMaterialEstadoService
    {
        MaterialEstadoDTO Actualizar(MaterialEstadoDTO dto, string usuario);
        List<MaterialEstadoDTO> ActualizarLista(List<MaterialEstadoDTO> dtos, string usuario);
        bool Eliminar(int id, string usuario);
        bool EliminarLista(List<int> ids, string usuario);
        MaterialEstadoDTO Insertar(MaterialEstadoDTO dto, string usuario);
        List<MaterialEstadoDTO> InsertarLista(List<MaterialEstadoDTO> dtos, string usuario);
        List<MaterialEstadoDTO> Obtener();
        MaterialEstadoDTO ObtenerPorId(int id);
    }
}
