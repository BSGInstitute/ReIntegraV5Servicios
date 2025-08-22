using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CiudadController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Ciudad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CiudadController : Controller
    {
        private ICiudadService _ciudadService;
        private IUnitOfWork unitOfWork;
        public CiudadController(IUnitOfWork _unitOfWork)
        {
            _ciudadService = new CiudadService(_unitOfWork);
            unitOfWork = _unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        [Authorize]
        public IActionResult Insertar([FromBody] CiudadEnvioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _ciudadService.Insertar(dto, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarColonia([FromBody] CiudadColoniaDTO dto)
        {
            try
            {
                unitOfWork.CiudadRepository.insertarColonia(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        [Authorize]

        public IActionResult InsertarLista([FromBody] List<Ciudad> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.Insertar(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        [Authorize]

        public IActionResult Actualizar([FromBody] CiudadEnvioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _ciudadService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        [Authorize]

        public IActionResult ActualizarLista([FromBody] List<Ciudad> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.Actualizar(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 28/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una ELIMINACION LOGICA
        /// </summary>
        /// <param name="id">Id registro</param>
        /// <returns>Retorna 200 y true o false </returns>
        [HttpDelete("Eliminar/{id}")]
        [Authorize]

        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _ciudadService.Eliminar(id, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de id entidades a actualizar</param>
        /// <returns>Retorna 200 y true o false </returns>
        [HttpPut("[Action]")]
        [Authorize]

        public IActionResult ActualizarCiudadesMultiples([FromBody] CiudadMultipleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _ciudadService.ActualizarCiudadMultiple(dto, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Ciudad para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var respuesta = _ciudadService.ObtenerCombo();
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de T_RegionCiudad para mostrarse en combo.
        /// </summary>
        /// <returns> List<RegionCiudadComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboRegionCiudad()
        {
            var respuesta = _ciudadService.ObtenerComboRegionCiudad();
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Ciudad
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerCiudad();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerNombreCiudadPorIdPais/{idPais}")]
        public IActionResult ObtenerNombreCiudadPorIdPais(int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerNombreCiudadPorIdPais(idPais);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[Action]")]
        public IActionResult ObtenerCiudadesPorPais(string idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerCiudadesPorPais(idPais);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la tabla T_Municipios
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        [HttpGet("[Action]/{idCiudadRef}")]
        public IActionResult ObtenerMunicipioPorCiudad(int idCiudadRef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerMunicipioPorCiudad(idCiudadRef);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la tabla T_Colonia
        /// </summary>
        /// <returns> List<ColoniaDTO> </returns>
        [HttpGet("[Action]/{idCiudadRef}/{idMunicipioMexico}")]
        public IActionResult ObtenerAsentamientoPorMunicipio(int idCiudadRef, int idMunicipioMexico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerAsentamientoPorMunicipio(idCiudadRef, idMunicipioMexico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[Action]/{codigoPostal}")]
        public IActionResult BusquedaDatosMexicoPorCodigoPostal(string codigoPostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.BusquedaPorCodigoPostal(codigoPostal);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani
        /// Fecha: 28/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la tabla T_Ciudad
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult VisualizarCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerTodoCiudades();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[Action]/{idCiudadRef}/{idCiudadMexico}")]
        public IActionResult ObtenerMunicipioPorEstadoyCiudad(int idCiudadRef, int? idCiudadMexico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerMunicipioPorEstadoyCiudad(idCiudadRef, idCiudadMexico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la tabla T_AsentamientoMexico
        /// </summary>
        /// <returns> List<ColoniaDTO> </returns>
        [HttpGet("[Action]/{idCiudadRef}/{idMunicipioMexico}/{idCiudadMexico}")]
        public IActionResult ObtenerAsentamientoPorMunicipioyCiudadMexico(int idCiudadRef, int idMunicipioMexico, int? idCiudadMexico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerAsentamientoPorMunicipioyCiudadMexico(idCiudadRef, idMunicipioMexico, idCiudadMexico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la tabla T_Colonia
        /// </summary>
        /// <returns> List<ColoniaDTO> </returns>
        [HttpGet("[Action]/{codigoPostal}")]
        public IActionResult BusquedaPorCodigoPostal(string codigoPostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.BusquedaPorCodigoPostal(codigoPostal);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros dentro de la tabla T_Municipios
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        [HttpGet("[Action]/{idCiudadRef}")]
        public IActionResult ObtenerCiudadMexicoByIdEstadoMexico(int idCiudadRef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _ciudadService.ObtenerCiudadMexicoByIdEstadoMexico(idCiudadRef);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
