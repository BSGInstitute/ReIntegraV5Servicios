using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    public class CriteriosEvaluacionProgramasEspecificosService : ICriteriosEvaluacionProgramasEspecificosService
    {
        /// Service: CriteriosEvaluacionProgramasEspecificosService
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// <summary>
        /// Gestión general de T_PespecificoEsquema
        /// </summary>
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CriteriosEvaluacionProgramasEspecificosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPespecificoEsquema, PEspecificoEsquema>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public PEspecificoEsquema Add(PEspecificoEsquema entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PEspecificoEsquema>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PEspecificoEsquema Update(PEspecificoEsquema entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PEspecificoEsquema>(modelo);
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
                _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PEspecificoEsquema> Add(List<PEspecificoEsquema> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PEspecificoEsquema>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PEspecificoEsquema> Update(List<PEspecificoEsquema> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PEspecificoEsquema>>(modelo);
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
                _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// /// <summary>
        /// Obtiene lista de programas especificos padre para esquemas mediante filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>List<DatosListaPespecificoEsquemaDTO></returns>
        public IEnumerable<DatosListaPespecificoEsquemaDTO> ObtenerProgramasEspecificoEsquemasFiltrosPadreIndividual(FiltroProgramaEspecificoEsquemaFiltroCompuestoDTO filtro)
        {
            try
            {
                return _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.ObtenerProgramasEspecificoEsquemasFiltrosPadreIndividual(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// /// <summary>
        /// Obtiene lista de programas especificos padre para esquemas mediante filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>List<DatosListaPespecificoEsquemaDTO></returns>
        public ValorDTO ObtenerEsquemaPorIdPEspecifico(int IdPEspecifico)
        {
            try
            {
                return _unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.ObtenerEsquemaPorIdPEspecifico(IdPEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
