using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CampaniaGeneralRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneral
    /// </summary>
    public class CampaniaGeneralRepository : GenericRepository<TCampaniaGeneral>, ICampaniaGeneralRepository
    {
        private Mapper _mapper;

        public CampaniaGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneral, CampaniaGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<TCampaniaGeneral> Add(IEnumerable<CampaniaGeneral> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneral> listado = new List<TCampaniaGeneral>();
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

        public IEnumerable<TCampaniaGeneral> Update(IEnumerable<CampaniaGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneral> listado = new List<TCampaniaGeneral>();
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


        #region Metodos Base
        private TCampaniaGeneral MapeoEntidad(CampaniaGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneral modelo = _mapper.Map<TCampaniaGeneral>(entidad);

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

        public TCampaniaGeneral Add(CampaniaGeneral entidad)
        {
            try
            {
                var CampaniaGeneral = MapeoEntidad(entidad);
                base.Insert(CampaniaGeneral);
                return CampaniaGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneral Update(CampaniaGeneral entidad)
        {
            try
            {
                var CampaniaGeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampaniaGeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(CampaniaGeneral);
                return CampaniaGeneral;
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
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneral para la tabla filtro Mailing
        /// </summary>
        /// <returns> List<CampaniaGeneralDTO> </returns>
        public IEnumerable<CampaniaGeneralEnvio> ObtenerCampaniaGeneral()
        {
            try
            {
                List<CampaniaGeneralEnvio> rpta = new List<CampaniaGeneralEnvio>();
                var query = @"select Id, Nombre, IdTipoAsociacion, IdCategoriaObjetoFiltro, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion from mkt.T_CampaniaGeneral where estado = 1 order by FechaCreacion desc ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampaniaGeneralEnvio>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneral para la tabla filtro Mailing
        /// </summary>
        /// <returns> List<CampaniaGeneralDTO> </returns>
        public IEnumerable<CampaniaGeneralEnvio> ObtenerCampaniaGeneralWhatsApp()
        {
            try
            {
                List<CampaniaGeneralEnvio> rpta = new List<CampaniaGeneralEnvio>();
                var query = @"select Id, Nombre, IdTipoAsociacion, IdCategoriaObjetoFiltro, UsuarioCreacion, FechaCreacion, FechaModificacion,IncluyeWhatsapp from mkt.T_CampaniaGeneral where estado = 1 AND IncluyeWhatsapp=1 order by FechaCreacion desc ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampaniaGeneralEnvio>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneral para la tabla filtro Mailing para whatsapp
        /// </summary>
        /// <returns> List<CampaniaGeneralDTO> </returns>
        public IEnumerable<ConfiguracionDeEnvioParaWhatsAppMasPlantilla> ObtenerConfiguracionDeEnvioParaWhatsAppMasPlantilla()
        {
            try
            {
                List<ConfiguracionDeEnvioParaWhatsAppMasPlantilla> rpta = new List<ConfiguracionDeEnvioParaWhatsAppMasPlantilla>();
                var query = @"SELECT 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.Id, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.FechaDeEnvio, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.FechaFinDeEnvio, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.Nombre, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.HoraDeEnvio,
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.IdCampaniaGeneralDetalle, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.UsuarioCreacion, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.UsuarioModificacion, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.TiempoEntreEnvios, 
                                whp.T_ConfiguracionDeEnvioParaWhatsApp.IdPlantilla, 
                                mkt.V_TPlantilla_Nombre_WhatsApp.Nombre AS NombrePlantilla
                            FROM whp.T_ConfiguracionDeEnvioParaWhatsApp 
                                INNER JOIN mkt.V_TPlantilla_Nombre_WhatsApp ON whp.T_ConfiguracionDeEnvioParaWhatsApp.IdPlantilla = mkt.V_TPlantilla_Nombre_WhatsApp.Id
                            WHERE (whp.T_ConfiguracionDeEnvioParaWhatsApp.Estado = 1)
                            ORDER BY whp.T_ConfiguracionDeEnvioParaWhatsApp.Id DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionDeEnvioParaWhatsAppMasPlantilla>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CampaniaGeneral AddSqlInsert(CampaniaGeneral entidad)
        {
            var CampaniaGeneral = MapeoEntidad(entidad);
            string sql = "INSERT INTO [mkt].[T_CampaniaGeneral] " +
                "([Nombre] ," +
                "[IdCategoriaOrigen] ," +
                "[FechaEnvio] ," +
                "[IdCategoriaObjetoFiltro] ," +
                "[IdHoraEnvio_Mailing] ," +
                "[IdTipoAsociacion] ," +
                "[IdProbabilidadRegistro_PW] ," +
                "[NroMaximoSegmentos] ," +
                "[CantidadPeriodoSinCorreo] ," +
                "[IdTiempoFrecuencia] ," +
                "[IdFiltroSegmento] ," +
                "[IdPlantilla_Mailing] ," +
                "[IdRemitenteMailing] ," +
                "[IncluyeWhatsapp] ," +
                "[FechaInicioEnvioWhatsapp] ," +
                "[FechaFinEnvioWhatsapp] ," +
                "[NumeroMinutosPrimerEnvio] ," +
                "[IdHoraEnvio_Whatsapp] ," +
                "[DiasSinWhatsapp] ," +
                "[IdPlantilla_Whatsapp] ," +
                "[Estado] ," +
                "[UsuarioCreacion] ," +
                "[UsuarioModificacion] ," +
                "[FechaCreacion] ," +
                "[FechaModificacion] ," +
                "[IncluirRebotes] ," +
                "[IdEstadoEnvio_Mailing] ," +
                "[IdEstadoEnvio_Whatsapp]) " +
                "VALUES " +
                "(@Nombre ," +
                "@IdCategoriaOrigen ," +
                "@FechaEnvio ," +
                "@IdCategoriaObjetoFiltro ," +
                "@IdHoraEnvioMailing ," +
                "@IdTipoAsociacion ," +
                "@IdProbabilidadRegistroPw ," +
                "@NroMaximoSegmentos ," +
                "@CantidadPeriodoSinCorreo ," +
                "@IdTiempoFrecuencia ," +
                "@IdFiltroSegmento ," +
                "@IdPlantillaMailing ," +
                "@IdRemitenteMailing ," +
                "@IncluyeWhatsapp ," +
                "@FechaInicioEnvioWhatsapp ," +
                "@FechaFinEnvioWhatsapp ," +
                "@NumeroMinutosPrimerEnvio ," +
                "@IdHoraEnvioWhatsapp ," +
                "@DiasSinWhatsapp ," +
                "@IdPlantillaWhatsapp ," +
                "@Estado ," +
                "@UsuarioCreacion ," +
                "@UsuarioModificacion ," +
                "GETDATE() ," +
                "GETDATE() ," +
                "@IncluirRebotes ," +
                "@IdEstadoEnvioMailing ," +
                "@IdEstadoEnvioWhatsapp) SELECT SCOPE_IDENTITY() as ID";
            var respuesta = _dapperRepository.FirstOrDefault(sql, new
            {
                CampaniaGeneral.Nombre,
                CampaniaGeneral.IdCategoriaOrigen,
                CampaniaGeneral.FechaEnvio,
                CampaniaGeneral.IdCategoriaObjetoFiltro,
                CampaniaGeneral.IdHoraEnvioMailing,
                CampaniaGeneral.IdTipoAsociacion,
                CampaniaGeneral.IdProbabilidadRegistroPw,
                CampaniaGeneral.NroMaximoSegmentos,
                CampaniaGeneral.CantidadPeriodoSinCorreo,
                CampaniaGeneral.IdTiempoFrecuencia,
                CampaniaGeneral.IdFiltroSegmento,
                CampaniaGeneral.IdPlantillaMailing,
                CampaniaGeneral.IdRemitenteMailing,
                CampaniaGeneral.IncluyeWhatsapp,
                CampaniaGeneral.FechaInicioEnvioWhatsapp,
                CampaniaGeneral.FechaFinEnvioWhatsapp,
                CampaniaGeneral.NumeroMinutosPrimerEnvio,
                CampaniaGeneral.IdHoraEnvioWhatsapp,
                CampaniaGeneral.DiasSinWhatsapp,
                CampaniaGeneral.IdPlantillaWhatsapp,
                CampaniaGeneral.Estado,
                CampaniaGeneral.UsuarioCreacion,
                CampaniaGeneral.UsuarioModificacion,
                CampaniaGeneral.IncluirRebotes,
                CampaniaGeneral.IdEstadoEnvioMailing,
                CampaniaGeneral.IdEstadoEnvioWhatsapp,
            });

            if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
            {
                var map = JsonConvert.DeserializeObject<MapeoParaConsultaSqlInsertId>(respuesta);
                entidad.Id = Convert.ToInt32(map.Id.Split(".")[0]);
                return entidad;
            }
            return null;
        }
        /// <summary>
        /// Actualiza el estado de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        public void ActualizarEstadoEnvioWhatsApp(int idCampaniaGeneral)
        {
            try
            {
                var spActualizarEstadoWhatsApp = "[mkt].[SP_ActualizarEstadoEnvioWhatsApp]";
                var resultadoSp = _dapperRepository.QuerySPFirstOrDefault(spActualizarEstadoWhatsApp, new { IdCampaniaGeneral = idCampaniaGeneral });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene Las Campanias generales a ejecutar
        /// </summary>
        /// <returns>Lista de objetos de clase ActividadCampaniaGeneralParaEjecutarDTO</returns>
        public List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO> ObtenerActividadCampaniaGeneralParaEjecutar()
        {
            try
            {
                #region Captura del tiempo
                var horaActual = DateTime.Now;

                string fechaEnvio = horaActual.ToString("dd/MM/yyyy");
                string minutoActual = string.Empty;

                minutoActual = horaActual.Minute.ToString().Length == 1 ? minutoActual = "0" + horaActual.Minute : minutoActual = horaActual.Minute.ToString();
                string horaEnvio = horaActual.Hour + ":" + minutoActual + ":00";
                #endregion

                var listaCampaniaGeneralWhatsapp = new List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO>();
                string query = "com.SP_CampaniaGeneral_ParaEjecutarWhatsapp";
                var resultadoListaWhatsApp = _dapperRepository.QuerySPDapper(query, new { fechaEnvio, horaEnvio });
                if (!string.IsNullOrEmpty(resultadoListaWhatsApp) && !resultadoListaWhatsApp.Contains("[]"))
                    listaCampaniaGeneralWhatsapp = JsonConvert.DeserializeObject<List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO>>(resultadoListaWhatsApp);

                return listaCampaniaGeneralWhatsapp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Object EjecutarReplicado(int IdCampaniaGeneral, string usuario)
        {
            try
            {
                var sql = "whp.SP_DuplicarDataDeFiltroWppATablasDEEnvioAntiguas";
                var resultadoSp = _dapperRepository.QuerySPDapper(sql, new { IdCampaniaGeneral, usuario });
                return resultadoSp;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public List<IdCampaniaGeneral> ObtenerCampaniaGeneralDetalle(int IdCampaniaGeneral)
        {
            try
            {
                List<IdCampaniaGeneral> rpta = new List<IdCampaniaGeneral>();
                var query = @"SELECT top 1  CGD.Id FROM mkt.T_CampaniaGeneral AS CG
                    INNER JOIN mkt.T_CampaniaGeneralDetalle AS CGD
                    ON CGD.IdCampaniaGeneral = CG.Id 
                    INNER JOIN mkt.T_WhatsAppConfiguracionEnvio AS WCED
                    ON WCED.IdCampaniaGeneralDetalle = CGD.Id
                    WHERE CG.Estado = 1
                    AND CGD.Estado = 1
                    AND WCED.Estado = 1
                    AND CGD.IdCampaniaGeneral = @IdCampaniaGeneral
                    order by WCED.id desc";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCampaniaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<IdCampaniaGeneral>>(resultado);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO> ObtenerCantidadCampaniaGeneralDetalle(int IdCampaniaGeneralDetalle)
        {
            try
            {
                List<InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO> rpta = new List<InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO>();
                var query = @"SELECT DISTINCT PEDC.Dia1 AS Total, PEDC.IdPersonal AS IdResponsable FROM whp.T_ConfiguracionDeEnvioParaWhatsApp AS CEPW
                INNER JOIN whp.T_PersonalEncargadoDeEnvioDeConsulta AS PEDC
                ON PEDC.IdConfiguracionDeEnvioParaWhatsApp = CEPW.Id
                WHERE CEPW.Estado = 1
                AND PEDC.Estado = 1
                AND CEPW.IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene Las Campanias generales a ejecutar
        /// </summary>
        /// <returns>Lista de objetos de clase ActividadCampaniaGeneralParaEjecutarDTO</returns>
        public List<ObtenerPrioridadesEnvioWhatsAppDTO> ObtenerPrioridadesEnvioWhatsAppOriginal()
        {
            try
            {
                #region Captura del tiempo
                var horaActual = DateTime.Now;
                string FechaInicioEnvioWhatsapp = horaActual.ToString("dd/MM/yyyy");
                string minutoActual = string.Empty;

                minutoActual = horaActual.Minute.ToString().Length == 1 ? minutoActual = "0" + horaActual.Minute : minutoActual = horaActual.Minute.ToString();
                string horaEnvio = horaActual.Hour + ":" + minutoActual + ":00";
                #endregion

                var listaCampaniaGeneralWhatsapp = new List<ObtenerPrioridadesEnvioWhatsAppDTO>();
                string query = "mkt.SP_ObtenerPrioridadesEnvioWhatsApp";
                var resultadoListaWhatsApp = _dapperRepository.QuerySPDapper(query, new { horaEnvio, FechaInicioEnvioWhatsapp });
                if (!string.IsNullOrEmpty(resultadoListaWhatsApp) && !resultadoListaWhatsApp.Contains("[]"))
                    listaCampaniaGeneralWhatsapp = JsonConvert.DeserializeObject<List<ObtenerPrioridadesEnvioWhatsAppDTO>>(resultadoListaWhatsApp);

                return listaCampaniaGeneralWhatsapp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObtenerPrioridadesEnvioWhatsAppDTO> ObtenerPrioridadesEnvioWhatsApp()
        {
            try
            {
                #region Captura del tiempo con zona horaria Perú (UTC-5)
                TimeZoneInfo zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime horaActual = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaPeru);

                string FechaInicioEnvioWhatsapp = horaActual.ToString("dd/MM/yyyy");
                string minutoActual = horaActual.Minute.ToString("D2");
                string horaEnvio = horaActual.Hour + ":" + minutoActual + ":00";
                #endregion

             
                Console.WriteLine($"[SERVER] FechaHoraPerú Actual: {horaActual:dd/MM/yyyy HH:mm:ss}");
                Console.WriteLine($"[DEBUG] Enviando a SP -> Fecha: {FechaInicioEnvioWhatsapp}, Hora: {horaEnvio}");

                var listaCampaniaGeneralWhatsapp = new List<ObtenerPrioridadesEnvioWhatsAppDTO>();
                string query = "mkt.SP_ObtenerPrioridadesEnvioWhatsApp";

                var resultadoListaWhatsApp = _dapperRepository.QuerySPDapper(query, new { horaEnvio, FechaInicioEnvioWhatsapp });

                if (!string.IsNullOrEmpty(resultadoListaWhatsApp) && !resultadoListaWhatsApp.Contains("[]"))
                {
                    listaCampaniaGeneralWhatsapp = JsonConvert.DeserializeObject<List<ObtenerPrioridadesEnvioWhatsAppDTO>>(resultadoListaWhatsApp);
                }

         
                listaCampaniaGeneralWhatsapp.Insert(0, new ObtenerPrioridadesEnvioWhatsAppDTO
                {
                    NombreCampania = $"[SERVER] {horaActual:dd/MM/yyyy HH:mm:ss} | SP => Fecha: {FechaInicioEnvioWhatsapp}, Hora: {horaEnvio}"
                });

                return listaCampaniaGeneralWhatsapp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ResultadoEjecucionCampaniaDTO ObtenerPrioridadesEnvioWhatsApp2()
        {
            try
            {
                TimeZoneInfo zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime horaServidor = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaPeru);

                string fechaServidor = horaServidor.ToString("dd/MM/yyyy");
                string horaServidorStr = horaServidor.ToString("HH:mm:ss");

                string query = "mkt.SP_ObtenerPrioridadesEnvioWhatsApp";

                var lista = new List<ObtenerPrioridadesEnvioWhatsAppDTO>();
                var resultadoRaw = _dapperRepository.QuerySPDapper(query, new
                {
                    horaEnvio = horaServidorStr,
                    FechaInicioEnvioWhatsapp = fechaServidor
                });

                if (!string.IsNullOrEmpty(resultadoRaw) && !resultadoRaw.Contains("[]"))
                    lista = JsonConvert.DeserializeObject<List<ObtenerPrioridadesEnvioWhatsAppDTO>>(resultadoRaw);

                var prioridadReal = lista.FirstOrDefault(x => x.HoraEnvio != null && x.FechaInicioEnvioWhatsapp != null);

                return new ResultadoEjecucionCampaniaDTO
                {
                    ListaPrioridades = lista,
                    HoraServidor = horaServidorStr,
                    FechaServidor = fechaServidor,
                    HoraProgramada = prioridadReal?.HoraEnvio,
                    FechaProgramada = prioridadReal?.FechaInicioEnvioWhatsapp
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}