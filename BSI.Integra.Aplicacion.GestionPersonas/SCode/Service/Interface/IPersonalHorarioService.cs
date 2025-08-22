using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPersonalHorarioService
    {
        List<List<TimeSpan?>> ObtenerHorarioAsTable(int idPersonal);
        IEnumerable<PersonalHorarioDTO> ObtenerPersonalHorario();
        HorarioAsesorSemanaDTO ObtenerHorario(int idPersonal);
        HorarioAsesorSemanaDTO ObtenerHorarioGP(int idPersonal);

    }
}
