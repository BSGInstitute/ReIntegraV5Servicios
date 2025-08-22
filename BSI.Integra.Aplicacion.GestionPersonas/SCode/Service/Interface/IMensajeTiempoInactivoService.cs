using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IMensajeTiempoInactivoService
    {
        IEnumerable<MensajeTiempoInactivoDTO> Obtener();
        MensajeTiempoInactivoDTO Insertar(MensajeTiempoInactivoDTO dto, string usuario);
        MensajeTiempoInactivoDTO Actualizar(MensajeTiempoInactivoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
