using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PaisController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión de Pais
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class OportunidadIsVerificadaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public OportunidadIsVerificadaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Insera Oportunidad Verificada
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarOportunidadVerificada([FromBody] OportunidadCodigoMatriculaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IOportunidadIsVerificadaService service = new OportunidadIsVerificadaService(_unitOfWork);
            var resultado = service.InsertarOportunidadVerificada(dto);
            return Ok(resultado);
        }

    }
}
