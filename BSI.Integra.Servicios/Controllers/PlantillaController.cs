using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PlantillaController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Plantilla
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class PlantillaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PlantillaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas de mensajes para chat
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <param name="usuarioNombre">Nombre de Usuario de Interfaz</param>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[Action]/{idPlantilla}/{usuarioNombre}")]
        [HttpGet]
        public ActionResult GenerarSpeechChatSoporte(int idPlantilla, string usuarioNombre)
        {
            try
            {
                IntegraAspNetUserService servicioIntegraAspNetUser = new IntegraAspNetUserService(unitOfWork);
                var reemplazoEtiquetaPlantilla = new PlantillaService(unitOfWork);
                var emailPersonal = servicioIntegraAspNetUser.ObtenerPorUsuario(usuarioNombre).FirstOrDefault();
                var emailreemplazo = new PlantillaEmailMandrillDTO();
                var datosMensaje = reemplazoEtiquetaPlantilla.ReemplazarSpeechChatSoporte(emailPersonal.Email, emailPersonal.PerId, idPlantilla);

                return Ok(datosMensaje.CuerpoHTML);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerPlantillaPanel()
        {
            try
            {
                var servicio = new PlantillaService(unitOfWork);
                var listaPlantillaAsociacionModuloSistema = servicio.ObtenerListarPlantilla();
                return Ok(listaPlantillaAsociacionModuloSistema);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idPlantilla}")]
        [HttpGet]
        public IActionResult ObtenerPlantillaClaveValorPorId(int idPlantilla)
        {
            try
            {
                var servicio = new PlantillaService(unitOfWork);
                var listaPlantillaAsociacionModuloSistema = servicio.ObtenerPlantillaClaveValor(idPlantilla);
                return Ok(listaPlantillaAsociacionModuloSistema);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerModulo()
        {
            try
            {
                var servicio = new PlantillaService(unitOfWork);
                var modulo = servicio.ObtenerModulo();
                return Ok(modulo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var servicio = new PlantillaService(unitOfWork);
                var modulo = servicio.ObtenerPorId(id);
                return Ok(modulo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[Action]")]
        public IActionResult InsertarPlantilla([FromBody] CompuestoPlantillaDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();

                var servicio = new PlantillaService(unitOfWork);
                var respuesta = servicio.Insertar(entidad, Usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[Action]")]
        public IActionResult Actualizar([FromBody] CompuestoPlantillaDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new PlantillaService(unitOfWork);
                var respuesta = servicio.Actualizar(entidad, Usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{id}")]
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
            var servicio = new PlantillaService(unitOfWork);
                var modulo = servicio.Eliminar(id, Usuario);
                return Ok(modulo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerPlantillasSpeech()
        {
            try
            {
                var servicio = new PlantillaService(unitOfWork);
                var modulo = servicio.ObtenerPlantillasSpeech();
                return Ok(modulo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerAllPlantillaSpeechDespedida()
        {
            try
            {
                var servicio = new PlantillaService(unitOfWork);
                var modulo = servicio.ObtenerAllPlantillaSpeechDespedida();
                return Ok(modulo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

  


    }
}
