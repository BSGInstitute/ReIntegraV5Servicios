using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPostulanteNivelPotencialService
    {
        IEnumerable<PostulanteNivelPotencialDTO> Obtener();
        PostulanteNivelPotencialDTO Insertar(PostulanteNivelPotencialDTO dto, string usuario);
        PostulanteNivelPotencialDTO Actualizar(PostulanteNivelPotencialDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
