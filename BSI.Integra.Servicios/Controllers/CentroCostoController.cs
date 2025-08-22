using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CentroCostoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión de CentroCosto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CentroCostoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        private ICentroCostoService _centroCostoService;

        public CentroCostoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _centroCostoService = new CentroCostoService(_unitOfWork);
        }
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] CentroCostoDTO centrocostoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _centroCostoService.Insertar(centrocostoDTO, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CentroCostoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _centroCostoService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _centroCostoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CentroCosto para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_centroCostoService.ObtenerCombo());
        }
        /// Tipo Función: GET 
        /// Autor: Gretel Canasa
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de CriterioEvaluacion y su detalles  
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCombosModulo()
        {
            var resultado = await _centroCostoService.ObtenerCombosModulo();
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo con antiguedad maxima de un anio basado en un Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Centro de Costo</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerRecientesAutocomplete")]
        public IActionResult ObtenerRecientesAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_centroCostoService.ObtenerRecientesAutocomplete(Filtros["valor"].ToString()));
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="Filtros">Filtros que contienen el Nombre Parcial</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerAutocomplete([FromBody] StringDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_centroCostoService.ObtenerFiltroAutocomplete(filtro.Valor));
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 14/03/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="filtro">StringDTO filtro centro costos</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult ObtenerAutocompleteV2([FromBody] StringDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_centroCostoService.ObtenerAutocompleteV2(filtro.Valor, _tokenManager.UserName));
        }
        /// Autor: Daniel Huaita
        /// Fecha: 16/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial que estan en estado lanzamiento y ejecucion.
        /// </summary>
        /// <param name="Filtros">Filtros que contienen el Nombre Parcial</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerAutocompleteCentroCosto")]
        public IActionResult ObtenerAutocompleteCentroCosto([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_centroCostoService.ObtenerAutocompleteCentroCosto(Filtros["valor"].ToString()));
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 09/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del centro de costo.
        /// </summary>
        /// <param name="idPEspecifico">id del programa Especifico</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDatosDelCentrodeCosto/{idPEspecifico}")]
        public IActionResult ObtenerDatosDelCentrodeCosto(int idPEspecifico)
        {
            return Ok(_centroCostoService.ObtenerDatosDelCentrodeCosto(idPEspecifico));
        }

        /// Tipo Función: GET
        /// Autor: Gretel Canasa 
        /// Fecha: 09/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del centro de costo.
        /// </summary>
        /// <param name="idPEspecifico">id del programa Especifico</param>
        /// <returns> retorna CentroCostoDTO </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            return Ok(_centroCostoService.Obtener());
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa 
        /// Fecha: 09/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del centro de costo con informacion de usarios creacion y modificacion
        /// </summary>
        /// <param name="idPEspecifico">id del programa Especifico</param>
        /// <returns> retorna CentroCostoDTO </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCcDatosUsuarios()
        {
            return Ok(_centroCostoService.ObtenerCcDatosUsuarios());
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa 
        /// Fecha: 04/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del centro de costo por Id.
        /// </summary>
        /// <param name="idPEspecifico">id del programa Especifico</param>
        /// <returns> retorna CentroCostoDTO </returns>
        [HttpGet("[action]/{id}")]
        public IActionResult ObtenerMasAdicionales(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_centroCostoService.ObtenerMasAdicionales(id));
        }

        /// Tipo Función: GET
        /// Autor: Gretel Canasa 
        /// Fecha: 04/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos Pgeneral por IdSubArea para Centro Costo.
        /// </summary>
        /// <param name="idSubArea">id del programa Especifico</param>
        /// <returns> retorna CentroCostoDTO </returns>
        [HttpGet("[action]/{idSubArea}")]
        public async Task<IActionResult> GetTroncalPgeneralBySubAreaAsync(int idSubArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _centroCostoService.ObtenerPGeneralPorIdSubAreaAsync(idSubArea));
        }
    }
}
