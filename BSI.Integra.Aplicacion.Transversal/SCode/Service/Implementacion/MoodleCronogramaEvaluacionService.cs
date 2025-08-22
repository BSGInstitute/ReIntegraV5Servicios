using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MoodleCronogramaEvaluacionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 27/09/2022
    /// <summary>
    /// Gestión general de MoodleCronogramaEvaluacion
    /// </summary>
    public class MoodleCronogramaEvaluacionService : IMoodleCronogramaEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MoodleCronogramaEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMoodleCronogramaEvaluacion, MoodleCronogramaEvaluacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Congela el cronogramaEvaluacion por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> RespuestaWebDTO </returns>
        public RespuestaWebDTO CongelarCronograma(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ultima version de cronogramaAutoEvaluacion por el idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns>List<CronogramaAutoEvaluacionV2DTO> </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionUltimaVersion(int idMatriculaCabecera)
        {
            var respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.Exist(idMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(idMatriculaCabecera);
                }
                respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersion(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return respuesta;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion del cronogramaAutoEvaluazion_ultimaVersion por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List<CronogramaAutoEvaluacionV2DTO> </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersion(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarUltimaVersionCongelada(int idMatriculaCabecera, string Usuario)
        {
            bool respuesta = false;
            try
            {
                var condicionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.Exist(w => w.IdMatriculaCabecera == idMatriculaCabecera);
                if (condicionCronograma != null)
                {
                    var versionMaxima = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersion(idMatriculaCabecera).
                        Max(w => w.Version);

                    var listado = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerPorIdMatriculaCabeceraYVersion(idMatriculaCabecera, versionMaxima);
                    if (listado != null && listado.Count() > 0)
                    {
                        respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.Delete(listado.Select(s => s.Id), Usuario);
                    }
                }

                respuesta = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion del cronogramaAutoEvaluazion_ultimaVersion por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List<CronogramaAutoEvaluacionV2DTO> </returns>
        public List<VersionCronogramaAutoEvaluacionDTO> ObtenerVersionesCronograma(int idMatriculaCabecera)
        {
            var respuesta = new List<VersionCronogramaAutoEvaluacionDTO>();
            try
            {
                bool existeCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.Exist(w => w.IdMatriculaCabecera == idMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(idMatriculaCabecera);
                }
                respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerVersionesCronograma(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return respuesta;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de cursos Moodle asociados al idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id Matricula Cebecera </param     
        /// <returns> List<IdentificadorCursoMoodlePorMatriculaComboDTO> </returns>
        public List<IdentificadorCursoMoodlePorMatriculaComboDTO> ObtenerComboCursosMoodlePorMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerComboCursosMoodlePorMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idCursoMoodle de primera actividad pendiente filtrado por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id Matricula Cebecera </param     
        /// <returns> int </returns>
        public int? ObtenerIdCursoMoodlePrimeraActividadPendiente(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerIdCursoMoodlePrimeraActividadPendiente(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma AutoEvaluacion UltimaVersion Promedio filtrado por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id Matricula Cebecera </param     
        /// <returns> List<CronogramaListaCursosOnlineV2PromedioDTO> </returns>
        public List<CronogramaListaCursosOnlineV2PromedioDTO> ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(int idMatriculaCabecera)
        {
            var respuesta = new List<CronogramaListaCursosOnlineV2PromedioDTO>();

            try
            {
                bool existeCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.Exist(w => w.IdMatriculaCabecera == idMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(idMatriculaCabecera);
                }
                respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si una Plantilla Existe basado en su identificador
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public bool ExistePorId(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MoodleCronogramaEvaluacionRepository.Exist(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Reprogra cronogramas
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idEvaluacionMoodle"></param>
        /// <param name="diasRecorrer"></param>
        /// <param name="recorreCronograma"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public RespuestaWebDTO ReprogramarCronograma(int idMatriculaCabecera, int idEvaluacionMoodle, int diasRecorrer, bool recorreCronograma, string usuario)
        {
            RespuestaWebDTO respuesta = new RespuestaWebDTO();
            try
            {
                bool existeCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.Exist(idMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(idMatriculaCabecera);
                    if (respuestaCreacionCronograma.Estado)
                    {
                        respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ReprogramarCronograma(idMatriculaCabecera, idEvaluacionMoodle, diasRecorrer, recorreCronograma);
                    }
                    else
                    {
                        respuesta.Estado = false;
                        respuesta.Mensaje = "No existe o no se puede generar el cronograma.";
                    }
                }
                else
                {
                    respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ReprogramarCronograma(idMatriculaCabecera, idEvaluacionMoodle, diasRecorrer, recorreCronograma);
                }
            }
            catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        /// Autor: Daniel Huaita
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obteniene el cronograma  de autoevaluación en su ultima versión
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionUltimaVersionPorCurso(int IdMatriculaCabecera, int IdCursoMoodle)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.ExistePorIdMatriculaCabecera(IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacion_UltimaVersionPorCurso(IdMatriculaCabecera, IdCursoMoodle);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return respuesta;
        }

        /// Autor: Daniel Huaita
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obteniene el cronograma  de autoevaluación segun su version
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionPorVersion(int IdMatriculaCabecera, int Version)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.ExistePorIdMatriculaCabecera(IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _unitOfWork.MoodleCronogramaEvaluacionRepository.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacion_PorVersion(IdMatriculaCabecera, Version);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }
    }
}
