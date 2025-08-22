using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioRespuestaRepository : IGenericRepository<TFormularioRespuestum>
    {
        #region Metodos Base
        TFormularioRespuestum Add(FormularioRespuesta entidad);
        TFormularioRespuestum Update(FormularioRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFormularioRespuestum> Add(IEnumerable<FormularioRespuesta> listadoEntidad);
        IEnumerable<TFormularioRespuestum> Update(IEnumerable<FormularioRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<FormularioRespuestaDTO> ObtenerFormularioRespuesta();
        IEnumerable<FormularioRespuestaFiltroDTO> ObtenerFiltroFormularioRespuestum();
        IEnumerable<ProgramaGeneralDatoDTO> ObtenerComboDato();



    }
}
