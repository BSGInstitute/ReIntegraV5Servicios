using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstructuraEspecificaRepository
    {
        bool CongelarEstructuraAlumno(object datos, string usuario);
        List<RegistroEstructuraCursoTareaCalificarDTO> ObtenerRegistroEstructuraTareaCalificarCapitulo(int IdPGeneral, int IdPrincipal, int IdAlumno);
        string ObtenerDuracionTotalPorIdMatriculaCabecera(int idMatriculaCabecera);
    }
}
