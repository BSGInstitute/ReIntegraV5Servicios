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
    /// Service: SolicitudSubCategoriaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudSubCategoria
    /// </summary>
    public class SolicitudSubCategoriaService : ISolicitudSubCategoriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SolicitudSubCategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudSubCategorium, SolicitudSubCategoria>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SolicitudSubCategoria Add(SolicitudSubCategoria entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudSubCategoriaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudSubCategoria>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudSubCategoria Update(SolicitudSubCategoria entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudSubCategoriaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudSubCategoria>(modelo);
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
                _unitOfWork.SolicitudSubCategoriaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudSubCategoria> Add(List<SolicitudSubCategoria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudSubCategoriaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudSubCategoria>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudSubCategoria> Update(List<SolicitudSubCategoria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudSubCategoriaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudSubCategoria>>(modelo);
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
                _unitOfWork.SolicitudSubCategoriaRepository.Delete(listadoIds, usuario);
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
        /// <returns> SolicitudSubCategoria </returns>
        public SolicitudSubCategoria ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SolicitudSubCategoriaRepository.ObtenerPorId(id);
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
        public bool InsertarProblema(SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            try
            {
                return _unitOfWork.SolicitudSubCategoriaRepository.InsertarProblema(solicitudCategoriaEntradaDTO);
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
        public bool ActualizarProblema(SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            try
            {
                return _unitOfWork.SolicitudSubCategoriaRepository.ActualizarProblema(solicitudCategoriaEntradaDTO);
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
        public IEnumerable<ComboSubCategoriaDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SolicitudSubCategoriaRepository.ObtenerCombo();
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
        public bool EliminarProblema(SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            try
            {
                return _unitOfWork.SolicitudSubCategoriaRepository.EliminarProblema(solicitudCategoriaEntradaDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
