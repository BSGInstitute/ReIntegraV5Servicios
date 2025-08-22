using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IProgramaGeneralProblemaDetalleSolucionRespuestaService
    {
        #region Metodos Base
        ProgramaGeneralProblemaDetalleSolucionRespuesta Add(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad);
        ProgramaGeneralProblemaDetalleSolucionRespuesta Update(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralProblemaDetalleSolucionRespuesta> Add(List<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad);
        List<ProgramaGeneralProblemaDetalleSolucionRespuesta> Update(List<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool GuardarCambiosAgenda(ProgramaGeneralProblemaDetalleSolucionRespuestaDTO obj, string usuario);
    }
}
