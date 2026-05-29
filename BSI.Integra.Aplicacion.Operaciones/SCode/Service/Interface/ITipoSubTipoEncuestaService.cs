using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface ITipoSubTipoEncuestaService
    {
        TipoSubTipoEncuesta Add(TipoSubTipoEncuesta entidad);
        TipoSubTipoEncuesta Update(TipoSubTipoEncuesta entidad);
        bool Delete(int id, string usuario);
        List<TipoSubTipoEncuestaDTO> ObtenerTodo();
        List<TipoSubTipoEncuestaDTO> ObtenerPorTipoEncuesta(int idTipoEncuesta);
        bool ExisteAsociacion(int idTipoEncuesta, int idSubTipoEncuesta);
    }
}
