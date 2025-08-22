using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioProgresivoSeccionPortalRepository : IGenericRepository<TFormularioProgresivoSeccionPortal>
    {
        IEnumerable<FormularioProgresivoSeccionPortal> ObtenerRegistros();
    }
}
