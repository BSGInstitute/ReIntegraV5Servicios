using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICompetidorRepository : IGenericRepository<TCompetidor>
    {
        #region Metodos Base
        TCompetidor Add(Competidor entidad);
        TCompetidor Update(Competidor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCompetidor> Add(IEnumerable<Competidor> listadoEntidad);
        IEnumerable<TCompetidor> Update(IEnumerable<Competidor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CompetidorDTO> ObtenerCompetidor();
        IEnumerable<CompetidorComboDTO> ObtenerCombo();
        IEnumerable<CompetidorOportunidadAgendaDTO> ObtenerCompetidorParaAgendaPorIdOportunidad(int idOportunidad);
        Task<List<CompetidorRawDTO>> ObtenerCompetidoresPorIdOportunidadAsync(int idOportunidad);
    }
}