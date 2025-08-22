using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Globalization;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: InteraccionChatIntegraRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 29/09/2022
    /// <summary>
    /// Gestión general de T_InteraccionChatIntegra
    /// </summary>
    public class InteraccionChatIntegraRepository : GenericRepository<TInteraccionChatIntegra>, IInteraccionChatIntegraRepository
    {
        private Mapper _mapper;

        public InteraccionChatIntegraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInteraccionChatIntegra, InteraccionChatIntegra>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TInteraccionChatIntegra MapeoEntidad(InteraccionChatIntegra entidad)
        {
            try
            {
                //crea la entidad padre
                TInteraccionChatIntegra modelo = _mapper.Map<TInteraccionChatIntegra>(entidad);

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

        public TInteraccionChatIntegra Add(InteraccionChatIntegra entidad)
        {
            try
            {
                var InteraccionChatIntegra = MapeoEntidad(entidad);
                base.Insert(InteraccionChatIntegra);
                return InteraccionChatIntegra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TInteraccionChatIntegra Update(InteraccionChatIntegra entidad)
        {
            try
            {
                var InteraccionChatIntegra = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                InteraccionChatIntegra.RowVersion = entidadExistente.RowVersion;

                base.Update(InteraccionChatIntegra);
                return InteraccionChatIntegra;
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


        public IEnumerable<TInteraccionChatIntegra> Add(IEnumerable<InteraccionChatIntegra> listadoEntidad)
        {
            try
            {
                List<TInteraccionChatIntegra> listado = new List<TInteraccionChatIntegra>();
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

        public IEnumerable<TInteraccionChatIntegra> Update(IEnumerable<InteraccionChatIntegra> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TInteraccionChatIntegra> listado = new List<TInteraccionChatIntegra>();
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


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha:  /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla T_SemaforoFinancieropor el id
        /// </summary>
        ///<param name="id">id del T_SemaforoFinancieroDetalle/param>
        /// <returns> SemaforoFinanciero </returns>
        /// <summary>
        /// Obtiene registros de los vslores asignados en T_TInteraccionChatIntegra
        /// </summary>
        /// <returns> List</returns>

        public IEnumerable<ReporteChatLogDTO> GenerarReporteChatLog(ChatReporteDTO chat)
        {
            try
            {
                DateTime FechaInicio = chat.FechaInicio.AddHours(-5);
                DateTime FechaFin = chat.FechaFin.AddHours(-5);
                var spDapper = "[com].[SP_ReporteChatSeguimiento]";

                var resultado = _dapperRepository.QuerySPDapper(spDapper,
                //var query = _dapper.QuerySPDapper("com.SP_ReporteChatSeguimiento",
                    new
                    {
                        chat.Areas,
                        chat.CentroCosto,
                        chat.Asesor,
                        FechaInicio,
                        FechaFin
                    });
                var res = JsonConvert.DeserializeObject<List<ReporteChatLogDTO>>(resultado);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro  de mes  T_InteraccionChatIntegra
        /// </summary>
        ///<param name="mes">/param>
        /// <returns> ReporteChatIntegra </returns>
        public string ObtenerNombreMes(int mes)
        {
            try
            {
                switch (mes)
                {
                    case 1:
                        return "Enero";
                    case 2:
                        return "Febrero";
                    case 3:
                        return "Marzo";
                    case 4:
                        return "Abril";
                    case 5:
                        return "Mayo";
                    case 6:
                        return "Junio";
                    case 7:
                        return "Julio";
                    case 8:
                        return "Agosto";
                    case 9:
                        return "Setiembre";
                    case 10:
                        return "Octubre";
                    case 11:
                        return "Noviembre";
                    case 12:
                        return "Diciembre";
                    default:
                        return "";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro  d enumero de Semana  T_InteraccionChatIntegra
        /// </summary>
        /// 
        /// <returns> ReporteChatIntegra </returns>

        public int ObtenerNumeroSemana(DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            return weekNum;
        }

        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Genera todos registro de la tabla T_InteraccionChatIntegra 
        /// </summary>
        ///<param name="chat">/param>
        /// <returns> ReporteChatIntegra </returns>
        public IEnumerable<ReporteChatIntegraDTO> GenerarReporteChat(ChatReporteDTO chat)
        {
            try
            {
                DateTime FechaInicio = chat.FechaInicio.AddHours(-5);
                DateTime FechaFin = chat.FechaFin.AddHours(-5);
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteChat", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
                //switch (chat.Desglose)
                //{
                //	case 1: //Dia
                //		query = _dapper.QuerySPDapper("com.SP_ReporteChat2", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
                //		break;
                //	case 2: //Semana
                //		query = _dapper.QuerySPDapper("com.SP_ReporteChatSemana", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
                //		break;
                //	case 3: //Mes
                //		query = _dapper.QuerySPDapper("com.SP_ReporteChatMes", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
                //		break;
                //	default:
                //		query = _dapper.QuerySPDapper("com.SP_ReporteChat2", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
                //		break;
                //}
                var res = JsonConvert.DeserializeObject<List<ReporteChatIntegraDTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_InteraccionChatIntegra asociados a un Id.
        /// </summary>
        /// <param name="id">Id de la Actividad Detalle</param>
        /// <returns> InteraccionChatIntegra </returns>
        public InteraccionChatIntegra ObtenerPorId(int id)
        {
            try
            {
                InteraccionChatIntegra rpta = new InteraccionChatIntegra();
                var query = @"SELECT Id,
                                       IdChatIntegraHistorialAsesor,
                                       IdAlumno,
                                       IdContactoPortalSegmento,
                                       IdTipoInteraccion,
                                       IdPGeneral,
                                       IdSubAreaCapacitacion,
                                       IdAreaCapacitacion,
                                       Ip,
                                       Pais,
                                       Region,
                                       Ciudad,
                                       Duracion,
                                       NroMensajes,
                                       NroPalabrasVisitor,
                                       NroPalabrasAgente,
                                       UsuarioTiempoRespuestaMaximo,
                                       UsuarioTiempoRespuestaPromedio,
                                       FechaInicio,
                                       FechaFin,
                                       Leido,
                                       Plataforma,
                                       Navegador,
                                       UrlFrom,
                                       UrlTo,
                                       IdEstadoChat,
                                       IdConjuntoAnuncio,
                                       IdChatSession,
                                       IdFaseOportunidad_PortalWeb,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       ClienteTiempoEspera,
                                       ContadorUsuarioPromedioRespuesta,
                                       TiempoRespuestaTotal,
                                       NroMensajesSinLeer 
                                FROM com.T_InteraccionChatIntegra 
                                WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<InteraccionChatIntegra>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
