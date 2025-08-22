using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: QuejaSugerenciaService
    /// Autor: Gilmer Qm.
    /// Fecha: 20/07/2023
    /// <summary>
    /// Gestión general de Reportes de Queja Sugerencia
    /// </summary>
    public class QuejaSugerenciaService : IQuejaSugerenciaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public QuejaSugerenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de quejas y sugerencias segun el filtro ingresado
        /// </summary>
        /// <param name="quejaSugerenciaFiltro">filtro para la seleccion del reporte de quejas y sugerencias</param>
        /// <returns>Lista del reporte quejas y sugerencias en un List<QuejaSugerenciaDTO></returns>
        public List<QuejaSugerenciaDTO> GenerarReporteQuejaSugerencia(QuejaSugerenciaFiltroDTO quejaSugerenciaFiltro)
        {
            try
            {
                string areas = null, subareas = null, programageneral = null, tipo = null;
                if (quejaSugerenciaFiltro.Area != null && quejaSugerenciaFiltro.Area.Count() > 0) areas = String.Join(",", quejaSugerenciaFiltro.Area);
                if (quejaSugerenciaFiltro.SubArea != null && quejaSugerenciaFiltro.SubArea.Count() > 0) subareas = String.Join(",", quejaSugerenciaFiltro.SubArea);
                if (quejaSugerenciaFiltro.ProgramaGeneral != null && quejaSugerenciaFiltro.ProgramaGeneral.Count() > 0) programageneral = String.Join(",", quejaSugerenciaFiltro.ProgramaGeneral);
                if (quejaSugerenciaFiltro.Tipo != null && quejaSugerenciaFiltro.Tipo.Count() > 0) tipo = String.Join(",", quejaSugerenciaFiltro.Tipo);
                DateTime fechainicio = new DateTime(quejaSugerenciaFiltro.FechaInicial.Year, quejaSugerenciaFiltro.FechaInicial.Month, quejaSugerenciaFiltro.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(quejaSugerenciaFiltro.FechaFin.Year, quejaSugerenciaFiltro.FechaFin.Month, quejaSugerenciaFiltro.FechaFin.Day, 23, 59, 59);
                return _unitOfWork.QuejaSugerenciaRepository.GenerarReporteQuejaSugerencia(fechainicio, fechafin, areas, subareas, programageneral, tipo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
