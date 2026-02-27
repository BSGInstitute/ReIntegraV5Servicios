using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class CampaniaRemarketingGeneralService : ICampaniaRemarketingGeneralService
    {
        private readonly ICampaniaRemarketingGeneralRepository _campaniaRemarketingGeneralRepository;

        public CampaniaRemarketingGeneralService(ICampaniaRemarketingGeneralRepository campaniaRemarketingGeneralRepository)
        {
            _campaniaRemarketingGeneralRepository = campaniaRemarketingGeneralRepository;
        }

        public List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania()
        {
            return _campaniaRemarketingGeneralRepository.ObtenerListadoCampania();
        }

        /// Autor: Humberto Oscata
        /// Fecha: 13/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos de rendimiento agrupados por fecha para un listado de campañas
        /// y construye el objeto de respuesta con capacidad de entrega y tasas
        /// </summary>
        /// <param name="ids">Lista de IDs de campañas remarketing</param>
        /// <returns>Rendimiento con capacidad de entrega y tasas</returns>
        public RendimientoCampaniaDTO ObtenerRendimientoListadoCampanias(List<int> ids)
        {
            var datosDiarios = _campaniaRemarketingGeneralRepository.ObtenerRendimientoCampanias(ids)
                .OrderBy(d => d.Fecha).ToList();

            var labels = datosDiarios.Select(d => d.Fecha.ToString("yyyy-MM-dd")).ToList();
            var enviados = datosDiarios.Select(d => d.Enviados).ToList();
            var rebotados = datosDiarios.Select(d => d.Rebotados).ToList();
            var rechazados = datosDiarios.Select(d => d.Rechazados).ToList();
            var abiertos = datosDiarios.Select(d => d.Abiertos).ToList();
            var clicks = datosDiarios.Select(d => d.Clicks).ToList();

            var totalProgramados = datosDiarios.Sum(d => d.TotalRegistros);
            var totalEnviados = datosDiarios.Sum(d => d.Enviados);
            var cantidadApertura = datosDiarios.Sum(d => d.Abiertos);
            var cantidadClicks = datosDiarios.Sum(d => d.Clicks);

            return new RendimientoCampaniaDTO
            {
                CapacidadEntrega = new CapacidadEntregaDTO
                {
                    Labels = labels,
                    Enviados = enviados,
                    Rebotados = rebotados,
                    Rechazados = rechazados,
                    TotalProgramados = totalProgramados,
                    TotalEnviados = totalEnviados,
                    PorcentajeEnviados = totalProgramados > 0
                        ? Math.Round((double)totalEnviados / totalProgramados * 100, 1)
                        : 0
                },
                Tasas = new TasasRendimientoDTO
                {
                    Labels = labels,
                    Abiertos = abiertos,
                    Clicks = clicks,
                    TasaApertura = totalEnviados > 0
                        ? Math.Round((double)cantidadApertura / totalEnviados * 100, 1)
                        : 0,
                    CantidadApertura = cantidadApertura,
                    TasaClicks = totalEnviados > 0
                        ? Math.Round((double)cantidadClicks / totalEnviados * 100, 1)
                        : 0,
                    CantidadClicks = cantidadClicks
                }
            };
        }

        public List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados()
        {
            return _campaniaRemarketingGeneralRepository.ObtenerListadoSegmentosCreados();
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos con informacion basica para la creacion de campanias
        /// </summary>
        /// <returns>Objeto con listado de combos</returns>
        public CombosConfiguracionCampaniaDTO ObtenerCombosConfiguracionCampania()
        {
            var medioEnvio = _campaniaRemarketingGeneralRepository.ObtenerMediosEnvio();
            var tipoMensaje = _campaniaRemarketingGeneralRepository.ObtenerTiposMensaje();
            var logicaEnvio = _campaniaRemarketingGeneralRepository.ObtenerLogicasEnvio();
            var categoriaArgumento = _campaniaRemarketingGeneralRepository.ObtenerCategoriaArgumento();
            var prioridadesUnicas = _campaniaRemarketingGeneralRepository.ObtenerPrioridadesUnicas();

            return new CombosConfiguracionCampaniaDTO
            {
                MedioEnvio = medioEnvio,
                TipoMensaje = tipoMensaje,
                LogicaEnvio = logicaEnvio,
                CategoriaArgumento = categoriaArgumento,
                PrioridadesUnicas = prioridadesUnicas,
            };
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalles del estado de ejecucion para una llamada, ademas de los mensajes generados hasta el momento
        /// </summary>
        /// <param name="idLlamadaIA">ID de la llamada a la IA para obtener mensajes</param>
        /// <returns>Detalles de estado ejecucion y listado de mensajes generados</returns>
        public async Task<EstadoEjecucionLlamadaIA> ObtenerResultadosGeneracionTextoPorCampania(string idLlamadaIA)
        {
            //Llamada a la api IA para saber el estado de la llamada
            var estadoEjecucionLlamadaIA = await ObtenerEstadoEjecucionLlamada(idLlamadaIA);

            //Llamada a la api IA para obtener el listado de mensajes generados
            var listaMensajesGeneradoIA = await ObtenerMensajesGeneradosPorIdLlamadaIA(idLlamadaIA, false);

            estadoEjecucionLlamadaIA.mensajesGenerados = listaMensajesGeneradoIA;

            return estadoEjecucionLlamadaIA;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza en base de datos una campaña y evalua sus caracteristicas para ejecutar su envio programado
        /// </summary>
        /// <returns>Estado de la programacion y/o ejecucion</returns>
        public async Task<bool> ActualizarEjecutarEnvioCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request, string usuario)
        {
            request.UsuarioCreacion = usuario;
            if (request.FechaEnvio == null)
                request.FechaEnvio = DateTime.Now;

            //Validar que la campaña configurada no tenga un envio en curso
            var estadoEnvio = _campaniaRemarketingGeneralRepository.ObtenerEstadoEnvioCampaniaRemarketing(request.Id ?? 0);
            if (estadoEnvio.IdEstadoEnvio == 4) //Envio en progreso
                return false;

            //Validar que no haya una generacion de mensajes IA en curso
            var estadoEjecucionLlamadaIA = await ObtenerEstadoEjecucionLlamada(request.IdentificadorLlamadaIA);
            if (estadoEjecucionLlamadaIA.pendientes > 0)
                return false;

            //Guardar los datos de configuracion de la campaña
            var respuesta = _campaniaRemarketingGeneralRepository.ActualizarCampaniaRemarketing(request);
            if (!respuesta)
                return false;

            //Iniciar o programar segun el tipo de envio
            if (request.MediosEnvio[0].Nombre.ToUpper() == "CORREO ELECTRÓNICO")
            {
                switch (request.EnvioSeleccionado.ToUpper())
                {
                    case "ENVIAR AHORA":
                        //Se ejecuta el envio masivo de correos en segundo plano
                        if (request.Id.HasValue && !string.IsNullOrEmpty(request.IdentificadorLlamadaIA))
                        {
                            _ = Task.Run(async () =>
                            {
                                try
                                {
                                    await EjecutarEnvioCampaniaRemarketing(request, usuario, false);
                                }
                                catch (Exception ex)
                                {
                                    _campaniaRemarketingGeneralRepository.ActualizarEstadoEnvioCampania(request.Id ?? 0, 6, usuario);
                                }
                            });
                        }
                        break;

                    case "PROGRAMADO":
                        //Se programa el envio
                        break;

                    default:
                        break;
                }
            }

            return respuesta;
        }

        public DetallesCampaniaDTO VerDetallesCampania(int idCampania)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerDetallesGeneralesEnvio(idCampania);
        }

        public CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerCampaniaRemarketingPorId(id);
        }

        public bool EliminarCampania(int id, string usuario)
        {
            return _campaniaRemarketingGeneralRepository.EliminarCampania(id, usuario);
        }

        public async Task<List<MensajeGeneradoIA>> ObtenerMensajeGeneradoPorId(string identificadorLlamadaIA, int idAlumno)
        {
            return await ObtenerMensajeGeneradoPorIdLLamadaAlumno(identificadorLlamadaIA, idAlumno, false);
        }

        public async Task<bool> ReenviarMensajeGenerado(ReenviarMensajeRequest request)
        {
            //Obtenemos el ulitmo mensaje generado para un alumno en una campania especifica
            var mensajesGenerados = await ObtenerMensajeGeneradoPorIdLLamadaAlumno(request.IdentificadorLlamadaIA, request.IdAlumno, false);
            var ultimoMensaje = mensajesGenerados[0];

            //Obtenemos el correo del alumno


            //Reenviamos el mensaje


            //Actualizamos estado envio en base de datos

            return true;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene alumnos, llama al metodo para generacion de envios y guarda la configuracion de la campania
        /// </summary>
        /// <returns>Estado de ejecucion</returns>
        public async Task<bool> GenerarListadoTextosRemarketing(ConfiguracionCampaniaRemarketingDTO request, string usuario)
        {
            request.UsuarioCreacion = usuario;
            if (request.FechaEnvio == null)
                request.FechaEnvio = DateTime.Now;

            //Validar que no haya una ejecucion en curso
            var estadoEjecucionLlamadaIA = await ObtenerEstadoEjecucionLlamada(request.IdentificadorLlamadaIA);

            if (estadoEjecucionLlamadaIA.pendientes > 0)
                return false;

            //Obtener listado de alumnos del segmento, para el envio
            var alumnosCorreos = _campaniaRemarketingGeneralRepository.ObtenerAlumnosCorreosPorFiltroSegmento(request.Segmento.Id);
            List<int> idsAlumno = alumnosCorreos.Select(x => x.IdAlumno).Distinct().ToList();

            //Llamada a la api IA para generar los textos
            RespuestaIdentificadorLlamadaIA resultado = new RespuestaIdentificadorLlamadaIA();
            resultado = await GenerarMensajesIAPorListaAlumnos(request.MediosEnvio[0].Nombre, request.TipoMensaje.Nombre, request.LogicaEnvio.Nombre,
                                                                request.CategoriaArgumento.Nombre, idsAlumno, request.Prioridades);

            request.IdentificadorLlamadaIA = resultado.id_llamada;

            //Guardar o Actualizar los datos de la campaña iniciada
            var respuesta = (request.FlagEditar == true)
                ? _campaniaRemarketingGeneralRepository.ActualizarCampaniaRemarketing(request)
                : _campaniaRemarketingGeneralRepository.InsertarCampaniaRemarketing(request);

            return respuesta;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 10/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el envío masivo de correos para una campaña de remarketing
        /// Utiliza TMK_MailService para el envío mediante Mandrill
        /// </summary>
        /// <param name="request">Detalles configurados para la campaña a enviar</param>
        /// <param name="usuario">Usuario que ejecuta la operación</param>
        /// <param name="argumentos">Boolean para obtener mensajes con o sin argumentos</param>
        /// <returns>Resultado del envío masivo</returns>
        public async Task<ResultadoEnvioMasivoDTO> EjecutarEnvioCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request, string usuario, bool argumentos)
        {
            var resultado = new ResultadoEnvioMasivoDTO
            {
                Detalle = new List<RemarketingEstadoCampaniaDTO>()
            };

            try
            {
                // 1. Obtener listado de alumnos con sus correos del segmento
                var alumnosCorreos = _campaniaRemarketingGeneralRepository.ObtenerAlumnosCorreosPorFiltroSegmento(request.Segmento.Id);

                if (alumnosCorreos == null || !alumnosCorreos.Any())
                    throw new Exception("No se encontraron alumnos para el segmento especificado");

                // 2. Obtener los mensajes generados por la IA
                var mensajesGenerados = await ObtenerMensajesGeneradosPorIdLlamadaIA(request.IdentificadorLlamadaIA, argumentos);

                if (mensajesGenerados == null || !mensajesGenerados.Any())
                    throw new Exception("No se encontraron mensajes generados para la llamada especificada");

                // 3. Crear diccionario de mensajes por IdAlumno para acceso rápido
                var mensajesPorAlumno = mensajesGenerados.ToDictionary(m => m.id_alumno, m => m);

                // 4. Obtener canvas de contenido adicional para la campaña
                var canvas = _campaniaRemarketingGeneralRepository.ObtenerCampaniaCanvas(request.Id ?? 0);

                // 5. Actualizar registro para marcar envio de campaña como Iniciada
                _campaniaRemarketingGeneralRepository.ActualizarEstadoEnvioCampania(request.Id ?? 0, 4, usuario);


                // 6. Procesar cada alumno
                var mailService = new TMK_MailService();
                foreach (var alumno in alumnosCorreos)
                {
                    var estadoEnvio = new RemarketingEstadoCampaniaDTO
                    {
                        IdCampaniaRemarketing = request.Id ?? 0,
                        IdAlumno = alumno.IdAlumno,
                        //Email = alumno.Correo,
                        UsuarioCreacion = usuario,
                        Enviado = false,
                        Entregado = false,
                        Abierto = false,
                        Rebotado = false
                    };

                    try
                    {
                        // Verificar si existe mensaje para este alumno
                        if (!mensajesPorAlumno.ContainsKey(alumno.IdAlumno))
                        {
                            estadoEnvio.EstadoMandrill = "sin_mensaje";
                            estadoEnvio.RazonRechazo = "No se encontró mensaje generado para este alumno";
                            resultado.Detalle.Add(estadoEnvio);
                            continue;
                        }

                        var mensajeAlumno = mensajesPorAlumno[alumno.IdAlumno];

                        // Preparar datos para el envío usando TMK_MailService
                        var mailData = new TMKMailDataDTO
                        {
                            Sender = request.RemitenteCorreo,
                            RemitenteC = request.RemitenteNombre,
                            Recipient = alumno.Correo,
                            Subject = request.Asunto,
                            Message = ConstruirContenidoConCanvas(canvas, mensajeAlumno.contenido)
                        };

                        mailService.SetData(mailData);

                        // Enviar correo y obtener resultado
                        var resultadoEnvio = mailService.SendMessageTask();

                        if (resultadoEnvio != null && resultadoEnvio.Any())
                        {
                            var primerResultado = resultadoEnvio.First();
                            estadoEnvio.IdentificadorMensaje = primerResultado.MensajeId;
                            estadoEnvio.EstadoMandrill = primerResultado.Estado;

                            // Interpretar el estado de Mandrill
                            switch (primerResultado.Estado?.ToLower())
                            {
                                case "sent":
                                case "queued":
                                    estadoEnvio.Enviado = true;
                                    resultado.TotalEnviados++;
                                    break;
                                case "rejected":
                                    estadoEnvio.Rebotado = true;
                                    estadoEnvio.RazonRechazo = "rejected";
                                    resultado.TotalRechazados++;
                                    break;
                                case "invalid":
                                    estadoEnvio.RazonRechazo = "email_invalido";
                                    resultado.TotalInvalidos++;
                                    break;
                                default:
                                    estadoEnvio.RazonRechazo = primerResultado.Estado;
                                    break;
                            }
                        }
                    }
                    catch (Exception exEnvio)
                    {
                        estadoEnvio.EstadoMandrill = "error";
                        estadoEnvio.RazonRechazo = exEnvio.Message;
                    }

                    resultado.Detalle.Add(estadoEnvio);
                    resultado.TotalProcesados++;
                }

                // 7. Guardar todos los estados en la base de datos
                _campaniaRemarketingGeneralRepository.InsertarEstadosEnvioCampaniaMasivo(resultado.Detalle);

                // 8. Actualizar registro para marcar envio de campaña como Finalizada
                _campaniaRemarketingGeneralRepository.ActualizarEstadoEnvioCampania(request.Id ?? 0, 2, usuario);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al ejecutar envío masivo: {ex.Message}");
            }
        }


        /// Autor: Humberto Oscata
        /// Fecha: 13/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Método invocado por el worker cada minuto para ejecutar campañas programadas.
        /// Consulta si existen campañas cuya fecha programada coincide con el minuto actual,
        /// realiza las validaciones necesarias y ejecuta el envío en segundo plano.
        /// </summary>
        public async Task EjecutarCampaniasProgramadas()
        {
            var campaniasProgramadas = _campaniaRemarketingGeneralRepository.ObtenerCampaniasProgramadasParaEjecutar();

            if (campaniasProgramadas == null || !campaniasProgramadas.Any())
                return;

            foreach (var campania in campaniasProgramadas)
            {
                //Validar que la campaña no tenga un envío en curso
                var estadoEnvio = _campaniaRemarketingGeneralRepository.ObtenerEstadoEnvioCampaniaRemarketing(campania.Id);
                if (estadoEnvio.IdEstadoEnvio == 4) //Envio en progreso
                    continue;

                //Validar que no haya una generación de mensajes IA en curso
                var estadoEjecucionLlamadaIA = await ObtenerEstadoEjecucionLlamada(campania.IdentificadorLlamadaIA);
                if (estadoEjecucionLlamadaIA.pendientes > 0)
                    continue;

                //Construir el request con los datos de la campaña programada
                var request = new ConfiguracionCampaniaRemarketingDTO
                {
                    Id = campania.Id,
                    Segmento = new SegmentoDTO { Id = campania.IdFiltroSegmento, Nombre = campania.NombreSegmento },
                    IdentificadorLlamadaIA = campania.IdentificadorLlamadaIA,
                    RemitenteCorreo = campania.RemitenteCorreo,
                    RemitenteNombre = campania.RemitenteNombre,
                    Asunto = campania.Asunto
                };

                //Ejecutar envío en segundo plano
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await EjecutarEnvioCampaniaRemarketing(request, "WORKER_SISTEMA", false);
                    }
                    catch (Exception ex)
                    {
                        _campaniaRemarketingGeneralRepository.ActualizarEstadoEnvioCampania(campania.Id, 6, "WORKER_SISTEMA");
                    }
                });
            }
        }

        public bool InsertarCampaniaCanvas(CampaniaCanvasDTO request, string usuario)
        {
            return _campaniaRemarketingGeneralRepository.InsertarCampaniaCanvas(request, usuario);
        }

        public bool ActualizarCampaniaCanvas(CampaniaCanvasDTO request, string usuario)
        {
            return _campaniaRemarketingGeneralRepository.ActualizarCampaniaCanvas(request, usuario);
        }

        public CampaniaCanvasDTO ObtenerCampaniaCanvas(int idRemarketingCampaniaGeneral)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerCampaniaCanvas(idRemarketingCampaniaGeneral);
        }

        public bool EliminarCampaniaCanvas(int idRemarketingCampaniaGeneral, string usuario)
        {
            return _campaniaRemarketingGeneralRepository.EliminarCampaniaCanvas(idRemarketingCampaniaGeneral, usuario);
        }

        private string ConstruirContenidoConCanvas(CampaniaCanvasDTO canvas, string contenidoIA)
        {
            if (canvas == null)
                return contenidoIA;

            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(canvas.ContenidoSuperior))
                sb.Append(canvas.ContenidoSuperior);

            sb.Append(contenidoIA);

            if (!string.IsNullOrEmpty(canvas.ContenidoInferior))
                sb.Append(canvas.ContenidoInferior);

            return sb.ToString();
        }

        #region Metodos de interaccion con API IA

        /// Autor: Humberto Oscata
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Inicia la generacion de mensajes por la api IA para un listado de alumnos
        /// </summary>
        /// <returns>Identificador de la llamada IA</returns>
        public async Task<RespuestaIdentificadorLlamadaIA> GenerarMensajesIAPorListaAlumnos(string canal, string tipoMensaje, string logicaEnvio,
                                                                                            string categoriaArgumento, List<int> idsAlumno, List<int> versionesArgumento)
        {
            string tipoMensajeTransformado = tipoMensaje.ToUpper().Replace(" ", "_");
            string logicaEnvioTransformado = logicaEnvio.ToUpper().Replace(" ", "_");

            //string baseUrl = "http://ia-remarketing-api.bsginstitute.com/testing/api/generacion_mensaje/generar_mensaje_mult";
            string baseUrl = "http://ia-remarketing-api.bsginstitute.com/api/generacion_mensaje/generar_mensaje_mult";

            string url = $"{baseUrl}?" +
                 $"canal={Uri.EscapeDataString(canal)}&" +
                 $"tipo_mensaje={Uri.EscapeDataString(tipoMensajeTransformado)}&" +
                 $"logica_envio={Uri.EscapeDataString(logicaEnvioTransformado)}&" +
                 $"categoria_argumento={Uri.EscapeDataString(categoriaArgumento)}";

            var body = new
            {
                ids_alumno = idsAlumno,
                argumentos_a_usar = versionesArgumento ?? new List<int>()
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al llamar al API externa: {responseContent}");

                var resultado = JsonConvert.DeserializeObject<RespuestaIdentificadorLlamadaIA>(responseContent);

                if (resultado == null)
                    throw new Exception("La respuesta de la API externa fue nula o inválida.");

                return resultado;
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 10/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes generados por la IA para el envío
        /// </summary>
        /// <param name="idLlamadaIA">Identificador de la llamada a la IA</param>
        /// <param name="argumentos">Boolean para definir si el metodo devolvera argumentos</param>
        /// <returns>Lista de mensajes generados</returns>
        public async Task<List<MensajeGeneradoIA>> ObtenerMensajesGeneradosPorIdLlamadaIA(string idLlamadaIA, bool argumentos)
        {
            //string url = $"http://ia-remarketing-api.bsginstitute.com/testing/api/generacion_mensaje/consulta_mensaje_llamada?id_llamada={idLlamadaIA}&con_argumentos={argumentos}";
            string url = $"http://ia-remarketing-api.bsginstitute.com/api/generacion_mensaje/consulta_mensaje_llamada?id_llamada={idLlamadaIA}&con_argumentos={argumentos}";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, null);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al obtener mensajes para envío: {responseContent}");

                var resultado = JsonConvert.DeserializeObject<List<MensajeGeneradoIA>>(responseContent);

                if (resultado == null)
                    return new List<MensajeGeneradoIA>();

                return resultado;
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el estado de ejecucion de una llamada a la api IA
        /// </summary>
        /// <returns>Detalles estado ejecucion</returns>
        public async Task<EstadoEjecucionLlamadaIA> ObtenerEstadoEjecucionLlamada(string idLlamadaIA)
        {
            try
            {
                //string url = $"http://ia-remarketing-api.bsginstitute.com/testing/api/generacion_mensaje/estado_ejecucion_llamada?id_llamada={idLlamadaIA}";
                string url = $"http://ia-remarketing-api.bsginstitute.com/api/generacion_mensaje/estado_ejecucion_llamada?id_llamada={idLlamadaIA}";

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, null);

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al llamar al API externa: {responseContent}");

                    var resultado = JsonConvert.DeserializeObject<EstadoEjecucionLlamadaIA>(responseContent);

                    if (resultado == null)
                        throw new Exception("La respuesta de la API externa fue nula o inválida.");

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al ejecutar envío masivo: {ex.Message}");
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 12/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un mensaje generado por por llamada a la api IA para un alumno especifico
        /// </summary>
        /// <returns>Mensaje generado por IA</returns>
        public async Task<List<MensajeGeneradoIA>> ObtenerMensajeGeneradoPorIdLLamadaAlumno(string identificadorLlamadaIA, int idAlumno, bool argumentos)
        {
            try
            {
                //string url = $"http://ia-remarketing-api.bsginstitute.com/testing/api/generacion_mensaje/consulta_mensaje_alumno?id_alumno={idAlumno}&id_llamada={identificadorLlamadaIA}&con_argumentos={argumentos}";
                string url = $"http://ia-remarketing-api.bsginstitute.com/api/generacion_mensaje/consulta_mensaje_alumno?id_alumno={idAlumno}&id_llamada={identificadorLlamadaIA}&con_argumentos={argumentos}";

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, null);

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al llamar al API externa: {responseContent}");

                    var resultado = JsonConvert.DeserializeObject<List<MensajeGeneradoIA>>(responseContent);

                    if (resultado == null)
                        throw new Exception("La respuesta de la API externa fue nula o inválida.");

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al ejecutar envío masivo: {ex.Message}");
            }
        }


        #endregion
    }
}
