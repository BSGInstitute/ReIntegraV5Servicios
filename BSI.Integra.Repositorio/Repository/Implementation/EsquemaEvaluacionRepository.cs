using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EsquemaEvaluacionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 14/11/2022
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacion
    /// </summary>
    public class EsquemaEvaluacionRepository : GenericRepository<TEsquemaEvaluacion>, IEsquemaEvaluacionRepository
    {
        private Mapper _mapper;

        public EsquemaEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacion, EsquemaEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalle>(MemberList.None).ReverseMap();


            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TEsquemaEvaluacion MapeoEntidad(EsquemaEvaluacion entidad)
        {
            try
            {
                TEsquemaEvaluacion modelo = _mapper.Map<TEsquemaEvaluacion>(entidad);
                if (entidad.EsquemaEvaluacionDetalles != null && entidad.EsquemaEvaluacionDetalles.Count > 0)
                {
                    modelo.TEsquemaEvaluacionDetalles = _mapper.Map<List<TEsquemaEvaluacionDetalle>>(entidad.EsquemaEvaluacionDetalles);
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEsquemaEvaluacion Add(EsquemaEvaluacion entidad)
        {
            try
            {
                var esquemaEvaluacion = MapeoEntidad(entidad);
                base.Insert(esquemaEvaluacion);
                return esquemaEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEsquemaEvaluacion Update(EsquemaEvaluacion entidad)
        {
            try
            {
                var esquemaEvaluacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                esquemaEvaluacion.RowVersion = entidadExistente.RowVersion;

                base.Update(esquemaEvaluacion);
                return esquemaEvaluacion;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TEsquemaEvaluacion> Add(IEnumerable<EsquemaEvaluacion> listadoEntidad)
        {
            try
            {
                List<TEsquemaEvaluacion> listado = new List<TEsquemaEvaluacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TEsquemaEvaluacion> Update(IEnumerable<EsquemaEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEsquemaEvaluacion> listado = new List<TEsquemaEvaluacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EsquemaEvaluacion.
        /// </summary>
        /// <returns> List<EsquemaEvaluacionDTO> </returns>
        public IEnumerable<EsquemaEvaluacionDTO> ObtenerTodo()
        {
            try
            {
                List<EsquemaEvaluacionDTO> rpta = new List<EsquemaEvaluacionDTO>();
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        IdFormaCalculoEvaluacion,
                        IdModalidadCurso,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        EsModulo 
                    FROM 
                        pla.T_EsquemaEvaluacion
                    WHERE
                        Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EsquemaEvaluacionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-OT-002@Error en ObtenerTodo() {ex.Message}", ex);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_EsquemaEvaluacion por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - EsquemaEvaluacion </returns>
        public EsquemaEvaluacion? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        Nombre,
                        IdFormaCalculoEvaluacion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        EsModulo 
                    FROM 
                        pla.T_EsquemaEvaluacion
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<EsquemaEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EER-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Nombre Congelamiento Esquema por idMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> string </returns>
        public string ObtenerNombreCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                List<CongelamientoPEspecificoMatriculaAlumnoDTO> esquema = new List<CongelamientoPEspecificoMatriculaAlumnoDTO>();
                var queryfiltro = @"SELECT 
                                        Id, IdMatriculaCabecera, IdPEspecifico, EstadoPadre, Nombre, IdProgramaGeneral 
                                    FROM 
                                        [ope].[V_CongelamientoPEspecificoAlumno] 
                                    WHERE 
                                        IdMatriculaCabecera = @IdMatriculaCabecera";
                var Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    esquema = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro)!;
                }
                else
                {
                    return null;
                }
                queryfiltro = @"SELECT 
                                    IdMatriculaCabecera, IdProgramaGeneral, Id, IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno, IdEsquemaEvaluacion, NombreEsquema,
                                    IdFormaCalculoEvaluacion, FormaCalculoEvaluacion, IdEsquemaEvaluacionPGeneral 
                                FROM 
                                    [ope].[V_CongelamientoPEspecificoEsquemasEvaluacionAlumno] 
                                WHERE 
                                    IdMatriculaCabecera = @IdMatriculaCabecera";
                Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { IdMatriculaCabecera = idMatriculaCabecera });
                esquema[0].EsquemasEvaluacion = new List<EsquemaEvaluacionCongeladoListadoDTO>();
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    esquema[0].EsquemasEvaluacion = JsonConvert.DeserializeObject<List<EsquemaEvaluacionCongeladoListadoDTO>>(Subfiltro)!;
                }
                return esquema[0].EsquemasEvaluacion.Count() > 0 ? esquema[0].EsquemasEvaluacion[0].NombreEsquema : null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerNombreCongelamientoEsquemaPorMatricula2(int idMatriculaCabecera)
        {
            try
            {
                List<CongelamientoPEspecificoMatriculaAlumnoDTO> esquema = new List<CongelamientoPEspecificoMatriculaAlumnoDTO>();
                var queryfiltro = @"
                                    SELECT 
                                        Id, IdMatriculaCabecera, IdPEspecifico, EstadoPadre, Nombre, IdProgramaGeneral 
                                    FROM 
                                        [ope].[V_CongelamientoPEspecificoAlumno] 
                                    WHERE 
                                        IdMatriculaCabecera = @IdMatriculaCabecera";
                var Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { idMatriculaCabecera });

                Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { idMatriculaCabecera });
                esquema = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro)!;

                queryfiltro = @"
                                SELECT 
                                    IdMatriculaCabecera, IdProgramaGeneral, Id, IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno, IdEsquemaEvaluacion, NombreEsquema,
                                    IdFormaCalculoEvaluacion, FormaCalculoEvaluacion, IdEsquemaEvaluacionPGeneral 
                                FROM 
                                    [ope].[V_CongelamientoPEspecificoEsquemasEvaluacionAlumno] 
                                WHERE 
                                    IdMatriculaCabecera = @IdMatriculaCabecera";
                Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { IdMatriculaCabecera = idMatriculaCabecera });
                esquema[0].EsquemasEvaluacion = new List<EsquemaEvaluacionCongeladoListadoDTO>();
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    esquema[0].EsquemasEvaluacion = JsonConvert.DeserializeObject<List<EsquemaEvaluacionCongeladoListadoDTO>>(Subfiltro)!;
                }
                return esquema[0].EsquemasEvaluacion.Count() > 0 ? esquema[0].EsquemasEvaluacion[0].NombreEsquema : null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista Detalladad de Item Evaluable por alumno
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPEspecifico"></param>
        /// <param name="grupo"></param>
        /// <returns> List<EsquemaEvaluacionItemEvaluableAlumnoDTO> </returns>
        public List<EsquemaEvaluacionItemEvaluableAlumnoDTO> ListadoDetalladoItemEvaluablePorAlumno(int idMatriculaCabecera, int idPEspecifico, int grupo)
        {
            try
            {
                var query = "SELECT * FROM ope.V_ListadoActividadesCalificables_Completo where IdMatriculaCabecera=@idMatriculaCabecera AND IdPespecifico=@idPespecifico AND Grupo=@grupo";
                var res = _dapperRepository.QueryDapper(query,
                    new
                    {
                        idMatriculaCabecera = idMatriculaCabecera,
                        idPespecifico = idPEspecifico,
                        grupo = grupo
                    });
                return JsonConvert.DeserializeObject<List<EsquemaEvaluacionItemEvaluableAlumnoDTO>>(res)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>bool</returns>
        public bool ExisteNuevaAulaVirtual(int idPEspecifico)
        {
            try
            {
                var query = "SELECT Id FROM [pla].[V_TPEspecificoNuevoAulaVirtual_DataBasica] WHERE Id = @idPEspecifico";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico });
                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int EliminarMatriculaCabecera(int IdMatriculaCabecera)
        {
            try
            {
                _dapperRepository.QuerySPFirstOrDefault("ope.SP_EliminarCongelamientoPEspecificoMatriculaAlumno", new { IdMatriculaCabecera });

                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int InsertarMatriculaNueva(int IdMatriculaCabecera)
        {
            try
            {

                _dapperRepository.QuerySPFirstOrDefault("ope.SP_CongelarEsquemaEvaluacionMatriculaAlumno", new { IdMatriculaCabecera });

                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int InsertarMatriculaAntigua(int IdMatriculaCabecera)
        {
            try
            {
                _dapperRepository.QuerySPFirstOrDefault("ope.SP_CongelarEsquemaEvaluacionMatriculaAlumnoAntiguo", new { IdMatriculaCabecera });

                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Congelamiento Esquema Por Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> Lista: List<CongelamientoPEspecificoMatriculaAlumnoDTO> </returns>
        public List<CongelamientoPEspecificoMatriculaAlumnoDTO> ObtenerCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                List<CongelamientoPEspecificoMatriculaAlumnoDTO> esquemas = new List<CongelamientoPEspecificoMatriculaAlumnoDTO>();
                var queryfiltro = @"
                                    SELECT 
                                        Id, IdMatriculaCabecera, IdPEspecifico, EstadoPadre, Nombre, IdProgramaGeneral 
                                    FROM 
                                        [ope].[V_CongelamientoPEspecificoAlumno] 
                                    WHERE 
                                        IdMatriculaCabecera = @IdMatriculaCabecera";
                var Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    esquemas = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro)!;
                }
                var i = 0;
                var j = 0;
                foreach (var esquema in esquemas)
                {
                    queryfiltro = @"
                                    SELECT 
                                        IdMatriculaCabecera, IdProgramaGeneral, Id, IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno, IdEsquemaEvaluacion, NombreEsquema, IdFormaCalculoEvaluacion,
                                        FormaCalculoEvaluacion, IdEsquemaEvaluacionPGeneral 
                                    FROM 
                                        [ope].[V_CongelamientoPEspecificoEsquemasEvaluacionAlumno] 
                                    WHERE 
                                        IdProgramaGeneral = @IdProgramaGeneral AND  (IdMatriculaCabecera = 0 OR IdMatriculaCabecera = @IdMatriculaCabecera)";
                    Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new { IdProgramaGeneral = esquema.IdProgramaGeneral, IdMatriculaCabecera = idMatriculaCabecera });
                    if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                    {
                        esquemas[i].EsquemasEvaluacion = JsonConvert.DeserializeObject<List<EsquemaEvaluacionCongeladoListadoDTO>>(Subfiltro)!;
                    }
                    if (esquema.EsquemasEvaluacion != null)
                    {
                        foreach (var esquemaevaluacion in esquemas[i].EsquemasEvaluacion)
                        {
                            queryfiltro = @"
                                            SELECT 
                                                Id, IdCriterioEvaluacion, IdProveedor, NombreEsquemaDetalle, Ponderacion, IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno,
                                                IdCriterioEvaluacionCategoria,  IdFormaCalculoEvaluacion, EsquemasEvaluacionDetalle, IdEsquemaEvaluacionPGeneral 
                                            FROM 
                                                [ope].[V_CongelamientoEsquemasEvaluacionDetallesAlumno] 
                                            WHERE 
                                                (IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno=@IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno AND 
                                                IdEsquemaEvaluacionPGeneral = 0) OR (IdEsquemaEvaluacionPGeneral = @IdEsquemaEvaluacionPGeneral AND 
                                                IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno=0)";
                            Subfiltro = _dapperRepository.QueryDapper(queryfiltro, new
                            {
                                IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno = esquemaevaluacion.IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno,
                                IdEsquemaEvaluacionPGeneral = esquemaevaluacion.IdEsquemaEvaluacionPGeneral
                            });
                            if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                            {
                                esquemas[i].EsquemasEvaluacion[j].EsquemasEvaluacionDetalle = JsonConvert.DeserializeObject<List<EsquemaEvaluacionDetalleCongeladoDTO>>(Subfiltro)!;
                            }
                            j++;
                        }
                    }
                    j = 0;
                    i++;
                };
                return esquemas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el congelamiento del Esquema Evaluacion
        /// </summary>
        /// <param name="json"></param>
        /// <returns> bool </returns>
        public bool ActualizarCongelamientoEsquemaPorMatricula(EditarCongelamientoPEspecificoMatriculaAlumnoDTO json)
        {
            try
            {
                var query = "ope.SP_ActualizarCongelamientoEsquemaEvaluacionMatricula";
                var res = _dapperRepository.QuerySPDapper(query,
                    new
                    {
                        IdMatriculaCabecera = json.IdMatriculaCabecera,
                        IdPEspecifico = json.IdPEspecifico,
                        idEsquemaEvaluacionGeneral = json.idEsquemaEvaluacionGeneral,
                    });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: TEsquemaEvaluacionRepositorioAula
        /// Autor: Max Mantilla
        /// Fecha: 10/02/2022
        /// <summary>
        /// Obtiene información de los esquemas de evaluación según su IdMatriculaCabecera
        /// </summary>
        /// <returns> List<CriterioEvaluacionCursoDTO> </returns>  
        public List<CriterioEvaluacionCursoDTO> ListadoCriteriosEvaluacionPorCurso(int IdMatriculaCabecera, int IdPEspecifico, int Grupo)
        {
            try
            {
                var _query = string.Empty;
                List<CriterioEvaluacionCursoDTO> listaCriteriosEvaluacionPorCurso = new List<CriterioEvaluacionCursoDTO>();
                _query = @"SELECT * FROM [pw].[V_PW_ReporteParametroEvaluacionNota] where IdMatriculaCabecera=@IdMatriculaCabecera and  IdPEspecifico=@IdPEspecifico";

                //V_ListadoActividadesCalificables_Completo
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    listaCriteriosEvaluacionPorCurso = JsonConvert.DeserializeObject<List<CriterioEvaluacionCursoDTO>>(respuesta);
                }
                return listaCriteriosEvaluacionPorCurso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: TEsquemaEvaluacionRepositorioAula
        /// Autor: Max Mantilla
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene información de los esquemas de evaluación para combo
        /// </summary>
        /// <returns> List<EsquemaEvaluacionComboDTO> </returns>  
        public List<EsquemaEvaluacionComboDTO> ObtenerComboEsquemaEvaluacion()
        {
            try
            {
                var _query = string.Empty;
                List<EsquemaEvaluacionComboDTO> Listado = new List<EsquemaEvaluacionComboDTO>();
                _query = "SELECT Id,Nombre FROM pla.T_EsquemaEvaluacion WHERE Estado=1";

                string respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    Listado = JsonConvert.DeserializeObject<List<EsquemaEvaluacionComboDTO>>(respuesta);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: TEsquemaEvaluacionRepositorioAula
        /// Autor: Max Mantilla
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene información de los criterios de evaluación por IdEsquemaEvaluacion
        /// </summary>
        /// <returns> List<EsquemaEvaluacionComboDTO> </returns>  
        public List<EsquemaCriterioEvaluacionDTO> ObtenerCriterioEvaluacionPorIdEsquema(int IdEsquemaEvaluacion)
        {
            try
            {
                var _query = string.Empty;
                List<EsquemaCriterioEvaluacionDTO> Listado = new List<EsquemaCriterioEvaluacionDTO>();
                _query = "SELECT IdEsquemaEvaluacion,EsquemaEvaluacion,IdCriterioEvaluacion,CriterioEvaluacion,Ponderacion FROM pla.V_ObtenerEsquemaCriterioEvaluacion WHERE IdEsquemaEvaluacion=@IdEsquemaEvaluacion";

                string respuesta = _dapperRepository.QueryDapper(_query, new { IdEsquemaEvaluacion = IdEsquemaEvaluacion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    Listado = JsonConvert.DeserializeObject<List<EsquemaCriterioEvaluacionDTO>>(respuesta);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 09/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo.
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_EsquemaEvaluacion
                            WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el esquema evaluacion predeterminado
        /// </summary>
        /// <param name=”idEsquemaEvaluacionPGeneral”>Identificador del esquema</param>
        /// <returns>bool</returns>
        public bool ModificarEsquemaEvaluacionPredefinido(int idEsquemaEvaluacionPGeneral)
        {
            try
            {
                var query = "ope.SP_ModificarEsquemaDeEvaluacionPredefinido";
                var resultado = _dapperRepository.QuerySPDapper(query, new { EsquemaPredeterminado = true, IdEsquemaEvaluacionPGeneral = idEsquemaEvaluacionPGeneral });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }










    }
}
