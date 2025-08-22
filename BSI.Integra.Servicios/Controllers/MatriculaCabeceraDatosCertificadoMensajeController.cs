using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
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
    /// Controlador: MatriculaCabeceraDatosCertificadoMensajeController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión de MatriculaCabeceraDatosCertificadoMensaje
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MatriculaCabeceraDatosCertificadoMensajeController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public MatriculaCabeceraDatosCertificadoMensajeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<MatriculaCabeceraDatosCertificadoMensaje> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<MatriculaCabeceraDatosCertificadoMensaje> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCambiosPendientes(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraDatosCertificadoMensajeService = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var estadoCambioDatos = matriculaCabeceraDatosCertificadoMensajeService.ObtenerCambiosPendientes(idMatriculaCabecera);
                return Ok(new { estadoCambioDatos });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerMatriculaCertificado(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ServiceMatriculaCabeceraDatosCertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var listado = ServiceMatriculaCabeceraDatosCertificadoMensaje.ObtenerListado(idMatriculaCabecera);


                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
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
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MatriculaCabeceraDatosCertificadoMensajes
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerMatriculaCabeceraDatosCertificadoMensaje")]
        public IActionResult ObtenerMatriculaCabeceraDatosCertificadoMensaje()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                return Ok(servicio.ObtenerMatriculaCabeceraDatosCertificadoMensaje());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MatriculaCabeceraDatosCertificadoMensajes para combo.
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
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cantidad de Registros de T_MatriculaCabeceraDatosCertificadoMensajes basado en un UserName.
        /// </summary>
        /// <param name="userName">Username de AspNetUsers</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCantidadMensajesPorUsername/{userName}")]
        public IActionResult ObtenerCantidadMensajesPorUsername(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                return Ok(servicio.ObtenerCantidadMensajesPorUsername(userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes que tiene un usuario
        /// </summary>
        /// <returns>MatriculaCabeceraDatosCertificadoMensajesDTO<returns>
        [Route("[Action]/{usuario}")]
        [HttpGet]
        public ActionResult ObtenerMensajesPorUsuario(string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioIntegraUsuario = new IntegraAspNetUserService(unitOfWork);
                var servicioMatriculaCertificadoMensajes = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);

                IntegraAspNetUser personal = unitOfWork.IntegraAspNetUserRepository.ObtenerIdPersonalPorUsuario(usuario);
                if (personal != null)
                {
                    List<MatriculaCabeceraDatosCertificadoMensajesDTO> listadoPendientes = servicioMatriculaCertificadoMensajes.ObtenerMensajesPendientes(personal.PerId);
                    List<MatriculaCabeceraDatosCertificadoMensajesDTO> listadoLeidos = servicioMatriculaCertificadoMensajes.ObtenerMensajesLeidos(personal.PerId);
                    return Ok(new { pendientes = listadoPendientes, leidos = listadoLeidos });
                }
                else
                {
                    return BadRequest("Usuario no encontrado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Miguel Quiñones
        /// Fecha: 30/09/2022
        /// Versión: 1.0
        /// <summary>
        /// En caso que  ya existan 3 o mas versiones de cambio de datos guardara un registro de certificado donde su aprovacion dependera a la confirmacion de su supervisor, caso contrario
        /// Guarda un registro nuevo de certificados aprovado
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>bool<returns>
        /// 
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCertificadoDatos([FromBody] MatriculaCabeceraDatosCertificadoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var idpersonal = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                var matriculaCabeceraDatosCertificadoMensajeService = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                var respuesta = matriculaCabeceraDatosCertificadoMensajeService.ObtenerDatosCertificadoPorMatricula(ObjetoDTO, idpersonal);
                //MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio(_integraDBContext);
                // MatriculaCabeceraDatosCertificadoDTO certificadoActual = repMatriculaCertificado.ObtenerDatosCertificadoPorMatricula(ObjetoDTO.IdMatriculaCabecera).First();


                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Daniel Huaita
        /// Fecha: 03/16/2023
        /// Versión: 1.0
        /// <summary>
        /// modifica un mensajes y lo apruba o desaprueba en base a sus respuesta
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>bool<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ModificarCertificadoMensaje([FromBody] MatriculaCabeceraDatosCertificadoMensajesDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraDatosCertificadoMensajeService(unitOfWork);
                //return Ok(servicio.ModificarCertificadoMensaje(ObjetoDTO));
                var aux = servicio.ModificarCertificadoMensaje(ObjetoDTO);
                if (aux == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("No se pudo republicar el certificado solicitado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
