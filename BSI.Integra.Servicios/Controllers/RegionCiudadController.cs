using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegionCiudadController : ControllerBase
    {


        private IUnitOfWork unitOfWork;

        public RegionCiudadController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] RegionCiudad entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<RegionCiudad> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] RegionCiudad entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Autor Modificacion: Flavio R. Mamani Fabian
        /// Fecha Modificacion: 27/04/2023
        /// Versión: 2.0
        /// <summary>
        /// Filtra programas especificos por nombre
        /// </summary>
        /// <returns>Json</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ObtenerRegionCiudad")]
        public IActionResult ObtenerRegionCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                return Ok(servicio.ObtenerRegionCiudad());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("FiltroPaisCiudad/{idPais}/{idCiudad}")]
        public IActionResult filtroPaisCiudad(int idPais, int idCiudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegionCiudadService(unitOfWork);
                return Ok(servicio.filtroPaisCiudad(idPais, idCiudad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: GIlmer Quispe
        /// Fecha: 07/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de Region ciudad filtrado por el Estado=1
        /// </summary> 
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                var regionCiudadService = new RegionCiudadService(unitOfWork);
                return Ok(regionCiudadService.ObtenerPorEstado());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
