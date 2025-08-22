using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadCompetidorRepository : IGenericRepository<TOportunidadCompetidor>
    {
        #region Metodos Base
        TOportunidadCompetidor Add(OportunidadCompetidor entidad);
        TOportunidadCompetidor Update(OportunidadCompetidor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadCompetidor> Add(IEnumerable<OportunidadCompetidor> listadoEntidad);
        IEnumerable<TOportunidadCompetidor> Update(IEnumerable<OportunidadCompetidor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OportunidadCompetidorDTO> ObtenerOportunidadCompetidor();
        IEnumerable<OportunidadCompetidorComboDTO> ObtenerCombo();
        IEnumerable<OportunidadCompetidorAgendaDTO> ObtenerOportunidadCompetidorPorIdOportunidad(int idOportunidad);
        OportunidadCompetidorDTO ObtenerOportunidadCompetidorPorId(int idOportunidadCompetidor);
        OportunidadCompetidor ObtenerPorId(int idOportunidadCompetidor);
        Task<OportunidadCompetidor> ObtenerPorIdAsync(int idOportunidadCompetidor);
    }
}