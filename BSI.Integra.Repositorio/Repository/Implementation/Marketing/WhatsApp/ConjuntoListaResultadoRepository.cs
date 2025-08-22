using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WhatsAppResultadoConjuntoListaDTO = BSI.Integra.Aplicacion.DTO.Modelos.WhatsAppResultadoConjuntoListaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConjuntoListaResultadoRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ConjuntoListaResultado
    /// </summary>
    public class ConjuntoListaResultadoRepository : GenericRepository<TConjuntoListaResultado>, IConjuntoListaResultadoRepository
    {
        private Mapper _mapper;

        public ConjuntoListaResultadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConjuntoListaResultado, ConjuntoListaResultado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConjuntoListaResultado MapeoEntidad(ConjuntoListaResultadoDTO entidad)
        {
            try
            {
     
                TConjuntoListaResultado modelo = _mapper.Map<TConjuntoListaResultado>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListaResultado Add(ConjuntoListaResultadoDTO entidad)
        {
            try
            {
                var ConjuntoListaResultado = MapeoEntidad(entidad);
                base.Insert(ConjuntoListaResultado);
                return ConjuntoListaResultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListaResultado Update(ConjuntoListaResultadoDTO entidad)
        {
            try
            {
                var ConjuntoListaResultado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConjuntoListaResultado.RowVersion = entidadExistente.RowVersion;

                base.Update(ConjuntoListaResultado);
                return ConjuntoListaResultado;
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


        public IEnumerable<TConjuntoListaResultado> Add(IEnumerable<ConjuntoListaResultadoDTO> listadoEntidad)
        {
            try
            {
                List<TConjuntoListaResultado> listado = new List<TConjuntoListaResultado>();
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

        public IEnumerable<TConjuntoListaResultado> Update(IEnumerable<ConjuntoListaResultadoDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoListaResultado> listado = new List<TConjuntoListaResultado>();
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

        public bool Eliminar(int idConjuntoListaDetalle)
        {
            try
            {
                this._dapperRepository.QuerySPDapper("mkt.SP_EliminarConjuntoListaResultadoPorConjuntoListaDetalle", new { idConjuntoListaDetalle });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina los registros que pertenecen a un conjunto lista
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public bool EliminarPorConjuntoLista(int idConjuntoLista, string nombreUsuario)
        {
            try
            {
                this._dapperRepository.QuerySPDapper("mkt.SP_EliminarConjuntoListaResultadoPorConjuntoLista", new { idConjuntoLista, nombreUsuario });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene El resultado por conjuntoListadetalle
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<WhatsAppResultadoConjuntoListaDTO> ObtenerConjuntoListaResultado(int IdConjuntoListaDetalle)
        {
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                string _queryResultado = "Select  IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,NroEjecucion From mkt.V_WhatsAppConjuntoListadetalleResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and Activo=1 ";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, new { IdConjuntoListaDetalle });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<WhatsAppResultadoConjuntoListaDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Autor: Gian Miranda
        /// Descripción: Obtiene El resultado por conjuntoListadetalle para pre validar
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Identificador del conjunto lista detalle</param>
        /// <returns>Retorna la lista de datos de cada usuario del conjunto de lista (PreWhatsAppResultadoConjuntoListaDTO)</returns>
        public List<PreWhatsAppResultadoConjuntoListaDTO> ObtenerListaPreparadaProcesamiento(int idConjuntoListaDetalle)
        {
            try
            {
                List<PreWhatsAppResultadoConjuntoListaDTO> resultadoLista = new List<PreWhatsAppResultadoConjuntoListaDTO>();

                string querySp = "mkt.SP_ObtenerConjuntoListaResultadoWhatsApp";
                var queryResultado = _dapperRepository.QuerySPDapper(querySp, new { IdConjuntoListaDetalle = idConjuntoListaDetalle });

                if (queryResultado != "[]" && queryResultado != "null")
                    resultadoLista = JsonConvert.DeserializeObject<List<PreWhatsAppResultadoConjuntoListaDTO>>(queryResultado);

                return resultadoLista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Obtiene El resultado por conjuntoListadetalle para pre validar
        /// </summary>
        /// <param name="idConjuntoLista">Identificador del conjunto de anuncios</param>
        /// <returns>Retorna ls lista de datos de cada usuario del conjunto de lista (PreWhatsAppResultadoConjuntoListaDTO)</returns>
        public List<PreWhatsAppResultadoConjuntoListaDTO> PreObtenerConjuntoListaResultado(int IdConjuntoListaDetalle)
        {
            try
            {
                List<PreWhatsAppResultadoConjuntoListaDTO> Resultado = new List<PreWhatsAppResultadoConjuntoListaDTO>();
                string Query = "Select  IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,NroEjecucion From mkt.V_WhatsAppConjuntoListadetalleResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and Activo=1 ";
                var QueryResultado = _dapperRepository.QueryDapper(Query, new { IdConjuntoListaDetalle });
                if (QueryResultado != "[]" && QueryResultado != "null")
                {
                    Resultado = JsonConvert.DeserializeObject<List<PreWhatsAppResultadoConjuntoListaDTO>>(QueryResultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el resultado de un conjunto lista detalle
        /// </summary>
        /// <param name="IdConjuntoListaDetalle"></param>
        /// <returns></returns>
        public List<WhatsAppResultadoConjuntoListaDTO> ObtenerConjuntoListaResultadoWhatsAppMasivoOperaciones(int idConjuntoListaDetalle)
        {
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                string _queryResultado = $@"
                                    SELECT 
                                            IdConjuntoListaResultado,
                                            IdAlumno,
                                            Celular,
                                            IdCodigoPais,
                                            IdPersonal,
                                            NroEjecucion
                                    FROM mkt.V_WhatsAppConjuntoListadetalleResultadoMasivoOperaciones
                                    WHERE IdConjuntoListaDetalle = @idConjuntoListaDetalle
                                    AND Activo = 1;
                                    ";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, new { idConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "[]")
                {
                    resultado = JsonConvert.DeserializeObject<List<WhatsAppResultadoConjuntoListaDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<WhatsAppResultadoConjuntoListaDTO> ObtenerOportunidadesReasignadasOperaciones()
        {
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                string _queryResultado = "Select  IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,IdPersonal,IdPgeneral,IdPlantilla From ope.V_ObtenerOportunidadesReasignadas Order by IdPersonal desc";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, null);
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<WhatsAppResultadoConjuntoListaDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RegularizarMensajeWhatsAppEnvioDTO> ObtenerEnvioSinMensaje()
        {
            try
            {
                List<RegularizarMensajeWhatsAppEnvioDTO> resultado = new List<RegularizarMensajeWhatsAppEnvioDTO>();
                string _queryResultado = "Select  Id,IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,IdPersonal,IdPgeneral,IdPlantilla From com.V_ObtenerRegistrosSinMensaje Order by IdWhatsAppConfiguracionEnvio desc";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, null);
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<RegularizarMensajeWhatsAppEnvioDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene El resultado por conjuntoListadetalle
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<FacebookAudienciaDatosAlumnoDTO> ObtenerConjuntoListaResultadoFacebook(int IdConjuntoListaDetalle)
        {
            try
            {
                List<FacebookAudienciaDatosAlumnoDTO> resultado = new List<FacebookAudienciaDatosAlumnoDTO>();
                string _queryResultado = "Select IdAlumno,Email1 From mkt.V_FacebookConjuntoListadetalleResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and Activo=1 ";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, new { IdConjuntoListaDetalle });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<FacebookAudienciaDatosAlumnoDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los conjunto lista resultado
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Id del conjunto lista detalle que se va a ejecutar</param>
        /// <returns>Lista con BO del conjunto Lista Resultado</returns>
        public List<TConjuntoListaResultado> ObtenerPorConjuntoListaDetalle(int idConjuntoListaDetalle)
        {
            try
            {
                return this.GetBy(x => x.IdConjuntoListaDetalle == idConjuntoListaDetalle && x.Activo == true).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todos los conjunto lista resultado
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Id del conjunto lista detalle que se va a ejecutar</param>
        /// <returns>Lista con BO del conjunto Lista Resultado</returns>
        public List<ConjuntoListaResultado> ObtenerPorConjuntoListaDetalleRedireccion(int idConjuntoListaDetalle)
        {
            try
            {
                List<ConjuntoListaResultado> resultadoFinal = new List<ConjuntoListaResultado>();
                var solicitudesCambiosDB = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerConjuntoListaResultadoPorIdConjuntoListaDetalle", new { IdConjuntoListaDetalle = idConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<ConjuntoListaResultado>>(solicitudesCambiosDB);
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todos los conjunto lista resultado Dapper
        /// </summary>
        /// <param name="idConjuntoListaDetalle"></param>
        /// <returns></returns>
        public List<ConjuntoListaResultado> ObtenerPorConjuntoListaDetalleDapper(int idConjuntoListaDetalle)
        {
            try
            {
                List<ConjuntoListaResultado> resultado = new List<ConjuntoListaResultado>();
                string _queryResultado = $@"
                                           SELECT Id, IdAlumno,
		                                            IdConjuntoListaDetalle,
		                                            EsVentaCruzada,
		                                            NroEjecucion,
		                                            Activo,
		                                            IdMigracion,
		                                            IdOportunidad
                                            FROM mkt.T_ConjuntoListaResultado
                                            WHERE IdConjuntoListaDetalle = @idConjuntoListaDetalle
                                                AND Activo = 1
                                        ";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, new { idConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "[]")
                {
                    resultado = JsonConvert.DeserializeObject<List<ConjuntoListaResultado>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
		/// Obtiene Hora Minima y las fases de oportunidad en una cadena
		/// </summary>
		/// <param name="filtros"></param>
		/// <returns></returns>
		public FiltroFasesOportunidadAlumnoDTO ObtenerHoraMinimaFasesCadena(FiltroHoraMinimaFasesCadenaDTO filtros)
        {
            try
            {
                FiltroFasesOportunidadAlumnoDTO FiltroFasesOportunidadAlumno = new FiltroFasesOportunidadAlumnoDTO();

                var FiltroFasesOportunidadAlumnoDB = _dapperRepository.QuerySPFirstOrDefault("[mkt].[SP_HoraYFasesFiltro]", filtros);

                if (!FiltroFasesOportunidadAlumnoDB.Contains("[]") && !string.IsNullOrEmpty(FiltroFasesOportunidadAlumnoDB))
                {
                    FiltroFasesOportunidadAlumno = JsonConvert.DeserializeObject<FiltroFasesOportunidadAlumnoDTO>(FiltroFasesOportunidadAlumnoDB);
                }
                return FiltroFasesOportunidadAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene los alumnos y oportunidad respectivas para el envio automatico
		/// </summary>
		/// <param name="filtros"></param>
		/// <returns></returns>
		public List<AlumnoOportunidadFiltroDTO> ObtenerAlumnoOportunidadEnvioAutomatico(AlumnoOportunidadEnvioAutomaticoDTO filtros)
        {
            try
            {
                List<AlumnoOportunidadFiltroDTO> ConjuntoListaOportunidadAlumno = new List<AlumnoOportunidadFiltroDTO>();

                var ConjuntoListaOportunidadAlumnoDB = _dapperRepository.QuerySPDapper("[mkt].[SP_ObtenerAlumnoOportunidadEnvioAutomatico]", filtros);

                if (!ConjuntoListaOportunidadAlumnoDB.Contains("[]") && !string.IsNullOrEmpty(ConjuntoListaOportunidadAlumnoDB))
                {
                    ConjuntoListaOportunidadAlumno = JsonConvert.DeserializeObject<List<AlumnoOportunidadFiltroDTO>>(ConjuntoListaOportunidadAlumnoDB);
                }

                return ConjuntoListaOportunidadAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// SE obtiene los Psid(Usuarios de Facebook) de un ConjuntoListaResultado y página
        /// </summary>
        /// <param name="IdConjuntoListaDetalle"></param>
        /// <param name="idFacebookPagina"></param>
        /// <returns></returns>
        public string[][] ObtenerMessengerUsuarioPorConjuntoListaResultado(int IdConjuntoListaDetalle, int idFacebookPagina)
        {
            try
            {
                List<MessengerUsuarioPsidDTO> resultado = new List<MessengerUsuarioPsidDTO>();
                string _queryResultado = "Select PSID From mkt.V_ObtenerMessengerUsuario_ConjuntoListaResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and IdFacebookPagina=@idFacebookPagina GROUP BY PSID";
                var queryResultado = _dapperRepository.QueryDapper(_queryResultado, new { IdConjuntoListaDetalle, idFacebookPagina });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<MessengerUsuarioPsidDTO>>(queryResultado);

                    string[][] array = new string[resultado.Count][];
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        array[i] = new string[1] { resultado[i].PSID };
                    }
                    return array;
                }
                else
                {
                    string[][] array = { };
                    return array;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Verifica existencia de mkt.T_ConjuntoListaResultado
        /// </summary>
        /// <param name="idConjuntoListaResultado">Id de ConjuntoListaResultado (PK de la tabla mkt.T_ConjuntoListaResultado)</param>
        /// <returns>Booleano para determinar si existe o no un ConjuntoListaResultado</returns>
        public bool ExisteConjuntoListaResultado(int idConjuntoListaResultado)
        {
            try
            {
                string spQuery = "[mkt].[SP_ExisteConjuntoListaResultado]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConjuntoListaResultado
                });

                return !string.IsNullOrEmpty(query) && !query.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Busca un registro en mkt.T_ConjuntoListaResultado
        /// </summary>
        /// <param name="idConjuntoListaResultado">Id de ConjuntoListaResultado (PK de la tabla mkt.T_ConjuntoListaResultado)</param>
        /// <returns>Busca registro para determinar si existe o no un ConjuntoListaResultado</returns>
        public ConjuntoListaResultado BuscaConjuntoListaResultado(int idConjuntoListaResultado)
        {
            try
            {
                var conjuntoListaResultado = new ConjuntoListaResultado();

                string spQuery = "[mkt].[SP_BuscaConjuntoListaResultado]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConjuntoListaResultado
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaResultado = JsonConvert.DeserializeObject<ConjuntoListaResultado>(query);
                }

                return conjuntoListaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
