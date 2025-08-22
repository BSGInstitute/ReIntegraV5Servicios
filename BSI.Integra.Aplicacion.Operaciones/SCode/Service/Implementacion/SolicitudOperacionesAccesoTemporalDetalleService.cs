using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: SolicitudOperacionesAccesoTemporalDetalleService
    /// Autor: Jonathan Caipo
    /// Fecha: 24/12/2022
    /// <summary>
    /// Gestión general de T_EvaluacionEscalaCalificacion
    /// </summary>
    public class SolicitudOperacionesAccesoTemporalDetalleService : ISolicitudOperacionesAccesoTemporalDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SolicitudOperacionesAccesoTemporalDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalle>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SolicitudOperacionesAccesoTemporalDetalle Add(SolicitudOperacionesAccesoTemporalDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudOperacionesAccesoTemporalDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudOperacionesAccesoTemporalDetalle Update(SolicitudOperacionesAccesoTemporalDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudOperacionesAccesoTemporalDetalle>(modelo);
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
                _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudOperacionesAccesoTemporalDetalle> Add(List<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudOperacionesAccesoTemporalDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudOperacionesAccesoTemporalDetalle> Update(List<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudOperacionesAccesoTemporalDetalle>>(modelo);
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
                _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Delete(listadoIds, usuario);
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
