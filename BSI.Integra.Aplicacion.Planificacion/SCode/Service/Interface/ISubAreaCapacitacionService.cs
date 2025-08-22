using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ISubAreaCapacitacionService
    {
        IEnumerable<SubAreaCapacitacionFiltroDTO> ObtenerCombo();
        IEnumerable<SubAreaCapacitacionAlternoDTO> Obtener();
        SubAreaCapacitacionAlternoDTO Insertar(SubAreaCapacitacionDTO subAreaCapacitacionDTO, string usuario);
        SubAreaCapacitacionAlternoDTO Actualizar(SubAreaCapacitacionDTO subAreaCapacitacionDTO, string usuario);
        bool Eliminar(int idSubAreaCapacitacion, string usuario);
        IEnumerable<ParametroContenidoDTO> ObtenerParametroContenidoPorIdSubAreaCapacitacion(int idSubAreaCapacitacion);
    }
}
