using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FacebookLeadController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FacebookLeadController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FacebookLeadController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa los leads enviados desde Webhook erroneos en objetos
        /// </summary>
        /// <param name="FacebookRangoFechaDTO">Objeto de clase FacebookRangoFechaDTO</param>
        /// <returns>Response 200 con el bool, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarFacebookLeadsErroneos([FromBody] FacebookRangoFechaDTO FiltroProcesamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                var facebookFormularioLeadgen = new FacebookFormularioLeadgenService(unitOfWork);


                try
                {
                    bool respuesta = facebookFormularioLeadgen.ProcesarDatosLeadErroneos(FiltroProcesamiento.FechaInicio, FiltroProcesamiento.FechaFin);
                }
                catch (Exception)
                {
                }


                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConversacionesMessenger(string idPagina, string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new FacebookService(unitOfWork);
                return Ok(servicio.DescargarConversacionPorIdPagina(idPagina, token));


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConversacionesMessengerPorIdUsuario(string idPagina,string idUsuario, string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new FacebookService(unitOfWork);
                return Ok(servicio.DescargarConversacionPorIdUsuario(idPagina, idUsuario, token));


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 02/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Procesa los leads enviados desde Webhook en objetos
        /// </summary>
        /// <param name="LeadgenInformacionDTO">Objeto de clase LeadgenInformacionDTO</param>
        /// <returns>Response 200 con el Id de AsignacionAutomaticaTemp, caso contrario response 400 con el mensaje de error</returns>
      
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarFacebookLead([FromBody] LeadgenInformacionDTO LeadgenInformacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new FacebookLeadService(unitOfWork);
                return Ok(servicio.ProcesarFacebookLead(LeadgenInformacionDTO));


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult EnvioCorreoFacebook()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                bool resultado = servicio.EnvioCorreoFacebook("Inicio Conexion Hook");

                if (resultado)
                {
                    return Ok("Correo enviado exitosamente");
                }
                else
                {
                    return StatusCode(500, "Ocurrió un error al enviar el correo");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
