using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IArticuloRepository : IGenericRepository<TArticulo>
    {
        #region Metodos Base
        TArticulo Add(Articulo entidad);
        TArticulo Update(Articulo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TArticulo> Add(IEnumerable<Articulo> listadoEntidad);
        IEnumerable<TArticulo> Update(IEnumerable<Articulo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        int? ObtenerMaximaIdWeb();
        public ArticuloCompuestFiltroTotalDTO ObtenerTodo(filtroPrueba paginador);
        List<FiltroDTO> ObtenerProgramasAsociadosArticulo(int IdArticulo);
        List<FiltroDTO> ObtenerProgramasNoAsociadosArticulo(int IdArticulo);
        List<ArticuloCompuestoDTO> ObtenerTodoArticulo();
    }
}
