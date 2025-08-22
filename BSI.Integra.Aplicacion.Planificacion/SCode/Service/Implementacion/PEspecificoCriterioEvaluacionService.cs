using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PEspecificoCriterioEvaluacionService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 21/07/2023
    /// <summary>
    /// Gestión general de T_PespecificoCriterioEvaluacion
    /// </summary>
    public class PEspecificoCriterioEvaluacionService: IPEspecificoCriterioEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PEspecificoCriterioEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPespecificoCriterioEvaluacion, PEspecificoCriterioEvaluacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public PEspecificoCriterioEvaluacion Add(PEspecificoCriterioEvaluacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.PEspecificoCriterioEvaluacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PEspecificoCriterioEvaluacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PEspecificoCriterioEvaluacion Update(PEspecificoCriterioEvaluacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.PEspecificoCriterioEvaluacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PEspecificoCriterioEvaluacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.PEspecificoCriterioEvaluacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PEspecificoCriterioEvaluacion> Add(List<PEspecificoCriterioEvaluacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PEspecificoCriterioEvaluacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PEspecificoCriterioEvaluacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PEspecificoCriterioEvaluacion> Update(List<PEspecificoCriterioEvaluacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PEspecificoCriterioEvaluacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PEspecificoCriterioEvaluacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.PEspecificoCriterioEvaluacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
