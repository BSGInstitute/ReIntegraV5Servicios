using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CorreoGmailRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/11/2022
    /// <summary>
    /// Gestión general de T_CorreoGmail
    /// </summary>
    public class CorreoGmailRepository : GenericRepository<TCorreoGmail>, ICorreoGmailRepository
    {
        private Mapper _mapper;

        public CorreoGmailRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCorreoGmail, CorreoGmail>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCorreoGmail MapeoEntidad(CorreoGmail entidad)
        {
            try
            {
                //crea la entidad padre
                TCorreoGmail modelo = _mapper.Map<TCorreoGmail>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCorreoGmail Add(CorreoGmail entidad)
        {
            try
            {
                var CorreoGmail = MapeoEntidad(entidad);
                base.Insert(CorreoGmail);
                return CorreoGmail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(CorreoGmail entidad)
        {
            try
            {
                var CorreoGmail = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CorreoGmail.RowVersion = entidadExistente.RowVersion;

                base.Update(CorreoGmail);
                return true;
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
        #endregion

        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// <summary>
        /// Filtro de correos por persona.
        /// </summary>
        /// <param name="idFolder">Id de folder de correo</param>
        /// <param name="queryFiltro">Filtro para query</param>
        /// <returns>Lista de objetos (CorreoDTO)</returns>
        public List<CorreoDTO> FiltroCorreosPorPersona(int idFolder, string queryFiltro)
        {
            try
            {
                var listaCorreoGmail = new List<CorreoDTO>();
                int TotalCorreos = 0;
                string _query = string.Empty;
                _query = @"
                        SELECT GmailCorreoId AS Id, Asunto, Fecha, EmailRemitente AS Remitente, Destinatarios, IdPersonal, EmailConCopia AS ConCopia
                        FROM mkt.T_CorreoGmail WITH(NOLOCK)
                        WHERE IdPersonal is not null AND IdGmailFolder=@IdFolder ";
                _query = _query + queryFiltro + " GROUP BY GmailCorreoId,Asunto,Fecha,EmailRemitente,Destinatarios,IdPersonal,EmailConCopia ORDER BY Fecha DESC";
                var _queryResultado = _dapperRepository.QueryDapper(_query, new { idFolder });
                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<CorreoDTO>>(_queryResultado);
                }
                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// <summary>
        /// Obtiene data modelada de mkt.T_GmailCorreo para casos webinar y aonline.
        /// </summary>
        /// <param name="queryFiltro"> Filtro para query </param>
        /// <returns> Lista de objetos (CorreoDTO) </returns>
        public List<CorreoDTO> FiltroCorreosPorPersonaGmailCorreo(string queryFiltro)
        {
            try
            {
                var listaCorreoGmail = new List<CorreoDTO>();
                int TotalCorreos = 0;
                string _query = string.Empty;
                _query = @"SELECT Id, Asunto, EmailBody,  Fecha, Remitente, Destinatarios, IdPersonal, ConCopia
                        FROM mkt.V_GmailCorreo_ObtenerMensajesEnviados
                        WHERE IdPersonal IS NOT NULL";
                _query = _query + queryFiltro + " GROUP BY Id, Asunto, EmailBody, Fecha, Remitente, Destinatarios, IdPersonal, ConCopia ORDER BY Fecha DESC";

                var queryResultado = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<CorreoDTO>>(queryResultado);
                }
                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// <summary>
        /// Cuenta correos por persona.
        /// </summary>
        /// <param name="idPersonal">Id de personal</param>
        /// <param name="idFolder">Id de folder de correo</param>
        /// <returns>int</returns>
        public int ContadorCorreosPorPersona(int idPersonal, int idFolder)
        {
            try
            {
                var listaCorreoGmail = new Dictionary<string, int>();
                int TotalCorreos = 0;
                string query = @"
                        SELECT COUNT(*) Cantidad
                        FROM 
                            mkt.T_CorreoGmail
                        WHERE 
                            IdPersonal is not null and IdPersonal=@IdPersonal and IdGmailFolder=@IdFolder";
                var _queryResultado = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal, IdFolder = idFolder });

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<Dictionary<string, int>>(_queryResultado);
                }
                if (listaCorreoGmail != null)
                {
                    var repuesta = listaCorreoGmail.Select(x => x.Value).FirstOrDefault();
                    if (repuesta != null)
                    {
                        TotalCorreos = repuesta;
                    }
                    else
                    {
                        TotalCorreos = 0;
                    }
                }
                else
                {
                    TotalCorreos = 0;
                }
                return TotalCorreos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene correos por grupos sin version.
        /// </summary>
        /// <param name="idCentroCosto">Id centro de costo</param>
        /// <param name="IdPaquete">Id de paquete</param>
        /// <param name="estado">Estado</param>
        /// <param name="subEstado">Sub estado</param>
        /// <returns> ListaCorreosGrupoDTO </returns>
        public ListaCorreosGrupoDTO ObtenerCorreosGruposSinVersion(int idCentroCosto, List<int> estado, List<int> subEstado)
        {
            try
            {
                var queryResultado = "";//inicializando variable _queryResultado
                var listaCorreoGmail = new List<StringDTO>();
                ListaCorreosGrupoDTO respuestaQuery = new ListaCorreosGrupoDTO();

                string query = string.Empty;
                if (estado.Count == 0 && subEstado.Count == 0) //Si solo hay Centro de costo
                {
                    query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE idCentroCosto = @IdCentroCosto";
                    queryResultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto });
                }
                if (estado.Count != 0 && subEstado.Count == 0) //si hay centro de costo, estado pero no subEstado
                {
                    query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE idCentroCosto = @IdCentroCosto and EstadoMatricula in @Estado";
                    queryResultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto, Estado = estado });
                }
                if (estado.Count != 0 && subEstado.Count != 0)//Si hay  los 3 campos
                {
                    query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE idCentroCosto= @IdCentroCosto and EstadoMatricula in @Estado and subEstadoMatricula in @SubEstado";
                    queryResultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto, Estado = estado, SubEstado = subEstado });
                }
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<StringDTO>>(queryResultado);
                    var repuesta = listaCorreoGmail.Select(x => x.Valor).ToList();

                    respuestaQuery.ListaCorreos = string.Join(",", repuesta);
                    respuestaQuery.TotalCorreos = repuesta.Count;
                    respuestaQuery.Errores = false;
                }
                else
                {
                    respuestaQuery.ListaCorreos = "";
                    respuestaQuery.TotalCorreos = 0;
                    respuestaQuery.Errores = true;
                }
                return respuestaQuery;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene correos por grupos con version.
        /// </summary>
        /// <param name="IdCentroCosto">Id centro de costo</param>
        /// <param name="IdPaquete">Id de paquete</param>
        /// <param name="Estados">Estado</param>
        /// <param name="SubEstados">Sub estado</param>
        /// <returns> ListaCorreosGrupoDTO </returns>
        public ListaCorreosGrupoDTO ObtenerCorreosGruposConVersion(int idCentroCosto, int idPaquete, List<int> estado, List<int> subEstado)
        {
            try
            {
                var queryResultado = "";//inicializando variable _queryResultado
                var listaCorreoGmail = new List<StringDTO>();
                ListaCorreosGrupoDTO respuestaQuery = new ListaCorreosGrupoDTO();
                string query = string.Empty;

                if (estado.Count == 0 && subEstado.Count == 0) //Si solo hay Centro de costo y paquete
                {
                    query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto = @IdCentroCosto and IdPaquete = @IdPaquete";

                    queryResultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto, IdPaquete = idPaquete });
                }
                if (estado.Count != 0 && subEstado.Count == 0) //si hay centro de costo,paquete y estado pero no SubEstado
                {
                    query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto = @IdCentroCosto and IdPaquete = @IdPaquete and EstadoMatricula in @Estado";

                    queryResultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto, IdPaquete = idPaquete, Estado = estado });
                }
                if (estado.Count != 0 && subEstado.Count != 0)//Si hay  los 4 campos
                {
                    query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto = @IdCentroCosto and IdPaquete = @IdPaquete and EstadoMatricula in @Estado and SubEstadoMatricula in @SubEstado";

                    queryResultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto, IdPaquete = idPaquete, Estado = estado, SubEstado = subEstado });
                }
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<StringDTO>>(queryResultado);
                    var repuesta = listaCorreoGmail.Select(x => x.Valor).ToList();

                    respuestaQuery.ListaCorreos = string.Join(",", repuesta);
                    respuestaQuery.TotalCorreos = repuesta.Count;
                    respuestaQuery.Errores = false;
                }
                else
                {
                    respuestaQuery.ListaCorreos = "";
                    respuestaQuery.TotalCorreos = 0;
                    respuestaQuery.Errores = true;
                }
                return respuestaQuery;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public AccesosMoodleDTO obtenerAccesosInicialesMoodle( int idAlumno)
        {
            try
            {
                var query = "SELECT IdAlumno, IdMoodle, UsuarioMoodle, PasswordMoodle FROM [ope].[V_TAccesosMoodle_ObtenerAccesosMoodle] WHERE IdAlumno = @IdAlumno";
                var res = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });
                return JsonConvert.DeserializeObject<AccesosMoodleDTO>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Plantilla obtenerPlantilla()
        {
            try
            {
                Plantilla plantilla = new Plantilla();
                var query = @"
                                SELECT 
                                    Id,
                                    Nombre,
                                    Descripcion,
                                    IdPlantillaBase,
                                    EstadoAgenda,
                                    Documento,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    IdPersonalAreaTrabajo,
                                    EstadoPlantillaIntegra FROM mkt.T_Plantilla WHERE Nombre LIKE '%Datos de Acceso Portal Web%' AND IdPlantillaBase = 2
                                                              
                            ";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<Plantilla>(resultadoQuery)!;
                }
                return plantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Plantilla obtenerPlantillaAccesoMoodleAlumnoWhatsApp()
        {
            try
            {
                Plantilla plantilla = new Plantilla();
                var query = @"
                                SELECT 
                                    Id,
                                    Nombre,
                                    Descripcion,
                                    IdPlantillaBase,
                                    EstadoAgenda,
                                    Documento,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    IdPersonalAreaTrabajo,
                                    EstadoPlantillaIntegra FROM mkt.T_Plantilla WHERE Nombre LIKE '%acceso_aula_virtual_%' AND IdPlantillaBase = 8
                                                              
                            ";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<Plantilla>(resultadoQuery)!;
                }
                return plantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Plantilla obtenerPlantillaAccesoMoodleAlumno()
        {
            try
            {
                Plantilla plantilla = new Plantilla();
                var query = @"
                                SELECT 
                                    Id,
                                    Nombre,
                                    Descripcion,
                                    IdPlantillaBase,
                                    EstadoAgenda,
                                    Documento,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    IdPersonalAreaTrabajo,
                                    EstadoPlantillaIntegra FROM mkt.T_Plantilla WHERE Nombre LIKE '%Datos de Acceso Portal Web%' AND IdPlantillaBase = 2
                                                              
                            ";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<Plantilla>(resultadoQuery)!;
                }
                return plantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Jose Vega
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el último UID (GmailCorreoId) almacenado para un personal específico en el INBOX.
        /// </summary>
        /// <param name="idPersonal">Id del personal</param>
        /// <returns>El UID más alto como long, o 0 si no hay registros.</returns>
        public long ObtenerUltimoUidPorPersonal(int idPersonal)
        {
            try
            {
                var query = @"
                    SELECT ISNULL(MAX(GmailCorreoId), 0) AS UltimoUid
                    FROM mkt.T_CorreoGmail WITH(NOLOCK)
                    WHERE IdPersonal = @IdPersonal
                      AND IdGmailFolder = 1
                      AND Estado = 1";

                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, long>>(resultado);
                    if (dict != null)
                    {
                        return dict.Values.FirstOrDefault();
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
