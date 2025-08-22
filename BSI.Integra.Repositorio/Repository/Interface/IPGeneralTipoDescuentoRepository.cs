using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPGeneralTipoDescuentoRepository : IGenericRepository<TPgeneralTipoDescuento>
    {
        #region Metodos Base
        TPgeneralTipoDescuento Add(PGeneralTipoDescuento entidad);
        TPgeneralTipoDescuento Update(PGeneralTipoDescuento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralTipoDescuento> Add(IEnumerable<PGeneralTipoDescuento> listadoEntidad);
        IEnumerable<TPgeneralTipoDescuento> Update(IEnumerable<PGeneralTipoDescuento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PGeneralTipoDescuentoDTO> ObtenerPGeneralTipoDescuento();
        IEnumerable<PGeneralTipoDescuentoComboDTO> ObtenerCombo();
        BoolDTO ObtenerFlagPromocion(int idPGeneral, int idTipoDescuento);
        Task<BoolDTO> ObtenerFlagPromocionAsync(int idPGeneral, int idTipoDescuento);

        IEnumerable<int> ObtenerProgramaPorDescuento(int IdTipoDescuento);
        public IEnumerable<PGeneralTipoDescuento> ObtenerPorId(int id);
        public PGeneralTipoDescuento? ObtenerPorIdTipoDescuentoYIdPGeneral(int idPGeneral, int idTipoDescuento);
        public IEnumerable<PGeneralTipoDescuento> ObtenerPorIdPGeneral(int idPGeneral);
    }
}