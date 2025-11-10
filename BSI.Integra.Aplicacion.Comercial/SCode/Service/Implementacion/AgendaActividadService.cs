using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Net;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: AgendaActividadActividadService
    /// Autor:  Gilmer Quispe.
    /// Fecha: 21/02/2023
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class AgendaActividadService : IAgendaActividadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AgendaActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 23/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de dias sin Contacto del Cliente 
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad Relacionada</param>
        /// <returns> int </returns>
        public int ObtenerDiasSinContactoPorOportunidad(int idOportunidad)
        {
            var fechasSinContacto = _unitOfWork.OportunidadLogRepository.ObtenerFechasSinContactoPorIdOportunidad(idOportunidad).ToList();
            if (fechasSinContacto.Count() != 0)
                fechasSinContacto.RemoveAll(f => f.Fecha_Log == DateTime.Now.Date);
            return fechasSinContacto.Count();
        }

        public int ValidarVisualizacionDatosOportunidad(int idOportunidad, int idPersonal)
        {
            return _unitOfWork.AlumnoRepository.InsertarSolicitudVisualizarDatosOportunidad(idOportunidad, idPersonal).Valor.Value;//0:no puede ver pero puede ingresar solicitud,1:puede verlo hasta la fecha,2:no puede verlo y no puede ingresar       
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cabecera Speech para Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> PGeneralCabeceraSpeechAgendaDTO </returns>
        public PGeneralCabeceraSpeechAgendaDTO ObtenerCabeceraSpeech(int idOportunidad, int idCentroCosto)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCabeceraSpeechAgenda(idOportunidad, idCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Publico Objetivo para Agenda
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<PGeneralPublicoObjetivoParaAgendaDTO> </returns>
        public IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoPrograma(int idCentroCosto, int idOportunidad)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPublicoObjetivoProgramaParaAgenda(idCentroCosto, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene certificaciones y argumentos para Agenda asociados a un Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralCertificacionDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionDetalleAgendaDTO> ObtenerRequisitosCertificacionProgramaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var programaGeneralCertificacionAgendaDTOs = _unitOfWork.ProgramaGeneralCertificacionRepository.ObtenerCertificacionesParaAgendaPorIdOportunidad(idOportunidad);
                var programaGeneralCertificacionDetalleAgendaDTOs = _mapper.Map<List<ProgramaGeneralCertificacionDetalleAgendaDTO>>(programaGeneralCertificacionAgendaDTOs);
                IProgramaGeneralCertificacionArgumentoService certificacionArgumentoService = new ProgramaGeneralCertificacionArgumentoService(_unitOfWork);
                programaGeneralCertificacionDetalleAgendaDTOs.ForEach(
                    c => c.Requisitos = certificacionArgumentoService.ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(c.IdCertificacion).ToList()
                );
                return programaGeneralCertificacionDetalleAgendaDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene motivaciones y argumentos para Agenda asociados a un Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralMotivacionDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> ObtenerArgumentosMotivacionProgramaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var programaGeneralMotivacionAgendaDTOs = _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerMotivacionesParaAgendaPorIdOportunidad(idOportunidad);
                var programaGeneralMotivacionDetalleAgendaDTOs = _mapper.Map<List<ProgramaGeneralMotivacionDetalleAgendaDTO>>(programaGeneralMotivacionAgendaDTOs);
                IProgramaGeneralMotivacionArgumentoService motivacionArgumentoService = new ProgramaGeneralMotivacionArgumentoService(_unitOfWork);
                programaGeneralMotivacionDetalleAgendaDTOs.ForEach(
                    c => c.Argumentos = motivacionArgumentoService.ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(c.IdMotivacion).ToList()
                );
                return programaGeneralMotivacionDetalleAgendaDTOs;
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
        /// Obtiene la informacion relacionada a Oportunidades basado en IdClasificacionPersona y IdAlumno* (No usado por el momento)
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<OportunidadInformacionDTO> </returns>
        public OportunidadInformacionDTO ObtenerOportunidadInformacion(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                var oportunidadInformacion = new OportunidadInformacionDTO();
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);

                oportunidadInformacion.ListaVentaCruzada = oportunidadService.ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(idClasificacionPersona).ToList();
                oportunidadInformacion.ListaHistorial = oportunidadService.ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(idClasificacionPersona).ToList();

                return oportunidadInformacion;
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
        /// Obtiene la informacion relacionada a Oportunidades basado en IdClasificacionPersona y IdAlumno* (No usado por el momento)
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<OportunidadInformacionDTO> </returns>
        public OportunidadInformacionDTO ObtenerOportunidadInformacionPersonalizado(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                var oportunidadInformacion = new OportunidadInformacionDTO();
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);

                oportunidadInformacion.ListaVentaCruzada = oportunidadService.ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(idClasificacionPersona).ToList();
                oportunidadInformacion.ListaHistorial = oportunidadService.ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(idClasificacionPersona).ToList();

                return oportunidadInformacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Version: 1.0
        /// <summary>
        /// Cargar información de competidores
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns>CargarInformacionCompetidoresRespuestaDTO</returns> 
        public async Task<CargarInformacionCompetidoresRespuestaDTO> CargarInformacionCompetidoresAsync(int idOportunidad)
        {
            try
            {
                if (idOportunidad == 0)
                    return new CargarInformacionCompetidoresRespuestaDTO { Error = "IdOportunidad no válido" };

                var tareaCompetidores = _unitOfWork.CompetidorRepository.ObtenerCompetidoresPorIdOportunidadAsync(idOportunidad);

                var competidoresRaw = await tareaCompetidores;

                var competidores = new List<Competidorv2DTO>();

                if (competidoresRaw?.Any() == true)
                {
                    var groupedByCompetidor = competidoresRaw.GroupBy(c => c.Id);

                    foreach (var group in groupedByCompetidor)
                    {
                        var firstItem = group.First();

                        var contenidoCompetidorProcesado = new List<string>();

                        foreach (var item in group)
                        {
                            if (!string.IsNullOrEmpty(item.ContenidoCompetidorVentajaDesventaja))
                            {
                                var texto = System.Net.WebUtility.HtmlDecode(item.ContenidoCompetidorVentajaDesventaja);
                                texto = System.Text.RegularExpressions.Regex.Replace(texto, @"<[^>]+>", "");
                                texto = System.Text.RegularExpressions.Regex.Replace(texto, @"\s+", " ");
                                var contenidoProcesado = texto.Trim();

                                if (!string.IsNullOrEmpty(contenidoProcesado))
                                {
                                    contenidoCompetidorProcesado.Add(contenidoProcesado);
                                }
                            }
                        }

                        competidores.Add(new Competidorv2DTO
                        {
                            Id = firstItem.Id,
                            IdOportunidad = firstItem.IdOportunidad,
                            Nombre = firstItem.Nombre,
                            DuracionCronologica = firstItem.DuracionCronologica,
                            CostoNeto = firstItem.CostoNeto,
                            Precio = firstItem.Precio,
                            Categoria = firstItem.Categoria,
                            Empresa = firstItem.Empresa,
                            RegionCiudad = firstItem.RegionCiudad,
                            Moneda = firstItem.Moneda,
                            ContenidosCompetidor = contenidoCompetidorProcesado
                        });
                    }
                }

                return new CargarInformacionCompetidoresRespuestaDTO
                {
                    IdOportunidad = idOportunidad,
                    Competidores = competidores,
                    Error = null
                };
            }
            catch (Exception ex)
            {
                return new CargarInformacionCompetidoresRespuestaDTO
                {
                    IdOportunidad = idOportunidad,
                    Competidores = new List<Competidorv2DTO>(),
                    Error = $"Error al cargar competidores: {ex.Message}"
                };
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObtenerProgramaGeneralProblemaDetallePorIdOportunidad(int idOportunidad)
        {
            try
            {
                IProgramaGeneralProblemaService programaGeneralProblemaService = new ProgramaGeneralProblemaService(_unitOfWork);
                IProgramaGeneralProblemaDetalleSolucionService programaGeneralProblemaDetalleSolucionService = new ProgramaGeneralProblemaDetalleSolucionService(_unitOfWork);

                var programaGeneralProblemaAgendaDTOs = programaGeneralProblemaService.ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(idOportunidad);
                var programaGeneralProblemaDetalleAgendaDTOs = _mapper.Map<List<ProgramaGeneralProblemaDetalleAgendaDTO>>(programaGeneralProblemaAgendaDTOs);
                programaGeneralProblemaDetalleAgendaDTOs.ForEach(
                    p => p.Argumentos = programaGeneralProblemaDetalleSolucionService.ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(p.IdProblema, idOportunidad).ToList()
                );
                return programaGeneralProblemaDetalleAgendaDTOs;
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
        /// Obtiene el ultimo Tipo de Interaccion de Correos asociados a un Alumno y un Personal.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<CorreoInteraccionV2AgendaDTO> </returns>
        public IEnumerable<CorreoInteraccionV2AgendaDTO> ObtenerCorreoInteraccionV2EnviadosPorPersonal(int idAlumno, int idPersonal)
        {
            try
            {
                return _unitOfWork.MandrilRepository.ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(idAlumno, idPersonal);
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
        /// Obtiene Competidores relacionados a una Oportunidad para Agenda.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<CompetidorOportunidadAgendaDTO> </returns>
        public IEnumerable<CompetidorOportunidadAgendaDTO> ObtenerCompetidorPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var competidorOportunidadAgendaDTOs = _unitOfWork.CompetidorRepository.ObtenerCompetidorParaAgendaPorIdOportunidad(idOportunidad).GroupBy(c => c.Id);
                var competidorOportunidadAgendaDTOs1 = new List<CompetidorOportunidadAgendaDTO>();
                foreach (var group in competidorOportunidadAgendaDTOs)
                {
                    var primerCompetidor = group.First();
                    primerCompetidor.IdCompetidorVentajaDesventaja = primerCompetidor.IdCompetidorVentajaDesventaja ?? 0;
                    primerCompetidor.TipoCompetidorVentajaDesventaja = primerCompetidor.TipoCompetidorVentajaDesventaja ?? 0;
                    primerCompetidor.ContenidoCompetidorVentajaDesventaja = string.Join("", group.ToList().Select(p => p.ContenidoCompetidorVentajaDesventaja ?? "Sin Desventaja"));
                    competidorOportunidadAgendaDTOs1.Add(primerCompetidor);
                }
                return competidorOportunidadAgendaDTOs1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Prerequisitos, Beneficios y Competidores asociados a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public OportunidadPrerequisitoBeneficioCompetidorDTO ObtenerPrerequisitosBeneficiosCompetidoresPorIdOportunidad(int idOportunidad)
        {
            IOportunidadCompetidorService oportunidadCompetidorService = new OportunidadCompetidorService(_unitOfWork);
            IDetalleOportunidadCompetidorService detalleOportunidadCompetidorService = new DetalleOportunidadCompetidorService(_unitOfWork);
            IProgramaGeneralPrerequisitoService programaGeneralPrerequisitoService = new ProgramaGeneralPrerequisitoService(_unitOfWork);
            IProgramaGeneralBeneficioService programaGeneralBeneficioService = new ProgramaGeneralBeneficioService(_unitOfWork);
            IProgramaGeneralBeneficioArgumentoService programaGeneralBeneficioArgumentoService = new ProgramaGeneralBeneficioArgumentoService(_unitOfWork);
            var oportunidadPrerequisitoBeneficioCompetidorDTO = new OportunidadPrerequisitoBeneficioCompetidorDTO();

            oportunidadPrerequisitoBeneficioCompetidorDTO.OportunidadCompetidor = oportunidadCompetidorService.ObtenerOportunidadCompetidorPorIdOportunidad(idOportunidad)?.FirstOrDefault();
            if (oportunidadPrerequisitoBeneficioCompetidorDTO.OportunidadCompetidor != null)
            {
                oportunidadPrerequisitoBeneficioCompetidorDTO.Competidores = detalleOportunidadCompetidorService.ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(oportunidadPrerequisitoBeneficioCompetidorDTO.OportunidadCompetidor.Id).ToList();
            }
            oportunidadPrerequisitoBeneficioCompetidorDTO.PrerequisitosGenerales = programaGeneralPrerequisitoService.ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(idOportunidad).ToList();
            oportunidadPrerequisitoBeneficioCompetidorDTO.PrerequisitosEspecificos = programaGeneralPrerequisitoService.ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(idOportunidad).ToList();

            var beneficios = programaGeneralBeneficioService.ObtenerProgramaGeneralBeneficioPorIdOportunidad(idOportunidad).ToList();
            oportunidadPrerequisitoBeneficioCompetidorDTO.Beneficios = beneficios.Select(
                b => new ProgramaGeneralBeneficioOportunidadDetalleDTO()
                {
                    Id = b.IdBeneficio,
                    Nombre = b.NombrePrerequisito,
                    Respuesta = b.Respuesta,
                    Completado = b.Completado,
                    Argumentos = programaGeneralBeneficioArgumentoService.ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(b.IdBeneficio).ToList()
                }
            ).ToList();
            return (oportunidadPrerequisitoBeneficioCompetidorDTO);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        public IEnumerable<OportunidadLogHistorialComentariosDTO> ObtenerHistorialComentariosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var oportunidadLogReporteSeguimientoYPersonalDTOs = _unitOfWork.OportunidadLogRepository.ObtenerReporteSeguimientoYPersonalPorIdOportunidad(idOportunidad);
                var oportunidadLogHistorialComentariosDTOs = (
                    from olog in oportunidadLogReporteSeguimientoYPersonalDTOs
                    group olog by new
                    {
                        olog.FaseInicio,
                        olog.FaseDestino,
                        olog.FechaModificacion,
                        olog.FechaSiguienteLlamada,
                        olog.IdFaseOportunidad,
                        olog.IdFaseOportunidadIP,
                        olog.IdFaseOportunidadPF,
                        olog.IdFaseOportunidadIC,
                        olog.FechaEnvioFaseOportunidadPF,
                        olog.FechaPagoFaseOportunidadPF,
                        olog.FechaPagoFaseOportunidadIC,
                        olog.IdOcurrencia,
                        olog.IdEstadoOcurrencia,
                        olog.IdOportunidadLog,
                        olog.NombreActividad,
                        olog.NombreOcurrencia,
                        olog.ComentarioActividad,
                        olog.IdFaseOportunidadInicial,
                        olog.Personal
                    } into g
                    select new OportunidadLogHistorialComentariosDTO
                    {
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        Personal = g.Key.Personal,
                        LlamadasIntegra = g.Select(o => new LlamadaIntegraDTO
                        {
                            Id = o.IdCentralLLamada,
                            TiempoDuracion = o.TiempoDuracion,
                            TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                            FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                            EstadoLlamada = o.EstadoLlamadaIntegra,
                            FechaFinLlamada = o.FechaFinLlamadaIntegra,
                            SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                            NombreGrabacion = o.NombreGrabacionIntegra,
                            Webphone = o.Webphone
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                        Llamadas3CX = g.Select(o => new Llamada3CXDTO
                        {
                            Id = o.IdTresCX,
                            TiempoDuracion = o.TiempoDuracion3CX,
                            FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                            EstadoLlamada = o.EstadoLlamadaTresCX,
                            FechaFinLlamada = o.FechaFinLlamadaTresCX,
                            SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                            NombreGrabacion = o.NombreGrabacionTresCX
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                    }).ToList();
                return oportunidadLogHistorialComentariosDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        /// AutorModificacion:Margiory Ramirez 
        /// Validacion si la oportunidad que trae es una venta cruzada,
        /// si no es sigue flujo,si es trae la oportunidad anterior y la actual
        /// Fecha Modificacion: 12/10/2023
        /// Versión: 1.0
        public List<ReporteSeguimientoNWActividadDTO?> ObtenerHistorialInteraccionesPorIdOportunidad(int idOportunidad)
        {
            try
            {
                IOportunidadLogService oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var ventaCruzada = _unitOfWork.OportunidadRepository.ValidarOportundadVentaCruzada(idOportunidad);
                var reporte = oportunidadLogService.ObtenerReporteSeguimientoNWActividadesPorIdOportunidad(idOportunidad);
                var reporteSeguimientoNWActividadDTOs = new List<ReporteSeguimientoNWActividadDTO?>
                    {
                        reporte.FirstOrDefault(x => x.Estado == "NO EJECUTADO")
                    };
                reporteSeguimientoNWActividadDTOs.AddRange(reporte.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());
                if (ventaCruzada != null)
                {
                    var RN1 = _unitOfWork.OportunidadRepository.ObtnerUltimoRN1(ventaCruzada);
                    if (RN1 != null && RN1 != 0)
                    {
                        var reporteRN1 = oportunidadLogService.ObtenerReporteSeguimientoNWActividadesPorIdOportunidad(RN1.Value);
                        var reporteSeguimientoNWActividadDTOsRN1 = new List<ReporteSeguimientoNWActividadDTO?>();
                        reporteSeguimientoNWActividadDTOsRN1.AddRange(reporteRN1.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());

                        reporteSeguimientoNWActividadDTOs.AddRange(reporteSeguimientoNWActividadDTOsRN1);
                    }

                }
                return (reporteSeguimientoNWActividadDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        /// AutorModificacion:Margiory Ramirez 
        /// Validacion si la oportunidad que trae es una venta cruzada,
        /// si no es sigue flujo,si es trae la oportunidad anterior y la actual
        /// Fecha Modificacion: 12/10/2023
        /// Versión: 1.0
        public List<ReporteSeguimientoNWActividadAlternoDTO?> ObtenerHistorialInteraccionesPorIdOportunidad3cx(int idOportunidad)
        {
            try
            {
                IOportunidadLogService oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var ventaCruzada = _unitOfWork.OportunidadRepository.ValidarOportundadVentaCruzada(idOportunidad);
                var reporte = oportunidadLogService.ObtenerReporteSeguimientoActividadesPorIdOportunidad(idOportunidad);
                var reporteSeguimientoNWActividadDTOs = new List<ReporteSeguimientoNWActividadAlternoDTO?>
                    {
                        reporte.FirstOrDefault(x => x.Estado == "NO EJECUTADO")
                    };
                reporteSeguimientoNWActividadDTOs.AddRange(reporte.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());
                if (ventaCruzada != null)
                {
                    var RN1 = _unitOfWork.OportunidadRepository.ObtnerUltimoRN1(ventaCruzada);
                    if (RN1 != null && RN1 != 0)
                    {
                        var reporteRN1 = oportunidadLogService.ObtenerReporteSeguimientoActividadesPorIdOportunidad(RN1.Value);
                        var reporteSeguimientoNWActividadDTOsRN1 = new List<ReporteSeguimientoNWActividadAlternoDTO?>();
                        reporteSeguimientoNWActividadDTOsRN1.AddRange(reporteRN1.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());
                        reporteSeguimientoNWActividadDTOs.AddRange(reporteSeguimientoNWActividadDTOsRN1);
                    }
                }
                return (reporteSeguimientoNWActividadDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 10/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
      public async Task<HistorialInteraccionesResponseDTO> ObtenerHistorialInteraccionesPorIdOportunidadMensajePersonalizado(int idOportunidad)
        {
            try
            {
                IOportunidadLogService oportunidadLogService = new OportunidadLogService(_unitOfWork);
                ITranscriptionService transcriptionService = new TranscriptionService(_unitOfWork);
                var ventaCruzada = _unitOfWork.OportunidadRepository.ValidarOportundadVentaCruzada(idOportunidad);
                var reporte = oportunidadLogService.ObtenerReporteSeguimientoActividadesPorIdOportunidad(idOportunidad);
                var reporteSeguimientoNWActividadDTOs = new List<ReporteSeguimientoNWActividadAlternoDTO?>
                    {
                        reporte.FirstOrDefault(x => x.Estado == "NO EJECUTADO")
                    };
                reporteSeguimientoNWActividadDTOs.AddRange(reporte.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());
                if (ventaCruzada != null)
                {
                    var RN1 = _unitOfWork.OportunidadRepository.ObtnerUltimoRN1(ventaCruzada);
                    if (RN1 != null && RN1 != 0)
                    {
                        var reporteRN1 = oportunidadLogService.ObtenerReporteSeguimientoActividadesPorIdOportunidad(RN1.Value);
                        var reporteSeguimientoNWActividadDTOsRN1 = new List<ReporteSeguimientoNWActividadAlternoDTO?>();
                        reporteSeguimientoNWActividadDTOsRN1.AddRange(reporteRN1.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());
                        reporteSeguimientoNWActividadDTOs.AddRange(reporteSeguimientoNWActividadDTOsRN1);
                    }
                }

                var response = new HistorialInteraccionesResponseDTO();

                // Procesar los registros: los primeros 4 van a PrimerasInteracciones, el resto a InteraccionesAnteriores
                for (int i = 0; i < reporteSeguimientoNWActividadDTOs.Count; i++)
                {
                    var actividad = reporteSeguimientoNWActividadDTOs[i];

                    if (i < 4)
                    {
                        // Para los primeros 4, obtener toda la información incluyendo transcripciones
                        if (actividad != null && actividad.LlamadasIntegra3cx != null)
                        {
                            foreach (var llamada in actividad.LlamadasIntegra3cx)
                            {
                                if (llamada.Id.HasValue)
                                {
                                    try
                                    {
                                        var transcripcion = await transcriptionService.ObtenerTranscripcion(llamada.Id.Value);
                                        llamada.Transcripcion = transcripcion;
                                    }
                                    catch (Exception)
                                    {
                                        llamada.Transcripcion = null;
                                    }
                                }
                            }
                        }
                        response.PrimerasInteracciones.Add(actividad);
                    }
                    else
                    {
                        // Para los registros después del 4to, crear un objeto resumido
                        if (actividad != null)
                        {
                            var interaccionResumida = new InteraccionAnteriorResumidaDTO
                            {
                                FechaModificacion = actividad.FechaModificacion,
                                FechaSiguienteLlamada = actividad.FechaSiguienteLlamada,
                                FaseInicio = actividad.FaseInicio,
                                FaseDestino = actividad.FaseDestino,
                                FechaInicioLlamada = actividad.LlamadasIntegra3cx?.FirstOrDefault()?.FechaInicioLlamada
                            };
                            response.InteraccionesAnteriores.Add(interaccionResumida);
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de Preguntas Frecuentes asociadas a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public (List<PreguntaFrecuenteSeccionesDTO> Data, List<PGeneralModeloCertificadoDTO> ModeloCertificado) ObtenerPreguntasFrecuentes(int idCentroCosto, int idOportunidad)
        {
            try
            {
                IInformacionProgramaService informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                IPGeneralService pGeneralService = new PGeneralService(_unitOfWork);
                IPreguntaFrecuentePGeneralService preguntaFrecuentePGeneralService = new PreguntaFrecuentePGeneralService(_unitOfWork);
                IProgramaGeneralModeloCertificadoService programaGeneralModeloCertificadoService = new ProgramaGeneralModeloCertificadoService(_unitOfWork);
                var programaCentroCostoDTO = pGeneralService.ObtenerDatosPFrecuentes(idCentroCosto);
                if (programaCentroCostoDTO.IdPGeneral != null)
                {
                    var preguntaFrecuentePGeneralDTO2s = preguntaFrecuentePGeneralService.ObtenerPreguntaFrecuente(programaCentroCostoDTO);
                    var preguntaFrecuenteSeccionesDTOs = informacionProgramaService.CargarInformacionPrograma(preguntaFrecuentePGeneralDTO2s);

                    var pGeneralModeloCertificadoDTOs = programaGeneralModeloCertificadoService.ObtenerModeloCertificadoPrograma(idOportunidad);
                    return (preguntaFrecuenteSeccionesDTOs, pGeneralModeloCertificadoDTOs);
                }
                else
                {
                    throw new BadRequestException("El Id del Centro Costo no exite!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas Frecuentes de Cambio
        /// </summary>
        /// <param name="idCentroCosto">Id del centro de costo</param>
        /// <param name="idPrograma">Id del programa general/param>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <returns> Lista de Preguntas Frecuentes de Cambio </returns>
        /// <returns> Lista objeto DTO : List<PreguntaFrecuenteSeccionesDTO> </returns>
        /// <returns> Lista objeto DTO : List<ProgramaGeneralModeloCertificadoDTO> </returns>
        public (List<PreguntaFrecuenteSeccionesDTO> Data, List<ProgramaGeneralModeloCertificadoDTO> ModeloCertificado) ObtenerPreguntasFrecuentesCambio(int idCentroCosto, int idPrograma, int idOportunidad)
        {
            try
            {
                var pGeneralAreaSubArea = _unitOfWork.PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral(idPrograma);
                if (pGeneralAreaSubArea == null)
                    throw new Exception("No existe el programa");

                var pEspecificoIdTipoDTO = _unitOfWork.PEspecificoRepository.ObtenerTipoIdPorIdCentroCosto(idCentroCosto);
                if (pEspecificoIdTipoDTO == null)
                    throw new Exception("No existe el centro costo");

                var preguntaFrecuentePGeneralDTO2s = _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPreguntaFrecuenteCambio(idPrograma, pGeneralAreaSubArea.IdArea, pGeneralAreaSubArea.IdSubArea, pEspecificoIdTipoDTO.TipoId);

                IInformacionProgramaService informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var preguntaFrecuenteSeccionesDTOs = informacionProgramaService.CargarInformacionPrograma(preguntaFrecuentePGeneralDTO2s);
                var programaGeneralModeloCertificadoDTOs = _unitOfWork.ProgramaGeneralModeloCertificadoRepository.ObtenerModeloCertificadoProgramaPorIdOportunidad(idOportunidad);

                return (preguntaFrecuenteSeccionesDTOs, programaGeneralModeloCertificadoDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public (List<PreguntaFrecuenteSeccionesV2DTO> Data, List<ProgramaGeneralModeloCertificadoDTO> ModeloCertificado) ObtenerPreguntasFrecuentesCambioV2(int idCentroCosto, int idPrograma, int idOportunidad)
        {
            try
            {
                var pGeneralAreaSubArea = _unitOfWork.PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral(idPrograma);
                if (pGeneralAreaSubArea == null)
                    throw new Exception("No existe el programa");

                var pEspecificoIdTipoDTO = _unitOfWork.PEspecificoRepository.ObtenerTipoIdPorIdCentroCosto(idCentroCosto);
                if (pEspecificoIdTipoDTO == null)
                    throw new Exception("No existe el centro costo");

                var preguntaFrecuentePGeneralDTO2s = _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPreguntaFrecuenteCambio(idPrograma, pGeneralAreaSubArea.IdArea, pGeneralAreaSubArea.IdSubArea, pEspecificoIdTipoDTO.TipoId);

                IInformacionProgramaService informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var preguntaFrecuenteSeccionesDTOs = informacionProgramaService.CargarInformacionProgramaV2(preguntaFrecuentePGeneralDTO2s);
                var programaGeneralModeloCertificadoDTOs = _unitOfWork.ProgramaGeneralModeloCertificadoRepository.ObtenerModeloCertificadoProgramaPorIdOportunidad(idOportunidad);

                return (preguntaFrecuenteSeccionesDTOs, programaGeneralModeloCertificadoDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Arbol de Ocurrencias por Actividad Cabecera e IdOcurrencia Padre
        /// </summary>
        /// <returns> Arbol de Ocurrencias </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        public List<ArbolOcurrenciaDTO> ObtenerArbolOcurrencias(int idActividadCabecera, int idOcurrenciaActividadPadre)
        {
            try
            {
                List<ArbolOcurrenciaDTO> arbolOcurenciaDTOs = _unitOfWork.OcurrenciaActividadRepository.ObtenerArbolOcurrencia(idActividadCabecera, idOcurrenciaActividadPadre);
                return (arbolOcurenciaDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Modificaciones del Alumno asociado a su Identificador
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public IEnumerable<AlumnoLogAgendaFechaStringDTO> ObtenerHistorialModificacionAlumnoPorIdAlumno(int idAlumno)
        {
            try
            {
                IAlumnoLogService alumnoLogService = new AlumnoLogService(_unitOfWork);
                return (alumnoLogService.ObtenerAlumnoLogParaAgendaPorIdAlumno(idAlumno));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Documentos Legales dependiendo de Datos del Personal y Pais del Alumno.
        /// </summary>
        /// <param name="idAreaPersonal">Id de PersonalAreaTrabajo</param>
        /// <param name="rol">Nombre del Rol del Personal</param>
        /// <param name="idAlumno"></param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        public IEnumerable<DocumentoLegalV3DTO> ObtenerDocumentoLegal(int idAreaPersonal, string rol, int idAlumno)
        {
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                var alumno = new Alumno();
                alumno = alumnoService.ObtenerPorId(idAlumno);
                if (alumno.IdCodigoPais.Value == null) { alumno.IdCodigoPais = 0; }
                var documentoLegal = _unitOfWork.DocumentoLegalRepository.ObtenerDocumentoLegalAgenda(idAreaPersonal, rol, alumno.IdCodigoPais.Value);
                return (documentoLegal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el valor de las Plantillas asociadas a una Fase Oportunidad
        /// </summary>
        /// <param name="idFaseOportunidad">Id de la Fase Oportunidad</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        public IEnumerable<PlantillaClaveValorAreaEtiquetaDTO> ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillasPorIdFaseOportunidad(idFaseOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="estadoPantalla">Flag: 0 -> IS, 1 -> Opo. Cerrada, 2-> Solo para mostrar</param>
        /// <returns> SeguimientoAsesorDTO </returns>
        public SeguimientoAsesorDTO ObtenerSeguimientoAsesor(int idPersonal, int idCategoriaOrigen, int estadoPantalla)
        {
            try
            {
                return _unitOfWork.OportunidadMaximaPorCategoriaRepository.ObtenerSeguimientoAsesor(idPersonal, idCategoriaOrigen, estadoPantalla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Documentos de Agenda para su Descarga (url o arreglo de bytes asignados) asociados a una Actividad Detalle.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> List<DocumentoAgendaDescargaDTO> </returns>
        public DocumentoAgendaDetalleDTO ObtenerDocumentosPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                IPGeneralService pGeneralService = new PGeneralService(_unitOfWork);
                IDocumentoAgendaService documentoAgendaService = new DocumentoAgendaService(_unitOfWork);
                var oportunidadCompuestoDTO = oportunidadService.ObtenerOportunidadCompuestoPorIdActividadDetalle(idActividadDetalle);
                var programaEspecificoPorCentroCostoDTO = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidadCompuestoDTO.IdCentroCosto!.Value);
                var pGeneralAtributosPrincipalesDTO = pGeneralService.ObtenerPGeneralAtributosPrincipalesPorId(programaEspecificoPorCentroCostoDTO.IdProgramaGeneral!.Value);

                var documentosDescarga = _mapper.Map<List<DocumentoAgendaDescargaDTO>>(documentoAgendaService.ObtenerDocumentoAgendaSinAuditoria());
                documentosDescarga = documentosDescarga.Select(
                    d => d.Generado ?
                       documentoAgendaService.ObtenerBytesDocumentoAgenda(d, programaEspecificoPorCentroCostoDTO, pGeneralAtributosPrincipalesDTO, oportunidadCompuestoDTO) :
                       documentoAgendaService.ObtenerUrlDocumentoAgenda(d, programaEspecificoPorCentroCostoDTO, pGeneralAtributosPrincipalesDTO)
                ).ToList();

                return new DocumentoAgendaDetalleDTO
                {
                    Oportunidad = oportunidadCompuestoDTO,
                    ProgramaEspecifico = programaEspecificoPorCentroCostoDTO,
                    Documentos = documentosDescarga
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Datos del Alumno
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        public (AlumnoInformacionDTO, SueldoPromedioDTO, ResultadoVisualizarOportunidadDTO) ObtenerDatosAlumno(int idClasificacionPersona, int idOportunidad, int idPersonal)
        {
            IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
            IEmpresaService empresaService = new EmpresaService(_unitOfWork);
            ISentinelService sentinelService = new SentinelService(_unitOfWork);
            IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);

            var alumnoInformacionDTO = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);

            var idTamanioEmpresa = alumnoInformacionDTO.IdEmpresa != null ? empresaService.ObtenerIdTamanioEmpresaPorIdEmpresa(alumnoInformacionDTO.IdEmpresa!.Value) : null;

            var sueldoPromedioDTO = sentinelService.ObtenerSueldoPromedio(new SueldoPromedioArgumentosDTO
            {
                IdEmpresa = alumnoInformacionDTO.IdEmpresa,
                Dni = alumnoInformacionDTO.DNI,
                IdCargo = alumnoInformacionDTO.IdCargo,
                IdIndustria = alumnoInformacionDTO.IdIndustria,
                IdTamanioEmpresa = idTamanioEmpresa?.Valor
            });

            var resultadoVisualizarOportunidadDTO = oportunidadService.ValidarVisualizarAgendaPorIdOportunidad(idOportunidad, idPersonal) ??
                new ResultadoVisualizarOportunidadDTO()
                {
                    Id = 0,
                    FechaVisibleHasta = DateTime.Now,
                    Valor = 0
                };
            return (alumnoInformacionDTO, sueldoPromedioDTO, resultadoVisualizarOportunidadDTO);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Datos del Alumno
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        public AlumnoInformacionDTO ObtenerDatosAlumnoPersonalizado(int idClasificacionPersona, int idOportunidad)
        {
            IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
            IEmpresaService empresaService = new EmpresaService(_unitOfWork);
            ISentinelService sentinelService = new SentinelService(_unitOfWork);
            IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);

            var alumnoInformacionDTO = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);

            var idTamanioEmpresa = alumnoInformacionDTO.IdEmpresa != null ? empresaService.ObtenerIdTamanioEmpresaPorIdEmpresa(alumnoInformacionDTO.IdEmpresa!.Value) : null;
   
            return (alumnoInformacionDTO);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos principales de Plantillas asociadas a WhatsApp para la Agenda.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsApp()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaWhatsAppAgenda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la Probabilidad de Sueldo asociado a la Oportunidad y Pais.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public StringDTO ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerProbabilidadSueldoOportunidad(idOportunidad, idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas Versión 1
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public CargarInformacionProgramaAutomaticoRespuestaDTO ObtenerInformacionProgramav1(Dictionary<string, string> filtro)
        {
            try
            {

                IInformacionProgramaService informacionProgramaService = new InformacionProgramaService(_unitOfWork);

                var idCentroCosto = Convert.ToInt32(filtro["idCentroCosto"]);
                var codigoPais = Convert.ToInt32(filtro["codigoPais"]);
                var idMatriculaCabecera = Convert.ToInt32(filtro["idMatriculaCabecera"]);
                var idOportunidad = Convert.ToInt32(filtro["idOportunidad"]);

                var cargarInformacionProgramaAutomaticoRespuestaDTO = informacionProgramaService.CargarInformacionProgramaAutomatico(idCentroCosto, codigoPais, idMatriculaCabecera, idOportunidad);
                return (cargarInformacionProgramaAutomaticoRespuestaDTO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene configuraciones necesarias para Agenda.
        /// </summary>
        /// <returns> ObtenerConfiguracionesDTO </returns>
        public ObtenerConfiguracionesDTO ObtenerConfiguraciones()
        {
            try
            {
                IAreaCapacitacionService areaCapacitacionService = new AreaCapacitacionService(_unitOfWork);
                ISubAreaCapacitacionService subAreaCapacitacionService = new SubAreaCapacitacionService(_unitOfWork);
                IPGeneralService pGeneralService = new PGeneralService(_unitOfWork);
                var obtenerConfiguracionesDTO = new ObtenerConfiguracionesDTO()
                {
                    AreasCapacitacion = areaCapacitacionService.ObtenerCombo(),
                    SubAreasCapacitacion = subAreaCapacitacionService.ObtenerCombo(),
                    ProgramasGenerales = pGeneralService.ObtenerCombo()
                };
                return obtenerConfiguracionesDTO;
            }
            catch (Exception e)
            {
                throw new BadRequestException("ASEE-X000003@Este error fue generado en la funcion ObtenerConfiguraciones," + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message + "@OPS! Al parecer tenemos un problema para obtener las configuraciones, intentalo nuevamente o brinda el codigo de error (ASEE-X000003) a soporte tecnico");
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 22/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas disponibles por fase
        /// </summary>
        /// <param name="idFaseOportunidad">Id de Fase de Oportunidad</param>
        /// <param name="idPersonalAreaTrabajo">Id de Arera de Trabajo de Personal</param>
        /// <returns> List<FiltroDTO> </returns>
        public IOrderedEnumerable<FiltroDTO> ObtenerPlantillaPorFase(int idFaseOportunidad, int idPersonalAreaTrabajo)
        {
            try
            {
                IPlantillaClaveValorService plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                IPersonalAreaTrabajoService personalAreaTrabajoService = new PersonalAreaTrabajoService(_unitOfWork);
                var plantillaFase = plantillaClaveValorService.ObtenerPlantillaGenerarMensaje(idFaseOportunidad).ToList();
                if (!_unitOfWork.PersonalAreaTrabajoRepository.ExistePorId(idPersonalAreaTrabajo))
                {
                    throw new BadRequestException("No existe el PersonalAreaTrabajo");
                }
                //if (idPersonalAreaTrabajo == ValorEstatico.IdPersonalAreaTrabajoOperaciones)
                if (idPersonalAreaTrabajo == 3)
                {
                    plantillaFase.AddRange(plantillaClaveValorService.ObtenerPlantillaGenerarMensajeOperaciones());
                }
                var filtroDTOs = plantillaFase.OrderBy(w => w.Nombre);
                return (filtroDTOs);
            }
            catch (Exception e)
            {
                throw new BadRequestException("ASEE-X000003@Este error fue generado en la funcion ObtenerPlantillaPorFase," + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message + "@OPS! Al parecer tenemos un problema para obtener la plantilla, intentalo nuevamente o brinda el codigo de error (ASEE-X000003) a soporte tecnico");
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si existe una oportunidadNuevaEntidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <param name="idContacto">Id Contacto</param>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> bool </returns>
        public bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ValidarRN2(idContacto, idCentroCosto, idOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha y Hora de Reprogramación Automática
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="codigoFase">Codigo de Fase</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public string ObtenerFechaHoraActividadReprogramacionAutomatica(int idOportunidad, string codigoFase, int idOcurrencia)
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                IPersonalHorarioService personalHorarioService = new PersonalHorarioService(_unitOfWork);
                IHoraReprogramacionAutomaticaService servicioHoraReprogramacion = new HoraReprogramacionAutomaticaService(_unitOfWork);

                var oportunidad = oportunidadService.ObtenerDatosParaReprogramacionAutomatica(idOportunidad);
                var horario = personalHorarioService.ObtenerHorarioAsTable(oportunidad.IdPersonalAsignado);
                var respuesta = servicioHoraReprogramacion.ObtenerFechaHoraActividadReprogramacionAutomatica(
                    oportunidad.IdActividadCabeceraUltima,
                    oportunidad.IdCategoriaOrigen,
                    oportunidad.IdPersonalAsignado,
                    codigoFase,
                    idOcurrencia,
                    horario);

                return (respuesta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Ocurrencias de Actividad por Ocurrencia Alterno
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> List<HojaActividadesDTO> </returns>
        public List<HojaActividadesDTO> ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ObtenerHojaActividadesPorIdOcurrenciaAlterno(idOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Versión: 1.1
        /// <summary>
        /// Actualiza Información de sentinelDTO por Alumno
        /// </summary>
        /// <param name="dni">DNI de la Persona</param>
        /// <param name="idContacto">Id del Contacto</param>
        /// <param name="usuario">Usuario de modificacion</param>
        /// <returns> Retorna true o false </returns>
        public ActualizarSentinelAlumnoDTO ActualizarSentinelAlumno(string dni, int idContacto, string usuario)
        {
            try
            {
                ISentinelService sentinelService = new SentinelService(_unitOfWork);
                ISentinelSdtEstandarItemService sentinelSdtEstandarItemService = new SentinelSdtEstandarItemService(_unitOfWork);
                ISentinelSdtInfGenService sentinelSdtInfGenService = new SentinelSdtInfGenService(_unitOfWork);
                ISentinelSdtLincreItemService sentinelSdtLincreItemService = new SentinelSdtLincreItemService(_unitOfWork);
                ISentinelSdtPoshisItemService sentinelSdtPoshisItemService = new SentinelSdtPoshisItemService(_unitOfWork);
                ISentinelRepLegItemService sentinelRepLegItemService = new SentinelRepLegItemService(_unitOfWork);
                ISentinelSdtRepSbsitemService sentinelSdtRepSbsitemService = new SentinelSdtRepSbsitemService(_unitOfWork);
                ISentinelSdtResVenItemService sentinelSdtResVenItemService = new SentinelSdtResVenItemService(_unitOfWork);
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                var resultadoSentinel = sentinelService.ObtenerIdSentinelPorDni(dni);
                var idSentinel = 0;
                if (resultadoSentinel != null && resultadoSentinel.Valor != null)
                {
                    idSentinel = resultadoSentinel.Valor.Value;
                }
                var alumno = alumnoService.ObtenerPorId(idContacto);
                SentinelDTO sentinelDTO = new SentinelDTO();

                bool respuesta = false;
                bool estado = true;
                if (idSentinel != null && idSentinel > 0)
                {
                    sentinelDTO = sentinelService.ObtenerSentinelPorDni(dni);
                    var sentinelSdtEstandarItem = sentinelSdtEstandarItemService.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtInfGen = sentinelSdtInfGenService.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtLincreItem = sentinelSdtLincreItemService.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtPoshisItem = sentinelSdtPoshisItemService.ObtenerPorIdSentinel(idSentinel);
                    var sentinelRepLegItem = sentinelRepLegItemService.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtRepSBSItem = sentinelSdtRepSbsitemService.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtResVenItem = sentinelSdtResVenItemService.ObtenerPorIdSentinel(idSentinel);

                    if (sentinelSdtEstandarItem != null && sentinelSdtEstandarItem.Count() > 0) { sentinelSdtEstandarItemService.Delete(sentinelSdtEstandarItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtInfGen != null && sentinelSdtInfGen.Count() > 0) { sentinelSdtInfGenService.Delete(sentinelSdtInfGen.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtLincreItem != null && sentinelSdtLincreItem.Count() > 0) { sentinelSdtLincreItemService.Delete(sentinelSdtLincreItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtPoshisItem != null && sentinelSdtPoshisItem.Count() > 0) { sentinelSdtPoshisItemService.Delete(sentinelSdtPoshisItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelRepLegItem != null && sentinelRepLegItem.Count() > 0) { sentinelRepLegItemService.Delete(sentinelRepLegItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtRepSBSItem != null && sentinelSdtRepSBSItem.Count() > 0) { sentinelSdtRepSbsitemService.Delete(sentinelSdtRepSBSItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtResVenItem != null && sentinelSdtResVenItem.Count() > 0) { sentinelSdtResVenItemService.Delete(sentinelSdtResVenItem.Select(p => p.Id).ToList(), usuario); }
                    /* Estado = 1 */
                    var resultadoActualizar = sentinelService.ActualizarSentinelAlumno(dni, usuario);
                    if (resultadoActualizar.DatosGenerales.Dni == "")
                    {
                        estado = false;
                    }
                    if (estado)
                    {
                        alumno.Dni = dni;
                        alumno = alumnoService.ValidarEstadoContactoWhatsAppTemporalAlterno(alumno);
                        if (resultadoActualizar.DatosGenerales != null)
                        {
                            alumno.FechaNacimiento = resultadoActualizar.DatosGenerales.FechaNacimiento;
                        }

                        alumno.IdEmpresa = (alumno.IdEmpresa == 0 || alumno.IdEmpresa == -1) ? null : alumno.IdEmpresa;
                        alumnoService.Update(alumno);
                        var entidadSentinel = sentinelService.MapeoEntidadDesdeDTO(sentinelDTO);
                        entidadSentinel.SentinelRepLegItems = sentinelRepLegItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Cargo);
                        entidadSentinel.SentinelSdtEstandarItems = sentinelSdtEstandarItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DniRuc);
                        entidadSentinel.SentinelSdtInfGens = sentinelSdtInfGenService.MapeoEntidadesDesdeListaDTO(new SentinelSdtInfGenDTO[] { resultadoActualizar.DatosGenerales }.ToList());
                        entidadSentinel.SentinelSdtLincreItems = sentinelSdtLincreItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.LineaCredito);
                        entidadSentinel.SentinelSdtPoshisItems = sentinelSdtPoshisItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.PosicionHistoria);
                        entidadSentinel.SentinelSdtRepSbsitems = sentinelSdtRepSbsitemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Deuda);
                        entidadSentinel.SentinelSdtResVenItems = sentinelSdtResVenItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DatosVencidas);
                        entidadSentinel.Dni = dni;
                        entidadSentinel.UsuarioModificacion = usuario;
                        entidadSentinel.FechaModificacion = DateTime.Now;
                        sentinelService.Update(entidadSentinel);
                        respuesta = true;
                    }
                }
                else
                {
                    sentinelDTO.Dni = dni;
                    sentinelDTO.Estado = true;
                    sentinelDTO.UsuarioCreacion = usuario;
                    sentinelDTO.UsuarioModificacion = usuario;
                    sentinelDTO.FechaCreacion = DateTime.Now;
                    sentinelDTO.FechaModificacion = DateTime.Now;
                    var resultadoActualizar = sentinelService.ActualizarSentinelAlumno(dni, usuario);
                    if (resultadoActualizar.DatosGenerales.Dni == "")
                    {
                        estado = false;
                    }
                    if (estado)
                    {
                        alumno = alumnoService.ValidarEstadoContactoWhatsAppTemporalAlterno(alumno);
                        if (resultadoActualizar.DatosGenerales != null)
                        {
                            alumno.FechaNacimiento = resultadoActualizar.DatosGenerales.FechaNacimiento;
                            alumno.UsuarioModificacion = usuario;
                            alumno.FechaModificacion = DateTime.Now;
                        }
                        alumno.Dni = dni;
                        alumnoService.Update(alumno);
                        var entidadSentinel = sentinelService.MapeoEntidadDesdeDTO(sentinelDTO);
                        entidadSentinel.SentinelRepLegItems = sentinelRepLegItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Cargo);
                        entidadSentinel.SentinelSdtEstandarItems = sentinelSdtEstandarItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DniRuc);
                        entidadSentinel.SentinelSdtInfGens = sentinelSdtInfGenService.MapeoEntidadesDesdeListaDTO(new SentinelSdtInfGenDTO[] { resultadoActualizar.DatosGenerales }.ToList());
                        entidadSentinel.SentinelSdtLincreItems = sentinelSdtLincreItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.LineaCredito);
                        entidadSentinel.SentinelSdtPoshisItems = sentinelSdtPoshisItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.PosicionHistoria);
                        entidadSentinel.SentinelSdtRepSbsitems = sentinelSdtRepSbsitemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Deuda);
                        entidadSentinel.SentinelSdtResVenItems = sentinelSdtResVenItemService.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DatosVencidas);
                        sentinelService.Add(entidadSentinel);
                        respuesta = true;
                    }
                }
                var actualizarSentinelAlumnoDTO = new ActualizarSentinelAlumnoDTO()
                {
                    Respuesta = respuesta,
                    IdSentinel = sentinelDTO.Id,
                    Estado = estado
                };
                return (actualizarSentinelAlumnoDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel para la cabecera de agenda
        /// </summary>
        /// <param name="idAlumno">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public SentinelDatosCabeceraDTO ObtenerSemaforoSentinelAlumno(int idAlumno)
        {
            try
            {
                ISentinelService servicioSentinel = new SentinelService(_unitOfWork);
                var sentinelDatosContactoDTO = servicioSentinel.ObtenerDatosAlumnoSentinel(idAlumno);
                var sentinelDatosCabeceraDTO = new SentinelDatosCabeceraDTO();
                if (sentinelDatosContactoDTO.IdSentinel != null)
                {
                    sentinelDatosCabeceraDTO = servicioSentinel.ObtenerCabeceraSentinel(sentinelDatosContactoDTO.IdSentinel.Value);
                }
                return (sentinelDatosCabeceraDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información de Tiempo de Capacitación
        /// </summary>
        /// <param name="oportunidadTiempoCapacitacionDTO">Id de Ocurrencia</param>
        /// <returns> bool </returns>
        public bool ActualizarTiempoCapacitacion(OportunidadTiempoCapacitacionDTO oportunidadTiempoCapacitacionDTO)
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                Oportunidad oportunidad = oportunidadService.ObtenerPorId(oportunidadTiempoCapacitacionDTO.Id);
                oportunidad.IdTiempoCapacitacionValidacion = oportunidadTiempoCapacitacionDTO.IdTiempoCapacitacionValidacion ?? 0;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.UsuarioModificacion = oportunidadTiempoCapacitacionDTO.Usuario;
                oportunidadService.Update(oportunidad);
                return (true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Datos Personales Alumno
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <param name="areaTrabajo">Area de Trabajo del Personal</param>
        /// <returns> Alumno </returns>
        public Alumno ActualizarAlumno(AlumnoActualizarDTO alumno, string usuario, string areaTrabajo)
        {
            try
            {

                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                IAlumnoLogService alumnoLogService = new AlumnoLogService(_unitOfWork);
                ICiudadService ciudadService = new CiudadService(_unitOfWork);

                List<AlumnoLog> alumnoLogs = new List<AlumnoLog>();
                var validarEmail1 = alumnoService.ValidarEmail1Alumno(alumno.Email1);
                var validarEmail2 = alumnoService.ValidarEmail2Alumno(alumno.Email2);

                if (validarEmail1.Id != null && validarEmail1.Id != 0)
                {
                    if (alumno.Email1 == validarEmail1.Email1 && alumno.Id != validarEmail1.Id) throw new ArgumentException("El Email 1 ya existe");
                }
                if (validarEmail2.Id != null && validarEmail2.Id != 0)
                {
                    if (alumno.Email2 == validarEmail2.Email1 && alumno.Id != validarEmail2.Id) throw new ArgumentException("El Email 2 ya existe");
                }

                if (alumno.IdEmpresa == 0) alumno.IdEmpresa = null;

                Alumno alumnoAnterior = alumnoService.ObtenerPorId(alumno.Id);

                if (alumnoAnterior == null || alumnoAnterior.Id <= 0)
                {
                    throw new BadRequestException("El Alumno no existe");
                }

                if (alumnoAnterior.Nombre1 != alumno.Nombre1) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Nombre 1", alumnoAnterior.Nombre1 ?? "", alumno.Nombre1 ?? "", usuario));
                if (alumnoAnterior.Nombre2 != alumno.Nombre2 && (alumnoAnterior.Nombre2 != null || alumno.Nombre2 != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Nombre 2", alumnoAnterior.Nombre2 ?? "", alumno.Nombre2 ?? "", usuario));
                if (alumnoAnterior.ApellidoPaterno != alumno.ApellidoPaterno) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Apellido Paterno", alumnoAnterior.ApellidoPaterno ?? "", alumno.ApellidoPaterno ?? "", usuario));
                if (alumnoAnterior.ApellidoMaterno != alumno.ApellidoMaterno && (alumnoAnterior.ApellidoMaterno != null || alumno.ApellidoMaterno != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Apellido Materno", alumnoAnterior.ApellidoMaterno ?? "", alumno.ApellidoMaterno ?? "", usuario));
                if (alumnoAnterior.Dni != alumno.Dni && (alumnoAnterior.Dni != null || alumno.Dni != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Dni", alumnoAnterior.Dni ?? "", alumno.Dni ?? "", usuario));
                if (alumnoAnterior.Email2 != alumno.Email2 && (alumnoAnterior.Email2 != null || alumno.Email2 != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Email 2", alumnoAnterior.Email2 ?? "", alumno.Email2 ?? "", usuario));
                if (alumnoAnterior.Celular2 != alumno.Celular2 && (alumnoAnterior.Celular2 != null || alumno.Celular2 != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Celular 2", alumnoAnterior.Celular2 ?? "", alumno.Celular2 ?? "", usuario));
                if (alumnoAnterior.Telefono != alumno.Telefono && (alumnoAnterior.Telefono != null || alumno.Telefono != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Telefono", alumnoAnterior.Telefono ?? "", alumno.Telefono ?? "", usuario));
                if (alumnoAnterior.Telefono2 != alumno.Telefono2 && (alumnoAnterior.Telefono2 != null || alumno.Telefono2 != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Telefono 2", alumnoAnterior.Telefono2 ?? "", alumno.Telefono2 ?? "", usuario));
                if (alumnoAnterior.Direccion != alumno.Direccion && (alumnoAnterior.Direccion != null || alumno.Direccion != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Direccion", alumnoAnterior.Direccion ?? "", alumno.Direccion ?? "", usuario));

                if (alumnoAnterior.IdCargo != alumno.IdCargo && alumnoAnterior.Cargo != alumno.Cargo) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Cargo", alumnoAnterior.Cargo ?? "", alumno.Cargo ?? "", usuario));
                if (alumnoAnterior.IdAtrabajo != alumno.IdAtrabajo && alumnoAnterior.Atrabajo != alumno.Atrabajo) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Trabajo", alumnoAnterior.Atrabajo ?? "", alumno.Atrabajo ?? "", usuario));
                if (alumnoAnterior.IdEmpresa != alumno.IdEmpresa && alumnoAnterior.Empresa != alumno.Empresa) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Empresa", alumnoAnterior.Empresa ?? "", alumno.Empresa ?? "", usuario));
                if (alumnoAnterior.IdAformacion != alumno.IdAformacion && alumnoAnterior.Aformacion != alumno.Aformacion) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Formacion", alumnoAnterior.Aformacion ?? "", alumno.Aformacion ?? "", usuario));
                if (alumnoAnterior.IdIndustria != alumno.IdIndustria && alumnoAnterior.Industria != alumno.Industria) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Industria", alumnoAnterior.Industria ?? "", alumno.Industria ?? "", usuario));
                if (alumno.IdCiudad != null)
                {
                    var ciudadAlumnoDestino = ciudadService.ObtenerNombreCiudadPorId(alumno.IdCiudad.Value);
                    if (alumno.Ciudad == null || alumnoAnterior.IdCiudad == 0)
                    {
                        if (alumnoAnterior.IdCiudad != alumno.IdCiudad) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Ciudad", alumno.Ciudad ?? "", ciudadAlumnoDestino.Valor ?? "", usuario));
                    }
                    else
                    {
                        if (alumnoAnterior.IdCiudad != alumno.IdCiudad) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Ciudad", alumno.Ciudad.ToString() ?? "", ciudadAlumnoDestino.Valor ?? "", usuario));
                    }
                }
                //alumnoBO.Id = Alumno.Id;
                alumnoAnterior.Nombre1 = alumno.Nombre1;
                alumnoAnterior.Nombre2 = alumno.Nombre2;
                alumnoAnterior.ApellidoPaterno = alumno.ApellidoPaterno;
                alumnoAnterior.ApellidoMaterno = alumno.ApellidoMaterno;
                alumnoAnterior.Dni = alumno.Dni;
                //Email1 = Alumno.Email1,
                alumnoAnterior.Email2 = alumno.Email2;
                alumnoAnterior.Celular = alumno.Celular;
                alumnoAnterior.Celular2 = alumno.Celular2;
                alumnoAnterior.Telefono = alumno.Telefono;
                alumnoAnterior.Telefono2 = alumno.Telefono2;
                alumnoAnterior.Direccion = alumno.Direccion;
                if (alumnoAnterior.IdPais == null || alumnoAnterior.IdPais == 0)
                {
                    alumnoAnterior.IdPais = alumno.IdCodigoPais;
                }
                alumnoAnterior.IdCargo = alumno.IdCargo;
                alumnoAnterior.Cargo = alumno.Cargo;
                alumnoAnterior.IdAtrabajo = alumno.IdAtrabajo;
                alumnoAnterior.Atrabajo = alumno.Atrabajo;
                if (alumnoAnterior.IdEmpresa != alumno.IdEmpresa) alumnoAnterior.Empresa = alumno.Empresa;
                alumnoAnterior.IdEmpresa = alumno.IdEmpresa;
                alumnoAnterior.IdAformacion = alumno.IdAformacion;
                alumnoAnterior.Aformacion = alumno.Aformacion;
                alumnoAnterior.IdIndustria = alumno.IdIndustria;
                alumnoAnterior.Industria = alumno.Industria;
                alumnoAnterior.IdCiudad = alumno.IdCiudad;
                alumnoAnterior.FechaModificacion = DateTime.Now;
                alumnoAnterior.UsuarioModificacion = usuario == null ? "" : usuario;

                if (areaTrabajo == "OP")
                {
                    if (alumnoAnterior.Genero != alumno.Genero && (alumnoAnterior.Genero != null || alumno.Genero != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Genero", alumnoAnterior.Genero ?? "", alumno.Genero ?? "", usuario));
                    if (alumnoAnterior.Parentesco != alumno.Parentesco && (alumnoAnterior.Parentesco != null || alumno.Parentesco != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Parentesco", alumnoAnterior.Parentesco ?? "", alumno.Parentesco ?? "", usuario));
                    if (alumnoAnterior.NombreFamiliar != alumno.NombreFamiliar && (alumnoAnterior.NombreFamiliar != null || alumno.NombreFamiliar != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Nombre Familiar", alumnoAnterior.NombreFamiliar ?? "", alumno.NombreFamiliar ?? "", usuario));
                    if (alumnoAnterior.TelefonoFamiliar != alumno.TelefonoFamiliar && (alumnoAnterior.TelefonoFamiliar != null || alumno.TelefonoFamiliar != "")) alumnoLogs.Add(alumnoLogService.ConstruirEntidadAlumnoLog(alumno.Id, "Telefono Familiar", alumnoAnterior.TelefonoFamiliar ?? "", alumno.TelefonoFamiliar ?? "", usuario));

                    alumnoAnterior.Genero = alumno.Genero;
                    alumnoAnterior.Parentesco = alumno.Parentesco;
                    alumnoAnterior.NombreFamiliar = alumno.NombreFamiliar;
                    alumnoAnterior.TelefonoFamiliar = alumno.TelefonoFamiliar;
                    //FechaNacimiento = alumno.FechaNacimiento,
                }
                alumnoAnterior = alumnoService.ValidarEstadoContactoWhatsAppTemporalAlterno(alumnoAnterior);
                alumnoService.Update(alumnoAnterior);
                alumnoLogService.Add(alumnoLogs);
                //ActualizarAlumno a v3
                try
                {
                    string url = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAlumno_a_v3?IdAlumno=" + alumnoAnterior.Id;
                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(url);
                    }
                }
                catch (Exception e)
                {
                }
                return (alumnoAnterior);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/20222
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel por Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno </param>
        /// <returns> Información de Sentinel por Alumno </returns>
        /// <returns> objetoDTO :  SentinelDatosContactoDTO </returns>
        public SentinelDatosContactoDTO ObtenerDatoSentinelAlumno(int idAlumno)
        {
            try
            {
                ISentinelService sentinelService = new SentinelService(_unitOfWork);
                var sentinelDatosContactoDTO = new SentinelDatosContactoDTO();
                ISentinelSdtLincreItemService sentinelSdtLincreItemService = new SentinelSdtLincreItemService(_unitOfWork);
                ISentinelSdtRepSbsitemService sentinelSdtRepSbsitemService = new SentinelSdtRepSbsitemService(_unitOfWork);
                sentinelDatosContactoDTO = sentinelService.ObtenerDatosAlumnoSentinel(idAlumno);
                if (sentinelDatosContactoDTO.IdSentinel != null)
                {
                    sentinelDatosContactoDTO.lineaCredito = sentinelSdtLincreItemService.ObtenerLineaCreditoPorIdSentinel(sentinelDatosContactoDTO.IdSentinel.Value);
                    sentinelDatosContactoDTO.lineaDeuda = sentinelSdtRepSbsitemService.ObtenerLineaDeuda(sentinelDatosContactoDTO.IdSentinel.Value);
                }
                return (sentinelDatosContactoDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas
        /// </summary>
        /// <param name="filtros">Filtros de busqueda </param>
        /// <returns> Lista de objeto DTO de resumen de programas </returns>
        /// <returns> Lista objeto DTO : CargarInformacionProgramaRespuestaDTO </returns>
        public string ObtenerInformacionPrograma(Dictionary<string, string> filtros)
        {
            try
            {
                IInformacionProgramaService informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var idPGeneral = Convert.ToInt32(filtros["idPGeneral"]);
                var idCodigoPais = Convert.ToInt32(filtros["codigoPais"]);
                var cargarInformacionProgramaRespuestaDTO = informacionProgramaService.CargarInformacionPrograma(idPGeneral, idCodigoPais, 0, 0);

                return cargarInformacionProgramaRespuestaDTO.InformacionPrograma;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas
        /// </summary>
        /// <param name="filtros">Filtros de busqueda </param>
        /// <returns> Lista de objeto DTO de resumen de programas </returns>
        /// <returns> Lista objeto DTO : CargarInformacionProgramaRespuestaDTO </returns>
        public List<ResumenProgramaV2DTO> ObtenerResumenProgramasV2(Dictionary<string, string> filtros)
        {
            try
            {
                IInformacionProgramaService informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                return (informacionProgramaService.CargarResumenProgramasV2(filtros));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="correoReceptor"></param>
        /// <param name="messageId"></param>
        /// <returns>CorreoAlumnoSpeechDTO</returns>
        public CorreoAlumnoSpeechDTO ObtenerCorreosEnviadosSpeech(string correoReceptor, string messageId)
        {
            try
            {
                return _unitOfWork.MandrilRepository.VerCorreoAlumnoSpeech(correoReceptor, messageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de empresas competidoras
        /// IdTipoEmpresa = 1: COMPETIDOR";
        /// IdTipoEmpresa = 0: NO_COMPETIDOR";
        /// </summary>
        public List<ComboDTO> ObtenerCompetidores()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerTodoCompetidores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Devuelve la informacion de los documentos subidos por oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        public List<DocumentoOportunidadInsertadoDTO> ObtenerDocumentosPorIdOportunidad(int idOportunidad)
        {
            try
            {

                IDocumentoOportunidadService documentoOportunidadService = new DocumentoOportunidadService(_unitOfWork);
                var documentoOportunidadInsertadoDTOs = documentoOportunidadService.ObtenerDocumentosPorOportunidad(idOportunidad);
                return (documentoOportunidadInsertadoDTOs);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Speech Bienvenida y Despedida por actividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la ActividadDetalle </param>
        /// <returns> Speech Bienvenida y Despedida por actividad detalle </returns>
        /// <returns> Objeto DTO : SpeechBienvenidaDespedidaDTO </returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdSpeechBienvenidaDespedida(int idActividadDetalle)
        {
            try
            {
                var speechBienvenidaDespedidaDTO = new SpeechBienvenidaDespedidaDTO();
                IPlantillaBaseService plantillaBaseService = new PlantillaBaseService(_unitOfWork);

                var idSpeechBienvenida = plantillaBaseService.ObtenerIdPorNombre("speech");
                speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = plantillaBaseService.ObtenerIdPlantillaSpeechBienvenida(idActividadDetalle, idSpeechBienvenida.Id).IdPlantillaBienvenida;

                var idSpeechDespedida = plantillaBaseService.ObtenerIdPorNombre("Speech Despedida");
                speechBienvenidaDespedidaDTO.IdPlantillaDespedida = plantillaBaseService.ObtenerIdPlantillaSpeechDespedida(idActividadDetalle, idSpeechDespedida.Id).IdPlantillaDespedida;

                return (speechBienvenidaDespedidaDTO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 14/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el registro de configuración de contacto de acuerdo al IdTipoDato.
        /// </summary>
        /// <param name="idTipoDato">Id del Tipo Dato</param>
        /// <returns> Confirmación de envío </returns>
        /// <returns> objeto DTO : ContactoConfiguracionDTO </returns>
        public ContactoConfiguracionDTO ObtenerConfiguracionContacto(int idTipoDato)
        {
            try
            {
                IContactoConfiguracionService contactoConfiguracionService = new ContactoConfiguracionService(_unitOfWork);
                var contactoConfiguracionDTO = contactoConfiguracionService.ObtenerConfiguracionContactoPorIdTipoDato(idTipoDato);
                if (contactoConfiguracionDTO.IdTipoDato != null)
                {
                    return (contactoConfiguracionDTO);
                }
                else
                {
                    throw new BadRequestException("No se encontro dato.");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 14/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el primer registro de configuración de contacto de acuerdo al IdTipoDato.
        /// </summary>
        /// <returns> Confirmación de envío </returns>
        /// <returns> objeto DTO : ReferidoConfiguracionDTO </returns>
        public ReferidoConfiguracionDTO ObtenerConfiguracionReferidos()
        {
            try
            {
                IReferidoConfiguracionService referidoConfiguracionService = new ReferidoConfiguracionService(_unitOfWork);
                return (referidoConfiguracionService.ObtenerConfiguracionReferidos());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Campo fechaFinalizacion de Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la MatriculaCabecera </param>
        /// <returns> FechaFinalizacion de Matricula </returns>
        /// <returns> String </returns>
        public string? ObtenerFechaFinalizacionMatricula(int idMatriculaCabecera)
        {
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var fechaFinalizacion = matriculaCabeceraService.ObtenerFechaFinalizacion(idMatriculaCabecera);
                return (fechaFinalizacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cantidad de Registros de T_MatriculaCabeceraDatosCertificadoMensajes basado en un UserName.
        /// </summary>
        /// <param name="userName">Username de AspNetUsers</param>
        public int ObtenerCantidadMensajesPorUsername(string userName)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerCantidadMensajesPorUsername(userName).Valor.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 08/09/2022
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        public DataCreditoDataDTO ActualizarInformacionDataCredito(string documento, int idAlumno, string usuario)
        {
            try
            {
                IDataCreditoService dataCreditoService = new DataCreditoService(_unitOfWork);
                IDataCreditoBusquedumService dataCreditoBusquedumService = new DataCreditoBusquedumService(_unitOfWork);

                var dataCreditoDataDTO = dataCreditoBusquedumService.ObtenerIdDataCreditoDeAlumnoPorId(idAlumno);
                dataCreditoService.ConsultarAlumnoColombia(documento, dataCreditoDataDTO.ApellidoPaterno, 1, usuario);
                dataCreditoDataDTO = dataCreditoBusquedumService.ObtenerIdDataCreditoDeAlumnoPorId(idAlumno);

                return (dataCreditoDataDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PGeneral para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<PGeneralComboDTO> ObtenerComboProgramaGeneral()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCombo().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Hace el envioSMSOportunidad de sms a los contactos por reprogramacion automatica por dia y ocurrencia
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> bool </returns>
        public bool EnviarIndividualSMSPorOcurrencia(int idOportunidad, int idOcurrencia)
        {
            try
            {

                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                IOcurrenciaService ocurrenciaService = new OcurrenciaService(_unitOfWork);
                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                ISmsConfiguracionEnvioService smsConfiguracionEnvioService = new SmsConfiguracionEnvioService(_unitOfWork);

                int seccionMensaje = 1;
                string mensajeFinal = string.Empty;
                List<string> listaSubMensajeFinal = new List<string>();
                int idPlantilla = 0;

                var smsEnvioAnexoDTO = smsConfiguracionEnvioService.ConfiguracionSmsOportunidad(idOportunidad);

                if (smsEnvioAnexoDTO == null) return (false);

                string urlBase = $"http://{smsEnvioAnexoDTO.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";

                // Validacion de mensaje si ya se le envioSMSOportunidad un sms el dia de hoy a esa oportunidad
                var envioSMSOportunidad = alumnoService.Obtener_EnvioSMSPorDiaOportunidad(idOportunidad, DateTime.Now);

                var ocurrencia = ocurrenciaService.ObtenerPorId(idOcurrencia);

                if (envioSMSOportunidad.Id == null && ocurrencia.IdEstadoOcurrencia == 2)
                {
                    var oportunidadDiasSinContactoDTO = smsConfiguracionEnvioService.ObtenerDiasSinContacto(idOportunidad);

                    /*Definicion de plantilla*/
                    switch (oportunidadDiasSinContactoDTO.DiasSinContacto)
                    {
                        case 1:
                            idPlantilla = 1458;//ValorEstatico.IdRecordatorioSms02;
                            break;
                        case 2:
                            idPlantilla = 1459;//ValorEstatico.IdRecordatorioSms03;
                            break;
                        case 3:
                            idPlantilla = 1460;//ValorEstatico.IdRecordatorioSms04;
                            break;
                        default:
                            idPlantilla = 0;
                            break;
                    }
                }
                idPlantilla = 1458; // Borrar: Solo para Pruebas
                if (idPlantilla > 0)
                {
                    ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                    {
                        IdOportunidad = idOportunidad,
                        IdPlantilla = idPlantilla
                    };
                    var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta);

                    string[] palabras = resultadoReemplazo.SmsReemplazado.Cuerpo.Split(' ');

                    foreach (string palabra in palabras)
                    {
                        if ((mensajeFinal + " " + palabra).Length <= 128)
                            mensajeFinal += " " + palabra;
                        else
                        {
                            listaSubMensajeFinal.Add(mensajeFinal.Trim());
                            mensajeFinal = palabra;
                        }
                    }

                    listaSubMensajeFinal.Add(mensajeFinal.Trim());
                    mensajeFinal = string.Empty;

                    foreach (string mensajeAEnviar in listaSubMensajeFinal)
                    {
                        string url = $"{urlBase}{smsEnvioAnexoDTO.Celular}&message={mensajeAEnviar.Replace(" ", "%20")}&[port={smsEnvioAnexoDTO.Tipo}-{smsEnvioAnexoDTO.Puerto}&][report=String&][timeout=5&][id=1]";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(url);
                        }

                        alumnoService.InsertaSMSOportunidadUsuario(smsEnvioAnexoDTO.Celular, smsEnvioAnexoDTO.IdPersonal, smsEnvioAnexoDTO.IdAlumno, mensajeAEnviar, seccionMensaje, smsEnvioAnexoDTO.IdCodigoPais.GetValueOrDefault(), "EnvioOcurrencia");

                        seccionMensaje++;
                    }

                    var insertado = alumnoService.InsertaSMSOportunidad(idOportunidad, DateTime.Now);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public ReporteIncidenciaDTO CargarReporteIncidencia(FiltroReporteIncidenciaDTO filtro)
        {
            try
            {
                ReporteIncidenciaDTO resultado = new ReporteIncidenciaDTO();
                resultado.ArbolOcurrencia = new List<ArbolOcurenciaAlternoDTO>();

                bool validarRN2 = _unitOfWork.OportunidadRepository.ValidarRN2(filtro.IdContacto, filtro.IdCentroCosto, filtro.IdOcurrenciaReporte);
                if (!validarRN2 && filtro.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2)
                {
                    throw new ConflictException("Existen al menos una oportunidad en seguimiento");
                }
                else
                {
                    var plantillas = _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.ObtenerPorIdOcurrenciaActividad(filtro.IdActividadOcurrencia);

                    if (plantillas.Count() > 0)
                    {
                        resultado.plantillaAutomaticaWhatsapp = plantillas.Where(x => x.NumeroDiasSinContacto == filtro.DiasSinContactoOportunidad).FirstOrDefault();
                    }
                    if (filtro.TieneOcurrencias)
                    {
                        resultado.ArbolOcurrencia = _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerArbolOcurrenciaAlterno(filtro.IdActividadCabecera, filtro.IdActividadOcurrencia).ToList();
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SolciitudBeneficioDTO AprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudAprobado)
        {
            SolciitudBeneficioDTO response = new SolciitudBeneficioDTO();
            try
            {
                string mensaje = string.Empty;
                var matriculaCabeceraBeneficio = _unitOfWork.MatriculaCabeceraBeneficiosRepository.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null && IdEstadoSolicitudAprobado > 0)
                {
                    matriculaCabeceraBeneficio.UsuarioAprobacion = Usuario;
                    matriculaCabeceraBeneficio.FechaAprobacion = DateTime.Now;
                    matriculaCabeceraBeneficio.FechaProgramada = DateTime.Now.AddDays(30);
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoSolicitudBeneficio = IdEstadoSolicitudAprobado; //4 Aprobar Solicitud
                    _unitOfWork.MatriculaCabeceraBeneficiosRepository.Update(matriculaCabeceraBeneficio);
                    _unitOfWork.Commit();
                    response.Respuesta = true;
                    response.Mensaje = "Se Aprobó la solicitud correctamente";
                    return (response);
                }
                else
                {
                    mensaje = "No se encontró el beneficio solicitado";
                    response.Respuesta = false;
                    return (response);
                }
            }
            catch
            {
                response.Respuesta = false;
                response.Mensaje = "Erro al aprobar la solicitud beneficios";
                return (response);
            }

        }
        public SolciitudBeneficioDTO RechazarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudRechazado, int IdEstadoMatriculaCabeceraBeneficio)
        {

            SolciitudBeneficioDTO response = new SolciitudBeneficioDTO();
            try
            {
                string mensaje = string.Empty;
                var matriculaCabeceraBeneficio = _unitOfWork.MatriculaCabeceraBeneficiosRepository.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null && IdEstadoSolicitudRechazado > 0)
                {
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoSolicitudBeneficio = IdEstadoSolicitudRechazado; //5 Rechazar Solicitud
                    matriculaCabeceraBeneficio.IdEstadoMatriculaCabeceraBeneficio = IdEstadoMatriculaCabeceraBeneficio; //3 Rechazado
                    _unitOfWork.MatriculaCabeceraBeneficiosRepository.Update(matriculaCabeceraBeneficio);
                    _unitOfWork.Commit();
                    response.Mensaje = "Se guardó correctamente";
                    response.Respuesta = true;
                    return response;
                }
                else
                {
                    response.Mensaje = "No se encontró el beneficio solicitado";
                    response.Respuesta = false;
                    return response;
                }
            }
            catch
            {
                response.Respuesta = false;
                response.Mensaje = "Erro al aprobar la solicitud beneficios";
                return (response);
            }
        }
        public SolciitudBeneficioDTO RestablecerSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario)
        {
            SolciitudBeneficioDTO response = new SolciitudBeneficioDTO();
            try
            {
                string mensaje = string.Empty;
                var matriculaCabeceraBeneficio = _unitOfWork.MatriculaCabeceraBeneficiosRepository.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null)
                {
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoSolicitudBeneficio = null;
                    matriculaCabeceraBeneficio.IdEstadoMatriculaCabeceraBeneficio = 1;
                    matriculaCabeceraBeneficio.FechaAprobacion = null;
                    matriculaCabeceraBeneficio.FechaSolicitud = null;
                    matriculaCabeceraBeneficio.FechaProgramada = null;
                    _unitOfWork.MatriculaCabeceraBeneficiosRepository.Update(matriculaCabeceraBeneficio);
                    _unitOfWork.Commit();
                    response.Respuesta = true;
                    response.Mensaje = "Se guardó correctamente";
                    return (response);
                }
                else
                {

                    response.Respuesta = false;
                    response.Mensaje = "No se encontró el beneficio solicitado";
                    return (response);
                }

            }
            catch
            {
                response.Respuesta = false;
                response.Mensaje = "Erro al aprobar la solicitud beneficios";
                return (response);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 15/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: ValorEtiquetaWhatsAppDTO </returns>
        public ValorEtiquetaWhatsAppDTO ObtenerValoresEtiquetaWhatsapp(int idOportunidad)
        {
            try
            {
                var resultado = _unitOfWork.OportunidadRepository.ObteneValoresEtiquetaWhatsApp(idOportunidad);
                return resultado;
            }
            catch (Exception)
            {
                throw;
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
            return _unitOfWork.OportunidadRepository.ObtenerReporteControlActividadesAgenda(idAsesor);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 12/03/2024
        /// Version: 1.0
        /// <summary>
        /// BuscarFichaPorCelular
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> Lista ResultadoBusquedaFichaAlumnoDTO </returns>
        public List<ResultadoBusquedaFichaAlumnoDTO> BuscarFichaPorCelular(string celular)
        {
            try
            {
                var resultado = new List<ResultadoBusquedaFichaAlumnoDTO>();
                var celularLimpio = celular.Trim();
                //if (celularLimpio.StartsWith("92357"))
                //{
                //    celularLimpio = celularLimpio.Substring(5);
                //}
                //if (celularLimpio.StartsWith("090151"))
                //{
                //    celularLimpio = celularLimpio.Substring(6);
                //}
                celularLimpio = new string(celularLimpio.Where(char.IsDigit).ToArray());
                if (celularLimpio == null)
                {
                    return resultado;
                }
                celularLimpio = celularLimpio.TrimStart('0');

                if (celularLimpio.StartsWith("51")
                    || celularLimpio.StartsWith("56")
                    || celularLimpio.StartsWith("57")
                    || celularLimpio.StartsWith("58")
                    )
                {
                    celularLimpio = celularLimpio.Substring(2);
                }
                else if (celularLimpio.StartsWith("591") || celularLimpio.StartsWith("521"))
                {
                    celularLimpio = celularLimpio.Substring(3);
                }
                else if (celularLimpio.StartsWith("52"))
                {
                    celularLimpio = celularLimpio.Substring(2);
                }

                if (celularLimpio.Length < 5)
                {
                    return resultado;
                }
                resultado = _unitOfWork.OportunidadRepository.BuscarFichaPorCelular(celularLimpio);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 27/05/2024
        /// Version: 1.0
        /// <summary>
        /// BuscarIdAgenteporCelular
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> Lista ResultadoBusquedaFichaAlumnoDTO </returns>
        public int ObtenerIdSkillPorCelular(string celular)
        {
            try
            {
                var resultado = 0;
                var celularLimpio = celular.Trim();
                celularLimpio = new string(celularLimpio.Where(char.IsDigit).ToArray());
                if (celularLimpio == null)
                {
                    return resultado;
                }
                celularLimpio = celularLimpio.TrimStart('0');

                if (celularLimpio.StartsWith("51")
                    || celularLimpio.StartsWith("56")
                    || celularLimpio.StartsWith("57")
                    || celularLimpio.StartsWith("58")
                    )
                {
                    celularLimpio = celularLimpio.Substring(2);
                }
                else if (celularLimpio.StartsWith("591") || celularLimpio.StartsWith("521"))
                {
                    celularLimpio = celularLimpio.Substring(3);
                }
                if (celularLimpio.Length < 5)
                {
                    return resultado;
                }


                var lista = _unitOfWork.OportunidadRepository.BuscarFichaPorCelular(celularLimpio);

                if (lista.Count() == 0)
                {
                    return resultado;
                }
                else if (lista.Count() >= 1)
                {
                    resultado = lista.First().IdSkill == null ? 0 : lista.First().IdSkill.Value;
                }


                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PrecioFinalProgramaAlumnoDTO ObtenerPrecioFinalProgramaAlumno(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.CronogramaPagoRepository.ObtenerPrecioFinalProgramaAlumno(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ColorPerfilProgramaDTO> ObtenerColorPerfilProgramaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerColorPerfilProgramaPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 01-10-2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Argumentos de Programa General y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoDetallePorIdOportunidad(int idOportunidad)
        {
            try
            {
                IProgramaGeneralPresentacionArgumentoService programaGeneralPresentacionArgumentoService = new ProgramaGeneralPresentacionArgumentoService(_unitOfWork);
                IProgramaGeneralPresentacionArgumentoDetalleSolucionService programaGeneralPresentacionArgumentoDetalleSolucionService = new ProgramaGeneralPresentacionArgumentoDetalleSolucionService(_unitOfWork);

                var programaGeneralPresentacionArgumentoAgendaDTOs = programaGeneralPresentacionArgumentoService.ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(idOportunidad);

                var programaGeneralPresentacionArgumentoDetalleAgendaDTOs = _mapper.Map<List<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO>>(programaGeneralPresentacionArgumentoAgendaDTOs);
                programaGeneralPresentacionArgumentoDetalleAgendaDTOs.ForEach(
                    p => p.Argumentos = programaGeneralPresentacionArgumentoDetalleSolucionService.ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucionParaAgenda(p.IdPresentacionArgumento, idOportunidad).ToList()
                );
                return programaGeneralPresentacionArgumentoDetalleAgendaDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}