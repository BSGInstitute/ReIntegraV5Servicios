using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IMaterialAccionService
    {
        MaterialAccionDTO Actualizar(MaterialAccionDTO dto, string usuario);
        List<MaterialAccionDTO> ActualizarLista(List<MaterialAccionDTO> dtos, string usuario);
        bool Eliminar(int id, string usuario);
        bool EliminarLista(List<int> ids, string usuario);
        MaterialAccionDTO Insertar(MaterialAccionDTO dto, string usuario);
        List<MaterialAccionDTO> InsertarLista(List<MaterialAccionDTO> dtos, string usuario);
        List<MaterialAccionDTO> Obtener();
        MaterialAccionDTO ObtenerPorId(int id);
    }
}
