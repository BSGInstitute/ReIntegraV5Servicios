using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IContratoEstadoService
    {
        IEnumerable<ContratoEstadoDTO> Obtener();
        ContratoEstadoDTO Insertar(ContratoEstadoDTO dto, string usuario);
        ContratoEstadoDTO Actualizar(ContratoEstadoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
