using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOtroMovimientoCajaService
    {
        #region Metodos Base
        OtroMovimientoCaja Add(OtroMovimientoCaja entidad);
        OtroMovimientoCaja Update(OtroMovimientoCaja entidad);
        bool Delete(int id, string usuario);

        List<OtroMovimientoCaja> Add(List<OtroMovimientoCaja> listadoEntidad);
        List<OtroMovimientoCaja> Update(List<OtroMovimientoCaja> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public List<OtroMovimientoCajaDTO> ObtenerListaOtroMovimientoCaja();
        public List<OtroMovimientoCajaDTO> ObtenerOtroMovimientoCajaPorID(int Id);
        public object ActualizarOtroMovimientoCaja(OtroMovimientoCajaDTO ObjetoDTO);
        public object InsertarOtroMovimientoCaja(OtroMovimientoCajaDTO ObjetoDTO);
    }
}
