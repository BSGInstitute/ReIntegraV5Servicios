using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GenerarFurController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de GenerarFur
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GenerarFurController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public GenerarFurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las ciudades de cada sede BSG, para mostrarse en combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCiudadesDeSedesExistentes")]
        public IActionResult ObtenerCiudadesDeSedesExistentes()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new CiudadService(unitOfWork);
                    return Ok(servicio.ObtenerCiudadesDeSedesExistentes());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las ciudades de cada sede BSG, para mostrarse en combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerNivelAcceso")]
        public IActionResult ObtenerNivelAcceso()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerNivelAcceso(_respuestaCorrecta.RegistroClaimToken.UserName, _respuestaCorrecta.RegistroClaimToken.IdPersonal));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
             }
            else
            {
                return Unauthorized();
            }
            
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Datos para el llenado de grilla
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerFursParaGrid")]
        public IActionResult ObtenerFursParaGrid([FromBody] ParametrosFurDTO json)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    json.IdRol = _respuestaCorrecta.RegistroClaimToken.IdRol;
                    json.IdPersonal = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                    json.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerFursParaGrid(json));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }


        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el FUR segun el codigo.
        /// </summary>
        /// <param name="codigo">Codigo del FUR</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerFursBusquedaCodigo/{codigo}")]
        public IActionResult ObtenerFursBusquedaCodigo(string codigo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerFursBusquedaCodigo(codigo));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
           
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Centro de Costo Autocomplete.
        /// </summary>
        /// <param name="codigo">Codigo del FUR</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCentroCostoAutomatico/{codigo}")]
        public IActionResult ObtenerCentroCostoAutomatico(string codigo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new CentroCostoService(unitOfWork);
                    return Ok(servicio.ObtenerCentroCostoAutoComplete(codigo));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
           
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de TipoPedidoFur.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerTipoPedidoFur")]
        public IActionResult ObtenerTipoPedidoFur()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new FurTipoPedidoService(unitOfWork);
                    return Ok(servicio.ObtenerTipoPedidoFur());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene servicios de un proveedor para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProductoFur/{IdProveedor}")]
        public IActionResult ObtenerProductoFur(int IdProveedor)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerProductoFur(IdProveedor));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Fur.
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpPut("ActualizarFur")]
        public IActionResult ActualizarFur([FromBody] FurDTO Json)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    Json.UsuarioCreacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    Json.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    Json.FechaModificacion = DateTime.Now;
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ActualizarFur(Json));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }


        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta Fur.
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpPost("InsertarFur")]
        public IActionResult InsertarFur(FurDTO Json)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    Json.UsuarioCreacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    Json.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    Json.FechaModificacion = DateTime.Now;
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.InsertarFur(Json));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta Fur.
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpPost("AprobarFurProyectado")]
        public IActionResult AprobarFurProyectado([FromBody] FurAprobarPoryectadosDTO json)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    json.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.AprobarFurProyectado(json));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
