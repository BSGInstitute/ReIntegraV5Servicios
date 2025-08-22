using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Dapper;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{

    /// Repositorio: PostulanteRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/10/2022
    /// <summary>
    /// Gestión general de T_Postulante
    /// </summary>
    public class PostulanteRepository : GenericRepository<TPostulante>, IPostulanteRepository
    {
        private Mapper _mapper;

        public PostulanteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulante, Postulante>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base

        private TPostulante MapeoEntidad(Postulante entidad)
        {
            try
            {
                TPostulante modelo = _mapper.Map<TPostulante>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPostulante Add(Postulante entidad)
        {
            try
            {
                var Postulante = MapeoEntidad(entidad);
                base.Insert(Postulante);
                return Postulante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPostulante Update(Postulante entidad)
        {
            try
            {
                var postulante = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                postulante.RowVersion = entidadExistente.RowVersion;


                base.Update(postulante);
                return postulante;
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


        public IEnumerable<TPostulante> Add(IEnumerable<Postulante> listadoEntidad)
        {
            try
            {
                List<TPostulante> listado = new List<TPostulante>();
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

        public IEnumerable<TPostulante> Update(IEnumerable<Postulante> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulante> listado = new List<TPostulante>();
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


        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de Postulante por su Id
        /// </summary>
        /// <param name="idPostulante"> Id de Expositor </param>
        /// <returns> PostulanteDTO </returns>
        public Postulante? ObtenerPorId(int idPostulante)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id	,
	                    Nombre,
	                    ApellidoPaterno,
	                    ApellidoMaterno,
	                    NroDocumento,
	                    Telefono,
	                    Celular,
	                    Email,
	                    Telefono2,
	                    Celular2,
	                    Celular3,
	                    Email2,
	                    Email3,
	                    FechaNacimiento,
	                    IdPais,
	                    IdCiudad,
	                    IdTipoDocumento,
	                    IdSexo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    UrlPerfilFacebook,
	                    UrlPerfilLinkedin,
	                    EsProcesoAnterior,
	                    Edad,
	                    TieneHijo,
	                    CantidadHijo,
	                    IdConvocatoriaPersonal,
	                    IdPersonal_OperadorProceso,
	                    IdPaginaReclutadoraPersonal,
	                    IdPostulanteNivelPotencial
                    FROM gp.T_Postulante
	                WHERE Estado = 1 AND Id=@idPostulante";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPostulante });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<Postulante>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registro de postulantes por el nombre
        /// </summary>
        /// <param name="nombre"> Nombre de Postulante </param>
        /// <returns> Lista ComboDTO</returns>
        public IEnumerable<ComboDTO> ObtenerPostulanteFiltroAutocomplete(string nombre)
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM gp.V_TPostulante_ObtenerPostulanteParaFiltro WHERE Nombre LIKE @Nombre AND Estado = 1 ORDER BY Nombre ASC;";
                var resultado = _dapperRepository.QueryDapper(query, new { Nombre = $"%{nombre}%" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de postulante por filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<PostulanteUltimoProcesoSeleccionDTO> ObtenerPostulantesUltimoProcesoSeleccion(EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            try
            {
                List<PostulanteUltimoProcesoSeleccionDTO> postulanteAutocompleteFiltro = new List<PostulanteUltimoProcesoSeleccionDTO>();
                string query = "SELECT IdPostulante,Postulante,IdProcesoSeleccion,ProcesoSeleccion, VersionCentil FROM gp.V_PostulanteUltimoProcesoSeleccion_V02";

                if (filtro.FiltroPorPostulante == true)
                {
                    if (filtro.IdsPostulantes.Count() > 0)
                    {
                        query += " WHERE IdPostulante IN @IdsPostulantes";
                    }
                }
                else
                {
                    if (filtro.FechaInicio == null || filtro.FechaFin == null)
                    {
                        filtro.FechaInicio = new DateTime(1900, 12, 31);
                        filtro.FechaFin = DateTime.Now;
                    }
                    query += @" WHERE FechaExamen BETWEEN  @FechaInicio AND @FechaFin";
                    if (filtro.IdProcesoSeleccion != null && filtro.IdProcesoSeleccion != 0)
                    {
                        query += @" AND IdProcesoSeleccion = @IdProcesoSeleccion";
                    }
                }

                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    filtro.FechaInicio,
                    filtro.FechaFin,
                    filtro.IdsPostulantes,
                    filtro.IdProcesoSeleccion
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    postulanteAutocompleteFiltro = JsonConvert.DeserializeObject<List<PostulanteUltimoProcesoSeleccionDTO>>(resultado)!;
                }
                return postulanteAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la clasificacion NEO PIR por postulante
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<PostulanteClasificacionNeoDTO> ObtenerPostulantesUltimoProcesoSeleccion(List<int> idsPostulantes)
        {
            try
            {
                var rpta = new List<PostulanteClasificacionNeoDTO>();
                var query = $"SELECT IdProcesoSeleccion, IdPostulante, RespuestaAlAzar, AquiescenciaAq, NegacionesNe FROM gp.V_ClasificacionNEO WHERE IdPostulante IN @IdsPostulantes";
                var resultado = _dapperRepository.QueryDapper(query, new { IdsPostulantes = idsPostulantes });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PostulanteClasificacionNeoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ObtenerIdMatriculaPorPostulante(int idPostulante)
        {
            try
            {
                string query = @"
                        SELECT
	                        MC.Id AS Valor
                        FROM
	                        gp.T_Postulante AS P
                        INNER JOIN mkt.T_Alumno AS AL ON P.Email = AL.Email1
	                        AND AL.Estado = 1
                        INNER JOIN fin.T_MatriculaCabecera AS MC ON MC.IdAlumno = AL.Id
                        WHERE
	                        P.Id = @IdPostulante
	                        AND MC.Estado = 1
                        ORDER BY AL.Id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor!.Value;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ReportePostulanteMatriculaDTO> ObtenerNotasMatriculaReporte(List<int> idsPostulantes)
        {
            try
            {

                List<ReportePostulanteMatriculaDTO> rpta = new List<ReportePostulanteMatriculaDTO>();
                string query = @"
                    SELECT IdPostulante,
	                    NombrePostulante,
	                    NombreProgramaEspecifico,
	                    ProgramaGeneral,
	                    Usuario,
	                    ValorEscala 
                    FROM gp.V_ObtenerCalificacionesPostulanteCurso
                    WHERE IdPostulante IN @IdsPostulante";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdsPostulante = idsPostulantes,
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReportePostulanteMatriculaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool RestablecerNotas(EnvioDatosReestablecerDTO dto, string usuario)
        {
            try
            {
                var query = "gp.SP_PW_ActualizarCursoCapacitacionMantenimientoPorIdPostulante";
                var queryRespuesta = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    dto.IdPostulante,
                    IdPGeneral = dto.IdProgramaGeneral,
                    UsuarioModificacion = usuario
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && queryRespuesta != "null")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int? ObtenerIdPostulanteProcesoSeleccionPorIdPostulante(int IdPostulante)
        {
            try
            {
                var resultado = _dapperRepository.FirstOrDefault("SELECT Id AS Valor FROM gp.T_PostulanteProcesoSeleccion WHERE IdPostulante = @IdPostulante AND Estado = 1", new { IdPostulante });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor!.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
		/// Obtiene datos del postulante junto a su token
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO? ObtenerPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
        {
            try
            {
                var query = "SELECT Id, IdPostulante, Postulante, Dni, Email, ProcesoSeleccion, Token, GuidAccess FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerPostulanteProceso] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PostulanteRepositorio
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 05/03/2024
        /// <summary>
        /// Obtiene el GrupoPregunta respondido por el alumno según su Id de Registro
        /// </summary>
        /// <returns>ValorStringDTO</returns>
        public DatosMatriculaPostulanteDTO? ObtenerDatosMatriculaIdPostulante(int idPostulante)
        {
            try
            {
                var query = "Select IdPostulante,IdAlumno,Usuario,Email,Contraseña,idPEspecifico,IdMatriculaCabecera,CodigoMatricula,NombrePostulante,ApellidoPostulante from gp.V_PW_ObtenerAlumnoUsuario where IdPostulante = @idPostulante";
                var respuesta = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                {
                    return JsonConvert.DeserializeObject<DatosMatriculaPostulanteDTO>(respuesta)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
        /// </summary>
        /// <param name="idPostulante"></param>
        /// <returns></returns>
        public ProcesoSeleccionInscritoDTO? ObtenerProcesoSeleccionInscrito(int idPostulanteProcesoSeleccion)
        {
            try
            {
                var query = "SELECT Id, IdPostulante, Postulante, IdProcesoSeleccion, ProcesoSeleccion, IdPuestoTrabajo, PuestoTrabajo, IdSede, Sede, FechaRegistro FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerProcesoSeleccionados] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1 AND Activo = 1 ORDER BY FechaRegistro DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<ProcesoSeleccionInscritoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 22/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de Postulantes 
        /// </summary>
        /// <param name="paginador">Paginador</param>
        /// <returns>Retorna datos del postulante en bloques para paginación</returns>
        public ResultadoDatosPostulanteDTO ObtenerPostulantesInscritos(PaginadorDTO paginador)
        {
            try
            {
                var traerCantidad = new
                {
                    Skip = paginador.skip,
                    Take = paginador.take,
                    Cantidad = true,
                };
                var traerDatos = new
                {
                    Skip = paginador.skip,
                    Take = paginador.take,
                    Cantidad = false,
                };

                ResultadoDatosPostulanteDTO rpta = new ResultadoDatosPostulanteDTO();

                //var query = @"SELECT *  FROM gp.V_TPostulante_ObtenerDatosPostulante3";
                string queryPostulante = "[gp].[SP_ObtenerDatosPostulante]";
                var ListaDatosPostulante = _dapperRepository.QuerySPDapper(queryPostulante, traerDatos);
                var TotaFilasPostulante = _dapperRepository.QuerySPFirstOrDefault(queryPostulante, traerCantidad);


                var respuesta = JsonConvert.DeserializeObject<IEnumerable<DatosPostulanteDTO>>(ListaDatosPostulante);
                var total = JsonConvert.DeserializeObject<TotalDatosPostulanteDTO>(TotaFilasPostulante);

                rpta.data = respuesta;
                rpta.Total = total.TotalFilas;
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PostulanteInformacionProcesoDTO> ObtenerPostulantesInscritosConProcesos()
        {
            try
            {
                List<PostulanteInformacionProcesoDTO> postulanteFiltro = new List<PostulanteInformacionProcesoDTO>();
                var query = @"SELECT IdPostulante,
                               NombrePostulante,
                               ApellidoPaterno,
                               ApellidoMaterno,
                               NroDocumento,
                               Email,
                               IdProcesoSeleccion,
                               ProcesoSeleccion,
                               FechaProcesoSeleccion,
                               NombrePersonal
                        FROM gp.V_ObtenerDatosPostulante
                        ORDER BY FechaProcesoSeleccion DESC";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    postulanteFiltro = JsonConvert.DeserializeObject<List<PostulanteInformacionProcesoDTO>>(res);
                }
                return postulanteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PostulanteProcesoEvaluacionesDTO> ObtenerPostulantesInscritosConProcesosExamenes(PostulanteProcesoDTO DataPostulante)
        {
            try
            {
                List<PostulanteProcesoEvaluacionesDTO> resultado = new List<PostulanteProcesoEvaluacionesDTO>();
                var query = @"
                    SELECT E.IdExamenTest  AS IdEvaluacion,
                           ET.Nombre       AS NombreEvaluacion,
                           EA.IdExamen     AS IdExamen,
                           E.Nombre        AS NombreExamen,
                           EA.EstadoExamen AS EstadoExamen
                    FROM gp.T_ExamenAsignado EA
                             INNER JOIN gp.T_ProcesoSeleccion PS ON EA.IdProcesoSeleccion = PS.Id
                             INNER JOIN gp.T_Examen E ON EA.IdExamen = E.Id
                        AND EA.Estado = 1
                             LEFT JOIN gp.T_ExamenTest ET ON ET.Id = E.IdExamenTest
                        AND ET.Estado = 1
                             LEFT JOIN gp.T_ConfiguracionAsignacionEvaluacion CAE ON ET.Id = CAE.IdEvaluacion
                        AND CAE.IdProcesoSeleccion = PS.Id AND CAE.Estado = 1
                    WHERE EA.IdPostulante = @IdPostulante
                      AND EA.IdProcesoSeleccion = @IdProcesoSeleccion
                    ORDER BY EA.FechaModificacion DESC";
                var res = _dapperRepository.QueryDapper(query, new
                {
                    IdPostulante = DataPostulante.IdPostulante,
                    IdProcesoSeleccion = DataPostulante.IdProcesoSeleccion
                });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<PostulanteProcesoEvaluacionesDTO>>(res);
                }
                return resultado;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 28/10/2024
        /// Version: 2.0
        /// <summary>
        /// Obtener información por filtro manual en la tabla de Postulantes
        /// </summary>
        /// <param name="datosPostulanteDTO"> filtro de búsqueda </param>
        /// <param name="filtroKendoGridDTO"> filtros de grilla </param>
        /// <param name="listaOperadores"> Operadores de comparación </param>
        /// <returns> Lista de registros filtrados : ResultadoDatosPostulanteDTO </returns>
        public ResultadoDatosPostulanteDTO ObtenerFiltroDatosPostulanteManual(
            DatosPostulanteDTO datosPostulanteDTO,
            FiltroKendoGridDTO filtroKendoGridDTO,
            IEnumerable<OperadorComparacionDTO> listaOperadores)
        {
            ResultadoDatosPostulanteDTO rpta = new ResultadoDatosPostulanteDTO();
            var operadoresComparacionDTO = new List<OperadorComparacionDTO>(listaOperadores);
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM [gp].[V_TPostulante_ObtenerDatosPostulanteV5] WHERE 1=1");

            var parametros = new DynamicParameters();


            if (filtroKendoGridDTO?.Filter != null && filtroKendoGridDTO.Filter.Filters.Any())
            {
                foreach (var item in filtroKendoGridDTO.Filter.Filters)
                {

                    switch (item.Operator.ToLower())
                    {
                        case "contains":
                            sqlBuilder.AppendLine($"AND {item.Field} LIKE @{item.Field}");
                            parametros.Add($"@{item.Field}", $"%{item.Value}%");
                            break;


                        case "endswith":
                            sqlBuilder.AppendLine($"AND {item.Field} LIKE @{item.Field}");
                            parametros.Add($"@{item.Field}", $"%{item.Value}");
                            break;


                        case "startswith":
                            sqlBuilder.AppendLine($"AND {item.Field} LIKE @{item.Field}");
                            parametros.Add($"@{item.Field}", $"{item.Value}%");
                            break;

                        default:
                            break;
                    }


                    //sqlBuilder.AppendLine($"AND {item.Field} LIKE @{item.Field}");
                    //parametros.Add($"@{item.Field}", $"%{item.Value}%");
                }
            }

            //Logica para que funcione el Sort
            if (filtroKendoGridDTO.Sort != null && filtroKendoGridDTO.Sort.Any())
            {
                sqlBuilder.AppendLine(" ORDER BY");
                sqlBuilder.AppendLine(string.Join(", ", filtroKendoGridDTO.Sort.Select(s => $"{s.Field} {(s.Dir ?? "asc")}")));
            }
            else
            {
                sqlBuilder.AppendLine(" ORDER BY FechaCreacion DESC");
            }

            sqlBuilder.AppendLine(" OFFSET @Skip ROWS");
            sqlBuilder.AppendLine(" FETCH NEXT @Take ROWS ONLY");
            parametros.Add("@Skip", filtroKendoGridDTO.Skip);
            parametros.Add("@Take", filtroKendoGridDTO.Take);

            if (filtroKendoGridDTO != null && filtroKendoGridDTO.Take != 0)
            {
                var datosPostulante = _dapperRepository.QueryDapper(sqlBuilder.ToString(), parametros);

                string queryTotal = "SELECT COUNT(*) as TotalFilas FROM [gp].[V_TPostulante_ObtenerDatosPostulanteV5]";
                var totalCount = _dapperRepository.FirstOrDefault(queryTotal, null);

                var respuesta = JsonConvert.DeserializeObject<IEnumerable<DatosPostulanteDTO>>(datosPostulante);
                var total = JsonConvert.DeserializeObject<TotalDatosPostulanteDTO>(totalCount);

                rpta.data = respuesta;
                rpta.Total = total.TotalFilas;
                //rpta.filtrosUsado = filtroKendoGridDTO.Filter.Filters;
                return rpta;
            }
            else
            {

                var datosPostulante = _dapperRepository.QueryDapper(sqlBuilder.ToString(), parametros);

                string queryTotal = "SELECT COUNT(*) as TotalFilas FROM [gp].[V_TPostulante_ObtenerDatosPostulanteV5]";
                var totalCount = _dapperRepository.FirstOrDefault(queryTotal, null);

                var respuesta = JsonConvert.DeserializeObject<IEnumerable<DatosPostulanteDTO>>(datosPostulante);
                var total = JsonConvert.DeserializeObject<TotalDatosPostulanteDTO>(totalCount);

                rpta.data = respuesta;
                rpta.Total = total.TotalFilas;
                //rpta.filtrosUsado = filtroKendoGridDTO.Filter.Filters;
                return rpta;
            }


        }



        /// Autor: Eliot Arias F.
        /// Fecha: 02/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de postulante si existe a travez de su email
        /// </summary>
        /// <param name="email"> email, para validar la existencia</param>
        /// <returns> true si existe, si no, retorna false</returns>
        public Boolean ObtenerPostulantePorEmail(string email)
        {
            try
            {
                PostulanteDTO postulanteValidar = new PostulanteDTO();
                var query = "Select * from gp.T_Postulante where Email = @EmailPostulante";
                var respuesta = _dapperRepository.FirstOrDefault(query, new { EmailPostulante = email });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                {

                    return true;
                }

                return false;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de elementos registrados para combo
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public IEnumerable<ResultadoFinalTextoDTO> ValidarCorreoPostulante(int IdPostulante)
        {
            try
            {
                List<ResultadoFinalTextoDTO> lista = new List<ResultadoFinalTextoDTO>();
                string query = "gp.SP_ValidarCorreoPostulante";
                var res = _dapperRepository.QuerySPDapper(query, new { IdPostulante = IdPostulante });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ResultadoFinalTextoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Alumno desde IdPostulante sin Matricula
        /// </summary>
        /// <returns> bool </returns>
        public PostulanteAlumnoDTO ObtenerIdAlumnoDesdeidPostulanteSinMatricula(int IdPostulante)
        {
            try
            {
                PostulanteAlumnoDTO valorStringDTO = new PostulanteAlumnoDTO();
                var query = "gp.SP_PW_ObtenerAlumnoPostulante";
                var queryRespuesta = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    IdPostulante = IdPostulante,

                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]") && queryRespuesta != "null")
                {
                    valorStringDTO = JsonConvert.DeserializeObject<PostulanteAlumnoDTO>(queryRespuesta);
                }
                return valorStringDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// Autor: Eliot Arias F.
		/// Fecha: 11/11/2024
		/// Version: 1.0
		/// <summary>
		/// Crea al alumno desde el idPostulante
		/// </summary>
		/// <returns> ResultadoFinalTextoDTO </returns>
		public ResultadoFinalTextoDTO CreacionAlumnoDesdePostulante(int idPostulante, string Usuario)
        {
            try
            {
                ResultadoFinalTextoDTO valorStringDTO = new ResultadoFinalTextoDTO();
                var query = "gp.SP_InsertarAlumnoDesdePostulante";
                var queryRespuesta = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    IdPostulante = idPostulante,
                    Usuario = Usuario
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]") && queryRespuesta != "null")
                {
                    valorStringDTO = JsonConvert.DeserializeObject<ResultadoFinalTextoDTO>(queryRespuesta);
                }
                return valorStringDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor: Eliot Arias F.
		/// Fecha: 11/11/2024
		/// Version: 1.0
		/// <summary>
		/// Obtiene la el id Matricula desde el id Alumno
		/// </summary>
		/// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerMatriculaconIdAlumno(int IdAlumno)
        {
            try
            {
                ValorIntDTO matricula = new ValorIntDTO();
                var resultado = _dapperRepository.FirstOrDefault("SELECT Id AS Valor FROM fin.T_MatriculaCabecera WHERE IdAlumno = @IdAlumno", new { IdAlumno });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    matricula = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }

                return matricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F.
		/// Fecha: 12/11/2024
		/// Version: 1.0
		/// <summary>
		/// Crea un usuario en el portal web a partir de los datos del postulante
		/// </summary>
		/// <returns> ValorIntDTO </returns>
        public CuentaPortalDevuelvePostulanteDTO CreacionCuentaPortaldePostulante(int idAlumno, string Email, string NombrePostulante, string ApellidoPostulante, int? IdPais, int? IdCiudad, string Celular)
        {
            try
            {
                var clave = GenerarContraseñaAleatoria();

                CuentaPortalDevuelvePostulanteDTO cuenta = new CuentaPortalDevuelvePostulanteDTO();
                var query = "conf.SP_CreateUsuarioClavePortalWeb";
                var queryRespuesta = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    IdAlumno = idAlumno,
                    Email = Email,
                    Clave = clave,
                    ClaveEncriptada = "AMG+LXfHdXZGSJEJpqAtKRrumAmq9j9sOtU0g2jkLjIJoiGFbAhsRYW/385elX0Djg==",
                    Nombres = NombrePostulante,
                    Apellidos = ApellidoPostulante,
                    Fijo = "000000000000",
                    Celular = Celular,
                    CodigoPais = IdPais,
                    CodigoCiudad = IdCiudad,
                    Fecha = DateTime.Now,
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]") && queryRespuesta != "null")
                {
                    cuenta = JsonConvert.DeserializeObject<CuentaPortalDevuelvePostulanteDTO>(queryRespuesta);
                }
                return cuenta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GenerarContraseñaAleatoria()
        {

            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Random random = new Random();


            string contraseña = new string(Enumerable.Repeat(caracteres, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return contraseña;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 03/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de examenes comparados entre 2 procesos de selección
        /// </summary>
        /// <returns> List<ComparacionProcesosSeleccionDTO> </returns>
        public List<ComparacionProcesosSeleccionDTO> CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino)
        {
            try
            {
                List<ComparacionProcesosSeleccionDTO> lista = new List<ComparacionProcesosSeleccionDTO>();
                string query = "gp.SP_CompararProcesosSeleccion";
                var res = _dapperRepository.QuerySPDapper(query, new { IdPostulante = IdPostulante, IdProcesoSeleccionOrigen = ProcesoOrigen, IdProcesoSeleccionDestino = ProcesoDestino });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ComparacionProcesosSeleccionDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza el cambio de proceso para un postulante comparando la nota anterior
        /// </summary>
        /// <returns> List<ComparacionProcesosSeleccionDTO> </returns>
        public List<ComparacionProcesosSeleccionDTO> ActualizarProcesoPostulante(PostulanteProcesoNuevoDTO Informacion)
        {
            try
            {
                List<ComparacionProcesosSeleccionDTO> lista = new List<ComparacionProcesosSeleccionDTO>();
                var examenes = string.Join(",", Informacion.IdsExamenAsignado.Select(x => x));
                string query = "gp.SP_CambioProcesoSeleccionPostulanteConNotas";
                var res = _dapperRepository.QuerySPDapper(query, new { IdPostulante = Informacion.IdPostulante, IdProcesoSeleccionOrigen = Informacion.IdProcesoSeleccionOrigen, IdProcesoSeleccionDestino = Informacion.IdProcesoSeleccionDestino, IdsExamenAsignado = examenes, NombreUsuario = Informacion.Usuario, IdPersonal = Informacion.IdPersonal });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ComparacionProcesosSeleccionDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza el cambio de proceso para un postulante sin Nota
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public List<ComparacionProcesosSeleccionDTO> ActualizarProcesoPostulanteSinNota(PostulanteProcesoNuevoDTO Informacion)
        {
            try
            {
                List<ComparacionProcesosSeleccionDTO> lista = new List<ComparacionProcesosSeleccionDTO>();
                string query = "gp.SP_CambiarProcesoSeleccionPostulanteSinNotaV5";
                var res = _dapperRepository.QuerySPDapper(query, new { IdPostulante = Informacion.IdPostulante, IdProcesoSeleccionDestino = Informacion.IdProcesoSeleccionDestino, IdPersonal = Informacion.IdPersonal, NombreUsuario = Informacion.Usuario });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ComparacionProcesosSeleccionDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
