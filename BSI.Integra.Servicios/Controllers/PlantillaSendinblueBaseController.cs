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
    public class PlantillaSendinblueBaseController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public PlantillaSendinblueBaseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("ObtenerComboPlantillas")]
        public IActionResult ObtenerComboPlantillas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaSendinblueBaseService(unitOfWork);
                return Ok(servicio.ObtenerComboPlantillas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObtenerComboPlantillasTodo")]
        public IActionResult ObtenerComboPlantillasTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaSendinblueBaseService(unitOfWork);
                return Ok(servicio.ObtenerComboPlantillasTodo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
