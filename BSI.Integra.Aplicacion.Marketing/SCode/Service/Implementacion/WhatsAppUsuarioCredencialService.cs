using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

using DocumentFormat.OpenXml.Wordprocessing;

using Google.Rpc;

using Nancy.Json;

using Newtonsoft.Json;

using RestSharp;

using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{

    /// Service: WhatsAppUsuarioCredencialService
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de WhatsAppUsuarioCredencial
    /// </summary>
    public class WhatsAppUsuarioCredencialService : IWhatsAppUsuarioCredencialService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppUsuarioCredencialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppUsuarioCredencial, WhatsAppUsuarioCredencial>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public WhatsAppUsuarioCredencial Add(WhatsAppUsuarioCredencial entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioCredencialRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppUsuarioCredencial>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppUsuarioCredencial Update(WhatsAppUsuarioCredencial entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioCredencialRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppUsuarioCredencial>(modelo);
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
                _unitOfWork.WhatsAppUsuarioCredencialRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppUsuarioCredencial> Add(List<WhatsAppUsuarioCredencial> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioCredencialRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppUsuarioCredencial>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppUsuarioCredencial> Update(List<WhatsAppUsuarioCredencial> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppUsuarioCredencialRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppUsuarioCredencial>>(modelo);
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
                _unitOfWork.WhatsAppUsuarioCredencialRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <param name="idPersonal">Id del personal</param>
        /// <param name="idPais">id del pais</param>
        /// <returns>CredencialTokenExpiraDTO</returns>
        public CredencialTokenExpiraDTO ValidarCredencialesUsuario(int idPersonal, int idPais)
        {
            try
            {
                return _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(idPersonal, idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las credenciales de login de un personal especifico
        /// </summary>
        /// <param name="idPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de tipo de CredencialUsuarioLoginDTO</returns>
        public CredencialUsuarioLoginDTO CredencialUsuarioLogin(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
