using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AlumnoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Alumno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AlumnoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IAlumnoService _alumnoService;
        public AlumnoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _alumnoService = new AlumnoService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de Alumnos basado en un Nombre Parcial.
        /// </summary>
        /// <param name="filtro">Filtros Clave Valor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerAutocomplete(StringDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_alumnoService.ObtenerAutocomplete(filtro.Valor));
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 28/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Alumno AutoComplete
        /// </summary>
        /// <returns> Lista de objetoDTO: List<AlumnoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult ObtenerAlumnoAutocomplete([FromBody] StringDTO filtro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                return Ok(alumnoService.ObtenerTodoFiltroAutoComplete(filtro.Valor));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de Alumnos basado en un Nombre Parcial.
        /// </summary>
        /// <param name="Filtros">Filtros Clave Valor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerAlumnoMatriculadoAutocomplete")]
        public IActionResult ObtenerAlumnoMatriculadoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AlumnoService(_unitOfWork);
                return Ok(servicio.ObtenerAlumnoMatriculadoAutocomplete(Filtros["valor"].ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Hace el envio de sms a los contactos por reprogramacion automatica por dia y ocurrencia
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> Retorna 200 y valor o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idOportunidad}/{idOcurrencia}")]
        public IActionResult EnviarIndividualSMSPorOcurrencia(int idOportunidad, int idOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioAlumno = new AlumnoService(_unitOfWork);
                var servicioOcurrencia = new OcurrenciaService(_unitOfWork);
                var servicioPlantillaPw = new PlantillaPwService(_unitOfWork);
                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                var servicioSMSConfiguracion = new SmsConfiguracionEnvioService(_unitOfWork);

                int seccionMensaje = 1;
                string mensajeFinal = string.Empty;
                List<string> listaSubMensajeFinal = new List<string>();
                int idPlantilla = 0;

                var configuracionEstablecida = servicioSMSConfiguracion.ConfiguracionSmsOportunidad(idOportunidad);

                if (configuracionEstablecida == null) return Ok(false);

                string urlBase = $"http://{configuracionEstablecida.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";

                // Validacion de mensaje si ya se le envio un sms el dia de hoy a esa oportunidad
                var envio = servicioAlumno.Obtener_EnvioSMSPorDiaOportunidad(idOportunidad, DateTime.Now);

                var ocurrencia = servicioOcurrencia.ObtenerPorId(idOcurrencia);

                if (envio == null && ocurrencia.IdEstadoOcurrencia == 2)
                {
                    var diasSinContacto = servicioSMSConfiguracion.ObtenerDiasSinContacto(idOportunidad);

                    /*Definicion de plantilla*/
                    switch (diasSinContacto.DiasSinContacto)
                    {
                        case 1:
                            idPlantilla = 1458;//Primer intento 1 dia sin contacto
                            break;
                        //case 2:
                        //    idPlantilla = 1459;//ValorEstatico.IdRecordatorioSms03;
                        //    break;
                        //case 3:
                        //    idPlantilla = 1460;//ValorEstatico.IdRecordatorioSms04;
                        //    break;
                        default:
                            idPlantilla = 0;
                            break;
                    }
                }
                if (idPlantilla > 0)
                {
                    ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                    {
                        IdOportunidad = idOportunidad,
                        IdPlantilla = idPlantilla
                    };
                    var etiquetasReemplazadas = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta);

                    string[] palabras = etiquetasReemplazadas.SmsReemplazado.Cuerpo.Split(' ');

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
                        string url = $"{urlBase}{configuracionEstablecida.Celular}&message={mensajeAEnviar.Replace(" ", "%20")}&[port={configuracionEstablecida.Puerto}&][report=String&][timeout=5]";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(url);
                        }

                        servicioAlumno.InsertaSMSOportunidadUsuario(configuracionEstablecida.Celular, configuracionEstablecida.IdPersonal, configuracionEstablecida.IdAlumno, mensajeAEnviar, seccionMensaje, configuracionEstablecida.IdCodigoPais.GetValueOrDefault(), "EnvioOcurrencia");


                        //Pruebas Carlos Peru
                        if (configuracionEstablecida.IdCodigoPais == 51)
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string celularCarlosPruebas = "980825734";
                                url = $"{urlBase}{celularCarlosPruebas}&message={mensajeAEnviar.Replace(" ", "%20")}&[port={configuracionEstablecida.Puerto}&][report=String&][timeout=5]";

                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(url);
                            }
                        }

                        seccionMensaje++;
                    }

                    var insertado = servicioAlumno.InsertaSMSOportunidad(idOportunidad, DateTime.Now);
                }

                return Ok(true);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 20/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Email Principal de Alumno
        /// </summary>
        /// <returns> Información de Alumno </returns>
        /// <returns> Entidad : alumnoRetorno </returns>
        [Route("[Action]/{usuario}")]
        [HttpPost]
        public ActionResult ActualizarEmailPrincipal([FromBody] AlumnoActualizarEmailPrincipalDTO alumnoCorreo, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _alumnoService.ActualizarEmailPrincipal(alumnoCorreo, usuario);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 20/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Email Principal de Alumno
        /// </summary>
        /// <returns> Información de Alumno </returns>
        /// <returns> Entidad : alumnoRetorno </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ReasignacionOportunidadesActualizarEmail([FromBody] AlumnoActualizarEmailPrincipalDTO alumnocorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                if (respuestaCorrecta.TokenValida)
                {
                    var usuario = respuestaCorrecta.RegistroClaimToken.UserName;
                    IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    var resultado = alumnoService.ReasignacionOportunidadesActualizarEmail(alumnocorreo, usuario);
                    return Ok(new
                    {
                        resultado.IdClasificacionPersona,
                        resultado.EstadoReasignacion
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Envia SMS a Oportunidad mediante el id y idPlantilla
        /// </summary>
        /// <returns> booleano </returns>
        [Route("[Action]/{idOportunidad}/{idPlantilla}")]
        [HttpGet]
        public ActionResult EnvioSmsOportunidadPlantilla(int idOportunidad, int idPlantilla)
        {
            try
            {
                var alumnoService = new AlumnoService(_unitOfWork);
                int seccionMensaje = 1;
                string mensajeFinal = string.Empty;
                List<string> listaSubMensajeFinal = new List<string>();

                /*Buscar configuracion para el envio de SMS individual*/
                var smsConfiguracionEnvioService = new SmsConfiguracionEnvioService(_unitOfWork);
                var configuracionEstablecida = smsConfiguracionEnvioService.ConfiguracionSmsOportunidad(idOportunidad);

                if (configuracionEstablecida != null)
                {
                    string urlBase = $"http://{configuracionEstablecida.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";
                    #region Cambio de etiqueta
                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);

                    ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                    {
                        IdOportunidad = idOportunidad,
                        IdPlantilla = idPlantilla,
                    };
                    var resultadoReemplazo = reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta);
                    #endregion

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
                        string url = $"{urlBase}{configuracionEstablecida.Celular}&message={mensajeAEnviar.Replace(" ", "%20")}&port={configuracionEstablecida.Tipo}-{configuracionEstablecida.Puerto}&report=String&timeout=5";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(url);
                        }

                        alumnoService.InsertaSMSOportunidadUsuario(configuracionEstablecida.Celular, configuracionEstablecida.IdPersonal, configuracionEstablecida.IdAlumno, mensajeAEnviar, seccionMensaje, configuracionEstablecida.IdCodigoPais.GetValueOrDefault(), "EnvioAutomatico");
                        seccionMensaje++;
                    }
                    var insertado = alumnoService.InsertaSMSOportunidad(idOportunidad, DateTime.Now);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Muestra el Estado del Contacto de WhatsApp por medio del idAlumno
        /// </summary>
        /// <returns></returns>
        [Route("[action]/{idAlumno}")]
        [HttpPost]
        public ActionResult EstadoContactoWhatsApp(int idAlumno)
        {
            var resultado = _alumnoService.ObtenerEstadoWhatsapp(idAlumno);
            return Ok(resultado);
        }
        [Route("[action]/{idAlumno}")]
        [HttpPost]
        public ActionResult ActualizarNombreAlumno([FromBody] AlumnoActualizarCertificadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var alumno = _alumnoService.ObtenerPorId(dto.Id);
                alumno.Nombre1 = dto.Nombre1;
                alumno.Nombre2 = dto.Nombre2;
                alumno.ApellidoPaterno = dto.ApellidoPaterno;
                alumno.ApellidoMaterno = dto.ApellidoMaterno;
                _alumnoService.Update(alumno);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm
        /// Fecha: 15/06/2023
        /// Version: 1.0
        /// <summary>
        /// retorna si existe o no un alumno por su DNI (Prueba para IVR - CFD)
        /// </summary>
        /// <returns> bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoPorDNI([FromBody] StringDTO filtro)
        {
            IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
            return Ok(alumnoService.ObtenerAlumnoPorDNI(filtro));
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm
        /// Fecha: 15/06/2023
        /// Version: 1.0
        /// <summary>
        /// retorna si existe o no un alumno por su DNI (Prueba para IVR - CFD)
        /// </summary>
        /// <returns> Id y Nombre del alumno </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoPorDNIV2([FromBody] StringDTO filtro)
        {
            IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
            return Ok(alumnoService.ObtenerAlumnoPorDNIV2(filtro));
        }
        /// TipoFuncion: GET
        /// Autor: Christian Quispe
        /// Fecha: 24/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Alumno AutoComplete
        /// </summary>
        /// <returns> Lista de objetoDTO: List<AlumnoFiltroAutocompleteDTO> </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerInformacionAlumno(int idAlumno)
        {
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                return Ok(alumnoService.ObtenerInformacionAlumno(idAlumno));
            }
            catch
            {
                throw;
            }
        }
    }
}
