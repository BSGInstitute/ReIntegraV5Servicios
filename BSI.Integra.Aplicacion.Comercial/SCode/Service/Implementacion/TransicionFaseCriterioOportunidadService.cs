using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Comercial;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    /// Service: SolicitudTipoReporteService
    /// Autor: Jose Vega.
    /// Fecha: 15/09/2025
    /// <summary>
    /// Gestión general de T_TransicionFaseCriterioOportunidad
    /// </summary>
    public class TransicionFaseCriterioOportunidadService : ITransicionFaseCriterioOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TransicionFaseCriterioOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseCriterioOportunidad, TransicionFaseCriterioOportunidad>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);
        }

        #region Metodos Base
        public TransicionFaseCriterioOportunidad Add(TransicionFaseCriterioOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseCriterioOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionFaseCriterioOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransicionFaseCriterioOportunidad Update(TransicionFaseCriterioOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseCriterioOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionFaseCriterioOportunidad>(modelo);
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
                _unitOfWork.TransicionFaseCriterioOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_TransicionFaseCriterioOportunidad por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> TransicionFaseCriterioOportunidad </returns>
        public TransicionFaseCriterioOportunidad ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.TransicionFaseCriterioOportunidadRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<TransicionFaseCriterioOportunidadDTO> </returns>
        public List<TransicionFaseCriterioOportunidadDTO> Obtener()
        {
            try
            {
                return _unitOfWork.TransicionFaseCriterioOportunidadRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
