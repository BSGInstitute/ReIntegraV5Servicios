using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReclamoService
    {
        List<ListarReclamosDTO> ListarReclamosAlumno(int idMatricula);
        List<ListarReclamosDTO> ListarReclamos();
    }
}
