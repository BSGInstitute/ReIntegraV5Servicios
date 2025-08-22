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
    /// Controlador: CajaPorRendirController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión de CajaPorRendir
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class CajaPorRendirController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CajaPorRendirController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CajaPorRendir entidad)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<CajaPorRendir> listado)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CajaPorRendir entidad)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    var respuesta = servicio.Update(entidad);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<CajaPorRendir> listado)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    var respuesta = servicio.Delete(id, usuario);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
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
                    var servicio = new CajaPorRendirService(unitOfWork);
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
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman
        /// Fecha: 15/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CajaPorRendir
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPost("ObtenerCajaPorRendir")]
        public IActionResult ObtenerCajaPorRendir([FromBody] CajaPorRendirFiltroDTO filtro)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerCajaPorRendir(filtro));
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
        /// Fecha: 15/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCajaPorRendirSolicitante/{IdResponsable}")]
        public IActionResult ObtenerCajaPorRendirSolicitante(int IdResponsable)
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
                    IdResponsable = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerCajaPorRendirSolicitante(IdResponsable));
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
        /// Fecha: 15/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpDelete("EliminarCajaPorRendirSolicitudEnviada/{id}/{idFur}/{usuario}")]
        public IActionResult EliminarCajaPorRendirSolicitudEnviada(int id, int idFur, string usuario)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.EliminarCajaPorRendirSolicitudEnviada(id, idFur, usuario));
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
        /// Fecha: 15/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPut("DevolverSolicitudEnviada")]
        public IActionResult DevolverSolicitudEnviada(CajaPorRendirDevolerDTO data)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.DevolverSolicitudEnviada(data));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los montos totales de la Caja
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpGet("ObtenerMontoTotalCaja/{idCaja}")]
        public IActionResult ObtenerMontoTotalCaja(int idCaja)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerMontoTotalCaja(idCaja));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los montos totales de la Caja
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpGet("ObtenerCajasPorRendirParaRendicion/{IdUsuario}")]
        public IActionResult ObtenerCajasPorRendirParaRendicion(int IdUsuario)
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
                    IdUsuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerCajasPorRendirParaRendicion(IdUsuario));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los montos totales de la Caja
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        [HttpGet("ObtenerCajasPorRendirSolitudEfectivo/{IdUsuario}")]
        public IActionResult ObtenerCajasPorRendirSolitudEfectivo(int IdUsuario)
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
                    IdUsuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerCajasPorRendirFinanzas(IdUsuario));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Acepta y Genera los Por rendir
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpPost("GenerarPorRendir")]
        public IActionResult GenerarPorRendir(GenerarPorRendirDTO generacionPorRendirDTO)
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
                    generacionPorRendirDTO.CajaPRCabecera.UsuarioModificacion= _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.GenerarPorRendir(generacionPorRendirDTO));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos par el detalle por rendir
        /// </summary>
        /// <param name="IdCajaPorRendirCabecera">ID CabeceraPR</param>
        /// <returns> 200 y IEnumerable<CajaPorRendirDTO> </returns>
        [HttpGet("ObtenerCajasPorRendirPorIdPorRendirCabecera/{IdCajaPorRendirCabecera}")]
        public IActionResult ObtenerCajasPorRendirPorIdPorRendirCabecera(int IdCajaPorRendirCabecera)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerCajasPorRendirPorIdPorRendirCabecera(IdCajaPorRendirCabecera));
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
        /// Fecha: 16/09/2022
        /// Versión: 1.0
        /// <summary>
        /// obtiene el monto limite del fur
        /// </summary>
        /// <returns> decimal </returns>
        [HttpGet("ObtenerMontoLimiteSolicitud/{IdFur}")]
        public IActionResult ObtenerMontoLimiteSolicitud(int IdFur)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ObtenerMontoLimiteSolicitud(IdFur));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Acepta y Genera los Por rendir
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpPost("GenerarPorRendirInmediato")]
        public IActionResult GenerarPorRendirInmediato(GenerarPorRendirInmediatoDTO PorRendirInmediatoDTO)
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
                    PorRendirInmediatoDTO.CajaPRCabecera.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.GenerarPorRendirInmediato(PorRendirInmediatoDTO));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// inserta CajaPor Rendir
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpPost("InsertarCajaPorRendir")]
        public IActionResult InsertarCajaPorRendir(DatosSolicitudDTO ObjetoDTO)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.InsertarCajaPorRendir(ObjetoDTO));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// actualzia CajaPor Rendir
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpPut("ActualizarCajaPorRendir")]
        public IActionResult ActualizarCajaPorRendir(DatosSolicitudDTO ObjetoDTO)
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
                    ObjetoDTO.IdPersonalSolicitante = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ActualizarCajaPorRendir(ObjetoDTO));
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
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// actualzia CajaPor Rendir
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        [HttpPut("ActualizarCajaPorRendirPonerEnviado")]
        public IActionResult ActualizarCajaPorRendirPonerEnviado(EsEnviadoSolicitudDTO data)
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
                    var servicio = new CajaPorRendirService(unitOfWork);
                    return Ok(servicio.ActualizarCajaPorRendirPonerEnviado(data));
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
