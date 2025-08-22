using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IVersionProgramaService
    {
        VersionProgramaDTO Insertar(VersionProgramaDTO dto, string usuario);
        VersionProgramaDTO Actualizar(VersionProgramaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        IEnumerable<VersionProgramaDTO> ObtenerVersionPrograma();
        VersionProgramaDTO ObtenerPorId(int id);
    }
}
