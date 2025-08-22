using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
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
    /// Service: PreguntaEncuestaOnlineService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudEncuestaOnline
    /// </summary>
    public class PreguntaEncuestaOnlineService : IPreguntaEncuestaOnlineService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaEncuestaOnlineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPreguntaEncuestaOnline, PreguntaEncuestaOnline>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public PreguntaEncuestaOnline Add(PreguntaEncuestaOnline entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaOnlineRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaEncuestaOnline>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreguntaEncuestaOnline Update(PreguntaEncuestaOnline entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaOnlineRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaEncuestaOnline>(modelo);
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
                _unitOfWork.PreguntaEncuestaOnlineRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaEncuestaOnline> Add(List<PreguntaEncuestaOnline> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaOnlineRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaEncuestaOnline>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaEncuestaOnline> Update(List<PreguntaEncuestaOnline> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaEncuestaOnlineRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaEncuestaOnline>>(modelo);
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
                _unitOfWork.PreguntaEncuestaOnlineRepository.Delete(listadoIds, usuario);
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
        public PreguntaEncuestaOnline ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaOnlineRepository.ObtenerPorId(id);
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
                return _unitOfWork.PreguntaEncuestaOnlineRepository.ObtenerCombo();
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
        public List<PreguntaAsociadaEncuestaOnlineDTO> ObtenerPreguntaEncuestaOnline(int idEncuestaOnline)
        {
            try
            {
                return _unitOfWork.PreguntaEncuestaOnlineRepository.ObtenerPreguntaEncuestaOnline(idEncuestaOnline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
