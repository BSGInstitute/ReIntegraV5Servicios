using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFormaPagoService
    {
        #region Metodos Base
        FormaPago Add(FormaPago entidad);
        FormaPago Update(FormaPago entidad);
        bool Delete(int id, string usuario);

        List<FormaPago> Add(List<FormaPago> listadoEntidad);
        List<FormaPago> Update(List<FormaPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        public object ObtenerFormasPago();
        public List<FormaPagoDTO> ObtenerListaFormaPago();


    }
}
