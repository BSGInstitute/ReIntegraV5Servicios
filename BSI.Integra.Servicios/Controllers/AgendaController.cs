using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AgendaController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión de Agenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class AgendaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public AgendaController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los filtros necesario para Agenda
        /// </summary>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var estadoOcurrenciaServicio = new EstadoOcurrenciaService(_unitOfWork);
                var faseOportunidadServicio = new FaseOportunidadService(_unitOfWork);
                var tipoDatodServicio = new TipoDatoService(_unitOfWork);
                var origendServicio = new OrigenService(_unitOfWork);
                var probabilidadRegistroPwServicio = new ProbabilidadRegistroPwService(_unitOfWork);
                var categoriaOrigenServicio = new CategoriaOrigenService(_unitOfWork);
                AgendaFiltroDTO agendaFiltro = new AgendaFiltroDTO();
                agendaFiltro.listaEstadoOcurrencia = estadoOcurrenciaServicio.ObtenerCombo();
                agendaFiltro.listaFaseOportunidad = faseOportunidadServicio.ObtenerCombo();
                agendaFiltro.listaTipoDato = tipoDatodServicio.ObtenerCombo();
                agendaFiltro.listaOrigen = origendServicio.ObtenerCombo();
                agendaFiltro.listaProbabilidadRegistro = probabilidadRegistroPwServicio.ObtenerCombo();
                agendaFiltro.listaCategoriaOrigen = categoriaOrigenServicio.ObtenerCombo();
                return Ok(agendaFiltro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el contenido de plantillas con ciertos filtros por defecto.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPlantillasMailing()
        {
            try
            {
                var plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                var plantillasMailing = plantillaClaveValorService.ObtenerPlantillasMailing();

                return Ok(new { data = plantillasMailing });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene configuraciones necesarias para Agenda.
        /// </summary>
        /// <returns>Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerConfiguraciones")]
        public ActionResult ObtenerConfiguraciones()
        {
            try
            {
                var servicioArea = new AreaCapacitacionService(_unitOfWork);
                var servicioSubArea = new SubAreaCapacitacionService(_unitOfWork);
                var servicioPGeneral = new PGeneralService(_unitOfWork);

                return Ok(new
                {
                    areasCapacitacion = servicioArea.ObtenerCombo(),
                    subAreasCapacitacion = servicioSubArea.ObtenerCombo(),
                    programasGenerales = servicioPGeneral.ObtenerPGeneralLanzamientoPorEjecucion()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab, con los filtros realizados
        /// </summary>
        /// <returns>Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("[Action]/{idTab}/{codigoAreaTrabajo}")]
        public ActionResult ObtenerActividadFiltradaPorAsesor(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(resultado.ActividadesAgenda);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <returns>Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("[Action]/{idTab}/{codigoAreaTrabajo}")]
        public ActionResult ObtenerActividadFiltradaPorAsesorRN2(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["RN2-B"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Carlos H. Crispin Riquelme.
        /// Fecha: 10/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <returns>Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("[Action]/{idTab}/{codigoAreaTrabajo}")]
        public ActionResult ObtenerActividadFiltradaPorAsesorRN2A(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["RN2-A"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="ObjetoDTO">Objeto de clase ObjetoDTO</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerRealizadas")]
        public IActionResult ObtenerRealizadas([FromBody] CompuestoAgendaFiltroDTO objetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AgendaService(_unitOfWork);
                return Ok(servicio.ObtenerRealizadas(objetoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="ObjetoDTO">Objeto de clase ObjetoDTO</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerMensajesRecibidosComercial(int idPersonal)
        {
            var servicio = new AgendaService(_unitOfWork);
            return Ok(servicio.ObtenerMensajesRecibidosComercial(idPersonal));
        }


        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="ObjetoDTO">Objeto de clase ObjetoDTO</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerMensajesRecibidosChatPortal(int idPersonal)
        {
            var servicio = new AgendaService(_unitOfWork);
            return Ok(servicio.ObtenerMensajesRecibidosChatPortal(idPersonal));
        }

        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 20/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="ObjetoDTO">Objeto de clase ObjetoDTO</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerCorreosAgendaComercial(int idPersonal)
        {
            var servicio = new AgendaService(_unitOfWork);
            return Ok(servicio.ObtenerCorreosAgendaComercial(idPersonal));
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener información de centro de Costo AutoComplete
        /// </summary>
        /// <param name="filtros">Filtros donde se ubica la cadena parcial</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerCentroCostoAutocompleteV2")]
        public ActionResult ObtenerCentroCostoAutocompleteV2([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro != null)
                {
                    var servicio = new CentroCostoService(_unitOfWork);
                    return Ok(servicio.ObtenerAutocompleteConPGeneral(filtro["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.f
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[Action]/{idOportunidad}/{idPlantilla}")]
        public ActionResult GenerarPlantillaWhatsappAlterno(int idOportunidad, int idPlantilla)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.GenerarPlantillaWhatsappAlterno(idOportunidad, idPlantilla);
                return Ok(new { plantilla = resultado.Plantilla, objetoplantilla = resultado.ListaEtiquetas });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para centro de costo para programa especifico v2.
        /// </summary>
        /// <returns> String </returns>
        [Route("[Action]/{idCentroCosto}/{idPlantilla}/{idPersonal?}")]
        [HttpGet]
        public ActionResult GenerarPlantillaCentroCostoV2(int idCentroCosto, int idPlantilla, int? idPersonal)
        {
            var servicioCentroCosto = new CentroCostoService(_unitOfWork);
            var servicioPlantillaClaveValor = new PlantillaClaveValorService(_unitOfWork);
            IReemplazoEtiquetaPlantillaService servicioReemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
            string plantilla = string.Empty;
            ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaDTO();
            try
            {
                plantilla = servicioPlantillaClaveValor.ObtenerPorIdPlantilla(idPlantilla).FirstOrDefault().Valor;
                var rpta = servicioCentroCosto.ObtenerCentroCostoParaPEspecifico(idCentroCosto);
                if (rpta != null)
                {
                    plantilla = plantilla.Replace("{tPartner.nombre}", rpta.NombrePartner);
                    plantilla = plantilla.Replace("{tPEspecifico.nombre}", rpta.NombrePEspecifico);
                }
                var resultado = servicioReemplazoEtiquetaPlantilla.ReemplazarEtiquetasSinOportunidad(idPlantilla, 0, idPersonal, idCentroCosto);
                return Ok(resultado.EmailReemplazado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades realizadas en la agenda
        /// </summary>
        /// <returns> objetoDTO: AgendaDTO</returns>
        /// GET: api/<controller>
        [Route("[Action]/{IdAsesor}/{CodigoAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerActividades(int IdAsesor, string CodigoAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService service = new AgendaService(_unitOfWork);
                var resultado = service.ObtenerActividades(IdAsesor, CodigoAreaTrabajo);
                return Ok(new { ActividadesAgenda = resultado });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las tabs de actividades realizadas en la agenda
        /// </summary>
        /// <returns> </returns>
        /// GET: api/<controller>
        [Route("[action]/{idAsesor}/{validar}/{codigoAreaTrabajo}/{flagAgendaWhatsapp}")]
        [HttpGet]
        public ActionResult ObtenerActividadesAgenda(int idAsesor, bool validar, string codigoAreaTrabajo, bool flagAgendaWhatsapp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.ObtenerActividadesAgenda(idAsesor, validar, codigoAreaTrabajo, flagAgendaWhatsapp);
                return Ok(new
                {
                    resultado.ActividadesAgenda,
                    resultado.EstadosTabs,
                    resultado.LogCarlos
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las tabs de actividades realizadas en la agenda
        /// </summary>
        /// <returns> </returns>
        /// GET: api/<controller>
        [Route("[action]/{idAsesor}/{validar}/{codigoAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerActividadesMarcador(int idAsesor, bool validar, string codigoAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.ObtenerActividadesAgenda(idAsesor, validar, codigoAreaTrabajo, false);
                return Ok(new
                {
                    resultado.ActividadesAgenda,
                    resultado.EstadosTabs,
                    resultado.LogCarlos
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera la plantilla de centros de costos de WhatsApp
        /// </summary>
        /// <param name="idCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="idPlantilla">Id de la plantilla (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="numero">Cadena con el numero de WhatsApp</param>
        /// <returns>Response 200 (Cadena con la plantilla), response 400</returns>
        [Route("[Action]/{idCentroCosto}/{idPlantilla}/{idPersonal}/{numero}")]
        [HttpGet]
        public ActionResult GenerarPlantillaCentroCostoWhatsapp(int idCentroCosto, int idPlantilla, int idPersonal, string numero)
        {

            var servicioCentroCosto = new CentroCostoService(_unitOfWork);
            var servicioPespecifico = new PEspecificoService(_unitOfWork);
            var servicioAlumno = new AlumnoService(_unitOfWork);
            var servicioPersonal = new PersonalService(_unitOfWork);
            var servicioOportunidad = new OportunidadService(_unitOfWork);
            //var servicioPgeneral = new PGeneralService(unitOfWork);
            var fecha = new List<ModalidadProgramaDTO>();

            var servicioPlantillaClaveValor = new PlantillaClaveValorService(_unitOfWork);

            string plantilla = string.Empty;
            string valor = string.Empty;
            string numeroAlterno = string.Empty;
            try
            {
                if (numero.StartsWith("51"))
                {
                    numero = numero.Substring(2, 9);
                    numeroAlterno = numero;
                }
                else if (numero.StartsWith("57"))
                {
                    numeroAlterno = numero.Substring(2);
                }
                else if (numero.StartsWith("591"))
                {
                    numeroAlterno = numero.Substring(3);
                }

                var alumno = servicioAlumno.ObtenerPorCelular(numero, numeroAlterno);
                var asesor = servicioPersonal.ObtenerDatoPersonal(idPersonal);
                //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                if (idCentroCosto == 0 || idCentroCosto == null)
                {
                    idCentroCosto = servicioOportunidad.ObtenerCentroCostoPorCelularAlumno(numero, idPersonal);
                }
                plantilla = servicioPlantillaClaveValor.ObtenerPorIdPlantilla(idPlantilla).FirstOrDefault().Valor;
                List<DatosPlantillaWhatsAppDTO> objetoPlantilla = new List<DatosPlantillaWhatsAppDTO>();
                var respuesta = servicioCentroCosto.ObtenerCentroCostoParaPlantillaWhatsApp(idCentroCosto);

                foreach (string word in plantilla.Split('{'))
                {
                    DatosPlantillaWhatsAppDTO plantillaEtiqueValor = new DatosPlantillaWhatsAppDTO();
                    if (word.Contains('}'))
                    {
                        string etiqueta = word.Split('}')[0];
                        //Separamos solo los Id´s

                        if (etiqueta.Contains("tPartner.nombre"))
                        {
                            if (idCentroCosto == 0)
                            {
                                return Ok(null);
                            }
                            valor = respuesta.NombrePartner;
                        }
                        else if (etiqueta.Contains("tPEspecifico.nombre"))
                        {
                            if (idCentroCosto == 0)
                            {
                                return Ok(null);
                            }
                            valor = respuesta.NombrePEspecifico;
                        }
                        else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                        {
                            if (idCentroCosto == 0)
                            {
                                return Ok(null);
                            }
                            valor = respuesta.NombrePGeneral;
                        }

                        if (etiqueta.Contains("Template"))
                        {
                            List<string> listaPalabras = new List<string>();
                            char[] delimitador = new char[] { '.' };
                            string _idPlantilla = "";
                            string _idColumna = "";
                            string[] array = etiqueta.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in array)
                            {
                                listaPalabras.Add(s);
                            }
                            _idPlantilla = listaPalabras[3].ToString();
                            _idColumna = listaPalabras[4].ToString();
                            string etiquetaTemp = _idPlantilla + "." + _idColumna;
                            var resultado = _unitOfWork.PEspecificoRepository.ObtenerSeccionEtiquetaAgendaMensaje(_idColumna, _idPlantilla, idCentroCosto);
                            if (resultado != null)
                                valor = resultado.Valor;
                            else
                                valor = null;
                        }
                        else
                        {

                            if ((etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.Nombre1" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2" || etiqueta == "tAlumnos.email1" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.email" || etiqueta == "T_Alumno.NombrePGeneralUltimoEnvioMasivo" || etiqueta == "T_Alumno.NombrePGeneralUltimaSolicitudInformacion") && idPersonal > 0)
                            {
                                switch (etiqueta)
                                {
                                    case "tPersonal.PrimerNombreApellidoPaterno":
                                        valor = asesor.PrimerNombreApellidoPaterno; break;
                                    case "tPersonal.email":
                                        valor = asesor.Email; break;
                                    case "tPersonal.Nombre1":
                                        valor = asesor.Nombre1; break;
                                    case "tPersonal.nombres":
                                        valor = asesor.Nombres; break;
                                    case "tPersonal.apellidos":
                                        valor = asesor.Apellidos; break;
                                    case "tPersonal.Telefono":
                                        {
                                            if (!string.IsNullOrEmpty(asesor.MovilReferencia))
                                            {
                                                valor = asesor.MovilReferencia;
                                            }
                                            else
                                            {
                                                if (asesor.Central == "192.168.0.20")
                                                {
                                                    //aqp
                                                    valor = "(51) 54 258787 - Anexo " + asesor.Anexo3Cx;
                                                }
                                                else
                                                {
                                                    if (asesor.Central == "192.168.0.20" || asesor.Central == "192.168.2.20")
                                                    {
                                                        //Arequipa //lima
                                                        valor = "(51) 1 207 2770 - Anexo " + asesor.Anexo3Cx;
                                                    }
                                                    else if (asesor.Central == "192.168.3.20")
                                                    {
                                                        //bogota
                                                        valor = "57 (601) 381 9462 - Anexo " + asesor.Anexo3Cx;
                                                    }
                                                    else if (asesor.Central == "192.168.4.20")
                                                    {
                                                        //cd mexico
                                                        valor = "52 (55) 4000 3255 - Anexo " + asesor.Anexo3Cx;
                                                    }
                                                    else if (asesor.Central == "192.168.5.20")
                                                    {
                                                        //santiago
                                                        valor = "56 (2) 2760 9120 - Anexo " + asesor.Anexo3Cx;
                                                    }
                                                    else
                                                    {
                                                        valor = "No registra central asignada";
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case "tAlumnos.apepaterno":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = alumno.ApellidoPaterno;
                                            }
                                        }
                                        break;
                                    case "tAlumnos.apematerno":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = alumno.ApellidoMaterno;
                                            }
                                        }
                                        break;
                                    case "tAlumnos.nombre1":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = alumno.Nombre1;
                                            }
                                        }
                                        break;
                                    case "tAlumnos.nombre2":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = alumno.Nombre2;
                                            }
                                        }
                                        break;
                                    case "T_Alumno.NombrePGeneralUltimoEnvioMasivo":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = servicioAlumno.ObtenerNombreProgramaGeneralUltimoEnvioMasivo(alumno.Id);
                                            }
                                        }
                                        break;
                                    case "T_Alumno.NombrePGeneralUltimaSolicitudInformacion":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = servicioAlumno.ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(alumno.Id);
                                            }
                                        }
                                        break;
                                    case "tAlumnos.email1":
                                        {
                                            if (alumno != null)
                                            {
                                                valor = alumno.Email1;
                                            }
                                        }
                                        break;
                                    default:
                                        valor = ""; break;
                                }

                            }
                        }
                        if (valor != null)
                        {
                            valor = valor.Replace("#$%", "<br>");
                            plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                            plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                            plantillaEtiqueValor.texto = valor;

                        }
                        else
                        {
                            plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                            plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                            plantillaEtiqueValor.texto = "";
                        }
                        objetoPlantilla.Add(plantillaEtiqueValor);


                    }
                }

                return Ok(new { plantilla, objetoPlantilla });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reservado sin Deuda, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReservadoSinDeuda(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Reservado Sin Deuda"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los pespecificos para los accesos temporales
        /// </summary>
        /// <returns>Response 200 (Objeto anonimo con los registros para los combos) o 400 con mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoAccesoTemporalCombo()
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.ObtenerPEspecificoAccesoTemporalCombo();
                return Ok(new
                {
                    resultado.ProgramasAsignados,
                    resultado.CursosAsignados
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Abandonado, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab de la agenda (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Codigo en cadena del area de trabajo</param>
        /// <param name="filtros">Objeto de tipo (Dictionary(string, string)>)</param>
        /// <returns>Response 200 (Objeto anonimo con los registros y el total) o 400 con mensaje de error</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorAbandonado(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Abandonado"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Por Contactar, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPorContactar(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Por Contactar"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Evaluacion, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorEvaluacion(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Evaluacion"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorMasDeUnaCuotaAtraso(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["1+ Cuota Atraso"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Certificado, con los filtros realizados
        /// </summary>
        /// <returns> ObjetoBO: AgendaBO </returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCertificado(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Certificado"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab BIC, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorBic(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Bic"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y acceso temporal
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorAccesoTemporal(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Acceso Temporal"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Pre Reporte CR, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPreReporteCR(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Pre Reporte CR"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reportado CR, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReportadoCR(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string>? filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Reportado CR"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Curso Pendiente, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCursoPendiente(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Curso Pendiente"],
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio mailing.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaEmailMandrillDTO </returns>
        [Route("[Action]/{idOportunidad}/{idPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaMailing(int idOportunidad, int idPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oportunidadService = new OportunidadService(_unitOfWork);
                var plantillaService = new PlantillaService(_unitOfWork);

                if (!oportunidadService.ExistePorId(idOportunidad))
                {
                    return BadRequest("Oportunidad no existente");
                }
                if (!plantillaService.ExistePorId(idPlantilla))
                {
                    return BadRequest("Plantilla no existente");
                }

                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                var plantillaEmail = new PlantillaEmailMandrillDTO();
                try
                {
                    var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO
                    {
                        IdOportunidad = idOportunidad,
                        IdPlantilla = idPlantilla
                    });
                    plantillaEmail = resultadoReemplazo.EmailReemplazado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return Ok(plantillaEmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la toda slas plantillas
        /// </summary>
        /// <returns>Response 200 (Lista de objetos de clase ContenidoPlantillaDTO), response 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantilla()
        {
            try
            {
                PlantillaClaveValorService servicioPlantillaClaveValor = new PlantillaClaveValorService(_unitOfWork);

                return Ok(new { data = servicioPlantillaClaveValor.ObtenerTodoPlantillasMailing() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Seguimiento academico, con los filtros realizados
        /// </summary>
        /// <param name="ddTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorSeguimientoAcademico(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Seguimiento Academico"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Cuota al dia, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCuotaAlDia(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Cuota AlDia"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Culminados, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCulminado(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Culminado"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Programadas Manuales, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorProgramacionManual(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Programacion Manual"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Programadas Manuales, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerActividadFichaChat([FromBody] ObtenerActividadFichaChatDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.ObtenerActividadFichaChat(request.IdTab, request.CodigoAreaTrabajo, request.IdAsesor, request.IdMatriculaCabecera);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Culminados deudor, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCulminadoDeudor(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Culminado Deudor"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Culminados deudor, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReservadoConDeuda(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Reservado Con Deuda"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Culminados deudor, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorRetirado(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Retirado"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Por Abandonar con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPorAbandonar(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["PorAbandonar"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab En Negociacion, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorEnNegociacion(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["En Negociacion"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab En Cierre De Negociacion, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorEnCierreNegociacion(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["En Cierre De Negociacion"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reasignado, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReasignado(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Reasignado"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Daniel H
        /// Fecha: 12/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Clases Online, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorClasesOnline(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Clases Online"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 12/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Pagos Del Día, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorPagosDelDia(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Pagos del dia"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 12/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Pagos atrasados mes actual previo, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorPagosAtrasadosMesActualPrevio(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Pago Atrasado(MesActual-Previo)"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 12/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab COntestan y Cortan, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorContestanCortan(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Contestan y Cortan"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 12/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab COntestan y Cortan, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorBicDeuda(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["BICs con Deuda"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Solicitud Cambio, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[Action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorSolicitudCambio(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Solicitud Cambio"].ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab pagos atrasados, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPagosAtrasados(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Pagos Atrasados"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <returns> OportunidadTabAgenda </returns>
        [Route("[Action]/{idPersonal}/{textoBuscar}/{tipo}")]
        [HttpGet]
        public ActionResult ObtenerClasificacionTab(int idPersonal, string textoBuscar, int tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService servicioOportunidad = new OportunidadService(_unitOfWork);
                var clasificacionTab = servicioOportunidad.ObtenerClasificacionTabAgenda(idPersonal, textoBuscar, tipo);
                return Ok(clasificacionTab);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <returns> OportunidadTabAgenda </returns>
        [Route("[Action]/{idPersonal}/{idMatriculaCabecera}/{tipo}")]
        [HttpGet]
        public ActionResult ObtenerClasificacionTabFicha(int idPersonal, int idMatriculaCabecera, int tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService servicioOportunidad = new OportunidadService(_unitOfWork);
                var clasificacionTab = servicioOportunidad.ObtenerClasificacionTabAgendaFicha(idPersonal, idMatriculaCabecera, tipo);
                return Ok(clasificacionTab);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Sin Contacto, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab de la agenda (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Codigo en cadena del area de trabajo</param>
        /// <param name="filtros">Objeto de tipo (Dictionary(string, string)>)</param>
        /// <returns>Response 200 (Objeto anonimo con los registros y el total) o 400 con mensaje de error</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorSinContacto(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Sin Contacto"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCompromisoPago(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Compromisos De Pagos"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorRecuperacionCurso(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["En Recuperacion De Curso"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorProyectoAplicacionPendiente(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Proyecto Aplicacion Pendiente"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorNotasPendientes(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Notas pendientes"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="idTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="codigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{idTab}/{codigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorBeneficiosPendientes(int idTab, string codigoAreaTrabajo, [FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaService servicio = new AgendaService(_unitOfWork);
                var resultado = servicio.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, filtros, 0);
                return Ok(new
                {
                    Records = resultado.ActividadesAgenda["Beneficios Pendientes"].OrderBy(x => x.UltimaFechaProgramada).ToList(),
                    Total = resultado.CantidadRN2
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para operaciones.
        /// </summary>
        /// <returns> Lista de ObjetoDTO: List<ContenidoPlantillaDTO> </returns>
        [Route("[Action]/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantillaPorPersonalAreaTrabajo(int idPersonalAreaTrabajo)
        {
            try
            {
                PlantillaClaveValorService plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                var listaPlantillasDisponibles = plantillaClaveValorService.ObtenerTodoPlantillaPorPersonalAreaTrabajo(idPersonalAreaTrabajo);
                return Ok(new { data = listaPlantillasDisponibles });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 03/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para la agenda de operaciones.
        /// </summary>
        /// <returns> Lista de ObjetoDTO: List<ContenidoPlantillaDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPlantillasModuloAgenda()
        {
            try
            {
                PlantillaClaveValorService plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                var listaPlantillasDisponibles = plantillaClaveValorService.ObtenerPlantillasModuloAgenda();
                return Ok(new { data = listaPlantillasDisponibles });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera plantilla para centro de costo para programa especifico.
        /// </summary>
        /// <returns> string: plantilla </returns>
        [Route("[Action]/{idCentroCosto}/{idPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaCentroCosto(int idCentroCosto, int idPlantilla)
        {
            try
            {
                AgendaService agendaService = new AgendaService(_unitOfWork);
                var plantilla = agendaService.GenerarPlantillaCentroCosto(idCentroCosto, idPlantilla);
                return Ok(plantilla);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 28/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idOportunidad}/{idPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsapp(int idOportunidad, int idPlantilla)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.GenerarPlantillaWhatsapp(idOportunidad, idPlantilla);
                return Ok(new
                {
                    plantilla = resultado.Plantilla,
                    objetoplantilla = resultado.ListaEtiquetas,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp de resumen de grabaciones
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idOportunidad}/{idPlantilla}/{idResumenGrabacionOnline}/{idPEspecifico}/{idProcesamientoTipoGenerar}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsappResumenGrabaciones(int idOportunidad, int idPlantilla, int idResumenGrabacionOnline, int idPEspecificoSesion, int idProcesamientoTipoGenerar)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.GenerarPlantillaWhatsappResumenGrabaciones(idOportunidad, idPlantilla, idResumenGrabacionOnline, idPEspecificoSesion, idProcesamientoTipoGenerar);
                return Ok(new
                {
                    plantilla = resultado.Plantilla,
                    objetoplantilla = resultado.ListaEtiquetas,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 28/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idAlumno}/{idPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsappAlumno(int idAlumno, int idPlantilla)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.GenerarPlantillaWhatsappAlumno(idAlumno, idPlantilla);
                return Ok(new
                {
                    plantilla = resultado.Plantilla,
                    objetoplantilla = resultado.ListaEtiquetas,
                    datoAlumno=resultado.DatoAlumno
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de centro de costos en lanzamiento y por ejecucion
        /// </summary>
        /// <returns> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCentroCostoAgenda()
        {
            IAgendaService agendaService = new AgendaService(_unitOfWork);
            var resultado = agendaService.ObtenerCentroCostoAgenda();
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 28/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idOportunidad}/{idPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsappComercial(int idOportunidad, int idPlantilla)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var resultado = agendaService.GenerarPlantillaWhatsappComercial(idOportunidad, idPlantilla);
                return Ok(new
                {
                    plantilla = resultado.Plantilla,
                    objetoplantilla = resultado.ListaEtiquetas,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel H.
        /// Fecha: 16/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las caracteriscias del avatar del alumno
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public IActionResult ObtenerAvatar(int idAlumno)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var Avatar = agendaService.ObtenerAvatar(idAlumno);
                return Ok(Avatar);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
