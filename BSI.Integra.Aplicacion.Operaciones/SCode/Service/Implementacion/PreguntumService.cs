using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{


    /// Service: PreguntumService
    /// Autor: Jorge Gamero
    /// Fecha: 06/05/2025
    /// <summary>
    /// Gestión general de T_Pregunta
    /// </summary>
    public class PreguntumService : IPreguntumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TPreguntaEncuestum, Preguntum>(MemberList.None).ReverseMap();
            cfg.CreateMap<TPreguntum, Preguntum>();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Preguntum Add(Preguntum entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Preguntum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Preguntum Update(Preguntum entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Preguntum>(modelo);
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
                _unitOfWork.PreguntumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Preguntum> Add(List<Preguntum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Preguntum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Preguntum> Update(List<Preguntum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Preguntum>>(modelo);
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
                _unitOfWork.PreguntumRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public Preguntum ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PreguntumRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<BancoPreguntumDTO> ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                return _unitOfWork.PreguntumRepository.ObtenerPreguntaEncuestaAsincronica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
