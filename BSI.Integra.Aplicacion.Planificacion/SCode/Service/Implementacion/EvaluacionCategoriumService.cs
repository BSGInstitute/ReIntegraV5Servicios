using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: EvaluacionCategoriumService
    /// Autor: Klebert Layme.
    /// Fecha: 08/05/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionCategorium
    /// </summary>
    public class EvaluacionCategoriumService : IEvaluacionCategoriumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EvaluacionCategoriumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCriterioEvaluacionCategorium, CriterioEvaluacionCategorium>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TCriterioEvaluacionCategorium, CriterioEvaluacionCategoriumDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<CriterioEvaluacionCategorium, CriterioEvaluacionCategoriumDTO>(MemberList.None).ReverseMap();
                }
              );
            _mapper = new Mapper(config);

        }
        /// Autor: Sergio Yepez.
        /// Fecha: 07/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EvaluacionCategoria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CriterioEvaluacionCategoriumRepository.ObtenerCombo().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }

    }
}