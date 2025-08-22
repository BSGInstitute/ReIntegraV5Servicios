using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadRemarketingAgendaService
    {
        #region Metodos Base
        OportunidadRemarketingAgenda Add(OportunidadRemarketingAgenda entidad);
        OportunidadRemarketingAgenda Update(OportunidadRemarketingAgenda entidad);
        bool Delete(int id, string usuario);

        List<OportunidadRemarketingAgenda> Add(List<OportunidadRemarketingAgenda> listadoEntidad);
        List<OportunidadRemarketingAgenda> Update(List<OportunidadRemarketingAgenda> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<OportunidadRemarketingAgendaDTO> ObtenerOportunidadRemarketingAgenda();
        bool DesactivarRedireccionRemarketingAnterior(int idOportunidad);
    }
}
