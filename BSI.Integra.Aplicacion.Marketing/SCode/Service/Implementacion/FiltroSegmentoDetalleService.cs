using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: FiltroSegmentoDetalleService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FiltroSegmentoDetalleService : IFiltroSegmentoDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public FiltroSegmentoDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFiltroSegmentoDetalle, FiltroSegmentoDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FiltroSegmentoDetalle Add(FiltroSegmentoDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FiltroSegmentoDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FiltroSegmentoDetalle Update(FiltroSegmentoDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FiltroSegmentoDetalle>(modelo);
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
                _unitOfWork.FiltroSegmentoDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroSegmentoDetalle> Add(List<FiltroSegmentoDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FiltroSegmentoDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroSegmentoDetalle> Update(List<FiltroSegmentoDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FiltroSegmentoDetalle>>(modelo);
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
                _unitOfWork.FiltroSegmentoDetalleRepository.Delete(listadoIds, usuario);
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
