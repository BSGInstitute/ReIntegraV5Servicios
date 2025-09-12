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
    /// Service: SolicitudTipoReporteService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudTipoReportes
    /// </summary>
    public class SolicitudTipoReporteService : ISolicitudTipoReporteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SolicitudTipoReporteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudTipoReporte, SolicitudTipoReporte>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SolicitudTipoReporte Add(SolicitudTipoReporte entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudTipoReporteRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudTipoReporte>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudTipoReporte Update(SolicitudTipoReporte entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudTipoReporteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudTipoReporte>(modelo);
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
                _unitOfWork.TransicionFaseRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudTipoReporte> Add(List<SolicitudTipoReporte> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudTipoReporteRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudTipoReporte>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudTipoReporte> Update(List<SolicitudTipoReporte> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudTipoReporteRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudTipoReporte>>(modelo);
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
                _unitOfWork.TransicionFaseRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public SolicitudTipoReporte ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SolicitudTipoReporteRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
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
                return _unitOfWork.SolicitudTipoReporteRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
