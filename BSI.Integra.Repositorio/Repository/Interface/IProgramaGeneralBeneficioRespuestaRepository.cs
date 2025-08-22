using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralBeneficioRespuestaRepository : IGenericRepository<TProgramaGeneralBeneficioRespuestum>
    {
        #region Metodos Base
        TProgramaGeneralBeneficioRespuestum Add(ProgramaGeneralBeneficioRespuesta entidad);
        TProgramaGeneralBeneficioRespuestum Update(ProgramaGeneralBeneficioRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralBeneficioRespuestum> Add(IEnumerable<ProgramaGeneralBeneficioRespuesta> listadoEntidad);
        IEnumerable<TProgramaGeneralBeneficioRespuestum> Update(IEnumerable<ProgramaGeneralBeneficioRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralBeneficioRespuestaDTO> ObtenerProgramaGeneralBeneficioRespuesta();
        ProgramaGeneralBeneficioRespuesta ObtenerPorIdOportunidadIdBeneficio(int idOportunidad, int idBeneficio);
    }
}