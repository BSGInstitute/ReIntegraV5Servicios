using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEsquemaEvaluacionService
    {
        string ObtenerNombreCongelamientoEsquemaPorMatricula(int idMatriculaCabecera);
        EsquemaEvaluacionNotaCursoDTO ObtenerDetalleCalificacionPorCurso(int idMatriculaCabecera, int idPEspecifico, int grupo);
        int InsertarMatricula(List<ValorIdMatriculaDTO> json);
        List<CongelamientoPEspecificoMatriculaAlumnoDTO> ObtenerCongelamientoEsquemaPorMatricula(int idMatriculaCabecera);
        bool ActualizarCongelamientoEsquemaPorMatricula(EditarCongelamientoPEspecificoMatriculaAlumnoDTO json);
        EsquemaEvaluacion_NotaCursosDTO ListadoCriteriosEvaluacionPorCurso(int IdMatriculaCabecera, int IdPEspecifico, int Grupo);
        public List<EsquemaEvaluacionComboDTO> ObtenerComboEsquemaEvaluacion();
        public List<EsquemaCriterioEvaluacionDTO> ObtenerCriterioEvaluacionPorIdEsquema(int IdEsquemaEvaluacion);
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        List<EsquemaEvaluacionPgeneralAsociadoDTO> ObtenerEsquemaAsociado(int idPGeneral);
        bool ActualizarAsignacion(EsquemaEvaluacionRegistrarAsignacionDTO esquemaDTO, string usuario);
        bool RegistrarAsignacion(EsquemaEvaluacionRegistrarAsignacionDTO esquemaDTO, string usuario);
        IEnumerable<DetalleEsquemaAsignadoDTO> ObtenerDetalleEsquemaAsignado(int idEsquemaAsignado);
        IEnumerable<EsquemaEvaluacionDetalleCompuestoDTO> ObtenerDetalleEsquema(int idEsquemaEvaluacion);
        string SubirArchivo(IList<IFormFile> archivos);
        IEnumerable<EsquemaEvaluacionDTO> ObtenerTodo();
        EsquemaEvaluacionDTO Insertar(EsquemaEvaluacionDTO dto, string usuario);
        EsquemaEvaluacionDTO Actualizar(EsquemaEvaluacionDTO dto, string usuario);

        IEnumerable<ComboDTO> ObtenerComboFormaCalculoEvaluacion();
        bool Eliminar(int id, string usuario);


    }
}
