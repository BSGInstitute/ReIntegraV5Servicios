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
    /// Controlador: CajaEgresoController
    /// Autor: Griselberto Huaman
    /// Fecha: 20/09/2022
    /// <summary>
    /// Gestión de CajaEgreso
    /// </summary>
    /// 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class CajaEgresoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CajaEgresoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CajaEgreso entidad)
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
                    entidad.UsuarioCreacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    entidad.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CajaEgresoService(unitOfWork);
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
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<CajaEgreso> listado)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CajaEgreso entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<CajaEgreso> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
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
                var servicio = new CajaEgresoService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
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
                var servicio = new CajaEgresoService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CajaEgreso
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPost("ObtenerCajaEgresoEnviado")]
        public IActionResult ObtenerCajaEgresoEnviado([FromBody] FiltroCajaEgresoDTO filtro)
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
                    filtro.idPersonalResponsable = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.ObtenerCajaEgresoEnviado(filtro));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CajaEgreso
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPost("InsertarCajaEgreso")]
        public IActionResult InsertarCajaEgreso([FromBody] InsertCajaEgresoDTO RequestDTO)
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
                    RequestDTO.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.InsertarCajaEgreso(RequestDTO));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Devuelve la solicitud de Caja Egreso
        /// </summary>
        /// <returns> true </returns>
        [HttpPut("DevolverSolicitudCajaEgreso")]
        public IActionResult DevolverSolicitudCajaEgreso([FromBody] CajaPorRendirDevolerDTO data)
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
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.DevolverSolicitudCajaEgreso(data.Id, data.Usuario));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Combo de T_CajaPorRendirCabecera
        /// </summary>
        /// <returns> true </returns>
        [HttpGet("ObtenerComboCabeceraPR")]
        public IActionResult ObtenerComboCabeceraPR()
        {
            try
            {
                var servicio = new CajaPorRendirCabeceraService(unitOfWork);
                return Ok(servicio.ObtenerComboCabeceraPR());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Combo de T_CajaPorRendirCabecera
        /// </summary>
        /// <returns> true </returns>
        [HttpGet("ObtenerRegistrosCajaEgreso/{IdCajaPorRendirCabecera}")]
        public IActionResult ObtenerRegistrosCajaEgreso(int IdCajaPorRendirCabecera)
        {
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                return Ok(servicio.ObtenerRegistrosCajaEgreso(IdCajaPorRendirCabecera));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Combo de T_CajaPorRendirCabecera
        /// </summary>
        /// <returns> true </returns>
        [HttpGet("ObtenerCajaPorRendirSolicitanteREC")]
        public IActionResult ObtenerCajaPorRendirSolicitanteREC()
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
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.ObtenerCajaPorRendirSolicitanteREC(_respuestaCorrecta.RegistroClaimToken.IdPersonal));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina la CajaEgreso , su comprobanteFur, Libera el Fur
        /// </summary>
        /// <returns> true </returns>
        [HttpDelete("EliminarCajaEgreso/{id}")]
        public IActionResult EliminarCajaEgreso(int id)
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
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.EliminarCajaEgreso(id, _respuestaCorrecta.RegistroClaimToken.UserName));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina la CajaEgreso , su comprobanteFur, Libera el Fur
        /// </summary>
        /// <returns> true </returns>
        [HttpPut("ActualizarRegistroEgresoCajaEnviado")]
        public IActionResult ActualizarRegistroEgresoCajaEnviado([FromBody] CajaEgresoActualizar data)
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
                    var servicio = new CajaEgresoService(unitOfWork);
                    data.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(servicio.ActualizarRegistroEgresoCajaEnviado(data));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina la CajaEgreso , su comprobanteFur, Libera el Fur
        /// </summary>
        /// <returns> true </returns>
        [HttpPut("ActualizarCajaEgresoEstablecerRendido/{IdPersonal}/{IdCajaPorRendirCabecera}")]
        public IActionResult ActualizarCajaEgresoEstablecerRendido(int IdPersonal, int IdCajaPorRendirCabecera)
        {
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                return Ok(servicio.ActualizarCajaEgresoEstablecerRendido(IdPersonal, IdCajaPorRendirCabecera));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina la CajaEgreso , su comprobanteFur, Libera el Fur
        /// </summary>
        /// <returns> true </returns>
        [HttpPost("GenerarRegistroEgresoCaja")]
        public IActionResult GenerarRegistroEgresoCaja([FromBody] GenerarRegistroEgresoDTO Data)
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
                    Data.CajaRECAprobado.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.GenerarRegistroEgresoCaja(Data));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina la CajaEgreso , su comprobanteFur, Libera el Fur
        /// </summary>
        /// <returns> true </returns>
        [HttpPost("GenerarRegistroEgresoCajaInmediato")]
        public IActionResult GenerarRegistroEgresoCajaInmediato([FromBody] GenerarRegistroEgresoInmediatoDTO Data)
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
                    Data.CajaEgresoAprobado.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.GenerarRegistroEgresoCajaInmediato(Data));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el monto limite del FUR
        /// </summary>
        /// <returns> true </returns>
        [HttpGet("ObtenerMontoLimite/{IdFur}")]
        public IActionResult ObtenerMontoLimite(int IdFur)
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
                    var servicio = new CajaEgresoService(unitOfWork);
                    return Ok(servicio.ObtenerMontoLimite(IdFur));
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
