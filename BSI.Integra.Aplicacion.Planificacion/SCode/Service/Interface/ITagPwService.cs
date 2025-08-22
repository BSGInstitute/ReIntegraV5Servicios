

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ITagPwService
    {
        IEnumerable<TagEntidadPwDTO> Obtener();
        IEnumerable<ParametroSeoPortalWebDTO> ObtenerParametroPorIdTag(int id);
        IEnumerable<TagEntidadPwDTO> Insertar(TagEntidadPwDTO dto, string usuario);
        IEnumerable<TagEntidadPwDTO> Actualizar(TagEntidadPwDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
