using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class RespuestaPreguntaService : IRespuestaPreguntaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public RespuestaPreguntaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRespuestaPreguntum, RespuestaPregunta>(MemberList.None).ReverseMap();
                cfg.CreateMap<TRespuestaPreguntum, RespuestaPreguntaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<RespuestaPregunta, RespuestaPreguntaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RespuestaPregunta Add(RespuestaPregunta entidad)
        {
            try
            {
                var modelo = _unitOfWork.RespuestaPreguntaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RespuestaPregunta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaPregunta Update(RespuestaPregunta entidad)
        {
            try
            {
                var modelo = _unitOfWork.RespuestaPreguntaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RespuestaPregunta>(modelo);
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
                _unitOfWork.RespuestaPreguntaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RespuestaPregunta> Add(List<RespuestaPregunta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RespuestaPreguntaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RespuestaPregunta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RespuestaPregunta> Update(List<RespuestaPregunta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RespuestaPreguntaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RespuestaPregunta>>(modelo);
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
                _unitOfWork.RespuestaPreguntaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_RespuestaPregunta por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> RespuestaPregunta </returns>
        public RespuestaPregunta ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.RespuestaPreguntaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 26/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Factores Desaprovatorios
        /// </summary>
        /// <returns> IEnumerable<RespuestaPreguntaFactorDesaprovatorioComboDTO> </returns>
        public IEnumerable<RespuestaPreguntaFactorDesaprovatorioComboDTO> ObtenerFactorDesaprovatorio()
        {
            try
            {
                return _unitOfWork.RespuestaPreguntaRepository.ObtenerFactorDesaprovatorio();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las respuestas asociadas a una pregunta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<PreguntaRespuestaAsincronicaDTO> ObtenerRespuestaPregunta(int idPregunta)
        {
            try
            {
                return _unitOfWork.RespuestaPreguntaRepository.ObtenerRespuestaPregunta(idPregunta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
