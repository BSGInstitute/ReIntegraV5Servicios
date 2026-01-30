using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.Implementacion
{
    /// Servicio: CriterioCalificacionFaseService
    /// Autor: José Vega
    /// Fecha: 15/09/2025
    /// <summary>
    /// Servicio para la gestión de CriterioCalificacionFase
    /// </summary>
    public class CriterioCalificacionFaseService : ICriterioCalificacionFaseService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CriterioCalificacionFaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, CriterioCalificacionFase>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);

        }

        #region Metodos Base
        public CriterioCalificacionFase Add(CriterioCalificacionFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioCalificacionFaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CriterioCalificacionFase Update(CriterioCalificacionFase entidad)
   {

            try
            {
                var modelo = _unitOfWork.CriterioCalificacionFaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionFase>(modelo);
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
                _unitOfWork.CriterioCalificacionFaseRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns>CriterioCalificacionFaseDTO</returns>
        public List<CriterioCalificacionFaseDTO> Obtener()
        {
            try
            {
                return _unitOfWork.CriterioCalificacionFaseRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_CriterioCalificacionFaseOportunidad por el Id.
        /// </summary>
        /// <param name="idTransicionCalificacionFase">ID de la transición</param>
        /// <returns>CriterioCalificacionFase</returns>
        public CriterioCalificacionFase ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.CriterioCalificacionFaseRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}