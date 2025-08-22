using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PlantillaV2SeccionEstiloController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de PlantillaV2SeccionEstilo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PlantillaV2SeccionEstiloController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PlantillaV2SeccionEstiloController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] PlantillaV2SeccionEstiloEnvio entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<PlantillaV2SeccionEstilo> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] PlantillaV2SeccionEstiloEnvio entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<PlantillaV2SeccionEstilo> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
                var respuesta = servicio.Update(listado);
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
                var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PlantillaV2SeccionEstilo para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PlantillaV2SeccionEstilo
        /// </summary>
        /// <returns> List<PlantillaV2SeccionEstiloDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerPlantillaV2SeccionEstilo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaV2SeccionEstiloService(unitOfWork);
                return Ok(servicio.ObtenerPlantillaV2SeccionEstilo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
