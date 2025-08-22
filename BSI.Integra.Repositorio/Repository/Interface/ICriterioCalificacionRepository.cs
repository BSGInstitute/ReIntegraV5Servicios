using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICriterioCalificacionRepository : IGenericRepository<TCriterioCalificacion>
    {
        public List<ComboDTO> ObtenerCombo();
    }
}
