using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PersonalHorarioController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022  
    /// <summary>
    /// Gestión de PersonalHorario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PersonalHorarioController : ControllerBase
    {
        private IPersonalHorarioService _personalHorarioService;
        public PersonalHorarioController(IUnitOfWork unitOfWork)
        {
            _personalHorarioService = new PersonalHorarioService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el horario del personal
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerHorario(int idPersonal)
        {
            var resultado = _personalHorarioService.ObtenerHorario(idPersonal);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Victor Hinojosa
        /// Fecha: 22/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el horario del personal para GP
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        /// 
        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerHorarioGP(int idPersonal)
        {
            var resultado = _personalHorarioService.ObtenerHorarioGP(idPersonal);
            return Ok(resultado);
        }
    }
}
