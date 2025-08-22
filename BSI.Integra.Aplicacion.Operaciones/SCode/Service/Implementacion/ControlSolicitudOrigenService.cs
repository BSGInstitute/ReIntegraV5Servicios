using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Service: ControlSolicitudOrigenService
    /// Autor: Jorge Gamero.
    /// Fecha: 17/07/2024
    /// <summary>
    /// Gestión general de T_ControlSolicitudOrigen
    /// </summary>
    public class ControlSolicitudOrigenService : IControlSolicitudOrigenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ControlSolicitudOrigenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TControlSolicitudOrigen, ControlSolicitudOrigen>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ControlSolicitudOrigen Add(ControlSolicitudOrigen entidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlSolicitudOrigenRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ControlSolicitudOrigen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlSolicitudOrigen Update(ControlSolicitudOrigen entidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlSolicitudOrigenRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ControlSolicitudOrigen>(modelo);
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
                _unitOfWork.ControlSolicitudOrigenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_ControlSolicitudOrigen por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> ControlSolicitudOrigen </returns>
        public ControlSolicitudOrigen ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.ControlSolicitudOrigenRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ControlSolicitudOrigenRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ControlSolicitudOrigen> ObtenerRegistros()
        {
            try
            {
                return _unitOfWork.ControlSolicitudOrigenRepository.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
