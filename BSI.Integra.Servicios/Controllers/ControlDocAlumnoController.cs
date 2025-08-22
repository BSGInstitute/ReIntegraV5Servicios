using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ControlDocAlumnoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión de ControlDocAlumno
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ControlDocAlumnoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ControlDocAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la calificación de matriculados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarCriterioCalificacionMatricula([FromBody] CriterioObservacionDTO dto)
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
                    IControlDocAlumnoService controlDocAlumnoService = new ControlDocAlumnoService(unitOfWork);
                    return Ok(controlDocAlumnoService.ActualizarCriterioCalificacion(dto, _respuestaCorrecta.RegistroClaimToken.UserName));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
                throw new Aplicacion.Base.Exceptions.UnauthorizedAccessRequestException("Sesion Finalizada");
        }

        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Actuailza la matrícula calificación de matrículados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarMatriculaObservacionMatricula([FromBody] CriterioObservacionDTO dto)
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
                    var servicioControlDocAlumno = new ControlDocAlumnoService(unitOfWork);
                    if (dto != null)
                    {
                        IControlDocAlumnoService controlDocAlumnoService = new ControlDocAlumnoService(unitOfWork);
                        return Ok(controlDocAlumnoService.ActualizarMatriculaObservacion(dto, _respuestaCorrecta.RegistroClaimToken.UserName));
                    }
                    else
                        throw new BadRequestException("No existe Valores en el objeto");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                throw new Aplicacion.Base.Exceptions.UnauthorizedAccessRequestException("Sesion Finalizada");
        }
    }
}
