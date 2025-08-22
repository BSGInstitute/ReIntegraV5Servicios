using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICompetidorService
    {
        #region Metodos Base
        Competidor Add(Competidor entidad);
        Competidor Update(Competidor entidad);
        bool Delete(int id, string usuario);

        List<Competidor> Add(List<Competidor> listadoEntidad);
        List<Competidor> Update(List<Competidor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CompetidorDTO> ObtenerCompetidor();
        IEnumerable<CompetidorComboDTO> ObtenerCombo();
        IEnumerable<CompetidorOportunidadAgendaDTO> ObtenerCompetidorParaAgendaPorIdOportunidad(int idOportunidad);
    }
}
