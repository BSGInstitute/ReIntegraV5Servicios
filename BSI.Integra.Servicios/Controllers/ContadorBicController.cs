using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ContadorBicController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 31/08/2023
    /// <summary>
    /// Gestión de ContadorBic
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ContadorBicController : Controller
    {
        private IContadorBicService _contadorBicService;
        public ContadorBicController(IUnitOfWork unitOfWork)
        {
            _contadorBicService = new ContadorBicService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el calculo de contador bic
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult CalcularDiasParaBIC()
        {
            var resultado = _contadorBicService.CalcularDiasParaBIC();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza el calculo de contador bic version alterna
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult CalcularDiasParaBICAlterno()
        {
            var resultado = _contadorBicService.CalcularDiasParaBICAlterno();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el calculo de contador bic
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]/{idOportunidad}")]
        public IActionResult CalcularDiasParaBICPorIdOportunidad(int idOportunidad)
        {
            var resultado = _contadorBicService.CalcularDiasParaBICPorIdOportunidad(idOportunidad);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el proceso de ejecutar bics
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]/{idPaisSede}")]
        public IActionResult EjecutarBIC(int idPaisSede)
        {
            var resultado = _contadorBicService.EjecutarBIC(idPaisSede);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 23/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza el proceso de ejecutar bics manualmente enviado los datos forzandolos
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult EjecutarBICManualmente()
        {
            var resultado = _contadorBicService.EjecutarBICManualmente();
            return Ok(resultado);
        }
    }
}
