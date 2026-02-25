using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Service: CriterioTareaService
    /// <summary>
    /// Gestión de T_CriterioTarea
    /// </summary>
    public class CriterioTareaService : ICriterioTareaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CriterioTareaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inserta un nuevo CriterioTarea
        /// </summary>
        public bool Insertar(CriterioTareaDTO criterioDTO, string usuario)
        {
            try
            {
                return _unitOfWork.CriterioTareaRepository.Insertar(criterioDTO, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaService.Insertar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Actualiza un CriterioTarea existente
        /// </summary>
        public bool Actualizar(CriterioTareaDTO criterioDTO, string usuario)
        {
            try
            {
                return _unitOfWork.CriterioTareaRepository.Actualizar(criterioDTO, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaService.Actualizar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Elimina lógicamente un CriterioTarea
        /// </summary>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                return _unitOfWork.CriterioTareaRepository.Eliminar(id, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaService.Eliminar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene un CriterioTarea por Id
        /// </summary>
        public CriterioTareaDTO ObtenerPorId(int idCriterio)
        {
            try
            {
                return _unitOfWork.CriterioTareaRepository.ObtenerPorId(idCriterio);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaService.ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista todos los CriterioTarea activos
        /// </summary>
        public List<CriterioTareaDTO> ListarCriterios()
        {
            try
            {
                return _unitOfWork.CriterioTareaRepository.ListarCriterios();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaService.ListarCriterios: {ex.Message}", ex);
            }
        }
    }
}
