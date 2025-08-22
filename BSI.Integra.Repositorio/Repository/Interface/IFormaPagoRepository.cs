using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormaPagoRepository : IGenericRepository<TFormaPago>
    {
        #region Metodos Base
        TFormaPago Add(FormaPago entidad);
        TFormaPago Update(FormaPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFormaPago> Add(IEnumerable<FormaPago> listadoEntidad);
        IEnumerable<TFormaPago> Update(IEnumerable<FormaPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public List<FormaPagoDTO> ObtenerListaFormaPago();


    }
}
