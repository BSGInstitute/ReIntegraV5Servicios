using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModalidadPagoRepository : IGenericRepository<TModalidadPago>
    {
        #region Metodos Base
        TModalidadPago Add(ModalidadPago entidad);
        TModalidadPago Update(ModalidadPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModalidadPago> Add(IEnumerable<ModalidadPago> listadoEntidad);
        IEnumerable<TModalidadPago> Update(IEnumerable<ModalidadPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ModalidadPagoDTO> ObtenerModalidadesPago();
    }
}
