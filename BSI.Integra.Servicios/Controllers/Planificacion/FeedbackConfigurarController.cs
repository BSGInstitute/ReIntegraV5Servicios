using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using System;
using System.Security.Claims;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{


    /// Controlador: FeedbackConfigurarController
    /// Autor: Marco Jose Villanueva Torres
    /// Fecha: 29/09/2023
    /// <summary>
    /// Gestión de FeedbackConfigurar
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FeedbackConfigurarController : Controller
    {
        private ITokenManager _tokenManager;
        private IFeedbackConfigurarService _feedbackConfigurarService;
        

       
        private IUnitOfWork _unitOfWork;
        public FeedbackConfigurarController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _feedbackConfigurarService = new FeedbackConfigurarService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Tipo Función: GET
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el feedbackConfigurar
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _feedbackConfigurarService.Obtener();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Sexo
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]   
        public IActionResult ObtenerComboSexo()
        {
            var resultado = _feedbackConfigurarService.ObtenerComboSexo();
            return Ok(resultado);
        }


        /// Tipo Función: POST  
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad FeedbackConfigurarDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>


        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public ActionResult Insertar(FeedbackConfigurarDTO feedbackConfigurarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _feedbackConfigurarService.Insertar(feedbackConfigurarDTO, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT  
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] FeedbackConfigurarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _feedbackConfigurarService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }



        /// Tipo Función: DELETE
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idFeedbackConfigurar}")]
        public IActionResult Eliminar(int idFeedbackConfigurar)
        {
            var respuesta = _feedbackConfigurarService.Eliminar(idFeedbackConfigurar, _tokenManager.UserName);
            return Ok(respuesta);
        }


    }
}
