using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Aplicacion.Interaccion.Service.Implementacion;
using BSI.Integra.Aplicacion.Interaccion.Service.Interface;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BSI.Integra.Servicios.Controllers.IntegraDBInteraccion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegistroInicioSesionEstadoController : Controller
    {
        private readonly IUnitOfWorkInteraccion unitOfWork;

        public RegistroInicioSesionEstadoController(IUnitOfWorkInteraccion unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 2024/05/31
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro Inicio Sesion
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                IRegistroInicioSesionEstadoService servicio = new RegistroInicioSesionEstadoService(unitOfWork);
                return Ok(servicio.Obtener());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: Post
        /// Autor: Max Mantilla R.
        /// Fecha: 03/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra el estado de la interacción de inicio de sesión en integra
        /// </summary>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult RegistrarInicioSesionEstado([FromBody] RegistroInicioSesionEstadoLogueoDTO objetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroInicioSesionEstadoService(unitOfWork);
                var Registro = servicio.RegistrarInicioSesionEstado(objetoDTO);

                return Ok(Registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
