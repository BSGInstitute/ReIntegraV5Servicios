using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Jsoup.Select;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using static QRCoder.PayloadGenerator;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión general de T_Oportunidad
    /// </summary>
    public class OportunidadRepository : GenericRepository<TOportunidad>, IOportunidadRepository
    {
        private Mapper _mapper;

        public OportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidad, Oportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAsignacionOportunidad, AsignacionOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAsignacionOportunidadLog, AsignacionOportunidadLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadLog, OportunidadLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCalidadProcesamientoAlterno, CalidadProcesamientoAlterno>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCalidadProcesamiento, CalidadProcesamiento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadCompetidor, OportunidadCompetidor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TComprobantePagoOportunidad, ComprobantePagoOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadClasificacionOperacione, OportunidadClasificacionOperaciones>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSolucionClienteByActividad, SolucionClienteByActividad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidor>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidad MapeoEntidad(Oportunidad entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidad modelo = _mapper.Map<TOportunidad>(entidad);

                ////mapea los hijos
                if (entidad.AsignacionOportunidads != null && entidad.AsignacionOportunidads.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TAsignacionOportunidad>>(entidad.AsignacionOportunidads);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TAsignacionOportunidads.Add(hijoNivel1);
                    }

                    if (entidad.AsignacionOportunidads.FirstOrDefault().AsignacionOportunidadLogs.Count > 0)
                    {
                        var entidadHijo2 = _mapper.Map<List<TAsignacionOportunidadLog>>(entidad.AsignacionOportunidads.FirstOrDefault().AsignacionOportunidadLogs);

                        foreach (var hijoNivel2 in entidadHijo2)
                        {
                            modelo.TAsignacionOportunidads.FirstOrDefault().TAsignacionOportunidadLogs.Add(hijoNivel2);
                        }
                    }
                }
                if (entidad.ActividadDetalles != null && entidad.ActividadDetalles.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TActividadDetalle>>(entidad.ActividadDetalles);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TActividadDetalles.Add(hijoNivel1);
                    }
                }
                if (entidad.OportunidadLogs != null && entidad.OportunidadLogs.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TOportunidadLog>>(entidad.OportunidadLogs);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TOportunidadLogs.Add(hijoNivel1);
                    }
                }
                if (entidad.CalidadProcesamientos != null && entidad.CalidadProcesamientos.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TCalidadProcesamiento>>(entidad.CalidadProcesamientos);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TCalidadProcesamientos.Add(hijoNivel1);
                    }
                }
                if (entidad.CalidadProcesamientoAlternos != null && entidad.CalidadProcesamientoAlternos.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TCalidadProcesamientoAlterno>>(entidad.CalidadProcesamientoAlternos);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TCalidadProcesamientoAlternos.Add(hijoNivel1);
                    }
                }
                if (entidad.OportunidadCompetidors != null && entidad.OportunidadCompetidors.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TOportunidadCompetidor>>(entidad.OportunidadCompetidors);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TOportunidadCompetidors.Add(hijoNivel1);
                    }
                    if (entidad.OportunidadCompetidors.FirstOrDefault().DetalleOportunidadCompetidors.Count > 0)
                    {
                        var entidadHijo2 = _mapper.Map<List<TDetalleOportunidadCompetidor>>(entidad.OportunidadCompetidors.FirstOrDefault().DetalleOportunidadCompetidors);

                        foreach (var hijoNivel2 in entidadHijo2)
                        {
                            modelo.TOportunidadCompetidors.FirstOrDefault().TDetalleOportunidadCompetidors.Add(hijoNivel2);
                        }
                    }
                }
                if (entidad.ModeloDataMinings != null && entidad.ModeloDataMinings.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TModeloDataMining>>(entidad.ModeloDataMinings);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TModeloDataMinings.Add(hijoNivel1);
                    }
                }
                if (entidad.ComprobantePagoOportunidads != null && entidad.ComprobantePagoOportunidads.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TComprobantePagoOportunidad>>(entidad.ComprobantePagoOportunidads);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TComprobantePagoOportunidads.Add(hijoNivel1);
                    }
                }
                if (entidad.OportunidadClasificacionOperaciones != null && entidad.OportunidadClasificacionOperaciones.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TOportunidadClasificacionOperacione>>(entidad.OportunidadClasificacionOperaciones);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TOportunidadClasificacionOperaciones.Add(hijoNivel1);
                    }
                }
                if (entidad.SolucionClienteByActividads != null && entidad.SolucionClienteByActividads.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TSolucionClienteByActividad>>(entidad.SolucionClienteByActividads);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TSolucionClienteByActividads.Add(hijoNivel1);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidad Add(Oportunidad entidad)
        {
            try
            {
                var Oportunidad = MapeoEntidad(entidad);
                base.Insert(Oportunidad);
                return Oportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidad Update(Oportunidad entidad)
        {
            try
            {
                var Oportunidad = MapeoEntidad(entidad);

                // Leer la oportunidad actual para verificar concurrencia
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id,
                    s => new { s.RowVersion, s.IdPersonalAsignado });

                var rowVersionOriginal = entidadExistente.RowVersion;
                var idPersonalAsignadoOriginal = entidadExistente.IdPersonalAsignado;

                // Asignar el RowVersion original para que Entity Framework pueda detectar cambios concurrentes
                Oportunidad.RowVersion = rowVersionOriginal;

                // Si la oportunidad estaba sin asignar (125) y se está asignando a alguien,
                // validar que aún esté sin asignar para prevenir asignaciones duplicadas
                bool esAsignacionNueva = (idPersonalAsignadoOriginal == 125 || idPersonalAsignadoOriginal == null)
                                       && Oportunidad.IdPersonalAsignado.HasValue
                                       && Oportunidad.IdPersonalAsignado != 125;

                if (esAsignacionNueva)
                {
                    // Volver a verificar justo antes del UPDATE para detectar asignaciones concurrentes
                    var verificacionFinal = base.FirstBy(w => w.Id == entidad.Id,
                        s => new { s.IdPersonalAsignado, s.RowVersion });

                    // Si la oportunidad ya fue asignada por otro proceso, lanzar excepción
                    if (verificacionFinal.IdPersonalAsignado != 125 && verificacionFinal.IdPersonalAsignado != null)
                    {
                        throw new System.Data.DBConcurrencyException(
                            $"La oportunidad {Oportunidad.Id} fue asignada por otro usuario. " +
                            $"Asesor asignado: {verificacionFinal.IdPersonalAsignado}."
                        );
                    }

                    // Si el RowVersion cambió, otra actualización ocurrió
                    if (!verificacionFinal.RowVersion.SequenceEqual(rowVersionOriginal))
                    {
                        throw new System.Data.DBConcurrencyException(
                            $"La oportunidad {Oportunidad.Id} fue modificada por otro usuario."
                        );
                    }

                    // Actualizar el RowVersion más reciente
                    Oportunidad.RowVersion = verificacionFinal.RowVersion;
                }

                // Realizar la actualización con Entity Framework
                // Entity Framework automáticamente validará el RowVersion
                base.Update(Oportunidad);
                return Oportunidad;
            }
            catch (System.Data.DBConcurrencyException)
            {
                // Re-lanzar las excepciones de concurrencia sin envolverlas
                throw;
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


        public IEnumerable<TOportunidad> Add(IEnumerable<Oportunidad> listadoEntidad)
        {
            try
            {
                List<TOportunidad> listado = new List<TOportunidad>();
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

        public IEnumerable<TOportunidad> Update(IEnumerable<Oportunidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidad> listado = new List<TOportunidad>();
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
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Oportunidad.
        /// </summary>
        /// <returns> List<OportunidadDTO> </returns>
        public IEnumerable<Oportunidad> ObtenerOportunidad()
        {
            try
            {
                List<Oportunidad> rpta = new List<Oportunidad>();
                var query = @"
                    SELECT
	                    Id,
	                    IdCentroCosto,
	                    IdPersonal_Asignado AS IdPersonalAsignado,
	                    IdTipoDato,
	                    IdFaseOportunidad,
	                    IdOrigen,
	                    IdAlumno,
	                    UltimoComentario,
	                    IdActividadDetalle_Ultima AS IdActividadDetalleUltima,
	                    IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                    IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,
	                    UltimaFechaProgramada,
	                    IdEstadoOportunidad,
	                    IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,
	                    IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                    IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,
	                    IdCategoriaOrigen,
	                    IdConjuntoAnuncio,
	                    IdCampaniaScoring,
	                    IdFaseOportunidad_IP AS IdFaseOportunidadIP,
	                    IdFaseOportunidad_IC AS IdFaseOportunidadIC,
	                    FechaEnvioFaseOportunidadPF,
	                    FechaPagoFaseOportunidadPF,
	                    FechaPagoFaseOportunidadIC,
	                    FechaRegistroCampania,
	                    IdFaseOportunidadPortal,
	                    IdFaseOportunidad_PF AS IdFaseOportunidadPF,
	                    CodigoPagoIC,
	                    FlagVentaCruzada,
	                    IdTiempoCapacitacion,
	                    IdTiempoCapacitacion_Validacion AS IdTiempoCapacitacionValidacion,
	                    IdSubCategoriaDato,
	                    IdInteraccionFormulario,
	                    UrlOrigen,
	                    FechaPaso2,
	                    Paso2,
	                    CodMailing,
	                    IdPagina,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    NroSolicitud,
	                    NroSolicitudPorArea,
	                    NroSolicitudPorSubArea,
	                    NroSolicitudPorProgramaGeneral,
	                    NroSolicitudPorProgramaEspecifico,
	                    IdClasificacionPersona,
	                    IdPersonalAreaTrabajo,
	                    IdPadre,
	                    IdAnuncioFacebook,
	                    ValidacionCorrecta
                    FROM com.T_Oportunidad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Oportunidad>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Oportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<OportunidadComboDTO> </returns>
        public IEnumerable<OportunidadComboDTO> ObtenerCombo()
        {
            try
            {
                List<OportunidadComboDTO> rpta = new List<OportunidadComboDTO>();
                var query = @"
                    SELECT
	                    OPO.Id,
	                    CONCAT(AL.ApellidoPaterno,' ',AL.ApellidoMaterno,', ',AL.Nombre1,' ',AL.Nombre1) AS Alumno,
	                    CC.Nombre AS CentroCosto
                    FROM com.T_Oportunidad AS OPO WITH (NOLOCK)
                    INNER JOIN pla.T_CentroCosto AS CC
	                    ON OPO.IdCentroCosto = CC.id
	                    AND CC.Estado = 1
                    INNER JOIN mkt.T_Alumno AS AL WITH (NOLOCK)
	                    ON OPO.IdAlumno = AL.Id
	                    AND AL.Estado = 1
                    WHERE OPO.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de Venta Cruzada para Agenda relacionada a un Id Clasificacion Persona.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> List<OportunidadVentaCruzadaAgendaDTO> </returns>
        public IEnumerable<OportunidadVentaCruzadaAgendaDTO> ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                List<OportunidadVentaCruzadaAgendaDTO> listaVentaCruzada = new List<OportunidadVentaCruzadaAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_Oportunidad_VentaCruzadaTop5", new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    listaVentaCruzada = JsonConvert.DeserializeObject<List<OportunidadVentaCruzadaAgendaDTO>>(resultadoStoreProcedure)!;
                    if (listaVentaCruzada != null)
                    {
                        listaVentaCruzada = listaVentaCruzada.Where(x => x.Orden == 1).OrderByDescending(v => v.Costo).ToList();
                    }
                }
                return listaVentaCruzada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CentroCostoVentaCruzadaDTO> ObtenerCentroCostoVentaCruzada(int idPGeneral)
        {
            try
            {
                List<CentroCostoVentaCruzadaDTO> rpta = new List<CentroCostoVentaCruzadaDTO>();

                var query = "EXEC pla.SP_ObtenerProgramaGeneralCentroCosto @IdPGeneral = @idPGeneral";

                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CentroCostoVentaCruzadaDTO>>(resultado);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Oportunidades para Agenda relacionado a un Id Clasificacion Persona.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> List<ClasificacionPersonaComboDTO> </returns>
        public IEnumerable<OportunidadHistorialAgendaDTO> ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                List<OportunidadHistorialAgendaDTO> historialOportunidad = new List<OportunidadHistorialAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_Oportunidad_HistorialOportunidades", new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    historialOportunidad = JsonConvert.DeserializeObject<List<OportunidadHistorialAgendaDTO>>(resultadoStoreProcedure);
                }
                return historialOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id Fase,Codigo Fase y la ultima Actividad Programada de la Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadCodigoFaseDTO> </returns>
        public OportunidadCodigoFaseDTO ObtenerCodigoFasePorIdOportunidad(int idOportunidad)
        {
            try
            {
                OportunidadCodigoFaseDTO faseOportunidad = new OportunidadCodigoFaseDTO();
                var query = @"SELECT FaseInicio, FechaSiguienteLlamada, IdFaseOportunidad  FROM com.V_ObtenerOportunidadCodigoFase WHERE IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (resultado == "null")
                    throw new Exception("Error: No se encontraron datos de la oportunidad: " + idOportunidad);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    faseOportunidad = JsonConvert.DeserializeObject<OportunidadCodigoFaseDTO>(resultado)!;
                }
                return faseOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos Compuestos de la Oportunidad asociada a una Actividad Detalle.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> OportunidadCompuestoDTO </returns>
        public OportunidadCompuestoDTO ObtenerOportunidadCompuestoPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                OportunidadCompuestoDTO oportunidad = new OportunidadCompuestoDTO();
                var query = @"
                    SELECT id,IdCentrocosto,IdPersonal_Asignado AS IdPersonalAsignado,IdTipoDato,IdFaseOportunidad,IdOrigen,CodigoPagoIC,IdAlumno,
	                    UltimoComentario,IdActividadDetalle_Ultima AS IdActividadDetalleUltima,IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                    IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,UltimaFechaProgramada,IdEstadoOportunidad,
	                    IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                    IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,IdCategoriaOrigen,NombrePatner AS NombrePartner,EncabezadoCorreoPartner,
	                    IdActividadDetalle,PrecioContado,NombreProgramaGeneral,Pw_Duracion AS PwDuracion,IdCategoriaPrograma,UrlVersion,
	                    UrlBrochurePrograma,FechaEnvio,Central,Anexo3CX,UrlFirmaCorreos,Email,IdPgeneral
                    FROM com.V_OportunidadCompuesto
                    WHERE IdActividadDetalle = @idActividadDetalle";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idActividadDetalle });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidad = JsonConvert.DeserializeObject<OportunidadCompuestoDTO>(resultadoQuery);
                }
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id de Solicitud Visualizacion, la Fecha Limite y Valor de Visualizacion
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> ResultadoVisualizarOportunidadDTO </returns>
        public ResultadoVisualizarOportunidadDTO ValidarVisualizarAgendaPorIdOportunidad(int idOportunidad, int idPersonal)
        {
            try
            {
                ResultadoVisualizarOportunidadDTO tiempoCapacitacion = new ResultadoVisualizarOportunidadDTO();
                var resultadoSP = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ValidarVisualizarOportunidadAgenda", new { idOportunidad, idPersonal });
                if (!string.IsNullOrEmpty(resultadoSP) && !resultadoSP.Contains("[]"))
                {
                    tiempoCapacitacion = JsonConvert.DeserializeObject<ResultadoVisualizarOportunidadDTO>(resultadoSP);
                }
                return tiempoCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de Oportunidad asociados a un Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> OportunidadBandejaCorreoDTO </returns>
        public OportunidadBandejaCorreoDTO? ObtenerOportunidadBandejaCorreoPorIdAlumno(int idAlumno)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_OportunidadPorAlumno", new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<OportunidadBandejaCorreoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_Oportunidad asociados a un Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public Oportunidad? ObtenerPorId(int idOportunidad)
        {
            try
            {
                var query = @"
                            SELECT
	                            Id,
	                            IdCentroCosto,
	                            IdPersonal_Asignado AS IdPersonalAsignado,
	                            IdTipoDato,
	                            IdFaseOportunidad,
	                            IdOrigen,
	                            IdAlumno,
	                            UltimoComentario,
	                            IdActividadDetalle_Ultima AS IdActividadDetalleUltima,
	                            IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                            IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,
	                            UltimaFechaProgramada,
	                            IdEstadoOportunidad,
	                            IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,
	                            IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                            IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,
	                            IdCategoriaOrigen,
	                            IdConjuntoAnuncio,
	                            IdCampaniaScoring,
	                            IdFaseOportunidad_IP AS IdFaseOportunidadIp,
	                            IdFaseOportunidad_IC AS IdFaseOportunidadIc,
	                            FechaEnvioFaseOportunidadPF AS FechaEnvioFaseOportunidadPf,
	                            FechaPagoFaseOportunidadPF AS FechaPagoFaseOportunidadPf,
	                            FechaPagoFaseOportunidadIC AS FechaPagoFaseOportunidadIc,
	                            FechaRegistroCampania,
	                            IdFaseOportunidadPortal,
	                            IdFaseOportunidad_PF AS IdFaseOportunidadPf,
	                            CodigoPagoIC AS CodigoPagoIc,
	                            FlagVentaCruzada,
	                            IdTiempoCapacitacion,
	                            IdTiempoCapacitacion_Validacion AS IdTiempoCapacitacionValidacion,
	                            IdSubCategoriaDato,
	                            IdInteraccionFormulario,
	                            UrlOrigen,
	                            FechaPaso2,
	                            Paso2,
	                            CodMailing,
	                            IdPagina,
	                            Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion,
	                            RowVersion,
	                            IdMigracion,
	                            NroSolicitud,
	                            NroSolicitudPorArea,
	                            NroSolicitudPorSubArea,
	                            NroSolicitudPorProgramaGeneral,
	                            NroSolicitudPorProgramaEspecifico,
	                            IdClasificacionPersona,
	                            IdPersonalAreaTrabajo,
	                            IdPadre,
	                            IdAnuncioFacebook,
	                            ValidacionCorrecta,
	                            EnLlamada,
	                            NumeroIntentoLlamada,
	                            FechaReprogramacionIntento
                            FROM com.T_Oportunidad
                            WHERE Estado = 1 AND Id = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Oportunidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_Oportunidad asociados a un Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public async Task<Oportunidad?> ObtenerPorIdAsync(int idOportunidad)
        {
            try
            {
                var query = @"
                            SELECT
	                            Id,
	                            IdCentroCosto,
	                            IdPersonal_Asignado AS IdPersonalAsignado,
	                            IdTipoDato,
	                            IdFaseOportunidad,
	                            IdOrigen,
	                            IdAlumno,
	                            UltimoComentario,
	                            IdActividadDetalle_Ultima AS IdActividadDetalleUltima,
	                            IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                            IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,
	                            UltimaFechaProgramada,
	                            IdEstadoOportunidad,
	                            IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,
	                            IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                            IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,
	                            IdCategoriaOrigen,
	                            IdConjuntoAnuncio,
	                            IdCampaniaScoring,
	                            IdFaseOportunidad_IP AS IdFaseOportunidadIp,
	                            IdFaseOportunidad_IC AS IdFaseOportunidadIc,
	                            FechaEnvioFaseOportunidadPF,
	                            FechaPagoFaseOportunidadPF,
	                            FechaPagoFaseOportunidadIC,
	                            FechaRegistroCampania,
	                            IdFaseOportunidadPortal,
	                            IdFaseOportunidad_PF AS IdFaseOportunidadPf,
	                            CodigoPagoIC,
	                            FlagVentaCruzada,
	                            IdTiempoCapacitacion,
	                            IdTiempoCapacitacion_Validacion AS IdTiempoCapacitacionValidacion,
	                            IdSubCategoriaDato,
	                            IdInteraccionFormulario,
	                            UrlOrigen,
	                            FechaPaso2,
	                            Paso2,
	                            CodMailing,
	                            IdPagina,
	                            Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion,
	                            RowVersion,
	                            IdMigracion,
	                            NroSolicitud,
	                            NroSolicitudPorArea,
	                            NroSolicitudPorSubArea,
	                            NroSolicitudPorProgramaGeneral,
	                            NroSolicitudPorProgramaEspecifico,
	                            IdClasificacionPersona,
	                            IdPersonalAreaTrabajo,
	                            IdPadre,
	                            IdAnuncioFacebook,
	                            ValidacionCorrecta,
	                            EnLlamada,
	                            NumeroIntentoLlamada,
	                            FechaReprogramacionIntento,
                                IdPersonal_CoordinadorSeguimiento
                            FROM com.T_Oportunidad
                            WHERE Estado = 1 AND Id = @idOportunidad";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idOportunidad });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Oportunidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_Oportunidad asociados a un Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public int? ObtenerIdCentroCostoPorId(int idOportunidad)
        {
            try
            {
                var query = "SELECT IdCentroCosto AS Valor FROM com.T_Oportunidad WHERE Estado = 1 AND Id = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public string ObtenerCronogramaPagoCompleto(int idOportunidad)
        {
            try
            {
                var _resultado = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"com.SP_ObtenerMontoPagoCronogramaPagoCuotasCompleto";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (_resultado.Count() > 0 && _resultado != null)//tabla
                {
                    var totalCuotas = _resultado.Max(x => x.NroCuota);
                    var ultimo = _resultado.Last();
                    _htmlFinal += $@"
                                    <table style='width: 390px;text-align: center;'>
                                        <tr>
                                            <th style='width:103px;height:28px;text-align: center;'> Nro. Cuota </th>
                                            <th style='width:140px;height:28px;text-align: center;'> Monto </th>
                                            <th style='width:193px;height:28px;text-align: center;'> Fecha de vencimiento </th>
                                        </tr>
                                        ";
                    foreach (var item in _resultado)
                    {
                        _htmlFinal += $@"
                                        <tr>
                                            <td style='width:103px;height:23px;text-align:center;'> {item.NroCuota}</td>
                                            <td style='width:140px;height:23px;text-align:center;'> {item.SimboloMoneda} {item.Cuota} </td>
                                            <td style='width:193px;height:23px;text-align:center;'> {item.FechaVencimiento.ToString("dd/MM/yyyy")} </td>
                                        </tr>
                                        ";
                    }
                    _htmlFinal += $@"</table>";
                }
                return _htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 10/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public async Task<string> ObtenerCronogramaPagoCompletoAsync(int idOportunidad)
        {
            try
            {
                var _resultado = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"com.SP_ObtenerMontoPagoCronogramaPagoCuotasCompleto";
                var resultado = await _dapperRepository.QuerySPDapperAsync(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (_resultado.Count() > 0 && _resultado != null)//tabla
                {
                    var totalCuotas = _resultado.Max(x => x.NroCuota);
                    var ultimo = _resultado.Last();
                    _htmlFinal += $@"
                                    <table style='width: 390px;text-align: center;'>
                                        <tr>
                                            <th style='width:103px;height:28px;text-align: center;'> Nro. Cuota </th>
                                            <th style='width:140px;height:28px;text-align: center;'> Monto </th>
                                            <th style='width:193px;height:28px;text-align: center;'> Fecha de vencimiento </th>
                                        </tr>
                                        ";
                    foreach (var item in _resultado)
                    {
                        _htmlFinal += $@"
                                        <tr>
                                            <td style='width:103px;height:23px;text-align:center;'> {item.NroCuota}</td>
                                            <td style='width:140px;height:23px;text-align:center;'> {item.SimboloMoneda} {item.Cuota} </td>
                                            <td style='width:193px;height:23px;text-align:center;'> {item.FechaVencimiento.ToString("dd/MM/yyyy")} </td>
                                        </tr>
                                        ";
                    }
                    _htmlFinal += $@"</table>";
                }
                return _htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Carlos H. Crispin Riquelme.
        /// Fecha: 02/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html para chile
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public async Task<string> ObtenerCronogramaPagoCompletoChileAsync(int idOportunidad)
        {
            try
            {
                var _resultado = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"com.SP_ObtenerMontoPagoCronogramaPagoCuotasCompleto";
                var resultado = await _dapperRepository.QuerySPDapperAsync(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }
                var _htmlFinal = "";
                var simboloMoneda = "";

                if (_resultado != null)
                {
                    if (_resultado.Count() == 1)
                    {
                        string totalPago = "";
                        totalPago = _resultado.FirstOrDefault() == null ? "" : _resultado.FirstOrDefault().Cuota.ToString();
                        _htmlFinal = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + " a ser cancelados en una sola cuota antes del ";

                    }
                    else
                    {
                        var valormatricula = _resultado.Where(w => w.NroCuota == 1).Select(w => w).FirstOrDefault();
                        var valorcuotas = _resultado.Where(w => w.NroCuota != 1).Select(w => w).FirstOrDefault();
                        var arancel = _resultado.Where(w => w.NroCuota != 1).Select(w => w).Sum(w => w.Cuota);
                        simboloMoneda = valormatricula == null ? "" : valormatricula.SimboloMoneda;

                        _htmlFinal = "El Alumno debera pagar: i) el valor de la matricula de " + simboloMoneda + " " + (valormatricula == null ? "" : valormatricula.Cuota.ToString()) + ", a mas tardar el dia " + (valormatricula == null ? "" : valormatricula.FechaVencimiento.ToString("dd/MM/yyyy")) + " , y ii) el valor del arancel del PROGRAMA " + simboloMoneda + " " + arancel.ToString() + ", el cual se pagara en " + (_resultado.Count - 1).ToString() + " cuotas mensuales iguales y sucesivas de " + simboloMoneda + " " + (valorcuotas == null ? "" : valorcuotas.Cuota.ToString()) + " cada una, a mas tardar el dia " + (valorcuotas == null ? "" : valorcuotas.FechaVencimiento.Day.ToString()) + " del mes respectivo siguiendo el siguiente cronograma de pago, excepto si optare por el pago anticipado<br><br> ";

                    }


                    if (_resultado.Count() > 0 && _resultado != null)//tabla
                    {
                        var totalCuotas = _resultado.Max(x => x.NroCuota);
                        var ultimo = _resultado.Last();
                        _htmlFinal += $@"
                                    <table style='width: 390px;text-align: center;'>
                                        <tr>
                                            <th style='width:103px;height:28px;text-align: center;'> Nro. Cuota </th>
                                            <th style='width:140px;height:28px;text-align: center;'> Monto </th>
                                            <th style='width:193px;height:28px;text-align: center;'> Fecha de vencimiento </th>
                                        </tr>
                                        ";
                        foreach (var item in _resultado)
                        {
                            _htmlFinal += $@"
                                        <tr>
                                            <td style='width:103px;height:23px;text-align:center;'> {item.NroCuota}</td>
                                            <td style='width:140px;height:23px;text-align:center;'> {item.SimboloMoneda} {item.Cuota} </td>
                                            <td style='width:193px;height:23px;text-align:center;'> {item.FechaVencimiento.ToString("dd/MM/yyyy")} </td>
                                        </tr>
                                        ";
                        }
                        _htmlFinal += $@"</table>";
                    }
                    return _htmlFinal.Replace("dolares", "dólares");
                }
                else
                {
                    return "";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public string ObtenerMontoTotal(int idOportunidad)
        {
            try
            {
                var _resultado = new ResumenCronogramaDTO();
                var query = $@"com.SP_ObtenerMontoTotal";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ResumenCronogramaDTO>(resultado);
                }
                return string.Concat(_resultado.SimboloMoneda, " ", _resultado.MontoTotal, " ", _resultado.NombrePluralMoneda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public async Task<string> ObtenerMontoTotalAsync(int idOportunidad)
        {
            try
            {
                var _resultado = new ResumenCronogramaDTO();
                var query = $@"com.SP_ObtenerMontoTotal";
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ResumenCronogramaDTO>(resultado);
                }
                return string.Concat(_resultado.SimboloMoneda, " ", _resultado.MontoTotal, " ", _resultado.NombrePluralMoneda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public string ObtenerVersion(int idOportunidad)
        {
            try
            {
                var _resultado = new StringDTO();
                var query = $@"com.SP_ObtenerVersionAlumno";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public async Task<string> ObtenerVersionAsync(int idOportunidad)
        {
            try
            {
                var _resultado = new StringDTO();
                var query = $@"com.SP_ObtenerVersionAlumno";
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por Id de la Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadCompuestoDTO </returns>
        public OportunidadCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                OportunidadCompuestoDTO oportunidad = new OportunidadCompuestoDTO();
                var query = @"
                    SELECT
	                    Id,IdCentrocosto, IdPespecifico,IdPersonal_Asignado AS IdPersonalAsignado,IdTipoDato,IdFaseOportunidad,IdOrigen,CodigoPagoIC,IdAlumno,
	                    UltimoComentario,IdActividadDetalle_Ultima AS IdActividadDetalleUltima,IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                    IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,UltimaFechaProgramada,IdEstadoOportunidad,
	                    IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                    IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,IdCategoriaOrigen,NombrePatner,EncabezadoCorreoPartner,IdActividadDetalle,
	                    PrecioContado,NombreProgramaGeneral,Pw_Duracion AS PwDuracion,IdCategoriaPrograma,UrlVersion,UrlBrochurePrograma,FechaEnvio,
	                    Central,Anexo3CX,UrlFirmaCorreos,Email,IdPgeneral
                    FROM com.V_OportunidadCompuesto
                    WHERE Id = @idOportunidad";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidad = JsonConvert.DeserializeObject<OportunidadCompuestoDTO>(resultadoQuery);
                }
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por Id de la Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadCompuestoDTO </returns>
        public async Task<OportunidadCompuestoDTO> ObtenerDatosCompuestosPorIdOportunidadAsync(int idOportunidad)
        {
            try
            {
                OportunidadCompuestoDTO oportunidad = new OportunidadCompuestoDTO();
                var query = @"
                    SELECT
	                    Id,IdCentrocosto,IdPersonal_Asignado AS IdPersonalAsignado,IdTipoDato,IdFaseOportunidad,IdOrigen,CodigoPagoIC,IdAlumno,
	                    UltimoComentario,IdActividadDetalle_Ultima AS IdActividadDetalleUltima,IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                    IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,UltimaFechaProgramada,IdEstadoOportunidad,
	                    IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                    IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,IdCategoriaOrigen,NombrePatner,EncabezadoCorreoPartner,IdActividadDetalle,
	                    PrecioContado,NombreProgramaGeneral,Pw_Duracion AS PwDuracion,IdCategoriaPrograma,UrlVersion,UrlBrochurePrograma,FechaEnvio,
	                    Central,Anexo3CX,UrlFirmaCorreos,Email,IdPgeneral
                    FROM com.V_OportunidadCompuesto
                    WHERE Id = @idOportunidad";
                var resultadoQuery = await _dapperRepository.FirstOrDefaultAsync(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidad = JsonConvert.DeserializeObject<OportunidadCompuestoDTO>(resultadoQuery);
                }
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si existe una oportunidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <param name="idContacto">Id Contacto</param>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> bool </returns>
        public bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia)
        {
            try
            {
                var _resultado = new BoolRN2DTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_ValidarRN2Agenda", new { idContacto, idCentroCosto, idOcurrencia });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<BoolRN2DTO>(resultado);
                }
                return _resultado.Estado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos necesarios de la oportunidad para calcualr la fecha de programacion Automatica
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> DatosOportunidadReprogramacionAutomaticaDTO </returns>
        public DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramacionAutomatica(int idOportunidad)
        {
            try
            {
                var datosReprogramacion = new DatosOportunidadReprogramacionAutomaticaDTO();
                var query = @"
                    SELECT IdPersonalAsignado,IdActividadCabeceraUltima,IdTipoDato,IdCategoriaOrigen
                    FROM com.V_TOportunidad_FechaProgramacionAutomatica
                    WHERE Id = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    datosReprogramacion = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionAutomaticaDTO>(resultado);
                }
                return datosReprogramacion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// Autor Modificacion: Jonathan Caipo
        /// Fecha Modificacion: 28/03/2023
        /// Version: 1.2
        /// <summary> Modificacion
        /// Se creó un SP que valide idOportunidad, idFaseOportunidad, idActividadDetalle
        /// </summary>
        /// <summary>
        /// Determina si una Oportunidad existe y si tiene la Fase consultada
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="idFaseOportunidad"> id de fase oportunidad </param>
        /// <param name="idActividadDetalle"> idActividadDetalle de la oportunidad </param>
        /// <returns> bool </returns>
        public ActividadTrabajadaDTO ValidarFaseOportunidad(int idOportunidad, int idFaseOportunidad, int idActividadDetalle)
        {
            try
            {
                ActividadTrabajadaDTO datosReprogramacion = new ActividadTrabajadaDTO();
                string querySP = "com.SP_ValidacionReprogramacionDeOportunidades";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(querySP, new { idOportunidad, idFaseOportunidad, idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    datosReprogramacion = JsonConvert.DeserializeObject<ActividadTrabajadaDTO>(resultado)!;
                }
                return datosReprogramacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// Autor Modificacion: Gilmer Quispe.
        /// Fecha Modificacion: 13/01/2023
        /// Version: 1.1
        /// <summary> Modificacion
        /// Se cambia condicion: [Antes IdFaseOportunidad] [Actual:IdActividadDetalle_Ultima]
        /// </summary>
        /// <summary>
        /// Determina si una Oportunidad existe y si tiene la Fase consultada
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="idFaseOportunidad"> id de fase oportunidad </param>
        /// <param name="idActividadDetalle"> idActividadDetalle de la oportunidad </param>
        /// <returns> bool </returns>
        public async Task<ActividadTrabajadaDTO> ValidarFaseOportunidadAsync(int idOportunidad, int idFaseOportunidad, int idActividadDetalle)
        {
            try
            {
                ActividadTrabajadaDTO datosReprogramacion = new ActividadTrabajadaDTO();
                string querySP = "com.SP_ValidacionReprogramacionDeOportunidades";
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync(querySP, new { IdOportunidad = idOportunidad, IdFaseOportunidad = idFaseOportunidad, IdActividadDetalle = idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    datosReprogramacion = JsonConvert.DeserializeObject<ActividadTrabajadaDTO>(resultado)!;
                }
                return datosReprogramacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene el centro costo probable por el contacto y la fecha
        /// </summary>
        /// <param name="idContacto"> Id de contacto </param>
        /// <param name="fechaActual"> Fecha </param>
        /// <returns> List<CentroCostoProbableDTO> </returns>
        public List<CentroCostoProbableDTO> ObtenerCentroCostoProbable(int idContacto, DateTime fechaActual)
        {
            try
            {
                List<CentroCostoProbableDTO> centroCostoProbables = new List<CentroCostoProbableDTO>();
                var _query = @"SELECT DISTINCT IdPEspecifico, Tipo, IdPersonal, Precio 
                                FROM com.V_ObtenerCentroCostoProbable 
                                WHERE IdAlumno = @idContacto AND estadop LIKE '%Lanzamiento%' 
                                AND probabilidadActualDesc = 'Muy Alta' AND activo = 1 
                                AND EstadoPersonal = 1 AND EstadoPeriodo = 1 
                                AND EstadoPeriodoMeta = 1 AND EstadoPEspecifico = 1 
                                AND EstadoProbabilidadContactoPrograma = 1 
                                AND @fechaActual BETWEEN FechaInicial AND FechaFin";
                var centroCostoProbablesDB = _dapperRepository.QueryDapper(_query, new { idContacto, fechaActual });
                centroCostoProbables = JsonConvert.DeserializeObject<List<CentroCostoProbableDTO>>(centroCostoProbablesDB);
                return centroCostoProbables;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si cumple las reglas de validacion por probabilidad
        /// </summary>
        /// <param name="idProbabilidadRegistroPwActual">Id de la probabilidad de registro actual</param>
        /// <returns> Booleano </returns>
        public bool ValidarProbabilidadVentaCruzada(int? idProbabilidadRegistroPwActual)
        {
            try
            {
                idProbabilidadRegistroPwActual = idProbabilidadRegistroPwActual == null ? 0 : idProbabilidadRegistroPwActual;
                var validacionProbabilidadesVentaCruzada = this.ObtenerValidacionesProbabilidadVentaCruzada();
                return validacionProbabilidadesVentaCruzada.Any(x => x.Valor == idProbabilidadRegistroPwActual);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las validades de probabilidad para venta cruzada
        /// </summary>
        /// <returns> List<ValorIntDTO> </returns>
        public List<ValorIntDTO> ObtenerValidacionesProbabilidadVentaCruzada()
        {
            try
            {
                var ventaCruzadaValidacion = new List<ValorIntDTO>();
                var _query = "SELECT IdProbabilidadRegistroPW AS Valor FROM mkt.V_TVentaCruzadaValidacionProbabilidad_ObtenerProbabilidades Where Estado = 1";
                var probabilidadesValidacionDB = _dapperRepository.QueryDapper(_query, null);
                ventaCruzadaValidacion = JsonConvert.DeserializeObject<List<ValorIntDTO>>(probabilidadesValidacionDB);
                return ventaCruzadaValidacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de la oportunidad a reasignar
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad </param>
        /// <returns> OportunidadReasignarDTO </returns>
        public OportunidadReasignarDTO ObtenerDatosOportunidadReasignacion(int idOportunidad)
        {
            try
            {
                var oportunidadReasignar = new OportunidadReasignarDTO();
                var _query = @"SELECT IdAsesor, NombreCompletoAsesor, EmailAsesor, IdJefe, NombreCompletoJefe, 
                                EmailJefe, IdOportunidad, CodigoFaseOportunidad, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno 
                              FROM   com.V_ObtenerDatosOportunidadAReasignar 
                              WHERE EstadoPersonal = 1 AND EstadoOportunidad = 1 AND EstadoFaseOportunidad = 1 
                              AND IdOportunidad IN ( @idOportunidad)";
                var datosOportunidadDB = _dapperRepository.FirstOrDefault(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(datosOportunidadDB) && !datosOportunidadDB.Contains("[]") && datosOportunidadDB != "null")
                {
                    oportunidadReasignar = JsonConvert.DeserializeObject<OportunidadReasignarDTO>(datosOportunidadDB);
                }
                return oportunidadReasignar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener probabildiades de modelos predictivos
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> Obtiene Probabilidad según un modelo predictivo : ProbabilidadOportunidadResumenDTO </returns>
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivo(int idOportunidad)
        {
            ProbabilidadOportunidadResumenDTO probabilidad;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var url = $"https://integrav4-ia-modelopredictivo.bsginstitute.com/api/ProbalidadInscripcionV4/ObtenerProbabilidad?idOportunidad={idOportunidad}";
                    var rpta = wc.DownloadString(new Uri(url));
                    probabilidad = JsonConvert.DeserializeObject<ProbabilidadOportunidadResumenDTO>(rpta);
                    return probabilidad;
                }
                catch (WebException e)
                {
                    string responseText = "";

                    var responseStream = e.Response?.GetResponseStream();

                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }

                    if (string.IsNullOrEmpty(responseText))
                        responseText = e.Message;

                    throw new Exception(responseText);
                }
            }
        }
        /// Autor: Margiory Ramirez.
        /// Fecha: 21/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtener probabildiades de modelos predictivos
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> Obtiene Probabilidad según un modelo predictivo : ProbabilidadOportunidadResumenDTO </returns>
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivoMarketing(int idOportunidad, int tipo)
        {
            ProbabilidadOportunidadResumenDTO probabilidad;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var url = $"https://integrav4-ia-modelopredictivo.bsginstitute.com/api/ProbabilidadInscripcionRegresion/ObtenerProbabilidadporTipo/{idOportunidad}/{tipo}";
                    var rpta = wc.DownloadString(new Uri(url));
                    probabilidad = JsonConvert.DeserializeObject<ProbabilidadOportunidadResumenDTO>(rpta);
                    return probabilidad;
                }
                catch (WebException e)
                {
                    string responseText = "";

                    var responseStream = e.Response?.GetResponseStream();

                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }

                    if (string.IsNullOrEmpty(responseText))
                        responseText = e.Message;

                    throw new Exception(responseText);
                }
            }
        }



        public void ObtenerProbabilidadTodosProgramasPorAlumno(int idAlumno)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    var url = $"https://integrav4-ia-modelopredictivo.bsginstitute.com/api/ProbabilidadInscripcionRegresion/ObtenerProbabilidadTodosProgramasPorAlumno/{idAlumno}";

                    var response = wc.DownloadString(new Uri(url));

                    Console.WriteLine($"Solicitud completada con éxito para el alumno {idAlumno}");
                }
                catch (WebException e)
                {
                    string responseText = "";

                    if (e.Response != null)
                    {
                        using (var reader = new StreamReader(e.Response.GetResponseStream()))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }

                    Console.WriteLine($"Error al procesar la solicitud para el alumno {idAlumno}: {responseText ?? e.Message}");
                    throw new Exception(string.IsNullOrEmpty(responseText) ? e.Message : responseText);
                }
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Registro de cada Oportunidad por filtro
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paginador"></param>
        /// <returns></returns>
        /// <exception>FiltrosRegistrarOportunidadDTO, PaginadorDTO</exception>
        public ResultadoOportunidadesDTO ObtenerPorFiltroRegistrarOportunidad(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador, int? idTipoProgramaCarrera)
        {
            try
            {

                var filtros = new object();
                string alumno = string.Empty;
                string queryCondicion = string.Empty;
                List<string> idFaseOportunidad = new();
                List<string> idPersonal = new();
                List<string> idOrigen = new();
                List<string> idCentroCosto = new();
                List<string> idTipoDato = new();
                string paginacion = string.Empty;

                if (!string.IsNullOrEmpty(obj.FasesOportunidad))
                {
                    queryCondicion += "AND IdFaseOportunidad in @IdFaseOportunidad ";
                    idFaseOportunidad = obj.FasesOportunidad.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.Origenes))
                {
                    queryCondicion += "AND IdOrigen in @IdOrigen ";
                    idOrigen = obj.Origenes.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.CentrosCosto))
                {
                    queryCondicion += "AND IdCentroCosto in @IdCentroCosto ";
                    idCentroCosto = obj.CentrosCosto.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.Asesores))
                {
                    queryCondicion += "AND IdPersonal in @IdPersonal ";
                    idPersonal = obj.Asesores.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.TiposDato))
                {
                    queryCondicion += "AND IdTipoDato in @IdTipoDato ";
                    idTipoDato = obj.TiposDato.Split(",").ToList();
                }
                if (obj.contacto != "")
                {
                    queryCondicion += @"AND ((@Alumno = '' AND (ApellidoPaterno + ' ' + ApellidoMaterno + ' ' + Nombre1 + ' ' + Nombre2 ) <> '')
                                        OR(@Alumno <> '' AND(ApellidoPaterno + ' ' + ApellidoMaterno + (CASE ApellidoMaterno WHEN '' THEN '' ELSE ' ' END) + Nombre1 + ' ' + Nombre2) LIKE '%' + @Alumno + '%')) ";
                    alumno = obj.contacto;
                }
                if (obj.FechaFin == null || obj.FechaInicio == null)
                {
                    obj.FechaFin = DateTime.Now;
                    obj.FechaInicio = DateTime.Now;
                }

                if (paginador.take != 0)
                {
                    paginacion = " OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                }
                obj.FechaFin = obj.FechaFin.Value.AddDays(1);
                string queryCampos = @" Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Email1,Email2,IdCentroCosto,NombreCentroCosto,IdPersonal,NombrePersonal,IdTipoDato,NombreTipoDato,IdFaseOportunidad,CodigoFase,CodigoFaseMaxima,IdOrigen,NombreOrigen,NombrePais,CodigoPais,NombreCiudad,CodigoCiudad,HoraPeru,HoraContacto,Celular,Telefono,Direccion,Dni,IdEmpresa,IdCargo,IdFormacion,IdTrabajo,IdIndustria,IdOportunidad,FechaCreacionOportunidad,IdReferido,Asociado,NombreGrupo,CodigoMailing";

                string condicionTipoProgramaCarrera = string.Empty;
                if (idTipoProgramaCarrera != null)
                {
                    condicionTipoProgramaCarrera = " AND IdTipoProgramaCarrera = 2";
                }
                string queryFinal = $@"SELECT {queryCampos} FROM [com].[V_ObtenerOportunidadesContactos2]
                                    WHERE FechaCreacionOportunidad BETWEEN @FechaInicio AND @FechaFin {queryCondicion} {condicionTipoProgramaCarrera}
                                    ORDER BY FechaCreacionOportunidad DESC {paginacion}";
                var queryOportunidad = _dapperRepository.QueryDapper(queryFinal, new { obj.FechaInicio, obj.FechaFin, idOrigen, idFaseOportunidad, idCentroCosto, idPersonal, alumno, idTipoDato, Skip = paginador.skip, Take = paginador.take });
                var respuesta = JsonConvert.DeserializeObject<List<ResultadoRegistrarOportunidadFiltroDTO>>(queryOportunidad);
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault($"SELECT COUNT(*) FROM [com].[V_ObtenerOportunidadesContactos2] WHERE FechaCreacionOportunidad BETWEEN @FechaInicio AND @FechaFin {queryCondicion} {condicionTipoProgramaCarrera}", new
                {
                    obj.FechaInicio,
                    obj.FechaFin,
                    IdFaseOportunidad = idFaseOportunidad,
                    IdCentroCosto = idCentroCosto,
                    IdOrigen = idOrigen,
                    IdPersonal = idPersonal,
                    Alumno = alumno,
                    IdTipoDato = idTipoDato
                }));

                //foreach (var item in respuesta)
                //{
                //    item.Email1 = EncriptarStringCorreo(item.Email1);
                //    item.Email2 = EncriptarStringCorreo(item.Email2);
                //    item.Celular = EncriptarStringNumero(item.Celular);
                //}

                return new ResultadoOportunidadesDTO { data = respuesta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResultadoOportunidadesDTO ObtenerPorFiltroRegistrarOportunidadSinAA(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador, string area, string tipoPersonal)
        {
            try
            {

                var filtros = new object();
                string alumno = string.Empty;
                string queryCondicion = string.Empty;
                List<string> idFaseOportunidad = new();
                List<string> idPersonal = new();
                List<string> idOrigen = new();
                List<string> idCentroCosto = new();
                List<string> idTipoDato = new();
                string paginacion = string.Empty;

                if (!string.IsNullOrEmpty(obj.FasesOportunidad))
                {
                    queryCondicion += "AND IdFaseOportunidad in @IdFaseOportunidad ";
                    idFaseOportunidad = obj.FasesOportunidad.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.Origenes))
                {
                    queryCondicion += "AND IdOrigen in @IdOrigen ";
                    idOrigen = obj.Origenes.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.CentrosCosto))
                {
                    queryCondicion += "AND IdCentroCosto in @IdCentroCosto ";
                    idCentroCosto = obj.CentrosCosto.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.Asesores))
                {
                    queryCondicion += "AND IdPersonal in @IdPersonal ";
                    idPersonal = obj.Asesores.Split(",").ToList();
                }
                if (!string.IsNullOrEmpty(obj.TiposDato))
                {
                    queryCondicion += "AND IdTipoDato in @IdTipoDato ";
                    idTipoDato = obj.TiposDato.Split(",").ToList();
                }
                if (obj.contacto != "")
                {
                    queryCondicion += @"AND ((@Alumno = '' AND (ApellidoPaterno + ' ' + ApellidoMaterno + ' ' + Nombre1 + ' ' + Nombre2 ) <> '')
                                        OR(@Alumno <> '' AND(ApellidoPaterno + ' ' + ApellidoMaterno + (CASE ApellidoMaterno WHEN '' THEN '' ELSE ' ' END) + Nombre1 + ' ' + Nombre2) LIKE '%' + @Alumno + '%')) ";
                    alumno = obj.contacto;
                }
                if (obj.FechaFin == null || obj.FechaInicio == null)
                {
                    obj.FechaFin = DateTime.Now;
                    obj.FechaInicio = DateTime.Now;
                }

                if (paginador.take != 0)
                {
                    paginacion = " OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                }
                obj.FechaFin = obj.FechaFin.Value.AddDays(1);
                string queryCampos = @" Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Email1,Email2,IdCentroCosto,NombreCentroCosto,IdPersonal,NombrePersonal,IdTipoDato,NombreTipoDato,IdFaseOportunidad,CodigoFase,CodigoFaseMaxima,IdOrigen,NombreOrigen,NombrePais,CodigoPais,NombreCiudad,CodigoCiudad,HoraPeru,HoraContacto,Celular,Telefono,Direccion,Dni,IdEmpresa,IdCargo,IdFormacion,IdTrabajo,IdIndustria,IdOportunidad,FechaCreacionOportunidad,IdReferido,Asociado,NombreGrupo,CodigoMailing";

                string condicionSinAsignacionAutomatica = string.Empty;


                condicionSinAsignacionAutomatica = " AND IdPersonal <> 125 ";

                string queryFinal = $@"SELECT {queryCampos} FROM [com].[V_ObtenerOportunidadesContactos2]
                                    WHERE FechaCreacionOportunidad BETWEEN @FechaInicio AND @FechaFin {queryCondicion} {condicionSinAsignacionAutomatica}
                                    ORDER BY FechaCreacionOportunidad DESC {paginacion}";
                var queryOportunidad = _dapperRepository.QueryDapper(queryFinal, new { obj.FechaInicio, obj.FechaFin, idOrigen, idFaseOportunidad, idCentroCosto, idPersonal, alumno, idTipoDato, Skip = paginador.skip, Take = paginador.take });
                var respuesta = JsonConvert.DeserializeObject<List<ResultadoRegistrarOportunidadFiltroDTO>>(queryOportunidad);
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault($"SELECT COUNT(*) FROM [com].[V_ObtenerOportunidadesContactos2] WHERE FechaCreacionOportunidad BETWEEN @FechaInicio AND @FechaFin {queryCondicion} {condicionSinAsignacionAutomatica}", new
                {
                    obj.FechaInicio,
                    obj.FechaFin,
                    IdFaseOportunidad = idFaseOportunidad,
                    IdCentroCosto = idCentroCosto,
                    IdOrigen = idOrigen,
                    IdPersonal = idPersonal,
                    Alumno = alumno,
                    IdTipoDato = idTipoDato
                }));

                //foreach (var item in respuesta)
                //{
                //    item.Email1 = EncriptarStringCorreo(item.Email1);
                //    item.Email2 = EncriptarStringCorreo(item.Email2);
                //    item.Celular = EncriptarStringNumero(item.Celular);
                //}
                return new ResultadoOportunidadesDTO { data = respuesta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                //return new { data = respuesta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Encripta el Email
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Encripta el número de celular
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public string EncriptarStringNumero(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int longitud = parametro.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    //respuesta = parametro.Remove(0, posicion) + new string('x', 4);
                    respuesta = parametro.Remove(posicion, 4) + new string('x', 4);
                }
            }
            return respuesta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idPersonal"></param>
        /// <returns>int</returns>
        public int ObtenerCentroCostoPorCelularAlumno(string numero, int idPersonal)
        {
            try
            {
                string queryOportunidad = "Select IdCentroCosto as Id From [com].[V_TOportunidadPorNumero] Where EstadoAlumno=1 AND (Celular =@numero) AND IdPersonal=@idPersonal AND FaseOportunidad in('BNC','IT','IP','PF','IC','IS','M') AND IdPersonal!=125 Order By FechaCreacion Desc";
                var resultado = _dapperRepository.FirstOrDefault(queryOportunidad, new { numero, idPersonal });
                if (resultado != "null" && resultado != "")
                {
                    var oportunidad = JsonConvert.DeserializeObject<FiltroDTO>(resultado)!;
                    return oportunidad.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el IdActividadDetalle_Ultima
        /// </summary>
        /// <param name="idActividadDetalle"> Id de la actividadDetalle</param>
        /// <param name="idOportunidad"> Id de la Oportunidad</param>
        /// <returns> Obtiene Probabilidad según un modelo predictivo : ProbabilidadOportunidadResumenDTO </returns>
        public string ActualizarIdActividadDetalleUltima(int idActividadDetalle, int idOportunidad)
        {
            try
            {
                string resultado;
                var _query = "com.SP_TOportunidad_ActualizarIdActividadDetalle_Ultima";
                resultado = _dapperRepository.QuerySPDapper(_query, new { IdActividadDetalle = idActividadDetalle, IdOportunidad = idOportunidad });
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Carlos Crispin.
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza IdPersonal_CoordinadorSeguimiento
        /// </summary>
        /// <param name="idOportunidad"> Id de la Oportunidad</param>
        /// <returns> Actualiza el idPersonal_CoordinadorSeguimiento con el id del jefe del asesor que lo tiene asignado en ese momento </returns>
        public async Task ActualizarIdPersonalCoordinadorSeguimiento(int idOportunidad)
        {
            try
            {
                string _queryactualizacion = "com.SP_TOportunidad_ActualizarIdPersonal_CoordinadorSeguimiento";
                var queryactualizacion = await _dapperRepository.QuerySPDapperAsync(_queryactualizacion, new { IdOportunidad = idOportunidad });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string InsertarContadorReprogramacionManual(int idOportunidad, int contador)
        {
            try
            {
                string resultado;
                var _query = "com.SP_TContadorReprogramacionManual_Insertar";
                resultado = _dapperRepository.QuerySPDapper(_query, new { IdOportunidad = idOportunidad, Contador = contador });
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public OportunidadBandejaCorreoDTO ObtenerOportunidadPorAsesorYAlumno(int idAlumno, int idPersonal, string numero)
        {
            try
            {
                if (numero.StartsWith("51"))
                {
                    numero = numero.Substring(2, 9);
                }
                if (idAlumno != 0)
                {
                    OportunidadBandejaCorreoDTO oportunidad = new OportunidadBandejaCorreoDTO();
                    string queryOportunidad = "com.SP_OportunidadPorAlumnoyAsesor";
                    var resultado = _dapperRepository.QuerySPFirstOrDefault(queryOportunidad, new { idAlumno, idPersonal });
                    if (resultado != "null" && resultado != "")
                    {
                        oportunidad = JsonConvert.DeserializeObject<OportunidadBandejaCorreoDTO>(resultado)!;

                        return oportunidad;
                    }
                    return null;
                }
                else
                {
                    OportunidadBandejaCorreoDTO oportunidad = new OportunidadBandejaCorreoDTO();
                    string queryOportunidad = "com.SP_OportunidadPorNumeroyAsesor";
                    var resultado = _dapperRepository.QuerySPFirstOrDefault(queryOportunidad, new { numero, idPersonal });
                    if (resultado != "null" && resultado != "")
                    {
                        oportunidad = JsonConvert.DeserializeObject<OportunidadBandejaCorreoDTO>(resultado)!;

                        return oportunidad;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidad de operaciones mediante los siguientes parametros
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public OportunidadOperacionesFiltroDTO? ObtenerOportunidadOperacionesPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                var query = @"SELECT 
                                Id, 
                                IdPersonal_Asignado, 
                                IdAlumno,
                                IdCentroCosto, 
                                IdPadre, 
                                IdFaseOportunidad,
                                IdPersonalAreaTrabajo, 
                                IdOportunidadClasificacionOperaciones
                            FROM com.V_TOportunidad_ObtenerOportunidadOperaciones
                            WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<OportunidadOperacionesFiltroDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el objeto oportunidadBO a partir de un string serializado
        /// </summary>
        /// <param name="oportunidadSerializada"></param>
        /// <returns></returns>
        public OportunidadDTO GenerarOportunidadOperacionesConParametros(int idOportunidad, string usuario, int idCentroCosto, int idActividadCabecera, int idPersonal, int idMatriculaCabecera)
        {
            try
            {
                string respuesta = "";
                string URI = $"https://integrav4-prepublicacion-servicios.bsginstitute.com/api/Oportunidad/GenerarOportunidadOperaciones/{idOportunidad}/{usuario}/{idCentroCosto}/{idActividadCabecera}/{idPersonal}/{idMatriculaCabecera}";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    respuesta = wc.DownloadString(URI);
                }
                return JsonConvert.DeserializeObject<OportunidadDTO>(respuesta)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina la oportunidad de operaciones en v3 y v4 fisicamente
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public bool EliminarOportunidadFisicaOperacionesV3V4(int idOportunidad)
        {
            try
            {
                var resultado = new Dictionary<string, bool>();
                string query = _dapperRepository.QuerySPFirstOrDefault("ope.SP_EliminarOportunidadOperacionesFisicamenteNuevoWebPhoneNuevoModelo", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
                }
                return resultado.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades en IS o M del mismo programa y del mismo alumno pero que no sea la misma oportunidad
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns> Lista de objetos de clase OportunidadISMDTO </returns>
        public List<OportunidadISMDTO> ValidarOportunidadesISM(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                var oportunidadesISM = new List<OportunidadISMDTO>();
                var queryAsesorById = @"SELECT IdOportunidad, IdProgramaGeneral, IdAlumno 
                                        FROM com.V_OportunidadesISM 
                                        WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral=@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapperRepository.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesISM = JsonConvert.DeserializeObject<List<OportunidadISMDTO>>(registrosBD) ?? new List<OportunidadISMDTO>();

                return oportunidadesISM;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 24/05/2023
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades en IS o M por alumno
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns> Lista de objetos de clase OportunidadISMDTO </returns>
        public bool ValidarOportunidadesISMPorIdAlumnoCelular(int idAlumno, string celular)
        {
            try
            {
                var query = @"SELECT TOP 1 IdOportunidad, IdProgramaGeneral, IdAlumno 
                                        FROM com.V_OportunidadesISM 
                                        WHERE (IdAlumno = @idAlumno OR Celular = @celular)";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAlumno, celular });
                return (!string.IsNullOrEmpty(resultado) && resultado != "null");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades con sus probabilidades del mismo programa y del mismo alumno que que no sea la misma oportunidad
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadProbabilidadDTO</returns>
        public List<OportunidadProbabilidadDTO> ValidarOportunidadesProbabilidad(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                var oportunidadesProbabilidad = new List<OportunidadProbabilidadDTO>();
                var query = @"SELECT IdOportunidad, IdProgramaGeneral, IdAlumno, PesoProbabilidad 
                                        FROM com.V_OportunidadesProbabilidades 
                                        WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdOportunidad <> @idOportunidad";
                var registrosBD = _dapperRepository.QueryDapper(query, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesProbabilidad = JsonConvert.DeserializeObject<List<OportunidadProbabilidadDTO>>(registrosBD);
                if (oportunidadesProbabilidad == null)
                {
                    oportunidadesProbabilidad = new List<OportunidadProbabilidadDTO>();
                }
                return oportunidadesProbabilidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno que que no sea la misma oportunidad
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoria(int idAlumno, int idOportunidad, string celular)
        {
            try
            {
                var oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var queryAsesorById = @"SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria 
                                        FROM com.V_OportunidadesMismoPGCategorias 
                                        WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapperRepository.QueryDapper(queryAsesorById, new { idAlumno, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno pero de diferente programa que no sea la misma oportunidad en fase IP
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaIPDiferentePG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                var oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesCategoriasIP WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral<>@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapperRepository.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno que que no sea la misma oportunidad en fase IP
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaIPMismoPG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                var oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesCategoriasIP WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral=@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapperRepository.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno pero de diferente programa que no sea la misma oportunidad en fase BNC IT RN
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaBNCITRNDiferentePG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                var oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var _queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesCategoriasBNCITRN WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral<>@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapperRepository.QueryDapper(_queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el flag de la validacion de la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad a actualizar el estado de la validacion (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="validacionCorrecta">Flag que determina si la validacion se completo exitosamente</param>
        /// <returns>Bool</returns>
        public bool ActualizarValidacionOportunidad(int idOportunidad, bool validacionCorrecta)
        {
            try
            {
                string spPeticion = "[com].[SP_ActualizarValidacionOportunidad]";
                string resultadoPeticion = _dapperRepository.QuerySPFirstOrDefault(spPeticion, new { IdOportunidad = idOportunidad, ValidacionCorrecta = validacionCorrecta });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Carlos Crispin.
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// Eliminar los cronoogramas asociados a la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad a actualizar</param>
        /// <returns>Bool</returns>
        public async Task EliminarCronogramaOportunidad(int idOportunidad)
        {
            try
            {
                string spPeticion = "[com].[SP_EliminarCronogramasIdOportunidad]";
                string resultadoPeticion = _dapperRepository.QuerySPFirstOrDefault(spPeticion, new { IdOportunidad = idOportunidad });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Carga una lista de oportunidades por id alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idClasificacionPersona"></param>
        /// <returns></returns>
        public List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidades(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                List<OportunidadVentaCruzadaDTO> oportunidadesVentaCruzada = new List<OportunidadVentaCruzadaDTO>();
                var oportunidadesVentaCruzadaDB = _dapperRepository.QuerySPDapper("com.SP_Oportunidad_VentaCruzadaTop5", new { idClasificacionPersona });

                if (!string.IsNullOrEmpty(oportunidadesVentaCruzadaDB) && !oportunidadesVentaCruzadaDB.Contains("[]"))
                {
                    oportunidadesVentaCruzada = JsonConvert.DeserializeObject<List<OportunidadVentaCruzadaDTO>>(oportunidadesVentaCruzadaDB)!;
                    if (oportunidadesVentaCruzada != null)
                    {
                        oportunidadesVentaCruzada = oportunidadesVentaCruzada.Where(x => x.Orden == 1).OrderByDescending(w => w.Costo).ToList();
                    }
                }
                return oportunidadesVentaCruzada;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Carga un historial de oportunidades por idAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idClasificacionPersona"></param>
        /// <returns></returns>
        public List<OportunidadHistorialDTO> CargarOportunidadHistorial(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                List<OportunidadHistorialDTO> oportunidadesHistorial = new List<OportunidadHistorialDTO>();
                var registrosBO = _dapperRepository.QuerySPDapper("com.SP_Oportunidad_HistorialOportunidades", new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    oportunidadesHistorial = JsonConvert.DeserializeObject<List<OportunidadHistorialDTO>>(registrosBO)!;
                }
                return oportunidadesHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idTipoSeguimientoAlumnoCategoria"></param>
        /// <returns></returns>
        public List<ObtenerSeguimientoAlumnoComentarioDTO> ObtenerComentariosOperaciones(int idOportunidad, int idTipoSeguimientoAlumnoCategoria)
        {
            try
            {
                List<ObtenerSeguimientoAlumnoComentarioDTO> Comentario = new List<ObtenerSeguimientoAlumnoComentarioDTO>();
                string _queryOportunidad = $@"
                        SELECT 
                            Id, IdSeguimientoAlumnoCategoria, SeguimientoAlumnoCategoria, Comentario, FechaCompromiso, NroCuota, 
                            NroSubCuota, FechaCreacion, UsuarioCreacion
                        FROM 
                            ope.V_ObtenerComentarioOperaciones
                        WHERE 
                            Estado = 1 AND IdOportunidad = @idOportunidad AND IdTipoSeguimientoAlumnoCategoria = @idTipoSeguimientoAlumnoCategoria
                            ORDER BY Id DESC";
                var queryOportunidad = _dapperRepository.QueryDapper(_queryOportunidad, new { idOportunidad, idTipoSeguimientoAlumnoCategoria });
                if (!queryOportunidad.Contains("[]"))
                {
                    Comentario = JsonConvert.DeserializeObject<List<ObtenerSeguimientoAlumnoComentarioDTO>>(queryOportunidad)!;
                }
                return Comentario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo pagos y academico
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idTipoSeguimientoAlumnoCategoria"></param>
        /// <returns></returns>
        public List<ObtenerSeguimientoPagosAlumnoComentarioDTO> ObtenerComentariosOperacionesPagosAcademicos(int idOportunidad)
        {
            try
            {
                List<ObtenerSeguimientoPagosAlumnoComentarioDTO> Comentario = new List<ObtenerSeguimientoPagosAlumnoComentarioDTO>();
                string _queryOportunidad = $@"
                        	SELECT CONVERT(DATE, FechaCreacion) AS Fecha,
                                   MAX(CONVERT(TIME, FechaCreacion)) AS HoraCreacionMaxima,
                                   STUFF(
                                   (
                                       SELECT '--' + UPPER(SeguimientoAlumnoCategoria) + ' -- ' + Comentario
                                       FROM ope.V_ObtenerComentarioOperaciones AS innerTable
                                       WHERE Estado = 1
                                             AND IdOportunidad = @idOportunidad
                                             AND (IdTipoSeguimientoAlumnoCategoria = 1)
                                             AND CONVERT(DATE, innerTable.FechaCreacion) = CONVERT(DATE, outerTable.FechaCreacion)
                                       FOR XML PATH('')
                                   ),
                                   1,
                                   2,
                                   ''
                                        ) AS ComentariosTipoPago,
                                   STUFF(
                                   (
                                       SELECT '--' + UPPER(SeguimientoAlumnoCategoria) + ' -- ' + Comentario
                                       FROM ope.V_ObtenerComentarioOperaciones AS innerTable
                                       WHERE Estado = 1
                                             AND IdOportunidad = @idOportunidad
                                             AND (IdTipoSeguimientoAlumnoCategoria = 2)
                                             AND CONVERT(DATE, innerTable.FechaCreacion) = CONVERT(DATE, outerTable.FechaCreacion)
                                       FOR XML PATH('')
                                   ),
                                   1,
                                   2,
                                   ''
                                        ) AS ComentariosTipoAcademico,
                            		MAX(outerTable.UsuarioCreacion) AS UsuarioCreacion
                            FROM ope.V_ObtenerComentarioOperaciones AS outerTable
                            WHERE Estado = 1
                                  AND IdOportunidad = @idOportunidad
                                  AND
                                  (
                                      IdTipoSeguimientoAlumnoCategoria = 1
                                      OR IdTipoSeguimientoAlumnoCategoria = 2
                                  )
                            GROUP BY CONVERT(DATE, FechaCreacion)
                            ORDER BY Fecha DESC;
";
                var queryOportunidad = _dapperRepository.QueryDapper(_queryOportunidad, new { idOportunidad });
                if (!queryOportunidad.Contains("[]"))
                {
                    Comentario = JsonConvert.DeserializeObject<List<ObtenerSeguimientoPagosAlumnoComentarioDTO>>(queryOportunidad)!;
                }
                return Comentario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidades filtrado por numero de alumno
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ValidarOportunidadWhatsAppDTO> ValidarOportunidadesWhatsApp(string numero, int idPGeneral)
        {
            try
            {
                List<ValidarOportunidadWhatsAppDTO> listaOportunidad = new List<ValidarOportunidadWhatsAppDTO>();
                string queryOportunidad = @"SELECT 
                                                FaseOportunidad,IdPgeneral,IdPersonal 
                                            FROM 
                                                [com].[V_TOportunidadValidarPorNumero] 
                                            WHERE 
                                                EstadoAlumno=1 AND Celular = @Numero AND FaseOportunidad in('BNC','IT','IP','PF','IC','IS','M','RN2') AND 
                                                EstadoPespecifico=1 ORDER BY FechaCreacion DESC";
                var query = _dapperRepository.QueryDapper(queryOportunidad, new { Numero = numero });
                var oportunidad = JsonConvert.DeserializeObject<List<ValidarOportunidadWhatsAppDTO>>(query);

                return oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        ///Repositorio: OportunidadRepositorio
        ///Autor: Edgar S.
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtener información por filtro de Oportunidades
        /// </summary>
        /// <param name="obj"> filtro de búsqueda </param>
        /// <param name="paginador"> paginador </param>
        /// <param name="filtroGrilla"> filtros de grilla </param>
        /// <param name="operadorComparacion"> Operadores de comparación </param>
        /// <returns> Lista de registros filtrados : ResultadoAsignacionManualFiltroTotalDTO </returns>
        public ResultadoAsignacionManualFiltroTotalDTO ObtenerPorFiltroPaginaManual(AsignacionManualOportunidadFiltroDTO obj, FiltroKendoGridDTO filter, List<OperadoresComparacionDTO> operadorComparacion)
        {
            try
            {
                var filtros = new object();
                string queryCondicion = string.Empty;
                string contacto = string.Empty;
                List<string> palabraContacto = new List<string>();
                string email1 = string.Empty;
                int flagVentaCruzada = 0;
                int usuarioModificacion = 0;
                string[] idFaseoportunidad = new string[6];
                string[] idArea = new string[6];
                string[] idSubArea = new string[6];
                string[] idPersonal = new string[6];
                string[] idCategoriaOrigen = new string[6];
                string[] idCentroCosto = new string[6];
                string[] idProbabilidad = new string[6];
                string[] idTipoDato = new string[6];
                string[] idPais = new string[6];
                string[] idPrograma = new string[6];
                string[] idTipoCategoriaOrigen = new string[6];

                // Filtros Grilla 
                string condicion = string.Empty;
                string centroCosto = string.Empty;
                string asesor = string.Empty;
                string tipoDato = string.Empty;
                string CodigoFase = string.Empty;
                string nombreFase = string.Empty;
                string email = string.Empty;
                string categoria = string.Empty;
                string nombreCampania = string.Empty;
                string contacto1 = string.Empty;
                string estadoOportunidad = string.Empty;
                string probabilidadActual = string.Empty;
                string nombreGrupo = string.Empty;
                string idAlumno = string.Empty;
                string areaCapacitacion = string.Empty;
                string subAreaCapacitacion = string.Empty;
                int diasSinContactoManhana = 0;
                string nombrePais = string.Empty;
                string usuarioModificacion2 = string.Empty;
                string areaFormacion = string.Empty;
                string cargo = string.Empty;
                string industria = string.Empty;
                string areaTrabajo = string.Empty;
                string probabilidadSegundo = string.Empty;
                string nombreSegundo = string.Empty;
                string fecharegistrocampania = string.Empty;
                string fechaCreacion = string.Empty;
                string fechaModificacion = string.Empty;

                if (filter.Filter != null)
                {

                    foreach (var item in filter.Filter.Filters)
                    {

                        //FILTROS
                        if (item.Field == "idAlumno" && item.Value.Contains(""))
                        {
                            condicion += " AND IdAlumno LIKE @IdAlumno ";
                            idAlumno = item.Value;
                        }
                        if (item.Field == "centroCosto" && item.Value.Contains(""))
                        {
                            condicion += " AND CentroCosto LIKE @CentroCosto ";
                            centroCosto = item.Value;
                        }
                        else if (item.Field == "asesor" && item.Value.Contains(""))
                        {
                            condicion += " AND Asesor LIKE @Asesor ";
                            asesor = item.Value;
                        }
                        else if (item.Field == "tipoDato" && item.Value.Contains(""))
                        {
                            condicion += " AND TipoDato LIKE @TipoDato ";
                            tipoDato = item.Value;
                        }
                        else if (item.Field == "codigoFase" && item.Value.Contains(""))
                        {
                            condicion += " AND CodigoFase LIKE @CodigoFase ";
                            CodigoFase = item.Value;
                        }
                        else if (item.Field == "contacto" && item.Value.Contains(""))
                        {
                            condicion += " AND Contacto LIKE @Contacto1 ";
                            contacto1 = item.Value;
                        }
                        else if (item.Field == "email" && item.Value.Contains(""))
                        {
                            condicion += " AND Email LIKE @Email ";
                            email = item.Value;
                        }
                        else if (item.Field == "categoria" && item.Value.Contains(""))
                        {
                            condicion += " AND Categoria LIKE @Categoria ";
                            categoria = item.Value;
                        }
                        else if (item.Field == "nombreCampania" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreCampania LIKE @NombreCampania ";
                            nombreCampania = item.Value;
                        }
                        else if (item.Field == "estadoOportunidad" && item.Value.Contains(""))
                        {
                            condicion += " AND EstadoOportunidad LIKE @EstadoOportunidad ";
                            estadoOportunidad = item.Value;
                        }
                        else if (item.Field == "probabilidadActual" && item.Value.Contains(""))
                        {
                            condicion += " AND ProbabilidadActual LIKE @ProbabilidadActual ";
                            probabilidadActual = item.Value;
                        }
                        else if (item.Field == "nombreGrupo" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreGrupo LIKE @NombreGrupo ";
                            nombreGrupo = item.Value;
                        }
                        else if (item.Field == "areaCapacitacion" && item.Value.Contains(""))
                        {
                            condicion += " AND AreaCapacitacion LIKE @AreaCapacitacion ";
                            areaCapacitacion = item.Value;
                        }
                        else if (item.Field == "subAreaCapacitacion" && item.Value.Contains(""))
                        {
                            condicion += " AND SubAreaCapacitacion LIKE @SubAreaCapacitacion ";
                            subAreaCapacitacion = item.Value;
                        }

                        else if (item.Field == "diasSinContactoManhana" && item.Value.Contains(""))
                        {
                            condicion += " AND DiasSinContactoManhana LIKE @DiasSinContactoManhana ";
                            diasSinContactoManhana = Int32.Parse(item.Value);
                        }

                        else if (item.Field == "nombrePais" && item.Value.Contains(""))
                        {
                            condicion += " AND NombrePais LIKE @NombrePais ";
                            nombrePais = item.Value;
                        }

                        else if (item.Field == "usuarioModificacion" && item.Value.Contains(""))
                        {
                            condicion += " AND UsuarioModificacion LIKE @UsuarioModificacion2 ";
                            usuarioModificacion2 = item.Value;
                        }

                        else if (item.Field == "areaFormacion" && item.Value.Contains(""))
                        {
                            condicion += " AND AreaFormacion LIKE @AreaFormacion ";
                            areaFormacion = item.Value;
                        }

                        else if (item.Field == "cargo" && item.Value.Contains(""))
                        {
                            condicion += " AND Cargo LIKE @Cargo ";
                            cargo = item.Value;
                        }
                        else if (item.Field == "industria" && item.Value.Contains(""))
                        {
                            condicion += " AND Industria LIKE @Industria ";
                            industria = item.Value;
                        }
                        else if (item.Field == "areaTrabajo" && item.Value.Contains(""))
                        {
                            condicion += " AND AreaTrabajo LIKE @AreaTrabajo ";
                            areaTrabajo = item.Value;
                        }
                        else if (item.Field == "probabilidadSegundo" && item.Value.Contains(""))
                        {
                            condicion += " AND ProbabilidadSegundo LIKE @ProbabilidadSegundo ";
                            probabilidadSegundo = item.Value;
                        }

                        else if (item.Field == "nombreSegundo" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreSegundo LIKE @NombreSegundo ";
                            nombreSegundo = item.Value;
                        }
                        else if (item.Field == "fechaRegistroCampania" && item.Value.Contains(""))
                        {
                            condicion += " AND CONVERT(VARCHAR(25), FechaRegistroCampania, 126) LIKE @FechaRegistroCampania ";
                            fecharegistrocampania = item.Value;
                        }
                        else if (item.Field == "fechaCreacion" && item.Value.Contains(""))
                        {
                            condicion += " AND CONVERT(VARCHAR(25), FechaCreacion, 126) LIKE @FechaCreacion ";
                            fechaCreacion = item.Value;
                        }
                        else if (item.Field == "fechaModificacion" && item.Value.Contains(""))
                        {
                            condicion += " AND CONVERT(VARCHAR(25), FechaModificacion, 126) LIKE @FechaModificacion ";
                            fechaModificacion = item.Value;
                        }

                    }
                }



                //Filtros Combos
                if (obj.Area != string.Empty)
                {
                    queryCondicion += "AND IdArea IN @IdArea ";
                    idArea = obj.Area.Split(",");
                }
                if (obj.subArea != string.Empty)
                {
                    queryCondicion += "AND IdSubArea IN @IdSubArea ";
                    idSubArea = obj.subArea.Split(",");
                }
                if (obj.FasesOportunidad != string.Empty)
                {
                    queryCondicion += "AND IdFaseOportunidad IN @IdFaseOportunidad ";
                    idFaseoportunidad = obj.FasesOportunidad.Split(",");
                }
                if (obj.CentrosCosto != string.Empty)
                {
                    queryCondicion += "AND IdCentroCosto IN @IdCentroCosto ";
                    idCentroCosto = obj.CentrosCosto.Split(",");
                }
                if (obj.Asesores != string.Empty)
                {
                    queryCondicion += "AND IdPersonal IN @IdPersonal ";
                    idPersonal = obj.Asesores.Split(",");
                }
                if (obj.Probabilidad != string.Empty)
                {
                    queryCondicion += "AND IdProbabilidad IN @IdProbabilidad ";
                    idProbabilidad = obj.Probabilidad.Split(",");
                }
                if (obj.Programa != string.Empty)
                {
                    queryCondicion += "AND IdPrograma IN @IdPrograma ";
                    idPrograma = obj.Programa.Split(",");
                }
                if (obj.Categorias != string.Empty)
                {
                    queryCondicion += "AND IdCategoriaOrigen IN @IdCategoriaOrigen ";
                    idCategoriaOrigen = obj.Categorias.Split(",");
                }
                if (obj.TiposDato != string.Empty)
                {
                    queryCondicion += "AND IdTipoDato IN @IdTipoDato ";
                    idTipoDato = obj.TiposDato.Split(",");
                }
                if (obj.Pais != string.Empty)
                {
                    queryCondicion += "AND IdPais IN @IdPais ";
                    idPais = obj.Pais.Split(",");
                }
                if (obj.TipoCategoriaOrigen != string.Empty)
                {
                    queryCondicion += "AND IdTipoCategoriaOrigen IN @IdTipoCategoriaOrigen ";
                    idTipoCategoriaOrigen = obj.TipoCategoriaOrigen.Split(",");
                }
                if (obj.contacto != string.Empty && obj.email == string.Empty)
                {
                    queryCondicion += "AND IdAlumno IN (SELECT Id FROM (SELECT ALU.Id, CONCAT(ALU.Nombre1, ' ', ALU.Nombre2, ' ', ALU.ApellidoPaterno, ' ', ALU.ApellidoMaterno) AS Contacto FROM mkt.T_Alumno AS ALU WITH (NOLOCK) WHERE ALU.Estado = 1) AS T0 WHERE T0.Contacto LIKE CONCAT('%',@Contacto,'%')) ";
                    contacto = Regex.Replace(obj.contacto.Trim(), @"\s+|\s", "%");
                }

                if (obj.email != string.Empty && obj.contacto == string.Empty)
                {
                    queryCondicion += "AND IdAlumno IN (SELECT Id FROM (SELECT ALU.Id, ALU.Email1 FROM mkt.T_Alumno AS ALU WITH (NOLOCK) WHERE ALU.Estado = 1) AS T0 WHERE T0.Email1 LIKE CONCAT('%',@Email1,'%')) ";
                    email1 = Regex.Replace(obj.email.Trim(), @"\s+|\s", "%");
                }

                if (obj.contacto != string.Empty && obj.email != string.Empty)
                {
                    queryCondicion += "AND IdAlumno IN (SELECT Id FROM (SELECT ALU.Id, CONCAT(ALU.Nombre1, ' ', ALU.Nombre2, ' ', ALU.ApellidoPaterno, ' ', ALU.ApellidoMaterno) AS Contacto, ALU.Email1 FROM mkt.T_Alumno AS ALU WITH (NOLOCK) WHERE ALU.Estado = 1) AS T0 WHERE T0.Contacto LIKE CONCAT('%',@Contacto,'%') AND T0.Email1 LIKE CONCAT('%',@Email1,'%')) ";
                    contacto = Regex.Replace(obj.contacto.Trim(), @"\s+|\s", "%");
                    email1 = Regex.Replace(obj.email.Trim(), @"\s+|\s", "%");
                }


                if (obj.UsuarioModificacion != string.Empty)
                {
                    queryCondicion += "AND PersonalModificacion = @UsuarioModificacion ";
                    usuarioModificacion = Int32.Parse(obj.UsuarioModificacion);
                }
                if (obj.ventaCruzada != string.Empty)
                {
                    queryCondicion += "AND FlagVentaCruzada = @FlagVentaCruzada ";
                    flagVentaCruzada = Int32.Parse(obj.ventaCruzada);
                }
                if (obj.FechaProgramacionInicio != null && obj.FechaProgramacionFin != null)
                {
                    queryCondicion += "AND UltimaFechaProgramada BETWEEN @FechaProgramacionInicio AND @FechaProgramacionFin ";
                    obj.FechaProgramacionInicio = new DateTime(obj.FechaProgramacionInicio.Value.Year, obj.FechaProgramacionInicio.Value.Month, obj.FechaProgramacionInicio.Value.Day, 0, 0, 0);
                    obj.FechaProgramacionFin = new DateTime(obj.FechaProgramacionFin.Value.Year, obj.FechaProgramacionFin.Value.Month, obj.FechaProgramacionFin.Value.Day, 23, 59, 59);
                }

                // Filtros por cantidad de oportunidades
                if (obj.NroOportunidades != null && obj.IdOperadorComparacionNroOportunidades != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroOportunidades.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroOportunidades " + simbolo + " " + obj.NroOportunidades;
                }
                if (obj.NroSolicitud != null && obj.IdOperadorComparacionNroSolicitud != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitud.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitud " + simbolo + " " + obj.NroSolicitud;
                }
                if (obj.NroSolicitudPorArea != null && obj.IdOperadorComparacionNroSolicitudPorArea != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorArea.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorArea " + simbolo + " " + obj.NroSolicitudPorArea;
                }
                if (obj.NroSolicitudPorSubArea != null && obj.IdOperadorComparacionNroSolicitudPorSubArea != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorSubArea.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorSubArea " + simbolo + " " + obj.NroSolicitudPorSubArea;
                }
                if (obj.NroSolicitudPorProgramaGeneral != null && obj.IdOperadorComparacionNroSolicitudPorProgramaGeneral != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorProgramaGeneral.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorProgramaGeneral " + simbolo + " " + obj.NroSolicitudPorProgramaGeneral;
                }
                if (obj.NroSolicitudPorProgramaEspecifico != null && obj.IdOperadorComparacionNroSolicitudPorProgramaEspecifico != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorProgramaEspecifico.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorProgramaEspecifico " + simbolo + " " + obj.NroSolicitudPorProgramaEspecifico;
                }

                if (obj.FechaFin == null || obj.FechaInicio == null)
                {
                    obj.FechaFin = DateTime.Now;
                    obj.FechaInicio = DateTime.Now;
                }
                obj.FechaFin = obj.FechaFin.Value.AddDays(1);
                string queryCampos = "IdAlumno,Id,IdCentroCosto,CentroCosto,IdPersonal,Asesor,IdTipoDato, TipoDato,IdFaseOportunidad,CodigoFase,IdOrigen,Contacto,Email,UsuarioModificacion,FechaRegistroCampania,Categoria,NombreGrupo,AreaFormacion,Cargo,Industria,AreaTrabajo,ProbabilidadSegundo,NombreSegundo,AreaCapacitacion,SubAreaCapacitacion,NombreCampania,FechaCreacion,FechaModificacion,UltimaFechaProgramada,IdEstadoOportunidad,EstadoOportunidad,ProbabilidadActual, NroOportunidades, NroSolicitud, NroSolicitudPorArea, NroSolicitudPorSubArea, NroSolicitudPorProgramaGeneral, NroSolicitudPorProgramaEspecifico, DiasSinContactoManhana, DiasSinContactoTarde, NombrePais,FechaAsignacion";


                //ORDER BY
                var ordenamiento = " ORDER BY ";

                if (filter.Sort != null && filter.Sort.Count() > 0)
                {
                    ordenamiento += string.Join(", ", filter.Sort.Select(w => w.Field + " " + w.Dir).ToList());
                }
                else
                {
                    ordenamiento += " FECHACREACION DESC ";
                }

                ResultadoAsignacionManualFiltroTotalDTO resultado = new ResultadoAsignacionManualFiltroTotalDTO();

                //SKIP TAKE
                if (filter != null && filter.Take != 0)
                {
                    string queryOportunidadConsulta = "SELECT " + queryCampos + " FROM com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + ordenamiento + " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                    //si le di all y la cantidad vario de datos // esto cambia si en la paginacion le doy una opcion mayor a 100
                    if (filter.Take > 100)
                    {
                        var cantidadRegistrostemp = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count(*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, Email1 = email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, idAlumno, CodigoFase = "%" + CodigoFase + "%", diasSinContactoManhana, NombrePais = "%" + nombrePais + "%", FechaRegistroCampania = "%" + fecharegistrocampania + "%", FechaCreacion = "%" + fechaCreacion + "%", FechaModificacion = "%" + fechaModificacion + "%", UsuarioModificacion2 = "%" + usuarioModificacion2 + "%", AreaFormacion = "%" + areaFormacion + "%", Cargo = "%" + cargo + "%", Industria = "%" + industria + "%", AreaTrabajo = "%" + areaTrabajo + "%", ProbabilidadSegundo = "%" + probabilidadSegundo + "%", NombreSegundo = "%" + nombreSegundo + "%" }));
                        var nuevacantidad = cantidadRegistrostemp.Select(w => w.Value).FirstOrDefault();
                        if (nuevacantidad == 0)
                        {
                            filter.Take = filter.Take;
                        }
                        else
                        {
                            filter.Take = nuevacantidad;
                        }

                    }

                    var queryOportunidad = _dapperRepository.QueryDapper(queryOportunidadConsulta, new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, Email1 = email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, idAlumno, CodigoFase = "%" + CodigoFase + "%", diasSinContactoManhana, NombrePais = "%" + nombrePais + "%", FechaRegistroCampania = "%" + fecharegistrocampania + "%", FechaCreacion = "%" + fechaCreacion + "%", FechaModificacion = "%" + fechaModificacion + "%", UsuarioModificacion2 = "%" + usuarioModificacion2 + "%", AreaFormacion = "%" + areaFormacion + "%", Cargo = "%" + cargo + "%", Industria = "%" + industria + "%", AreaTrabajo = "%" + areaTrabajo + "%", ProbabilidadSegundo = "%" + probabilidadSegundo + "%", NombreSegundo = "%" + nombreSegundo + "%", Skip = filter.Skip, Take = filter.Take });
                    var rpta = JsonConvert.DeserializeObject<List<ResultadoAsignacionManualFiltroDTO>>(queryOportunidad);
                    var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count(*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, Email1 = email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, idAlumno, CodigoFase = "%" + CodigoFase + "%", diasSinContactoManhana, NombrePais = "%" + nombrePais + "%", FechaRegistroCampania = "%" + fecharegistrocampania + "%", FechaCreacion = "%" + fechaCreacion + "%", FechaModificacion = "%" + fechaModificacion + "%", UsuarioModificacion2 = "%" + usuarioModificacion2 + "%", AreaFormacion = "%" + areaFormacion + "%", Cargo = "%" + cargo + "%", Industria = "%" + industria + "%", AreaTrabajo = "%" + areaTrabajo + "%", ProbabilidadSegundo = "%" + probabilidadSegundo + "%", NombreSegundo = "%" + nombreSegundo + "%" }));


                    resultado.data = rpta;
                    resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }
                else
                {
                    string queryOportunidadConsulta = "SELECT " + queryCampos + " FROM com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin " + queryCondicion + ordenamiento;
                    var queryOportunidad = _dapperRepository.QueryDapper(queryOportunidadConsulta, new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, idAlumno, CodigoFase = "%" + CodigoFase + "%", diasSinContactoManhana, NombrePais = "%" + nombrePais + "%", FechaRegistroCampania = "%" + fecharegistrocampania + "%", FechaCreacion = "%" + fechaCreacion + "%", FechaModificacion = "%" + fechaModificacion + "%", UsuarioModificacion2 = "%" + usuarioModificacion2 + "%", AreaFormacion = "%" + areaFormacion + "%", Cargo = "%" + cargo + "%", Industris = "%" + industria + "%", AreaTrabajo = "%" + areaTrabajo + "%", ProbabilidadSegundo = "%" + probabilidadSegundo + "%", NombreSegundo = "%" + nombreSegundo + "%" });
                    var rpta = JsonConvert.DeserializeObject<List<ResultadoAsignacionManualFiltroDTO>>(queryOportunidad);
                    var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count (*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, idAlumno, CodigoFase = "%" + CodigoFase + "%", diasSinContactoManhana, NombrePais = "%" + nombrePais + "%", FechaRegistroCampania = "%" + fecharegistrocampania + "%", FechaCreacion = "%" + fechaCreacion + "%", FechaModificacion = "%" + fechaModificacion + "%", UsuarioModificacion2 = "%" + usuarioModificacion2 + "%", AreaFormacion = "%" + areaFormacion + "%", Cargo = "%" + cargo + "%", Industris = "%" + industria + "%", AreaTrabajo = "%" + areaTrabajo + "%", ProbabilidadSegundo = "%" + probabilidadSegundo + "%", NombreSegundo = "%" + nombreSegundo + "%" }));

                    resultado.data = rpta;
                    resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }

                return resultado;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el asesor asignado segun el Programa General
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public ResultadosDTO ObtenerIdPersonalAsignadoChat(int idAlumno, int idCentroCosto)
        {
            ResultadosDTO asesorChat = new ResultadosDTO();
            var registrosBD = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerIdAsignadoChat", new { idAlumno, idCentroCosto });

            asesorChat = JsonConvert.DeserializeObject<ResultadosDTO>(registrosBD);
            return asesorChat;
        }


        /// Autor: Margiory ramirez Neyra
        /// Fecha: 24/11/2022
        /// <summary>
        /// Obtiene la lista de asesores candidatos para asignarles la oportunidad
        /// </summary>
        /// <param name="idCentroCosto">Id del centro de costo a analizar de la oportunidad entrante (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="idSubCategoriaDato">Id de la subcategoria dato de la oportunidad entrante(PK de la tabla mkt.T_SubCategoriaDato)</param>
        /// <param name="idPais">Id del pais de la oportunidad entrante (PK de la tabla conf.T_Pais)</param>
        /// <param name="probabilidad">Id de la probabilidad de la oportunidad entrante(PK de la tabla mkt.T_ProbabilidadRegistro_PW)</param>
        /// <param name="prioridad">Prioridad a analizar en el flujo (Configuracion de la interfaz de configuracion de asignacion automatica)</param>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaAsesorPosibilidadDTO</returns>
        public List<AsignacionAutomaticaAsesorPosibilidadDTO> ObtenerAsesorParaCentroCosto(int idCentroCosto, int idSubCategoriaDato, int idPais, int probabilidad, int prioridad)
        {
            try
            {
                List<AsignacionAutomaticaAsesorPosibilidadDTO> oportunidadesHistorial = new List<AsignacionAutomaticaAsesorPosibilidadDTO>();
                var registrosBO = _dapperRepository.QuerySPDapper("com.SP_ObtenerAsesorAsignacionAutomaticaAsesorCentroCosto", new { idCentroCosto, idSubCategoriaDato, idPais, probabilidad, prioridad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    oportunidadesHistorial = JsonConvert.DeserializeObject<List<AsignacionAutomaticaAsesorPosibilidadDTO>>(registrosBO);
                }
                return oportunidadesHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="textoaBuscar"></param>
        /// <param name="tipo"></param>
        /// <returns> OportunidadTabAgendaDTO </returns>
        public OportunidadTabAgendaDTO ObtenerClasificacionTabAgenda(int idPersonal, string textoaBuscar, int tipo)
        {
            try
            {
                OportunidadTabAgendaDTO clasificacion = new OportunidadTabAgendaDTO();
                if (tipo == 1)
                {
                    string _query = $@"Ope.SP_ObtenerClasificacionTabAgenda";
                    var resultado = _dapperRepository.QuerySPFirstOrDefault(_query, new { idPersonal, textoaBuscar });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                    {
                        clasificacion = JsonConvert.DeserializeObject<OportunidadTabAgendaDTO>(resultado)!;
                    }
                }
                else
                {
                    string _query = $@"Ope.SP_ObtenerClasificacionTabAgendaporDNI";
                    var resultado = _dapperRepository.QuerySPFirstOrDefault(_query, new { idPersonal, textoaBuscar });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                    {
                        clasificacion = JsonConvert.DeserializeObject<OportunidadTabAgendaDTO>(resultado)!;
                    }
                }
                return clasificacion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="textoaBuscar"></param>
        /// <param name="tipo"></param>
        /// <returns> OportunidadTabAgendaDTO </returns>
        public OportunidadTabAgendaFichaDTO ObtenerClasificacionTabAgendaFicha(int idPersonal, int idMatriculaCabecera, int tipo)
        {
            try
            {
                OportunidadTabAgendaFichaDTO clasificacion = new OportunidadTabAgendaFichaDTO();

                string _query = $@"ope.SP_ObtenerTabAgendaAlumno";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(_query, new { idPersonal, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    clasificacion = JsonConvert.DeserializeObject<OportunidadTabAgendaFichaDTO>(resultado)!;
                }
                return clasificacion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 24/2/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <param name="Paginador"></param>
        /// <returns> OportunidadTabAgendaDTO </returns>
        public object ObtenerPorFiltroRevertirFase(RevertirFaseFiltroDTO obj, PaginadorDTO paginador, List<GridFilterDTO> filter)
        {
            try
            {
                var total = 0;
                var filtros = new object();
                string _queryCondicion = "";

                string Contacto = "";
                string[] IdFaseoportunidad = new string[6];
                string[] IdPersonal = new string[6];
                string[] IdOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdTipoDato = new string[6];
                string Oportunidad = "";
                string Asesor = "";
                string TipoDato = "";
                string FaseOportunidad = "";
                string Origen = "";
                string Alumno = "";



                if (filter != null)
                {
                    foreach (var item in filter)
                    {

                        if (item.Field == "fase" && item.Value.Contains(""))
                        {
                            _queryCondicion += " AND FaseOportunidad LIKE @FaseOportunidad ";
                            FaseOportunidad = item.Value;
                        }
                        else if (item.Field == "oportunidad" && item.Value.Contains(""))
                        {
                            _queryCondicion += " AND Oportunidad LIKE @Oportunidad ";
                            Oportunidad = item.Value;
                        }
                        else if (item.Field == "asesor" && item.Value.Contains(""))
                        {
                            _queryCondicion += " AND Asesor LIKE @Asesor ";
                            Asesor = item.Value;
                        }
                        else if (item.Field == "tipoDato" && item.Value.Contains(""))
                        {
                            _queryCondicion += " AND TipoDato LIKE @TipoDato ";
                            TipoDato = item.Value;
                        }
                        else if (item.Field == "origen" && item.Value.Contains(""))
                        {
                            _queryCondicion += " AND Origen LIKE @Origen ";
                            Origen = item.Value;
                        }
                        else if (item.Field == "alumno" && item.Value.Contains(""))
                        {
                            _queryCondicion += " AND Alumno LIKE @Alumno ";
                            Alumno = item.Value;
                        }

                    }
                }


                if (obj.FaseOportunidad != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdFaseOportunidad in @IdFaseOportunidad ";
                    IdFaseoportunidad = obj.FaseOportunidad.Split(",");
                }
                if (obj.Oportunidad != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdCentroCosto in @IdCentroCosto ";
                    IdCentroCosto = obj.Oportunidad.Split(",");
                }
                if (obj.Asesor != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdPersonal in @IdPersonal ";
                    IdPersonal = obj.Asesor.Split(",");
                }
                if (obj.Origen != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdOrigen in @IdOrigen ";
                    IdOrigen = obj.Origen.Split(",");
                }
                if (obj.TipoDato != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdTipoDato in @IdTipoDato ";
                    IdTipoDato = obj.TipoDato.Split(",");
                }

                if (obj.Alumno != "")
                {
                    _queryCondicion = _queryCondicion + "AND Alumno like CONCAT('%',@Contacto,'%')";
                    Contacto = obj.Alumno;
                }

                string _queryCampos = " Id,Oportunidad,Asesor,TipoDato,FaseOportunidad,Origen,Alumno, IdFaseOportunidad, IdCentroCosto, IdPersonal, IdOrigen, IdTipoDato, IdAlumno";

                if (paginador != null && paginador.take != 0)
                {
                    string _queryOportunidad = "Select " + _queryCampos + " from com.V_ObtenerOportunidad_RevertirFase where Estado = 1 " + _queryCondicion + " order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                    var queryOportunidad = _dapperRepository.QueryDapper(_queryOportunidad, new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato, Oportunidad, Asesor, TipoDato, FaseOportunidad, Origen, Alumno, Skip = paginador.skip, Take = paginador.take });
                    var rpta = JsonConvert.DeserializeObject<List<RevertirFaseFiltroDTO>>(queryOportunidad);
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count(*) From com.V_ObtenerOportunidad_RevertirFase where Estado = 1 " + _queryCondicion + "", new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato, Oportunidad, Asesor, TipoDato, FaseOportunidad, Origen, Alumno }));

                    return new { data = rpta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                }
                else
                {
                    string _queryOportunidad = "Select " + _queryCampos + " from com.V_ObtenerOportunidad_RevertirFase where Estado = 1 " + _queryCondicion + " order by FechaCreacion desc";
                    var queryOportunidad = _dapperRepository.QueryDapper(_queryOportunidad, new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato, Oportunidad, Asesor, TipoDato, FaseOportunidad, Origen, Alumno, });
                    var rpta = JsonConvert.DeserializeObject<List<RevertirFaseFiltroDTO>>(queryOportunidad);
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count (*) From com.V_ObtenerOportunidad_RevertirFase where Estado = 1" + _queryCondicion + "", new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato, Oportunidad, Asesor, TipoDato, FaseOportunidad, Origen, Alumno }));

                    return new { data = rpta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idPersonal por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public bool TienePersonalOperaciones(int idOportunidad)
        {
            try
            {
                List<IdDTO> listaId = new List<IdDTO>();
                string _query = $@"
                            SELECT Personal.Id AS Id
                            FROM fin.T_MatriculaCabecera AS MatriculaCabecera
                                 INNER JOIN com.T_MontoPagoCronograma AS MontoPagoCronograma ON MatriculaCabecera.IdCronograma = MontoPagoCronograma.Id
                                 INNER JOIN com.T_Oportunidad AS Oportunidad ON MontoPagoCronograma.IdOportunidad = Oportunidad.Id
                                 INNER JOIN conf.T_Integra_AspNetUsers AS AspNetUsers ON AspNetUsers.UserName = MatriculaCabecera.UsuarioCoordinadorAcademico
                                 INNER JOIN gp.T_Personal AS Personal ON Personal.Id = AspNetUsers.PerId
                            WHERE MatriculaCabecera.Estado = 1
                                  AND MontoPagoCronograma.Estado = 1
                                  AND Oportunidad.Estado = 1
                                  AND MatriculaCabecera.UsuarioCoordinadorAcademico <> '0'
                                  AND MontoPagoCronograma.IdOportunidad = @idOportunidad
                                  AND Personal.Activo = 1";
                var resultado = _dapperRepository.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdDTO>>(resultado)!;
                }
                if (listaId.Count() == 0)
                {
                    return false;
                }
                else if (listaId.Count() >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }




        public Oportunidad ObtenerOportunidadPorId(int idOportunidad)
        {

            try
            {
                Oportunidad faseOportunidad = new Oportunidad();
                var query = @"
                    SELECT *
                    FROM com.V_ObtenerOportunidadCodigoFase
                    WHERE IdOportunidad = @idOportunidad";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (resultadoQuery == "null") throw new Exception("Error: No se encontraron datos de la oportunidad: " + idOportunidad);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    faseOportunidad = JsonConvert.DeserializeObject<Oportunidad>(resultadoQuery);
                }
                return faseOportunidad;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idPersonal por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public IdDTO ObtenerIdPersonalOperaciones(int idOportunidad)
        {
            try
            {
                List<IdDTO> listaId = new List<IdDTO>();
                string _query = $@"
                            SELECT Personal.Id AS Id
                            FROM fin.T_MatriculaCabecera AS MatriculaCabecera
                                 INNER JOIN com.T_MontoPagoCronograma AS MontoPagoCronograma ON MatriculaCabecera.IdCronograma = MontoPagoCronograma.Id
                                 INNER JOIN com.T_Oportunidad AS Oportunidad ON MontoPagoCronograma.IdOportunidad = Oportunidad.Id
                                 INNER JOIN conf.T_Integra_AspNetUsers AS AspNetUsers ON AspNetUsers.UserName = MatriculaCabecera.UsuarioCoordinadorAcademico
                                 INNER JOIN gp.T_Personal AS Personal ON Personal.Id = AspNetUsers.PerId
                            WHERE MatriculaCabecera.Estado = 1
                                  AND MontoPagoCronograma.Estado = 1
                                  AND Oportunidad.Estado = 1
                                  AND MatriculaCabecera.UsuarioCoordinadorAcademico <> '0'
                                  AND MontoPagoCronograma.IdOportunidad = @idOportunidad
                                  AND Personal.Activo = 1";
                var resultado = _dapperRepository.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdDTO>>(resultado)!;
                }
                if (listaId.Count() < 1)
                {
                    throw new Exception("No existe id");
                }
                return listaId.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por la ActividadDetalle
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns> DatosOportunidadDocumentosCompuestoDTO </returns>
        public DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorActividades(int idActividadDetalle)
        {
            try
            {
                var datoOportunidad = new DatosOportunidadDocumentosCompuestoDTO();
                string queryOportunidad = @"
                                            SELECT 
                                                Id, IdCentroCosto, IdPersonal_Asignado as IdPersonalAsignado, IdTipoDato, IdFaseOportunidad, IdOrigen, CodigoPagoIC, IdAlumno, UltimoComentario,
                                                IdActividadDetalle_Ultima as IdActividadDetalleUltima, IdActividadCabecera_Ultima as IdActividadCabeceraUltima, IdEstadoActividadDetalle_UltimoEstado as IdEstadoActividadDetalleUltimoEstado, UltimaFechaProgramada,
                                                IdEstadoOportunidad, IdEstadoOcurrencia_Ultimo as IdEstadoOcurrenciaUltimo, IdFaseOportunidad_Maxima as IdFaseOportunidadMaxima, IdFaseOportunidad_Inicial as IdFaseOportunidadInicial, IdCategoriaOrigen,
                                                NombrePatner, EncabezadoCorreoPartner, IdActividadDetalle, PrecioContado, NombreProgramaGeneral, pw_duracion, UrlVersion,
                                                IdCategoriaPrograma, UrlBrochurePrograma, FechaEnvio, Central, Anexo3CX, UrlFirmaCorreos, Email, IdPgeneral, IdCodigoPais 
                                            FROM 
                                                com.V_OportunidadCompuesto 
                                            WHERE 
                                                IdActividadDetalle = @IdActividadDetalle";
                var oportunidad = _dapperRepository.FirstOrDefault(queryOportunidad, new { IdActividadDetalle = idActividadDetalle });
                if (!string.IsNullOrEmpty(oportunidad) && !oportunidad.Equals("null"))
                {
                    datoOportunidad = JsonConvert.DeserializeObject<DatosOportunidadDocumentosCompuestoDTO>(oportunidad)!;
                }
                return datoOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Oportunidades de Operaciones
        /// </summary>
        /// <param name="paginador">paginador</param>
        /// <param name="filtro"> Filtro Modulo </param>
        /// <param name="filterGrid"> filtros de grilla </param>        
        /// <returns> ResultadoFiltroAsignacionOportunidadDTO </returns>
        public ResultadoFiltroAsignacionOportunidadDTO ObtenerPorFiltroPaginaManualOperaciones(PaginadorDTO paginador, AsignacionManualOportunidadOperacionesFiltroDTO filtro, GridFiltersDTO filterGrid, List<string> listaCodigoMatricula)
        {
            try
            {
                if (filterGrid != null)
                {
                    foreach (var item in filterGrid.Filters)
                    {
                        if (item.Field == "Email" && item.Value.Contains(""))
                        {
                            filtro.Email = item.Value.Trim();
                        }
                        else if (item.Field == "CodigoMatricula" && item.Value.Contains(""))
                        {
                            filtro.CodigoMatricula = item.Value.Trim();
                        }
                    }
                }

                ResultadoFiltroAsignacionOportunidadDTO obj = new ResultadoFiltroAsignacionOportunidadDTO();

                if (filtro.CodigoMatricula != "" && filtro.CodigoMatricula != null)
                {
                    listaCodigoMatricula.Add(filtro.CodigoMatricula);
                }
                var filtrosV2 = new
                {
                    ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x)),
                    ListaCentroCosto = filtro.ListaCentroCosto == null ? "" : string.Join(",", filtro.ListaCentroCosto.Select(x => x)),
                    ListaEstados = filtro == null ? "" : string.Join(",", filtro.ListaEstados.Select(x => x)),
                    ListaSubEstados = filtro.ListaSubestados == null ? "" : string.Join(",", filtro.ListaSubestados.Select(x => x)),
                    Email = filtro.Email,
                    CodigoMatricula = listaCodigoMatricula == null ? "" : string.Join(",", listaCodigoMatricula.Select(x => x)),
                    Modalidad = filtro.ListaModalidad == null ? "" : string.Join(",", filtro.ListaModalidad.Select(x => x)),
                    Skip = paginador.skip,
                    Take = paginador.take,
                    Cantidad = false,
                };
                var filtros2V2 = new
                {
                    ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x)),
                    ListaCentroCosto = filtro.ListaCentroCosto == null ? "" : string.Join(",", filtro.ListaCentroCosto.Select(x => x)),
                    ListaEstados = filtro.ListaEstados == null ? "" : string.Join(",", filtro.ListaEstados.Select(x => x)),
                    ListaSubEstados = filtro.ListaSubestados == null ? "" : string.Join(",", filtro.ListaSubestados.Select(x => x)),
                    Email = filtro.Email,
                    CodigoMatricula = listaCodigoMatricula == null ? "" : string.Join(",", listaCodigoMatricula.Select(x => x)),
                    Modalidad = filtro.ListaModalidad == null ? "" : string.Join(",", filtro.ListaModalidad.Select(x => x)),
                    Skip = paginador.skip,
                    Take = paginador.take,
                    Cantidad = true,
                };

                string queryOportunidad = "com.[SP_ObtenerOportunidadesOperaciones_AsignacionManualV5]";
                var resQueryOportunidad = _dapperRepository.QuerySPDapper(queryOportunidad, filtrosV2);
                var queryCantidad = _dapperRepository.QuerySPFirstOrDefault(queryOportunidad, filtros2V2);
                var respuesta = JsonConvert.DeserializeObject<List<AsignacionManualOportunidadOperacionesDTO>>(resQueryOportunidad);
                var total = JsonConvert.DeserializeObject<TotalOportunidadAsignacionManualOperacionesDTO>(queryCantidad);

                obj.Lista = respuesta;
                obj.Total = total.Cantidad;
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Email de Personal y alumno por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: EmailPersonalAlumnoDTO </returns>
        public EmailPersonalAlumnoDTO ObtenerEmailPorOportunidad(int idOportunidad)
        {
            try
            {
                EmailPersonalAlumnoDTO emails = new EmailPersonalAlumnoDTO();
                var query = @"
                            SELECT 
                                EmailPersonal,EmailAlumno 
                            FROM 
                                [com].[V_TOportunidad_EmailPersonalAlumno] 
                            WHERE 
                                Id = @IdOportunidad";
                var ressultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(ressultado) && !ressultado.ToUpper().Contains("NULL"))
                {
                    emails = JsonConvert.DeserializeObject<EmailPersonalAlumnoDTO>(ressultado)!;
                }
                return emails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de oportunidades para reporte
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> List<OportunidadVentaCruzadaDTO> </returns>
        public List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidadesReporte(int idAlumno)
        {
            try
            {
                var oportunidadesVentaCruzada = new List<OportunidadVentaCruzadaDTO>();
                var oportunidadesVentaCruzadaDB = _dapperRepository.QuerySPDapper("com.SP_Oportunidad_VentaCruzadaTop5Reporte", new { idAlumno });

                if (!string.IsNullOrEmpty(oportunidadesVentaCruzadaDB) && !oportunidadesVentaCruzadaDB.Contains("[]"))
                {
                    oportunidadesVentaCruzada = JsonConvert.DeserializeObject<List<OportunidadVentaCruzadaDTO>>(oportunidadesVentaCruzadaDB);
                    if (oportunidadesVentaCruzada != null)
                    {
                        oportunidadesVentaCruzada = oportunidadesVentaCruzada.Where(x => x.Orden == 1).ToList();
                    }
                }
                return oportunidadesVentaCruzada;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Carga una lista de problemas cliente por idOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<OportunidadProblemaClienteDTO> ObtenerOportunidadProblemasCliente(int idOportunidad)
        {
            try
            {
                var listaOportunidadProblemasCliente = new List<OportunidadProblemaClienteDTO>();
                var problemasClienteDB = _dapperRepository.QuerySPDapper("com.SP_Oportunidad_ProblemasCliente", new { idOportunidad });
                listaOportunidadProblemasCliente = JsonConvert.DeserializeObject<List<OportunidadProblemaClienteDTO>>(problemasClienteDB);
                return listaOportunidadProblemasCliente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion complementaria para el Reporte de Seguimiento
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> ReporteSeguimientoOportunidadComplementosDTO </returns>
        public ReporteSeguimientoOportunidadComplementosDTO ObtenerInformacionComplementariaReporteSeguimiento(int idOportunidad)
        {
            try
            {
                var complementosDTO = new ReporteSeguimientoOportunidadComplementosDTO();
                var _query = @"SELECT ProbabilidadActual, CodigoFase, CategoriaOrigen, CentroCosto,EstadoMatricula,SubEstadoMatricula,IdCentroCosto,IdPersonalAsignado, alu.Celular
                                FROM com.V_ObtenerComplementosReporteSeguimientoOportundidad AS vista LEFT JOIN mkt.T_Alumno AS alu WITH (NOLOCK) ON vista.IdAlumno=alu.Id WHERE IdOportunidad = @idOportunidad";
                var queryRespuesta = _dapperRepository.FirstOrDefault(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(queryRespuesta) && queryRespuesta != "null")
                {
                    complementosDTO = JsonConvert.DeserializeObject<ReporteSeguimientoOportunidadComplementosDTO>(queryRespuesta);
                }
                return complementosDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 28/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las oportunidades anteriores de un Alumno
        /// </summary>
        /// <returns> List<OportunidadPendienteWhatsappDTO> </returns>
        public List<OportunidadPendienteWhatsappDTO> ObtenerOportunidadesPendientesEnvioWhatsapp()
        {
            try
            {
                List<OportunidadPendienteWhatsappDTO> oportunidadesPendientes = new List<OportunidadPendienteWhatsappDTO>();
                var _query = @"SELECT  IdOportunidad,
                                        IdPersonal,
                                        IdCategoriaOrigen
                                FROM com.V_OportunidadesPendientesWhatsappAsignacion";
                var respuestaQuery = _dapperRepository.QueryDapper(_query, null);
                oportunidadesPendientes = JsonConvert.DeserializeObject<List<OportunidadPendienteWhatsappDTO>>(respuestaQuery);
                return oportunidadesPendientes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 28/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las oportunidades anteriores de un Alumno
        /// </summary>
        /// <param name="idAlumno"> Id del almuno </param> 
        /// <returns> List<OportunidadAnteriorDTO> </returns>
        public List<OportunidadAnteriorDTO> ObtenerOportunidadesAnterioresAlumno(int idAlumno)
        {
            try
            {
                List<OportunidadAnteriorDTO> oportunidadesAnteriores = new List<OportunidadAnteriorDTO>();
                var _query = @"SELECT  IdOportunidad,
                                        FaseOportunidad,
                                        FechaCreacion,
                                        CentroCosto,
                                        TipoDato,
                                        CategoriaOrigen,
                                        IdAlumno,
                                        EstadoOportunidad
                                FROM com.V_ObtenerOportunidadesReporteSeguimiento 
                                WHERE IdAlumno = @idAlumno AND EstadoOportunidad = 1 ORDER BY FechaCreacion DESC";
                var respuestaQuery = _dapperRepository.QueryDapper(_query, new { idAlumno });
                oportunidadesAnteriores = JsonConvert.DeserializeObject<List<OportunidadAnteriorDTO>>(respuestaQuery);
                return oportunidadesAnteriores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los dias de reprogramacion manual
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: DatosOportunidadReprogramacionManualOperacionesDTO </returns>
        public DatosOportunidadReprogramacionManualOperacionesDTO ObtenerCasosReprogramacionManualOperacionesAlterno(int idOportunidad)
        {
            try
            {
                DatosOportunidadReprogramacionManualOperacionesDTO resultado = new DatosOportunidadReprogramacionManualOperacionesDTO();
                var registrosBD = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerCasosReprogramacionManualOperacionesAlterno", new { idOportunidad });
                if (!string.IsNullOrEmpty(registrosBD) && registrosBD != "null")
                {
                    resultado = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionManualOperacionesDTO>(registrosBD)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos necesarios de la oportunidad para calcualr la fecha de programacion Automatica
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: DatosOportunidadReprogramacionAutomaticaDTO </returns>
        public DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramcionAutomatica(int idOportunidad)
        {
            try
            {
                DatosOportunidadReprogramacionAutomaticaDTO oportunidad = new DatosOportunidadReprogramacionAutomaticaDTO();
                string queryOportunidad = @"
                                           SELECT 
                                                IdPersonalAsignado,IdActividadCabeceraUltima,IdTipoDato,IdCategoriaOrigen 
                                           FROM 
                                                com.V_TOportunidad_FechaProgramacionAutomatica 
                                           WHERE 
                                                Id = @IdOportunidad";
                var query = _dapperRepository.FirstOrDefault(queryOportunidad, new { idOportunidad });
                if (!query.Equals("null"))
                {
                    oportunidad = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionAutomaticaDTO>(query)!;
                    return oportunidad;
                }
                else
                {
                    throw new Exception("No Existe Oportunidad con Identificador " + idOportunidad);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO </returns>
        public DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO ObtenerCalculoReprogramaciones(int idOportunidad)
        {
            try
            {
                DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO Resultado = new DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO();
                var registrosBD = _dapperRepository.QuerySPFirstOrDefault("ope.SP_CalculoContactoAlumno", new { idOportunidad });

                Resultado = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO>(registrosBD);
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO </returns>
        public DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO ObtenerSubEstadoAlumno(int idOportunidad)
        {
            try
            {
                DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO Resultado = new DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO();
                var registrosBD = _dapperRepository.QuerySPFirstOrDefault("ope.SP_CalculoSubestadoAlumno", new { idOportunidad });
                Resultado = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO>(registrosBD);
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosAlumnoDTO </returns>
        public DatosAlumnoDTO ObtenerDatosAlumno(int idOportunidad)
        {
            try
            {
                var Resultado = new DatosAlumnoDTO();
                var registrosBD = _dapperRepository.QuerySPFirstOrDefault("ope.SP_DatosAlumnoCorreo", new { idOportunidad });
                Resultado = JsonConvert.DeserializeObject<DatosAlumnoDTO>(registrosBD);
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosAlumnoDTO </returns>
        public DatosAlumnoOportunidadDTO ObtenerDatosOportunidadAlumno(int idAlumno)
        {
            try
            {
                string _queryOportunidad = @"SELECT
                                                IdOportunidad
	                                            ,CodigoMatricula
	                                            ,Id
	                                            ,	IdCodigoPais
	                                            ,Celular
	                                            ,Nombre1
	                                            ,Nombre2
	                                            ,ApellidoPaterno
	                                            ,ApellidoMaterno
                                            FROM
                                                [ope].[V_OportunidadAlumno]
                                                            WHERE
                                                Id = @Id; ";
                var queryOportunidad = _dapperRepository.FirstOrDefault(_queryOportunidad, new { Id = idAlumno });
                var Oportunidad = JsonConvert.DeserializeObject<DatosAlumnoOportunidadDTO>(queryOportunidad);
                return Oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene Informacion de usuario por numero de alumno
        /// </summary>
        /// <param name="Numero"></param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerOportunidadPorNumero(string numero)
        {
            try
            {
                string _queryOportunidad = "Select IdPersonal,IdAlumno From [com].[V_TOportunidadPorNumero] Where EstadoAlumno=1 AND (Celular =@Numero) AND FaseOportunidad in('BNC','IT','IP','PF','IC','IS','M','ISM') AND IdPersonal!=125 Order By FechaCreacion Desc";
                var queryOportunidad = _dapperRepository.FirstOrDefault(_queryOportunidad, new { Numero = numero });
                var Oportunidad = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryOportunidad);
                return Oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion de usuario por numero de alumno
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <param name="numero"></param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerOportunidadPorNumero(int idCentroCosto, string numero)
        {
            try
            {
                string queryO = "SELECT IdPersonal FROM [com].[V_ObtenerChatAsignadosParaWhatsApp] WHERE IdCentroCosto = @IdCentroCosto";
                var queryPrograma = _dapperRepository.FirstOrDefault(queryO, new { IdCentroCosto = idCentroCosto });
                var oportunidad = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryPrograma);

                string queryA = "SELECT Id AS IdAlumno FROM [mkt].[T_Alumno] WITH (NOLOCK) WHERE Celular = @Numero ORDER BY FechaCreacion DESC";
                var queryAlumno = _dapperRepository.FirstOrDefault(queryA, new { Numero = numero });
                var alumno = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryAlumno);

                if (alumno != null)
                {
                    oportunidad.IdAlumno = alumno.IdAlumno;
                }
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de Tiempo Capacitacion que pertenecen al registro de T_Oportunidad asociada al Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadTiempoCapacitacionDTO> </returns>
        public OportunidadTiempoCapacitacionDTO ObtenerTiempoCapacitacionPorIdOportunidad(int idOportunidad)
        {
            try
            {
                OportunidadTiempoCapacitacionDTO tiempoCapacitacion = new OportunidadTiempoCapacitacionDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdTiempoCapacitacion,
	                    IdTiempoCapacitacion_Validacion AS IdTiempoCapacitacionValidacion
                    FROM com.T_Oportunidad
                    WHERE Estado = 1 AND Id = @idOportunidad";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    tiempoCapacitacion = JsonConvert.DeserializeObject<OportunidadTiempoCapacitacionDTO>(resultadoQuery);
                }
                return tiempoCapacitacion;
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
        /// Obtiene los datos de Tiempo Capacitacion que pertenecen al registro de T_Oportunidad asociada al Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadTiempoCapacitacionDTO> </returns>
        public async Task<OportunidadTiempoCapacitacionDTO> ObtenerTiempoCapacitacionPorIdOportunidadAsync(int idOportunidad)
        {
            try
            {
                OportunidadTiempoCapacitacionDTO tiempoCapacitacion = new OportunidadTiempoCapacitacionDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdTiempoCapacitacion,
	                    IdTiempoCapacitacion_Validacion AS IdTiempoCapacitacionValidacion
                    FROM com.T_Oportunidad
                    WHERE Estado = 1 AND Id = @idOportunidad";
                var resultadoQuery = await _dapperRepository.FirstOrDefaultAsync(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    tiempoCapacitacion = JsonConvert.DeserializeObject<OportunidadTiempoCapacitacionDTO>(resultadoQuery);
                }
                return tiempoCapacitacion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas la oportunidades de un Alumno por medio de su Id
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> List<Oportunidad>: oportunidades </returns>
        public List<Oportunidad> ObtenerPorIdAlumno(int idAlumno)
        {
            try
            {
                List<Oportunidad> oportunidades = new List<Oportunidad>();
                var query = @"
                            SELECT
	                            Id,
	                            IdCentroCosto,
	                            IdPersonal_Asignado AS IdPersonalAsignado,
	                            IdTipoDato,
	                            IdFaseOportunidad,
	                            IdOrigen,
	                            IdAlumno,
	                            UltimoComentario,
	                            IdActividadDetalle_Ultima AS IdActividadDetalleUltima,
	                            IdActividadCabecera_Ultima AS IdActividadCabeceraUltima,
	                            IdEstadoActividadDetalle_UltimoEstado AS IdEstadoActividadDetalleUltimoEstado,
	                            UltimaFechaProgramada,
	                            IdEstadoOportunidad,
	                            IdEstadoOcurrencia_Ultimo AS IdEstadoOcurrenciaUltimo,
	                            IdFaseOportunidad_Maxima AS IdFaseOportunidadMaxima,
	                            IdFaseOportunidad_Inicial AS IdFaseOportunidadInicial,
	                            IdCategoriaOrigen,
	                            IdConjuntoAnuncio,
	                            IdCampaniaScoring,
	                            IdFaseOportunidad_IP AS IdFaseOportunidadIp,
	                            IdFaseOportunidad_IC AS IdFaseOportunidadIc,
	                            FechaEnvioFaseOportunidadPF,
	                            FechaPagoFaseOportunidadPF,
	                            FechaPagoFaseOportunidadIC,
	                            FechaRegistroCampania,
	                            IdFaseOportunidadPortal,
	                            IdFaseOportunidad_PF AS IdFaseOportunidadPf,
	                            CodigoPagoIC,
	                            FlagVentaCruzada,
	                            IdTiempoCapacitacion,
	                            IdTiempoCapacitacion_Validacion AS IdTiempoCapacitacionValidacion,
	                            IdSubCategoriaDato,
	                            IdInteraccionFormulario,
	                            UrlOrigen,
	                            FechaPaso2,
	                            Paso2,
	                            CodMailing,
	                            IdPagina,
	                            Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion,
	                            RowVersion,
	                            IdMigracion,
	                            NroSolicitud,
	                            NroSolicitudPorArea,
	                            NroSolicitudPorSubArea,
	                            NroSolicitudPorProgramaGeneral,
	                            NroSolicitudPorProgramaEspecifico,
	                            IdClasificacionPersona,
	                            IdPersonalAreaTrabajo,
	                            IdPadre,
	                            IdAnuncioFacebook,
	                            ValidacionCorrecta,
	                            EnLlamada,
	                            NumeroIntentoLlamada,
	                            FechaReprogramacionIntento
                            FROM com.T_Oportunidad
                            WHERE Estado = 1 AND IdAlumno = @IdAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    oportunidades = JsonConvert.DeserializeObject<List<Oportunidad>>(resultado)!;
                }
                return oportunidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/03/2023
        /// Version: 1.0
        /// <summary>
        /// Valida al alumno para saber si se realizará la reasignación de oportunidades.
        /// </summary>
        /// <param name="idAlumnoPrincipal"></param>
        /// <param name="idAlumnoSecundario"></param>
        /// <returns></returns>
        public FlagActualizarCorreoDTO VerificacionOportunidades(int idAlumnoPrincipal, int idAlumnoSecundario)
        {
            try
            {
                FlagActualizarCorreoDTO resultado = new();
                var query = _dapperRepository.QuerySPFirstOrDefault("com.SP_VerificarCambioDeCorreo", new { idAlumnoPrincipal, idAlumnoSecundario });
                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    resultado = JsonConvert.DeserializeObject<FlagActualizarCorreoDTO>(query)!;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en VerificacionOportunidades: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/03/2023
        /// Version: 1.0
        /// <summary>
        /// Reasigna las oportunidades a un solo alumno.
        /// </summary>
        /// <param name="idAlumnoPrincipal"></param>
        /// <param name="idAlumnoSecundario"></param>
        /// <returns></returns>
        public FlagReasignacionDTO ResignacionOportunidades(int idAlumnoPrincipal, int idAlumnoSecundario)
        {
            try
            {
                FlagReasignacionDTO resultado = new FlagReasignacionDTO();
                var query = _dapperRepository.QuerySPFirstOrDefault("com.SP_CambioNuevaAsignacionDeOportunidades", new { idAlumnoPrincipal, idAlumnoSecundario });
                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    resultado = JsonConvert.DeserializeObject<FlagReasignacionDTO>(query)!;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ResignacionOportunidades: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene las oportunidades que se han reprogramado manualmente la ultima reprogramacion
        /// </summary>
        /// <returns></returns>
        public List<OportunidadProgramadaManualDTO> ObtenerProgramacionManualConsecutivos()
        {
            var oportunidadesCategoria = new List<OportunidadProgramadaManualDTO>();
            var queryAsesorById = "SELECT IdOportunidad FROM com.V_OportunidadReprogramacionManual";
            var registrosBD = _dapperRepository.QueryDapper(queryAsesorById, null);
            oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadProgramadaManualDTO>>(registrosBD);
            if (oportunidadesCategoria == null)
            {
                oportunidadesCategoria = new List<OportunidadProgramadaManualDTO>();
            }
            return oportunidadesCategoria;
        }


        public OportunidadAnteriorAlternoDTO? ValidarOportundadVentaCruzada(int idOportunidad)
        {
            try
            {
                var query = @"SELECT  IdOportunidad,
                                        FaseOportunidad,
                                        FechaCreacion,
                                        CentroCosto,
                                        TipoDato,
                                        CategoriaOrigen,
                                        IdAlumno,
                                        EstadoOportunidad
                                FROM com.V_ObtenerOportunidadesReporteSeguimiento 
                                WHERE IdOportunidad = @IdOportunidad AND EstadoOportunidad = 1  AND TipoDato ='Venta Cruzada'";
                var respuestaQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    return JsonConvert.DeserializeObject<OportunidadAnteriorAlternoDTO>(respuestaQuery);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? ObtnerUltimoRN1(OportunidadAnteriorAlternoDTO oportunidad)
        {
            try
            {
                var query = @"SELECT TOP 1 OPO.id  as Valor FROM com.T_Oportunidad AS OPO
								INNER JOIN com.T_ActividadDetalle AS AD ON AD.IdOportunidad = OPO.Id AND AD.Estado =1
								WHERE OPO.FechaModificacion <=@FechaCreacion AND OPO.IdAlumno =@idAlumno AND  OPO.Estado=1 AND OPO.IdFaseOportunidad=9 AND OPO.Id !=@IdOportunidad ORDER BY OPO.FechaModificacion DESC";
                var respuestaQuery = _dapperRepository.FirstOrDefault(query, new { FechaCreacion = oportunidad.FechaCreacion, idAlumno = oportunidad.IdAlumno, IdOportunidad = oportunidad.IdOportunidad });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    var resultado = JsonConvert.DeserializeObject<IntDTO>(respuestaQuery);
                    return resultado.Valor;

                }
                return null;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActividadAgendaDTO? ObtenerDatosOportunidad(int idOportunidad)
        {
            try
            {
                var query = @"SELECT * FROM com.V_DatosFichaAlumno WHERE IdOportunidad = @idOportunidad";
                var respuestaQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    return JsonConvert.DeserializeObject<ActividadAgendaDTO>(respuestaQuery);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActividadAgendaDTO? ObtenerOportunidadPredictivo(int idAlumno)
        {
            try
            {
                var query = @"SELECT V1.* FROM com.V_DatosFichaAlumno AS V1
                            INNER JOIN com.T_Oportunidad AS opo ON opo.id = v1.IdOportunidad
                            WHERE V1.IdAlumno=@idAlumno AND opo.UltimoComentario LIKE '%predictivo%'";
                var respuestaQuery = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    return JsonConvert.DeserializeObject<ActividadAgendaDTO>(respuestaQuery);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el programa general a partir del alumno
        /// </summary>
        /// <param name="idOportunidadRN2"></param>
        /// <returns></returns>
        public ComboDTO? ObtenerProgramaGeneralPredictivo(int idOportunidadRN2)
        {
            try
            {
                var query = @"
                        SELECT idProgramaGeneral AS Id, pg.Nombre FROM com.T_Oportunidad AS opo
                        INNER JOIN pla.T_PEspecifico AS pe ON pe.IdCentroCosto = opo.IdCentroCosto
	                        AND pe.Estado = 1
                        INNER JOIN pla.T_PGeneral AS pg ON pg.Id = pe.IdProgramaGeneral AND pg.Estado = 1
                        WHERE opo.Id=@idOportunidadRN2 AND opo.Estado=1
                        ";
                //var query = @"
                //        SELECT idProgramaGeneral AS Id, pg.Nombre FROM com.T_DatosPredictivo AS dp
                //        INNER JOIN pla.T_PGeneral AS pg ON pg.Id = dp.idProgramaGeneral AND pg.Estado = 1
                //        WHERE dp.IdAlumno=@idAlumno AND dp.Estado=1";
                var respuestaQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidadRN2 });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    return JsonConvert.DeserializeObject<ComboDTO>(respuestaQuery);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 20/12/2023
        /// Version: 1.0
        /// <summary>
        /// Actualizar el estado de seguimiento por whatsapp de la oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public void ActualizarSeguimientoWhatsAppOportunidad(int idOportunidad, int idActividad, bool estado)
        {
            try
            {
                var query = @"[com].[SP_ActualizarSeguimientoWhatsAppOportunidad]";
                var respuestaQuery = _dapperRepository.QuerySPDapper(query, new { idOportunidad, idActividad, estado });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/01/2024
        /// Version: 1.0
        /// <summary>
        /// ObteneValoresEtiquetasWhatsApp
        /// </summary>
        /// <param name="idBusqueda"></param>
        /// <returns> PgeneralDocumentoSeccionDTO </returns>
        public ValorEtiquetaWhatsAppDTO? ObteneValoresEtiquetaWhatsApp(int idOportunidad)
        {
            try
            {
                string queryPrograma = @"
                        SELECT 
                            AL.Nombre1 AS AlumnoNombre1, 
                            CONCAT(Per.Nombre1,' ',PER.Nombre2,' ',PER.ApellidoPaterno,' ',PER.ApellidoMaterno) AS PersonalNombreCompleto,
                            PG.Nombre AS PgeneralNombre
                        FROM com.T_Oportunidad AS OPO WITH (NOLOCK)
                        INNER JOIN pla.T_PEspecifico AS PE ON PE.IdCentroCosto = OPO.IdCentroCosto
                        INNER JOIN pla.T_PGeneral AS PG ON PG.Id = PE.IdProgramaGeneral
                        INNER JOIN gp.T_Personal AS Per ON Per.Id = OPO.IdPersonal_Asignado
                        INNER JOIN mkt.T_Alumno AS AL WITH (NOLOCK) ON AL.Id = OPO.IdAlumno
                        WHERE OPO.Id = @idOportunidad";
                var query = _dapperRepository.FirstOrDefault(queryPrograma, new { idOportunidad });
                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    return JsonConvert.DeserializeObject<ValorEtiquetaWhatsAppDTO>(query)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 19/01/2024
        /// Version: 1.0
        /// <summary>
        /// InsertarEnviosWhatsappDiasSinContacto
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        public string InsertarEnviosWhatsappDiasSinContacto(int idOportunidad)
        {
            try
            {
                string resultado;
                var _query = "com.SP_TEnviosWhatsappDiasSinContacto_Insertar";
                resultado = _dapperRepository.QuerySPDapper(_query, new { IdOportunidad = idOportunidad });
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 12/03/2024
        /// Version: 1.0
        /// <summary>
        /// Conteo de actividades totales, ejecutadas, its generados, ips generados
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> ControlActividadAgendaDTO </returns>
        public ControlActividadAgendaDTO ObtenerReporteControlActividadesAgenda(int idAsesor)
        {
            try
            {
                var rpta = new ControlActividadAgendaDTO();
                var fechaActual = DateTime.Now;
                var fechaInicio = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 0, 0, 0);
                var fechaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 23, 59, 59);
                var query = _dapperRepository.QuerySPFirstOrDefault("com.SP_ReporteControlActividadesAgenda", new
                {
                    IdAsesor = idAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                });
                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ControlActividadAgendaDTO>(query)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-ORCAA-001@Error en ObtenerReporteControlActividadesAgenda: {ex.Message}", ex);
            }
        }


        /// Autor: Junior Llerena
        /// Fecha: 28/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene métricas comparativas diarias de un asesor
        /// </summary>
        /// <param name="idAsesor">ID del asesor</param>
        /// <param name="fecha">Fecha opcional (por defecto hoy)</param>
        /// <returns>MetricasComparativasDiariasDTO con comparación vs día anterior</returns>
        public MetricasComparativasDiariasDTO ObtenerMetricasComparativasDiarias(int idAsesor, DateTime? fecha = null)
        {
            try
            {
                var fechaActual = (fecha ?? DateTime.Today).Date;
                var fechaAnterior = fechaActual.AddDays(-1);

                if (fechaAnterior.DayOfWeek == System.DayOfWeek.Sunday)
                {
                    fechaAnterior = fechaAnterior.AddDays(-1);
                }

                var esHoy = fechaActual == DateTime.Today;

                (int total, int ejecutadas, int its, int ips) ObtenerDatosHistorico(DateTime fechaConsulta)
                {
                    string query = @"
                        SELECT TOP 1
                            TotalActividad,
                            Ejecutado,
                            ItGenerado,
                            IpGenerado
                        FROM com.T_ControlActividadCongelado
                        WHERE IdPersonal = @idAsesor
                          AND CAST(Fecha AS DATE) = @fecha
                          AND Estado = 1
                        ORDER BY FechaCreacion DESC";

                    var resultado = _dapperRepository.FirstOrDefault(query, new
                    {
                        idAsesor = idAsesor,
                        fecha = fechaConsulta
                    });

                    int total = 0, ejecutadas = 0, its = 0, ips = 0;

                    if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    {
                        var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultado);

                        if (obj != null)
                        {
                            total = obj.ContainsKey("TotalActividad") ? Convert.ToInt32(obj["TotalActividad"]) : 0;
                            ejecutadas = obj.ContainsKey("Ejecutado") ? Convert.ToInt32(obj["Ejecutado"]) : 0;
                            its = obj.ContainsKey("ItGenerado") ? Convert.ToInt32(obj["ItGenerado"]) : 0;
                            ips = obj.ContainsKey("IpGenerado") ? Convert.ToInt32(obj["IpGenerado"]) : 0;
                        }
                    }

                    return (total, ejecutadas, its, ips);
                }

                int totalActividadesHoy, ejecutadasHoy, itsGeneradosHoy, ipsGeneradosHoy;

                if (esHoy)
                {
                    var datosHoy = ObtenerReporteControlActividadesAgenda(idAsesor);
                    totalActividadesHoy = datosHoy.Totales;
                    ejecutadasHoy = datosHoy.Ejecutadas;
                    itsGeneradosHoy = datosHoy.ItsGenerados;
                    ipsGeneradosHoy = datosHoy.IpsGenerados;
                }
                else
                {
                    var datosHoy = ObtenerDatosHistorico(fechaActual);
                    totalActividadesHoy = datosHoy.total;
                    ejecutadasHoy = datosHoy.ejecutadas;
                    itsGeneradosHoy = datosHoy.its;
                    ipsGeneradosHoy = datosHoy.ips;
                }

                var datosAyer = ObtenerDatosHistorico(fechaAnterior);

                MetricaComparativaDTO CalcularMetrica(int hoy, int ayer)
                {
               
                    int porcentaje = ayer > 0 ? (int)Math.Round(((double)(hoy - ayer) / ayer) * 100) : 0;

                    string estado = porcentaje >= 0 ? "Positivo" : "Negativo";

                    return new MetricaComparativaDTO
                    {
                        Hoy = hoy,
                        Ayer = ayer,
                        Porcentaje = porcentaje,
                        Estado = estado
                    };
                }

                return new MetricasComparativasDiariasDTO
                {
                    Success = true,
                    Fecha = fechaActual.ToString("yyyy-MM-dd"),
                    FechaComparacion = fechaAnterior.ToString("yyyy-MM-dd"),
                    IdAsesor = idAsesor,
                    Metricas = new MetricasDTO
                    {
                        TotalActividades = CalcularMetrica(totalActividadesHoy, datosAyer.total),
                        Ejecutadas = CalcularMetrica(ejecutadasHoy, datosAyer.ejecutadas),
                        ItsGenerados = CalcularMetrica(itsGeneradosHoy, datosAyer.its),
                        IpsGenerados = CalcularMetrica(ipsGeneradosHoy, datosAyer.ips)
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"#HA-OMCD-001@Error en ObtenerMetricasComparativasDiarias: {ex.Message}", ex);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 24/04/2024
        /// Version: 1.0
        /// <summary>
        /// BuscarFichaPorCelular
        /// </summary>
        /// <param name="celular"></param>
        /// <returns> Lista ResultadoBusquedaFichaAlumnoDTO </returns>
        public List<ResultadoBusquedaFichaAlumnoDTO> BuscarFichaPorCelular(string celular)
        {
            try
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_BuscarFichaPorCelular", new
                {
                    celular
                });
                if (!string.IsNullOrEmpty(query) && query != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ResultadoBusquedaFichaAlumnoDTO>>(query)!;
                }
                return new List<ResultadoBusquedaFichaAlumnoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-ORCAA-001@Error en BuscarFichaPorCelular: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los colores para los perfiles de la oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> Lista ColorPerfilProgramaDTO </returns>
        public IEnumerable<ColorPerfilProgramaDTO> ObtenerColorPerfilProgramaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var query = "mkt.SP_ColorPerfilProgramaOportunidad";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    IdOportunidad = idOportunidad,
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ColorPerfilProgramaDTO>>(resultado)!;
                }
                return new List<ColorPerfilProgramaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerColorPerfilProgramaPorIdOportunidad: {ex.Message}", ex);
            }
        }
        public List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO> ObtenerComentariosOperacionesPagosAcademicos2(int idOportunidad)
        {
            try
            {
                List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO> Comentario = new List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO>();
                string _queryOportunidad = $@"SELECT
	                    SACom.Comentario,
	                    SACat.Nombre AS tipoCategoria,
	                    SACat.IdTipoSeguimientoAlumnoCategoria,
	                    SACom.FechaCreacion,
	                    CONCAT(P.Nombres, ' ', P.Apellidos) AS usuarioCreacion
                    FROM ope.T_SeguimientoAlumnoComentario AS SACom
                    INNER JOIN ope.T_SeguimientoAlumnoCategoria AS SACat ON SACat.Id = SACom.IdSeguimientoAlumnoCategoria
                    INNER JOIN gp.T_Personal AS P ON P.Id = SACom.IdPersonal
                    WHERE
	                    IdOportunidad = @idOportunidad
	                    AND SACat.IdTipoSeguimientoAlumnoCategoria IN (1, 2)
                    ORDER BY SACom.FechaCreacion DESC;";
                var queryOportunidad = _dapperRepository.QueryDapper(_queryOportunidad, new { idOportunidad });
                if (!queryOportunidad.Contains("[]"))
                {
                    Comentario = JsonConvert.DeserializeObject<List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO>>(queryOportunidad)!;
                }
                return Comentario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 2024-11-19
        /// Version: 1.0
        /// <summary>
        /// Obtiene al personal configurado para el emvío de correo.
        /// </summary>
        /// <returns> List<ComprobantePagoOportunidadDTO> </returns>
        public List<CorreoNotificacionDTO> ObtenerCorreoNotificacion()
        {
            try
            {
                List<CorreoNotificacionDTO> rpta = new List<CorreoNotificacionDTO>();
                var query = @"SELECT IdPersonal	,
		                            Email,
		                            IdPais,
		                            IdCorreoNotificacionTipo,
		                            NombreNotificacionTipo,
		                            DescripcionNotificacionTipo,
                                    Principal
                            FROM com.V_ObtenerCorreoNotificacion";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CorreoNotificacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 2025-02-01
        /// Version: 1.0
        /// <summary>
        /// Inserta un archivo subido a Azure 
        /// </summary>

        public string InsertarArchivo(string nombreArchivo, string usuario)
        {
            try
            {
                string resultado;
                var _query = "mkt.SP_ArchivoOportunidad_Insertar";
                resultado = _dapperRepository.QuerySPDapper(_query, new { NombreArchivo = nombreArchivo, Usuario = usuario });
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 2025-01-31
        /// Version: 1.0
        /// <summary>
        /// Obtien Archivo s de Azure
        /// </summary>
        public List<ArchivoOportunidadDTO> ObtenerArchivosOportunidad()
        {
            try
            {
                List<ArchivoOportunidadDTO> listaArchivos = new List<ArchivoOportunidadDTO>();
                var query = @"SELECT Id, NombreArchivo, FechaCreacion
                      FROM mkt.T_ArchivoOportunidad WHERE  Estado=1 
                      ORDER BY FechaCreacion DESC";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaArchivos = JsonConvert.DeserializeObject<List<ArchivoOportunidadDTO>>(resultado);
                }
                return listaArchivos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener archivos de oportunidad", ex);
            }
        }


        public void InsertarHistorialOportunidadTabla(int idOportunidad, string usuario)
        {
            try
            {
                var query = @"
            INSERT INTO mkt.T_HistorialOportunidadMasiva 
            (IdOportunidad, UsuarioCreacion, UsuarioModificacion, Origen, FechaCreacion, FechaModificacion, Estado)
            VALUES 
            (@IdOportunidad, @UsuarioCreacion, @UsuarioModificacion, @Origen, GETDATE(), GETDATE(), 1)";

                var parametros = new
                {
                    IdOportunidad = idOportunidad,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    Origen = "MasivaOportunidad"
                };

                _dapperRepository.QueryDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar en historial de oportunidad masiva", ex);
            }
        }

        public void InsertarHistorialOportunidad(int idOportunidad, string usuario)
        {
            try
            {
                var query = "mkt.SP_HistorialOportunidadMasiva_Insertar";

                var parametros = new
                {
                    IdOportunidad = idOportunidad,
                    Usuario = usuario,
                    Origen = "MasivaOportunidad"
                };


                _dapperRepository.QuerySPDapper(query, parametros);

                Console.WriteLine("✅ Historial de oportunidad masiva insertado correctamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Error al insertar en historial de oportunidad masiva", ex);
            }
        }
        public List<OportunidadMasivaDTO> ObtenerOportunidadesMasivas()
        {
            try
            {
                List<OportunidadMasivaDTO> listaOportunidades = new List<OportunidadMasivaDTO>();

                var query = @"SELECT Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, 
                             NombrePais, NombreCiudad, Celular, Email1, 
                             NombreCargo, NombreFormacion, NombreAreaTrabajo, 
                             NombreIndustria, NombreCentroCosto, NombrePersonal, 
                             NombreTipoDato, NombreOrigen, CodigoFase
                      FROM mkt.V_ObtenerHistorialOportunidadMasiva ORDER BY IdHistorial DESC";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaOportunidades = JsonConvert.DeserializeObject<List<OportunidadMasivaDTO>>(resultado);
                }

                return listaOportunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener oportunidades masivas", ex);
            }
        }
        public OportunidadConversionesDTO ObtenerInformacionOportunidadConversion(int idOportunidad)
        {
            try
            {
                OportunidadConversionesDTO informacionConversion = new OportunidadConversionesDTO();
                var query = @"SELECT 
                            FFL.Id AS IdFacebookFormularioLeadgen,
                            FFL.IdLeadgenFacebook AS LeadId,
                            AA.IdOportunidad,
                            FFL.Email,
                            FFL.Telefono,
                            FFL.NombreCompleto,
                            FFL.Ciudad 
                        FROM mkt.T_AsignacionAutomatica AS AA
                        INNER JOIN mkt.T_AsignacionAutomatica_Temp AS AAT ON AAT.ID=AA.IdAsignacionAutomaticaTemp
                        INNER JOIN mkt.T_FacebookFormularioLeadgen AS FFL ON FFL.Id=AAT.IdFacebookFormularioLeadgen
                        WHERE AA.IdOportunidad=@idOportunidad";

                var resultado = _dapperRepository.FirstOrDefault(query, new {idOportunidad});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    informacionConversion = JsonConvert.DeserializeObject<OportunidadConversionesDTO>(resultado);
                    return informacionConversion;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public OportunidadDetalleProbabilidadDTO ObtenerInformacionOportunidadProbabilidad(int idOportunidad)
        {
            try
            {
                OportunidadDetalleProbabilidadDTO informacionOportunidad = new OportunidadDetalleProbabilidadDTO();
                var query = @"SELECT
                                AC.Id AS IdAreaCapacitacion,
                                AC.Nombre AS AreaCapacitacion,
                                PRpw.Nombre AS ClasificacionProbabilidad,
                                OLH.IdOportunidad,
                                        OLH.IdFaseOportunidad_Ant AS IdFaseOportunidadAnterior,
                                        OLH.IdFaseOportunidad AS IdFaseOportunidadActual
                            FROM
                                mkt.V_ModeloPredictivoProbabilidadIdProbabilidadRegistro MPPPR
                                INNER JOIN com.T_Oportunidad AS O ON O.id=MPPPR.IdOportunidad AND O.Estado=1
                                INNER JOIN pla.T_PEspecifico AS PE ON PE.IdCentroCosto=O.IdCentroCosto
                                INNER JOIN pla.T_PGeneral AS PG ON PG.Id=PE.IdProgramaGeneral
                                INNER JOIN pla.T_AreaCapacitacion AS AC ON AC.Id=PG.IdArea
                                LEFT JOIN mkt.T_ProbabilidadRegistro_PW PRpw ON PRpw.Id = MPPPR.IdProbabilidadRegistroPW 
                                OUTER APPLY (
                                    SELECT TOP 1 
                                        IdOportunidad,
                                        IdFaseOportunidad_Ant,
                                        IdFaseOportunidad
                                    FROM com.V_OportunidadLogHistorico WITH (NOLOCK)  
                                    WHERE IdOportunidad = MPPPR.IdOportunidad
                                    ORDER BY FechaCreacion DESC, id DESC
                                ) AS OLH
                            WHERE MPPPR.IdOportunidad = @idOportunidad";

                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    informacionOportunidad = JsonConvert.DeserializeObject<OportunidadDetalleProbabilidadDTO>(resultado);
                    return informacionOportunidad;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// Autor: Junior Llerena
        /// Fecha: 01/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los códigos de descuento asociados a un alumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Objeto con códigos de descuento del alumno</returns>
        public AlumnoCodigosDescuentosDTO ObtenerCodigoDescuentoAlumno(int idAlumno)
        {
            try
            {
                string query = @"
                    SELECT ppcd.Id,
                           a.Id AS IdAlumno,
                           ppcd.PorcentajeDescuento,
                           ppcd.CodigoDescuentoArmado AS CodigoDescuento,
                           ppcd.Utilizado,
                           ppcd.Estado,
                           ppcd.Correo
                    FROM [192.168.2.5].integraDB_PortalWeb.mkt.T_ProgressiveProfilingCodigoDescuentoCorreo ppcd
                    INNER JOIN mkt.T_alumno a ON ppcd.Correo COLLATE DATABASE_DEFAULT = a.Email1 COLLATE DATABASE_DEFAULT
                    WHERE a.id = @idAlumno
                      AND ppcd.Estado = 1
                    ORDER BY ppcd.id DESC;";

                var resultado = _dapperRepository.QueryDapper(query, new { idAlumno });

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var codigos = JsonConvert.DeserializeObject<List<CodigoDTO>>(resultado);

                    if (codigos != null && codigos.Count > 0)
                    {
                        var alumnoDescuentos = new AlumnoCodigosDescuentosDTO
                        {
                            IdAlumno = idAlumno
                        };

                        var descuentosProperty = typeof(AlumnoCodigosDescuentosDTO).GetProperty("Descuentos");
                        descuentosProperty.SetValue(alumnoDescuentos, codigos);

                        return alumnoDescuentos;
                    }
                }
                var alumnoVacio = new AlumnoCodigosDescuentosDTO
                {
                    IdAlumno = idAlumno
                };
                var propertyVacio = typeof(AlumnoCodigosDescuentosDTO).GetProperty("Descuentos");
                propertyVacio.SetValue(alumnoVacio, new List<CodigoDTO>());

                return alumnoVacio;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCDA-001@Error en ObtenerCodigoDescuentoAlumno: {ex.Message}", ex);
            }
        }

        /// TipoFuncion: Public
        /// Autor: Junior Llerena
        /// Fecha: 29/12/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza el centro de costo de una actividad
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <param name="idActividad">Id de la Actividad</param>
        /// <returns>True si se actualizó correctamente</returns>
        public bool ActualizarCentroCosto(int idCentroCosto, int idActividad)
        {
            try
            {
                var query = "com.SP_ActualizarCentroCostoPorActividad";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdCentroCosto = idCentroCosto, IdActividadDetalle_Ultima = idActividad });
                return !string.IsNullOrEmpty(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-ACC-001@Error en ActualizarCentroCosto: {ex.Message}", ex);
            }
        }


    }
}
