using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPagoRepository : IGenericRepository<TPago>
    {
        #region Metodos Base
        TPago Add(Pago entidad);
        TPago Update(Pago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPago> Add(IEnumerable<Pago> listadoEntidad);
        IEnumerable<TPago> Update(IEnumerable<Pago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        
    }
}
