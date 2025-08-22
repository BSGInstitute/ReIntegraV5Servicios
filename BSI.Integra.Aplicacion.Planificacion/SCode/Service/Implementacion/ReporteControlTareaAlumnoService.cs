using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ReporteControlTareaAlumnoService
    /// Autor: Jonathan Caipo
    /// Fecha: 03/05/2023
    /// Version 1.0
    /// <summary>
    /// Gestión general del Reporte Control de Tareas de Alumnos
    /// </summary>
    public class ReporteControlTareaAlumnoService : IReporteControlTareaAlumnoService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteControlTareaAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                filtroReporte.FechaInicio = new DateTime(filtroReporte.FechaInicio.Year, filtroReporte.FechaInicio.Month, filtroReporte.FechaInicio.Day, 0, 0, 0);
                filtroReporte.FechaFin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReporteControlTareaAlumnoRepository.GenerarReporteControlTareaAlumno(filtroReporte);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 05/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el encargado de revision de trabajo de Pares 
        /// </summary>
        /// <param name="datos">Datos para acutalizar </param>
        /// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
        public string ActualizarPersonaCalificacionControlTareaAlumno(ReporteControlTareaAlumnoActualizacionDTO dto)
        {
            try
            {
                _unitOfWork.ReporteControlTareaAlumnoRepository.ActualizarPersonaCalificacionControlTareaAlumno(dto.Id, dto.IdProveedor);
                var nombreProveedor = _unitOfWork.ProveedorRepository.ObtenerNombreProveedorPorId(dto.IdProveedor);
                return nombreProveedor;
            }
            catch
            {
                throw;
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
                return  _unitOfWork.ReporteControlTareaAlumnoRepository.ActualizarNota(dto, usuario);
            }
            catch
            {
                throw;
            }
        }
    }
}
