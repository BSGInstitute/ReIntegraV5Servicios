using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralPrerequisitoService
    {
        IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(int idOportunidad);
        bool EliminarPreRequisitos(int idPrerequisito, string usuario);
        ProgramaGeneralPrerequisitoDTO GuardarPreRequisitos(CompuestoPreRequisitoModalidadAlternaDTO jsonDTO, string usuario);
    }
}
