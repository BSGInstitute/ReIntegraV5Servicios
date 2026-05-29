using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoSubTipoEncuestaRepository : IGenericRepository<TTipoSubTipoEncuesta>
    {
        #region Metodos Base
        TTipoSubTipoEncuesta Add(TipoSubTipoEncuesta entidad);
        TTipoSubTipoEncuesta Update(TipoSubTipoEncuesta entidad);
        bool Delete(int id, string usuario);
        #endregion

        List<TipoSubTipoEncuestaDTO> ObtenerTodo();
        List<TipoSubTipoEncuestaDTO> ObtenerPorTipoEncuesta(int idTipoEncuesta);
        bool ExisteAsociacion(int idTipoEncuesta, int idSubTipoEncuesta);
    }
}
