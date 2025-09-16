using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: TransicionCalificacionFaseService
    /// Autor: Jose Vega.
    /// Fecha: 15/09/2025
    /// <summary>
    /// Servicio para la gestión de TransicionFaseOportunidad
    /// </summary>
    public class TransicionFaseOportunidadService : ITransicionFaseOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public TransicionFaseOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseOportunidad, TransicionFaseOportunidad>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);
        }

        #region
        public TransicionFaseOportunidad Add(TransicionFaseOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionFaseOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TransicionFaseOportunidad Update(TransicionFaseOportunidad entidad)
        {


            try
            {
                var modelo = _unitOfWork.TransicionFaseOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionFaseOportunidad>(modelo);
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
                _unitOfWork.TransicionFaseOportunidadRepository.Delete(id, usuario);
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
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<TransicionFaseOportunidadDTO> </returns>
        public List<TransicionFaseOportunidadDTO> Obtener()
        {
            try
            {
                return _unitOfWork.TransicionFaseOportunidadRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_TransicionFaseOportunidad por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> TransicionFaseOportunidad </returns>
        public TransicionFaseOportunidad ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.TransicionFaseOportunidadRepository.ObtenerPorId(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}