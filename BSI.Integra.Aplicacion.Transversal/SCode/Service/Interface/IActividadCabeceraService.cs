using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IActividadCabeceraService
    {
        #region Metodos Base
        ActividadCabecera Add(ActividadCabecera entidad);
        ActividadCabecera Update(ActividadCabecera entidad);
        bool Delete(int id, string usuario);

        List<ActividadCabecera> Add(List<ActividadCabecera> listadoEntidad);
        List<ActividadCabecera> Update(List<ActividadCabecera> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion



        List<DTO.ComboDTO> ObtenerFiltro();
        public List<ActividadCabeceraDTO> ObtenerTodoActividadAutomatica();
        public List<ComboDTO> ObtenerActividadesBaseMasivo();
        public List<ActividadCabeceraDTO> ObtenerActividadPorId(int IdActividadCabecera);
        public List<ActividadCabeceraDiaSemana> ObtenerActividadDiaPorID(int idActividadCabecera);


        }
}
