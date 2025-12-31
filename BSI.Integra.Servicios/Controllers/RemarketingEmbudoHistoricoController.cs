using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RemarketingEmbudoHistoricoController
    /// Autor: Max Mantilla Rodriguez.
    /// Fecha: 27/12/2025
    /// <summary>
    /// Gestión de Tipo de Dato T_RemarketingEmbudoHistorico
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RemarketingEmbudoHistoricoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public RemarketingEmbudoHistoricoController(IUnitOfWork unitOfWork)
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
        public IActionResult Insertar([FromBody] TagsEnvio entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
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
        public IActionResult InsertarLista([FromBody] List<Tags> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
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
        public IActionResult Actualizar([FromBody] TagsEnvio entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
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
        public IActionResult ActualizarLista([FromBody] List<Tags> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
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
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodriguez.
        /// Fecha: 27/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Procesa la oportunidad para clasificación de remarketing en embudo histórico
        /// </summary>
        /// <param name="IdOportunidad">Identificador de la Oportunidad</param>
        /// <returns>Response 200 con el bool, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<bool>> EvaluarEmbudoRemarketing(DateTime? FechaCorte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
                var resultado = await servicio.EvaluarEmbudoRemarketing(FechaCorte);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
