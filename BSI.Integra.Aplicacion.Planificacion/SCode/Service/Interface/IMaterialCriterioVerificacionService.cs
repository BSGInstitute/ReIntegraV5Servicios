using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IMaterialCriterioVerificacionService
    {
        MaterialCriterioVerificacionDTO Actualizar(MaterialCriterioVerificacionDTO dto, string usuario);
        List<MaterialCriterioVerificacionDTO> ActualizarLista(List<MaterialCriterioVerificacionDTO> dtos, string usuario);
        bool Eliminar(int id, string usuario);
        bool EliminarLista(List<int> ids, string usuario);
        MaterialCriterioVerificacionDTO Insertar(MaterialCriterioVerificacionDTO dto, string usuario);
        List<MaterialCriterioVerificacionDTO> InsertarLista(List<MaterialCriterioVerificacionDTO> dtos, string usuario);
        List<MaterialCriterioVerificacionDTO> Obtener();
        MaterialCriterioVerificacionDTO ObtenerPorId(int id);
    }
}
