using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Service: RemitenteMailingService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 09/11/2022
    /// <summary>
    /// Gestión general de T_RemitenteMailing
    /// </summary>
    public class RemitenteMailingService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RemitenteMailingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRemitenteMailing, RemitenteMailing>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RemitenteMailing Add(RemitenteMailing entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemitenteMailing>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RemitenteMailing Update(RemitenteMailing entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemitenteMailing>(modelo);
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
                _unitOfWork.RemitenteMailingRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemitenteMailing> Add(List<RemitenteMailing> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemitenteMailing>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemitenteMailing> Update(List<RemitenteMailing> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemitenteMailing>>(modelo);
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
                _unitOfWork.RemitenteMailingRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Max Mantilla Rodríguez
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtener la lista de registros de RemitentesMailing
        /// </summary>
        /// <param name="nombre"> nombre de búsqueda </param>
        /// <returns> Lista de registros de remitentes mailing : List<RemitenteMailingDTO> </returns>
        public List<RemitenteMailingDTO> ObtenerTodosRemitenteMailing()
        {
            try
            {
                return _unitOfWork.RemitenteMailingRepository.ObtenerTodosRemitenteMailing();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista con los asesores y sus emails dado el Id de RemitenteMailing
        /// </summary>
        /// <returns></returns>
        public List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor(int IdRemitenteMailing)
        {
            try
            {
                return _unitOfWork.RemitenteMailingRepository.ObtenerListaRemitenteMailingAsesor(IdRemitenteMailing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
