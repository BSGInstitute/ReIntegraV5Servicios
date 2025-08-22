using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Security;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using RestSharp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using System.Transactions;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    /// Controlador: WhatsAppConfiguracionEnvio
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 12/12/20222
    /// <summary>
    /// Gestion de los contactos en sendinblue
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppConfiguracionEnvioController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        private AlumnoRepository _repAlumno;
        private PersonalRepository _repPersonal;
        private PlantillaClaveValorRepository _repPlantillaClaveValor;
        private PlantillaRepository _repPlantilla;
        private CentroCostoRepository _repCentroCosto;
        private PEspecificoRepository _repPespecifico;
        private PGeneralRepository _repPgeneral;
        private WhatsAppConfiguracionLogEjecucionRepository _repWhatsAppConfiguracionLogEjecucion;
        private WhatsAppUsuarioCredencialRepository _repTokenUsuario;
        private readonly WhatsAppMensajePublicidadRepository _repWhatsAppMensajePublicidad;
        private readonly WhatsAppConfiguracionEnvioDetalleRepository _repWhatsAppConfiguracionEnvioDetalle;
        private readonly ConjuntoListaResultadoRepository _repConjuntoListaResultado;
        private WhatsAppConfiguracionRepository _repCredenciales;
        private readonly OportunidadRepository _repOportunidad;
        public PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado;
        public WhatsAppConfiguracionEnvioController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Esta funcion Obtiene la configuracion de whatsapp del conjunto de lista
        /// </summary>
        /// <param name="IdConjuntoLista">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos de clase ConjuntoListaDetalleWhatsAppDTO</returns>
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerListaConfiguracion(int IdConjuntoLista)
        {
            try
            {
                WhatsAppConfiguracionEnvioService _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioService(unitOfWork);
                var resultado = _repWhatsAppConfiguracionEnvio.ConsultaWhatsAppYConfiguracionEnvio(IdConjuntoLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="Servidor">Nombre del servidor que registra la caida</param>
        /// <returns>Response 200 con true, caso contrario 400</returns>
        [Route("[Action]/{Servidor}")]
        [HttpGet]
        public ActionResult InsertarRegistroCaidaServidor(string Servidor)
        {
            try
            {
                WhatsAppConfiguracionEnvioService _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioService(unitOfWork);
                _repWhatsAppConfiguracionEnvio.InsertarRegistroCaidaServidor(Servidor);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="EstadoWhatsAppHabilitado">Estado de whatsapp habilitado</param>
        /// <returns>Response 200 con true, caso contrario 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizaEstadoEnvioAutomaticoWhatsApp([FromBody] WhatsAppHabilitadoRecuperacionDTO EstadoWhatsAppHabilitado)
        {

            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                WhatsAppConfiguracionEnvioService _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioService(unitOfWork);
                RegistroRecuperacionWhatsAppService _repRegistroRecuperacionWhatsApp = new RegistroRecuperacionWhatsAppService(unitOfWork);

                _repWhatsAppConfiguracionEnvio.ActualizarEstadoWhatsAppRecuperacion(EstadoWhatsAppHabilitado.Tipo, _respuestaCorrecta.RegistroClaimToken.UserName, EstadoWhatsAppHabilitado.EstadoHabilitado, 332); //ValorEstatico.IdModuloSistemaWhatsAppMailing
                if (!EstadoWhatsAppHabilitado.EstadoHabilitado)
                {
                    _repRegistroRecuperacionWhatsApp.DesactivarCompletadoRegistroWhatsApp(_respuestaCorrecta.RegistroClaimToken.UserName);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// version: 1.0
        /// <summary>
        /// Realiza una insercion de la lsisat de whatsapp configuracion envio
        /// </summary>
        /// <param name="ObjetoDTO"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarWhatsAppConfiguracionEnvio([FromBody] List<InsertarWhatsAppConfiguracionEnvioDTO> ObjetoDTO)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                WhatsAppConfiguracionEnvioPorProgramaService _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaService(unitOfWork);
                _repWhatsAppConfiguracionEnvioPorPrograma.InsertarWhatsAppConfiguracionEnvio(ObjetoDTO, _respuestaCorrecta.RegistroClaimToken.UserName);

                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// version: 1.0
        /// <summary>
        /// Procesa las listas de whatsapp para envio automatico
        /// </summary>
        /// <param name="ListasWhatsApp"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarListasWhatsAppEnvioAutomatico([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            try
            {
                return Ok(new WhatsAppConfiguracionEnvioService(unitOfWork).ProcesarListasWhatsAppEnvioAutomaticoOperaciones(ListasWhatsApp));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult EnvioMasivoReasignacionesOperaciones()
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).EnvioMasivoReasignacionesOperaciones());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ReanudarEnvioAutomatico(int IdConjuntoLista)
        {
            try
            {
                return Ok(new WhatsAppConfiguracionEnvioService(unitOfWork).ReanudarEnvioAutomatico(IdConjuntoLista));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///Funcion donde se realiza el estado del numero de whatsapp del alumno es llamado desde complementos 
        /// </summary>
        /// <returns>ok/returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarNumerosWhatsapp()
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).ProcesarNumerosWhatsapp());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///Funcion donde se realiza el envio de whatsapp de prueba 
        /// </summary>
        /// <returns>ok/returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarenviowhatsappPruebaDesarrollo()
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).EjecutarenviowhatsappPruebaDesarrollo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: día/mes/año
        /// Versión: 1.0
        /// <summary>
        /// Calculo de ejecucion de estado de whatsapp de prueba
        /// </summary>
        /// <returns> 200 o 40*</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarEjecucionEstadoWhatsappMasivo()
        {
            try
            {
                AlumnoService alumno = new AlumnoService(unitOfWork);
                int i = 0;
                bool bandera = true;
                while (bandera)
                {
                    var alumnosPeru = alumno.ObtenerALumnosaValidarWhatsappPeru(5000, i);
                    if (alumnosPeru.Count > 0)
                    {
                        alumnosPeru = alumno.ValidarEstadoContactoWhatsAppMasivo(51, alumnosPeru);
                        alumno.Update(alumnosPeru);
                        i++;
                    }
                    else
                    {
                        bandera = false;
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 07/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Calculo masivo del estado de whatsapp de los alumnos
        /// </summary>
        /// <returns>200 o 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarEjecucionEstadoWhatsappMasivoFinal()
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).ProcesarEjecucionEstadoWhatsappMasivoFinal());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 07/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Calculo masivo del estado de whatsapp de los alumnos
        /// </summary>
        /// <returns>200 o 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult RegularizarEstadoWhatsapp()
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).RegularizarEstadoWhatsapp());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <returns>Objeto de tipo WhatsAppHostDatosDTO</returns>
        [Route("Obtener/Configuracion/HorariosDeEnvio/Combo/{intervalo}")]
        [HttpGet]
        public IActionResult ObtenerConfiguracionDeHorariosDeEnvioParaCombo(int intervalo)
        {
            try
            {
                return Ok(new WhatsAppConfiguracionService(unitOfWork).ObtenerConfiguracionDeHorariosDeEnvioParaCombo(intervalo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}