using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PEspecificoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión de PEspecifico
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ModuloSistemaPaqueteController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IModuloSistemaPaqueteService _moduloSistemaPaqueteService;
        public ModuloSistemaPaqueteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _moduloSistemaPaqueteService = new ModuloSistemaPaqueteService(unitOfWork);
        }
        /// Tipo Función: POST
		/// Autor: Christian A. Quispe Mamani
		/// Fecha: 31/10/2023
		/// Version: 1.0
		/// <summary>
        /// Insertar un Modulo Sistema Paquete para V5
		/// </summary>
		/// <returns>  </returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ModuloSistemaPaqueteV5DTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _moduloSistemaPaqueteService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Christian A. Quispe Mamani
		/// Fecha: 31/10/2023
		/// Version: 1.0
		/// <summary>
        /// Insertar un Modulo Sistema Paquete para V5
		/// </summary>
		/// <returns>  </returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] ModuloSistemaPaqueteV5DTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _moduloSistemaPaqueteService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Christian A. Quispe Mamani
		/// Fecha: 31/10/2023
		/// Version: 1.0
		/// <summary>
        /// Insertar un Modulo Sistema Paquete para V5
		/// </summary>
		/// <returns>  </returns>
        [Authorize]
        [Route("[Action]/{id}")]
        [HttpDelete]
        public ActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _moduloSistemaPaqueteService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Christian A. Quispe Mamani
		/// Fecha: 31/10/2023
		/// Version: 1.0
		/// <summary>
        /// Insertar un Modulo Sistema Paquete para V5
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var resultado = _moduloSistemaPaqueteService.Obtener();
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Christian A. Quispe Mamani
		/// Fecha: 31/10/2023
		/// Version: 1.0
		/// <summary>
        /// Insertar un Modulo Sistema Paquete para V5
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerModulos(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var resultado = _moduloSistemaPaqueteService.ObtenerModulos(id);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Christian A. Quispe Mamani
		/// Fecha: 31/10/2023
		/// Version: 1.0
		/// <summary>
        /// Insertar un Modulo Sistema Paquete para V5
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerListaModulos(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var resultado = _moduloSistemaPaqueteService.ObtenerListaModulos(id);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
