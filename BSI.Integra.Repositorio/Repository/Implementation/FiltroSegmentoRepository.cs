using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FiltroSegmentoRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FiltroSegmentoRepository : GenericRepository<TFiltroSegmento>, IFiltroSegmentoRepository
    {
        private Mapper _mapper;

        public FiltroSegmentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFiltroSegmento, FiltroSegmento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TFiltroSegmento MapeoEntidad(FiltroSegmento entidad)
        {
            try
            {
                //crea la entidad padre
                TFiltroSegmento modelo = _mapper.Map<TFiltroSegmento>(entidad);

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
      

        public TFiltroSegmento Add(FiltroSegmento entidad)
        {
            try
            {
                var FiltroSegmento = MapeoEntidad(entidad);
                base.Insert(FiltroSegmento);
                return FiltroSegmento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFiltroSegmento Update(FiltroSegmento entidad)
        {
            try
            {
                var FiltroSegmento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FiltroSegmento.RowVersion = entidadExistente.RowVersion;

                base.Update(FiltroSegmento);
                return FiltroSegmento;
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


        public IEnumerable<TFiltroSegmento> Add(IEnumerable<FiltroSegmento> listadoEntidad)
        {
            try
            {
                List<TFiltroSegmento> listado = new List<TFiltroSegmento>();
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

        public IEnumerable<TFiltroSegmento> Update(IEnumerable<FiltroSegmento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFiltroSegmento> listado = new List<TFiltroSegmento>();
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



        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<FiltroDTO> items = new List<FiltroDTO>();
                var _query = "select Id, Nombre from mkt.T_FiltroSegmento where Estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<FiltroDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM mkt.T_FiltroSegmento WHERE estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboDTO> ObtenerSubArea(string idAreas)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT 
                                id, 
                                nombre 
                              FROM pla.V_RegistrosFiltroSubAreaCapacitacion
                                WHERE estado = 1 AND IdAreaCapacitacion IN (select  item from conf.F_Splitstring(@idAreas,','))";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idAreas });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ComboDTO> ObtenerProgramaSubArea(string idAreas, string idSubAreas)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT id, nombre FROM pla.V_TPGeneral_ObtenerDatos WHERE idArea IN (select  item from conf.F_Splitstring(@idAreas,',')) AND IdSubArea IN (select  item from conf.F_Splitstring(@idSubAreas,','))";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idAreas, idSubAreas });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboDTO> ObtenerProgramaEspecifico(string IdProgramaGeneral)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT id, nombre FROM pla.V_ListaProgramaEspecificoParaTabla WHERE IdProgramaGeneral IN (select  item from conf.F_Splitstring(@IdProgramaGeneral,',')) ";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdProgramaGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroSegmentoCompuestoDTO> ObtenerResultadoFiltro(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                var listadoFiltroSegmentoCompuestos = new List<FiltroSegmentoCompuestoDTO>();

                var query = "";

                switch (idFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        query = "mkt.SP_ObtenerResultadoFiltroTipoAlumno";
                        break;
                    case 2://docente
                        query = "";
                        break;
                    case 6:///prospecto SP_ObtenerResultadoFiltroCopiaPruebaTemporal
                        query = "mkt.SP_ObtenerResultadoFiltro";
                        break;
                    default:
                        break;
                }
                var listadoFiltroSegmentoDB = _dapperRepository.QuerySPDapper(query, new { IdFiltroSegmento = id });

                if (!string.IsNullOrEmpty(listadoFiltroSegmentoDB) && !listadoFiltroSegmentoDB.Contains("[]"))
                {
                    listadoFiltroSegmentoCompuestos = JsonConvert.DeserializeObject<List<FiltroSegmentoCompuestoDTO>>(listadoFiltroSegmentoDB);
                }
                return listadoFiltroSegmentoCompuestos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de correos a los que se va notificar
        /// </summary>
        /// <returns> bool </returns>
        public List<AddresseeDTO> ObtenerAddressee()
        {

            try
            {
                List<AddresseeDTO> ListaAddressee = new List<AddresseeDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT  Email FROM mkt.T_ListaCorreoAlerta WHERE Estado = 1 AND IdTipoCorreoAlerta IN (5,6)", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAddressee = JsonConvert.DeserializeObject<List<AddresseeDTO>>(resultado);
                }
                return ListaAddressee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Datos previamente calculados
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarPorFiltroSegmento(int idFiltroSegmento, string nombreUsuario)
        {
            try
            {
                _dapperRepository.QuerySPDapper("mkt.SP_EliminarFiltroSegmentoCalculado", new { idFiltroSegmento, nombreUsuario });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Filtro Segmento
        /// </summary>
        /// <returns> bool </returns>
        public FiltroSegmentoDTO ObtenerFiltroSegmentoDatosPorId(int id)
        {
            try
            {
                FiltroSegmentoDTO item = new FiltroSegmentoDTO();
                var _query =
                    @"
                    SELECT 
                       Id, 
                       Nombre,
                       Descripcion,
                       IdFiltroSegmentoTipoContacto, 
                       IdOperadorComparacionNroSolicitudInformacion, 
                       NroSolicitudInformacion, 
                       IdOperadorComparacionNroOportunidades, 
                       NroOportunidades, 
                       FechaInicioCreacionUltimaOportunidad, 
                       FechaFinCreacionUltimaOportunidad, 
                       FechaInicioModificacionUltimaActividadDetalle, 
                       FechaFinModificacionUltimaActividadDetalle, 
                       EsRn2, 
                       FechaInicioProgramacionUltimaActividadDetalleRn2, 
                       FechaFinProgramacionUltimaActividadDetalleRn2, 
                       FechaInicioFormulario, 
                       FechaFinFormulario, 
                       FechaInicioChatIntegra, 
                       FechaFinChatIntegra, 
                       IdOperadorComparacionTiempoMaximoRespuestaChatOnline, 
                       TiempoMaximoRespuestaChatOnline, 
                       IdOperadorComparacionNroPalabrasClienteChatOnline, 
                       NroPalabrasClienteChatOnline, 
                       IdOperadorComparacionTiempoPromedioRespuestaChatOnline, 
                       TiempoPromedioRespuestaChatOnline, 
                       IdOperadorComparacionNroPalabrasClienteChatOffline, 
                       NroPalabrasClienteChatOffline, 
                       FechaInicioCorreo, 
                       FechaFinCorreo, 
                       IdOperadorComparacionNroCorreosAbiertos, 
                       NroCorreosAbiertos, 
                       IdOperadorComparacionNroCorreosNoAbiertos, 
                       NroCorreosNoAbiertos, 
                       IdOperadorComparacionNroClicksEnlace, 
                       NroClicksEnlace, 
                       IdOperadorComparacionNroCorreosAbiertosMailchimp, 
                       NroCorreosAbiertosMailchimp, 
                       IdOperadorComparacionNroCorreosNoAbiertosMailchimp, 
                       NroCorreosNoAbiertosMailchimp, 
                       IdOperadorComparacionNroClicksEnlaceMailchimp, 
                       NroClicksEnlaceMailchimp, 
                       EsSuscribirme, 
                       EsDesuscribirme, 
                       ConsiderarFiltroGeneral, 
                       ConsiderarFiltroEspecifico, 
                       TieneVentaCruzada, 
                       IdOperadorComparacionNroTotalLineaCreditoVigente, 
                       NroTotalLineaCreditoVigente, 
                       IdOperadorComparacionMontoTotalLineaCreditoVigente, 
                       MontoTotalLineaCreditoVigente, 
                       IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente, 
                       MontoMaximoOtorgadoLineaCreditoVigente, 
                       IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente, 
                       MontoMinimoOtorgadoLineaCreditoVigente, 
                       IdOperadorComparacionNroTotalLineaCreditoVigenteVencida, 
                       NroTotalLineaCreditoVigenteVencida, 
                       IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida, 
                       MontoTotalLineaCreditoVigenteVencida, 
                       IdOperadorComparacion_NroTcOtorgada, 
                       NroTcOtorgada, 
                       IdOperadorComparacionMontoTotalOtorgadoEnTcs, 
                       MontoTotalOtorgadoEnTcs, 
                       IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc, 
                       MontoMaximoOtorgadoEnUnaTc, 
                       IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc, 
                       MontoMinimoOtorgadoEnUnaTc, 
                       IdOperadorComparacionMontoDisponibleTotalEnTcs, 
                       MontoDisponibleTotalEnTcs, 
                       FechaInicioLlamada, 
                       FechaFinLlamada, 
                       IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad, 
                       DuracionPromedioLlamadaPorOportunidad, 
                       IdOperadorComparacionDuracionTotalLlamadaPorOportunidad, 
                       DuracionTotalLlamadaPorOportunidad, 
                       IdOperadorComparacionNroLlamada, 
                       NroLlamada, 
                       IdOperadorComparacionDuracionLlamada, 
                       DuracionLlamada, 
                       IdOperadorComparacionTasaEjecucionLlamada, 
                       TasaEjecucionLlamada, 
                       FechaInicioInteraccionSitioWeb, 
                       FechaFinInteraccionSitioWeb, 
                       IdOperadorComparacionTiempoVisualizacionTotalSitioWeb, 
                       TiempoVisualizacionTotalSitioWeb, 
                       IdOperadorComparacionNroClickEnlaceTodoSitioWeb, 
                       NroClickEnlaceTodoSitioWeb, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma, 
                       TiempoVisualizacionTotalPaginaPrograma, 
                       IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas, 
                       TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas, 
                       IdOperadorComparacionNroClickEnlacePaginaPrograma, 
                       NroClickEnlacePaginaPrograma, 
                       ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma, 
                       ConsiderarClickBotonMatricularmePaginaPrograma, 
                       ConsiderarClickBotonVersionPruebaPaginaPrograma, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaBSCampus, 
                       TiempoVisualizacionTotalPaginaBSCampus, 
                       IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBSCampus, 
                       TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBSCampus, 
                       IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea, 
                       NroVisitasDirectorioTagAreaSubArea, 
                       IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea, 
                       TiempoVisualizacionTotalDirectorioTagAreaSubArea, 
                       IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea, 
                       NroClickEnlaceDirectorioTagAreaSubArea, 
                       IdOperadorComparacionNroVisitasPaginaMisCursos, 
                       NroVisitasPaginaMisCursos, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos, 
                       TiempoVisualizacionTotalPaginaMisCursos, 
                       IdOperadorComparacionNroClickEnlacePaginaMisCursos, 
                       NroClickEnlacePaginaMisCursos, 
                       IdOperadorComparacionNroVisitaPaginaCursoDiplomado, 
                       NroVisitaPaginaCursoDiplomado, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado, 
                       TiempoVisualizacionTotalPaginaCursoDiplomado, 
                       IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado, 
                       NroClicksEnlacePaginaCursoDiplomado, 
                       ConsiderarClickFiltroPaginaCursoDiplomado, 
                       IdOperadorComparacionNroSolicitudInformacionPg, 
                       NroSolicitudInformacionPg, 
                       IdOperadorComparacionNroSolicitudInformacionArea, 
                       NroSolicitudInformacionArea, 
                       IdOperadorComparacionNroSolicitudInformacionSubArea, 
                       NroSolicitudInformacionSubArea, 
                       ConsiderarOportunidadHistorica, 
                       ConsiderarCategoriaDato, 
                       ConsiderarInteraccionOfflineOnline, 
                       ConsiderarInteraccionSitioWeb, 
                       ConsiderarInteraccionFormularios, 
                       ConsiderarInteraccionChatPW, 
                       ConsiderarInteraccionCorreo, 
                       ConsiderarHistorialFinanciero, 
                       ConsiderarInteraccionWhatsApp, 
                       ConsiderarInteraccionChatMessenger, 
                       ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, 
                       FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, 
                       FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, 
                       IdTiempoFrecuenciaMatriculaAlumno, 
                       CantidadTiempoMatriculaAlumno,
                       ConsiderarConMessengerValido,
                       ConsiderarConWhatsAppValido,
                       ConsiderarConEmailValido,
                       IdTiempoFrecuenciaCumpleaniosContactoDentroDe,
                       CantidadTiempoCumpleaniosContactoDentroDe,
                       FechaInicioMatriculaAlumno,
                       FechaFinMatriculaAlumno,
                       ConsiderarAlumnosAsignacionAutomaticaOperaciones,
                       ExcluirMatriculados,
                       IdOperadorMedidaTiempoCreacionOportunidad,
                       NroMedidaTiempoCreacionOportunidad,
                       IdOperadorMedidaTiempoUltimaActividadEjecutada,
                       NroMedidaTiempoUltimaActividadEjecutada,
                       EnvioAutomaticoEstadoActividadDetalle,
                       ConsiderarYaEnviados,
                       ConsiderarEnvioAutomatico,
                       AplicaSobreCreacionOportunidad,
                       AplicaSobreUltimaActividad,
                       ConsiderarUltimaOportunidad
                FROM mkt.V_TFiltroSegmento_Panel
                WHERE Id = @id
                      AND Estado = 1;
                ";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { id });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    item = JsonConvert.DeserializeObject<FiltroSegmentoDTO>(respuestaDapper);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Filtro valor segmento
        /// </summary>
        /// <returns> bool </returns>
        public List<FiltroSegmentoValorTipoDTO> ObtenerFiltroValorPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FiltroSegmentoValorTipoDTO> items = new List<FiltroSegmentoValorTipoDTO>();
                string _query = @"
                                SELECT Id, IdCategoriaObjetoFiltro, Valor
                                FROM [mkt].[V_TFiltroSegmentoValorTipo_ConfiguracionFiltroSegmento]
                                WHERE Estado = 1 AND IdFiltroSegmento = @idFiltroSegmento";

                string filtroSegmentoValorTipoDB = _dapperRepository.QueryDapper(_query, new { idFiltroSegmento });

                items = JsonConvert.DeserializeObject<List<FiltroSegmentoValorTipoDTO>>(filtroSegmentoValorTipoDB);

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FiltroSegmentoDetalleDTO> ObtenerDetallePorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FiltroSegmentoDetalleDTO> items = new List<FiltroSegmentoDetalleDTO>();
                string _query = @"
                                SELECT Id, IdCategoriaObjetoFiltro, Valor, IdOperadorComparacion, CantidadTiempoFrecuencia, IdTiempoFrecuencia
                                FROM [mkt].[V_TFiltroSegmentoDetalle_ConfiguracionFiltroSegmento]
                                WHERE Estado = 1 AND IdFiltroSegmento = @idFiltroSegmento";

                string filtroSegmentoDetalleDB = _dapperRepository.QueryDapper(_query, new { idFiltroSegmento });

                items = JsonConvert.DeserializeObject<List<FiltroSegmentoDetalleDTO>>(filtroSegmentoDetalleDB);

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void EjecutarFiltroTipoContactoAlumnoExAlumno(FiltroSegmentoDTO obj)
        {
            try
            {
                var IdFiltroSegmento = obj.Id;
                var ListaAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var ListaSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var ListaPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var ListaPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));
                var ListaEstadoMatricula = string.Join(",", obj.ListaEstadoMatricula.Select(x => x.Valor));
                var ListaSubEstadoMatricula = string.Join(",", obj.ListaSubEstadoMatricula.Select(x => x.Valor));
                var ListaModalidadCurso = string.Join(",", obj.ListaModalidadCurso.Select(x => x.Valor));
                var ListaPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                var ListaCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));
                var ListaDocumentoAlumno = string.Join(",", obj.ListaDocumentoAlumno.Select(x => x.Valor));
                var ListaActividadCabecera = string.Join(",", obj.ListaActividadCabecera.Select(x => x.Valor));
                var ListaOcurrencia = string.Join(",", obj.ListaOcurrencia.Select(x => x.Valor));
                var CantidadTiempoMatriculaAlumno = obj.CantidadTiempoMatriculaAlumno;
                var IdTiempoFrecuenciaMatriculaAlumno = obj.IdTiempoFrecuenciaMatriculaAlumno;
                var FechaInicioMatriculaAlumno = obj.FechaInicioMatriculaAlumno;
                var FechaFinMatriculaAlumno = obj.FechaFinMatriculaAlumno;
                var CantidadTiempoCumpleaniosContactoDentroDe = obj.CantidadTiempoCumpleaniosContactoDentroDe;
                var IdTiempoFrecuenciaCumpleaniosContactoDentroDe = obj.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
                var NombreUsuario = obj.NombreUsuario;
                var ConsiderarTabEstadoSesion = obj.ListaSesion.Any();
                var ConsiderarTabEstadoAvanceAcademico = obj.ListaEstadoAcademico.Any();
                var ConsiderarTabEstadoPago = obj.ListaEstadoPago.Any();
                var ConsiderarTabPorcentajeAvance = obj.ListaPorcentajeAvance.Any();
                var ConsiderarTabEstadoLlamadaTelefonica = obj.ListaEstadoLlamada.Any();
                var ConsiderarTabEstadoSesionWebinar = obj.ListaSesionWebinar.Any();
                var ConsiderarTabEstadoTrabajoAlumno = (obj.ListaTrabajoAlumno.Any() || obj.ListaTrabajoAlumnoFinal.Any()) ? true : false;
                var ConsiderarConMessengerValido = obj.ConsiderarConMessengerValido;
                var ConsiderarConWhatsAppValido = obj.ConsiderarConWhatsAppValido;
                var ConsiderarConEmailValido = obj.ConsiderarConEmailValido;
                var ListaTarifario = string.Join(",", obj.ListaTarifario.Select(x => x.Valor));
                var ConsiderarAlumnosAsignacionAutomaticaOperaciones = obj.ConsiderarAlumnosAsignacionAutomaticaOperaciones;

                var parametros = new
                {
                    IdFiltroSegmento,
                    ListaAreaCapacitacion,
                    ListaSubAreaCapacitacion,
                    ListaPGeneral,
                    ListaPEspecifico,
                    ListaEstadoMatricula,
                    ListaSubEstadoMatricula,
                    ListaModalidadCurso,
                    ListaPais,
                    ListaCiudad,
                    ListaDocumentoAlumno,
                    ListaActividadCabecera,
                    ListaOcurrencia,
                    CantidadTiempoMatriculaAlumno,
                    IdTiempoFrecuenciaMatriculaAlumno,
                    FechaInicioMatriculaAlumno,
                    FechaFinMatriculaAlumno,
                    CantidadTiempoCumpleaniosContactoDentroDe,
                    IdTiempoFrecuenciaCumpleaniosContactoDentroDe,
                    NombreUsuario,
                    ConsiderarTabEstadoSesion,
                    ConsiderarTabEstadoAvanceAcademico,
                    ConsiderarTabEstadoPago,
                    ConsiderarTabPorcentajeAvance,
                    ConsiderarTabEstadoLlamadaTelefonica,
                    ConsiderarTabEstadoSesionWebinar,
                    ConsiderarTabEstadoTrabajoAlumno,
                    ConsiderarConMessengerValido,
                    ConsiderarConWhatsAppValido,
                    ConsiderarConEmailValido,
                    ListaTarifario,
                    ConsiderarAlumnosAsignacionAutomaticaOperaciones
                };

                _dapperRepository.QuerySPDapper("mkt.SP_EjecutarFiltroSegmentoTipoAlumno", parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void EjecutarFiltroTipoContactoProspecto(FiltroSegmentoDTO obj)
        {
            try
            {
                var IdFiltroSegmento = obj.Id;
                var ListaAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var ListaSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var ListaPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var ListaPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));

                var IdOperadorComparacionNroSolicitudInformacion = obj.IdOperadorComparacionNroSolicitudInformacion;
                var NroSolicitudInformacion = obj.NroSolicitudInformacion;
                var IdOperadorComparacionNroOportunidades = obj.IdOperadorComparacionNroOportunidades;
                var NroOportunidades = obj.NroOportunidades;

                var EsRN2 = obj.EsRn2;
                DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 = null;
                DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 = null;

                if (EsRN2)
                {
                    FechaInicioProgramacionUltimaActividadDetalleRn2 = obj.FechaInicioProgramacionUltimaActividadDetalleRn2.Value.Date;
                    FechaFinProgramacionUltimaActividadDetalleRn2 = obj.FechaFinProgramacionUltimaActividadDetalleRn2.Value.Date;
                }


                var IdOperadorComparacionNroSolicitudInformacionPg = obj.IdOperadorComparacionNroSolicitudInformacionPg;
                var NroSolicitudInformacionPg = obj.NroSolicitudInformacionPg;
                var IdOperadorComparacionNroSolicitudInformacionArea = obj.IdOperadorComparacionNroSolicitudInformacionArea;
                var NroSolicitudInformacionArea = obj.NroSolicitudInformacionArea;
                var IdOperadorComparacionNroSolicitudInformacionSubArea = obj.IdOperadorComparacionNroSolicitudInformacionSubArea;
                var NroSolicitudInformacionSubArea = obj.NroSolicitudInformacionSubArea;

                var ExcluirMatriculados = obj.ExcluirMatriculados;

                var FechaInicioCreacionUltimaOportunidad = obj.FechaInicioCreacionUltimaOportunidad;
                var FechaFinCreacionUltimaOportunidad = obj.FechaFinCreacionUltimaOportunidad;

                var FechaInicioModificacionUltimaActividadDetalle = obj.FechaInicioModificacionUltimaActividadDetalle;
                var FechaFinModificacionUltimaActividadDetalle = obj.FechaFinModificacionUltimaActividadDetalle;

                var ListaOportunidadInicialFaseMaxima = string.Join(",", obj.ListaOportunidadInicialFaseMaxima.Select(x => x.Valor));
                var ListaOportunidadInicialFaseActual = string.Join(",", obj.ListaOportunidadInicialFaseActual.Select(x => x.Valor));
                var ListaOportunidadActualFaseMaxima = string.Join(",", obj.ListaOportunidadActualFaseMaxima.Select(x => x.Valor));
                var ListaOportunidadActualFaseActual = string.Join(",", obj.ListaOportunidadActualFaseActual.Select(x => x.Valor));

                var ListaPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                var ListaCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));

                var ListaTipoCategoriaOrigen = string.Join(",", obj.ListaTipoCategoriaOrigen.Select(x => x.Valor));
                var ListaCategoriaOrigen = string.Join(",", obj.ListaCategoriaOrigen.Select(x => x.Valor));


                var ListaCargo = string.Join(",", obj.ListaCargo.Select(x => x.Valor));
                var ListaIndustria = string.Join(",", obj.ListaIndustria.Select(x => x.Valor));
                var ListaAreaFormacion = string.Join(",", obj.ListaAreaFormacion.Select(x => x.Valor));
                var ListaAreaTrabajo = string.Join(",", obj.ListaAreaTrabajo.Select(x => x.Valor));

                var FechaInicioFormulario = obj.FechaInicioFormulario;
                var FechaFinFormulario = obj.FechaFinFormulario;
                var ListaTipoFormulario = string.Join(",", obj.ListaTipoFormulario.Select(x => x.Valor));
                var ListaTipoInteraccionFormulario = string.Join(",", obj.ListaTipoInteraccionFormulario.Select(x => x.Valor));

                var ListaProbabilidadOportunidad = string.Join(",", obj.ListaProbabilidadOportunidad.Select(x => x.Valor));
                var ListaActividadLlamada = string.Join(",", obj.ListaActividadLlamada.Select(x => x.Valor));

                var FechaInicioChatIntegra = obj.FechaInicioChatIntegra;
                var FechaFinChatIntegra = obj.FechaFinChatIntegra;
                var IdOperadorComparacionTiempoMaximoRespuestaChatOnline = obj.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                var TiempoMaximoRespuestaChatOnline = obj.TiempoMaximoRespuestaChatOnline;
                var IdOperadorComparacionNroPalabrasClienteChatOnline = obj.IdOperadorComparacionNroPalabrasClienteChatOnline;
                var NroPalabrasClienteChatOnline = obj.NroPalabrasClienteChatOnline;
                var IdOperadorComparacionTiempoPromedioRespuestaChatOnline = obj.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                var TiempoPromedioRespuestaChatOnline = obj.TiempoPromedioRespuestaChatOnline;
                var IdOperadorComparacionNroPalabrasClienteChatOffline = obj.IdOperadorComparacionNroPalabrasClienteChatOffline;
                var NroPalabrasClienteChatOffline = obj.NroPalabrasClienteChatOffline;

                var FechaInicioCorreo = obj.FechaInicioCorreo;
                var FechaFinCorreo = obj.FechaFinCorreo;
                var IdOperadorComparacionNroCorreosAbiertos = obj.IdOperadorComparacionNroCorreosAbiertos;
                var NroCorreosAbiertos = obj.NroCorreosAbiertos;
                var IdOperadorComparacionNroCorreosNoAbiertos = obj.IdOperadorComparacionNroCorreosNoAbiertos;
                var NroCorreosNoAbiertos = obj.NroCorreosNoAbiertos;
                var IdOperadorComparacionNroClicksEnlace = obj.IdOperadorComparacionNroClicksEnlace;
                var NroClicksEnlace = obj.NroClicksEnlace;
                var EsSuscribirme = obj.EsSuscribirme;
                var EsDesuscribirme = obj.EsDesuscribirme;

                var IdOperadorComparacionNroCorreosAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                var NroCorreosAbiertosMailChimp = obj.NroCorreosAbiertosMailChimp;
                var IdOperadorComparacionNroCorreosNoAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                var NroCorreosNoAbiertosMailChimp = obj.NroCorreosNoAbiertosMailChimp;
                var IdOperadorComparacionNroClicksEnlaceMailChimp = obj.IdOperadorComparacionNroClicksEnlaceMailChimp;
                var NroClicksEnlaceMailChimp = obj.NroClicksEnlaceMailChimp;

                var ConsiderarFiltroGeneral = obj.ConsiderarFiltroGeneral;
                var ConsiderarFiltroEspecifico = obj.ConsiderarFiltroEspecifico;
                var TieneVentaCruzada = obj.TieneVentaCruzada;

                var FechaInicioLlamada = obj.FechaInicioLlamada;
                var FechaFinLlamada = obj.FechaFinLlamada;

                var IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                var DuracionPromedioLlamadaPorOportunidad = obj.DuracionPromedioLlamadaPorOportunidad;
                var IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                var DuracionTotalLlamadaPorOportunidad = obj.DuracionTotalLlamadaPorOportunidad;
                var IdOperadorComparacionNroLlamada = obj.IdOperadorComparacionNroLlamada;
                var NroLlamada = obj.NroLlamada;
                var IdOperadorComparacionDuracionLlamada = obj.IdOperadorComparacionDuracionLlamada;
                var DuracionLlamada = obj.DuracionLlamada;
                var IdOperadorComparacionTasaEjecucionLlamada = obj.IdOperadorComparacionTasaEjecucionLlamada;
                var TasaEjecucionLlamada = obj.TasaEjecucionLlamada;

                var IdOperadorComparacionNroTotalLineaCreditoVigente = obj.IdOperadorComparacionNroTotalLineaCreditoVigente;
                var NroTotalLineaCreditoVigente = obj.NroTotalLineaCreditoVigente;
                var IdOperadorComparacionMontoTotalLineaCreditoVigente = obj.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                var MontoTotalLineaCreditoVigente = obj.MontoTotalLineaCreditoVigente;
                var IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                var MontoMaximoOtorgadoLineaCreditoVigente = obj.MontoMaximoOtorgadoLineaCreditoVigente;
                var IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                var MontoMinimoOtorgadoLineaCreditoVigente = obj.MontoMinimoOtorgadoLineaCreditoVigente;
                var IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                var NroTotalLineaCreditoVigenteVencida = obj.NroTotalLineaCreditoVigenteVencida;
                var IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                var MontoTotalLineaCreditoVigenteVencida = obj.MontoTotalLineaCreditoVigenteVencida;
                var IdOperadorComparacionNroTcOtorgada = obj.IdOperadorComparacionNroTcOtorgada;
                var NroTcOtorgada = obj.NroTcOtorgada;
                var IdOperadorComparacionMontoTotalOtorgadoEnTcs = obj.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                var MontoTotalOtorgadoEnTcs = obj.MontoTotalOtorgadoEnTcs;

                var IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                var MontoMaximoOtorgadoEnUnaTc = obj.MontoMaximoOtorgadoEnUnaTc;
                var IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                var MontoMinimoOtorgadoEnUnaTc = obj.MontoMinimoOtorgadoEnUnaTc;
                var IdOperadorComparacionMontoDisponibleTotalEnTcs = obj.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                var MontoDisponibleTotalEnTcs = obj.MontoDisponibleTotalEnTcs;


                var FechaInicioInteraccionSitioWeb = obj.FechaInicioInteraccionSitioWeb;
                var FechaFinInteraccionSitioWeb = obj.FechaFinInteraccionSitioWeb;
                var IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = obj.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                var TiempoVisualizacionTotalSitioWeb = obj.TiempoVisualizacionTotalSitioWeb;
                var IdOperadorComparacionNroClickEnlaceTodoSitioWeb = obj.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                var NroClickEnlaceTodoSitioWeb = obj.NroClickEnlaceTodoSitioWeb;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                var TiempoVisualizacionTotalPaginaPrograma = obj.TiempoVisualizacionTotalPaginaPrograma;
                var IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var IdOperadorComparacionNroClickEnlacePaginaPrograma = obj.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                var NroClickEnlacePaginaPrograma = obj.NroClickEnlacePaginaPrograma;
                var ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = obj.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                var ConsiderarClickBotonMatricularmePaginaPrograma = obj.ConsiderarClickBotonMatricularmePaginaPrograma;
                var ConsiderarClickBotonVersionPruebaPaginaPrograma = obj.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;
                var TiempoVisualizacionTotalPaginaBscampus = obj.TiempoVisualizacionTotalPaginaBscampus;
                var IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                var TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;

                var IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                var NroVisitasDirectorioTagAreaSubArea = obj.NroVisitasDirectorioTagAreaSubArea;
                var IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var TiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                var NroClickEnlaceDirectorioTagAreaSubArea = obj.NroClickEnlaceDirectorioTagAreaSubArea;
                var IdOperadorComparacionNroVisitasPaginaMisCursos = obj.IdOperadorComparacionNroVisitasPaginaMisCursos;
                var NroVisitasPaginaMisCursos = obj.NroVisitasPaginaMisCursos;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                var TiempoVisualizacionTotalPaginaMisCursos = obj.TiempoVisualizacionTotalPaginaMisCursos;
                var IdOperadorComparacionNroClickEnlacePaginaMisCursos = obj.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                var NroClickEnlacePaginaMisCursos = obj.NroClickEnlacePaginaMisCursos;
                var IdOperadorComparacionNroVisitaPaginaCursoDiplomado = obj.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                var NroVisitaPaginaCursoDiplomado = obj.NroVisitaPaginaCursoDiplomado;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                var TiempoVisualizacionTotalPaginaCursoDiplomado = obj.TiempoVisualizacionTotalPaginaCursoDiplomado;
                var IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = obj.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                var NroClicksEnlacePaginaCursoDiplomado = obj.NroClicksEnlacePaginaCursoDiplomado;
                var ConsiderarClickFiltroPaginaCursoDiplomado = obj.ConsiderarClickFiltroPaginaCursoDiplomado;


                var ListaVCAreaCapacitacion = string.Join(",", obj.ListaVCArea.Select(x => x.Valor));
                var ListaVCSubAreaCapacitacion = string.Join(",", obj.ListaVCSubArea.Select(x => x.Valor));
                var ListaVCPGeneralCapacitacion = string.Join(",", obj.ListaVCPGeneral.Select(x => x.Valor));

                var ListaProbabilidadVentaCruzada = string.Join(",", obj.ListaProbabilidadVentaCruzada.Select(x => x.Valor));

                var ConsiderarTabOportunidadHistorica = obj.ConsiderarOportunidadHistorica;
                var ConsiderarTabCategoriaDato = obj.ConsiderarCategoriaDato;
                var ConsiderarTabInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline;
                var ConsiderarTabInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb;
                var ConsiderarTabInteraccionFormularios = obj.ConsiderarInteraccionFormularios;
                var ConsiderarTabInteraccionChatPw = obj.ConsiderarInteraccionChatPw;
                var ConsiderarTabInteraccionCorreo = obj.ConsiderarInteraccionCorreo;
                var ConsiderarTabHistorialFinanciero = obj.ConsiderarHistorialFinanciero;
                var ConsiderarTabInteraccionWhatsApp = obj.ConsiderarInteraccionWhatsApp;
                var ConsiderarTabInteraccionChatMessenger = obj.ConsiderarInteraccionChatMessenger;

                var ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                var ConsiderarConMessengerValido = obj.ConsiderarConMessengerValido;
                var ConsiderarConWhatsAppValido = obj.ConsiderarConWhatsAppValido;
                var ConsiderarConEmailValido = obj.ConsiderarConEmailValido;

                var NombreUsuario = obj.NombreUsuario;

                var parametros = new
                {
                    IdFiltroSegmento,
                    ListaAreaCapacitacion,
                    ListaSubAreaCapacitacion,
                    ListaPGeneral,
                    ListaPEspecifico,

                    IdOperadorComparacionNroSolicitudInformacion,
                    NroSolicitudInformacion,
                    IdOperadorComparacionNroOportunidades,
                    NroOportunidades,

                    EsRN2,
                    FechaInicioProgramacionUltimaActividadDetalleRn2,
                    FechaFinProgramacionUltimaActividadDetalleRn2,

                    FechaInicioCreacionUltimaOportunidad,
                    FechaFinCreacionUltimaOportunidad,

                    FechaInicioModificacionUltimaActividadDetalle,
                    FechaFinModificacionUltimaActividadDetalle,

                    ListaOportunidadInicialFaseMaxima,
                    ListaOportunidadInicialFaseActual,
                    ListaOportunidadActualFaseMaxima,
                    ListaOportunidadActualFaseActual,

                    ListaPais,
                    ListaCiudad,

                    ListaTipoCategoriaOrigen,
                    ListaCategoriaOrigen,

                    ListaCargo,
                    ListaIndustria,
                    ListaAreaFormacion,
                    ListaAreaTrabajo,

                    FechaInicioFormulario,
                    FechaFinFormulario,
                    ListaTipoFormulario,
                    ListaTipoInteraccionFormulario,

                    FechaInicioChatIntegra,
                    FechaFinChatIntegra,
                    IdOperadorComparacionTiempoMaximoRespuestaChatOnline,
                    TiempoMaximoRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOnline,
                    NroPalabrasClienteChatOnline,
                    IdOperadorComparacionTiempoPromedioRespuestaChatOnline,
                    TiempoPromedioRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOffline,
                    NroPalabrasClienteChatOffline,

                    FechaInicioCorreo,
                    FechaFinCorreo,
                    IdOperadorComparacionNroCorreosAbiertos,
                    NroCorreosAbiertos,
                    IdOperadorComparacionNroCorreosNoAbiertos,
                    NroCorreosNoAbiertos,
                    IdOperadorComparacionNroClicksEnlace,
                    NroClicksEnlace,
                    EsSuscribirme,
                    EsDesuscribirme,

                    IdOperadorComparacionNroCorreosAbiertosMailChimp,
                    NroCorreosAbiertosMailChimp,
                    IdOperadorComparacionNroCorreosNoAbiertosMailChimp,
                    NroCorreosNoAbiertosMailChimp,
                    IdOperadorComparacionNroClicksEnlaceMailChimp,
                    NroClicksEnlaceMailChimp,

                    ConsiderarFiltroGeneral,
                    ConsiderarFiltroEspecifico,
                    TieneVentaCruzada,

                    ListaProbabilidadOportunidad,
                    ListaActividadLlamada,

                    FechaInicioLlamada,
                    FechaFinLlamada,

                    IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad,
                    DuracionPromedioLlamadaPorOportunidad,
                    IdOperadorComparacionDuracionTotalLlamadaPorOportunidad,
                    DuracionTotalLlamadaPorOportunidad,
                    IdOperadorComparacionNroLlamada,
                    NroLlamada,
                    IdOperadorComparacionDuracionLlamada,
                    DuracionLlamada,
                    IdOperadorComparacionTasaEjecucionLlamada,
                    TasaEjecucionLlamada,

                    IdOperadorComparacionNroTotalLineaCreditoVigente,
                    NroTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoTotalLineaCreditoVigente,
                    MontoTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente,
                    MontoMaximoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente,
                    MontoMinimoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionNroTotalLineaCreditoVigenteVencida,
                    NroTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida,
                    MontoTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionNroTcOtorgada,
                    NroTcOtorgada,
                    IdOperadorComparacionMontoTotalOtorgadoEnTcs,
                    MontoTotalOtorgadoEnTcs,

                    IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc,
                    MontoMaximoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc,
                    MontoMinimoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoDisponibleTotalEnTcs,
                    MontoDisponibleTotalEnTcs,
                    ExcluirMatriculados,

                    FechaInicioInteraccionSitioWeb,
                    FechaFinInteraccionSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalSitioWeb,
                    TiempoVisualizacionTotalSitioWeb,
                    IdOperadorComparacionNroClickEnlaceTodoSitioWeb,
                    NroClickEnlaceTodoSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma,
                    TiempoVisualizacionTotalPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,

                    IdOperadorComparacionNroClickEnlacePaginaPrograma,
                    NroClickEnlacePaginaPrograma,
                    ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma,
                    ConsiderarClickBotonMatricularmePaginaPrograma,
                    ConsiderarClickBotonVersionPruebaPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus,
                    TiempoVisualizacionTotalPaginaBscampus,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea,

                    NroVisitasDirectorioTagAreaSubArea,
                    IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    TiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea,
                    NroClickEnlaceDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroVisitasPaginaMisCursos,
                    NroVisitasPaginaMisCursos,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos,
                    TiempoVisualizacionTotalPaginaMisCursos,
                    IdOperadorComparacionNroClickEnlacePaginaMisCursos,

                    NroClickEnlacePaginaMisCursos,
                    IdOperadorComparacionNroVisitaPaginaCursoDiplomado,
                    NroVisitaPaginaCursoDiplomado,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado,
                    TiempoVisualizacionTotalPaginaCursoDiplomado,
                    IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado,
                    NroClicksEnlacePaginaCursoDiplomado,
                    ConsiderarClickFiltroPaginaCursoDiplomado,

                    ListaVCAreaCapacitacion,
                    ListaVCSubAreaCapacitacion,
                    ListaVCPGeneralCapacitacion,

                    ListaProbabilidadVentaCruzada,

                    //nuevos filtros
                    IdOperadorComparacionNroSolicitudInformacionPg,
                    NroSolicitudInformacionPg,
                    IdOperadorComparacionNroSolicitudInformacionArea,
                    NroSolicitudInformacionArea,
                    IdOperadorComparacionNroSolicitudInformacionSubArea,
                    NroSolicitudInformacionSubArea,

                    //filtros tabs
                    ConsiderarTabOportunidadHistorica,
                    ConsiderarTabCategoriaDato,
                    ConsiderarTabInteraccionOfflineOnline,
                    ConsiderarTabInteraccionSitioWeb,
                    ConsiderarTabInteraccionFormularios,
                    ConsiderarTabInteraccionChatPw,
                    ConsiderarTabInteraccionCorreo,
                    ConsiderarTabHistorialFinanciero,
                    ConsiderarTabInteraccionWhatsApp,
                    ConsiderarTabInteraccionChatMessenger,

                    ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,

                    ConsiderarConMessengerValido,
                    ConsiderarConWhatsAppValido,
                    ConsiderarConEmailValido,

                    NombreUsuario
                };

                var listadoFiltroSegmentoDB = _dapperRepository.QuerySPDapper("mkt.SP_EjecutarFiltroSegmento", parametros, 55);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FacebookAudienciaComboDTO> ObtenerComboFacebook()
        {
            try
            {
                List<FacebookAudienciaComboDTO> listaFacebookAudiencia = new List<FacebookAudienciaComboDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT  Email FROM mkt.T_ListaCorreoAlerta WHERE Estado = 1 AND IdTipoCorreoAlerta IN (5,6)", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    listaFacebookAudiencia = JsonConvert.DeserializeObject<List<FacebookAudienciaComboDTO>>(resultado);
                }
                return listaFacebookAudiencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FacebookAudienciaHistorialDTO> listaFacebookAudienciaHistorial = new List<FacebookAudienciaHistorialDTO>();
                var _query = "SELECT NombreCuentaPublicitaria, FacebookIdCuentaPublicitaria, FacebookIdAudiencia, Nombre, FechaModificacion, Subtipo FROM mkt.V_ObtenerAudienciaCuentaPublicitaria WHERE IdFiltroSegmento = @idFiltroSegmento AND EstadoFacebookAudiencia = 1 AND EstadoFacebookAudienciaCuentaPublicitaria = 1 AND Origen = 'Propio'";
                var respuestaQuery = _dapperRepository.QueryDapper(_query, new { idFiltroSegmento });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]") && !respuestaQuery.Contains("null"))
                {
                    listaFacebookAudienciaHistorial = JsonConvert.DeserializeObject<List<FacebookAudienciaHistorialDTO>>(respuestaQuery);
                }
                return listaFacebookAudienciaHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool? InsertarFacebookAudienciaAlumno(string ListaIdAlumno, int IdFacebookAudiencia, string UsuarioCreacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_InsertarFacebookAudienciaAlumno", new { ListaIdAlumno, IdFacebookAudiencia, UsuarioCreacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Alumno ObtenerAlumnoPorId(int idAlumno)
        {
            try
            {
                var query = @"
                    SELECT Id,
                        Nombre1,
                        Nombre2,
                        ApellidoPaterno,
                        ApellidoMaterno,
                        COALESCE(DNI, NroDocumento) AS Dni,
                        Direccion,
                        FechaNacimiento,
                        Pais,
                        Ciudad,
                        Telefono,
                        Celular,
                        Email1,
                        Email2,
                        NivelFormacion,
                        Profesion,
                        Empresa,
                        EstadoCivil,
                        TelefonoFamiliar,
                        NombreFamiliar,
                        Parentesco,
                        TelefonoTrabajo,
                        TelefonoTrabajoAnexo,
                        Genero,
                        Skype,
                        Fax,
                        IdPais,
                        UbigeoPais,
                        UbigeoDepartamento,
                        UbigeoProvincia,
                        UbigeoCiudad,
                        UbigeoDistrito,
                        DireccionCalle,
                        DireccionAv,
                        DireccionZona,
                        DireccionComp,
                        DireccionTorre,
                        DireccionEdificio,
                        DireccionDpto,
                        DireccionUrb,
                        DireccionMz,
                        DireccionLt,
                        ReferenciaDetallada,
                        HoraMaxima,
                        Puesto,
                        AniversarioBodas,
                        NroHijo,
                        ValidacionTelefonica,
                        FaseContacto,
                        IdCargo,
                        Cargo,
                        IdAFormacion,
                        AFormacion,
                        IdATrabajo,
                        ATrabajo,
                        IdIndustria,
                        Industria,
                        IdReferido,
                        Referido,
                        IdCodigoPais,
                        NombrePais,
                        IdCiudad,
                        NombreCiudad,
                        HoraContacto,
                        HoraPeru,
                        IdCodigoRegionCiudad,
                        Telefono2,
                        Celular2,
                        IdEmpresa,
                        IdOportunidad_Inicial as  IdOportunidadInicial,
                        UsClave,
                        IdTipoDocumento,
                        NroDocumento,
                        DescripcionCargo,
                        Asociado,
                        DeSuscrito,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        NroOportunidades,
                        EsPersonaValida,
                        EsEliminadoPorRegularizacion,
                        TieneOportunidad,
                        TieneMatricula,
                        EsRepetido,
                        IdEstadoContactoWhatsApp,
                        IdEstadoContactoMailing,
                        DireccionEnvioCertificado,
                        UsarNuevaDireccionParaEnvio,
                        CiudadEnvioCertificado,
                        IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
                        CodigoPortal,
                        IdNumeroTipoDocumento,
                        IdGenero,
                        Comentario,
                        Municipio,
                        IdMunicipioMexico,
                        EstadoLugar,
                        CodigoPostal,
                        Colonia,
                        IdAsentamientoMexico,
                        IdCiudadMexico,
                        Curp,
                        Rfc,
                        PrincipalResponsabilidadProfesional,
                        IdExperiencia,
                        IdTamanioEmpresaAgenda
                    FROM mkt.T_Alumno
                    WHERE Estado = 1 AND Id = @idAlumno";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Alumno>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}




