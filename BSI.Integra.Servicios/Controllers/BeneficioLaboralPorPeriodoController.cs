using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: BeneficioLaboralPorPeriodoController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class BeneficioLaboralPorPeriodoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public BeneficioLaboralPorPeriodoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
        public IActionResult Insertar([FromBody] BeneficioLaboralPorPeriodo entidad)
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
                    entidad.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    entidad.UsuarioCreacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                    var respuesta = servicio.Add(entidad);
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<BeneficioLaboralPorPeriodo> listado)
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
                    var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                    var respuesta = servicio.Add(listado);
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] BeneficioLaboralPorPeriodo entidad)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                var respuesta = servicio.Update(entidad);
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
        public IActionResult ActualizarLista([FromBody] List<BeneficioLaboralPorPeriodo> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
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
                    usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                    var respuesta = servicio.Delete(listadoIds, usuario);
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
        /// Autor: Griselberto Huamanc.
        /// Fecha: 31/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Retorna lista de Beneficios laborales correspondientes al periodo.
        /// </summary>
        /// <param name="ObjetoDTO">Lista de parametros</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpGet("ObtenerBeneficioLaboralSegunPeriodo/{IdPeriodo}")]
        public IActionResult ObtenerBeneficioLaboralSegunPeriodo(int IdPeriodo)
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
                    var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                    var respuesta = servicio.ObtenerBeneficioLaboralSegunPeriodo(IdPeriodo);
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
        /// Autor: Griselberto Huamanc.
        /// Fecha: 31/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza la insercion o actualizacion de los valores Beneficios laborales area Comercial.
        /// </summary>
        /// <param name="ObjetoDTO">Lista de parametros</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpPost("InsertarBeneficioLaboralPorPeriodo")]
        public IActionResult InsertarBeneficioLaboralPorPeriodo([FromBody] ListaBeneficioLaboralDTO ObjetoDTO)
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
                    ObjetoDTO.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new BeneficioLaboralPorPeriodoService(unitOfWork);
                    var respuesta = servicio.InsertarBeneficioLaboralPorPeriodo(ObjetoDTO);
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
