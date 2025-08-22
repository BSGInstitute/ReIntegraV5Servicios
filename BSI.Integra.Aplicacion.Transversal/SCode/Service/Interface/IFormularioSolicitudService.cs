using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoFormularioSolicitudDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFormularioSolicitudService
    {
        #region Metodos Base
        FormularioSolicitud Add(FormularioSolicitud entidad);
        FormularioSolicitud Update(FormularioSolicitud entidad);
        bool Delete(int id, string usuario);

        List<FormularioSolicitud> Add(List<FormularioSolicitud> listadoEntidad);
        List<FormularioSolicitud> Update(List<FormularioSolicitud> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<FormularioSolicitudDTO> ObtenerFormularioSolicitud();

        bool EliminarFormularioSolicitud(int IdFormulario, string Usuario);
        IEnumerable<FormularioSolicitudCompuestoDTO> ObtenerTodo(FiltroCompuestroGrillaDTO paginador);
        FormularioSolicitud InsertarFormularioSolicitud(InsertarFormularioSolicitudCampoDTO obj);

        IEnumerable<ConjuntoAnuncioFiltroCompuestoDTO> ObtenerConjuntoAnunciosFiltro(string filtro);
        IEnumerable<FiltroDTO> FormularioRespuestaFiltro(string filtro);

        IEnumerable<DTO.ComboDTO> ObtenerComboFs(InsertarFormulario2DTO nombre);
        //CompuestoConjuntoAnuncioDTO InsertarConjuntoAnuncio(CompuestoConjuntoAnuncioDTO Obj);

        //FormularioSolicitudDTO ActualizarFormularioSolicitud(InsertarFormularioSolicitudCampoDTO obj);




    }
}
