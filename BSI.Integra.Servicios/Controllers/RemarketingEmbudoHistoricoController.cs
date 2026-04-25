using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
        /// Autor: Max Mantilla R.
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] RemarketingEmbudoHistorico entidad)
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
        /// Autor: Max Mantilla R.
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<RemarketingEmbudoHistorico> listado)
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
        /// Autor: Max Mantilla R.
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] RemarketingEmbudoHistorico entidad)
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
        /// Autor: Max Mantilla R.
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<RemarketingEmbudoHistorico> listado)
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
        public async Task<ActionResult<bool>> EvaluarEmbudoRemarketing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
                var resultado = await servicio.EvaluarEmbudoRemarketing();
                return Ok(resultado);
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
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerNivelEsquemaEmbudoRemarketing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
                var resultado = servicio.ObtenerNivelEsquemaEmbudoRemarketing();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodriguez.
        /// Fecha: 04/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Procesa y clasifica en el embudo de remarketing a un alumno específico.
        /// Si ya existe un registro para el alumno, lo actualiza; caso contrario, inserta uno nuevo.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno a evaluar.</param>
        /// <returns>Response 200 con el bool, caso contrario response 400 con el mensaje de error.</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<bool> EvaluarEmbudoRemarketingAlumno(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string Usuario = "Evaluacion Manual";
                var servicio = new RemarketingEmbudoHistoricoService(unitOfWork);
                var resultado = servicio.EvaluarEmbudoRemarketingAlumno(IdAlumno,Usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
