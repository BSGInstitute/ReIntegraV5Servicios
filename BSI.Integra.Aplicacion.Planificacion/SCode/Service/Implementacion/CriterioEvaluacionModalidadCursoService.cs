using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: CriterioEvaluacionModalidadCursoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionModalidadCurso
    /// </summary>
    public class CriterioEvaluacionModalidadCursoService : ICriterioEvaluacionModalidadCursoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CriterioEvaluacionModalidadCursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCurso>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica en la tabla T_CriterioEvaluacionModalidadCurso 
        /// </summary>
        /// <param name="idCriterioEvaluacion"> Primary key de T_CriterioEvaluacion </param>
        /// <param name="usuario"> Usuario que modifica al alumno </param> 
        /// <param name="idsCriterioEvaluacionTipoProgramaNuevos"> Todos los Ids de T_CriterioEvaluacionTipoPrograma que se guaradaran </param> 
        /// <returns> bool </returns>  
        public bool EliminacionLogicaPorCriterioEvaluacion(int idCriterioEvaluacion, string usuario, List<int> idsCriterioEvaluacionTipoProgramaNuevos)
        {
            try
            {
                List<int> listaBorrar = new List<int>();
                var criterioEvaluacionTipoProgramas = _unitOfWork.CriterioEvaluacionModalidadCursoRepository.ObtenerPorIdCriterioEvaluacion(idCriterioEvaluacion);
                listaBorrar = criterioEvaluacionTipoProgramas.Select(x => x.IdModalidadCurso).ToList();
                listaBorrar.RemoveAll(x => idsCriterioEvaluacionTipoProgramaNuevos.Any(y => y == x));

                var resultado = _unitOfWork.CriterioEvaluacionModalidadCursoRepository.Delete(listaBorrar, usuario);
                _unitOfWork.Commit();
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public  List<CriterioEvaluacionModalidadDTO> ListarCriteriosEvaluacionModalidad()
        {
            try
            {
               return _unitOfWork.CriterioEvaluacionModalidadCursoRepository.ListarCriteriosEvaluacionModalidad();

            }
            catch
            {
                throw;
            }
        }
    }
}
