using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoCampaniaGeneralWhatsAppDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CampaniaGeneralWhatsAppRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneralWhatsApp
    /// </summary>
    public class CampaniaGeneralWhatsAppRepository : GenericRepository<TCampaniaGeneralWhatsApp>, ICampaniaGeneralWhatsAppRepository
    {
        private Mapper _mapper;

        public CampaniaGeneralWhatsAppRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneralWhatsApp, CampaniaGeneralWhatsApp>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCampaniaGeneralWhatsApp MapeoEntidad(CampaniaGeneralWhatsApp entidad)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralWhatsApp modelo = _mapper.Map<TCampaniaGeneralWhatsApp>(entidad);

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

        public TCampaniaGeneralWhatsApp Add(CampaniaGeneralWhatsApp entidad)
        {
            try
            {
                var CampaniaGeneralWhatsApp = MapeoEntidad(entidad);
                base.Insert(CampaniaGeneralWhatsApp);
                return CampaniaGeneralWhatsApp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralWhatsApp Update(CampaniaGeneralWhatsApp entidad)
        {
            try
            {
                var CampaniaGeneralWhatsApp = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampaniaGeneralWhatsApp.RowVersion = entidadExistente.RowVersion;

                base.Update(CampaniaGeneralWhatsApp);
                return CampaniaGeneralWhatsApp;
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

        public IEnumerable<TCampaniaGeneralWhatsApp> Add(IEnumerable<CampaniaGeneralWhatsApp> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralWhatsApp> listado = new List<TCampaniaGeneralWhatsApp>();
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

        public IEnumerable<TCampaniaGeneralWhatsApp> Update(IEnumerable<CampaniaGeneralWhatsApp> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralWhatsApp> listado = new List<TCampaniaGeneralWhatsApp>();
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

        

        public List<ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO> ObtenerCampaniaGeneralDetalleWhatsApp(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO> dto = new List<ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO>();
                _query = "SELECT Id,NombreCampaniaGeneralWhatsApp,FechaInicioEnvioWhatsapp,HoraEnvio,COALESCE(IdCampaniaGeneralDetalleWhatsApp,0)AS IdCampaniaGeneralDetalleWhatsApp,COALESCE(NombreCampaniaOrigen,'') AS NombreCampaniaOrigen,COALESCE(Prioridad,0) AS Prioridad,COALESCE(Nombre,'') AS Nombre,COALESCE(ActivarMasivo,0) AS ActivarMasivo,COALESCE(Programados,0) AS Programados,COALESCE(CantidadBase, 0) AS CantidadBase,COALESCE(Enviados,0)AS Enviados  FROM mkt.V_ObtenerCampaniaGeneralDetalleWhatsApp WHERE Id =  @Id ORDER BY IdCampaniaGeneralDetalleWhatsApp ASC	;";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarCampaniaGeneralWhatsApp(StringDTO nombreCampania, string usuario)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralWhatsApp] @Nombre, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = nombreCampania.Valor, Usuario = usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public CampaniaGeneralWhatsAppDTO ObtenerCampaniaGeneralWhatsApp(IdDTO id)
        {
            try
            {
                var _query = "SELECT Nombre, FechaInicioEnvioWhatsapp, HoraEnvio FROM mkt.T_CampaniaGeneralWhatsApp WHERE Id = @Id";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    var dto = JsonConvert.DeserializeObject<CampaniaGeneralWhatsAppDTO>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ActualizarCampaniaGeneralWhatsApp(ActualizarCampaniaGeneralWhatsAppDTO json)
        {
            try
            {
                var _query = string.Empty; 
                _query = "exec [mkt].[SP_ActualizarCampaniaGeneralWhatsApp] @Nombre, @HoraEnvio, @FechaInicioWhatsapp,  @Id, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, HoraEnvio = json.HoraEnvio, FechaInicioWhatsapp = json.FechaInicioEnvioWhatsapp, Id = json.Id, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ObtenerCampaniaGeneralGrillaWhatsAppDTO> ObtenerCampaniaGeneralGrillaWhatsApp( )
        {
            try
            {
                List<ObtenerCampaniaGeneralGrillaWhatsAppDTO> ObtenerCampaniaGeneralGrillaWhatsApp = new List<ObtenerCampaniaGeneralGrillaWhatsAppDTO>();
                var _query = string.Empty;
                _query = "SELECT CGW.Id,CGW.Nombre,	CGW.FechaInicioEnvioWhatsapp,CGW.HoraEnvio,CGW.Cantidad FROM mkt.V_ObtenerCampaniaGeneralGrillaWhatsApp AS CGW WITH(NOLOCK) ORDER BY Id DESC";
                var respuesta = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                {
                    return ObtenerCampaniaGeneralGrillaWhatsApp = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralGrillaWhatsAppDTO>>(respuesta);
                }
                else return ObtenerCampaniaGeneralGrillaWhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public bool EliminarCampaniaGeneralWhatsApp(EliminarCampaniaGeneralWhatsAppDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_EliminarCampaniaGeneralWhatsApp] @IdCampaniaGeneralWhatsApp, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new {  IdCampaniaGeneralWhatsApp = json.Id , Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_ActualizarActivarMasivoPorCampania]  @ActivarMasivo, @Id,  @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = json.IdCampaniaGeneralDetalleWhatsApp, ActivarMasivo = json.ActivarMasivo, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EliminarCampaniaGeneralDetalleWhatsApp(EliminarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_EliminarCampaniaGeneralDetalleWhatsApp] @IdCampaniaGeneralDetalleWhatsApp, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarCampaniaGeneralDetalleWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralDetalleWhatsApp] @Nombre,@IdCampaniaGeneralWhatsApp,@Prioridad,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, IdCampaniaGeneralWhatsApp = json.IdCampaniaGeneralWhatsApp, Prioridad = json.Prioridad, Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool InsertarCampaniaGeneralDetalleExcelWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralDetalleExcelWhatsApp] @Nombre,@IdCampaniaGeneralWhatsApp,@Prioridad,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, IdCampaniaGeneralWhatsApp = json.IdCampaniaGeneralWhatsApp, Prioridad = json.Prioridad, Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ActualizarCamposCampaniaGeneralDetalleWhatsApp(ActualizarCamposCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_ActualizarCamposCampaniaGeneralDetalleWhatsApp] @Nombre , @IdCampaniaGeneralDetalleWhatsApp,@Prioridad,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre , IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp , Prioridad = json.Prioridad , Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO dto = new ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO();
                _query = "SELECT Id, Nombre, Prioridad, IdCampaniaGeneral,IdCampaniaGeneralDetalle FROM mkt.V_ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp WHERE Id =@Id;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> dto = new List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO>();
                _query = "SELECT Id,CantidadBase,CantidadDisponible,IdCampaniaGeneralDetalleResponsableWhatsApp,Asesor,Plantilla,CentroCosto,Cantidad,Enviados,AlumnoConfigurado FROM mkt.V_ObtenerCampaniaGeneralDetalleResponsablePorPrioridad WHERE Id = @Id;";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(IdDiasWhatsappDTO datos)
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> dto = new List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO>();
                _query = "EXEC [mkt].[SP_ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno] @Id, @Dias ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = datos.Id, Dias = datos.Dias });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EliminarCampaniaGeneralDetalleResponsableWhatsApp (EliminarCampaniaGeneralDetalleResponsableWhatsAppDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_EliminarCampaniaGeneralDetalleResponsableWhatsApp] @IdCampaniaGeneralDetalleResponsableWhatsApp,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleResponsableWhatsApp = json.IdCampaniaGeneralDetalleResponsableWhatsApp, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ValorDevueltoDTO InsertarCampaniaGeneralDetalleResponsableWhatsApp(InsertarCampaniaGeneralDetalleResponsableWhatsAppDTO json)
        {
            try
            {
                ValorDevueltoDTO dto = new ValorDevueltoDTO();
                var _query = string.Empty;
                _query = "EXEC [mkt].[SP_InsertarCampaniaGeneralDetalleResponsableWhatsApp] @IdCampaniaGeneralDetalleWhatsApp ,@IdPersonal,@IdAreaCapacitacion,@IdPlantilla,@IdCentroCosto,@Cantidad, @Usuario";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, IdPersonal = json.IdPersonal, IdAreaCapacitacion = json.IdAreaCapacitacion, IdPlantilla = json.IdPlantilla, IdCentroCosto = json.IdCentroCosto, Cantidad = json.Cantidad, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<ValorDevueltoDTO>(respuesta);
                }
                return dto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> CombosCampaniaGeneralDetall(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> dto = new List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO>();
                _query = "SELECT Id,CantidadBase,CantidadDisponible,Asesor,Plantilla,CentroCosto,Cantidad,Enviados,AlumnoConfigurado FROM mkt.V_ObtenerCampaniaGeneralDetalleResponsablePorPrioridad WHERE Id = @Id;";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_ProcesarDataPorPrioridadSendinblue]	@IdCampaniaGeneralDetalleWhatsApp ,@IdCampaniaGeneralDetalle ,@Usuario, @Dias ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, IdCampaniaGeneralDetalle = json.IdCampaniaGeneralDetalle, Usuario = json.Usuario, Dias = json.Dias });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ComboCampaniaGeneralDetalleResponsableWhatsAppDTO ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp()
        {

            try
            {
                var _query = string.Empty;
                ComboCampaniaGeneralDetalleResponsableWhatsAppDTO dto = new ComboCampaniaGeneralDetalleResponsableWhatsAppDTO();
                dto.IdPGeneral = new List<ComboGeneralDTO>();
                _query = "SELECT Id, Nombre FROM mkt.V_RegistroResponsableWhatsapp";
                var respuestaIdPersonall = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaIdPersonall) && !respuestaIdPersonall.Contains("[]"))
                {
                    dto.IdPersonal = JsonConvert.DeserializeObject<List<ComboGeneralDTO>>(respuestaIdPersonall);
                }

                _query = "SELECT Id,Nombre FROM mkt.T_Plantilla WHERE Estado = 1 ORDER BY Id DESC;";
                var respuestaIdPlantilla = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaIdPlantilla) && !respuestaIdPlantilla.Contains("[]"))
                {
                    dto.IdPlantilla = JsonConvert.DeserializeObject<List<ComboGeneralDTO>>(respuestaIdPlantilla);
                }
                _query = "SELECT Id AS IdCentroCosto,nombre FROM mkt.V_ComboCentroCosto; ";
                var respuestaIdCentroCosto = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaIdCentroCosto) && !respuestaIdCentroCosto.Contains("[]"))
                {
                    dto.IdCentroCosto = JsonConvert.DeserializeObject<List<ComboGeneralCentroCostoAreaCapacitacionDTO>>(respuestaIdCentroCosto);
                }
                return dto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ObtenerComboRespuestaWhatsAppp ObtenerComboRespuestaWhatsAppp()
        {

            try
            {
                var _query = string.Empty;
                ObtenerComboRespuestaWhatsAppp dto = new ObtenerComboRespuestaWhatsAppp();
                dto.IdPaisDTO = new List<ComboPaisDTO>();

                _query = @"	SELECT  IdPais , P.NombrePais FROM conf.T_WhatsAppConfiguracionApi AS C
                                INNER JOIN conf.T_Pais AS P
                                ON P.Id = C.IdPais
                             WHERE P.Estado = 1
                            AND C.Estado = 1";
                var respuestaIdPais = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaIdPais) && !respuestaIdPais.Contains("[]"))
                {
                    dto.IdPaisDTO = JsonConvert.DeserializeObject<List<ComboPaisDTO>>(respuestaIdPais);
                }
                _query = "SELECT Id,Nombre FROM mkt.T_Plantilla WHERE Estado = 1 ORDER BY Id DESC;";
                var respuestaIdPlantilla = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaIdPlantilla) && !respuestaIdPlantilla.Contains("[]"))
                {
                    dto.IdPlantilla = JsonConvert.DeserializeObject<List<ComboGeneralDTO>>(respuestaIdPlantilla);
                }
                _query = "SELECT Id AS IdCentroCosto,nombre FROM mkt.V_ComboCentroCosto; ";
                var respuestaIdCentroCosto = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaIdCentroCosto) && !respuestaIdCentroCosto.Contains("[]"))
                {
                    dto.IdCentroCosto = JsonConvert.DeserializeObject<List<ComboGeneralCentroCostoAreaCapacitacionDTO>>(respuestaIdCentroCosto);
                }
                return dto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue()
        {

            try
            {
                var _query = string.Empty;
                ObtenerComboCampaniasSendinBlueDTO dto = new ObtenerComboCampaniasSendinBlueDTO();
                _query = "SELECT CG.Id AS IdCampaniaGeneral, CG.Nombre FROM mkt.T_CampaniaGeneral AS CG WITH (NOLOCK);";
                var respuestaCampaniaGeneral = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaCampaniaGeneral) && !respuestaCampaniaGeneral.Contains("[]"))
                {
                    dto.IdCampaniaGeneral = JsonConvert.DeserializeObject<List<ComboCampaniaGeneralDTO>>(respuestaCampaniaGeneral);
                }
                _query = "SELECT CGD.IdCampaniaGeneral,CGD.Id AS IdCampaniaGeneralDetalle, CGD.Nombre FROM mkt.T_CampaniaGeneralDetalle AS CGD WITH (NOLOCK) WHERE CGD.Estado = 1; ";
                var respuestaCampaniaGeneralDetalle = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(respuestaCampaniaGeneralDetalle) && !respuestaCampaniaGeneralDetalle.Contains("[]"))
                {
                    dto.IdCampaniaGeneralDetalle = JsonConvert.DeserializeObject<List<IdCampaniaGeneralDetalleDTO>>(respuestaCampaniaGeneralDetalle);
                }
                return dto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<AlumnoWhatsAppMasivoBaseDTO> ObtenerAlumnoConfiguradoPorPrioridad(PrioridadDatosDTO obj)
        {
            try
            {
                List<AlumnoWhatsAppMasivoBaseDTO> dto = new List<AlumnoWhatsAppMasivoBaseDTO>();

                var _query = "exec [mkt].[SP_ObtenerAlumnoConfiguradoPorPrioridadAlterno]	@IdCampaniaGeneralDetalleWhatsApp,@Cantidad ,@Dias ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleWhatsApp = obj.IdCampaniaGeneralDetalleWhatsApp, Cantidad = obj.Cantidad, Dias = obj.Dias });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<AlumnoWhatsAppMasivoBaseDTO>>(respuesta);
                }
                return dto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<AlumnoInformacionBasicaDTO> ObtenerDatosAlumno(List<int> ListaAlumnos)
        {

            try
            {
                string IdsConcatenados = string.Join(",", ListaAlumnos);
                List<AlumnoInformacionBasicaDTO> dto = new List<AlumnoInformacionBasicaDTO>();
                var _query = "exec [mkt].[SP_ObtenerDatosAlumnoMasivo]@ListaId ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { ListaId = IdsConcatenados });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<List<AlumnoInformacionBasicaDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoDTO json)
        {
            try
            {
                var _query = "exec mkt.SP_ProcesarDataPorPrioridadExcel	@IdCampaniaGeneralDetalleWhatsApp ,@ListaAlumnos ,@Usuario, @Dias ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, ListaAlumnos = json.ListaDeAlumnos, Usuario = json.Usuario, Dias = json.Dias });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue()
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> dto = new List<ObtenerComboCentroCostoCampaniasSendinBlueDTO>();
                _query = "SELECT Id,nombre FROM mkt.V_ComboCentroCosto;";
                var respuesta = _dapperRepository.QueryDapper(_query, new {});

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<List<ObtenerComboCentroCostoCampaniasSendinBlueDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                List<ReporteInteraccionCampaniaGeneralDetalleDTO> dto = new List<ReporteInteraccionCampaniaGeneralDetalleDTO>();
                _query = "SELECT Id,Nombre,Prioridad,NombreCampaniaOrigen,NombreDetalle,CentroCosto,personal,Plantilla,Programados,Enviados,Entregados,Leidos,ChatsValidos,ChatsInvalidos,OportunidadesCreadas FROM mkt.V_ReporteInteraccionCampaniaGeneralDetalle WHERE Id = @Id;";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<ReporteInteraccionCampaniaGeneralDetalleDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ObtenerDatosPorPrioridadAsignadaDTO ObtenerDatosPorPrioridadAsignada(int IdCampaniaGeneralDetalleResponsableWhatsApp)
        {

            try
            {
                var _query = string.Empty;
                ObtenerDatosPorPrioridadAsignadaDTO dto = new ObtenerDatosPorPrioridadAsignadaDTO();
                _query = "SELECT TOP 1 CGDR.Id, CGDR.IdPersonal, CGDR.IdAreaCapacitacion, CGDR.IdPlantilla, CGDR.IdCentroCosto, PE.IdProgramaGeneral FROM mkt.T_CampaniaGeneralDetalleResponsableWhatsApp AS CGDR INNER JOIN pla.T_PEspecifico AS PE ON PE.IdCentroCosto = CGDR.IdCentroCosto WHERE CGDR.Id = @IdCampaniaGeneralDetalleResponsableWhatsApp;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdCampaniaGeneralDetalleResponsableWhatsApp = IdCampaniaGeneralDetalleResponsableWhatsApp });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<ObtenerDatosPorPrioridadAsignadaDTO>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsApp(InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsApp] @Json,@IdCampaniaGeneralDetalleResponsableWhatsApp,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Json = json.Json, IdCampaniaGeneralDetalleResponsableWhatsApp = json.IdCampaniaGeneralDetalleResponsableWhatsApp,Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool SumaChatValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_SumaChatValidoWhatsApp] @IdAlumno, @CelularWhatsApp, @IdCampaniaGeneralDetalleWhatsApp,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularWhatsApp = json.CelularWhatsApp, IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool RestaChatValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_RestaChatValidoWhatsApp] @IdAlumno, @CelularWhatsApp, @IdCampaniaGeneralDetalleWhatsApp,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularWhatsApp = json.CelularWhatsApp, IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool SumaChatInValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_SumaChatInValidoWhatsApp] @IdAlumno, @CelularWhatsApp, @IdCampaniaGeneralDetalleWhatsApp,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularWhatsApp = json.CelularWhatsApp, IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool RestaChatInValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_RestaChatInValidoWhatsApp] @IdAlumno, @CelularWhatsApp, @IdCampaniaGeneralDetalleWhatsApp,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularWhatsApp = json.CelularWhatsApp, IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool SumaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_SumaOportunidadWhatsApp] @IdAlumno, @CelularWhatsApp, @IdCampaniaGeneralDetalleWhatsApp,  @IdCentroCosto,  @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularWhatsApp = json.CelularWhatsApp, IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, IdCentroCosto = json.IdCentroCosto,  Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool RestaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_RestaOportunidadWhatsApp] @IdAlumno, @CelularWhatsApp, @IdCampaniaGeneralDetalleWhatsApp,  @IdCentroCosto,  @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularWhatsApp = json.CelularWhatsApp, IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp, IdCentroCosto = json.IdCentroCosto, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ObtenerProgramaGeneral(int IdCentroCosto)
        {

            try
            {
                var _query = string.Empty;
                IdProgramaGeneralPlantillaDTO dto = new IdProgramaGeneralPlantillaDTO();
                _query = "SELECT TOP 1 P.IdProgramaGeneral FROM pla.T_PEspecifico AS P WHERE P.IdCentroCosto = @IdCentroCosto AND P.Estado = 1 ";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdCentroCosto = IdCentroCosto });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<IdProgramaGeneralPlantillaDTO>(respuesta);
                    return dto.IdProgramaGeneral;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerPlantillaWhatsApp(int IdPlantilla)
        {

            try
            {
                var _query = string.Empty;
                ObtenerPlantillaWhatsAppDTO dto = new ObtenerPlantillaWhatsAppDTO();
                _query = "SELECT TOP 1 Descripcion FROM mkt.T_Plantilla WHERE Id = @Id AND Estado = 1";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Id = IdPlantilla });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<ObtenerPlantillaWhatsAppDTO>(respuesta);
                    return dto.Descripcion;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ProgramaprobabilidadDTO> ObtenerProgramaProbabilidadAlumno(int idAlumno, int top)
        {

            try
            {

                List<ProgramaprobabilidadDTO> dto = new List<ProgramaprobabilidadDTO>();
                var _query = "exec mkt.SP_ObtenerProgramasProbabilidadPorAlumno @Idalumno, @Top";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = idAlumno, Top = top });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<List<ProgramaprobabilidadDTO>>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public NombreDTO OntenerUltimoProgramaInteresado(int idAlumno)
        {

            try
            {

                NombreDTO dto = new NombreDTO();
                var _query = "exec mkt.SP_ObtenerUltimoPGeneralPorAlumno @IdAlumno";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdAlumno = idAlumno });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<NombreDTO>(respuesta);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DiasWhatsappDTO ObtenerDiasPorPrioridadWhatsapp(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                DiasWhatsappDTO dto = new DiasWhatsappDTO();
                _query = "SELECT TOP 1 * FROM mkt.V_ObtenerDiasPorPrioridadWhatsapp WHERE Id = @Id;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<DiasWhatsappDTO>(respuesta);
                    return dto;
                }
                else
                {
                    return dto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Humberto Oscata
        /// Fecha: 29/08/2025
        /// Version: 1.0
        /// <summary>
        /// Devuelve el ultimo mensaje de campania que se envio a un alumno
        /// </summary>
        /// <param name="celularAlumno">Celular del alumno</param>
        /// <returns>Ultimo mensaje de campania enviado</returns>
        public string ObtenerUltimoMensajeCampaniaEnviado(string celularAlumno)
        {
            try
            {
                UltimoMensajeDTO ultimoMensaje = new UltimoMensajeDTO();
                var _query = @"SELECT TOP 1
		                                CGDRAW.MensajePlantillaHtml AS UltimoMensaje
	                                FROM
		                                mkt.T_CampaniaGeneralDetalleResponsableAlumnoWhatsApp AS CGDRAW
	                                WHERE
		                                CGDRAW.CelularWhatsApp = @Celular AND CGDRAW.MensajePlantillaHtml IS NOT NULL
	                                ORDER BY
		                                CGDRAW.FechaCreacion DESC";

                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Celular = celularAlumno });
                return respuesta;

                //if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                //{
                //    ultimoMensaje = JsonConvert.DeserializeObject<UltimoMensajeDTO>(respuesta);
                //    return ultimoMensaje.UltimoMensaje;
                //}
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}









