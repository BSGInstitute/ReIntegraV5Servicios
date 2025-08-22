using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoSolicitudBeneficioRepository : IGenericRepository<TEstadoSolicitudBeneficio>
    {
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
