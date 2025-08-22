using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPublicoObjetivoRespuestaRepository : IGenericRepository<TPublicoObjetivoRespuestum>
    {
        #region Metodos Base
        TPublicoObjetivoRespuestum Add(PublicoObjetivoRespuesta entidad);
        TPublicoObjetivoRespuestum Update(PublicoObjetivoRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPublicoObjetivoRespuestum> Add(IEnumerable<PublicoObjetivoRespuesta> listadoEntidad);
        IEnumerable<TPublicoObjetivoRespuestum> Update(IEnumerable<PublicoObjetivoRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PublicoObjetivoRespuesta> ObtenerPublicoObjetivoRespuesta();
        PublicoObjetivoRespuesta ObtenerPorIdOportunidadIdDocumentoSeccion(int idOportunidad, int idDocumentoSeccion);
    }
}