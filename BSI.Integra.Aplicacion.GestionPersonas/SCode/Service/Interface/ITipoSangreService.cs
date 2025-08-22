using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface ITipoSangreService
    {
        IEnumerable<TipoSangreDTO> Obtener();
        TipoSangreDTO Insertar(TipoSangreDTO dto, string usuario);
        TipoSangreDTO Actualizar(TipoSangreDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
