using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MandrilRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/07/2022
    /// <summary>
    /// Gestión general de T_Mandril
    /// </summary>
    public class MandrilRepository : GenericRepository<TMandril>, IMandrilRepository
    {
        private Mapper _mapper;

        public MandrilRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMandril, Mandril>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMandril MapeoEntidad(Mandril entidad)
        {
            try
            {
                //crea la entidad padre
                TMandril modelo = _mapper.Map<TMandril>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMandril Add(Mandril entidad)
        {
            try
            {
                var Mandril = MapeoEntidad(entidad);
                base.Insert(Mandril);
                return Mandril;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMandril Update(Mandril entidad)
        {
            try
            {
                var Mandril = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Mandril.RowVersion = entidadExistente.RowVersion;

                base.Update(Mandril);
                return Mandril;
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


        public IEnumerable<TMandril> Add(IEnumerable<Mandril> listadoEntidad)
        {
            try
            {
                List<TMandril> listado = new List<TMandril>();
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

        public IEnumerable<TMandril> Update(IEnumerable<Mandril> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMandril> listado = new List<TMandril>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Mandril.
        /// </summary>
        /// <returns> List<MandrilDTO> </returns>
        public IEnumerable<MandrilDTO> ObtenerMandril()
        {
            try
            {
                List<MandrilDTO> rpta = new List<MandrilDTO>();
                var query = @"
                    SELECT Id,IdAlumno,Evento,IdEvent,Ip,Ts,Url,UserAgent,LocationCity,LocationCountry,LocationCountryShort,LocationLatitude,
	                    LocationLongitude,LocationPostalCode,LocationRegion,LocationTimezone,UserAgentMobile,UserAgentOsCompany,UserAgentOsCompanyUrl,
	                    UserAgentOsFamily,UserAgentOsIcon,UserAgentOsName,UserAgentOsUrl,UserAgentType,UserAgentUaCompany,UserAgentUaCompanyUrl,UserAgentUaFamily,
	                    UserAgentUaIcon,UserAgentUaName,UserAgentUaUrl,UserAgentUaVersion,MessageBgToolsCode,MessageBounceDescription,MessageDiag,MessageEmail,
	                    MessageId,MessageSender,MessageState,MessageSubAccount,MessageSubject,MessageTags,MessageTemplate,MessageTs,MessageVersion,
	                    IdTipoInteraccion,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_Mandril
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MandrilDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo Tipo de Interaccion de Correos asociados a un Alumno y un Personal.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<CorreoInteraccionV2AgendaDTO> </returns>
        public IEnumerable<CorreoInteraccionV2AgendaDTO> ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(int idAlumno, int idPersonal)
        {
            try
            {
                List<CorreoInteraccionV2AgendaDTO> interacciones = new List<CorreoInteraccionV2AgendaDTO>();
                var query = @"
                    SELECT Id,FechaCreacion,Categoria,Asunto,CorreoReceptor,CorreoRemitente,IdAlumno,IdPersonal,MessageId,UltimaInteraccion
                    FROM com.V_UltimoTipoInteraccionMandrilPorCorreo
                    WHERE IdAlumno = @idAlumno AND IdPersonal = @idPersonal
                    ORDER BY Id DESC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idAlumno, idPersonal });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    interacciones = JsonConvert.DeserializeObject<List<CorreoInteraccionV2AgendaDTO>>(resultadoQuery);
                }
                return interacciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="correoReceptor"></param>
        /// <param name="messageId"></param>
        /// <returns>CorreoAlumnoSpeechDTO</returns>
        public CorreoAlumnoSpeechDTO VerCorreoAlumnoSpeech(string correoReceptor, string messageId)
        {
            try
            {
                string query = "com.SP_ObtenerCorreoSpeech";
                var queryRespuesta = _dapperRepository.QuerySPFirstOrDefault(query, new { MessageId = messageId, CorreoReceptor = correoReceptor });
                CorreoAlumnoSpeechDTO listaCorreos = JsonConvert.DeserializeObject<CorreoAlumnoSpeechDTO>(queryRespuesta);
                return listaCorreos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico para mezclar con correos recibidos
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        /// <returns>CorreoDTO</returns>
        public List<CorreoDTO> ListaInteraccionCorreoAlumnoCorreo(int idAlumno, int idAsesor)
        {
            try
            {
                string _query = @"SELECT sub.*, TIM.Descripcion AS Estado
                FROM
                (
                    SELECT MAX(Id) AS Id, MAX(FechaCreacion) AS Fecha , Asunto, CorreoReceptor AS Destinatarios, CorreoRemitente AS Remitente, IdAlumno, IdAsesor AS IdPersonal, MessageId, 'Enviado' AS Tipo
                    FROM  com.V_Correo_InteraccionesV2
                    WHERE IdAlumno = @IdAlumno AND IdAsesor = @IdAsesor
                    GROUP BY  Categoria, Asunto, CorreoReceptor, CorreoRemitente, IdAlumno, IdAsesor, MessageId
                ) AS sub
                LEFT JOIN com.T_Mandril AS M ON sub.Id = M.Id
                LEFT JOIN mkt.T_TipoInteraccionMandril AS TIM ON M.Evento = TIM.Nombre
                ORDER BY Id DESC
                ";
                var _queryRespuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = idAlumno, IdAsesor = idAsesor });
                List<CorreoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoDTO>>(_queryRespuesta);
                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        ///  /// <param name="messageId"></param>
        /// <returns>CorreoInteraccionesAlumnoDTO</returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumno(int idAlumno, int idAsesor, string messageId)
        {
            try
            {
                string _query = "SELECT Id, FechaCreacion, Categoria, Asunto, Estado, CorreoReceptor, CorreoRemitente FROM  com.V_Correo_InteraccionesV2 WHERE IdAlumno = @IdAlumno AND IdAsesor = @IdAsesor AND MessageId=@MessageId";
                var _queryRespuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = idAlumno, IdAsesor = idAsesor, MessageId = messageId });
                List<CorreoInteraccionesAlumnoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoInteraccionesAlumnoDTO>>(_queryRespuesta);
                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
