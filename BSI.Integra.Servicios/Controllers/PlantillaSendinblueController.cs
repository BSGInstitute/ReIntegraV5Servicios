using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Plantilla Sendinblue
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión de Plantilla Sendinblue
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PlantillaSendinblueController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public PlantillaSendinblueController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("ObtenerPlantillaPorId/{id}")]
        public IActionResult ObtenerPlantillaPorId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaSendinblueService(unitOfWork);
                return Ok(servicio.ObtenerPlantilllaPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AgregarPlantillaDatos")]
        public IActionResult AgregarPlantillaDatos(PlantillaSendinblueInsertarDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new PlantillaSendinblueDatoService(unitOfWork);
                return Ok(servicio.AgregarPlantillaDatos(datos, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ActualizarPlantillaDatos")]
        public IActionResult ActualizarPlantillaDatos(PlantillaSendinblueActualizarDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new PlantillaSendinblueDatoService(unitOfWork);
                return Ok(servicio.ActualizarPlantillaDatos(datos, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObtenerTodo")]
        public IActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaSendinblueService(unitOfWork);
                return Ok(servicio.ObtenerTodo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
