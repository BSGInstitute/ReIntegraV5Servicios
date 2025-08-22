using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{

    /// Service: EncuestaOnlineService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_PreguntaEncuesta
    /// </summary>
    public class EncuestaOnlineService : IEncuestaOnlineService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EncuestaOnlineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEncuestaOnline, EncuestaOnline>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public EncuestaOnline Add(EncuestaOnline entidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaOnlineRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EncuestaOnline>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EncuestaOnline Update(EncuestaOnline entidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaOnlineRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EncuestaOnline>(modelo);
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
                _unitOfWork.EncuestaOnlineRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EncuestaOnline> Add(List<EncuestaOnline> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaOnlineRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EncuestaOnline>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EncuestaOnline> Update(List<EncuestaOnline> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaOnlineRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EncuestaOnline>>(modelo);
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
                _unitOfWork.EncuestaOnlineRepository.Delete(listadoIds, usuario);
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
        public EncuestaOnline ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.ObtenerPorId(id);
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
                return _unitOfWork.EncuestaOnlineRepository.ObtenerCombo();
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
        public List<EncuestaRegistradaDTO> ObtenerEncuestaOnline()
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.ObtenerEncuestaOnline();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 27/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene las versiones actuales de encuestas sincrónicas
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<VersionEncuestaSincronicaDTO> ObtenerVersionEncuestaSincronico()
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.ObtenerVersionEncuestaSincronico();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 28/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos que permiten crear encuestas asincronicas
        /// </summary>
        /// <param name="IdPGeneral"> IdPGeneral de la entidad </param>
        /// <returns> List<EncuestaEstructuraAsincronicaDTO> </returns>
        public List<EncuestaEstructuraAsincronicaDTO> ObtenerEncuestaAsincronicaAsignada(int IdPGeneral)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.ObtenerEncuestaAsincronicaAsignada(IdPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de encuestas asincronica
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerEncuestaAsincronica()
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.ObtenerEncuestaAsincronica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una encuesta a un curso asincronica
        /// </summary>
        /// <param name="encuesta"> Id de la entidad </param>
        /// <returns> true o false </returns>
        public bool InsertarEncuestaSesionProgramaAsincronica(EncuestaAsincronicaDTO encuesta)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.InsertarEncuestaSesionProgramaAsincronica(encuesta);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una encuesta a un curso asincronica
        /// </summary>
        /// <param name="id"> id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true o false </returns>
        public bool EliminarEncuestaAsincronicaAsignada(int Id, string Usuario)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.EliminarEncuestaAsincronicaAsignada(Id, Usuario);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 25/04/2025
        /// Version: 1.0
        /// <summary>
        /// Consulta si existe tipo de encuesta con versión
        /// </summary>
        /// <param name="idTipoEncuesta"> Id del tipo de encuesta </param>
        /// <param name="version"> Número de versión </param>
        /// <returns> bool </returns>
        public bool ExisteEncuestaOnlineTipoEncuestaVersion(int? idTipoEncuesta, int? version)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.ExisteEncuestaOnlineTipoEncuestaVersion(idTipoEncuesta, version);
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
        /// Consulta si existe tipo de encuesta con versión
        /// </summary>
        /// <param name="encuestaAsincronica"> Informacion de la encuesta sincronica </param>
        /// <returns> int </returns>
        public List<PreguntaExamenAsincronicaDTO> InsertarEncuestaAsincronica(EncuestaAsincronicaEntradaDTO encuestaAsincronica)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.InsertarEncuestaAsincronica(encuestaAsincronica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una lista preguntas a una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronicaEntradaDTO"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        public bool InsertarListaPreguntaAsincronica(List<PreguntaExamenAsincronicaDTO> encuestaAsincronica)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.InsertarListaPreguntaAsincronica(encuestaAsincronica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una pregunta a una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronicaEntradaDTO"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        public bool InsertarPreguntaEncuestaAsincronica(PreguntaExamenAsincronicaDTO encuestaAsincronica)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.InsertarPreguntaEncuestaAsincronica(encuestaAsincronica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una pregunta de una encuesta asincronica
        /// </summary>
        /// <param name="id"> id de la pregunta relacionada con la encuesta asincrona </param>
        /// <param name="usuario"> usuario modificacion </param>
        /// <returns> true o false </returns>
        public bool DeletePreguntaEncuestaAsincronica(int id, string usuario)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.DeletePreguntaEncuestaAsincronica(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una encuesta asincronica
        /// </summary>
        /// <param name="id"> id de la encuesta asincrona </param>
        /// <param name="usuario"> usuario modificacion </param>
        /// <returns> true o false </returns>
        public bool DeleteEncuestaAsincronica(int id, string usuario)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.DeleteEncuestaAsincronica(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronica"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        public bool UpdateEncuestaAsincronica(EncuestaAsincronicaEntradaDTO encuestaAsincronica)
        {
            try
            {
                return _unitOfWork.EncuestaOnlineRepository.UpdateEncuestaAsincronica(encuestaAsincronica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
