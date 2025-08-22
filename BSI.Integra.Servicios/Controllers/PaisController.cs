using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    public class PaisController : ControllerBase
    {
        private IPaisService _paisService;
        private ITokenManager _tokenManager;
        public PaisController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _paisService = new PaisService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _paisService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Pais
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPais()
        {
            return Ok(_paisService.ObtenerPais());
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Pais para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPaisCombo()
        {
            return Ok(_paisService.ObtenerPaisCombo());
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Pais para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            return Ok(_paisService.ObtenerCombo());
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos basicos de los paises junto con su Zona Horaria.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPaisZonaHoraria()
        {
            return Ok(_paisService.ObtenerPaisZonaHoraria());
        }
        /// Tipo Función: GET
        /// Autor: Victor Hinojosa 
        /// Fecha: 25/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos basicos de los paises junto con su Zona Horaria.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        
        [HttpGet("[action]")]
        public IActionResult ObtenerPaisConEstadoVisualizacion()
        {
                return Ok(_paisService.ObtenerPaisConEstadoVisualizacion().Reverse());
        }

        /// Tipo Función: GET
        /// Autor: Margion Ramirez.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las rutas de la url
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerRutaUrlBlockStoragePais()
        {
            return Ok(new
            {
                rutaBandera = _paisService.ObtenerRutaUrlBandera(),
                rutaIcono = _paisService.ObtenerRutaUrlIcono(),
            });
        }
        /// Tipo Función: POST
        /// Autor: Margiory
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult RegistrarPais([FromForm] RegistroPaisDTO registroPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _paisService.RegistrarPais(registroPais, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe
        /// Fecha: 24/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la vista V_TPais_Filtro si Estado = 1
        /// </summary>
        /// <returns> Lista combo paises con moneda </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboConMoneda()
        {
            return Ok(_paisService.ObtenerComboConMoneda());
        }
    }
}
