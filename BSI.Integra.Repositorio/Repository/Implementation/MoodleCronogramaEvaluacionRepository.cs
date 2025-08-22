using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MoodleCronogramaEvaluacionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 27/09/2022
    /// <summary>
    /// Gestión general de T_MoodleCronogramaEvaluacion
    /// </summary>
    public class MoodleCronogramaEvaluacionRepository : GenericRepository<TMoodleCronogramaEvaluacion>, IMoodleCronogramaEvaluacionRepository
    {
        private Mapper _mapper;

        public MoodleCronogramaEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMoodleCronogramaEvaluacion, MoodleCronogramaEvaluacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
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
                var query = _dapperRepository.QuerySPFirstOrDefault("ope.SP_CongelarCronogramaEvaluacion", new { idMatriculaCabecera = idMatriculaCabecera });
                var res = JsonConvert.DeserializeObject<RespuestaWebDTO>(query);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion del cronogramaAutoEvaluazion_ultimaVersion por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List<CronogramaAutoEvaluacionV2DTO> </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionUltimaVersion(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = @"SELECT CodigoMatricula,
                                        EscalaCalificacion, FechaCronograma,  FechaRendicion,
                                        Id,  IdCursoMoodle,  IdEvaluacionMoodle, IdMatriculaCabecera,
                                        IdUsuarioMoodle, NombreCurso, NombreEvaluacion,  Nota, Orden, Version 
                                    FROM ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion 
                                    WHERE IdMatriculaCabecera = @IdMatriculaCabecera 
                                    ORDER BY IdMatriculaCabecera, Version desc, Orden";
                var query = _dapperRepository.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion del T_T_MoodleCronogramaEvaluacion por el IdMatriculaCabecera y Version
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List<CronogramaAutoEvaluacionV2DTO> </returns>
        public IEnumerable<MoodleCronogramaEvaluacionDTO> ObtenerPorIdMatriculaCabeceraYVersion(int idMatriculaCabecera, int version)
        {
            try
            {
                string sqlQuery = @"SELECT Id, IdMatriculaCabecera, IdCursoMoodle, IdEvaluacionMoodle, NombreEvaluacion, IdMigracion, FechaEstimada, Orden, Version, 
                                    UsuarioCreacion, UsuarioModificacion FROM ope.T_MoodleCronogramaEvaluacion WHERE IdMatriculaCabecera = @IdMatriculaCabecera 
                                    AND Version = @Version AND Estado = 1";
                var query = _dapperRepository.QueryDapper(sqlQuery, new { IdMatriculaCabecera = idMatriculaCabecera, Version = version });

                var respuesta = JsonConvert.DeserializeObject<IEnumerable<MoodleCronogramaEvaluacionDTO>>(query);

                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de versiones del cronograma de autoevaluaciones
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cebecera</param     
        /// <returns> List<VersionCronogramaAutoEvaluacionDTO> </returns>
        public List<VersionCronogramaAutoEvaluacionDTO> ObtenerVersionesCronograma(int idMatriculaCabecera)
        {
            try
            {
                var respuesta = new List<VersionCronogramaAutoEvaluacionDTO>();
                string sql_query = @"select IdMatriculaCabecera, Version from ope.V_ObtenerCronogramaEvaluaciones_Total where IdMatriculaCabecera = @IdMatriculaCabecera group by IdMatriculaCabecera, Version Order by 1, 2";
                var respuestaQuery = _dapperRepository.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!respuestaQuery.Contains("[]") && !string.IsNullOrEmpty(respuestaQuery))
                {
                    respuesta = JsonConvert.DeserializeObject<List<VersionCronogramaAutoEvaluacionDTO>>(respuestaQuery);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
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
                string sqlQuery = "SELECT IdCursoMoodle, IdUsuarioMoodle, NombreCurso, IdMatriculaCabecera, IdOportunidad FROM ope.V_ObtenerIdentificadoresCursoMoodlePorMatricula WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var query = _dapperRepository.QueryDapper(sqlQuery, new { IdMatriculaCabecera = idMatriculaCabecera });

                var respuesta = JsonConvert.DeserializeObject<List<IdentificadorCursoMoodlePorMatriculaComboDTO>>(query);
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                string sqlQuery = "SELECT TOP 1 IdCursoMoodle FROM ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion WHERE Nota IS NULL AND IdMatriculaCabecera = @IdMatriculaCabecera ORDER BY Orden";
                var query = _dapperRepository.FirstOrDefault(sqlQuery, new { IdMatriculaCabecera = idMatriculaCabecera });

                var respuesta = JsonConvert.DeserializeObject<IdentificadorCursoMoodlePorMatriculaComboDTO>(query);
                return respuesta?.IdCursoMoodle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion del cronogramaAutoEvaluazion_ultimaVersionPromedio por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List<CronogramaListaCursosOnlineV2PromedioDTO> </returns>
        public List<CronogramaListaCursosOnlineV2PromedioDTO> ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(int idMatriculaCabecera)
        {
            try
            {
                string sqlQuery = "select * from ope.V_ObtenerCronogramaEvaluaciones_UltimaVersionPromedio where IdMatriculaCabecera = @IdMatriculaCabecera";
                var query = _dapperRepository.QueryDapper(sqlQuery, new { IdMatriculaCabecera = idMatriculaCabecera });
                var respuesta = JsonConvert.DeserializeObject<List<CronogramaListaCursosOnlineV2PromedioDTO>>(query);
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Reptrograma cronograma
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idEvaluacionMoodle"></param>
        /// <param name="diasRecorrer"></param>
        /// <param name="recorreCronograma"></param>
        /// <returns> RespuestaWebDTO </returns>
        public RespuestaWebDTO ReprogramarCronograma(int idMatriculaCabecera, int idEvaluacionMoodle, int diasRecorrer, bool recorreCronograma)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("ope.SP_ReprogramarCronogramaEvaluacion", new { idMatriculaCabecera = idMatriculaCabecera, idEvaluacionMoodle = idEvaluacionMoodle, diasRecorrer = diasRecorrer, recorreCronograma = recorreCronograma });

                var respuesta = JsonConvert.DeserializeObject<RespuestaWebDTO>(query);

                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Daniel Huaita.
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// verifica si existe cronogramaevaluaciones moodle
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> bool </returns>
        public bool ExistePorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                string sqlQuery = "select * FROM ope.T_MoodleCronogramaEvaluacion WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var query = _dapperRepository.FirstOrDefault(sqlQuery, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(query) && !query.Contains("Null"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Daniel Huaita.
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// verifica si existe cronogramaevaluaciones moodle segun su ultima version
        /// </summary>
        /// <param name="idMatriculaCabecera" name="idCursoMoodle">Id de la Matricula Cabecera, Id del curso Moodle</param>
        /// <returns> List<CronogramaAutoEvaluacionV2DTO> </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersionPorCurso(int idMatriculaCabecera, int idCursoMoodle)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion where IdMatriculaCabecera = @IdMatriculaCabecera and IdCursoMoodle = @IdCursoMoodle Order by IdMatriculaCabecera, Version desc, Orden";
                var query = _dapperRepository.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera, IdCursoMoodle = idCursoMoodle });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Daniel Huaita.
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// verifica si existe cronogramaevaluaciones moodle segun la version indicada
        /// </summary>
        /// <param name="idMatriculaCabecera" name="idCursoMoodle">Id de la Matricula Cabecera, Id del curso Moodle</param>
        /// <returns> List<CronogramaAutoEvaluacionV2DTO> </returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_PorVersion(int idMatriculaCabecera, int version)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_Total where IdMatriculaCabecera = @IdMatriculaCabecera and Version = @Version Order by 1";
                var query = _dapperRepository.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera, Version = version });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
