using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RecordatorioClasesOnlineIvrController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de RecordatorioClasesOnlineIvr
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RecordatorioClasesOnlineIvrController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public RecordatorioClasesOnlineIvrController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        ///// Tipo Función: POST
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una insercion basica a la tabla
        ///// </summary>
        ///// <param name="entidad">Entidad a insertar</param>
        ///// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        //[HttpPost("Insertar")]
        //public IActionResult Insertar([FromBody] RecordatorioClasesOnlineIvrRecibidoDTO entidad)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            var respuesta = servicio.Add(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }


        //}
        ///// Tipo Función: POST
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una insercion basica a la tabla de una lista
        ///// </summary>
        ///// <param name="listado">Lista de entidades a insertar</param>
        ///// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        //[HttpPost("InsertarLista")]
        //public IActionResult InsertarLista([FromBody] List<RecordatorioClasesOnlineIvr> listado)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            var respuesta = servicio.Add(listado);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: PUT
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una actualizacion basica a la tabla
        ///// </summary>
        ///// <param name="entidad">Entidad a modificar</param>
        ///// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        //[HttpPut("Actualizar")]
        //public IActionResult Actualizar([FromBody] RecordatorioClasesOnlineIvrRecibidoDTO entidad)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            var respuesta = servicio.Update(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: PUT
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una actualizacion basica a la tabla de una lista
        ///// </summary>
        ///// <param name="listado">Lista de entidades a actualizar</param>
        ///// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        //[HttpPut("ActualizarLista")]
        //public IActionResult ActualizarLista([FromBody] List<RecordatorioClasesOnlineIvr> listado)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            var respuesta = servicio.Update(listado);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: DELETE
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una eliminacion logica basica a la tabla
        ///// </summary>
        ///// <param name="id">Id de la entidad a eliminar</param>
        ///// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        ///// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        //[HttpDelete("Eliminar/{id}")]
        //public IActionResult Eliminar(int id)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: DELETE
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una eliminacion logica basica a la tabla de una lista
        ///// </summary>
        ///// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        ///// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        ///// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        //[HttpDelete("EliminarListado/{listaIds}/{usuario}")]
        //public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            var respuesta = servicio.Delete(listadoIds, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }


        //}
        ///// Tipo Función: GET
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene todos los registros guardados en T_RecordatorioClasesOnlineIvr
        ///// </summary>
        ///// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        //[HttpGet("ObtenerRecordatorioClasesOnlineIvr")]
        //public IActionResult ObtenerRecordatorioClasesOnlineIvr()
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        try
        //        {
        //            var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
        //            return Ok(servicio.ObtenerRecordatorioClasesOnlineIvr());
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_RecordatorioClasesOnlineIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioClasesOnlineIvr </returns>
        [HttpGet("ObtenerRecordatorioClasesOnlineIvrById/{Id}")]
        public IActionResult  ObtenerRecordatorioClasesOnlineIvrById(int Id)
        {
            try
            {
                var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
                return Ok(servicio.ObtenerRecordatorioClasesOnlineIvrById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioClasesOnlineIvrDTO> </returns>
        [HttpGet("ObtenerDatoLlamadaRecordatorioClasesOnline")]
        public IActionResult ObtenerDatoLlamadaRecordatorioClasesOnline()
        {

            try
            {
                var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
                return Ok(servicio.ObtenerDatoLlamadaRecordatorioClasesOnline());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioClasesOnlineIvrDTO> </returns>
        [HttpGet("ObtenerDatoLlamadaRecordatorioClasesOnlineById/{Id}")]
        public IActionResult ObtenerDatoLlamadaRecordatorioClasesOnlineById(int Id)
        {

            try
            {
                var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
                return Ok(servicio.ObtenerDatoLlamadaRecordatorioClasesOnlineById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el intento
        /// </summary>
        /// <returns> bool</returns>
        [HttpPut("ActualizarIntento/{Id}")]
        public IActionResult ActualizarIntento(int Id)
        {

            try
            {
                var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
                return Ok(servicio.ActualizarIntento(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpPut("ActualizarConcluido/{Id}")]
        public IActionResult ActualizarConcluido(int Id)
        {

            try
            {
                var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
                return Ok(servicio.ActualizarConcluido(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpPut("ActualizarIntentoConcluido/{Id}")]
        public IActionResult ActualizarIntentoConcluido(int Id)
        {

            try
            {
                var servicio = new RecordatorioClasesOnlineIvrService(unitOfWork);
                return Ok(servicio.ActualizarIntentoConcluido(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpPost("convertirHora")]
        public IActionResult convertirHora(int horas)
        {

            try
            {
                var fecha = DateTime.Now;
                fecha = fecha.AddHours(horas);
                var hora = fecha.ToString("h:mmtt", System.Globalization.CultureInfo.InvariantCulture);
                return Ok(hora);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
