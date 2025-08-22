using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IExamenFeedbackService
    {
        IEnumerable<ExamenFeedbackDTO> Obtener();
        ExamenFeedbackDTO Insertar(ExamenFeedbackDTO dto, string usuario);
        ExamenFeedbackDTO Actualizar(ExamenFeedbackDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
