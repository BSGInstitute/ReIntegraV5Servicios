using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadRemarketingAgendaRepository : IGenericRepository<TOportunidadRemarketingAgendum>
    {
        #region Metodos Base
        TOportunidadRemarketingAgendum Add(OportunidadRemarketingAgenda entidad);
        TOportunidadRemarketingAgendum Update(OportunidadRemarketingAgenda entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadRemarketingAgendum> Add(IEnumerable<OportunidadRemarketingAgenda> listadoEntidad);
        IEnumerable<TOportunidadRemarketingAgendum> Update(IEnumerable<OportunidadRemarketingAgenda> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OportunidadRemarketingAgendaDTO> ObtenerOportunidadRemarketingAgenda();
        bool DesactivarRedireccionRemarketingAnterior(int idOportunidad);
        Task<bool> DesactivarRedireccionRemarketingAnteriorAsync(int idOportunidad);
        public bool EliminarRedireccionRemarketingAnterior(int idOportunidad);
    }
}