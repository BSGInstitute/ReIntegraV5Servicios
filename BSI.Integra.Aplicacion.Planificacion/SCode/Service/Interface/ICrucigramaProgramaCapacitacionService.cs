
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICrucigramaProgramaCapacitacionService
    {
        CrucigramaProgramaCapacitacionCombosDTO ObtenerCombos();
        IEnumerable<ReporteExcelCrucigramasDTO> ObtenerReporteCrucigramasExportacionExcel();
        IEnumerable<CrucigramaProgramaCapacitacionRespuestaDTO> ObtenerCrucigramasRegistrados();
        IEnumerable<CrucigramaProgramaCapacitacionDetalleDTO> ObtenerRespuestaCrucigramaProgramaCapacitacion(int idCrucigramaProgramaCapacitacion);
        bool EliminarCrucigrama(int id, string usuario);
        bool EliminarCrucigramasSeleccionados(List<int> ids, string usuario);
        ImportarExcelRespuestaDTO ImportarExcel(IFormFile files);
        bool InsertarCrucigrama(CompuestoCrucigramaProgramaCapacitacionDTO dto, string usuario);
        bool ActualizarCrucigrama(CompuestoCrucigramaProgramaCapacitacionDTO dto, string usuario);
    }
}
