using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IConfigurarProcesoSeleccionService
    {
        IEnumerable<ComboDTO> ObtenerComboPuestoTrabajo();
        IEnumerable<ComboDTO> ObtenerComboSedeTrabajo();
        IEnumerable<CriterioEvaluacionProcesoDTO> ObtenerComboCriterioSeleccion();
        IEnumerable<ProcesoSeleccionRangoDTO> ObtenerProcesoSeleccionRango();
        List<ConfigurarProcesoSeleccionDTO> ObtenerProcesoSeleccion();
        IEnumerable<ComboDTO> ObtenerExamenes();
        List<EstructuraBasicaDTO> ObtenerExamenesNoAsociados(int IdProcesoSeleccion);
        EvaluacionesAsociacionDTO ObtenerEvaluacionesAsociacion(int IdProcesoSeleccion);
        List<ExamenAsignadoProcesoDTO> ObtenerExamenesAsociados(int IdProcesoSeleccion);
        EvaluacionPuntajeDTO ObtenerEvaluacionPuntaje(int IdProcesoSeleccion);
        bool Actualizar(ProcesoSeleccionAgrupadoInsertarModificarDTO dto, string usuario);
        List<ProcesoSeleccionEtapaDTO> ObtenerProcesoSeleccionEtapa(int IdProcesoSeleccion);
        bool ActualizarProcesoSeleccionConfiguracionCalificacion(PuntajeEvaluacionAgrupadaComponenteDTO Json, string usuario);
    }
}
