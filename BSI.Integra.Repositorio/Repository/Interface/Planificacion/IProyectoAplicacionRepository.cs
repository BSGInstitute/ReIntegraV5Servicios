using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProyectoAplicacionRepository
    {
        List<ProyectoAplicacionPorMatriculaCabeceraDTO> ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera);
    }
}
