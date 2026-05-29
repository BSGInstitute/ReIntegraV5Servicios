using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface ISubTipoEncuestaService
    {
        SubTipoEncuesta Add(SubTipoEncuesta entidad);
        SubTipoEncuesta Update(SubTipoEncuesta entidad);
        bool Delete(int id, string usuario);
        SubTipoEncuesta ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        List<SubTipoEncuestaDTO> ObtenerTodo();
    }
}
