using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralCertificacionRespuestaRepository : IGenericRepository<TProgramaGeneralCertificacionRespuestum>
    {
        #region Metodos Base
        TProgramaGeneralCertificacionRespuestum Add(ProgramaGeneralCertificacionRespuesta entidad);
        TProgramaGeneralCertificacionRespuestum Update(ProgramaGeneralCertificacionRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralCertificacionRespuestum> Add(IEnumerable<ProgramaGeneralCertificacionRespuesta> listadoEntidad);
        IEnumerable<TProgramaGeneralCertificacionRespuestum> Update(IEnumerable<ProgramaGeneralCertificacionRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralCertificacionRespuestaDTO> ObtenerProgramaGeneralCertificacionRespuesta();
        ProgramaGeneralCertificacionRespuestaDTO ObtenerCertificacionRespuesta(int idOportunidad, int idCertificacion);
        ProgramaGeneralCertificacionRespuesta ObtenerPorIdOportunidadIdCertificacion(int idOportunidad, int idCertificacion);
    }
}