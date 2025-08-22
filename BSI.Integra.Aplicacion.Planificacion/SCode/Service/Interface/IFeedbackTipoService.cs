using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IFeedbackTipoService
    {
        FeedbackTipoDTO Insertar(FeedbackTipoDTO dto, string usuario);
        FeedbackTipoDTO Actualizar(FeedbackTipoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        List<FeedbackTipoDTO> Obtener();
    }
}
