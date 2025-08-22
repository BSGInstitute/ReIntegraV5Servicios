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
    /// Service: AsesorChatDetalleService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 17/11/2022
    /// <summary>
    /// Gestión general de T_AsesorChatDetalle
    /// </summary>
    public class AsesorChatDetalleService : IAsesorChatDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsesorChatDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAsesorChatDetalle, AsesorChatDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AsesorChatDetalle Add(AsesorChatDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsesorChatDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsesorChatDetalle Update(AsesorChatDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsesorChatDetalle>(modelo);
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
                _unitOfWork.AsesorChatDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsesorChatDetalle> Add(List<AsesorChatDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsesorChatDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsesorChatDetalle> Update(List<AsesorChatDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsesorChatDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsesorChatDetalle>>(modelo);
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
                _unitOfWork.AsesorChatDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de pais asignados para un asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerPaisesPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                return _unitOfWork.AsesorChatDetalleRepository.ObtenerPaisesPorIdAsesorChat(idAsesorChat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de programa generales asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerProgramasGeneralesPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                return _unitOfWork.AsesorChatDetalleRepository.ObtenerProgramasGeneralesPorIdAsesorChat(idAsesorChat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de area capacitacion asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerAreasCapacitacionPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                return _unitOfWork.AsesorChatDetalleRepository.ObtenerAreasCapacitacionPorIdAsesorChat(idAsesorChat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de sub area capacitacion asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerSubAreasCapacitacionPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                return _unitOfWork.AsesorChatDetalleRepository.ObtenerSubAreasCapacitacionPorIdAsesorChat(idAsesorChat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualizar el asesor chat detalle e inserta un log en la tabla com.T_ChatIntegraHistorialAsesor
        /// </summary>
        /// <param name="idAsesorChat">Id del asesor chat enlazado a la configuracion (PK de la tabla com.T_AsesorChat)</param>
        /// <param name="idPersonal">Id del personal asignado a la configuracion (PK de la tabla gp.T_Personal)</param>
        /// <param name="usuario">Cadena con el usuario que ejecuto la actualizacion</param>
        /// <param name="listaProgramas">Lista de indices de los programas generales que estan habilitados para esa configuracion (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="listaPaises">Lista de indices de los paises que estan habilitados para esa configuracion (PK de la tabla conf.T_Pais)</param>
        /// <returns></returns>
        public void ActualizarAsesorChaDetalleYLog(int idAsesorChat, int idPersonal, string usuario, string listaProgramas, string listaPaises)
        {
            try
            {
                _unitOfWork.AsesorChatDetalleRepository.ActualizarAsesorChaDetalleYLog(idAsesorChat, idPersonal, usuario, listaProgramas, listaPaises);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza a 0 el idAsesorChat en la tabla com.T_AsesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorchat (PK de la tabla com.T_AsesorChat)</param>
        /// <param name="nombreUsuario">Nombre del usuario que ejecuta el eliminado</param>
        /// <returns></returns>
        public void EliminarAsesorChatDetalle(int idAsesorChat, string nombreUsuario)
        {
            try
            {
                _unitOfWork.AsesorChatDetalleRepository.EliminarAsesorChatDetalle(idAsesorChat, nombreUsuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
