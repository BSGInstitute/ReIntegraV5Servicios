using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;


namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioSolicitudRepository : IGenericRepository<TFormularioSolicitud>
    {
        #region Metodos Base
        TFormularioSolicitud Add(FormularioSolicitud entidad);
        TFormularioSolicitud Update(FormularioSolicitud entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFormularioSolicitud> Add(IEnumerable<FormularioSolicitud> listadoEntidad);
        IEnumerable<TFormularioSolicitud> Update(IEnumerable<FormularioSolicitud> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<FormularioSolicitudDTO> ObtenerFormularioSolicitud();

        IEnumerable<FormularioSolicitudCompuestoDTO> ObtenerTodo(FiltroCompuestroGrillaDTO paginador);

        IEnumerable<ConjuntoAnuncioFiltroCompuestoDTO> ObtenerConjuntoAnunciosFiltro(string filtro);
        IEnumerable<FiltroDTO> FormularioRespuestaFiltro(string filtro);

        IEnumerable<ComboDTO> ObtenerComboFs(InsertarFormulario2DTO nombre);
        //InsertarFormularioSolicitudCampoDTO InsertarFormularioSolicitud(InsertarFormularioSolicitudCampoDTO obj);
        //CompuestoConjuntoAnuncioDTO InsertarConjuntoAnuncio(CompuestoConjuntoAnuncioDTO Ob);









    }
}
