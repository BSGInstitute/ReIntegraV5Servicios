using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IFormularioProgresivoSeccionPortalService
    {
        IEnumerable<FormularioProgresivoSeccionPortal> ObtenerRegistros();
    }
}
