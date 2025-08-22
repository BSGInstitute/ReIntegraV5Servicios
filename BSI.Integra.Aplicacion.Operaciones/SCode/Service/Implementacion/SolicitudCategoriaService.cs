using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: SolicitudCategoriaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudCategorias
    /// </summary>
    public class SolicitudCategoriaService : ISolicitudCategoriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SolicitudCategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudCategorium, SolicitudCategoria>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SolicitudCategoria Add(SolicitudCategoria entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCategoriaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudCategoria>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudCategoria Update(SolicitudCategoria entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCategoriaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudCategoria>(modelo);
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
                _unitOfWork.SolicitudCategoriaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudCategoria> Add(List<SolicitudCategoria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCategoriaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudCategoria>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudCategoria> Update(List<SolicitudCategoria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCategoriaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudCategoria>>(modelo);
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
                _unitOfWork.SolicitudCategoriaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudCategoria </returns>
        public SolicitudCategoria ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SolicitudCategoriaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboSolicitudDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SolicitudCategoriaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<TipoReporteCategoriaDTO> ObtenerTipoReporteCategoria()
        {
            try
            {
                return _unitOfWork.SolicitudCategoriaRepository.ObtenerTipoReporteCategoria();
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
        public IEnumerable<TipoReporteSubCategoriaDTO> ObtenerTipoReporteSubCategoria()
        {
            try
            {
                return _unitOfWork.SolicitudCategoriaRepository.ObtenerTipoReporteSubCategoria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de la tabla asociados al IdTipoReporte
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboSolicitudDTO> ObtenerComboPorTipoReporte(int idTipoReporte)
        {
            try
            {
                return _unitOfWork.SolicitudCategoriaRepository.ObtenerComboPorTipoReporte(idTipoReporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
