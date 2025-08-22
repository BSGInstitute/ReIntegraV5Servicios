using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IProgramaGeneralMotivacionRespuestaService
    {
        #region Metodos Base
        ProgramaGeneralMotivacionRespuesta Add(ProgramaGeneralMotivacionRespuesta entidad);
        ProgramaGeneralMotivacionRespuesta Update(ProgramaGeneralMotivacionRespuesta entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralMotivacionRespuesta> Add(List<ProgramaGeneralMotivacionRespuesta> listadoEntidad);
        List<ProgramaGeneralMotivacionRespuesta> Update(List<ProgramaGeneralMotivacionRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralMotivacionRespuestaDTO> ObtenerProgramaGeneralMotivacionRespuesta();
        ProgramaGeneralMotivacionRespuesta ObtenerPorIdOportunidadIdMotivacion(int idOportunidad, int idMotivacion);
        ProgramaGeneralMotivacionRespuestaDTO GuardarCambios(ProgramaGeneralMotivacionRespuestaDTO motivacion, string userName);
    }
}
