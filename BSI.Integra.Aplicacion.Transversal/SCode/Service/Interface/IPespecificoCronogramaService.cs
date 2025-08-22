using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPespecificoCronogramaService
    {
        IEnumerable<PEspecificoCronogramaGrupalGrupoDTO> ObtenerPEspecificoCronogramaGrupal(int idPEspecifico);
        IEnumerable<PEspecificoCronogramaGrupalDTO> CalcularSesionesCronogramaGrupoCompletoDesdeGrupo(int idPespecifico, int grupo);
    }
}
