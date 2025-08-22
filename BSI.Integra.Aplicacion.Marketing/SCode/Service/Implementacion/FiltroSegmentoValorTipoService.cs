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
    /// Service: FiltroSegmentoValorTipoService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FiltroSegmentoValorTipoService : IFiltroSegmentoValorTipoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public FiltroSegmentoValorTipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFiltroSegmentoValorTipo, FiltroSegmentoValorTipo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FiltroSegmentoValorTipo Add(FiltroSegmentoValorTipo entidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoValorTipoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FiltroSegmentoValorTipo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FiltroSegmentoValorTipo Update(FiltroSegmentoValorTipo entidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoValorTipoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FiltroSegmentoValorTipo>(modelo);
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
                _unitOfWork.FiltroSegmentoValorTipoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroSegmentoValorTipo> Add(List<FiltroSegmentoValorTipo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoValorTipoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FiltroSegmentoValorTipo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroSegmentoValorTipo> Update(List<FiltroSegmentoValorTipo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoValorTipoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FiltroSegmentoValorTipo>>(modelo);
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
                _unitOfWork.FiltroSegmentoValorTipoRepository.Delete(listadoIds, usuario);
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
