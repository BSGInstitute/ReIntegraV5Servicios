using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IOportunidadCompetidorService
    {
        #region Metodos Base
        OportunidadCompetidor Add(OportunidadCompetidor entidad);
        OportunidadCompetidor Update(OportunidadCompetidor entidad);
        bool Delete(int id, string usuario);

        List<OportunidadCompetidor> Add(List<OportunidadCompetidor> listadoEntidad);
        List<OportunidadCompetidor> Update(List<OportunidadCompetidor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OportunidadCompetidorDTO> ObtenerOportunidadCompetidor();
        IEnumerable<OportunidadCompetidorComboDTO> ObtenerCombo();
        IEnumerable<OportunidadCompetidorAgendaDTO> ObtenerOportunidadCompetidorPorIdOportunidad(int idOportunidad);
        OportunidadCompetidorDTO ObtenerOportunidadCompetidorPorId(int idOportunidadCompetidor);
        OportunidadCompetidor MapeoEntidadDesdeDTO(OportunidadCompetidorDTO dto);
        OportunidadCompetidor ObtenerPorId(int idOportunidadCompetidor);
    }
}
