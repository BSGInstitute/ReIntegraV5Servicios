using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
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

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class WhatsAppMensajeRecibidoPostulanteRepository : GenericRepository<TWhatsAppMensajeRecibidoPostulante>, IWhatsAppMensajeRecibidoPostulanteRepository
    {
        private Mapper _mapper;
        public WhatsAppMensajeRecibidoPostulanteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<WhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulanteDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TWhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulanteDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppMensajeRecibidoPostulante MapeoEntidad(WhatsAppMensajeRecibidoPostulante entidad)
        {
            try
            {
                TWhatsAppMensajeRecibidoPostulante modelo = _mapper.Map<TWhatsAppMensajeRecibidoPostulante>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TWhatsAppMensajeRecibidoPostulante Add(WhatsAppMensajeRecibidoPostulante entidad)
        {
            try
            {
                var WhatsAppMensajeRecibidoPostulante = MapeoEntidad(entidad);
                base.Insert(WhatsAppMensajeRecibidoPostulante);
                return WhatsAppMensajeRecibidoPostulante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TWhatsAppMensajeRecibidoPostulante Update(WhatsAppMensajeRecibidoPostulante entidad)
        {
            try
            {
                var WhatsAppMensajeRecibidoPostulante = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppMensajeRecibidoPostulante.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppMensajeRecibidoPostulante);
                return WhatsAppMensajeRecibidoPostulante;
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
        public IEnumerable<TWhatsAppMensajeRecibidoPostulante> Add(IEnumerable<WhatsAppMensajeRecibidoPostulante> listadoEntidad)
        {
            try
            {
                List<TWhatsAppMensajeRecibidoPostulante> listado = new List<TWhatsAppMensajeRecibidoPostulante>();
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
        public IEnumerable<TWhatsAppMensajeRecibidoPostulante> Update(IEnumerable<WhatsAppMensajeRecibidoPostulante> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppMensajeRecibidoPostulante> listado = new List<TWhatsAppMensajeRecibidoPostulante>();
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

        /// Autor: Eliot Arias F.
        /// Fecha: 17/12/2024
        /// version : 1.2
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene la lista de chats con postulantes asociados al idPersonal
        /// </summary>
        /// <returns>Lista de WhatsAppMensajeEnviadoPostulante</returns>
        public IEnumerable<WhatsAppMensajesPostulanteDTO> WhatsAppUltimoMensajeRecibidosChat(int IdPersonal)
        {
            try
            {
                List<WhatsAppMensajesPostulanteDTO> listaMensajes = new List<WhatsAppMensajesPostulanteDTO>();
                var _query = string.Empty;
                _query = @"
							SELECT wa.Numero, 
								   wa.Mensaje, 
								   wa.IdPersonal, 
								   wa.FechaCreacion, 
								   wa.IdPais, 
								   ISNULL(wa.IdPostulante, 0) IdPostulante,
								   CASE
									   WHEN pos.Nombre IS NULL
									   THEN wa.Numero
									   ELSE RTRIM(CONCAT(pos.Nombre, ' ', pos.ApellidoPaterno, ' ', pos.ApellidoMaterno))
								   END NombrePostulante
							FROM gp.V_UltimoChatWhatsAppPostulante wa
								 LEFT JOIN gp.T_Postulante pos ON wa.IdPostulante = pos.Id
								 LEFT JOIN conf.T_ClasificacionPersona cp ON cp.IdTablaOriginal = pos.Id and cp.IdTipoPersona = 5
							WHERE wa.IdPersonal = @idPersonal
							ORDER BY wa.FechaCreacion DESC;
						"
                ;

                var respta = _dapperRepository.QueryDapper(_query, new { IdPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesPostulanteDTO>>(respta);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 17/12/2024
        /// version : 1.0
        /// <param name="numero"></param> 
        /// <summary>
        /// Valida si el postulante envio un mensaje en las ultimas 24 horas
        /// </summary>
        /// <returns>Lista de WhatsAppMensajeEnviadoPostulante</returns>
        public Boolean ValidarMensajeRecibido24Horas(string numero)
        {
            try
            {
                // Query SQL
                string _query = @"
                            SELECT TOP 1 Id 
                            FROM gp.T_WhatsAppMensajeRecibidoPostulante 
                            WHERE WaFrom = @Numero 
                            AND FechaCreacion > DATEADD(HOUR, -24, GETDATE())";

                var result = _dapperRepository.FirstOrDefault(_query, new { Numero = numero });

                //return string.IsNullOrEmpty(result);
                if (result == "null")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar mensajes recibidos: " + ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 19/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppHistorialPostulanteMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialPostulanteMensajesDTO> listaMensajes = new List<WhatsAppHistorialPostulanteMensajesDTO>();
                var query = string.Empty;
                string numeroMexicoIn = "";
                string numeroMexicoOut = "";
                string numeroCanadaIn = "";

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM gp.V_ChatWhatsAppHistorialPostulante WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                }
                else
                {
                    if (area == "GP")
                    {
                        numeroMexicoIn = numero.StartsWith("52") ? "521" + numero.Substring(2) : numero;
                        numeroMexicoOut = numero.StartsWith("521") ? "52" + numero.Substring(3) : numero;
                        numeroCanadaIn = numero.StartsWith("1") ? "11" + numero.Substring(1) : numero;

                        query = @$"SELECT DISTINCT
                                       Numero,
                                       Tipo,
                                       Mensaje,
                                       IdPersonal,
                                       IdPostulante,
                                       IdPais,
                                       Registro,
                                       FechaCreacion,
                                       NombrePersonal,
                                       MAX(EstadoMensaje) AS EstadoMensaje,
                                       MAX(final.FechaEstado) AS FechaEstado
                                FROM
                                (
                                    SELECT resultado.WaId,
                                           Numero,
                                           Tipo,
                                           Mensaje,
                                           IdPersonal,
                                           ISNULL(IdPostulante, 0) IdPostulante,
                                           resultado.IdPais,
                                           Registro,
                                           resultado.FechaCreacion,
                                           NombrePersonal,
                                           AreaAbrev,
                                           estado.WaStatus,
                                           CASE
                                               WHEN estado.WaStatus = 'sent' THEN
                                                   1
                                               WHEN estado.WaStatus = 'delivered' THEN
                                                   2
                                               WHEN estado.WaStatus = 'read' THEN
                                                   3
                                           END AS EstadoMensaje,
                                           estado.FechaCreacion AS FechaEstado
                                    FROM gp.V_ChatWhatsAppHistorialPostulante AS resultado
                                        LEFT JOIN gp.T_WhatsAppEstadoMensajeEnviadoPostulante AS estado
                                            ON estado.WaId = resultado.WaId
                                    WHERE MensajeOfensivo = 0
                                          AND
                                          (
                                              Numero = '{numeroMexicoIn}'
                                              OR Numero = '{numeroMexicoOut}'
                                              OR Numero = '{numeroCanadaIn}'
                                              OR Numero = '{numero}'
                                          )
                                ) AS final
                                GROUP BY Numero,
                                         Tipo,
                                         Mensaje,
                                         IdPersonal,
                                         IdPostulante,
                                         IdPais,
                                         Registro,
                                         FechaCreacion,
                                         NombrePersonal
                                ORDER BY FechaCreacion ASC;";
                    }                    
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM gp.V_ChatWhatsAppHistorialPostulante WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                }
                var result = _dapperRepository.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialPostulanteMensajesDTO>>(result);

                List<StringDTO> ListaPalabrasOfensivas = new List<StringDTO>();
                string query1 = string.Empty;
                query1 = "SELECT PalabraFiltrada AS Valor FROM mkt.T_DiccionarioPalabraOfensiva";
                var respuesta = _dapperRepository.QueryDapper(query1, null);
                ListaPalabrasOfensivas = JsonConvert.DeserializeObject<List<StringDTO>>(respuesta);

                List<WhatsAppHistorialPostulanteMensajesDTO> ListaMensajesWhatsAppRespondidosActualizada = new List<WhatsAppHistorialPostulanteMensajesDTO>();
                string palabraOfensivaComparar = "";
                string armarPalabraSensurada = "";

                foreach (WhatsAppHistorialPostulanteMensajesDTO alumnoDatos in listaMensajes)
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


    }
}
