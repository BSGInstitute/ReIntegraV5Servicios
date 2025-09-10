using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    /// Service: FaseCalificacionService
    /// Autor: Joseph Llanque.
    /// Fecha: 03/07/2025
    /// <summary>
    /// Gestión general de TFaseCalificacion
    /// </summary>
    public class FaseCalificacionService: IFaseCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FaseCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFaseCalificacion, FaseCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public FaseCalificacion Add(FaseCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseCalificacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FaseCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaseCalificacion Update(FaseCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseCalificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FaseCalificacion>(modelo);
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
                _unitOfWork.FaseCalificacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FaseCalificacion> Add(List<FaseCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseCalificacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FaseCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FaseCalificacion> Update(List<FaseCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseCalificacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FaseCalificacion>>(modelo);
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
                _unitOfWork.FaseCalificacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FaseCalificacion </returns>
        public FaseCalificacion ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.FaseCalificacionRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FaseCalificacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FaseCalificacion> ObtenerFases()
        {
            try
            {
                return _unitOfWork.FaseCalificacionRepository.ObtenerFases();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

 
}
