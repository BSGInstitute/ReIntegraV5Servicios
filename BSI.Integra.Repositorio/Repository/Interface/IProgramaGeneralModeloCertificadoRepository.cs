using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralModeloCertificadoRepository : IGenericRepository<TProgramaGeneralModeloCertificado>
    {
        #region Metodos Base
        TProgramaGeneralModeloCertificado Add(ProgramaGeneralModeloCertificado entidad);
        TProgramaGeneralModeloCertificado Update(ProgramaGeneralModeloCertificado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProgramaGeneralModeloCertificado> Add(IEnumerable<ProgramaGeneralModeloCertificado> listadoEntidad);
        IEnumerable<TProgramaGeneralModeloCertificado> Update(IEnumerable<ProgramaGeneralModeloCertificado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralModeloCertificado? ObtenerPorId(int id);
        List<ProgramaGeneralModeloCertificadoDTO> ObtenerModeloCertificadoProgramaPorIdOportunidad(int idOportunidad);
        List<PGeneralModeloCertificadoDTO> ObtenerModeloCertificadoPrograma(int idOportunidad);
        List<CompuestoProblemaModeloCertificadoDTO> ObteneCertificacionesPorModalidades(int idPGeneral);
    }
}
