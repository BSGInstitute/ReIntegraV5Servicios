using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Autor: Joseph Llanque
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Servicio de agenda de docentes. Orquesta las consultas del repositorio
    /// para construir las respuestas de los dos endpoints de GestionDocenteAgenda.
    /// </summary>
    public class GestionDocenteAgendaService : IGestionDocenteAgendaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteAgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de docentes que tienen cursos asignados con su personal asignado.
        /// </summary>
        /// <returns>Lista de DocenteConCursoDTO.</returns>
        public List<DocenteConCursoDTO> ObtenerDocentesConCursos()
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerDocentesConCursos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle completo de un docente: cabecera con datos personales, flujo asignado
        /// y todos sus cronogramas con sesiones, priorizando el curso indicado.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente/proveedor.</param>
        /// <param name="idPEspecifico">Identificador del curso a priorizar en la lista.</param>
        /// <param name="idGestionContacto">Identificador opcional del GestionContacto para obtener el flujo.</param>
        /// <returns>DocenteAgendaDetalleDTO con toda la información.</returns>
        public DocenteAgendaDetalleDTO ObtenerDetalleDocente(int idProveedor, int idPEspecifico, int? idGestionContacto)
        {
            try
            {
                var cabecera = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCabeceraDocente(idProveedor);
                if (cabecera == null) return null;

                DocenteAgendaFlujoDTO flujo = null;
                if (idGestionContacto.HasValue)
                {
                    flujo = _unitOfWork.GestionDocenteAgendaRepository.ObtenerFlujoDocente(idGestionContacto.Value);
                }

                var cronogramas = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCronogramasDocente(idProveedor, idPEspecifico);

                foreach (var cronograma in cronogramas)
                {
                    cronograma.Sesiones = _unitOfWork.GestionDocenteAgendaRepository.ObtenerSesionesPorCursoYDocente(idProveedor, cronograma.IdPEspecifico);
                }

                return new DocenteAgendaDetalleDTO
                {
                    Cabecera = cabecera,
                    Flujo = flujo,
                    Cronogramas = cronogramas
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
