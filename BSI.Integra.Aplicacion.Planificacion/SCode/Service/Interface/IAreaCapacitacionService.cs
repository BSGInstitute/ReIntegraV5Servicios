using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IAreaCapacitacionService
    {
        //#region Metodos Base
        //AreaCapacitacion Add(AreaCapacitacion entidad);
        //AreaCapacitacion Update(AreaCapacitacion entidad);
        //bool Delete(int id, string usuario);

        //List<AreaCapacitacion> Add(List<AreaCapacitacion> listadoEntidad);
        //List<AreaCapacitacion> Update(List<AreaCapacitacion> listadoEntidad);
        //bool Delete(List<int> listadoIds, string usuario);
        //#endregion
        AreaCapacitacionDTO Actualizar(CompuestoAreaDTO dto, string usuario);

        bool Eliminar(int id, string usuario);
        //bool EliminarLista(List<int> ids, string usuario);
        AreaCapacitacionDTO Insertar(CompuestoAreaDTO dto, string usuario);
        List<AreaCapacitacionDTO> InsertarLista(List<AreaCapacitacionDTO> dtos, string usuario);

        IEnumerable<AreaCapacitacionDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<AreaCapacitacionFiltroDTO> ObtenerFiltro();
    }
}
