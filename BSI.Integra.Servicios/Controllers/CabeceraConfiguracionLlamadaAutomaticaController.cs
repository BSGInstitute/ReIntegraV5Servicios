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
    /// Controlador: CabeceraConfiguracionLlamadaAutomaticaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de CabeceraConfiguracionLlamadaAutomatica
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class CabeceraConfiguracionLlamadaAutomaticaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CabeceraConfiguracionLlamadaAutomaticaController(IUnitOfWork unitOfWork)
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
        public IActionResult Insertar([FromBody] CabeceraConfiguracionLlamadaAutomaticaDTO entidad)
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
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
        public IActionResult InsertarLista([FromBody] List<CabeceraConfiguracionLlamadaAutomatica> listado)
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
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
        public IActionResult Actualizar([FromBody] CabeceraConfiguracionLlamadaAutomaticaDTO entidad)
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
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
        public IActionResult ActualizarLista([FromBody] List<CabeceraConfiguracionLlamadaAutomatica> listado)
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
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCabeceraConfiguracionLlamadaAutomatica")]
        public IActionResult ObtenerCabeceraConfiguracionLlamadaAutomatica()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerCabeceraConfiguracionLlamadaAutomatica());
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCombo")]
        public IActionResult ObtenerCombo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerCombo());
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboIvrEjecucion")]
        public IActionResult ObtenerComboIvrEjecucion()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new IvrEjecucionService(unitOfWork);
                    return Ok(servicio.ObtenerCombo());
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboIvrTipoConfiguracion")]
        public IActionResult ObtenerComboIvrTipoConfiguracion()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new IvrTipoConfiguracionService(unitOfWork);
                    return Ok(servicio.ObtenerCombo());
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSesionesRecordatorioWebinar/{IdPEspecifico}/{IdTipoModalidad}")]
        public IActionResult ObtenerSesionesRecordatorioWebinar(int IdPEspecifico, int IdTipoModalidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new PEspecificoSesionService(unitOfWork);
                    return Ok(servicio.ObtenerSesionesRecordatorioWebinar(IdPEspecifico, IdTipoModalidad));
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSesionesRecordatorioClases/{IdPEspecifico}/{IdTipoModalidad}")]
        public IActionResult ObtenerSesionesRecordatorioClases(int IdPEspecifico, int IdTipoModalidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new PEspecificoSesionService(unitOfWork);
                    return Ok(servicio.ObtenerSesionesRecordatorioClases(IdPEspecifico, IdTipoModalidad));
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

        /// Tipo Función: POS
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerDetalleCabeceraConfiguracionClases")]
        public IActionResult ObtenerDetalleCabeceraConfiguracionClases(FiltroDetalleCabeceraConfiguracionDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerDetalleCabeceraConfiguracionClases(filtro.IdCabecera, filtro.IdsSesion));
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

        /// Tipo Función: POS
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerDetalleCabeceraConfiguracionWebinar")]
        public IActionResult ObtenerDetalleCabeceraConfiguracionWebinar(FiltroDetalleCabeceraConfiguracionDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerDetalleCabeceraConfiguracionWebinar(filtro.IdCabecera, filtro.IdsSesion));
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDetalleCabeceraConfiguracionCuota/{IdCabecera}")]
        public IActionResult ObtenerDetalleCabeceraConfiguracionCuota(int IdCabecera)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerDetalleCabeceraConfiguracionCuota(IdCabecera));
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

        /// Tipo Función: POS
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerDetalleCabeceraConfiguracionAsistencia")]
        public IActionResult ObtenerDetalleCabeceraConfiguracionAsistencia(FiltroDetalleCabeceraConfiguracionDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerDetalleCabeceraConfiguracionAsistencia(filtro.IdCabecera, filtro.IdsSesion));
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

        /// Tipo Función: POS
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO/{IdCabecera}")]
        public IActionResult ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(int IdCabecera)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                    return Ok(servicio.ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(IdCabecera));
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("RealizarCalculoDeLlamadasDiaHoy")]
        public IActionResult RealizarCalculoDeLlamadasDiaHoy()
        {

            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.RealizarCalculoDeLlamadasDiaHoy());
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
        /// Obtiene todos los registros guardados en T_CabeceraConfiguracionLlamadaAutomatica para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDatoLlamada/{IdIvrEjecucion}")]
        public IActionResult ObtenerDatoLlamada(int IdIvrEjecucion)
        {
            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.ObtenerDatoLlamada(IdIvrEjecucion));
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
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
        [HttpPut("ActualizarIntento/{Id}")]
        public IActionResult ActualizarIntento(int Id)
        {

            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
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
        [HttpPut("ActualizarIntentoConcluido/{Id}")]
        public IActionResult ActualizarIntentoConcluido(int Id)
        {

            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.ActualizarIntentoConcluido(Id));
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
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpGet("ObtenerDetalleParaIvr/{Celular}")]
        public IActionResult ObtenerDetalleParaIvr(string Celular)
        {

            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.ObtenerDetalleParaIvr(Celular));
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
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpGet("ValidacionEjecucionDialer/{IdIvrEjecucion}")]
        public IActionResult ValidacionEjecucionDialer(int IdIvrEjecucion)
        {
            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.ObtenerRangoHoraEjecucionDialer(IdIvrEjecucion));
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
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpGet("RealizarGenerarRegistroEjecucionDialer/{IdIvrEjecucion}")]
        public IActionResult RealizarGenerarRegistroEjecucionDialer(int IdIvrEjecucion)
        {
            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.RealizarGenerarRegistroEjecucionDialer(IdIvrEjecucion));
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
        [HttpPut("ActualizarProcesoCompletadoEjecucionDialer/{IdIvrEjecucion}")]
        public IActionResult ActualizarProcesoCompletadoEjecucionDialer(int IdIvrEjecucion)
        {
            try
            {
                var servicio = new CabeceraConfiguracionLlamadaAutomaticaService(unitOfWork);
                return Ok(servicio.ActualizarProcesoCompletadoEjecucionDialer(IdIvrEjecucion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
