using AutoMapper;
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
    /// Service: WhatsAppNumeroValidadoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/12/2022
    /// <summary>
    /// Gestión general de WhatsAppNumeroValidado
    /// </summary>
    public class WhatsAppNumeroValidadoService : IWhatsAppNumeroValidadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppNumeroValidadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppNumeroValidado, WhatsAppNumeroValidado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public WhatsAppNumeroValidado Add(WhatsAppNumeroValidado entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppNumeroValidadoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppNumeroValidado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppNumeroValidado Update(WhatsAppNumeroValidado entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppNumeroValidadoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppNumeroValidado>(modelo);
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
                _unitOfWork.WhatsAppNumeroValidadoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppNumeroValidado> Add(List<WhatsAppNumeroValidado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppNumeroValidadoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppNumeroValidado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppNumeroValidado> Update(List<WhatsAppNumeroValidado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppNumeroValidadoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppNumeroValidado>>(modelo);
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
                _unitOfWork.WhatsAppNumeroValidadoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 06/12/2022
        /// <summary>
        /// Verifica si existe el numero en la tabla T_WhatsAppNumeroValidado
        /// </summary>
        /// <param name="numero"></param>
        /// <returns> true or false </returns>
        public bool VerificarNumeroValidado(string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppNumeroValidadoRepository.VerificarNumeroValidado(numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
