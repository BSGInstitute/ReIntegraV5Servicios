using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface ITipoEncuestumService
    {
        TipoEncuesta Add(TipoEncuesta entidad);
        TipoEncuesta Update(TipoEncuesta entidad);
        bool Delete(int id, string usuario);
        TipoEncuesta ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerComboTipoModalidad();
        List<TipoEncuestaDTO> ObtenerTodo();
    }
}
