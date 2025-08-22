using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IMaestroEvaluacionService
    {
        IEnumerable<ExamenTestDTO> Obtener();
        ExamenTestDTO Actualizar(ExamenTestDTO examenTest);
        bool Eliminar(int id, string usuario);
        ExamenTestDTO Insertar(ExamenTestDTO examenTest);
        List<GrupoComponenteEvaluacionDTO> ObtenerEvaluacionEditarGrupoGrilla(int idEvaluacion);
        bool InsertarCentilGrupoComponente(CentilDTO centil, string usuario);
        bool ActualizarCentilGrupoComponente(CentilDTO centil, string usuario);
        List<CentilDTO> ObtenerCentilesPorEvaluacion(int idEvaluacion);
        List<GrupoComponenteDTO> ObtenerGruposComponenteDesglosado(int idEvaluacion);
        List<CentilDTO> ObtenerCentilGrupoComponente(int idGrupoComponenteEvaluacion);
        List<ComboDTO> ObtenerExamenesAsignados(int idEvaluacion);
        List<ComboDTO> ObtenerExamenesNoAsignados(int idEvaluacion);
        List<ComboDTO> ObtenerGrupos(int idEvaluacion);
        ObtenerDataComboMaestroEvaluacionDTO ObtenerCombosModulo();
        List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int idEvaluacion);
    }
}
