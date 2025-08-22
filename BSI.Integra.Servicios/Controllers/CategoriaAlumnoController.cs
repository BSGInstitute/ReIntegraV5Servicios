using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CategoriaAlumnoController
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión de Categoría Alumno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CategoriaAlumnoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CategoriaAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Categoria Alumno
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCategoriaAlumno()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaAlumnoService servicioCategoriaAlumno = new CategoriaAlumnoService(unitOfWork);
                //se tiene que crear una vista que muestre la categoria con los estados y subestados
                return Ok(servicioCategoriaAlumno.ObtenerCategoriaAlumno());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha Pago Categoria por martriculaCabecera
        /// </summary>
        /// <param name="matriculaCabecera"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{matriculaCabecera}")]
        public ActionResult ObtenerFechaPagoCategoria(int matriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaAlumnoService servicioCategoriaAlumno = new CategoriaAlumnoService(unitOfWork);
                return Ok(servicioCategoriaAlumno.ObtenerFechaPago(matriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
