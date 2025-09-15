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

        /*public List<TransicionFaseCriterioOportunidad> Add(List<TransicionFaseCriterioOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseCriterioOportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TransicionFaseCriterioOportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TransicionFaseCriterioOportunidad> Update(List<TransicionFaseCriterioOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseCriterioOportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TransicionFaseCriterioOportunidad>>(modelo);
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
                _unitOfWork.TransicionFaseCriterioOportunidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        #endregion
        /// Autor: Gilmer Quispe
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
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
        /// Autor: Gilmer Quispe
        /// Fecha: 25/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
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
