using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IArticuloService
    {
        #region Metodos Base
        Articulo Add(Articulo entidad);
        Articulo Update(Articulo entidad);
        bool Delete(int id, string usuario);
        List<Articulo> Add(List<Articulo> listadoEntidad);
        List<Articulo> Update(List<Articulo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public int? ObtenerMaximaIdWeb();
        public ArticuloCompuestFiltroTotalDTO ObtenerTodo(filtroPrueba paginador);
        public List<FiltroDTO> ObtenerProgramasAsociadosArticulo(int IdArticulo);
        public List<FiltroDTO> ObtenerProgramasNoAsociadosArticulo(int IdArticulo);
        Articulo InsertarArticuloParametroSeo(InsertarArticuloParametroSeoDTO obj);
        Articulo ActualizarArticuloParametroSeo(InsertarArticuloParametroSeoDTO obj);

    }
}
