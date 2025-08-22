using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISubTipoMovimientoCajaService
    {
        #region Metodos Base
        SubTipoMovimientoCaja Add(SubTipoMovimientoCaja entidad);
        SubTipoMovimientoCaja Update(SubTipoMovimientoCaja entidad);
        bool Delete(int id, string usuario);

        List<SubTipoMovimientoCaja> Add(List<SubTipoMovimientoCaja> listadoEntidad);
        List<SubTipoMovimientoCaja> Update(List<SubTipoMovimientoCaja> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        public List<SubTipoMovimientoCajaDTO> ObtenerListaSubTipoMovimientoCaja();


    }
}
