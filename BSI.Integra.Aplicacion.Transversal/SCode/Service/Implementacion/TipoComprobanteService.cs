using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoComprobanteService
    /// Autor: Griselberto Huaman.
    /// Fecha: 17/12/2022
    /// <summary>
    /// Gestión general de T_TipoComprobante
    /// </summary>
    public class TipoComprobanteService : ITipoComprobanteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoComprobanteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoComprobante, TipoComprobante>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoComprobante, TipoComprobanteDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoComprobante Add(TipoComprobanteDTO data)
        {
            try
            {
                var entidad = _mapper.Map<TipoComprobante>(data);
                entidad.Id = 0;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;
                var modelo = _unitOfWork.TipoComprobanteRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoComprobante>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoComprobante Update(TipoComprobanteDTO data)
        {
            try
            {
                var rep = _unitOfWork.TipoComprobanteRepository;
                var entidad = _mapper.Map<TipoComprobante>(rep.FirstById(data.Id));
               
                entidad.Nombre = data.Nombre;
                entidad.IdPais = data.IdPais;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioModificacion = data.Usuario;
                var modelo = _unitOfWork.TipoComprobanteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoComprobante>(modelo);
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
                _unitOfWork.TipoComprobanteRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoComprobante> Add(List<TipoComprobante> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoComprobanteRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoComprobante>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoComprobante> Update(List<TipoComprobante> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoComprobanteRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoComprobante>>(modelo);
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
                _unitOfWork.TipoComprobanteRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoComprobante
        /// </summary>
        /// <returns> List<TipoComprobanteDTO> </returns>
        public IEnumerable<TipoComprobanteDTO> ObtenerListaTipoComprobante()
        {
            try
            {
                return _unitOfWork.TipoComprobanteRepository.ObtenerListaTipoComprobante();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }




    }
}

