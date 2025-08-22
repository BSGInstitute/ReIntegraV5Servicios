using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IProgramaGeneralBeneficioRespuestaService
    {
        #region Metodos Base
        ProgramaGeneralBeneficioRespuesta Add(ProgramaGeneralBeneficioRespuesta entidad);
        ProgramaGeneralBeneficioRespuesta Update(ProgramaGeneralBeneficioRespuesta entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralBeneficioRespuesta> Add(List<ProgramaGeneralBeneficioRespuesta> listadoEntidad);
        List<ProgramaGeneralBeneficioRespuesta> Update(List<ProgramaGeneralBeneficioRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralBeneficioRespuestaDTO> ObtenerProgramaGeneralBeneficioRespuesta();
        ProgramaGeneralBeneficioRespuesta ObtenerPorIdOportunidadIdBeneficio(int idOportunidad, int idBeneficio);
    }
}
