using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Service: SubCriterioTareaService
    /// <summary>
    /// Gestión de T_SubCriterioTarea
    /// </summary>
    public class SubCriterioTareaService : ISubCriterioTareaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubCriterioTareaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inserta un nuevo SubCriterioTarea
        /// </summary>
        public bool Insertar(SubCriterioTareaDTO subCriterioDTO, string usuario)
        {
            try
            {
                return _unitOfWork.SubCriterioTareaRepository.Insertar(subCriterioDTO, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaService.Insertar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Actualiza un SubCriterioTarea existente
        /// </summary>
        public bool Actualizar(SubCriterioTareaDTO subCriterioDTO, string usuario)
        {
            try
            {
                return _unitOfWork.SubCriterioTareaRepository.Actualizar(subCriterioDTO, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaService.Actualizar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Elimina lógicamente un SubCriterioTarea
        /// </summary>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                return _unitOfWork.SubCriterioTareaRepository.Eliminar(id, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaService.Eliminar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene un SubCriterioTarea por Id
        /// </summary>
        public SubCriterioTareaDTO ObtenerPorId(int idSubCriterio)
        {
            try
            {
                return _unitOfWork.SubCriterioTareaRepository.ObtenerPorId(idSubCriterio);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaService.ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista todos los SubCriterioTarea activos
        /// </summary>
        public List<SubCriterioTareaDTO> ListarSubCriterios()
        {
            try
            {
                return _unitOfWork.SubCriterioTareaRepository.ListarSubCriterios();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaService.ListarSubCriterios: {ex.Message}", ex);
            }
        }
    }
}
