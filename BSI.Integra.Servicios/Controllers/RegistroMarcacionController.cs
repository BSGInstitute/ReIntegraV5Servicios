using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PersonalController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión de Registro de Marcacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class RegistroMarcacionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public RegistroMarcacionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el area del usuario indicado
        /// </summary>
        /// <param name="usuario"> Nombre de usuario a filtrar</param>
        /// <returns>Retorna Area del personal </returns>
        [HttpGet("[Action]/{usuario}")]
        public IActionResult ObtenerAreaPersonal(string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioIntegraAspNetUsers = new IntegraAspNetUserService(unitOfWork);
                var servicioPersonal = new PersonalService(unitOfWork);
                var user = unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(usuario);
                var personal = servicioPersonal.ObtenerPersonalPorId(user.Id);
                if (personal.AreaAbrev != null)
                {
                    return Ok(new { Area = personal.AreaAbrev });
                }
                else
                {
                    return BadRequest("No se encontró usuario.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros del CSV para mostrar en la grilla
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPost("ProcesarExcelRegistroMarcacion")]
        public IActionResult ProcesarExcelRegistroMarcacion([FromForm] IFormFile ArchivoExcel)
        {
            try
            {
                var servicio = new PersonalService(unitOfWork);
                var respuesta = servicio.ProcesarExcelRegistroMarcacion(ArchivoExcel);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta maracaciones .
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPost("InsertarMarcacionPersonal")]
        public IActionResult InsertarMarcacionPersonal(StringDTO JsonString )
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PersonalService(unitOfWork);
                var result = servicio.InsertarMarcacionPersonal(JsonString.Valor, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
