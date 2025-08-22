using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class WhatsAppConfiguracionPreEnvioRepository : GenericRepository<TWhatsAppConfiguracionPreEnvio>, IWhatsAppConfiguracionPreEnvioRepository
    {
        private Mapper _mapper;

        public WhatsAppConfiguracionPreEnvioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracionPreEnvio, WhatsAppConfiguracionPreEnvioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TWhatsAppConfiguracionPreEnvio MapeoEntidad(WhatsAppConfiguracionPreEnvioDTO entidad)
        {
            try
            {
                TWhatsAppConfiguracionPreEnvio modelo = _mapper.Map<TWhatsAppConfiguracionPreEnvio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionPreEnvio Add(WhatsAppConfiguracionPreEnvioDTO entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                base.Insert(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(IEnumerable<TWhatsAppConfiguracionPreEnvio> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TWhatsAppConfiguracionPreEnvio Update(WhatsAppConfiguracionPreEnvioDTO entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralConfiguracionPlantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TWhatsAppConfiguracionPreEnvio Update(TWhatsAppConfiguracionPreEnvio entidad)
        {
            try
            {
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                entidad.RowVersion = entidadExistente.RowVersion;

                base.Update(entidad);
                return entidad;
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


        public IEnumerable<TWhatsAppConfiguracionPreEnvio> Add(IEnumerable<WhatsAppConfiguracionPreEnvioDTO> listadoEntidad)
        {
            try
            {
                List<TWhatsAppConfiguracionPreEnvio> listado = new List<TWhatsAppConfiguracionPreEnvio>();
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

        public IEnumerable<TWhatsAppConfiguracionPreEnvio> Update(IEnumerable<WhatsAppConfiguracionPreEnvioDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppConfiguracionPreEnvio> listado = new List<TWhatsAppConfiguracionPreEnvio>();
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

        public IEnumerable<TWhatsAppConfiguracionPreEnvio> GetBy(Expression<Func<TWhatsAppConfiguracionPreEnvio, bool>> filter)
        {
            try
            {
                return base.GetBy(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion que permite consumir de la tabla T_WhatsAppConfiguracionPreEnvio los datos pre-procesados del conjunto de lista
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Retorna el conjunto de datso pre-procesados</returns>
        public List<WhatsAppConfiguracionPreEnvioDTO> ListasWhatsAppEnvioAutomaticoMasivoPreProcesada(int IdConjuntoListaDetalle)
        {
            try
            {
                List<WhatsAppConfiguracionPreEnvioDTO> ConfiguracionPre = new List<WhatsAppConfiguracionPreEnvioDTO>();
                string Query = string.Empty;
                Query = "SELECT Id,IdWhatsappMensajePublicidad,IdConjuntoListaResultado,IdAlumno,Celular,IdPais AS IdCodigoPais,NroEjecucion,Validado,Plantilla,IdPersonal,IdPlantilla,IdWhatsAppEstadoValidacion,objetoplantilla,Procesado FROM mkt.T_WhatsAppConfiguracionPreEnvio WHERE Estado=1 and Validado=1 and Procesado=0 and MensajeProceso='No Porcesado' and IdConjuntoListaDetalle=@IdConjuntoListaDetalle";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionPreEnvioDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Autor: Carlos Crispin Riquelme
        /// Descripción: Funcion que permite consumir de la tabla T_WhatsAppConfiguracionPreEnvio los datos pre-procesados del conjunto de lista de las campanias generales
        /// </summary>
        /// <param name="IdCampaniaGeneralDetalle">Id de la campania general detalle</param>
        /// <param name="cantidad">Cantidad de contactos a enviar</param>
        /// <returns>Retorna el conjunto de datso pre-procesados</returns>
        public List<WhatsAppConfiguracionPreEnvioDTO> ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(int cantidad, int idCampaniaGeneralDetalle, int idPersonal)
        {
            try
            {
                List<WhatsAppConfiguracionPreEnvioDTO> ConfiguracionPre = new List<WhatsAppConfiguracionPreEnvioDTO>();

                string querySP = "mkt.SP_ObtenerListaWhatsAppCampaniaGeneralPreEnvio";

                var respuestaConsulta = _dapperRepository.QuerySPDapper(querySP, new { CantidadAEnviar = cantidad, IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionPreEnvioDTO>>(respuestaConsulta);

                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Autor: 
        /// Descripción: Funcion que lee los datos de una vista para tener los registros WhatsApp Configurados del PreEnvio
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public List<VistaWhatsAppConfiguracionPreEnvioDTO> ListasVisualizarWhatsAppEnvioAutomaticoMasivoPreProcesada(int IdConjuntoListaDetalle)
        {
            try
            {
                List<VistaWhatsAppConfiguracionPreEnvioDTO> ConfiguracionPre = new List<VistaWhatsAppConfiguracionPreEnvioDTO>();
                string Query = string.Empty;
                Query = "SELECT Id, IdWhatsappMensajePublicidad, IdConjuntoListaResultado, IdAlumno, Alumno, Celular, IdPais, Pais, NroEjecucion, Validado, Plantilla, IdPersonal, Personal, IdPlantilla, IdWhatsAppEstadoValidacion, WhatsAppEstadoValidacion, objetoplantilla FROM mkt.V_RegistroWhatsAppConfiguracionPreEnvio WHERE IdConjuntoListaDetalle=@IdConjuntoListaDetalle";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<VistaWhatsAppConfiguracionPreEnvioDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Autor:
        /// Descripción: Permite obtener los registros consultando de forma directa a la tabla segun el Id del conjunto de lista
        /// </summary>
        /// <param name="IdConjuntoLista"></param>
        /// <returns>Objeto de clase RegistroSeguimientoPreProcesoListaWhatsAppDTO</returns>
        public RegistroSeguimientoPreProcesoListaWhatsAppDTO RegistroSeguimientoPreProcesoListaWhatsApp(int IdConjuntoLista)
        {
            try
            {
                RegistroSeguimientoPreProcesoListaWhatsAppDTO Registro = new RegistroSeguimientoPreProcesoListaWhatsAppDTO();
                string QueryPersona = string.Empty;
                QueryPersona = "SELECT Id, IdEstadoSeguimientoPreProcesoListaWhatsApp, IdConjuntoLista FROM mkt.T_SeguimientoPreProcesoListaWhatsApp WHERE Estado=1 AND IdConjuntoLista=@IdConjuntoLista";
                var queryModalidad = _dapperRepository.FirstOrDefault(QueryPersona, new { IdConjuntoLista });
                if (!string.IsNullOrEmpty(queryModalidad) && !queryModalidad.Contains("[]"))
                {
                    Registro = JsonConvert.DeserializeObject<RegistroSeguimientoPreProcesoListaWhatsAppDTO>(queryModalidad);
                }

                return Registro;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inserta una nueva configuracion de WhatsAppConfiguracionPreEnvioBO
        /// </summary>
        /// <param name="listaNuevoWhatsAppConfiguracionPreEnvio">Objeto de tipo WhatsAppConfiguracionPreEnvioBO</param>
        /// <returns>Entero con el Id del scope</returns>
        public bool InsertarConfiguracionPreEnvioRepositorioMailingGeneral(List<WhatsAppConfiguracionPreEnvioBO> listaNuevoWhatsAppConfiguracionPreEnvio)
        {
            try
            {
                int contador = 0;
                string spQuery = "[mkt].[SP_InsertarWhatsAppConfiguracionPreEnvioMailingGeneral]";

                foreach (var nuevoWhatsAppConfiguracionPreEnvio in listaNuevoWhatsAppConfiguracionPreEnvio)
                {
                    try
                    {
                        if (contador >= 2500)
                        {
                            contador = 0;
                            Thread.Sleep(1000);
                        }

                        _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                        {
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaResultado,
                            nuevoWhatsAppConfiguracionPreEnvio.IdAlumno,
                            nuevoWhatsAppConfiguracionPreEnvio.Celular,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPais,
                            nuevoWhatsAppConfiguracionPreEnvio.NroEjecucion,
                            nuevoWhatsAppConfiguracionPreEnvio.Validado,
                            nuevoWhatsAppConfiguracionPreEnvio.Plantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPersonal,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPGeneral,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPlantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion,
                            ObjetoPlantilla = nuevoWhatsAppConfiguracionPreEnvio.objetoplantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPrioridadMailChimpListaCorreo,
                            nuevoWhatsAppConfiguracionPreEnvio.Procesado,
                            nuevoWhatsAppConfiguracionPreEnvio.MensajeProceso,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioCreacion,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioModificacion
                        });

                        contador++;
                    }
                    catch (Exception)
                    {
                        _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                        {
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaResultado,
                            nuevoWhatsAppConfiguracionPreEnvio.IdAlumno,
                            nuevoWhatsAppConfiguracionPreEnvio.Celular,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPais,
                            nuevoWhatsAppConfiguracionPreEnvio.NroEjecucion,
                            nuevoWhatsAppConfiguracionPreEnvio.Validado,
                            nuevoWhatsAppConfiguracionPreEnvio.Plantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPersonal,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPGeneral,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPlantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion,
                            ObjetoPlantilla = nuevoWhatsAppConfiguracionPreEnvio.objetoplantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPrioridadMailChimpListaCorreo,
                            nuevoWhatsAppConfiguracionPreEnvio.Procesado,
                            nuevoWhatsAppConfiguracionPreEnvio.MensajeProceso,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioCreacion,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioModificacion
                        });

                        contador++;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public TWhatsAppConfiguracionPreEnvio FirstById(int id)
        {
            try
            {
                return base.FirstById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Descripción: Funcion con los resultados donde ya se registra si los numeros son validos, plantilla, varibles en la plantilla y su estado del numero, permitiendo guardar la lista pre procesada para el envio por el servicio
        /// </summary>
        /// <param name="RegistrosProcesados">Lista de registros procesados</param>
        /// <returns>Retorna true si el proceso cuncluye de forma exitosa, caso contrario false</returns>
        public bool RegistraPreValidacionCampaniaGeneral(List<WhatsAppResultadoCampaniaGeneralDTO> RegistrosProcesados, int IdPGeneral, int IdPlantilla)
        {
            try
            {
                List<TWhatsAppConfiguracionPreEnvio> listaWhatsAppConfiguracionPreEnvio = new List<TWhatsAppConfiguracionPreEnvio>();

                foreach (var item in RegistrosProcesados)
                {
                    try
                    {
                        TWhatsAppConfiguracionPreEnvio whatsAppConfiguracionPreEnvio = new TWhatsAppConfiguracionPreEnvio();

                        whatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad = item.IdWhatsappMensajePublicidad;
                        whatsAppConfiguracionPreEnvio.IdConjuntoListaResultado = 1;
                        whatsAppConfiguracionPreEnvio.IdPrioridadMailChimpListaCorreo = item.IdPrioridadMailChimpListaCorreo;
                        whatsAppConfiguracionPreEnvio.IdAlumno = item.IdAlumno;
                        whatsAppConfiguracionPreEnvio.Celular = item.Celular.TrimStart('0');
                        whatsAppConfiguracionPreEnvio.IdPais = item.IdCodigoPais;
                        whatsAppConfiguracionPreEnvio.NroEjecucion = 1;
                        whatsAppConfiguracionPreEnvio.Validado = item.Validado;
                        whatsAppConfiguracionPreEnvio.Plantilla = item.Plantilla;
                        whatsAppConfiguracionPreEnvio.IdPersonal = item.IdPersonal;
                        whatsAppConfiguracionPreEnvio.IdPgeneral = IdPGeneral;
                        whatsAppConfiguracionPreEnvio.IdPlantilla = IdPlantilla;
                        whatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion = item.Validado ? 1 : 4;

                        if (item.ListaObjetoPlantilla != null && item.ListaObjetoPlantilla.Any())
                            whatsAppConfiguracionPreEnvio.ObjetoPlantilla = JsonConvert.SerializeObject(item.ListaObjetoPlantilla);
                        else
                            whatsAppConfiguracionPreEnvio.ObjetoPlantilla = string.Empty;

                        whatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle = 1;
                        whatsAppConfiguracionPreEnvio.Procesado = false;
                        whatsAppConfiguracionPreEnvio.MensajeProceso = "No Procesado";
                        whatsAppConfiguracionPreEnvio.Estado = true;
                        whatsAppConfiguracionPreEnvio.FechaCreacion = DateTime.Now;
                        whatsAppConfiguracionPreEnvio.FechaModificacion = DateTime.Now;
                        whatsAppConfiguracionPreEnvio.UsuarioCreacion = "PreDatosAutomatica";
                        whatsAppConfiguracionPreEnvio.UsuarioModificacion = "PreDatosAutomatica";

                        listaWhatsAppConfiguracionPreEnvio.Add(whatsAppConfiguracionPreEnvio);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                bool resultado = base.Insert(listaWhatsAppConfiguracionPreEnvio);

                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Descripción: Obtiene la pre lista a ser procesada
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> PreListaWhatsAppEnvioMasivo(int IdCampaniaGeneralDetalleResponsableWhatsApp)
        {
            try
            {
                List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> ConfiguracionPre = new List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO>();
                string Query = string.Empty;
                Query = @"	SELECT 
		                    CGDRA.Id ,
		                    CGDRA.IdCampaniaGeneralDetalleResponsableWhatsApp,
		                    CGDRA.CelularWhatsApp,
		                    CGDRA.IdAlumno,
		                    CGDRA.WhatsAppEmpresaIdPais,
		                    CGDRA.MensajePlantillaHtml,
		                    CGDRA.ObjetoPlantilla,
                            P.id as IdPlantilla,
		                    CGDR.IdPersonal,
		                    P.Descripcion,
							CGDRA.Dias
	                    FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoWhatsApp AS CGDRA  WITH (NOLOCK)
	                    INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableWhatsApp AS CGDR WITH (NOLOCK)
	                    ON CGDRA.IdCampaniaGeneralDetalleResponsableWhatsApp = CGDR.Id 
	                    INNER JOIN mkt.T_Plantilla AS P WITH (NOLOCK)
	                    ON P.Id = CGDR.IdPlantilla
	                    WHERE CGDR.Estado = 1
		                    AND CGDRA.Estado = 1
		                    AND P.Estado = 1
		                    AND CGDRA.IdCampaniaGeneralDetalleResponsableWhatsApp = @IdCampaniaGeneralDetalleResponsableWhatsApp
		                    AND CGDRA.IdAlumno NOT IN (
			                    SELECT 
				                    DISTINCT
				                    CGDRAE2.IdAlumno
			                    FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp AS CGDRAE2 WITH (NOLOCK)
			                    INNER JOIN mkt.T_WhatsAppEstadoMensajeEnviadoApiGraph AS API
			                    ON API.WaId = CGDRAE2.WaId
			                    INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableAlumnoWhatsApp AS CGDRA2  WITH (NOLOCK)
			                    ON CGDRAE2.IdCampaniaGeneralDetalleResponsableAlumnoWhatsApp = CGDRA2.Id
			                    INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableWhatsApp AS CGDR2 WITH (NOLOCK)
			                    ON CGDRA2.IdCampaniaGeneralDetalleResponsableWhatsApp = CGDR2.Id 
			                    WHERE CGDR2.Estado = 1
				                    AND CGDRA2.Estado = 1
				                    AND CGDRAE2.Estado = 1
				                    AND CGDRAE2.WaId IS NOT NULL
				                    AND API.WaStatus = 'sent'
				                    AND CGDRA2.IdCampaniaGeneralDetalleResponsableWhatsApp = @IdCampaniaGeneralDetalleResponsableWhatsApp
	                        ) ";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdCampaniaGeneralDetalleResponsableWhatsApp });
                if (!string.IsNullOrEmpty(QueryRespuesta))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Descripción: Obtiene detalle campania
        /// </summary>
        /// <param name="IdcampaniaGeneralDetalleResponsableWhatsApp">Identificador campania general</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public DetalleCampaniaDTO ObtenerDetalleDeCampaniaWhatsApp(int IdcampaniaGeneralDetalleResponsableWhatsApp)
        {
            DetalleCampaniaDTO ConfiguracionPre = new DetalleCampaniaDTO();
            try
            {

                string querySP = "mkt.ObtenerDetalleDeCampaniaWhatsApp";

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { IdcampaniaGeneralDetalleResponsableWhatsApp });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<DetalleCampaniaDTO>(respuestaConsulta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                return ConfiguracionPre;
            }
        }
        /// Descripción: Obtiene la pre lista a ser procesada
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public List<IdLogDTO> logsActivos(int IdCampaniaGeneralDetalleResponsableWhatsApp)
        {
            try
            {
                List<IdLogDTO> ConfiguracionPre = new List<IdLogDTO>();
                string Query = string.Empty;
                Query = @"SELECT
                            LOGW.Id
                        FROM
                            mkt.T_CampaniaGeneralDetalleResponsableAlumnoLogWhatsApp AS LOGW
	                        INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableWhatsApp AS CGDR
	                        ON CGDR.Id = LOGW.IdCampaniaGeneralDetalleResponsableWhatsApp
	                        INNER JOIN mkt.T_CampaniaGeneralDetalleWhatsApp AS CGDW
	                        ON CGDW.Id = CGDR.IdCampaniaGeneralDetalleWhatsApp
                        WHERE
                            LOGW.IdCampaniaGeneralDetalleResponsableWhatsApp = @IdCampaniaGeneralDetalleResponsableWhatsApp
                            AND LOGW.Estado = 1
	                        AND CGDR.Estado = 1
	                        AND CGDW.Estado = 1
	                        AND CGDW.ActivarMasivo = 1
	                         ";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdCampaniaGeneralDetalleResponsableWhatsApp });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<IdLogDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Descripción: Obtiene la pre lista a ser procesada
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public async Task<List<IdLogDTO>> logsActivosAsync(int IdCampaniaGeneralDetalleResponsableWhatsApp)
        {
            try
            {
                List<IdLogDTO> ConfiguracionPre = new List<IdLogDTO>();
                string Query = string.Empty;
                Query = @"SELECT
                            LOGW.Id
                        FROM
                            mkt.T_CampaniaGeneralDetalleResponsableAlumnoLogWhatsApp AS LOGW
	                        INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableWhatsApp AS CGDR
	                        ON CGDR.Id = LOGW.IdCampaniaGeneralDetalleResponsableWhatsApp
	                        INNER JOIN mkt.T_CampaniaGeneralDetalleWhatsApp AS CGDW
	                        ON CGDW.Id = CGDR.IdCampaniaGeneralDetalleWhatsApp
                        WHERE
                            LOGW.IdCampaniaGeneralDetalleResponsableWhatsApp = @IdCampaniaGeneralDetalleResponsableWhatsApp
                            AND LOGW.Estado = 1
	                        AND CGDR.Estado = 1
	                        AND CGDW.Estado = 1
	                        AND CGDW.ActivarMasivo = 1
	                         ";
                var QueryRespuesta = await _dapperRepository.QueryDapperAsync(Query, new { IdCampaniaGeneralDetalleResponsableWhatsApp });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<IdLogDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Descripción: Obtiene la pre lista a ser procesada
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public bool ValidarEnvioDuplicado(string CelularWhatsApp, int Dias)
        {
            try
            {
                List<EstadoDTO> ConfiguracionPre = new List<EstadoDTO>();
                string Query = string.Empty;
                Query = @"SELECT TOP 1 Estado 
                          FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp
                          WHERE CelularWhatsApp = @CelularWhatsApp
                          AND CONVERT(DATE, FechaCreacion) > CAST(GETDATE() - @Dias AS DATE)
                          ORDER BY Estado DESC;";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { CelularWhatsApp = CelularWhatsApp, Dias = Dias });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<EstadoDTO>>(QueryRespuesta);
                    if (ConfiguracionPre[0].Estado == true)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// Descripción: Obtiene plnatilla de  WhatsApp
        /// </summary>
        /// <param name="idPlantilla">Identificador plantilla</param>
        /// <returns>Detalle de registros WhatsApp Configurados del PreEnvio</returns>
        public List<DetallePlantillasDTO> ObtenerDetallePlantillaWhatsApp(int idPlantilla)
        {
            List<DetallePlantillasDTO> ConfiguracionPre = new List<DetallePlantillasDTO>();
            try
            {

                string querySP = "SELECT Imagen, Boton FROM mkt.T_DetallePlantilla WHERE IdPlantilla = @idPlantilla";

                var respuestaConsulta = _dapperRepository.QueryDapper(querySP, new { idPlantilla });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<DetallePlantillasDTO>>(respuestaConsulta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                return ConfiguracionPre;
            }
        }
        /// <summary>
        /// Descripción: Inserta el resultado del masivo
        /// </summary>
        /// <returns>Objeto de clase RegistroSeguimientoPreProcesoListaWhatsAppDTO</returns>
        public bool InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(string json, string WaId, int IdCampaniaGeneralDetalleResponsableLogWhatsApp)
        {

            RespuestaValDTO ConfiguracionPre = new RespuestaValDTO();
            try
            {

                string querySP = "mkt.SP_InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp";

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { IdCampaniaGeneralDetalleResponsableLogWhatsApp = IdCampaniaGeneralDetalleResponsableLogWhatsApp, json = json, WaId = WaId });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<RespuestaValDTO>(respuestaConsulta);
                }
                return ConfiguracionPre.Valor;
            }
            catch (Exception ex)
            {
                return ConfiguracionPre.Valor;
            }
        }

        public bool ValidarDesuscritos(string CelularWhatsApp)
        {
            try
            {
                string Query = string.Empty;
                Query = @"SELECT NumeroTelefono
                          FROM mkt.T_WhatsAppDesuscrito
                          WHERE NumeroTelefono = CelularWhatsApp
                          AND estado = 1;";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { CelularWhatsApp = CelularWhatsApp });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
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

        public bool InsertarMensajeEnviadoErroneoWhatsappLog(MensajeEnviadoErroneoWhatsappLogDTO datos)
        {
            try
            {

                string queryMessenger = string.Empty;
                queryMessenger = "mkt.SP_TMensajeEnviadoErroneoWhatsAppLog_Insertar";
                var messenger = _dapperRepository.QuerySPDapper(queryMessenger, new {
                    datos.CelularWhatsapp, datos.IdAlumno, 
                    datos.IdCampaniaGeneralDetalleResponsableWhatsapp, 
                    datos.IdPlantilla,
                    datos.MensajePlantillaHtml,
                    datos.ObjetoPlantilla,
                    datos.IdPais,
                    datos.NumeroEnviado,
                    datos.MensajeErroneo,
                    datos.WaId ,
                    datos.UsuarioModificacion,
                    datos.UsuarioCreacion,
                   
                
            });;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
