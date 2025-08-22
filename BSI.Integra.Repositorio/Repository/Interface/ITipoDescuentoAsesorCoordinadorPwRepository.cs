using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoDescuentoAsesorCoordinadorPwRepository : IGenericRepository<TTipoDescuentoAsesorCoordinadorPw>
    {
        #region Metodos Base
        TTipoDescuentoAsesorCoordinadorPw Add(TipoDescuentoAsesorCoordinadorPw entidad);
        TTipoDescuentoAsesorCoordinadorPw Update(TipoDescuentoAsesorCoordinadorPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoDescuentoAsesorCoordinadorPw> Add(IEnumerable<TipoDescuentoAsesorCoordinadorPw> listadoEntidad);
        IEnumerable<TTipoDescuentoAsesorCoordinadorPw> Update(IEnumerable<TipoDescuentoAsesorCoordinadorPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoDescuentoAsesorCoordinadorPw> ObtenerPorIdTipoDescuento(int idTipoDescuento);
        IEnumerable<string> ObtenerTiposPorIdTipoDescuento(int idTipoDescuento);
    }
}
