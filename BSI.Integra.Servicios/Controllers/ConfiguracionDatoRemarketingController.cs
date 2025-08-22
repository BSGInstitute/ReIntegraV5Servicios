using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionDatoRemarketingController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionDatoRemarketingController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ConfiguracionDatoRemarketingController(IUnitOfWork unitOfWork)
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
        public IActionResult Insertar([FromBody] ConfiguracionDatoRemarketing entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
                var respuesta = servicio.Add(entidad);
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
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<ConfiguracionDatoRemarketing> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
                var respuesta = servicio.Add(listado);
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
        public IActionResult Actualizar([FromBody] ConfiguracionDatoRemarketing entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
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
        public IActionResult ActualizarLista([FromBody] List<ConfiguracionDatoRemarketing> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
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
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros guardados en T_ConfiguracionDatoRemarketing para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }

        /// Tipo Función: GET
        /// Autor:Margiory Ramirez Neyra.
        /// Fecha:04/0/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de configuracion de remarketing
        /// </summary>
        /// <returns>Response 200 con lista de objetos de clase ConfiguracionDatoRemarketingAgrupadoGrillaDTO, caso contrario response 400 con el mensaje de error</returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerListaConfiguracionesDatoRemarketing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
                return Ok(servicio.ObtenerConfiguracionesDatoRemarketing());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de combos de configuracion de dato remarketing
        /// </summary>
        /// <returns>Response 200 con lista de objetos de clase ComboConfiguracionDatoRemarketingDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosParaConfiguracionDatoRemarketing()

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);

                return Ok(servicio.ObtenerCombosParaConfiguracionDatoRemarketing());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la configuracion de dato remarketing
        /// </summary>
        /// <param name="ConfiguracionDatoRemarketingAActualizar">Objeto de clase ConfiguracionDatoRemarketingDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConfiguracionDatoRemarketing([FromBody] ConfiguracionDatoRemarketingDTO ConfiguracionDatoRemarketingActualizar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);


                bool resultadoActualizacion = servicio.ActualizarListaConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingActualizar);

                return Ok(resultadoActualizacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina la configuracion de dato remarketing
        /// </summary>
        /// <param name="ConfiguracionDatoRemarketingAEliminar">Objeto de clase ConfiguracionDatoRemarketingAEliminarDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Authorize]
        [Route("EliminarConfiguracionDatoRemarketing/{id}")]
        [HttpDelete]
        public ActionResult EliminarConfiguracionDatoRemarketing(int id)
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
                    var servicio = new ConfiguracionDatoRemarketingService(unitOfWork);
                    ConfiguracionDatoRemarketingAEliminarDTO data = new ConfiguracionDatoRemarketingAEliminarDTO()
                    {
                        Id = id,
                        Usuario = _respuestaCorrecta.RegistroClaimToken.UserName
                };

                    bool resultadoActualizacion = servicio.EliminarConfiguracionDatoRemarketingGeneral(data);

                    return Ok(resultadoActualizacion);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
           
        }


    }
}
