using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{


    /// Service: PreguntaEncuestaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_PreguntaEncuesta
    /// </summary>
    public class PreguntaEncuestaService : IPreguntaEncuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaEncuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPreguntaEncuestum, PreguntaEncuesta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public PreguntaEncuesta Add(PreguntaEncuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaEncuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreguntaEncuesta Update(PreguntaEncuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaEncuesta>(modelo);
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
                _unitOfWork.PreguntaEncuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaEncuesta> Add(List<PreguntaEncuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaEncuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaEncuesta> Update(List<PreguntaEncuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaEncuesta>>(modelo);
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
                _unitOfWork.PreguntaEncuestaRepository.Delete(listadoIds, usuario);
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
        public PreguntaEncuesta ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRepository.ObtenerPorId(id);
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
                return _unitOfWork.PreguntaEncuestaRepository.ObtenerCombo();
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
        public List<BancoPreguntaEncuestaDTO> ObtenerPreguntaEncuesta()
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRepository.ObtenerPreguntaEncuesta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 07/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las preguntas asincronicas
        /// </summary> 
        /// <returns> List<PreguntaEncuestaAsincronicaDTO> </returns>
        public List<PreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRepository.ObtenerPreguntaEncuestaAsincronica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 07/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos las preguntas asincronicas
        /// </summary>
        /// <param name="idEncuesta"> Id de la entidad </param>
        /// <returns> List<PreguntaEncuestaAsincronicaDTO> </returns>
        public List<BancoPreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronicaPorId(int idEncuesta)
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRepository.ObtenerPreguntaEncuestaAsincronicaPorId(idEncuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
