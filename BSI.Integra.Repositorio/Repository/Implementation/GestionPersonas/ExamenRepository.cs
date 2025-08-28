using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ExamenRepository : GenericRepository<TExaman>, IExamenRepository
    {
        private Mapper _mapper;

        public ExamenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExaman, Examen>(MemberList.None).ReverseMap();
                cfg.CreateMap<Examen, ExamenDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Examen, TExaman>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);

        }
        public List<FactorComponenteDTO> ObtenerComponentePorEvalauacion(int idEvaluacion) {
            try
            {
                List<FactorComponenteDTO> listComponentes;
                var query = "SELECT Id, Nombre, Factor FROM gp.T_Examen WHERE Estado = 1 AND IdExamenTest = @Id";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = idEvaluacion });
                if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
                {
                    listComponentes = JsonConvert.DeserializeObject<List<FactorComponenteDTO>>(resultado).ToList();
                    return listComponentes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<int> ObtenerIdGruposPorEvaluacion(int idEvaluacion)
        {
            try
            {
                List<int> idGrupos;
                var query = "SELECT DISTINCT IdGrupoComponenteEvaluacion FROM gp.T_Examen WHERE Estado = 1 AND IdExamenTest = @Id";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = idEvaluacion });

                var listaResultado = JsonConvert.DeserializeObject<List<dynamic>>(resultado);

                if (listaResultado != null && listaResultado.Any() && listaResultado.Any(x => x.IdGrupoComponenteEvaluacion != null))
                {
                    idGrupos = listaResultado
                        .Where(x => x.IdGrupoComponenteEvaluacion != null) 
                        .Select(x => (int)x.IdGrupoComponenteEvaluacion)
                        .ToList();
                    return idGrupos;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EvaluacionPersonaCompletoDTO> ObtenerEvaluacionPersonaCompleto()
        {
            try
            {
                List<EvaluacionPersonaCompletoDTO> EvaluacionPersona = new List<EvaluacionPersonaCompletoDTO>();
                var campos = "IdExamen,NombreEvaluacion,TituloEvaluacion,Instrucciones,CronometrarExamen,TiempoLimiteExamen,IdExamenTest,ExamenTest,IdCriterioEvaluacionProceso,IdExamenConfiguracionFormato,ColorTextoTitulo,TamanioTextoTitulo,ColorFondoTitulo,TipoLetraTitulo,ColorTextoEnunciado" +
                    ",TamanioTextoEnunciado,ColorFondoEnunciado,TipoLetraEnunciado,ColorTextoRespuesta,TamanioTextoRespuesta,ColorFondoRespuesta,TipoLetraRespuesta,IdExamenComportamiento,ResponderTodasLasPreguntas,IdEvaluacionFeedBackAprobado" +
                    ",IdEvaluacionFeedBackDesaprobado,IdEvaluacionFeedBackCancelado,IdExamenConfigurarResultado,CalificarExamen,PuntajeExamen,PuntajeAprobacion,MostrarResultado,MostrarPuntajeTotal,MostrarPuntajePregunta,UsuarioModificacion,RequiereCentil,IdFormulaPuntaje, Factor";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerEValuaciones WHERE Estado = 1";
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionPersona = JsonConvert.DeserializeObject<List<EvaluacionPersonaCompletoDTO>>(dataDB);
                }
                return EvaluacionPersona;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Examen> ObtenerPorIdCriterioEvaluacionProceso(int idCriterioEvaluacionProceso)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
                        Titulo,
                        RequiereTiempo,
                        DuracionMinutos,
                        Instrucciones,
                        IdExamenTest,
                        IdExamenConfiguracionFormato,
                        IdExamenComportamiento,
                        IdExamenConfigurarResultado,
                        IdCriterioEvaluacionProceso,
                        IdGrupoComponenteEvaluacion,
                        RequiereCentil,
                        IdFormulaPuntaje,
                        Factor,
                        IdCentroCosto,
                        CantidadDiasAcceso,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_Examen
                    WHERE IdCriterioEvaluacionProceso=@idCriterioEvaluacionProceso AND estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { idCriterioEvaluacionProceso });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<Examen>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorIdCriterioEvaluacionProceso(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// <summary>
        ///	Obtiene información de Notas de Postulantes
        /// </summary>
        /// <param name="filtro"> Filtros de Búsqueda </param>
        /// <returns> List<DatosExamenPostulanteDTO> </returns>
        public List<DatosExamenPostulanteDTO> ObtenerReporteExamenPostulante(EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    Postulante = filtro.IdsPostulantes.Count() == 0 ? null : string.Join(",", filtro.IdsPostulantes),
                    IdProcesoSeleccion = filtro.IdProcesoSeleccion == null ? null : filtro.IdProcesoSeleccion.ToString(),
                    filtro.FechaInicio,
                    filtro.FechaFin,
                };
                var query = "gp.SP_ReporteExamenSelecionPersonal_V2";
                var resultado = _dapperRepository.QuerySPDapper(query, filtros);
                var rpta = JsonConvert.DeserializeObject<List<DatosExamenPostulanteDTO>>(resultado)!;
                if (filtro.idsPostulanteGrupoComparacion != null)
                {
                    var filtrosGrupoComparacion = new
                    {
                        Postulante = filtro.IdsPostulantes.Count == 0 ? null : string.Join(",", filtro.idsPostulanteGrupoComparacion.Select(x => x)),
                        IdProcesoSeleccion = filtro.IdProcesoSeleccion == null ? null : string.Join(",", filtro.IdProcesoSeleccionGrupoComparacion)
                    };
                    var queryGrupoComparacion = "gp.SP_ReporteExamenPersonalGrupoComparacion";
                    var resGrupoComparacion = _dapperRepository.QuerySPDapper(queryGrupoComparacion, filtrosGrupoComparacion);
                    var rptaGrupoComparacion = JsonConvert.DeserializeObject<List<DatosExamenPostulanteDTO>>(resGrupoComparacion)!;
                    rpta.AddRange(rptaGrupoComparacion);
                }
                rpta = rpta.OrderBy(w => w.IdExamen).ToList();
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// <summary>
        /// Obtiene lista de configuración de cantidad de evaluaciones y componentes asignados
        /// </summary>
        /// <returns> List<ObtenerConfiguracionExamenTestDTO> </returns>
        public List<ConfiguracionExamenTestDTO> ObtenerConfiguracionPuntaje()
        {
            try
            {
                List<ConfiguracionExamenTestDTO> lista = new List<ConfiguracionExamenTestDTO>();
                var query = "SELECT IdExamenTest, IdExamen, IdGrupo, CantidadPreguntas FROM [gp].[V_ObtenerConfiguracionPorExamenTest] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ConfiguracionExamenTestDTO>>(resultado)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// <summary>
        /// Obtiene lista de configuración de cantidad de evaluaciones y componentes asignados
        /// </summary>
        /// <returns> List<ObtenerConfiguracionExamenTestDTO> </returns>
        public List<string> ObtenerNombresExamenReportePostulante()
        {
            try
            {
                var query = @"
                    SELECT Nombre AS Valor
                    FROM gp.T_Examen
                    WHERE IdCentroCosto IS NOT NULL
                        AND CantidadDiasAcceso IS NOT NULL
                        AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var rpta = JsonConvert.DeserializeObject<List<StringDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor).ToList();
                }
                return new List<string>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene lista de calificacion y centiles por IdProcesoSeleccion
        /// </summary>
        /// <returns></returns>
        public List<ObtenerCalificacionCentilDTO> ObtenerInformacionCentilPorProcesoSeleccion(List<int> idsProcesoSeleccion)
        {
            try
            {
                var query = $@"
                    SELECT
	                    Id,
	                    CalificaPorCentil,
	                    EsCalificable,
	                    IdProcesoSeleccionRango,
	                    IdProcesoSeleccion,
	                    IdExamen,
	                    IdExamenTest,
	                    IdGrupoComponenteEvaluacion,
	                    PuntajeMinimo,
	                    IdCentil,
	                    Centil,
	                    IdSexoCentil,
	                    ValorMinimo,
	                    ValorMaximo,
	                    VersionCentil,
	                    EsVigente
                    FROM gp.V_ObtenerCalificacionCentiles_V02
                    WHERE IdProcesoSeleccion IN @IdsProcesoSeleccion AND Estado = 1";
                var res = _dapperRepository.QueryDapper(query, new { IdsProcesoSeleccion = idsProcesoSeleccion });
                return JsonConvert.DeserializeObject<List<ObtenerCalificacionCentilDTO>>(res)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene lista de Centiles por IdExamen
        /// </summary>
        /// <returns></returns>
        public List<CentilDTO> ObtenerCentilesAsociados(IEnumerable<int> idsExamen)
        {
            try
            {
                var query = $@"
                    SELECT
	                    Id,
	                    IdExamenTest,
	                    IdGrupoComponenteEvaluacion,
	                    IdExamen,
	                    IdSexo,
	                    ValorMinimo,
	                    ValorMaximo,
	                    Centil,
	                    CentilLetra,
	                    Estado,
	                    EsVigente,
	                    Version
                    FROM gp.V_Centil_ObtenerCalificacionCentilesPorExamen_V02
                    WHERE
	                    IdExamen IN @IdsExamen
	                    AND Estado = 1;";
                var res = _dapperRepository.QueryDapper(query, new { IdsExamen = idsExamen });
                return JsonConvert.DeserializeObject<List<CentilDTO>>(res)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Examen? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT
	                    Id,
	                    Nombre,
	                    Titulo,
	                    RequiereTiempo,
	                    DuracionMinutos,
	                    Instrucciones,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    IdExamenTest,
	                    IdExamenConfiguracionFormato,
	                    IdExamenComportamiento,
	                    IdExamenConfigurarResultado,
	                    IdCriterioEvaluacionProceso,
	                    IdGrupoComponenteEvaluacion,
	                    RequiereCentil,
	                    IdFormulaPuntaje,
	                    Factor,
	                    IdCentroCosto,
	                    CantidadDiasAcceso
                    FROM gp.T_Examen
                    WHERE
	                    Id = @Id
	                    AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<Examen>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ComboDTO> ObtenerExamenes()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = $@"
                    SELECT
	                    Id,
	                    Nombre
                    FROM gp.T_Examen
                    WHERE Estado = 1 ORDER BY Id ASC";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(res)!;
                }
                return rpta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ExamenVDTO> ObtenerComponentesAsociadosEvaluacion(int idEvaluacion)
        {
            try
            {
                List<ExamenVDTO> lista = new List<ExamenVDTO>();
                var _query = "SELECT IdExamen, NombreExamen, FactorComponente, IdEvaluacion FROM gp.V_TExamenTest_ObtenerExamenesAsociados WHERE Estado = 1 AND IdEvaluacion = @IdEvaluacion";
                var resultado = _dapperRepository.QueryDapper(_query, new { IdEvaluacion = idEvaluacion });
                if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
                {
                    lista = JsonConvert.DeserializeObject<List<ExamenVDTO>>(resultado);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<EstructuraBasicaDTO> ObtenerExamenNoAsignadoProcesoSeleccion(int IdProcesoSeleccion)
        {
            try
            {
                List<EstructuraBasicaDTO> respuesta = new List<EstructuraBasicaDTO>();
                var query = "gp.SP_ExamenesNoAsociadosConfiguracion";
                var parametros = new
                {
                    idProcesoSeleccion = IdProcesoSeleccion
                };
                var resp = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resp) && !resp.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<EstructuraBasicaDTO>>(resp)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<ExamenAsignadoProcesoDTO> ObtenerExamenAsignadoProcesoSeleccion(int IdProcesoSeleccion)
        {
            try
            {
                List<ExamenAsignadoProcesoDTO> respuesta = new List<ExamenAsignadoProcesoDTO>();
                var query = "gp.SP_ExamenesAsociadosConfiguracion";
                var parametros = new
                {
                    @IdProcesoSeleccion = IdProcesoSeleccion
                };
                var resp = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resp) && !resp.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ExamenAsignadoProcesoDTO>>(resp)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ExamenVDTO> ObtenerEvaluacion() {
            try
            {
                List<ExamenVDTO> rpta = new List<ExamenVDTO>();
                var query = @"
                    SELECT IdExamen, NombreExamen, 
                        FactorComponente, IdEvaluacion 
                        FROM gp.V_TExamenTest_ObtenerExamenesAsociados WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ExamenVDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
