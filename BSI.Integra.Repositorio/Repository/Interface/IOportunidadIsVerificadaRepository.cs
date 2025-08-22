using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadIsVerificadaRepository : IGenericRepository<TOportunidadIsVerificadum>
    {
        #region Metodos Base
        TOportunidadIsVerificadum Add(OportunidadIsVerificada entidad);
        TOportunidadIsVerificadum Update(OportunidadIsVerificada entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadIsVerificadum> Add(IEnumerable<OportunidadIsVerificada> listadoEntidad);
        IEnumerable<TOportunidadIsVerificadum> Update(IEnumerable<OportunidadIsVerificada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        OportunidadIsVerificada ObtenerPorIdOportunidadOIdMatriculaCabecera(int idOportunidad, int idOportunidadMatriculaCabecera);
        List<OportunidadesVerificadasDTO> ObtenerOportunidadesVerificadas();
        List<OportunidadIsVerificadaDTO> ObtenerOportunidadIsVerificadaSinPeriodo();
        List<OportunidadIsVerificadaDTO> ObtenerOportunidadIsVerificadaConPeriodo(DateTime fechaInicio, DateTime fechaFin);
    }
}
