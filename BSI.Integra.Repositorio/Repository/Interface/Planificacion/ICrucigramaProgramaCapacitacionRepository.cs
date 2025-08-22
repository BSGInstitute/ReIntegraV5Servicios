using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICrucigramaProgramaCapacitacionRepository : IGenericRepository<TCrucigramaProgramaCapacitacion>
    {
        #region Metodos Base
        TCrucigramaProgramaCapacitacion Add(CrucigramaProgramaCapacitacion entidad);
        TCrucigramaProgramaCapacitacion Update(CrucigramaProgramaCapacitacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCrucigramaProgramaCapacitacion> Add(IEnumerable<CrucigramaProgramaCapacitacion> listadoEntidad);
        IEnumerable<TCrucigramaProgramaCapacitacion> Update(IEnumerable<CrucigramaProgramaCapacitacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ReporteExcelCrucigramasDTO> ObtenerReporteCrucigramasExportacionExcel();
        IEnumerable<CrucigramaProgramaCapacitacionRespuestaDTO> ObtenerCrucigramasRegistrados();
        IEnumerable<CrucigramaProgramaCapacitacionDetalleDTO> ObtenerRespuestaCrucigramaProgramaCapacitacion(int idCrucigramaProgramaCapacitacion);
        CrucigramaProgramaCapacitacion? ObtenerPorId(int id);
    }
}
