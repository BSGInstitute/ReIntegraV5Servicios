using AutoMapper;
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
    /// Service: AsesorChatServiceMkt
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 17/11/2022
    /// <summary>
    /// Gestión general de T_AsesorChat
    /// </summary>
    public class AsesorChatMktService : IAsesorChatMktService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsesorChatMktService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAsesorChat, AsesorChat>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AsesorChat Add(AsesorChat entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsesorChat>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsesorChat Update(AsesorChat entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsesorChat>(modelo);
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
                _unitOfWork.AsesorChatRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsesorChat> Add(List<AsesorChat> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsesorChat>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsesorChat> Update(List<AsesorChat> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsesorChat>>(modelo);
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
                _unitOfWork.AsesorChatRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public ChatAsignadoNoAsignadoCompuestoDTO ObtenerChatAsignadosNoAsignados(FiltroCompuestroGrillaDTO paginador)
        {
            try
            {
                return _unitOfWork.AsesorChatRepository.ObtenerChatAsignadosNoAsignados(paginador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores(FiltroCompuestroGrillaDTO paginador)
        {
            try
            {
                return _unitOfWork.AsesorChatRepository.ObtenerChatListaAsesores(paginador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores2()
        {
            try
            {
                return _unitOfWork.AsesorChatRepository.ObtenerChatListaAsesores2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       

    }
}
