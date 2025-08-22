using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: WhatsAppUsuarioService
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de WhatsAppUsuario
    /// </summary>
    public class WhatsAppUsuarioService : IWhatsAppUsuarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppUsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppUsuario, WhatsAppUsuario>(MemberList.None).ReverseMap();
                cfg.CreateMap<WhatsAppUsuario, WhatsAppUsuarioDTO>(MemberList.None).ReverseMap();
            }

            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public WhatsAppUsuario Add(WhatsAppUsuario entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppUsuario>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppUsuario Update(WhatsAppUsuario entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppUsuario>(modelo);
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
                _unitOfWork.WhatsAppUsuarioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public List<WhatsAppPersonalDTO> ObtenerListaPersonal()
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioRepository.ObtenerListaPersonal();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<WhatsAppUsuarioListaGrillaDTO> ObtenerCredencialesUsuario()
        {
            try
            {
                var dto = _unitOfWork.WhatsAppUsuarioRepository.ObtenerCredencialesUsuario();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 17/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_WhatsAppUsuario por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> WhatssAppUsuario </returns>
        public WhatsAppUsuario ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.WhatsAppUsuarioRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
