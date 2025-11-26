using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
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
    public class PuntosGeneralesService: IPuntosGeneralesService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PuntosGeneralesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCalificacionPuntoGeneral, PuntosGeneralesCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public PuntosGeneralesCalificacion Add(PuntosGeneralesCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntosGeneralesRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PuntosGeneralesCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PuntosGeneralesCalificacion Update(PuntosGeneralesCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntosGeneralesRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PuntosGeneralesCalificacion>(modelo);
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
                _unitOfWork.PuntosGeneralesRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PuntosGeneralesCalificacion> Add(List<PuntosGeneralesCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntosGeneralesRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PuntosGeneralesCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PuntosGeneralesCalificacion> Update(List<PuntosGeneralesCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntosGeneralesRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PuntosGeneralesCalificacion>>(modelo);
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
                _unitOfWork.PuntosGeneralesRepository.Delete(listadoIds, usuario);
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
        /// <returns> PuntosGeneralesCalificacion </returns>
        public PuntosGeneralesCalificacion ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PuntosGeneralesRepository.ObtenerPorId(id);
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
                return _unitOfWork.PuntosGeneralesRepository.ObtenerCombo();
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
        public IEnumerable<PuntosGeneralesCalificacion> ObtenerPuntosGenerales()
        {
            try
            {
                return _unitOfWork.PuntosGeneralesRepository.ObtenerPuntosGenerales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla por area
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<PuntosGeneralesCalificacion> ObtenerPuntosGeneralesPorArea(int idPersonalAreaTrabajo)
        {
            try
            {
                return _unitOfWork.PuntosGeneralesRepository.ObtenerPuntosGeneralesPorArea(idPersonalAreaTrabajo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
