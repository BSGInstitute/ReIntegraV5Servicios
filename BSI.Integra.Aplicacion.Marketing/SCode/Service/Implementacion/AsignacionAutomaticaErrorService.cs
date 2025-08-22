using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: AsignacionAutomaticaErrorService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 03/11/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomaticaError
    /// </summary>
    public class AsignacionAutomaticaErrorService : IAsignacionAutomaticaErrorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsignacionAutomaticaErrorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAsignacionAutomaticaError, AsignacionAutomaticaError>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AsignacionAutomaticaError Add(AsignacionAutomaticaError entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaErrorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomaticaError>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomaticaError Update(AsignacionAutomaticaError entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaErrorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomaticaError>(modelo);
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
                _unitOfWork.AsignacionAutomaticaErrorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomaticaError> Add(List<AsignacionAutomaticaError> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaErrorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomaticaError>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomaticaError> Update(List<AsignacionAutomaticaError> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaErrorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomaticaError>>(modelo);
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
                _unitOfWork.AsignacionAutomaticaErrorRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<AsignacionAutomaticaErrorDTO> ObtenerError(int idAsignacionAutomatica)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaErrorRepository.ObtenerError(idAsignacionAutomatica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ValorIntDTO> ObtenerErrorAsignacionAutomatica(int Id)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaErrorRepository.ObtenerErrorAsignacionAutomatica(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
