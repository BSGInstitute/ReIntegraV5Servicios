using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;
using System.Data;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoCampaniaGeneralSmsDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CampaniaGeneralSmsRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneralSms
    /// </summary>
    public class CampaniaGeneralSmsRepository : ICampaniaGeneralSmsRepository
    {
        private IDapperRepository _dapperRepository;
        public CampaniaGeneralSmsRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<ObtenerCampaniaGeneralDetalleSmsGrupoDTO> ObtenerCampaniaGeneralDetalleSms(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerCampaniaGeneralDetalleSmsGrupoDTO> dto = new List<ObtenerCampaniaGeneralDetalleSmsGrupoDTO>();
                _query = "SELECT Id,NombreCampaniaGeneralSms,FechaInicioEnvioSms,HoraEnvio,COALESCE(IdCampaniaGeneralDetalleSms,0)AS IdCampaniaGeneralDetalleSms,COALESCE(NombreCampaniaOrigen,'') AS NombreCampaniaOrigen,COALESCE(Prioridad,0) AS Prioridad,COALESCE(Nombre,'') AS Nombre,COALESCE(ActivarMasivo,0) AS ActivarMasivo,COALESCE(Programados,0) AS Programados,COALESCE(CantidadBase, 0) AS CantidadBase,COALESCE(Enviados,0)AS Enviados  FROM mkt.V_ObtenerCampaniaGeneralDetalleSms WHERE Id =  @Id ORDER BY IdCampaniaGeneralDetalleSms ASC	;";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralDetalleSmsGrupoDTO>>(respuesta);
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

        public bool InsertarCampaniaGeneralSms(StringDTO nombreCampania, string usuario)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralSms] @Nombre, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = nombreCampania.Valor, Usuario = usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public CampaniaGeneralSmsDTO ObtenerCampaniaGeneralSms(IdDTO id)
        {
            try
            {
                var _query = "SELECT Nombre, FechaInicioEnvioSms, HoraEnvio FROM mkt.T_CampaniaGeneralSms WHERE Id = @Id";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    var dto = JsonConvert.DeserializeObject<CampaniaGeneralSmsDTO>(respuesta);
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


        public bool ActualizarCampaniaGeneralSms(ActualizarCampaniaGeneralSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_ActualizarCampaniaGeneralSms] @Nombre, @HoraEnvio, @FechaInicioSms,  @Id, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, HoraEnvio = json.HoraEnvio, FechaInicioSms = json.FechaInicioEnvioSms, Id = json.Id, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ObtenerCampaniaGeneralGrillaSmsDTO> ObtenerCampaniaGeneralGrillaSms()
        {
            try
            {
                List<ObtenerCampaniaGeneralGrillaSmsDTO> ObtenerCampaniaGeneralGrillaSms = new List<ObtenerCampaniaGeneralGrillaSmsDTO>();
                var _query = string.Empty;
                _query = "SELECT CGW.Id,CGW.Nombre,	CGW.FechaInicioEnvioSms,CGW.HoraEnvio,CGW.Cantidad FROM mkt.V_ObtenerCampaniaGeneralGrillaSms AS CGW WITH(NOLOCK) ORDER BY Id DESC";
                var respuesta = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                {
                    return ObtenerCampaniaGeneralGrillaSms = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralGrillaSmsDTO>>(respuesta);
                }
                else return ObtenerCampaniaGeneralGrillaSms;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EliminarCampaniaGeneralSms(EliminarCampaniaGeneralSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_EliminarCampaniaGeneralSms] @IdCampaniaGeneralSms, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralSms = json.Id, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_ActualizarActivarMasivoPorCampaniaSms]  @ActivarMasivo, @Id,  @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = json.IdCampaniaGeneralDetalleSms, ActivarMasivo = json.ActivarMasivo, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EliminarCampaniaGeneralDetalleSms(EliminarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_EliminarCampaniaGeneralDetalleSms] @IdCampaniaGeneralDetalleSms, @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarCampaniaGeneralDetalleSms(InsertarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralDetalleSms] @Nombre,@IdCampaniaGeneralSms,@Prioridad,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, IdCampaniaGeneralSms = json.IdCampaniaGeneralSms, Prioridad = json.Prioridad, Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool InsertarCampaniaGeneralDetalleExcelSms(InsertarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralDetalleExcelSms] @Nombre,@IdCampaniaGeneralSms,@Prioridad,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, IdCampaniaGeneralSms = json.IdCampaniaGeneralSms, Prioridad = json.Prioridad, Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ActualizarCamposCampaniaGeneralDetalleSms(ActualizarCamposCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_ActualizarCamposCampaniaGeneralDetalleSms] @Nombre , @IdCampaniaGeneralDetalleSms,@Prioridad,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Nombre = json.Nombre, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, Prioridad = json.Prioridad, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO ObtenerConfiguracionCampaniaGeneralDetalleSms(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO dto = new ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO();
                _query = "SELECT Id, Nombre, Prioridad, IdCampaniaGeneral,IdCampaniaGeneralDetalle FROM mkt.V_ObtenerConfiguracionCampaniaGeneralDetalleSms WHERE Id =@Id;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO>(respuesta);
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

        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id)
        {

            try
            {
                var _query = string.Empty;
                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO> dto = new List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO>();
                _query = "SELECT Id,CantidadBase,CantidadDisponible,IdCampaniaGeneralDetalleResponsableSms,Asesor,Plantilla,CentroCosto,Cantidad,Enviados,AlumnoConfigurado FROM mkt.V_ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSms WHERE Id = @Id;";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = id.Id });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO>>(respuesta);
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

        public bool EliminarCampaniaGeneralDetalleResponsableSms(EliminarCampaniaGeneralDetalleResponsableSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_EliminarCampaniaGeneralDetalleResponsableSms] @IdCampaniaGeneralDetalleResponsableSms,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleResponsableSms = json.IdCampaniaGeneralDetalleResponsableSms, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ValorDevueltoDTO InsertarCampaniaGeneralDetalleResponsableSms(InsertarCampaniaGeneralDetalleResponsableSmsDTO json)
        {
            try
            {
                ValorDevueltoDTO dto = new ValorDevueltoDTO();
                var _query = string.Empty;
                _query = "EXEC [mkt].[SP_InsertarCampaniaGeneralDetalleResponsableSms] @IdCampaniaGeneralDetalleSms ,@IdPersonal,@IdAreaCapacitacion,@IdPlantilla,@IdCentroCosto,@Cantidad, @Usuario";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, IdPersonal = json.IdPersonal, IdAreaCapacitacion = json.IdAreaCapacitacion, IdPlantilla = json.IdPlantilla, IdCentroCosto = json.IdCentroCosto, Cantidad = json.Cantidad, Usuario = json.Usuario });
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
        public bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueSmsDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_ProcesarDataPorPrioridadSendinblueSms]	@IdCampaniaGeneralDetalleSms ,@IdCampaniaGeneralDetalle ,@Usuario ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, IdCampaniaGeneralDetalle = json.IdCampaniaGeneralDetalle, Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ComboCampaniaGeneralDetalleResponsableSmsDTO ObtenerComboCampaniaGeneralDetalleResponsableSms()
        {

            try
            {
                var _query = string.Empty;
                ComboCampaniaGeneralDetalleResponsableSmsDTO dto = new ComboCampaniaGeneralDetalleResponsableSmsDTO();
                dto.IdPGeneral = new List<ComboGeneralDTO>();
                _query = "SELECT Id, CONCAT(Nombres,' ', Apellidos) AS Nombre FROM gp.T_Personal WHERE Estado = 1 AND Activo = 1 AND IdPersonalAreaTrabajo = 4;";
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


        public ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue()
        {

            try
            {
                var _query = string.Empty;
                ObtenerComboCampaniasSendinBlueDTO dto = new ObtenerComboCampaniasSendinBlueDTO();
                _query = "SELECT CG.Id AS IdCampaniaGeneral, CG.Nombre FROM mkt.T_CampaniaGeneral CG WITH (NOLOCK) WHERE CG.Nombre LIKE '%Peru%'\r\n;";
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



        public List<AlumnoSmsMasivoBaseDTO> ObtenerAlumnoConfiguradoPorPrioridad(PrioridadDatosSmsDTO obj)
        {
            try
            {
                List<AlumnoSmsMasivoBaseDTO> dto = new List<AlumnoSmsMasivoBaseDTO>();

                var _query = "exec [mkt].[SP_ObtenerAlumnoConfiguradoPorPrioridadSms]	@IdCampaniaGeneralDetalleSms,@Cantidad  ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleSms = obj.IdCampaniaGeneralDetalleSms, Cantidad = obj.Cantidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    dto = JsonConvert.DeserializeObject<List<AlumnoSmsMasivoBaseDTO>>(respuesta);
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
        public bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoSmsDTO json)
        {
            try
            {
                var _query = "exec mkt.SP_ProcesarDataPorPrioridadExcelSms	@IdCampaniaGeneralDetalleSms ,@ListaAlumnos ,@Usuario ";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, ListaAlumnos = json.ListaDeAlumnos, Usuario = json.Usuario });
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
                var respuesta = _dapperRepository.QueryDapper(_query, new { });

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

        public ObtenerDatosPorPrioridadAsignadaDTO ObtenerDatosPorPrioridadAsignada(int IdCampaniaGeneralDetalleResponsableSms)
        {

            try
            {
                var _query = string.Empty;
                ObtenerDatosPorPrioridadAsignadaDTO dto = new ObtenerDatosPorPrioridadAsignadaDTO();
                _query = "SELECT TOP 1 CGDR.Id, CGDR.IdPersonal, CGDR.IdAreaCapacitacion, CGDR.IdPlantilla, CGDR.IdCentroCosto, PE.IdProgramaGeneral FROM mkt.T_CampaniaGeneralDetalleResponsableSms AS CGDR INNER JOIN pla.T_PEspecifico AS PE ON PE.IdCentroCosto = CGDR.IdCentroCosto WHERE CGDR.Id = @IdCampaniaGeneralDetalleResponsableSms;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdCampaniaGeneralDetalleResponsableSms = IdCampaniaGeneralDetalleResponsableSms });

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

        public bool InsertarCampaniaGeneralDetalleResponsableAlumnoSms(InsertarCampaniaGeneralDetalleResponsableAlumnoSmsDTO json)
        {
            try
            {
                var _query = "exec [mkt].[SP_InsertarCampaniaGeneralDetalleResponsableAlumnoSms] @Json,@IdCampaniaGeneralDetalleResponsableSms,@Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Json = json.Json, IdCampaniaGeneralDetalleResponsableSms = json.IdCampaniaGeneralDetalleResponsableSms, Usuario = json.Usuario });
                return !string.IsNullOrEmpty(respuesta) && respuesta != "null";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool SumaChatValidoSms(SumaValidadorChatSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_SumaChatValidoSms] @IdAlumno, @CelularSms, @IdCampaniaGeneralDetalleSms,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularSms = json.CelularSms, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool RestaChatValidoSms(SumaValidadorChatSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_RestaChatValidoSms] @IdAlumno, @CelularSms, @IdCampaniaGeneralDetalleSms,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularSms = json.CelularSms, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool SumaChatInValidoSms(SumaValidadorChatSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_SumaChatInValidoSms] @IdAlumno, @CelularSms, @IdCampaniaGeneralDetalleSms,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularSms = json.CelularSms, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool RestaChatInValidoSms(SumaValidadorChatSmsDTO json)
        {
            try
            {
                var _query = string.Empty;
                _query = "exec [mkt].[SP_RestaChatInValidoSms] @IdAlumno, @CelularSms, @IdCampaniaGeneralDetalleSms,   @Usuario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularSms = json.CelularSms, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, Usuario = json.Usuario });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
                else return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //public bool SumaOportunidadSms(SumaOportunidadWhatsAootDTO json)
        //{
        //    try
        //    {
        //        var _query = string.Empty;
        //        _query = "exec [mkt].[SP_SumaOportunidadSms] @IdAlumno, @CelularSms, @IdCampaniaGeneralDetalleSms,  @IdCentroCosto,  @Usuario";
        //        var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularSms = json.CelularSms, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, IdCentroCosto = json.IdCentroCosto,  Usuario = json.Usuario });
        //        if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
        //        else return false;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //public bool RestaOportunidadSms(SumaOportunidadWhatsAootDTO json)
        //{
        //    try
        //    {
        //        var _query = string.Empty;
        //        _query = "exec [mkt].[SP_RestaOportunidadSms] @IdAlumno, @CelularSms, @IdCampaniaGeneralDetalleSms,  @IdCentroCosto,  @Usuario";
        //        var respuesta = _dapperRepository.QueryDapper(_query, new { IdAlumno = json.IdAlumno, CelularSms = json.CelularSms, IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms, IdCentroCosto = json.IdCentroCosto, Usuario = json.Usuario });
        //        if (!string.IsNullOrEmpty(respuesta) && respuesta != "null") return true;
        //        else return false;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
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


        public List<PlantillaSmsDato> ObtenerPlantillaSms()
        {

            try
            {
                var _query = string.Empty;
                List<PlantillaSmsDato> dto = new List<PlantillaSmsDato>();
                _query = "SELECT id, nombre FROM mkt.T_PlantillaSms where estado = 1 order by id desc";
                var respuesta = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<List<PlantillaSmsDato>>(respuesta);
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

        public List<ObtenerPrioridadesEnvioSmsDTO> ObtenerPrioridadesEnvioSms()
        {
            try
            {
                #region Captura del tiempo
                var horaActual = DateTime.Now;

                //string FechaInicioEnvioSms = "2023-08-09";
                string FechaInicioEnvioSms = horaActual.ToString("dd/MM/yyyy");
                string minutoActual = string.Empty;

                minutoActual = horaActual.Minute.ToString().Length == 1 ? minutoActual = "0" + horaActual.Minute : minutoActual = horaActual.Minute.ToString();
                //string horaEnvio = "12:32:02";
                string horaEnvio = horaActual.Hour + ":" + minutoActual + ":00";
                #endregion

                var listaCampaniaGeneralSms = new List<ObtenerPrioridadesEnvioSmsDTO>();
                string query = "mkt.SP_ObtenerPrioridadesEnvioSms";
                var resultadoListaSms = _dapperRepository.QuerySPDapper(query, new { horaEnvio, FechaInicioEnvioSms });
                if (!string.IsNullOrEmpty(resultadoListaSms) && !resultadoListaSms.Contains("[]"))
                    listaCampaniaGeneralSms = JsonConvert.DeserializeObject<List<ObtenerPrioridadesEnvioSmsDTO>>(resultadoListaSms);

                return listaCampaniaGeneralSms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<EmailAsesoresDto> ObtenerListaAsesores()
        {

            try
            {
                var _query = string.Empty;
                List<EmailAsesoresDto> dto = new List<EmailAsesoresDto>();
                _query = "SELECT pe.Email FROM mkt.T_AsesorMarketing am INNER JOIN gp.T_Personal pe ON am.IdPersonal = pe.id";
                var respuesta = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {

                    dto = JsonConvert.DeserializeObject<List<EmailAsesoresDto>>(respuesta);
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

        public List<CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO> ObtenerLogActivoCampaniaGeneralDetalleResponsableSms(int IdCampaniaGeneralDetalleResponsableSms)
        {
            List<CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO> SmsConfiguracionLogEjecucion = new List<CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO>();
            try
            {
                var Query = "SELECT Id, IdCampaniaGeneralDetalleResponsableSms,FechaEnvio,HoraEnvio,Estado FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoLogSms WHERE IdCampaniaGeneralDetalleResponsableSms = @IdCampaniaGeneralDetalleResponsableSms AND Estado = 1";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdCampaniaGeneralDetalleResponsableSms });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Equals("[]"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    SmsConfiguracionLogEjecucion = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO>>(QueryRespuesta);
                    return SmsConfiguracionLogEjecucion;
                }
                return SmsConfiguracionLogEjecucion;
            }
            catch
            {
                return SmsConfiguracionLogEjecucion;
            }
        }

        public bool EliminarLogSms(int Id, string Usuario)
        {
            try
            {
                string spQuery = "[mkt].[SP_EliminarLogSms]";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = Id,
                    Usuario = Usuario
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int InsertarLogSms(int IdCampaniaGeneralDetalleResponsableSms, string HoraEnvio, string FechaInicioEnvioSms, string Usuario)
        {
            try
            {

                string spQuery = "[mkt].[SP_InsertarLogSms]";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    IdCampaniaGeneralDetalleResponsableSms,
                    HoraEnvio,
                    FechaInicioEnvioSms,
                    Usuario
                });
                if (!string.IsNullOrEmpty(query) & !query.Equals("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    IdLogInsertadoDTO IdCampaniaGeneralDetalleResponsableLogSms = JsonConvert.DeserializeObject<IdLogInsertadoDTO>(query);
                    return IdCampaniaGeneralDetalleResponsableLogSms.Valor;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public List<PreCampaniaGeneralDetalleResponsableAlumnoSmsDTO> PreListaSmsEnvioMasivo(int IdCampaniaGeneralDetalleResponsableSms)
        {
            try
            {
                List<PreCampaniaGeneralDetalleResponsableAlumnoSmsDTO> ConfiguracionPre = new List<PreCampaniaGeneralDetalleResponsableAlumnoSmsDTO>();
                string Query = string.Empty;
                Query = @"	SELECT 
		                    CGDRA.Id ,
		                    CGDRA.IdCampaniaGeneralDetalleResponsableSms,
		                    CGDRA.CelularSms,
		                    CGDRA.IdAlumno,
		                    CGDRA.IdPais,
		                    CGDRA.MensajePlantillaHtml,
		                    CGDRA.ObjetoPlantilla,
		                    CGDR.IdPersonal,
		                    P.Descripcion
	                    FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoSms AS CGDRA  WITH (NOLOCK)
	                    INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableSms AS CGDR WITH (NOLOCK)
	                    ON CGDRA.IdCampaniaGeneralDetalleResponsableSms = CGDR.Id 
	                    INNER JOIN mkt.T_Plantilla AS P WITH (NOLOCK)
	                    ON P.Id = CGDR.IdPlantilla
	                    WHERE CGDR.Estado = 1
		                    AND CGDRA.Estado = 1
		                    AND P.Estado = 1
		                    AND CGDRA.IdCampaniaGeneralDetalleResponsableSms = @IdCampaniaGeneralDetalleResponsableSms
		                    AND CGDRA.IdAlumno NOT IN (
			                    SELECT 
				                    DISTINCT
				                    CGDRAE2.IdAlumno
			                    FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoEnviadoSms AS CGDRAE2 WITH (NOLOCK)
			                    INNER JOIN mkt.T_SmsEstadoMensajeEnviadoApiGraph AS API
			                    ON API.WaId = CGDRAE2.WaId
			                    INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableAlumnoSms AS CGDRA2  WITH (NOLOCK)
			                    ON CGDRAE2.IdCampaniaGeneralDetalleResponsableAlumnoSms = CGDRA2.Id
			                    INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableSms AS CGDR2 WITH (NOLOCK)
			                    ON CGDRA2.IdCampaniaGeneralDetalleResponsableSms = CGDR2.Id 
			                    WHERE CGDR2.Estado = 1
				                    AND CGDRA2.Estado = 1
				                    AND CGDRAE2.Estado = 1
				                    AND CGDRAE2.WaId IS NOT NULL
				                    AND API.WaStatus = 'Message acepted for delivery'
				                    AND CGDRA2.IdCampaniaGeneralDetalleResponsableSms = @IdCampaniaGeneralDetalleResponsableSms
	                        ) ";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdCampaniaGeneralDetalleResponsableSms });
                if (!string.IsNullOrEmpty(QueryRespuesta))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<PreCampaniaGeneralDetalleResponsableAlumnoSmsDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DetalleCampaniaDTO ObtenerDetalleDeCampaniaSms(int IdcampaniaGeneralDetalleResponsableSms)
        {
            DetalleCampaniaDTO ConfiguracionPre = new DetalleCampaniaDTO();
            try
            {

                string querySP = "[mkt].[SP_ObtenerDetalleDeCampaniaSms]";

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { IdcampaniaGeneralDetalleResponsableSms });

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

        public List<IdLogDTO> logsActivos(int IdCampaniaGeneralDetalleResponsableSms)
        {
            try
            {
                List<IdLogDTO> ConfiguracionPre = new List<IdLogDTO>();
                string Query = string.Empty;
                Query = @"SELECT
                            LOGW.Id
                        FROM
                            mkt.T_CampaniaGeneralDetalleResponsableAlumnoLogSms AS LOGW
	                        INNER JOIN mkt.T_CampaniaGeneralDetalleResponsableSms AS CGDR
	                        ON CGDR.Id = LOGW.IdCampaniaGeneralDetalleResponsableSms
	                        INNER JOIN mkt.T_CampaniaGeneralDetalleSms AS CGDW
	                        ON CGDW.Id = CGDR.IdCampaniaGeneralDetalleSms
                        WHERE
                            LOGW.IdCampaniaGeneralDetalleResponsableSms = @IdCampaniaGeneralDetalleResponsableSms
                            AND LOGW.Estado = 1
	                        AND CGDR.Estado = 1
	                        AND CGDW.Estado = 1
	                        AND CGDW.ActivarMasivo = 1
	                         ";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdCampaniaGeneralDetalleResponsableSms });
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


        public List<SmsEnviarMensajeDTO> ObtenerDataEnvio(int IdcampaniaGeneralDetalleResponsableSms)
        {
            List<SmsEnviarMensajeDTO> ConfiguracionPre = new List<SmsEnviarMensajeDTO>();
            try
            {
                string query = "SELECT * FROM mkt.V_ObtenerDataEnvioSms WHERE IdCampaniaGeneralDetalleResponsableSms = @IdcampaniaGeneralDetalleResponsableSms";

                var respuestaConsulta = _dapperRepository.QueryDapper(query, new { IdcampaniaGeneralDetalleResponsableSms });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<SmsEnviarMensajeDTO>>(respuestaConsulta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                // Manejar la excepción si es necesario
                return ConfiguracionPre;
            }
        }


        public bool ValidarEnvioDuplicado(string CelularWhatsApp)
        {
            try
            {
                List<EstadoDTO> ConfiguracionPre = new List<EstadoDTO>();
                string Query = string.Empty;
                Query = @"SELECT TOP 1 Estado 
                          FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoEnviadoSms
                          WHERE CelularSms = @CelularWhatsApp
                          ORDER BY Estado DESC;";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { CelularWhatsApp });
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

        public bool InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoSms(string json, string WaId, int IdCampaniaGeneralDetalleResponsableLogSms)
        {

            RespuestaSmsDTO ConfiguracionPre = new RespuestaSmsDTO();
            try
            {

                string querySP = "mkt.SP_InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoSms";

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { IdCampaniaGeneralDetalleResponsableLogSms = IdCampaniaGeneralDetalleResponsableLogSms, json = json, WaId = WaId });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<RespuestaSmsDTO>(respuestaConsulta);
                }
                return ConfiguracionPre.Respuesta;
            }
            catch (Exception ex)
            {
                return ConfiguracionPre.Respuesta;
            }
        }


        public IdDTO InsertarPlantillaSms(PlantillaSmsDTO datos)
        {
            try
            {
                IdDTO respuesta = new IdDTO();

                string querySP = "mkt.Sp_InsertarPlantillaSms";

                // Crear objeto DynamicParameters para manejar parámetros de entrada y salida
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", datos.Nombre);
                parameters.Add("@Usuario", datos.Usuario);
                parameters.Add("@NuevoId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Ejecutar procedimiento almacenado usando Dapper
                _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                // Obtener el valor del parámetro de salida
                respuesta.Id = parameters.Get<int>("@NuevoId");

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool InsertarDetalllePlantillaSms(DetallePlantillaSmsDTO datos)
        {


            try
            {

                string querySP = "mkt.Sp_InsertarDetallePlantillaSms";
                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { datos.IdPlantillaSms, datos.Text, datos.CustomData, datos.IsPremium, datos.IsFlash, datos.IsLongmessage, datos.IsRandomRoute, datos.ShortUrlConfig, datos.Url, datos.DomainShortUrl, datos.Usuario });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    JsonConvert.DeserializeObject<RespuestaSmsDTO>(respuestaConsulta);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<ObtenerPlantillaSmsGrillaDTO> ObtenerPlantilla()
        {
            try
            {

                List<ObtenerPlantillaSmsGrillaDTO> respuesta = new List<ObtenerPlantillaSmsGrillaDTO>();

                string query = "SELECT id, nombre, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion FROM mkt.T_PlantillaSms where estado = 1 order by id desc";

                var respuestaConsulta = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<ObtenerPlantillaSmsGrillaDTO>>(respuestaConsulta);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObtenerDetallePlantillaSmsDTO> ObtenerDetallePlantilla(IdDTO id)
        {
            try
            {

                List<ObtenerDetallePlantillaSmsDTO> respuesta = new List<ObtenerDetallePlantillaSmsDTO>();

                string query = "SELECT * FROM mkt.V_ObtenerDetallePlantillaSms where id = @id";

                var respuestaConsulta = _dapperRepository.QueryDapper(query, new { id.Id });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<ObtenerDetallePlantillaSmsDTO>>(respuestaConsulta);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarPlantillaSms(ActualizarPlantillaSmsDTO datos)
        {
            try
            {

                string querySP = "mkt.SP_ActualizarDetallePlantillaSms";
                var parameters = new
                {
                    datos.IdPlantillaSms,
                    datos.Nombre,
                    datos.Text,
                    datos.IsPremium,
                    datos.IsFlash,
                    datos.IsLongmessage,
                    datos.ShortUrlConfig,
                    datos.Url,
                    datos.Usuario
                };

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);


                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return false;
            }
        }

        public bool EliminarPlantillaSms(IdDTO datos)
        {
            try
            {

                string querySP = "mkt.SP_EliminarPlantillaSms";

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { @IdPlantillaSms = datos.Id });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    JsonConvert.DeserializeObject<RespuestaSmsDTO>(respuestaConsulta);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario)
        {
            try
            {
                StringDTO formulario = new StringDTO();

                var _query = $"exec mkt.SP_GeneradorFormulariosLinkSms @Nombre = '{datos.Nombre}', @IdCentroCosto = {datos.IdCentroCosto}, @UsuarioResponsable = {usuario}";
                var query = _dapperRepository.FirstOrDefault(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    formulario = JsonConvert.DeserializeObject<StringDTO>(query);
                }
                return formulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool InsertarRespuestaSmsEnvio(respuestaMensajeSmsHook datos, string usuario)
        {
            try
            {
                var _query = $"exec mkt.Sp_InsertarEstadoMensajeEnviado @messageId = '{datos.messageId}', @statusMessage = '{datos.statusMessage}', @statusCode = {datos.statusCode}, @Usuario = '{usuario}'";

                _dapperRepository.QueryDapper(_query, null);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarAlumnoEnviado(InsertarResponsableAlumnoEnviadoSms datos, string usuario)
        {
            try
            {
                var _query = $"exec mkt.SP_InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoSms @IdCampaniaGeneralDetalleResponsableAlumnoSms = '{datos.IdCampaniaGeneralDetalleResponsableAlumnoSms}', " +
                             $"@Idalumno = '{datos.IdAlumno}', " +
                             $"@CelularSms = '{datos.CelularSms}', " +
                             $"@MessageId = '{datos.MessageId}', " +
                             $"@JsonEnvio = '{datos.JsonEnvio}', " +
                             $"@Usuario = '{usuario}'";

                _dapperRepository.QueryDapper(_query, null);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool InsertarAlumnoErroneo(InsertarResponsableAlumnoEnviadoSms datos, string usuario)
        {
            try
            {
                var _query = $"exec mkt.SP_InsertarCampaniaGeneralDetalleResponsableAlumnoErroneoSms @IdCampaniaGeneralDetalleResponsableAlumnoSms = '{datos.IdCampaniaGeneralDetalleResponsableAlumnoSms}', " +
                             $"@Idalumno = '{datos.IdAlumno}', " +
                             $"@CelularSms = '{datos.CelularSms}', " +
                             $"@MessageId = '{datos.MessageId}', " +
                             $"@JsonEnvio = '{datos.JsonEnvio}', " +
                             $"@Usuario = '{usuario}'";

                _dapperRepository.QueryDapper(_query, null);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool InsertarPruebaPlantillaSms(PruebaPlantillaSmsDTO datos)
        {


            try
            {

                string querySP = "mkt.SP_InsertarCampaniaGeneralEnvioPruebaSms";

                var respuestaConsulta = _dapperRepository.QuerySPFirstOrDefault(querySP, new { datos.Celular, @Texto = datos.Text, datos.CustomData, datos.IsPremium, datos.IsFlash, datos.IsLongmessage, datos.IsRandomRoute, datos.ShortUrlConfig, datos.MessageId, datos.Url, datos.DomainShortUrl, datos.Usuario });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                {
                    JsonConvert.DeserializeObject<RespuestaSmsDTO>(respuestaConsulta);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<GrillaSms> ObtenerGrillaSms(int tab, int dia)
        {
            try
            {
                List<GrillaSms> formulario = new List<GrillaSms>();

                var _query = $"exec mkt.Sp_GrillaMensajesSms @Tipo = '{tab}', @Dia = {dia}";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    formulario = JsonConvert.DeserializeObject<List<GrillaSms>>(query);
                }
                return formulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ChatSms> ObtenerChatPorAlumno(string celular)
        {
            try
            {
                List<ChatSms> formulario = new List<ChatSms>();

                var _query = $"exec mkt.Sp_ChatSms @Celular = '{celular}'";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    formulario = JsonConvert.DeserializeObject<List<ChatSms>>(query);
                }
                return formulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<DatosAlumno> ObtenerAlumnoPorCelular(string celular)
        {
            try
            {
                List<DatosAlumno> formulario = new List<DatosAlumno>();

                var _query = $"select * from  mkt.V_AlumnoPorCelular where RIGHT(Celular, 9) = RIGHT('{celular}', 9)";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    formulario = JsonConvert.DeserializeObject<List<DatosAlumno>>(query);
                }
                return formulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public CondifuracionEnvioSmsDTO ObtenerConfiguracionEnvio()
        {
            try
            {
                CondifuracionEnvioSmsDTO formulario = new CondifuracionEnvioSmsDTO();

                var _query = $"select usuario, clave as pass from  mkt.T_ConfiguracionSms where Estado = 1";
                var query = _dapperRepository.FirstOrDefault(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    formulario = JsonConvert.DeserializeObject<CondifuracionEnvioSmsDTO>(query);
                }
                return formulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}









