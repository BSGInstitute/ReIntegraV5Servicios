using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstadoActividadDetalleService
    {
        #region Metodos Base
        EstadoActividadDetalle Add(EstadoActividadDetalle entidad);
        EstadoActividadDetalle Update(EstadoActividadDetalle entidad);
        bool Delete(int id, string usuario);

        List<EstadoActividadDetalle> Add(List<EstadoActividadDetalle> listadoEntidad);
        List<EstadoActividadDetalle> Update(List<EstadoActividadDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion



        List<DTO.ComboDTO> ObtenerDetalleActividadFiltroCodigo();
        }
}
