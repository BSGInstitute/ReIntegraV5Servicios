using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionProyeccionFurController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class ConfiguracionProyeccionFurController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ConfiguracionProyeccionFurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor:Margiory Ramirez
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Inserta los fregristross  a la  tabal TipoDaato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult InsertarConfiguracionProyeccionFur([FromBody] ConfiguracionProyeccionFurDTO entidad)
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
                    var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                    var respuesta = servicio.InsertarConfiguracionProyeccionFur(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
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
        /// Autor:Margiory Ramirez
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Actuliza  los fregristros  a la  tabal 
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        [HttpPut("[Action]")]
        public IActionResult ActualizarConfiguracionProyeccionFur([FromBody] ConfiguracionProyeccionFurDTO entidad)
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
                    var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                    var respuesta = servicio.ActualizarConfiguracionProyeccionFur(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
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
        /// Autor:Margiory Ramirez
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtniene todos los registros de la tabla
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        [HttpGet("[Action]")]
        public IActionResult ObtenerConfiguracionProyeccionFur()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                return Ok(servicio.ObtenerConfiguracionProyeccionFur());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez.
        /// Fecha: 08/03/2023
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
                    var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                    var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
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
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// desactiva la configuracion del fur
        /// </summary>
        /// <returns> bool </returns>
        [Authorize]
        [HttpPost("DesactivarConfiguracion/{Id}")]
        public IActionResult DesactivarConfiguracion(int Id)
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
                    var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                    var respuesta = servicio.desactivarConfiguracion(Id,_respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
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
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Cambia los etados de activo para la configuracion de fURS
        /// </summary>
        /// <returns> bool </returns>
        [Authorize]
        [HttpPost("CambiarActivoConfiguracion/{IdNuevo}")]
        public IActionResult CambiarActivoConfiguracion([FromBody]List<int> IdActual, int IdNuevo)
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
                    var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                    var respuesta = servicio.CambiarActivoConfiguracion(IdActual, IdNuevo, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
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









































