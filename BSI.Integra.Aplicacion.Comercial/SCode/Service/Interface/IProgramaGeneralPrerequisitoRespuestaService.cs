using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IProgramaGeneralPrerequisitoRespuestaService
    {
        #region Metodos Base
        ProgramaGeneralPrerequisitoRespuesta Add(ProgramaGeneralPrerequisitoRespuesta entidad);
        ProgramaGeneralPrerequisitoRespuesta Update(ProgramaGeneralPrerequisitoRespuesta entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralPrerequisitoRespuesta> Add(List<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad);
        List<ProgramaGeneralPrerequisitoRespuesta> Update(List<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralPrerequisitoRespuestaDTO> ObtenerProgramaGeneralPrerequisitoRespuesta();
        ProgramaGeneralPrerequisitoRespuesta ObtenerPrerequisitoRespuesta(int idOportunidad, int idPrerequisito);
    }
}
