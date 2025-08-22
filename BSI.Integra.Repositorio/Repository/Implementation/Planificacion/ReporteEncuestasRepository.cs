using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ReporteEncuestaFinalRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 17/05/2023
    /// <summary>
    /// Gestión general del Reporte de Encuestas Finales
    /// </summary>
    public class ReporteEncuestasRepository : IReporteEncuestasRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteEncuestasRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Repositorio : ReporteEncuestaFinalRepository
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// <summary>
        /// Obtiene los datos de la encuesta final realizada en el nuevo aula virtual para el reporte
        /// </summary>
        /// <param name="filtro">Datos traidos desde la interfaz para el filtro en el sp</param>
        /// <returns>Lista de objetos del tipo ReporteEncuestasDTO</returns>
        public IEnumerable<ReporteEncuestasDTO> ObtenerDatosEncuestas(ReporteEncuestasFiltroDTO dto, int idExamenEncuesta)
        {
            try
            {
                string pGenerales = null, pEspecificos = null, idsExpositores = null;

                if (dto.IdsProgramasGenerales != null && dto.IdsProgramasGenerales.Count() > 0)
                    pGenerales = string.Join(",", dto.IdsProgramasGenerales);

                if (dto.IdsProgramasEspecificos != null && dto.IdsProgramasEspecificos.Count() > 0)
                    pEspecificos = string.Join(",", dto.IdsProgramasEspecificos);

                if (dto.IdsDocentes != null && dto.IdsDocentes.Count() > 0)
                    idsExpositores = string.Join(",", dto.IdsDocentes);

                string query = "[pla].[SP_ReporteEncuestas]";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    Programa = pGenerales,
                    Curso = pEspecificos,
                    dto.FechaInicio,
                    dto.FechaFin,
                    IdExamen = idExamenEncuesta,
                    IdExpositor = idsExpositores,
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ReporteEncuestasDTO>>(resultado)!;
                }
                return new List<ReporteEncuestasDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#REFR-GREF-001@Error en ObtenerDatosEncuestaFinal() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/07/2023
        /// Version 1.0
        /// <summary>
        /// Obtiene preguntas y respuestas del encuesta final
        /// </summary>
        /// <returns> Lista DTO - IEnumerable<ObtenerPreguntasExamenDTO> </returns>
        public IEnumerable<ObtenerPreguntasExamenDTO> ObtenerPreguntasExamenFinal(int idExamenEncuesta)
        {
            try
            {
                var query = @"
                            SELECT NroOrden, IdRespuesta, IdPregunta, EnunciadoPregunta, NombreTipoRespuesta, IdTipoRespuesta, IdTipoPregunta, 
                            IdExamen, NroOrdenRespuesta, EnunciadoRespuesta, TipoPregunta, Estado
                            FROM gp.V_ObtenerPreguntaExamen
                            WHERE IdExamen = @IdExamenEncuesta AND Estado = 1 ORDER BY NroOrden";
                var resultado = _dapperRepository.QueryDapper(query, new { IdExamenEncuesta = idExamenEncuesta });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ObtenerPreguntasExamenDTO>>(resultado)!;
                }
                return new List<ObtenerPreguntasExamenDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#REFR-OPE-002@Error en ObtenerPreguntasExamenFinal() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/07/2023
        /// Version 1.0
        /// <summary>
        /// Obtiene preguntas y respuestas del encuesta final
        /// </summary>
        /// <returns> Lista DTO - IEnumerable<ObtenerPreguntasExamenDTO> </returns>
        public IEnumerable<ObtenerPreguntasExamenDTO> ObtenerPreguntasExamen(int idExamenEncuesta)
        {
            try
            {
                var query = "gp.SP_ObtenerPreguntaExamen";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdExamenEncuesta = idExamenEncuesta });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ObtenerPreguntasExamenDTO>>(resultado)!;
                }
                return new List<ObtenerPreguntasExamenDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#REFR-OPE-002@Error en ObtenerPreguntasExamen() {ex.Message}", ex);
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 02/05/2025
        /// Version 1.0
        /// <summary>
        /// Obtiene el idExamenEncuesta
        /// </summary>
        /// <returns> int </returns>
        public int ObtenerIdExamenEncuesta(int idTipoEncuesta,int version)
        {
            try
            {
                var query = @"SELECT Id FROM gp.T_Examen WHERE Nombre = 'Encuesta' AND Estado=1 AND IdTipoEncuesta = @idTipoEncuesta AND [Version] = @version";

                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    idTipoEncuesta,version
                });

                
                if (resultado != null )
                {
                    var respuesta = JsonConvert.DeserializeObject<IdExamenEncuestaDTO>(resultado);

                    return respuesta?.Id ?? 0;
                }

                return 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"#REFR-OPE-002@Error en ObtenerIdExamenEncuesta() {ex.Message}", ex);
            }
        }

        public List<VersionEncuestaDTO> ObtenerVersionEncuesta(int idTipoEncuesta)
        {
            try
            {
                var query = @"SELECT DISTINCT [Version] FROM gp.T_Examen WHERE Nombre = 'Encuesta' AND Estado = 1 AND IdTipoEncuesta = @IdTipoEncuesta AND [Version] IS NOT NULL";

                var resultado = _dapperRepository.QueryDapper(query,new { idTipoEncuesta });


                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<VersionEncuestaDTO>>(resultado);
                }

                return new List<VersionEncuestaDTO>();

            }
            catch (Exception ex)
            {
                throw new Exception($"#REFR-OPE-002@Error en ObtenerVersionEncuesta() {ex.Message}", ex);
            }
        }
        
    }
}
