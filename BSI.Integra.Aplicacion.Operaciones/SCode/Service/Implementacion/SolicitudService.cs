using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Service: SolicitudService
    /// Autor: Gilmer Quispe.
    /// Fecha: 23/12/2022
    /// <summary>
    /// Gestión general de T_Solicitud
    /// </summary>
    public class SolicitudService : ISolicitudService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SolicitudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitud, Solicitud>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Solicitud Add(Solicitud entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Solicitud>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Solicitud Update(Solicitud entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Solicitud>(modelo);
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
                _unitOfWork.SolicitudRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Solicitud> Add(List<Solicitud> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Solicitud>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Solicitud> Update(List<Solicitud> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Solicitud>>(modelo);
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
                _unitOfWork.SolicitudRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_Solicitud por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> Solicitud </returns>
        public Solicitud ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SolicitudRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<TipoReporteSubCategoriaDTO> </returns>
        public IEnumerable<ReporteSolicitudDTO> ObtenerTipoReporteSubCategoria()
        {
            try
            {
                return _unitOfWork.SolicitudRepository.ObtenerSolicitudes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de SOlicitudALumno
        /// </summary> 
        /// <returns> IEnumerable<HistorialSolicitudAlumnoDTO> </returns>
        public IEnumerable<HistorialSolicitudAlumnoDTO> ObtenerHistorialSolicitudAlumno(int IdMatriculaCabecera,int IdPEspecifico)
        {
            try
            {
                return _unitOfWork.SolicitudRepository.ObtenerHistorialSolicitudAlumno(IdMatriculaCabecera, IdPEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene estados de solicitud
        /// </summary> 
        /// <returns> IEnumerable<EstadoSolicitudDTO> </returns>
        public IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitud()
        {
            try
            {
                return _unitOfWork.SolicitudRepository.ObtenerEstadosSolicitud();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene estados de solicitud para revision
        /// </summary> 
        /// <returns> IEnumerable<EstadoSolicitudDTO> </returns>
        public IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitudRevision()
        {
            try
            {
                return _unitOfWork.SolicitudRepository.ObtenerEstadosSolicitudRevision();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene estados de solicitud para gestion
        /// </summary> 
        /// <returns> IEnumerable<EstadoSolicitudDTO> </returns>
        public IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitudGestion()
        {
            try
            {
                return _unitOfWork.SolicitudRepository.ObtenerEstadosSolicitudGestion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
