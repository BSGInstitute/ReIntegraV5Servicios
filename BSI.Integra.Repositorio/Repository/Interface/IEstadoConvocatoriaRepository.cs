using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoConvocatoriaRepository : IGenericRepository<TEstadoConvocatorium>
    {
        #region Metodos Base
        TEstadoConvocatorium Add(EstadoConvocatoria entidad);
        TEstadoConvocatorium Update(EstadoConvocatoria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoConvocatorium> Add(IEnumerable<EstadoConvocatoria> listadoEntidad);
        IEnumerable<TEstadoConvocatorium> Update(IEnumerable<EstadoConvocatoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
