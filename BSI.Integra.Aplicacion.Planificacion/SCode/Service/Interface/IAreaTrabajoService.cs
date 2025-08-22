using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IAreaTrabajoService
    {
        AreaTrabajoDTO Actualizar(AreaTrabajoDTO dto, string usuario);

        bool Eliminar(int id, string usuario);
        //bool EliminarLista(List<int> ids, string usuario);
        AreaTrabajoDTO Insertar(AreaTrabajoDTO dto, string usuario);
        List<AreaTrabajoDTO> InsertarLista(List<AreaTrabajoDTO> dtos, string usuario);
        AreaTrabajoDTO ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerAreaAgenda();
    }
}
