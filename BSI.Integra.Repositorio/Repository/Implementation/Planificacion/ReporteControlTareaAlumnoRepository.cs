using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ReporteControlTareaAlumnoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 04/05/2023
    /// <summary>
    /// Gestión general de Reporte Control de Tareas de Alumnos
    /// </summary>
    public class ReporteControlTareaAlumnoRepository : IReporteControlTareaAlumnoRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteControlTareaAlumnoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/05/2023
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de proyectos presentados por el alumno, segun los filtros suministrados.
        /// </summary>
        /// <returns> Retorma una lista List<ProyectoPresentadoPorAlumnoDTO> </returns>
        public IEnumerable<ReporteControlTareaAlumnoDTO> GenerarReporteControlTareaAlumno(ReporteControlTareaAlumnoFiltroDTO filtroReporte)
        {
            try
            {
                string programaEspecifico = null, alumno = null, codigoMatricula = null;
                if (filtroReporte.IdsProgramasEspecificos != null && filtroReporte.IdsProgramasEspecificos.Count() > 0)
                    programaEspecifico = String.Join(",", filtroReporte.IdsProgramasEspecificos);

                if (filtroReporte.IdsAlumnos != null && filtroReporte.IdsAlumnos.Count() > 0)
                    alumno = String.Join(",", filtroReporte.IdsAlumnos);

                if (filtroReporte.IdMatriculaCabecera > 0 && filtroReporte.IdMatriculaCabecera != 0)
                    codigoMatricula = String.Join("%,", filtroReporte.IdMatriculaCabecera);

                var resultado = _dapperRepository.QuerySPDapper("[pla].[SP_ReporteTrabajoDeParesV2]", new { filtroReporte.FechaInicio, filtroReporte.FechaFin, programaEspecifico, codigoMatricula, alumno, filtroReporte.EstadoTarea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ReporteControlTareaAlumnoDTO>>(resultado)!;
                }
                return new List<ReporteControlTareaAlumnoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#RCTAR-GRCTA-001@Error en GenerarReporteControlTareaAlumno() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/07/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza el encargado ed la revision de una tarea
        /// </summary>
        /// <returns>un entero </returns>
        public int ActualizarPersonaCalificacionControlTareaAlumno(int id, int idProveedor)
        {
            try
            {
                var query = "com.SP_ActualizarEncargadoTrabajoPares";
                var subQuery = _dapperRepository.QuerySPDapper(query, new { id, idProveedor });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"#RCTAR-APCCTA-002@Error en ActualizarPersonaCalificacionControlTareaAlumno() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 11/07/2023
		/// Versión: 1.0
		/// <summary>
		/// Actualiza la nota de la tarea
		/// </summary>
		/// <param name="dto">Datos para acutalizar </param>
		/// <returns> El reporte retorna una Lista List<TrabajoDeParesDTO> </returns>
        public bool ActualizarNota(NotaDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_ActualizarNotaTrabajoPares";
                var resultado = _dapperRepository.QuerySPDapper(query, new { Id = dto.Id, Nota = dto.Nota, Usuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"#RCTA-AN-003@Error en ActualizarNota() {ex.Message}", ex);
            }
        }
    }
}
