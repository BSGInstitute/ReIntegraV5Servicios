using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PEspecificoSesionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_PEspecificoSesion
    /// </summary>
    public class PEspecificoSesionRepository : GenericRepository<TPespecificoSesion>, IPEspecificoSesionRepository
    {
        private Mapper _mapper;

        public PEspecificoSesionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoSesion, PEspecificoSesion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoSesion MapeoEntidad(PEspecificoSesion entidad)
        {
            try
            {
                TPespecificoSesion modelo = _mapper.Map<TPespecificoSesion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoSesion Add(PEspecificoSesion entidad)
        {
            try
            {
                var pEspecificoSesion = MapeoEntidad(entidad);
                base.Insert(pEspecificoSesion);
                return pEspecificoSesion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoSesion Update(PEspecificoSesion entidad)
        {
            try
            {
                var pEspecificoSesion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                pEspecificoSesion.RowVersion = entidadExistente.RowVersion;

                base.Update(pEspecificoSesion);
                return pEspecificoSesion;
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


        public IEnumerable<TPespecificoSesion> Add(IEnumerable<PEspecificoSesion> listadoEntidad)
        {
            try
            {
                List<TPespecificoSesion> listado = new List<TPespecificoSesion>();
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

        public IEnumerable<TPespecificoSesion> Update(IEnumerable<PEspecificoSesion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoSesion> listado = new List<TPespecificoSesion>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla PEspecificoSesion mediante le Id
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada de la URL de ubicaciond e la ciudad</returns>
        public PEspecificoSesion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT
	                    Id,
	                    IdPEspecifico AS IdPespecifico,
	                    FechaHoraInicio,
	                    Duracion,
	                    IdExpositor,
	                    Comentario,
	                    SesionAutoGenerada,
	                    IdAmbiente,
	                    Predeterminado,
	                    Grupo,
	                    EsSesionInicio,
	                    Version,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    GrupoSesion,
	                    IdSesionRA,
	                    IdProveedor,
	                    UrlWebex,
	                    CuentaWebex,
	                    IdModalidadCurso,
	                    FechaCancelacionWebinar,
	                    ComentarioCancelacionWebinar,
	                    EsWebinarConfirmado,
	                    MostrarPortalWeb,
	                    IdEstadoEnvioCorreo,
	                    EnvioSesionCorreo,
	                    EnvioSesionCorreoRegularizacion,
	                    FechaHoraRegularizacion,
	                    EnvioAutomaticoCorreoWebinar,
	                    EnvioAutomaticoWhatsAppWebinar,
	                    RegularizacionCorreoWebinar,
	                    RegularizacionWhatsAppWebinar,
	                    UsuarioEnvioCorreoWebinar,
	                    UsuarioEnvioWhatsAppWebinar,
	                    FechaRegularizacionCorreoWebinar,
	                    FechaRegularizacionWhatsAppWebinar
                    FROM pla.T_PEspecificoSesion WHERE Estado=1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<PEspecificoSesion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla PEspecificoSesion mediante le Id
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada de la URL de ubicaciond e la ciudad</returns>
        public IEnumerable<PEspecificoSesion> ObtenerPorIds(IEnumerable<int> ids)
        {
            try
            {
                var query = @"SELECT
	                    Id,
	                    IdPEspecifico AS IdPespecifico,
	                    FechaHoraInicio,
	                    Duracion,
	                    IdExpositor,
	                    Comentario,
	                    SesionAutoGenerada,
	                    IdAmbiente,
	                    Predeterminado,
	                    Grupo,
	                    EsSesionInicio,
	                    Version,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    GrupoSesion,
	                    IdSesionRA,
	                    IdProveedor,
	                    UrlWebex,
	                    CuentaWebex,
	                    IdModalidadCurso,
	                    FechaCancelacionWebinar,
	                    ComentarioCancelacionWebinar,
	                    EsWebinarConfirmado,
	                    MostrarPortalWeb,
	                    IdEstadoEnvioCorreo,
	                    EnvioSesionCorreo,
	                    EnvioSesionCorreoRegularizacion,
	                    FechaHoraRegularizacion,
	                    EnvioAutomaticoCorreoWebinar,
	                    EnvioAutomaticoWhatsAppWebinar,
	                    RegularizacionCorreoWebinar,
	                    RegularizacionWhatsAppWebinar,
	                    UsuarioEnvioCorreoWebinar,
	                    UsuarioEnvioWhatsAppWebinar,
	                    FechaRegularizacionCorreoWebinar,
	                    FechaRegularizacionWhatsAppWebinar
                    FROM pla.T_PEspecificoSesion WHERE Estado=1 AND Id IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoSesion>>(resultado)!;
                }
                return new List<PEspecificoSesion>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la sede en formato de html
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada de la URL de ubicaciond e la ciudad</returns>
        public string ObtenerUrlUbicacionCiudad(int id)
        {
            try
            {
                var urlUbicacion = "";
                var resultadoFinal = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerIdCiudadDictadoClasesPorPEspecificoSesion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }

                var IdCiudad = resultadoFinal.Valor;

                if (IdCiudad == 4)
                {
                    urlUbicacion = "https://www.google.com/maps/place/BSG+Institute/@-16.391617,-71.549994,17z/data=!4m5!3m4!1s0x0:0xfa338199798a589d!8m2!3d-16.3916171!4d-71.5499941?hl=en";
                }
                else if (IdCiudad == 14)
                {
                    urlUbicacion = "https://www.google.com/maps/place/BSG+Institute/@-12.118881,-77.035406,15z/data=!4m5!3m4!1s0x0:0x44216f10931e46b7!8m2!3d-12.1188811!4d-77.0354064?hl=en";
                }
                else if (IdCiudad == 1956)
                {
                    urlUbicacion = "https://www.google.com/maps/search/Av+Marcelo+Terceros+B%C3%A1nzer+304,+Santa+Cruz+de+la+Sierra,+Santa+Cruz,+Bolivia/@-17.763943,-63.197579,15z?hl=en";
                }
                else if (IdCiudad == 2066)
                {
                    urlUbicacion = "https://www.google.com/maps/search/Cra.+45+%23%23108-27+Bogot%C3%A1,+Cundinamarca+Colombia/@4.696379,-74.056552,17z?hl=en";
                }

                return urlUbicacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la direccion de dictado de clases por sesión
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada del PEspecificoSesion</returns>
        public string ObtenerDireccionDictadoClases(int id)
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = $@"ope.SP_ObtenerDireccionDictadoClasesPorPEspecificoSesion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la direccion de dictado de clases por sesión
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada con el nombre de la ciudad donde se lleva a cabo el dictado de la sesion</returns>
        public string ObtenerNombreCiudadDictadoClases(int id)
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = $@"ope.SP_ObtenerNombreCiudadDictadoClasesPorPEspecificoSesion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del docente que dicta la clase
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada con el nombre del docente encargado del dictado de clases</returns>
        public string ObtenerNombreDocenteDictadoClases(int id)
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = $@"ope.SP_ObtenerNombreDocentePorPEspecificoSesion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecificoSesion = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Lista los horarios de los cursos de Webex
        /// </summary>
        /// <param name="id">Id del pespecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena con la lista de cursos de Webex</returns>
        public string ObtenerHorarioSemanaSesionWebex(int idPespecifico)
        {
            try
            {
                List<PEspecificoProximaSesionWebexDTO> listado = new List<PEspecificoProximaSesionWebexDTO>();
                string htmlFinal = string.Empty;
                var query = "pla.SP_ObtenerSesionProximoPorPespecifico";
                var res = _dapperRepository.QuerySPDapper(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PEspecificoProximaSesionWebexDTO>>(res)!;
                    listado = listado.OrderBy(w => w.FechaInicio).ToList();
                    htmlFinal += $@"";
                    DateTime? ultimaFecha = null;
                    foreach (var item in listado)
                    {
                        if (ultimaFecha == null)
                        {
                            ultimaFecha = item.FechaInicio;
                        }
                        if ((ultimaFecha.Value.Date - item.FechaInicio.Date).Days < 2)
                        {
                            ultimaFecha = item.FechaInicio;
                            htmlFinal += $@"<strong>{item.NombreDia} {item.FechaInicio.ToString("dd/MM/yyyy")}</strong>
                                        <ul>
                                        
                                          <li><strong>Hora de inicio:</strong> {item.FechaInicio.ToString("hh:mm tt")}</li>                                         
                                          <li><strong>Hora de término:</strong> {item.FechaFin.ToString("hh:mm tt")}</li>
                                     </ul>
                                     ";
                        }
                    }
                    htmlFinal += $@"";
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 09/02/2023
        /// Version: 2.0
        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico y idMatriculaCabecera
        /// </summary>
        /// <param name="idPespecifico" name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionRecuperacionDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionRecuperacionDTO>();
                var obtenerSesionPorPEspecificoDB = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerSesionesPorPEspecifico]", new { idPespecifico, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionRecuperacionDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecificoPadre">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioPorIdsPEspecificoPadre(List<int> idsPEspecificoPadre)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                                    IdPEspecifico,
                                    FechaHoraInicio
                                FROM pla.V_ListaFechaInicioPEspecificoPadrePEspecificoHijoPorIdPadre
                                WHERE PEspecificoPadreId IN @idsPEspecificoPadre";
                string repuesta = _dapperRepository.QueryDapper(query, new { idsPEspecificoPadre });
                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(repuesta);
                }
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecificoPadre">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public async Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioPorIdsPEspecificoPadreAsync(List<int> idsPEspecificoPadre)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                            IdPEspecifico,
                            FechaHoraInicio
                        FROM pla.V_ListaFechaInicioPEspecificoPadrePEspecificoHijoPorIdPadre
                        WHERE PEspecificoPadreId IN @idsPEspecificoPadre";

                string respuesta = await _dapperRepository.QueryDapperAsync(query, new { idsPEspecificoPadre });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(respuesta) ?? new List<PEspecificoSesionFechaHoraInicioDTO>();
                }

                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecifico">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioPorIdsPEspecifico(List<int> idsPEspecifico)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                                    IdPEspecifico,
                                    FechaHoraInicio
                                FROM pla.V_ListaFechaInicioPEspecificoSesionPorIdPEspecifico
                                WHERE IdPEspecifico IN @idsPEspecifico";
                string repuesta = _dapperRepository.QueryDapper(query, new { idsPEspecifico });
                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(repuesta);
                }
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecifico">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public async Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioPorIdsPEspecificoAsync(List<int> idsPEspecifico)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                            IdPEspecifico,
                            FechaHoraInicio
                        FROM pla.V_ListaFechaInicioPEspecificoSesionPorIdPEspecifico
                        WHERE IdPEspecifico IN @idsPEspecifico";

                string respuesta = await _dapperRepository.QueryDapperAsync(query, new { idsPEspecifico });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(respuesta) ?? new List<PEspecificoSesionFechaHoraInicioDTO>();
                }

                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecifico">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioSinSesionPorIdsPEspecifico(List<int> idsPEspecifico)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                                    IdPEspecifico,
                                    FechaHoraInicio
                                FROM pla.V_ListaFechaInicioPEspecificoSesionSinInicioPorIdPEspecifico
                                WHERE Orden=1 AND IdPEspecifico IN @idsPEspecifico";
                string repuesta = _dapperRepository.QueryDapper(query, new { idsPEspecifico });
                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(repuesta)!;
                }
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecifico">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public async Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioSinSesionPorIdsPEspecificoAsync(List<int> idsPEspecifico)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                            IdPEspecifico,
                            FechaHoraInicio
                        FROM pla.V_ListaFechaInicioPEspecificoSesionSinInicioPorIdPEspecifico
                        WHERE Orden=1 AND IdPEspecifico IN @idsPEspecifico";

                string respuesta = await _dapperRepository.QueryDapperAsync(query, new { idsPEspecifico });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(respuesta) ?? new List<PEspecificoSesionFechaHoraInicioDTO>();
                }

                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecifico">Lista de Id PEspecifico</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public IEnumerable<InformacionProgramaEspecificoSesionDTO> ObtenerInformacionProgramaEspecificoSesion(int idPespecifico)
        {
            try
            {
                string query = @"
                    SELECT
	                    Id,
	                    FechaHoraInicio,
	                    Duracion,
	                    IdExpositor,
	                    IdProveedor,
	                    Comentario,
	                    IdAmbiente
                    FROM pla.V_ListaPespecificoSesionAlterno
                    WHERE
	                    Estado = 1
	                    AND IdPEspecifico = @idPespecifico;";
                string repuesta = _dapperRepository.QueryDapper(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(repuesta) && repuesta != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<InformacionProgramaEspecificoSesionDTO>>(repuesta)!;
                }
                return new List<InformacionProgramaEspecificoSesionDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerInformacionProgramaEspecificoSesion(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha hora inicio de Pespecifico Sesion
        /// </summary>
        /// <param name="idsPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="grupo">Grupo</param>
        /// <returns> List<PEspecificoSesionFechaHoraInicioDTO> </returns>
        public IEnumerable<PEspecificoCronogramaGrupalDTO> ObtenerSesionesPorPEspecificoGrupo(int idPespecifico, int grupo)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_ObtenerSesionesPorGrupoPorPEspecifico", new { idPespecifico, grupo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoCronogramaGrupalDTO>>(resultado)!;
                }
                return new List<PEspecificoCronogramaGrupalDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSesionesPorPEspecificoGrupo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Devulve todas las sesiones del grupo segun el id pespecifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> Grupo Sesion</param>
        /// <returns>Objeto</returns> 
        public List<PEspecificoSesionGrupoAnteriorDTO> ObtenerSesionesPorPEspecificoGrupoAnterior(int idPespecifico, int grupo)
        {
            try
            {
                List<PEspecificoSesionGrupoAnteriorDTO> rpta = new List<PEspecificoSesionGrupoAnteriorDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_ObtenerSesionesPorGrupoPorPEspecifico", new { idPespecifico, grupo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionGrupoAnteriorDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// <summary>
        /// Genera un dto compuesto para procesar pdf
        /// </summary>
        /// <param name="programaEspecifico"></param>
        /// <returns></returns>
        public IEnumerable<PespecificoSesionCompuestoDTO> ObtenerCronogramaIndividualPorPEspecifico(DatosProgramaEspecificoDTO programaEspecifico)
        {
            try
            {
                IEnumerable<InformacionProgramaEspecificoSesionDTO> rpta = new List<InformacionProgramaEspecificoSesionDTO>();
                string query = "SELECT Id, FechaHoraInicio, Duracion, IdExpositor, IdProveedor, Comentario, IdAmbiente FROM pla.V_ListaPespecificoSesion WHERE Estado=1 AND IdPEspecifico=@IdPespecifico;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = programaEspecifico.Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<InformacionProgramaEspecificoSesionDTO>>(resultado)!;
                }
                var sesiones = rpta.Select(x => new PespecificoSesionCompuestoDTO
                {
                    Id = x.Id,
                    FechaHoraInicio = x.FechaHoraInicio,
                    Duracion = x.Duracion,
                    DuracionTotal = string.IsNullOrEmpty(programaEspecifico.Duracion) ? 0 : Convert.ToDecimal(programaEspecifico.Duracion),
                    Curso = programaEspecifico.Nombre,
                    IdExpositor = x.IdExpositor,
                    IdProveedor = x.IdProveedor,
                    IdAmbiente = x.IdAmbiente,
                    IdCiudad = programaEspecifico.IdCiudad,
                    PEspecificoHijoId = programaEspecifico.Id,
                    Tipo = programaEspecifico.Tipo,
                    Comentario = x.Comentario,
                    EsSesionInicial = x.Id == programaEspecifico.IdSesion_Inicio,
                    MostrarPDF = true
                });
                return sesiones;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PES-OCIPE-001@Error en ObtenerCronogramaIndividualPorPEspecifico(): {ex.Message}", ex);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
		/// Valida los cruces entre fechas y docentes en los cronogramas
		/// </summary>
		/// <param name="objeto"></param>
		/// <returns> Lista DTO - IEnumerable<CruceSesionPEspecificoDTO> </returns>
		public IEnumerable<CruceSesionPEspecificoDTO> ValidarCrucesSesiones(InformacionCronogramaSesionesDTO dto, double duracion)
        {
            try
            {
                var resultado = string.Empty;
                string query = @"SELECT DISTINCT IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor,IdProveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor FROM pla.V_ObtenerInformacionSesionesPEspecifico 
                WHERE 
                    (@Fecha BETWEEN FechaHoraInicio AND FechaFin 
                        OR @FechaFin BETWEEN FechaHoraInicio AND FechaFin) 
                    AND EstadoPEspecifico != 'Cancelado' AND EstadoPEspecifico != 'Concluido' AND Estado = 1 AND Id != @Id";

                if (dto.IdAmbiente != null && dto.IdProveedor == null)
                {
                    query = $"{query} AND IdAmbiente = @IdAmbiente  ";
                    resultado = _dapperRepository.QueryDapper(query, new { Fecha = dto.FechaHoraInicio, FechaFin = dto.FechaHoraInicio.AddHours(duracion), dto.IdAmbiente, dto.Id });
                }
                else if (dto.IdProveedor != null && dto.IdAmbiente == null)
                {
                    query = $"{query} AND IdProveedor = @IdProveedor";
                    resultado = _dapperRepository.QueryDapper(query, new { Fecha = dto.FechaHoraInicio, FechaFin = dto.FechaHoraInicio.AddHours(duracion), dto.IdProveedor, dto.Id });
                }
                else
                {
                    query = $"{query} AND (IdAmbiente = @IdAmbiente OR IdProveedor = @IdProveedor)";
                    resultado = _dapperRepository.QueryDapper(query, new { Fecha = dto.FechaHoraInicio, FechaFin = dto.FechaHoraInicio.AddHours(duracion), dto.IdAmbiente, dto.IdProveedor, dto.Id });
                }
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CruceSesionPEspecificoDTO>>(resultado)!;
                }
                return new List<CruceSesionPEspecificoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-VCS-001@Error en ValidarCrucesSesiones() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Modalidad Sesión
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <param name="grupo"></param>
        /// <param name="idModalidadCurso"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool ActualizarModalidadSesion(int idPespecifico, int grupo, int idModalidadCurso, string usuario)
        {
            try
            {
                var query = "pla.SP_ActualizarModalidadPespecificoSesion";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idPespecifico, grupo, idModalidadCurso, usuario });
                return (!string.IsNullOrEmpty(resultado) && resultado != "[]");
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-AMS-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la sesión inicial
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <param name="grupo"></param>
        /// <returns> int - valor.Valor</returns>
        public int ObtenerSesionInicial(int idPespecifico, int grupo)
        {
            try
            {
                var query = "SELECT IdPespecificoSesion AS Valor FROM [pla].[V_ObtenerSesionesPorPespecificoGrupoSesion] WHERE Estado = 1 AND IdPespecifico = @idPespecifico AND GrupoCronograma = @grupo ORDER BY FechaHoraInicio ASC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPespecifico, grupo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado)!;
                    return rpta.Valor.GetValueOrDefault();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-OSI-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
		/// Obtenie datos pespecifico hijo por id sesion
		/// </summary>
		/// <param name="objeto"></param>
		/// <returns> Lista PadrePespecificoHijoCompuestoDTO </returns>
        public PadrePespecificoHijoCompuestoDTO? ObtenerDatosPespecificoHijoPorSesion(int idSesion)
        {
            try
            {
                string query = @"SELECT Id, IdSesion, PEspecificoHijoId, PEspecificoPadreId FROM pla.V_TPEspecificoSesion_ObtenerHijo WHERE Estado=1 AND IdSesion=@IdSesion;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdSesion = idSesion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<PadrePespecificoHijoCompuestoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-ODPHPS-001@Error en ObtenerDatosPespecificoHijoPorSesion() {ex.Message}", ex);
            }
        }
        /// <summary>
		/// Obtiene Id de las sesiones mediante id programa especifico padre
		/// </summary>
		/// <param name="idPespecificoPadre"></param>
		/// <param name="numeroGrupo"></param>
		/// <returns></returns>
		public IEnumerable<int> ObtenerIdSesiones(int idPespecificoPadre, int numeroGrupo)
        {
            try
            {
                var query = "SELECT Id AS Valor FROM pla.V_ObtenerIdProgramaEspecificoSesion WHERE Estado = 1 AND EsSesionInicio = 1 AND Grupo = @numeroGrupo AND PEspecificoPadreId = @idPespecificoPadre";
                var resultado = _dapperRepository.QueryDapper(query, new { numeroGrupo, idPespecificoPadre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var rpta = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor.GetValueOrDefault());
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-OIS-001@Error en ObtenerIdSesiones() {ex.Message}", ex);
            }
        }
        /// <summary>
		/// Obtiene Id de las sesiones mediante id programa especifico individuales
		/// </summary>
		/// <param name="idPespecifico"></param>
		/// <param name="numeroGrupo"></param>
		/// <returns></returns>
		public IEnumerable<int> ObtenerIdSesionesIndividuales(int idPespecifico, int numeroGrupo)
        {
            try
            {
                var query = "SELECT Id AS Valor FROM pla.V_ObtenerIdProgramaEspecificoSesionIndividual WHERE Estado = 1 AND EsSesionInicio = 1 AND Grupo = @numeroGrupo AND IdPEspecifico = @idPespecifico";
                var resultado = _dapperRepository.QueryDapper(query, new { numeroGrupo, idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var rpta = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor.GetValueOrDefault());
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-OISI-001@Error en ObtenerIdSesiones() {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Obtiene El id de los Pespecificos que estan en las sesiones
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public IEnumerable<int> ListaPespecificoSesiones(int idPespecifico)
        {
            try
            {
                string query = "SELECT Id AS Valor FROM pla.V_TPespecificoSesionporIdEspecifico WHERE Estado=1 AND IdPEspecifico=@IdPespecifico;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var rpta = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor.GetValueOrDefault());
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ListaPespecificoSesiones: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene grupos para el filtro de PEspecifico
        /// </summary>
        /// <returns> Lista DTO - List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerGruposProgramaEspecificoFiltro()
        {
            try
            {
                var query = "SELECT Grupo AS Id, CONCAT('Grupo ',Grupo) AS Nombre FROM pla.T_PEspecificoSesion WHERE Estado = 1 GROUP BY Grupo ORDER BY Grupo";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-OGPEF-001@Error en ObtenerGruposProgramaEspecificoFiltro() {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Obtiene El id de los Pespecificos que estan en las sesiones
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public IEnumerable<DetalleSesionesAlumnosDTO> ObtenerDetalleSesionesPorAlumnosFiltrado(SesionFiltroDTO detalleSesionesFiltro)
        {
            try
            {
                var IdPGeneral = detalleSesionesFiltro.IdPGeneral;
                var IdPEspecifico = detalleSesionesFiltro.IdPEspecifico;
                var IdSesion = detalleSesionesFiltro.IdSesion;
                var IdMatriculaCabecera = detalleSesionesFiltro.IdMatriculaCabecera;
                var CodigoMatricula = detalleSesionesFiltro.CodigoMatricula;

                string _query = "SELECT IdPGeneral, IdPEspecifico, IdSesion, IdCoordinadoraAcademica, NombreCoordinadoraAcademica, IdMatriculaCabecera, CodigoMatricula, NombreAlumno, CentroCosto, EstadoMatricula, Confirmo,EnvioCorreo, EnvioWhatsApp " +
                    "FROM pla.V_ObtenerDetalleSesionAlumnosWebinar WHERE IdSesion = @IdSesion";
                if (IdPGeneral != 0)
                    _query += " and IdPGeneral=@IdPGeneral";
                if (IdPEspecifico != 0)
                    _query += " and IdPEspecifico=@IdPEspecifico";
                if (CodigoMatricula != null)
                    _query += " or CodigoMatricula=@CodigoMatricula";
                _query += " ORDER BY Confirmo DESC";
                var query = _dapperRepository.QueryDapper(_query, detalleSesionesFiltro);
                var listado = JsonConvert.DeserializeObject<IEnumerable<DetalleSesionesAlumnosDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene El id de los Pespecificos que estan en las sesiones
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public DetalleSesionesAlumnosDTO? ObtenerDetalleSesionAlumnoPorIdSesionYIdMatriculaCabecera(int IdSesion, int IdMatriculaCabecera)
        {
            try
            {
                string _query = @"SELECT IdPGeneral, IdPEspecifico, IdSesion, IdCoordinadoraAcademica, NombreCoordinadoraAcademica, IdMatriculaCabecera, CodigoMatricula, NombreAlumno, CentroCosto, EstadoMatricula, Confirmo,EnvioCorreo, EnvioWhatsApp
                    FROM pla.V_ObtenerDetalleSesionAlumnosWebinar WHERE IdSesion = @IdSesion AND IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { IdSesion, IdMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<DetalleSesionesAlumnosDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public List<PEspecificoSesionesRecordatorioClases> ObtenerSesionesRecordatorioClases(int IdPEspecifico, int IdTipoModalidad)
        {
            try
            {
                List<PEspecificoSesionesRecordatorioClases> resultado = new List<PEspecificoSesionesRecordatorioClases>();
                var queryResult = _dapperRepository.QuerySPDapper("[ope].[SP_LlamadaAutomaticaObtenerDatoRecordatorioClase]", new { IdPEspecifico, IdTipoModalidad });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<PEspecificoSesionesRecordatorioClases>>(queryResult);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public List<PEspecificoSesionesRecordatorioClases> ObtenerSesionesRecordatorioWebinar(int IdPEspecifico, int IdTipoModalidad)
        {
            try
            {
                List<PEspecificoSesionesRecordatorioClases> resultado = new List<PEspecificoSesionesRecordatorioClases>();
                var queryResult = _dapperRepository.QuerySPDapper("[ope].[SP_LlamadaAutomaticaObtenerDatoRecordatorioWebinar]", new { IdPEspecifico, IdTipoModalidad });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<PEspecificoSesionesRecordatorioClases>>(queryResult);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idPEspecifico por IdPespecificoSesion
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <returns> int </returns>
        public int ObtenerPEspecificoPorPEspecificoSesion(int IdPespecificoSesion)
        {
            try
            {
                var query = "SELECT IdPEspecifico FROM pla.T_PEspecificoSesion WHERE Id = @IdPespecificoSesion AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecificoSesion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var lista = JsonConvert.DeserializeObject<List<dynamic>>(resultado);
                    if (lista != null && lista.Count > 0)
                    {
                        return lista[0].IdPEspecifico;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-OPPS-001@Error en ObtenerPEspecificoPorPEspecificoSesion() {ex.Message}", ex);
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Enviar notificaciones a los alumnos asociados a la sesion, si esta es eliminada
        /// </summary>
        /// <param name="IdPEspecificoSesion"></param>
        /// <param name="Usuario"></param>
        /// <returns> true o false </returns>
        public bool NotificarAlumnosPEspecificoSesionCancelacionPortal(int IdPEspecificoSesion, string Usuario)
        {
            try
            {
                var query = "pla.SP_CrearNotificacionCancelarSesionIntegra";
                var respuesta = _dapperRepository.QuerySPDapper(query, new { IdPEspecificoSesion, Usuario });

                if (!string.IsNullOrEmpty(respuesta) && respuesta != null && respuesta != "null")
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PESR-OPPS-001@Error en NotificarAlumnos() {ex.Message}", ex);
            }
        }
        /// Autor: Jeremy Pacheco
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Enviar notificaciones a los alumnos asociados a la sesion, si esta es reprogramada
        /// </summary>
        /// <param name="idPEspecificoSesion"></param>
        /// <param name="fechaNuevaHoraInicio"></param>
        /// <param name="usuario"></param>
        /// <returns> true o false </returns>
        public bool NotificarAlumnosPEspecificoSesionReprogramacionPortal(int idPEspecificoSesion, DateTime fechaNuevaHoraInicio, string usuario)
        {
            try
            {
                string query = "pla.SP_CrearNotificacionReprogramacionSesionIntegra";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idPEspecificoSesion, fechaNuevaHoraInicio, usuario });

                if (!string.IsNullOrEmpty(resultado) && resultado != null && resultado != "null")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-OIPH-001@Error en NotificacionAlumnosPEspecificoSesionPortal: {ex.Message}", ex);
            }
        }
        public bool EsWebinarPasado(int idPEspecificoSesion)
        {
            try
            {
                return this.Exist(x => x.Id == idPEspecificoSesion && x.FechaHoraInicio < DateTime.Now);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
