using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CargoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión de Cargo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class CargoController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ICargoService _cargoService;
        public CargoController(IUnitOfWork unitOfWork, ICargoService cargoService)
        {
            this.unitOfWork = unitOfWork;
            _cargoService = cargoService;
        }

        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cargo por Id
        /// </summary>
        /// <param name="id">Id del Cargo</param>
        /// <returns> CargoDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _cargoService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de T_Cargo
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] CargoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _cargoService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Cargo </param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CargoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _cargoService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de Cargo
        /// </summary>
        /// <param name="id">Id de Cargo</param>
        /// <returns>True</returns>
        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _cargoService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Cargo
        /// </summary>
        /// <returns> lista de CargoDTO </returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _cargoService.Obtener();
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Cargo para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CargoService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerCargoFiltro")]
        public IActionResult ObtenerCargoFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CargoService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
