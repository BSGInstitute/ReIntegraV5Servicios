using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IProcesoSeleccionService
    {
        List<ProcesoSeleccionCompuestoDTO> ObtenerProcesoSeleccionConvocatoria();

        List<ProcesoSeleccionDTO> ObtenerProcesosSeleccion();

        List<ProcesoSeleccionDTO> ObtenerProcesoSeleccionTotal();

        IEnumerable<ProcesoSeleccionEstadoFiltroDTO> ObtenerEstadoProcesoSeleccion();
        IEnumerable<ProcesoSeleccionComboReporteDTO> ObtenerCombo();



    }
}
