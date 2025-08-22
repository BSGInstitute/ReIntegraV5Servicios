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
    /// Controlador: EstadoMatriculaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de EstadoMatricula
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EstadoMatriculaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public EstadoMatriculaController(IUnitOfWork unitOfWork)
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
        public IActionResult Insertar([FromBody] EstadoMatricula entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public IActionResult InsertarLista([FromBody] List<EstadoMatricula> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public IActionResult Actualizar([FromBody] EstadoMatricula entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public IActionResult ActualizarLista([FromBody] List<EstadoMatricula> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        [HttpDelete("EliminarListado")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_EstadoMatricula
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerEstadoMatricula")]
        public IActionResult ObtenerEstadoMatricula()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                return Ok(servicio.ObtenerEstadoMatricula());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_EstadoMatricula para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                return Ok(servicio.ObtenerEstadoMatriculaCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los SubEstados de Subestado,a traves de un proceso alamacenado
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSubEstadoIndividual/{IdEstadoMatricula}")]
        public IActionResult ObtenerSubEstadoIndividual(int IdEstadoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                return Ok(servicio.ObtenerSubEstadoIndividual(IdEstadoMatricula));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de T_EstadoMatricula asociados a una Matricula Activa para mostrarse en combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerEstadoMatriculaParaMatriculados")]
        public IActionResult ObtenerEstadoMatriculaParaMatriculados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EstadoMatriculaService(unitOfWork);
                return Ok(servicio.ObtenerEstadoMatriculaParaMatriculados());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// inserta en T_EstadoMatricula y actualiza en T_SubEstadoMatricula
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("InsertarEstadoSubestado")]
        public IActionResult InsertarEstadoSubestado([FromBody] CRUDEstadoMatriculaDTO data)
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
                    data.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new EstadoMatriculaService(unitOfWork);
                    return Ok(servicio.InsertarEstadoSubestado(data));
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
        /// elmimar en T_EstadoMatricula y actualiza en T_SubEstadoMatricula
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpDelete("EliminarEstadoSubEstado/{id}")]
        public IActionResult EliminarEstadoSubEstado(int id)
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
                    var servicio = new EstadoMatriculaService(unitOfWork);
                    return Ok(servicio.EliminarEstadoSubEstado(id, _respuestaCorrecta.RegistroClaimToken.UserName));
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
        /// Actualiza en T_EstadoMatricula , actualiza en T_SubEstado
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPut("EditarEstado")]
        public IActionResult EditarEstado([FromBody] CRUDEstadoMatriculaDTO data)
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
                    data.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new EstadoMatriculaService(unitOfWork);
                    return Ok(servicio.EditarEstado(data));
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

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Estado Matriculado de Alumno por Id
        /// </summary>
        /// <param name="idAlumno">Id del alumno</param>
        /// <returns> Estado Matriculado de Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<EstadoMatriculadoDTO> </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerEstadoMatriculado(int idAlumno)
        {
            if (idAlumno <= 0)
            {
                return BadRequest("Id de alumno no válido.");
            }
            try
            {
                var servicioEstadoMatriculado = new EstadoMatriculaService(unitOfWork);
                var estados = servicioEstadoMatriculado.ObtenerEstadoMatriculado(idAlumno);
                return Ok(estados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ///// TipoFuncion: GET
        ///// Autor: Joseph LLanque.
        ///// Fecha: 27/09/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene Estado Matriculado de Alumno por Id
        ///// </summary>
        ///// <param name="idAlumno">Id del alumno</param>
        ///// <returns> Estado Matriculado de Alumno </returns>
        ///// <returns> Lista de Objeto DTO : List<EstadoMatriculadoDTO> </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerMatriculaAlumno(int idAlumno)
        {
            if (idAlumno <= 0)
            {
                return BadRequest("Id de alumno no válido.");
            }
            try
            {
                var servicioEstadoMatriculado = new EstadoMatriculaService(unitOfWork);
                var estados = servicioEstadoMatriculado.ObtenerMatriculaAlumno(idAlumno);
                return Ok(estados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// FUNCION QUE DEVUELVE LOS ESTADOS DE MATRICULA
        /// Agenda-BandejaCorreosOperaciones.js
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerFiltroEstadosMatricula()//LPPG
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaService servicioEstadoMatricula = new EstadoMatriculaService(unitOfWork);
                var lista = servicioEstadoMatricula.ObtenerEstadosMatricula();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// FUNCION QUE DEVUELVE LOS SUBESTADOS DE UN ID DE MATRICULA ESTADO
        /// </summary>
        /// <param name="idEstadoMatricula"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult FiltroObtenerSubEstadosMatricula(List<int> idEstadoMatricula) //LPPG
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaService estadoMatriculaService = new EstadoMatriculaService(unitOfWork);
                List<TCRM_SubEstadoMatriculaDTO> listEstadosMatricula = new List<TCRM_SubEstadoMatriculaDTO>();
                listEstadosMatricula = estadoMatriculaService.FiltroObtenerSubEstadosMatricula(idEstadoMatricula);
                return Ok(listEstadosMatricula);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
