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
    /// Controlador: Conjunto Lista
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión de Conjunto Lista
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PlantillaSendinblueImagenController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public PlantillaSendinblueImagenController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpPost("EliminarImagen")]
        public IActionResult EliminarImagen(IdDTO id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new PlantillaSendinblueImagenService(unitOfWork);
                return Ok(servicio.Delete(id.Id, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("InsertarImagen")]
        public IActionResult InsertarImagen([FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new PlantillaSendinblueImagenService(unitOfWork);
                return Ok(servicio.AdjuntarArchivoMarketing(file, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerImagenesPlantilla")]
        public IActionResult ObtenerImagenesPlantilla()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaSendinblueImagenService(unitOfWork);
                return Ok(servicio.ObtenerImagenesPlantilla());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
