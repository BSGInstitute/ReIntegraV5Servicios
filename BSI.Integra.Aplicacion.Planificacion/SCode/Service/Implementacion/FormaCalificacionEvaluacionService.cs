using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: FormaCalificacionEvaluacionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 31/05/2023
    /// <summary>
    /// Gestión general de T_FormaCalificacionEvaluacion
    /// </summary>
    public class FormaCalificacionEvaluacionService : IFormaCalificacionEvaluacionService
    {
        private IUnitOfWork _unitOfWork;

        public FormaCalificacionEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormaCalificacionEvaluacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FormaCalificacionEvaluacionRepository.ObtenerCombo().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
