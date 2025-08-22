using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IActividadCabeceraRepository : IGenericRepository<TActividadCabecera>
    {
        #region Metodos Base
        public TActividadCabecera Add(ActividadCabecera entidad);
        TActividadCabecera Update(ActividadCabecera entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TActividadCabecera> Add(IEnumerable<ActividadCabecera> listadoEntidad);
        IEnumerable<TActividadCabecera> Update(IEnumerable<ActividadCabecera> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ComboDTO> ObtenerFiltro();
        public List<ActividadCabeceraDTO> ObtenerTodoActividadAutomatica();
        public List<ComboDTO> ObtenerActividadesBaseMasivo();
        public List<ActividadCabeceraDTO> ObtenerActividadPorId(int IdActividadCabecera);


    }
}
