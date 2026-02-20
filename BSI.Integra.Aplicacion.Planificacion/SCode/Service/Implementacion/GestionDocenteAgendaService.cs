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

        /// <summary>
        /// Obtiene la lista plana de docentes con sus cursos y flujos asignados.
        /// </summary>
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

        /// <summary>
        /// Construye el detalle completo de un docente: cabecera, flujo y cronogramas con sesiones.
        /// El cronograma con idPEspecifico solicitado tiene esPriorizado = true y aparece primero.
        /// </summary>
        public DetalleDocenteAgendaDTO ObtenerDetalleDocente(int idProveedor, int idPEspecifico, int? idGestionContacto)
        {
            try
            {
                var cabecera = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCabeceraDocente(idProveedor);
                if (cabecera == null)
                    return null;

                FlujoDocenteAgendaDTO flujo = null;
                if (idGestionContacto.HasValue)
                {
                    flujo = _unitOfWork.GestionDocenteAgendaRepository.ObtenerFlujoDocente(idGestionContacto.Value);
                }

                var cronogramas = _unitOfWork.GestionDocenteAgendaRepository
                    .ObtenerCronogramasDocente(idProveedor, idPEspecifico);

                foreach (var cronograma in cronogramas)
                {
                    cronograma.Sesiones = _unitOfWork.GestionDocenteAgendaRepository
                        .ObtenerSesionesPorCronograma(idProveedor, cronograma.IdPEspecifico);
                }

                return new DetalleDocenteAgendaDTO
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
