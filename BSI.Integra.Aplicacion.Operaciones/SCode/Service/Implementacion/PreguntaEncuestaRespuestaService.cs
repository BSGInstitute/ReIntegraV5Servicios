using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{
    /// Service: SolicitudTipoReporteService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudTipoReportes
    /// </summary>
    public class PreguntaEncuestaRespuestaService : IPreguntaEncuestaRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaEncuestaRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPreguntaEncuestaRespuestum, PreguntaEncuestaRespuesta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public PreguntaEncuestaRespuesta Add(PreguntaEncuestaRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaEncuestaRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreguntaEncuestaRespuesta Update(PreguntaEncuestaRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRespuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaEncuestaRespuesta>(modelo);
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
                _unitOfWork.PreguntaEncuestaRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaEncuestaRespuesta> Add(List<PreguntaEncuestaRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaEncuestaRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaEncuestaRespuesta> Update(List<PreguntaEncuestaRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaEncuestaRespuesta>>(modelo);
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
                _unitOfWork.PreguntaEncuestaRespuestaRepository.Delete(listadoIds, usuario);
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
        public PreguntaEncuestaRespuesta ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRespuestaRepository.ObtenerPorId(id);
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
                return _unitOfWork.PreguntaEncuestaRespuestaRepository.ObtenerCombo();
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
        public List<PreguntaEncuestaRespuestaDTO> ObtenerCategoriaEncuesta()
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRespuestaRepository.ObtenerPreguntaEncuestaRespuesta();
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
        /// Obtiene todos las respuestas asociadas a una pregunta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<PreguntaRespuestaDTO> ObtenerRespuestaPregunta(int idPregunta)
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaRespuestaRepository.ObtenerRespuestaPregunta(idPregunta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
