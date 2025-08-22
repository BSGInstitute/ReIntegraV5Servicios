using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
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
    /// Controlador: ConvocatoriaPersonalController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ConvocatoriaPersonal
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConvocatoriaPersonalController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ConvocatoriaPersonalController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ConvocatoriaPersonalRecibidoDTO entidad)
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
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    var respuesta = servicio.Add(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<ConvocatoriaPersonal> listado)
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
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] ConvocatoriaPersonalRecibidoDTO entidad)
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
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    var respuesta = servicio.Update(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<ConvocatoriaPersonal> listado)
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
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    var respuesta = servicio.Update(listado);
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
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
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
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
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
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{listaIds}/{usuario}")]
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
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    var respuesta = servicio.Delete(listadoIds, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ConvocatoriaPersonal
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerConvocatoriasRegistradas")]
        public IActionResult ObtenerConvocatoriasRegistradas()
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    return Ok(servicio.ObtenerConvocatoriasRegistradas());
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
        /// Obtiene todos los registros guardados en T_ConvocatoriaPersonal
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDetalleConvocatorias/{idConvocatoria}")]
        public IActionResult ObtenerDetalleConvocatorias(int idConvocatoria)
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    return Ok(servicio.ObtenerDetalleConvocatorias(idConvocatoria));
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
        /// Obtiene todos los registros de Sede por NOmbre
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboPorNombreSede/{Sede}")]
        public IActionResult ObtenerComboPorNombreSede(string Sede)
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new SedeService(unitOfWork);
                    return Ok(servicio.ObtenerComboPorNombreSede(Sede));
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
        /// Obtiene todos los registros de Sede por NOmbre
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProveedoresConvocatoriaPersonal")]
        public IActionResult ObtenerProveedoresConvocatoriaPersonal()
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new ProveedorService(unitOfWork);
                    return Ok(servicio.ObtenerProveedoresConvocatoriaPersonal());
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

        // Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal responsable de seleccion
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        [HttpGet("ObtenerComboPersonalGestionPersonas")]
        public IActionResult ObtenerComboPersonalGestionPersonas()
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new PersonalService(unitOfWork);
                    return Ok(servicio.ObtenerComboPersonalGestionPersonas());
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

        // Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal responsable de seleccion
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        [HttpGet("ObtenerSedeTrabajoCombo")]
        public IActionResult ObtenerSedeTrabajoCombo()
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new SedeTrabajoService(unitOfWork);
                    return Ok(servicio.ObtenerSedeTrabajoCombo());
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
        // Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal responsable de seleccion
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        [HttpGet("ObtenerProcesoSeleccionCombo")]
        public IActionResult ObtenerProcesoSeleccionCombo()
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new ProcesoSeleccionService(unitOfWork);
                    return Ok(servicio.ObtenerProcesoSeleccionConvocatoria());
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

        // Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal responsable de seleccion
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        [HttpGet("ObtenerTodosCombosConvotoriaPersonal")]
        public IActionResult ObtenerTodosCombosConvotoriaPersonal()
        {
            var claimsidentity = User.Identity as ClaimsIdentity;
            var _respuestacorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsidentity);

            if (_respuestacorrecta.TokenValida)
            {
                try
                {
                    var servicio = new ConvocatoriaPersonalService(unitOfWork);
                    return Ok(servicio.ObtenerTodosCombosConvotoriaPersonal());
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
