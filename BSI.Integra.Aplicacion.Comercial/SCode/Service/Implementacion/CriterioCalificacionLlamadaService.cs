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

    /// Service: CriterioCalificacionService
    /// Autor: Joseph Llanque .
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudTipoReportes
    /// </summary>
    public class CriterioCalificacionLlamadaService : ICriterioCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CriterioCalificacionLlamadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCriterioCalificacionLlamadum, CriterioCalificacionLlamada>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public CriterioCalificacionLlamada Add(CriterioCalificacionLlamada entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioCalificacionLlamadaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionLlamada>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CriterioCalificacionLlamada Update(CriterioCalificacionLlamada entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioCalificacionLlamadaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionLlamada>(modelo);
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
                _unitOfWork.CriterioCalificacionLlamadaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CriterioCalificacionLlamada> Add(List<CriterioCalificacionLlamada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioCalificacionLlamadaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CriterioCalificacionLlamada>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CriterioCalificacionLlamada> Update(List<CriterioCalificacionLlamada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioCalificacionLlamadaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CriterioCalificacionLlamada>>(modelo);
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
                _unitOfWork.CriterioCalificacionLlamadaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Joseph Llanque 
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> CriterioCalificacion </returns>
        public CriterioCalificacionLlamada ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.CriterioCalificacionLlamadaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque 
        /// Fecha: 25/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CriterioCalificacionLlamada> ObtenerCriterios()
        {
            try
            {
                return _unitOfWork.CriterioCalificacionLlamadaRepository.ObtenerCriterios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque 
        /// Fecha: 25/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CriterioCalificacionLlamadaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
