using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
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
    /// Service: CriterioEvaluacionTipoProgramaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionTipoPrograma
    /// </summary>
    public class CriterioEvaluacionTipoProgramaService : ICriterioEvaluacionTipoProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CriterioEvaluacionTipoProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica en la tabla CriterioEvaluacionTipoPrograma 
        /// </summary>
        /// <param name="idCriterioEvaluacion"> Primary key de T_CriterioEvaluacion </param>
        /// <param name="usuario"> Usuario que modifica al alumno </param> 
        /// <param name="idsCriterioEvaluacionTipoProgramaNuevos"> Todos los Ids de T_CriterioEvaluacionTipoPrograma que se guaradaran </param> 
        /// <returns> bool </returns>  
        public bool EliminacionLogicaPorCriterioEvaluacion(int idCriterioEvaluacion, string usuario, List<int> idsTipoPrograma)
        {
            try
            {
                var criterioEvaluacionTipoProgramas = _unitOfWork.CriterioEvaluacionTipoProgramaRepository.ObtenerPorIdCriterioEvaluacion(idCriterioEvaluacion);
                criterioEvaluacionTipoProgramas.RemoveAll(x => idsTipoPrograma.Any(y => y == x.IdTipoPrograma));
                var resultado = _unitOfWork.CriterioEvaluacionTipoProgramaRepository.Delete(criterioEvaluacionTipoProgramas.Select(x => x.Id.Value), usuario);
                _unitOfWork.Commit();
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

