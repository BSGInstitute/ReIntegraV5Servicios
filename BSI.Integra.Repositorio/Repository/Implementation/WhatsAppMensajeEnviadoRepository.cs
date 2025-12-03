using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppMensajeEnviadoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión general de T_WhatsAppMensajeEnviado
    /// </summary>
    public class WhatsAppMensajeEnviadoRepository : GenericRepository<TWhatsAppMensajeEnviado>, IWhatsAppMensajeEnviadoRepository
    {
        private Mapper _mapper;

        public WhatsAppMensajeEnviadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppMensajeEnviado, WhatsAppMensajeEnviado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppMensajeEnviado MapeoEntidad(WhatsAppMensajeEnviado entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppMensajeEnviado modelo = _mapper.Map<TWhatsAppMensajeEnviado>(entidad);

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

        public TWhatsAppMensajeEnviado Add(WhatsAppMensajeEnviado entidad)
        {
            try
            {
                var WhatsAppMensajeEnviado = MapeoEntidad(entidad);
                base.Insert(WhatsAppMensajeEnviado);
                return WhatsAppMensajeEnviado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppMensajeEnviado Update(WhatsAppMensajeEnviado entidad)
        {
            try
            {
                var WhatsAppMensajeEnviado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppMensajeEnviado.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppMensajeEnviado);
                return WhatsAppMensajeEnviado;
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


        public IEnumerable<TWhatsAppMensajeEnviado> Add(IEnumerable<WhatsAppMensajeEnviado> listadoEntidad)
        {
            try
            {
                List<TWhatsAppMensajeEnviado> listado = new List<TWhatsAppMensajeEnviado>();
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

        public IEnumerable<TWhatsAppMensajeEnviado> Update(IEnumerable<WhatsAppMensajeEnviado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppMensajeEnviado> listado = new List<TWhatsAppMensajeEnviado>();
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
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_WhatsAppMensajeEnviado.
        /// </summary>
        /// <returns> List<WhatsAppMensajeEnviadoDTO> </returns>
        public IEnumerable<WhatsAppMensajeEnviadoDTO> ObtenerWhatsAppMensajeEnviado()
        {
            try
            {
                List<WhatsAppMensajeEnviadoDTO> rpta = new List<WhatsAppMensajeEnviadoDTO>();
                var query = @"
                    SELECT
	                    Id,WaTo,WaId,WaType,WaTypeMensaje,WaRecipientType,WaBody,WaFile,WaFileName,WaMimeType,WaSha256,WaLink,WaCaption,
	                    IdPais,EsMigracion,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,IdPersonal,IdAlumno
                    FROM mkt.T_WhatsAppMensajeEnviado
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppMensajeEnviadoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_WhatsAppMensajeEnviado para mostrarse en combo.
        /// </summary>
        /// <returns> List<WhatsAppMensajeEnviadoComboDTO> </returns>
        public IEnumerable<WhatsAppMensajeEnviadoComboDTO> ObtenerCombo()
        {
            try
            {
                List<WhatsAppMensajeEnviadoComboDTO> rpta = new List<WhatsAppMensajeEnviadoComboDTO>();
                var query = @"
                    SELECT
	                    Id,WaBody
                    FROM mkt.T_WhatsAppMensajeEnviado
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppMensajeEnviadoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;

                if (area == "VE")
                {
                    query = @"
                        SELECT
                            TOP 1
	                        WA.Numero, WA.Mensaje, WA.IdPersonal, WA.FechaCreacion, WA.IdPais, ISNULL(WA.IdAlumno,0) AS IdAlumno,
	                        CASE
		                        WHEN AL.Nombre1 IS NULL
			                        THEN WA.Numero
		                        ELSE CONCAT(AL.Nombre1,' ',AL.ApellidoPaterno)
	                        END AS NombreAlumno
                        FROM com.V_HistorialChatWhatsAppCom AS WA
                        LEFT JOIN mkt.T_Alumno AS AL WITH(NOLOCK) ON WA.IdAlumno = AL.Id
                        WHERE WA.Numero = @numero ORDER BY WA.FechaCreacion DESC";
                }
                else if (area == "OP")
                {
                    query = @"
                        SELECT
                            TOP 1
	                        WA.Numero, WA.Mensaje, WA.IdPersonal, WA.FechaCreacion, WA.IdPais, ISNULL(WA.IdAlumno,0) AS IdAlumno,
	                        CASE
		                        WHEN AL.Nombre1 IS NULL
			                        THEN WA.Numero
		                        ELSE CONCAT(AL.Nombre1,' ',AL.ApellidoPaterno)
	                        END AS NombreAlumno
                        FROM mkt.V_HistorialChatWhatsApp AS WA
                        LEFT JOIN mkt.T_Alumno AS AL WITH(NOLOCK) ON WA.IdAlumno = AL.Id
                        WHERE WA.Numero = @numero ORDER BY WA.FechaCreacion DESC";
                }
                else if (area == "OP-DOC")
                {
                    query = @"
                        SELECT
	                        WA.Numero, WA.Mensaje, WA.IdPersonal, WA.FechaCreacion, WA.IdPais, ISNULL(WA.IdAlumno,0) AS IdAlumno,
	                        CASE
		                        WHEN AL.Nombre1 IS NULL
			                        THEN WA.Numero
		                        ELSE CONCAT(AL.Nombre1,' ',AL.ApellidoPaterno)
	                        END AS NombreAlumno
                        FROM mkt.V_HistorialChatWhatsAppRecibido AS WA
                        LEFT JOIN mkt.T_Alumno AS AL WITH(NOLOCK) ON WA.IdAlumno=AL.Id
                        LEFT JOIN gp.T_Personal AS P ON P.Id=WA.IdPersonal
                        WHERE WA.Numero = @numero AND P.AreaAbrev = 'OP'
                        ORDER BY WA.FechaCreacion DESC";
                }
                else
                {
                    query = @"
                        SELECT
	                        WA.Numero, WA.Mensaje, WA.IdPersonal, WA.FechaCreacion, WA.IdPais, ISNULL(WA.IdAlumno,0) AS IdAlumno,
	                        CASE
		                        WHEN AL.Nombre1 IS NULL
			                        THEN WA.Numero
		                        ELSE CONCAT(AL.Nombre1,' ',AL.ApellidoPaterno)
	                        END AS NombreAlumno
                        FROM mkt.V_HistorialChatWhatsAppRecibido AS WA
                        LEFT JOIN mkt.T_Alumno AS AL WITH(NOLOCK) ON WA.IdAlumno=AL.Id
                        WHERE WA.IdPersonal = @idPersonal AND WA.Numero = @numero ORDER BY WA.FechaCreacion DESC";
                }
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialMensajesDTO> listaMensajes = new List<WhatsAppHistorialMensajesDTO>();
                var query = string.Empty;
                string numeroMexicoIn = "";
                string numeroMexicoOut = "";
                string numeroCanadaIn = "";

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        numeroMexicoIn = numero.StartsWith("52") ? "521" + numero.Substring(2) : numero;
                        numeroMexicoOut = numero.StartsWith("521") ? "52" + numero.Substring(3) : numero;
                        numeroCanadaIn = numero.StartsWith("1") ? "11" + numero.Substring(1) : numero;

                        query = @$"SELECT DISTINCT Numero, Tipo, SubTipo, Mensaje, IdPersonal, IdAlumno, IdPais, Registro, FechaCreacion,NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje, MAX(final.FechaEstado) AS FechaEstado
                                FROM (
                                    SELECT resultado.WaId, Numero, Tipo, SubTipo, Mensaje, IdPersonal, ISNULL(IdAlumno, 0) IdAlumno, resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal, AreaAbrev, estado.WaStatus,
                                           CASE
                                               WHEN estado.WaStatus = 'sent' THEN 1
                                               WHEN estado.WaStatus = 'delivered' THEN 2
                                               WHEN estado.WaStatus = 'read' THEN 3
                                           END AS EstadoMensaje,
                                           estado.FechaCreacion AS FechaEstado
                                    FROM [com].[V_HistorialChatWhatsAppCom] AS resultado
                                    LEFT JOIN com.T_WhatsAppEstadoMensajeEnviadoCom AS estado ON estado.WaId = resultado.WaId
                                    WHERE MensajeOfensivo = 0 AND (Numero ='{numeroMexicoIn}' OR Numero = '{numeroMexicoOut}' OR Numero = '{numeroCanadaIn}' OR Numero=@numero)
                                ) AS final
                                GROUP BY Numero, Tipo, SubTipo, Mensaje, IdPersonal, IdAlumno, IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER BY FechaCreacion ASC;";
                    }
                    else if (area == "OP")
                    {
                        numeroMexicoIn = numero.StartsWith("52") ? "521" + numero.Substring(2) : numero;
                        numeroMexicoOut = numero.StartsWith("521") ? "52" + numero.Substring(3) : numero;
                        numeroCanadaIn = numero.StartsWith("1") ? "11" + numero.Substring(1) : numero;

                        query = @$"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje, MAX(final.FechaEstado) AS FechaEstado
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
		                                 WHEN estado.WaStatus='sent'
			                                THEN 1
		                                 WHEN estado.WaStatus='delivered'
			                                THEN 2
		                                 WHEN estado.WaStatus='read'
			                                THEN 3
		                                END AS EstadoMensaje,
		                                estado.FechaCreacion AS FechaEstado
	                                FROM ope.V_HistorialChatWhatsAppApiOperaciones  AS resultado
	                                LEFT JOIN (
										SELECT WaId, WaStatus, FechaCreacion FROM mkt.T_WhatsAppEstadoMensajeEnviado 
										UNION
										SELECT WaId, WaStatus, FechaCreacion FROM ope.T_WhatsAppEstadoMensajeEnviadoAtc
									) AS estado ON estado.WaId = resultado.WaId
	                                WHERE  (Numero ='{numeroMexicoIn}' OR Numero = '{numeroMexicoOut}' OR Numero = '{numeroCanadaIn}' OR Numero=@numero)
                                ) AS final
                                GROUP BY Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else if (area == "OP-DOC")
                    {
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
			                                WHEN estado.WaStatus='sent'
				                                THEN 1
			                                WHEN estado.WaStatus='delivered'
				                                THEN 2
			                                WHEN estado.WaStatus='read'
				                                THEN 3
		                                END AS EstadoMensaje
	                                FROM mkt.V_HistorialChatWhatsApp AS resultado
	                                LEFT JOIN mkt.T_WhatsAppEstadoMensajeEnviado AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero AND AreaAbrev='OP'
                                ) AS final
                                GROUP BY 
                                Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesDTO>>(credencialTokenExpiraDB);

                List<StringDTO> ListaPalabrasOfensivas = new List<StringDTO>();
                string query1 = string.Empty;
                query1 = "SELECT PalabraFiltrada AS Valor FROM mkt.T_DiccionarioPalabraOfensiva";
                var respuesta = _dapperRepository.QueryDapper(query1, null);
                ListaPalabrasOfensivas = JsonConvert.DeserializeObject<List<StringDTO>>(respuesta);

                List<WhatsAppHistorialMensajesDTO> ListaMensajesWhatsAppRespondidosActualizada = new List<WhatsAppHistorialMensajesDTO>();
                string palabraOfensivaComparar = "";
                string armarPalabraSensurada = "";

                foreach (WhatsAppHistorialMensajesDTO alumnoDatos in listaMensajes)
                {
                    string UltimoMensaje = alumnoDatos.Mensaje;
                    if (UltimoMensaje != null)
                    {
                        //UltimoMensaje = alumnoDatos.Mensaje?.ToLower();
                        foreach (var palabraOfensiva in ListaPalabrasOfensivas)
                        {
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToString();
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToLower();
                            if (UltimoMensaje.ToLower().Contains(palabraOfensivaComparar))
                            {
                                armarPalabraSensurada = palabraOfensivaComparar.Substring(0, 1);
                                int switCaracter = 0;
                                for (int i = 0; i < palabraOfensivaComparar.Length - 1; i++)
                                {
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "#";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "&";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "/";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                }
                                UltimoMensaje = UltimoMensaje.Replace(palabraOfensivaComparar, armarPalabraSensurada);
                            }
                        }
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    else
                    {
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    ListaMensajesWhatsAppRespondidosActualizada.Add(alumnoDatos);
                }
                listaMensajes.Clear();
                listaMensajes.AddRange(ListaMensajesWhatsAppRespondidosActualizada);

                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesOperacionesDTO> ListaHistorialMensajeChatatc(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialMensajesOperacionesDTO> listaMensajes = new List<WhatsAppHistorialMensajesOperacionesDTO>();
                var query = string.Empty;
                string numeroMexicoIn = "";
                string numeroMexicoOut = "";
                string numeroCanadaIn = "";

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                    }
                    else if (area == "OP")
                    {
                        numeroMexicoIn = numero.StartsWith("52") ? "521" + numero.Substring(2) : numero;
                        numeroMexicoOut = numero.StartsWith("521") ? "52" + numero.Substring(3) : numero;
                        numeroCanadaIn = numero.StartsWith("1") ? "11" + numero.Substring(1) : numero;

                        query = @$"SELECT	DISTINCT
                        		Numero
                        		,Tipo
                        		,Mensaje
                        		,IdPersonal
                        		,IdAlumno
                        		,IdPais
                        		,Registro
                        		,FechaCreacion
                        		,NombrePersonal
                        		,AreaPersonal
                        		,MAX(EstadoMensaje)		AS EstadoMensaje
                        		,MAX(final.FechaEstado) AS FechaEstado
                        FROM(
                        	SELECT
                        		resultado.WaId
                        		,Numero
                        		,Tipo
                        		,Mensaje
                        		,IdPersonal
                        		,ISNULL(IdAlumno, 0)	IdAlumno
                        		,resultado.IdPais
                        		,Registro
                        		,resultado.FechaCreacion
                        		,NombrePersonal
                        		,AreaAbrev
                        		,estado.WaStatus
                        		,CASE
                        			WHEN estado.WaStatus='sent'
                        				THEN 1
                        			WHEN estado.WaStatus='delivered'
                        				THEN 2
                        			WHEN estado.WaStatus='read'
                        				THEN 3
                        			END					AS EstadoMensaje
                        		,estado.FechaCreacion	AS FechaEstado
                        	FROM
                        		ope.V_HistorialChatWhatsAppApiOperaciones	AS resultado
                        	LEFT JOIN(
                        		SELECT
                        			WaId
                        			,WaStatus
                        			,FechaCreacion
                        		FROM
                        			mkt.T_WhatsAppEstadoMensajeEnviado
                        		UNION
                        		SELECT
                        			WaId
                        			,WaStatus
                        			,FechaCreacion
                        		FROM
                        			ope.T_WhatsAppEstadoMensajeEnviadoAtc
                        	)												AS estado ON estado.WaId=resultado.WaId
                        	WHERE
                        		(Numero='{numeroMexicoIn}' OR Numero='{numeroMexicoOut}' OR Numero='{numeroCanadaIn}' OR Numero=@numero)
                        ) AS final
                        GROUP BY
                        	Numero
                        	,Tipo
                        	,Mensaje
                        	,IdPersonal
                        	,IdAlumno
                        	,IdPais
                        	,Registro
                        	,FechaCreacion
                        	,NombrePersonal
                        	,AreaPersonal
                        ORDER BY
                        	FechaCreacion ASC;";
                    }
                    else if (area == "OP-DOC")
                    {
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
			                                WHEN estado.WaStatus='sent'
				                                THEN 1
			                                WHEN estado.WaStatus='delivered'
				                                THEN 2
			                                WHEN estado.WaStatus='read'
				                                THEN 3
		                                END AS EstadoMensaje
	                                FROM mkt.V_HistorialChatWhatsApp AS resultado
	                                LEFT JOIN mkt.T_WhatsAppEstadoMensajeEnviado AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero AND AreaAbrev='OP'
                                ) AS final
                                GROUP BY 
                                Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesOperacionesDTO>>(credencialTokenExpiraDB);

                List<StringDTO> ListaPalabrasOfensivas = new List<StringDTO>();
                string query1 = string.Empty;
                query1 = "SELECT PalabraFiltrada AS Valor FROM mkt.T_DiccionarioPalabraOfensiva";
                var respuesta = _dapperRepository.QueryDapper(query1, null);
                ListaPalabrasOfensivas = JsonConvert.DeserializeObject<List<StringDTO>>(respuesta);

                List<WhatsAppHistorialMensajesOperacionesDTO> ListaMensajesWhatsAppRespondidosActualizada = new List<WhatsAppHistorialMensajesOperacionesDTO>();
                string palabraOfensivaComparar = "";
                string armarPalabraSensurada = "";

                foreach (WhatsAppHistorialMensajesOperacionesDTO alumnoDatos in listaMensajes)
                {
                    string UltimoMensaje = alumnoDatos.Mensaje;
                    if (UltimoMensaje != null)
                    {
                        //UltimoMensaje = alumnoDatos.Mensaje?.ToLower();
                        foreach (var palabraOfensiva in ListaPalabrasOfensivas)
                        {
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToString();
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToLower();
                            if (UltimoMensaje.ToLower().Contains(palabraOfensivaComparar))
                            {
                                armarPalabraSensurada = palabraOfensivaComparar.Substring(0, 1);
                                int switCaracter = 0;
                                for (int i = 0; i < palabraOfensivaComparar.Length - 1; i++)
                                {
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "#";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "&";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "/";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                }
                                UltimoMensaje = UltimoMensaje.Replace(palabraOfensivaComparar, armarPalabraSensurada);
                            }
                        }
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    else
                    {
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    ListaMensajesWhatsAppRespondidosActualizada.Add(alumnoDatos);
                }
                listaMensajes.Clear();
                listaMensajes.AddRange(ListaMensajesWhatsAppRespondidosActualizada);

                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesOperacionesDTO> ListaHistorialMensajeChatOperaciones(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialMensajesOperacionesDTO> listaMensajes = new List<WhatsAppHistorialMensajesOperacionesDTO>();
                var query = string.Empty;
                string numeroMexicoIn = "";
                string numeroMexicoOut = "";
                string numeroCanadaIn = "";

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal,AreaPersonal " +
                         "FROM ope.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal,AreaPersonal " +
                         "FROM ope.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                    }
                    else if (area == "OP")
                    {
                        numeroMexicoIn = numero.StartsWith("52") ? "521" + numero.Substring(2) : numero;
                        numeroMexicoOut = numero.StartsWith("521") ? "52" + numero.Substring(3) : numero;
                        numeroCanadaIn = numero.StartsWith("1") ? "11" + numero.Substring(1) : numero;

                        query = @$"SELECT	DISTINCT
                                		Numero
                                		,Tipo
                                		,Mensaje
                                		,IdPersonal
                                		,IdAlumno
                                		,IdPais
                                		,Registro
                                		,FechaCreacion
                                		,NombrePersonal
                                		,AreaPersonal
                                		,MAX(EstadoMensaje)		AS EstadoMensaje
                                		,MAX(final.FechaEstado) AS FechaEstado
                                FROM(
                                	SELECT
                                		resultado.WaId
                                		,Numero
                                		,Tipo
                                		,Mensaje
                                		,IdPersonal
                                		,ISNULL(IdAlumno, 0)	IdAlumno
                                		,resultado.IdPais
                                		,Registro
                                		,resultado.FechaCreacion
                                		,NombrePersonal
                                		,AreaAbrev
                                        ,resultado.AreaPersonal
                                		,estado.WaStatus
                                		,CASE
                                			WHEN estado.WaStatus='sent'
                                				THEN 1
                                			WHEN estado.WaStatus='delivered'
                                				THEN 2
                                			WHEN estado.WaStatus='read'
                                				THEN 3
                                			END					AS EstadoMensaje
                                		,estado.FechaCreacion	AS FechaEstado
                                	FROM
                                		ope.V_HistorialChatWhatsAppApiOperaciones	AS resultado
                                	LEFT JOIN(
                                		SELECT
                                			WaId
                                			,WaStatus
                                			,FechaCreacion
                                		FROM
                                			mkt.T_WhatsAppEstadoMensajeEnviado
                                		UNION
                                		SELECT
                                			WaId
                                			,WaStatus
                                			,FechaCreacion
                                		FROM
                                			ope.T_WhatsAppEstadoMensajeEnviadoAtc
                                	)												AS estado ON estado.WaId=resultado.WaId
                                	WHERE
                                		(Numero='{numeroMexicoIn}' OR Numero='{numeroMexicoOut}' OR Numero='{numeroCanadaIn}' OR Numero=@numero)
                                ) AS final
                                GROUP BY
                                	Numero
                                	,Tipo
                                	,Mensaje
                                	,IdPersonal
                                	,IdAlumno
                                	,IdPais
                                	,Registro
                                	,FechaCreacion
                                	,NombrePersonal
                                	,AreaPersonal
                                ORDER BY
                                	FechaCreacion ASC;";
                    }
                    else if (area == "OP-DOC")
                    {
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal,AreaPersonal, MAX(EstadoMensaje) AS EstadoMensaje
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaPersonal,AreaAbrev,estado.WaStatus,
		                                CASE
			                                WHEN estado.WaStatus='sent'
				                                THEN 1
			                                WHEN estado.WaStatus='delivered'
				                                THEN 2
			                                WHEN estado.WaStatus='read'
				                                THEN 3
		                                END AS EstadoMensaje
	                                FROM ope.V_HistorialChatWhatsApp AS resultado
	                                LEFT JOIN mkt.T_WhatsAppEstadoMensajeEnviado AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero AND AreaAbrev='OP'
                                ) AS final
                                GROUP BY 
                                Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal,AreaPersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal,AreaPersonal " +
                         "FROM ope.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesOperacionesDTO>>(credencialTokenExpiraDB);

                List<StringDTO> ListaPalabrasOfensivas = new List<StringDTO>();
                string query1 = string.Empty;
                query1 = "SELECT PalabraFiltrada AS Valor FROM mkt.T_DiccionarioPalabraOfensiva";
                var respuesta = _dapperRepository.QueryDapper(query1, null);
                ListaPalabrasOfensivas = JsonConvert.DeserializeObject<List<StringDTO>>(respuesta);

                List<WhatsAppHistorialMensajesOperacionesDTO> ListaMensajesWhatsAppRespondidosActualizada = new List<WhatsAppHistorialMensajesOperacionesDTO>();
                string palabraOfensivaComparar = "";
                string armarPalabraSensurada = "";

                foreach (WhatsAppHistorialMensajesOperacionesDTO alumnoDatos in listaMensajes)
                {
                    string UltimoMensaje = alumnoDatos.Mensaje;
                    if (UltimoMensaje != null)
                    {
                        //UltimoMensaje = alumnoDatos.Mensaje?.ToLower();
                        foreach (var palabraOfensiva in ListaPalabrasOfensivas)
                        {
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToString();
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToLower();
                            if (UltimoMensaje.ToLower().Contains(palabraOfensivaComparar))
                            {
                                armarPalabraSensurada = palabraOfensivaComparar.Substring(0, 1);
                                int switCaracter = 0;
                                for (int i = 0; i < palabraOfensivaComparar.Length - 1; i++)
                                {
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "#";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "&";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "/";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                }
                                UltimoMensaje = UltimoMensaje.Replace(palabraOfensivaComparar, armarPalabraSensurada);
                            }
                        }
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    else
                    {
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    ListaMensajesWhatsAppRespondidosActualizada.Add(alumnoDatos);
                }
                listaMensajes.Clear();
                listaMensajes.AddRange(ListaMensajesWhatsAppRespondidosActualizada);

                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatAtc(int idPersonal, string numero, string area, string idPais)
        {
            try
            {
                List<WhatsAppHistorialMensajesDTO> listaMensajes = new List<WhatsAppHistorialMensajesDTO>();
                var query = string.Empty;

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                    else if (area == "OP" && idPais != "51")
                    {
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje, MAX(final.FechaEstado) AS FechaEstado
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
		                                 WHEN estado.WaStatus='sent'
			                                THEN 1
		                                 WHEN estado.WaStatus='delivered'
			                                THEN 2
		                                 WHEN estado.WaStatus='read'
			                                THEN 3
		                                END AS EstadoMensaje,
		                                estado.FechaCreacion AS FechaEstado
	                                FROM mkt.V_HistorialChatWhatsApp AS resultado
	                                LEFT JOIN mkt.T_WhatsAppEstadoMensajeEnviado AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero
                                ) AS final
                                GROUP BY Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else if (area == "OP" && idPais == "51")
                    {
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje, MAX(final.FechaEstado) AS FechaEstado
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
		                                 WHEN estado.WaStatus='sent'
			                                THEN 1
		                                 WHEN estado.WaStatus='delivered'
			                                THEN 2
		                                 WHEN estado.WaStatus='read'
			                                THEN 3
		                                END AS EstadoMensaje,
		                                estado.FechaCreacion AS FechaEstado
	                                FROM [ope].[V_HistorialChatWhatsAppAtc] AS resultado
	                                LEFT JOIN ope.T_WhatsAppEstadoMensajeEnviadoAtc AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero
                                ) AS final
                                GROUP BY Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else if (area == "OP-DOC")
                    {
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
			                                WHEN estado.WaStatus='sent'
				                                THEN 1
			                                WHEN estado.WaStatus='delivered'
				                                THEN 2
			                                WHEN estado.WaStatus='read'
				                                THEN 3
		                                END AS EstadoMensaje
	                                FROM mkt.V_HistorialChatWhatsApp AS resultado
	                                LEFT JOIN mkt.T_WhatsAppEstadoMensajeEnviado AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero AND AreaAbrev='OP'
                                ) AS final
                                GROUP BY 
                                Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesDTO>>(credencialTokenExpiraDB);

                List<StringDTO> ListaPalabrasOfensivas = new List<StringDTO>();
                string query1 = string.Empty;
                query1 = "SELECT PalabraFiltrada AS Valor FROM mkt.T_DiccionarioPalabraOfensiva";
                var respuesta = _dapperRepository.QueryDapper(query1, null);
                ListaPalabrasOfensivas = JsonConvert.DeserializeObject<List<StringDTO>>(respuesta);

                List<WhatsAppHistorialMensajesDTO> ListaMensajesWhatsAppRespondidosActualizada = new List<WhatsAppHistorialMensajesDTO>();
                string palabraOfensivaComparar = "";
                string armarPalabraSensurada = "";

                foreach (WhatsAppHistorialMensajesDTO alumnoDatos in listaMensajes)
                {
                    string UltimoMensaje = alumnoDatos.Mensaje;
                    if (UltimoMensaje != null)
                    {
                        //UltimoMensaje = alumnoDatos.Mensaje?.ToLower();
                        foreach (var palabraOfensiva in ListaPalabrasOfensivas)
                        {
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToString();
                            palabraOfensivaComparar = palabraOfensiva.Valor?.ToLower();
                            if (UltimoMensaje.ToLower().Contains(palabraOfensivaComparar))
                            {
                                armarPalabraSensurada = palabraOfensivaComparar.Substring(0, 1);
                                int switCaracter = 0;
                                for (int i = 0; i < palabraOfensivaComparar.Length - 1; i++)
                                {
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "#";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "&";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "/";
                                    }
                                    if (switCaracter < palabraOfensivaComparar.Length - 1)
                                    {
                                        switCaracter++;
                                        armarPalabraSensurada = armarPalabraSensurada + "%";
                                    }
                                }
                                UltimoMensaje = UltimoMensaje.Replace(palabraOfensivaComparar, armarPalabraSensurada);
                            }
                        }
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    else
                    {
                        alumnoDatos.Mensaje = UltimoMensaje;
                    }
                    ListaMensajesWhatsAppRespondidosActualizada.Add(alumnoDatos);
                }
                listaMensajes.Clear();
                listaMensajes.AddRange(ListaMensajesWhatsAppRespondidosActualizada);

                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibido(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;
                query = @"SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, 
                            wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null 
                            THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno  
                            FROM mkt.V_UltimoChatWhatsAppContactoRecibido wa 
                            LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id 
                            WHERE wa.IdPersonal=@idPersonal Order by wa.FechaCreacion Desc";
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 12/09/2022
        /// <summary>
        /// Obtiene Lista de último mensaje enviado por IdPersonal
        /// </summary>
        /// <param name="idPersonal">Id de personal</param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsEnviado(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;
                query = "mkt.SP_UltimoChatWhatsAppContactoEnviado";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(resultado);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        ///Repositorio: WhatsAppMensajeEnviadoRepositorio
        ///Autor: Jonathan Caipo
        ///Fecha: 13/03/2021
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppHistorialMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialMensajesDTO> listaMensajes = new List<WhatsAppHistorialMensajesDTO>();
                var query = string.Empty;

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                    "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaModificacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaModificacion Asc";
                    }
                    else if (area == "OP")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                        "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaModificacion Asc";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                        "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaModificacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesDTO>>(credencialTokenExpiraDB)!;
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadas(string plantilla, string numero)
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT TOP 1 Id FROM mkt.T_WhatsAppMensajeEnviado WHERE WaRecipientType='hsm' AND WaBody=@Plantilla AND WaTo=@Numero AND FechaCreacion > GETDATE()-1";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { plantilla, numero });
            if (queryAsesor == "null" || queryAsesor == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// Autor: Christian Quispe
        /// Fecha: 23/10/2023
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadasApiComercial(string plantilla, string numero)
        {
            string _query = "SELECT TOP 1 Id FROM com.T_WhatsAppMensajeEnviadoCom WHERE WaType='hsm' AND WaBody=@Plantilla AND WaTo=@Numero";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { plantilla, numero });
            return (queryAsesor == "null" || queryAsesor == "") ? false : true; //false->envia , true->no envia 
        }
        /// Autor: Christian Quispe
        /// Fecha: 23/10/2023
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <param name="idPersonal"> id Personal </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadasApiComercialPersonal(string plantilla, string numero, int idPersonal)
        {
            string _query = "SELECT TOP 1 Id FROM com.T_WhatsAppMensajeEnviadoCom WHERE WaType='hsm' AND WaBody=@Plantilla AND WaTo=@Numero AND IdPersonal=@IdPersonal";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { plantilla, numero, idPersonal });
            return (queryAsesor == "null" || queryAsesor == "") ? false : true; //false->envia , true->no envia 
        }
        /// Autor: Christian Quispe
        /// Fecha: 23/10/2023
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadasApiComercial(string plantilla, string numero, DateTime fechaUltimoMensajeRecibido)
        {
            string _query = "SELECT TOP 1 Id FROM com.T_WhatsAppMensajeEnviadoCom WHERE WaType='hsm' AND WaBody=@Plantilla AND WaTo=@Numero AND FechaCreacion > @FechaUltimoMensajeRecibido";

            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { plantilla, numero, fechaUltimoMensajeRecibido });
            if (queryAsesor == "null" || queryAsesor == "")
            {
                string _query2 = "SELECT TOP 1 Id FROM com.T_WhatsAppMensajeEnviadoCom WHERE WaType='hsm' AND WaBody=@Plantilla AND WaTo=@Numero AND FechaCreacion > GETDATE()-1";
                var queryAsesor2 = _dapperRepository.FirstOrDefault(_query2, new { plantilla, numero });
                return (queryAsesor2 == "null" || queryAsesor2 == "") ? false : true; //false->envia , true->no envia 

                //si se resetea por cada mensaje del cliente
                //return false;

            }
            else
            {
                return true;
            }
        }
        /// Autor: Christian Quispe
        /// Fecha: 23/10/2023
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarMesajeRecibidosApiComercial(string numero)
        {
            string _query = "SELECT TOP 1 Id FROM com.T_WhatsAppMensajeRecibidoCom WHERE WaFrom=@numero AND FechaCreacion > GETDATE()-1";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { numero });
            return (queryAsesor == "null" || queryAsesor == "") ? false : true;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero)
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT TOP 1 Id FROM ope.T_WhatsAppMensajeEnviadoAtc WHERE WaType='hsm' AND WaBody=@Plantilla AND WaTo=@Numero AND FechaCreacion > GETDATE()-1";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { plantilla, numero });
            if (queryAsesor == "null" || queryAsesor == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidarMesajesEnviadosEn24Horas(string numero)
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT TOP 1 Id FROM mkt.T_WhatsAppMensajeRecibido WHERE WaFrom=@Numero AND FechaCreacion > GETDATE()-1";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { numero });

            string _query2 = "SELECT TOP 1 Id FROM ope.T_WhatsAppMensajeRecibidoAtc WHERE WaFrom=@Numero AND FechaCreacion > GETDATE()-1";
            var queryAsesor2 = _dapperRepository.FirstOrDefault(_query2, new { numero });

            var queryFinalAsesor = queryAsesor == "null" || queryAsesor == "" ? queryAsesor2 : queryAsesor;

            if (queryFinalAsesor == "null" || queryFinalAsesor == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidarMesajesEnviadosEn24HorasComercial(string Numero, string CuentaIdentificadorWA)
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = @"SELECT TOP 1 WAMRC.id
                            FROM com.T_WhatsAppMensajeRecibidoCom WAMRC
		                            WHERE WAMRC.WaFrom=@numero AND WAMRC.FechaCreacion > GETDATE()-1 AND  WAMRC.PhoneNumberId = @cuentaIdentificadorWA";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { numero = Numero, cuentaIdentificadorWA = CuentaIdentificadorWA });
            return (queryAsesor == "null" || queryAsesor == "") ? true : false;
        }
        public MensajeChatDTO UltimoMensajeRecibido(string Numero, string CuentaIdentificadorWA)
        {
            MensajeChatDTO Mensaje = new MensajeChatDTO();
            string _query = @"SELECT TOP 1 WAMRC.id,WAMRC.FechaCreacion FechaMensaje
                            FROM com.T_WhatsAppMensajeRecibidoCom WAMRC
		                            WHERE WAMRC.WaFrom=@numero AND  WAMRC.PhoneNumberId = @cuentaIdentificadorWA order by fechacreacion desc";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { numero = Numero, cuentaIdentificadorWA = CuentaIdentificadorWA });

            if (queryAsesor == "null" || queryAsesor == "")
            {
                return null;
            }
            else
            {
                Mensaje = JsonConvert.DeserializeObject<MensajeChatDTO>(queryAsesor);
                return Mensaje;
            }

        }
        public bool ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero)
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT TOP 1 Id FROM ope.T_WhatsAppMensajeRecibidoAtc WHERE WaFrom=@Numero AND FechaCreacion > GETDATE()-1";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, new { numero });
            if (queryAsesor == "null" || queryAsesor == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene Lista de último mensaje de chat por IdPersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChats(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var _query = string.Empty;
                _query = @"SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno  
                          FROM mkt.V_UltimoChatWhatsAppContacto wa  
                          LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id  
                          LEFT JOIN conf.T_ClasificacionPersona cp on cp.IdTablaOriginal=al.Id         
                          WHERE cp.Id=@idPersonal 
                           Order by wa.FechaCreacion Desc";
                var CredencialTokenExpiraDB = _dapperRepository.QueryDapper(_query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene el historial en base al tipo de agenda
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Numero de celular </param>
        /// <param name="Area"> Area del asesor </param>
        /// <param name="idTipoAgenda"> Id del tipo de Agenda </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area, int idTipoAgenda)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var _query = string.Empty;

                if (idTipoAgenda == 3)
                {
                    _query = $@"
                                SELECT Numero, 
                                        Mensaje, 
                                        IdPersonal, 
                                        FechaCreacion, 
                                        IdPais, 
                                        IdAlumno, 
                                        NombreAlumno
                                FROM ope.V_ObtenerHistorialChatWhatsAppRecibidoProveedor
                                WHERE Numero = @Numero
                                ORDER BY FechaCreacion DESC;
                                ";
                }
                else
                {
                    if (area == "VE")
                    {
                        _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                             "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
                             "LEFT JOIN mkt.T_Alumno al  WITH (NOLOCK) on wa.IdAlumno=al.Id " +
                             "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@Numero Order by wa.FechaCreacion Desc";
                    }
                    else if (area == "OP")
                    {
                        _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                             "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
                             "LEFT JOIN mkt.T_Alumno al  WITH (NOLOCK) on wa.IdAlumno=al.Id " +
                             "WHERE wa.Numero=@Numero Order by wa.FechaCreacion Desc";
                    }
                    else
                    {
                        _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                             "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
                             "LEFT JOIN mkt.T_Alumno al WITH (NOLOCK)  on wa.IdAlumno=al.Id " +
                             "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@Numero Order by wa.FechaCreacion Desc";
                    }
                }

                var CredencialTokenExpiraDB = _dapperRepository.QueryDapper(_query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene mensaje multimedia de WhatsApp
        /// </summary>
        /// <param name="waId"> Id de chat WhatsApp </param>
        /// <returns> String </returns>
        public string ObtenerMensajeMultimedia(string waId)
        {
            try
            {
                WhatsAppHistorialMensajesDTO whatsAppHistorialMensajesDTO = new WhatsAppHistorialMensajesDTO();
                var _query = string.Empty;
                _query = @"SELECT Mensaje FROM mkt.V_HistorialChatWhatsApp WHERE WaId=@WaId";

                var CredencialTokenExpiraDB = _dapperRepository.FirstOrDefault(_query, new { WaId = waId });
                whatsAppHistorialMensajesDTO = JsonConvert.DeserializeObject<WhatsAppHistorialMensajesDTO>(CredencialTokenExpiraDB);
                return whatsAppHistorialMensajesDTO.Mensaje;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene conversacion por numero.
        /// </summary>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerConversacionNumero(string numero)
        {
            PersonalAlumnoDTO Conversacion = new PersonalAlumnoDTO();
            string _queryConversacion = "Select mw.IdPersonal,ISNULL(mw.IdAlumno,0) IdAlumno From mkt.V_TWhatsAppMensajeEnviado_ObtenerAsesorAlumno mw inner join gp.T_Personal pe on mw.IdPersonal=pe.Id Where pe.Activo=1 and mw.WaTo=@Numero Order by mw.FechaCreacion desc";
            var queryConversacion = _dapperRepository.FirstOrDefault(_queryConversacion, new { Numero = numero });
            if (queryConversacion == null || queryConversacion == "")
            {
                return null;
            }
            else
            {
                Conversacion = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryConversacion);
                return Conversacion;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene personal con minimo de chats por personal.
        /// </summary>
        /// <returns> PersonalNumeroMinimoChatDTO </returns>
        public PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChat()
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT TOP 1 WU.IdPersonal,ISNULL(CON.NumeroChats,0) AS NumeroChats " +
                            "FROM mkt.T_WhatsAppUsuario WU " +
                            "INNER JOIN gp.T_Personal PER ON WU.IdPersonal=PER.Id " +
                            "LEFT JOIN mkt.V_UltimoChatWhatsAppContactoByAsesores CON ON WU.IdPersonal=CON.IdPersonal " +
                            "WHERE WU.Estado=1 AND PER.Rol<>'Sistemas' " +
                            "ORDER BY CON.NumeroChats ASC";
            var queryAsesor = _dapperRepository.FirstOrDefault(_query, null);
            if (queryAsesor == null || queryAsesor == "")
            {
                return null;
            }
            else
            {
                Conversacion = JsonConvert.DeserializeObject<PersonalNumeroMinimoChatDTO>(queryAsesor);
                return Conversacion;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal ordenado por Fecha Modificación para Control de Mensajes Ofensivos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibidoControlMensaje(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;
                query = "mkt.SP_UltimoChatWhatsAppContactoRecibidoControlMensajes";
                var credencialTokenExpiraDB = _dapperRepository.QuerySPDapper(query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 05/06/2023
        /// <summary>
        /// Obtiene información de api whatsApp
        /// </summary>
        /// <param name="idPais"> Id de Pais </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<InfoApiWhatsappDTO> ListaInformacionApiWhatsapp(int idPais)
        {
            try
            {
                List<InfoApiWhatsappDTO> listaInformacion = new List<InfoApiWhatsappDTO>();
                var query = string.Empty;
                //Si el Id Pais es 0 se obtiene todas las configuraciones de lo contrario se obtiene la información del pais indicado
                if (idPais == 0)
                {
                    query = "SELECT Numero, VName, IdPais, Bearer, NumeroIndentificador, VersionApi, FechaModificacion " +
                    "FROM conf.T_WhatsAppConfiguracionApi " +
                    "WHERE Estado = 1";
                }
                else
                {
                    query = "SELECT Numero, VName, IdPais, Bearer, NumeroIndentificador, VersionApi, FechaModificacion " +
                    "FROM conf.T_WhatsAppConfiguracionApi " +
                    "WHERE Estado = 1" + " AND  IdPais =" + idPais.ToString();
                }

                var data = _dapperRepository.QueryDapper(query, null);
                listaInformacion = JsonConvert.DeserializeObject<List<InfoApiWhatsappDTO>>(data);
                return listaInformacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketing(int Tab, int Dia)
        {
            try
            {
                List<ChatWhatsAppMarketingDTO> ChatWhatsAppMarketing = new List<ChatWhatsAppMarketingDTO>();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerChatWhatsAppMarketing", new { Tab, Dia });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ChatWhatsAppMarketing = JsonConvert.DeserializeObject<List<ChatWhatsAppMarketingDTO>>(resultado);
                }
                return ChatWhatsAppMarketing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Desconocido
        /// Autor Edicion: Humberto Oscata
        /// Version: 1.1
        /// <summary>
        /// Obtiene el listado de ultimos mensajes por cliente, para una rango de fecha especifico
        /// </summary>
        /// <param name="tab">Tipo de mensajes a devolver</param>
        /// <param name="fechaInicio">Fecha inicio del rango</param>
        /// <param name="fechaFin">Fecha fin del rango</param>
        /// <returns>Lista de ultimos mensajes por cliente</returns>
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketingv2(int tab, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {               
                var jsonResultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerChatWhatsAppMarketingV2", new { Tab = tab, FechaInicio = fechaInicio, FechaFin = fechaFin });
                if (!string.IsNullOrEmpty(jsonResultado) && !jsonResultado.Equals("[]"))
                {
                    var resultado = JsonConvert.DeserializeObject<List<ChatWhatsAppMarketingDTO>>(jsonResultado)!;
                    
                    return resultado.OrderByDescending(x => x.Fecha)
                        .ThenByDescending(x => x.Tiempo)
                        .ToList();
                }

                return new List<ChatWhatsAppMarketingDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 02/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppFacebookMarketing(int Tab, int Dia, int IdAsesor)
        {
            try
            {
                List<ChatWhatsAppMarketingDTO> ChatWhatsAppMarketing = new List<ChatWhatsAppMarketingDTO>();

                var parametros = new
                {
                    Tab,
                    Dia,
                    IdAsesor = IdAsesor == 0 ? (int?)null : IdAsesor
                };

                var resultado = _dapperRepository.QuerySPDapper("[mkt].[SP_PW_WhatsAppObtenerChatMarketing]", parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ChatWhatsAppMarketing = JsonConvert.DeserializeObject<List<ChatWhatsAppMarketingDTO>>(resultado);
                }

                return ChatWhatsAppMarketing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EsAsesorVentasValido(int idAsesor)
            {
                try
                {
                    string query = @"
            SELECT COUNT(1)
            FROM mkt.V_WhatsAppPersonalVentasActivos
            WHERE IdPersonal_Asignado = @IdAsesor";

                    string resultadoJson = _dapperRepository.QueryDapper(query, new { IdAsesor = idAsesor });
                    var array = JArray.Parse(resultadoJson);
                    var obj = (JObject)array[0];
                    int cantidad = obj.Properties().First().Value.Value<int>();

                    return cantidad > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al validar asesor ventas: {ex.Message}");
                }
            }

        



        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ObtenerChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerChatWhatsAppMarketingPorCelular", new { Celular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ChatWhatsAppMarketing = JsonConvert.DeserializeObject<List<ObtenerChatWhatsAppMarketingPorCelularDTO>>(resultado);
                }
                return ChatWhatsAppMarketing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ObtenerChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingMasivoPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerChatWhatsAppMarketingMasivoPorCelular", new { Celular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ChatWhatsAppMarketing = JsonConvert.DeserializeObject<List<ObtenerChatWhatsAppMarketingPorCelularDTO>>(resultado);
                }
                return ChatWhatsAppMarketing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ObtenerChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingBusquedaPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerChatWhatsAppMarketingBusquedaPorCelular", new { Celular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ChatWhatsAppMarketing = JsonConvert.DeserializeObject<List<ObtenerChatWhatsAppMarketingPorCelularDTO>>(resultado);
                }
                return ChatWhatsAppMarketing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public bool ArchivarChat(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ArchivarChatWhatsApp", new { Celular, IdAlumno, IdPersonal, UsuarioModificacion });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// DesArchiva Chat de wsp
        /// </summary>
        public bool DesArchivarChat(string Celular, string UsuarioModificacion)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_DesArchivarChatWhatsApp", new { Celular, UsuarioModificacion });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public bool Desuscribir(string Celular, int IdAlumno, string UsuarioModificacion)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_DesuscribirAlumno", new { Celular, IdAlumno, UsuarioModificacion });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// DesArchiva Chat de wsp
        /// </summary>
        public bool SuscribirAlumno(string Celular, int IdAlumno, string UsuarioModificacion)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_SuscribirAlumno", new { Celular, IdAlumno, UsuarioModificacion });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para mostrarse en combo.
        /// </summary>
        public List<ComboDTO> ObtenerComboIndustria()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_Industria WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margior Ramirez
        /// Fecha: 04/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para mostrarse en combo.
        /// </summary>
        public List<ComboTamanioEmpresa> ObtenerComboTamanioEmpresa()
        {
            try
            {
                List<ComboTamanioEmpresa> rpta = new List<ComboTamanioEmpresa>();
                var query = "SELECT Id,Nombre FROM  pla.T_TamanioEmpresaAgenda WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboTamanioEmpresa>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para mostrarse en combo.
        /// </summary>
        public List<ComboDTO> ObtenerComboAreaFormacion()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_AreaFormacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para mostrarse en combo.
        /// </summary>
        public List<ComboDTO> ObtenerComboAreaTrabajo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_AreaTrabajo WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para mostrarse en combo.
        /// </summary>
        public List<ComboDTO> ObtenerComboCargo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_Cargo WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros del alumno para mostrarse
        /// </summary>
        public ObtenerAtributosAlumnoDTO ObtenerDatosAlumnoWhatsApp(int IdAlumno)
        {
            try
            {
                ObtenerAtributosAlumnoDTO ObtenerAtributosAlumno = new ObtenerAtributosAlumnoDTO();
                var query = string.Empty;
                query = @"SELECT 
	                        ALU.Id,
	                        ALU.Nombre1,
	                        ALU.Nombre2,
	                        ALU.Celular,
	                        ALU.Celular2,
                            ALU.Telefono,
	                        ALU.ApellidoPaterno,
	                        ALU.ApellidoMaterno,
	                        ALU.Email1,
	                        ALU.Email2,
                            ALU.DNI,
	                        COALESCE(ALU.IdIndustria, 0) AS IdIndustria,
	                        COALESCE(ALU.IdAFormacion, 0) AS IdAFormacion,
	                        COALESCE(ALU.IdATrabajo, 0) AS IdATrabajo,
	                        COALESCE(ALU.IdCargo, 0) AS IdCargo,
                            COALESCE(ALU.IdTamanioEmpresaAgenda, 0) AS IdTamanioEmpresaAgenda,
	                        COALESCE(Desuscrito.Estado, 0) AS Desuscrito,
	                        COALESCE(Archivado.Estado, 0) AS Archivado
                        FROM mkt.T_Alumno AS ALU
                        OUTER APPLY (
	                        SELECT TOP 1 WD.Estado
	                        FROM mkt.T_WhatsAppDesuscrito AS WD
	                        WHERE WD.Estado = 1 AND WD.NumeroTelefono = (SELECT TOP 1 WaFrom FROM mkt.T_WhatsAppMensajeRecibidoMkt WHERE IdAlumno = @IdAlumno)
	                        ORDER BY WD.Id DESC
                        )AS Desuscrito
                        OUTER APPLY (
	                        SELECT TOP 1 WA.Estado
	                        FROM mkt.T_WhatsAppChatArchivado AS WA
	                        WHERE WA.Estado = 1 AND WA.Celular = (SELECT TOP 1 WaFrom FROM mkt.T_WhatsAppMensajeRecibidoMkt WHERE IdAlumno = @IdAlumno)
	                        ORDER BY WA.Id DESC
                        )AS Archivado
                        WHERE ALU.Id = @IdAlumno";
                var credencialTokenExpiraDB = _dapperRepository.FirstOrDefault(query, new { IdAlumno });
                ObtenerAtributosAlumno = JsonConvert.DeserializeObject<ObtenerAtributosAlumnoDTO>(credencialTokenExpiraDB);
                return ObtenerAtributosAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros del alumno para mostrarse
        /// </summary>
        public ObtenerAtributosAlumnoOriginalDTO ObtenerDatosOriginalesAlumnoWhatsApp(int? IdAlumno)
        {
            try
            {
                ObtenerAtributosAlumnoOriginalDTO ObtenerAtributosAlumno = new ObtenerAtributosAlumnoOriginalDTO();
                var query = string.Empty;
                query = @"SELECT 
	                        ALU.Id,
	                        ALU.Nombre1,
	                        ALU.Nombre2,
	                        ALU.ApellidoPaterno,
	                        ALU.ApellidoMaterno,
	                        ALU.Email1,
	                        ALU.Email2,
                            ALU.Celular,
                            ALU.Telefono,
	                        COALESCE(ALU.IdIndustria, 0) AS IdIndustria,
	                        COALESCE(ALU.IdAFormacion, 0) AS IdAFormacion,
	                        COALESCE(ALU.IdATrabajo, 0) AS IdATrabajo,
	                        COALESCE(ALU.IdCargo, 0) AS IdCargo,
                            COALESCE(ALU.IdTamanioEmpresaAgenda, 0) AS IdTamanioEmpresaAgenda
                          
                        FROM mkt.T_Alumno AS ALU
                        WHERE ALU.Id = @IdAlumno";
                var credencialTokenExpiraDB = _dapperRepository.FirstOrDefault(query, new { IdAlumno });
                ObtenerAtributosAlumno = JsonConvert.DeserializeObject<ObtenerAtributosAlumnoOriginalDTO>(credencialTokenExpiraDB);
                return ObtenerAtributosAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 28/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros del alumno para mostrarse
        /// </summary>
        public List<HistorialAlumnoDTO> ObtenerHistorialAlumnoWhatsApp(int IdAlumno)
        {
            try
            {
                List<HistorialAlumnoDTO> ObtenerAtributosAlumno = new List<HistorialAlumnoDTO>();
                var query = string.Empty;
                query = @"SELECT IdOportunidad,IdAlumno,CelularWhatsApp,IdCampaniaGeneralDetalleWhatsApp,IdCentroCosto,Tipo,Categoria,FechaCreacion, ChatValido,ChatInValido,ChatOportunidad  FROM mkt.V_HistorialAlumnoWhatsApp WHERE IdAlumno = @IdAlumno ORDER BY FechaCreacion DESc";
                var credencialTokenExpiraDB = _dapperRepository.QueryDapper(query, new { IdAlumno = IdAlumno });
                ObtenerAtributosAlumno = JsonConvert.DeserializeObject<List<HistorialAlumnoDTO>>(credencialTokenExpiraDB);
                return ObtenerAtributosAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza datos del alumno
        /// </summary>
        public bool ActualizarDatosAlumno(int? IdAlumno, string CampoActualizado, string? ValorAnterior, string? ValorNuevo, string Usuario)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ActualizarAlumnoLog", new { IdAlumno, CampoActualizado, ValorAnterior, ValorNuevo, Usuario });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool InsertarMensajesLogJsonEnvios(int? IdAlumno, string Numero, string Mensaje)
        {
            try
            {
                var _resultado = new IntDTO();
                //Mensaje = Mensaje.Replace("#", "").Replace("%","");
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_InsertarWhatsappEnviosLog", new { IdAlumno, Numero, Mensaje });
                //_resultado = JsonConvert.DeserializeObject<IntDTO>(resultado);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<ProbabilidaWhatsAppDTO> ObtenerProbabilidadPorOportunidad(int idOportunidad)
        {
            try
            {
                var query = @"SELECT IdOportunidad,IdProbabilidadRegistroPW, Probabilidad
              FROM mkt.V_ModeloPredictivoProbabilidadIdProbabilidadRegistro
              WHERE IdOportunidad = @idOportunidad";

                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                List<ProbabilidaWhatsAppDTO> listaProbabilidad = JsonConvert.DeserializeObject<List<ProbabilidaWhatsAppDTO>>(resultado);
                return listaProbabilidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ProgramaPorOportunidadDTO> ObtenerProgramaPorOportunidadWhatsapp(int idOportunidad)
        {

            try
            {
                var query = @"SELECT IdOportunidad,IdArea,IdCentroCosto,IdClasificacionPersona,IdAlumno,CentroCostoNombre,IdEspecifico,EspecificoNombre,IdPGeneral,ProgramaNombre FROM mkt.V_ProgramaPorOportunidadWhatsapp  WHERE IdOportunidad= @idOportunidad";

                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                List<ProgramaPorOportunidadDTO> nombrePrograma = JsonConvert.DeserializeObject<List<ProgramaPorOportunidadDTO>>(resultado);
                return nombrePrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<OportunidadVentaCruzadaWhatsappDTO> ObtenerVentaCruzadaPorIdAlumnoWhatsapp(int idAlumno, int idArea, int idPGeneral)
        {
            try
            {
                List<OportunidadVentaCruzadaWhatsappDTO> listaVentaCruzada = new List<OportunidadVentaCruzadaWhatsappDTO>();

                var resultado = _dapperRepository.QuerySPDapper(
                    "[mkt].[SP_Oportunidad_VentaCruzadaTop3_PorPrograma]",
                    new { IdAlumno = idAlumno, IdArea = idArea, IdPGeneral = idPGeneral }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaVentaCruzada = JsonConvert.DeserializeObject<List<OportunidadVentaCruzadaWhatsappDTO>>(resultado)!;
                }

                return listaVentaCruzada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerPersonalOportunidad()
        {
            try
            {
                List<ComboDTO> personal = new List<ComboDTO>();
                string _query = @"SELECT   
                           Id, CONCAT(Nombres, ' ', Apellidos) As Nombre   
                         FROM gp.T_Personal   
                         WHERE Activo = 1";
                var res = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    personal = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return personal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AsesorActualDTO ObtenerIdAsesorActual(int idOportunidad)
        {
            try
            {
                AsesorActualDTO asesorActual = null;
                string query = @"SELECT Id AS IdOportunidad, IdPersonal_Asignado AS IdAsesor
                         FROM com.T_Oportunidad
                         WHERE Id = @IdOportunidad AND Estado = 1";

                var parametros = new { IdOportunidad = idOportunidad };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var listaAsesorActual = JsonConvert.DeserializeObject<List<AsesorActualDTO>>(resultado);
                    asesorActual = listaAsesorActual.FirstOrDefault();
                }

                return asesorActual ?? new AsesorActualDTO { IdOportunidad = idOportunidad, IdAsesor = 0 };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el asesor actual: " + ex.Message);
            }
        }

        public ModeloPredictivoDTO ObtenerModeloPredictivoPorAlumnoYPrograma(int idAlumno, int idPGeneral)
        {
            try
            {
                var query = @"
                SELECT 
                    IdAlumno,
                    IdPGeneral,
                    Probabilidad,
                    Tipo,
                    IdModeloPredictivoTipo
                FROM 
                    mkt.T_ModeloPredictivoProbabilidadAlumnoPorPrograma
                WHERE 
                    IdAlumno = @idAlumno AND IdPGeneral = @idPGeneral";

                var resultado = _dapperRepository.QueryDapper(query, new { idAlumno, idPGeneral });

                ModeloPredictivoDTO modeloPredictivo = JsonConvert.DeserializeObject<List<ModeloPredictivoDTO>>(resultado).FirstOrDefault();

                return modeloPredictivo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void EliminarRegistroModeloPredictivoPorAlumno(int idAlumno)
        {
            try
            {
                var query = @"
            DELETE FROM mkt.T_ModeloPredictivoProbabilidadAlumnoPorPrograma
            WHERE IdAlumno = @idAlumno AND Estado = 1";

                _dapperRepository.QueryDapper(query, new { idAlumno });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el registro para el alumno con Id {idAlumno} y Estado = 1: {ex.Message}");
            }
        }
        public bool VerificarActualizarAlumno(int idAlumno)
        {
            var query = @"
                         SELECT 
            COUNT(DISTINCT CONCAT(CAST(FechaModificacion AS DATE), FORMAT(FechaModificacion, 'HH:mm:ss'))) AS TotalActualizaciones
        FROM 
            mkt.T_AlumnoLog
        WHERE 
            IdAlumno = @IdAlumno
            AND CAST(FechaModificacion AS DATE) = CAST(GETDATE() AS DATE);";

            var resultado = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno });

            int cantidadActualizaciones = 0;

            if (!string.IsNullOrEmpty(resultado))
            {
                try
                {
                    var deserializedResult = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(resultado);

                    if (deserializedResult != null && deserializedResult.Count > 0)
                    {
                        cantidadActualizaciones = deserializedResult[0].Values.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deserializando el resultado de la consulta: " + ex.Message);
                }
            }

            return cantidadActualizaciones < 2;
        }





        public void RegistrarActualizacionAlumno(int idAlumno, string usuario)
        {
            var query = @"
          INSERT INTO mkt.T_AlumnoActualizacionRegistro 
        (IdAlumno, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion)
             VALUES 
        (@IdAlumno, 1, @Usuario, @Usuario, GETDATE(), GETDATE());";

            var result = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno, Usuario = usuario });
        }


        /// Autor: Humberto Oscata
        /// Fecha: 28/08/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el rango de probabilidad (A, B, C) de un IdAlumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns>Rango de probabilidad</returns>
        public string ObtenerRangoProbabilidadAlumno(int idAlumno)
        {
            try
            {
                var query = @"SELECT TOP 1
                                        PGPCD.Tipo AS Rango
                                    FROM
                                        com.T_Oportunidad O
                                        INNER JOIN mkt.V_ModeloPredictivoProbabilidadIdProbabilidadRegistro MP
                                            ON O.Id = MP.IdOportunidad
                                        LEFT JOIN pla.T_ProgramaGeneralPuntoCorteDetalle PGPCD
                                            ON PGPCD.IdProgramaGeneralPuntoCorte = ISNULL(MP.IdProgramaGeneralPuntoCorte_Pais, MP.IdProgramaGeneralPuntoCorte_PorDefecto)
                                            AND MP.Probabilidad >= PGPCD.ValorMinimo
                                            AND MP.Probabilidad < PGPCD.ValorMaximo
                                            AND PGPCD.Estado = 1
                                    WHERE
                                        O.IdAlumno = @IdAlumno
                                    ORDER BY
                                        MP.IdOportunidad DESC";

                var response = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });

                return response ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 29/08/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene chats de para WhatsAppMarketing por celular y rango de fecha
        /// </summary>
        /// <param name="celularAlumno">Celular del alumno</param>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>Listadode chats asociados al celularAlumno</returns>
        public List<MensajeExtraccionRegistroDTO> ObtenerChatWhatsAppMarketingPorCelularRangoFecha(string celularAlumno, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<MensajeExtraccionRegistroDTO> ChatsWhatsAppMarketing = new List<MensajeExtraccionRegistroDTO>();

                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerChatWhatsAppMarketingPorCelularRangoFecha", new { Celular = celularAlumno, FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ChatsWhatsAppMarketing = JsonConvert.DeserializeObject<List<MensajeExtraccionRegistroDTO>>(resultado);
                }

                return ChatsWhatsAppMarketing;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
