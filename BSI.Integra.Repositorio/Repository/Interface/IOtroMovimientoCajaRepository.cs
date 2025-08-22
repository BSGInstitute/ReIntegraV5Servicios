using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOtroMovimientoCajaRepository : IGenericRepository<TOtroMovimientoCaja>
    {
        #region Metodos Base
        TOtroMovimientoCaja Add(OtroMovimientoCaja entidad);
        TOtroMovimientoCaja Update(OtroMovimientoCaja entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOtroMovimientoCaja> Add(IEnumerable<OtroMovimientoCaja> listadoEntidad);
        IEnumerable<TOtroMovimientoCaja> Update(IEnumerable<OtroMovimientoCaja> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<OtroMovimientoCajaDTO> ObtenerListaOtroMovimientoCaja();
        public List<OtroMovimientoCajaDTO> ObtenerOtroMovimientoCajaPorID(int Id);
    };
}
