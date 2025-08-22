using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOrigenService
    {
        #region Metodos Base
        Origen Add(OrigenDTO entidad, string Usuario);
        Origen Update(OrigenDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<Origen> Add(List<Origen> listadoEntidad);
        List<Origen> Update(List<Origen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<OrigenDTO> ObtenerOrigen();
        List<TarifarioDetalleAgendaDTO> ObtenerTarifariosDetallesAgenda(int idMatriculaCabecera);
        OrigenIdCategoriaOrigenDTO IdCategoriaOrigenPorOrigen(int idOrigen);
        List<ComboFiltroDTO> ObtenerOrigeneParaRegistrarOportunidad(string Area);
        List<ComboFiltroDTO> ObtenerOrigenPorCategoriaOrigen(int idCategoriaOrigenInbox, int idCategoriaOrigenCorreo, int idCategoriaOrigenComentarios);
        List<ComboFiltroDTO> ObtenerOrigenChat(string nombre);
        public IEnumerable<ComboDTO> ObtenerTodoFiltro();
        List<TarifarioDTO> ObtenerTarifarios();
        List<TarifarioDetalleConfiguracionDTO> ObtenerTarifariosDetalles(int idTarifario);
        List<TarifarioDTO> InsertarTarifario(TarifarioNuevoDTO objeto);
        List<TarifarioDTO> ActualizarTarifario(TarifarioNuevoDTO objeto);
        List<TarifarioDTO> EliminarTarifario(int idTarifario, string usuario);
        List<TarifarioDetalleDTO> EliminarTarifarioDetallePais(int id, string usuario);
        List<ComboFiltroDTO> ObtenerCombosOrigen();
    }
}
