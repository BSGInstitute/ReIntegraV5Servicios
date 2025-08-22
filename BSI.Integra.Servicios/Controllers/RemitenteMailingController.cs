using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueSendersDTO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RemitenteMailingController
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 09/11/2022
    /// <summary>
    /// Gestión de Remitente Mailing
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RemitenteMailingController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;

        public RemitenteMailingController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;

        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] RemitenteMailing entidad)
        {
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
                        var servicio = new RemitenteMailingService(unitOfWork);
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
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<RemitenteMailing> listado)
        {

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
                        var servicio = new RemitenteMailingService(unitOfWork);
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
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: PUT
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] RemitenteMailing entidad)
        {

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
                        var servicio = new RemitenteMailingService(unitOfWork);
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
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: PUT
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<RemitenteMailing> listado)
        {

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
                        var servicio = new RemitenteMailingService(unitOfWork);
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
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserAsp").Select(s => s.Value).First();
                        var servicio = new RemitenteMailingService(unitOfWork);
                        var respuesta = servicio.Delete(id, Usuario);
                        return Ok(respuesta);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado")]
        public IActionResult EliminarListado(List<int> listadoIds)
        {
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserAsp").Select(s => s.Value).First();
                        var servicio = new RemitenteMailingService(unitOfWork);
                        var respuesta = servicio.Delete(listadoIds, Usuario);
                        return Ok(respuesta);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de remitentes mailing
        /// </summary>
        /// <returns> List<RemitenteMailingDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerRemitentesMailing()
        {
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
                        var servicio = new RemitenteMailingService(unitOfWork);
                        var Registros = servicio.ObtenerTodosRemitenteMailing();
                        return Ok(Registros);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de remitentes mailing
        /// </summary>
        /// <returns> List<RemitenteMailingDTO> </returns>
        [Route("[Action]/{IdRemitenteMailing}")]
        [HttpGet]
        public IActionResult ObtenerListaRemitenteMailingAsesor(int IdRemitenteMailing)
        {
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
                        var servicio = new RemitenteMailingService(unitOfWork);
                        var Registros = servicio.ObtenerListaRemitenteMailingAsesor(IdRemitenteMailing);
                        return Ok(Registros);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de de asesores para combo
        /// </summary>
        /// <returns> List<PersonalActivoEmailDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboAsesores()
        {
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
                        var servicio = new PersonalService(unitOfWork);
                        var Registros = servicio.ObtenerTodoPersonalActivoParaFiltro();
                        return Ok(Registros);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Creacion de registros de remitentes de mailing
        /// </summary>
        /// <returns>  </returns>
        [HttpPost("[Action]")]
        public IActionResult CrearRemitenteMailing([FromBody] RemitenteMailingCreacionDTO Remitente)
        {
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();

                        var servicio = new RemitenteMailingService(unitOfWork);
                        var servicioAsesor = new RemitenteMailingAsesorService(unitOfWork);
                        RemitenteMailing NuevaRemitenteMailing = new RemitenteMailing
                        {
                            Nombre = Remitente.formulario.Nombre,
                            Descripcion = Remitente.formulario.Descripcion,
                            Estado = true,
                            UsuarioCreacion = Usuario,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        var remitenteInsert=servicio.Add(NuevaRemitenteMailing);
                        SendingblueObtenerSenders senderAsesor = sendingblue.ObtenerSenders();
                        foreach (var asesor in Remitente.asesores)
                        {
                            int IdSenderAsesor=0;
                            foreach(var sender in senderAsesor.senders)
                            {
                                if (sender.email == asesor.CorreoElectronico)
                                {
                                    IdSenderAsesor = sender.id;
                                }
                            }
                            if(IdSenderAsesor == 0)
                            {
                                SengindblueSenders DatosAsesor = new SengindblueSenders
                                {
                                    name = asesor.NombreCompleto,
                                    email = asesor.CorreoElectronico,
                                };
                                var SenderNuevo = sendingblue.AgregarSender(DatosAsesor);
                                IdSenderAsesor = (int)(SenderNuevo.id);
                            }
                            RemitenteMailingAsesor Asesor = new RemitenteMailingAsesor
                            {
                                IdRemitenteMailing = remitenteInsert.Id,
                                IdPersonal = asesor.Id,
                                NombreCompleto = asesor.NombreCompleto,
                                CorreoElectronico = asesor.CorreoElectronico,
                                IdSenderSendinBlue = IdSenderAsesor,
                                Estado = true,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            servicioAsesor.Add(Asesor);
                        }
                        return Ok(remitenteInsert);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizacion de registros de remitentes de mailing
        /// </summary>
        /// <returns>  </returns>
        [HttpPost("[Action]")]
        public IActionResult ActualizarRemitenteMailing([FromBody] RemitenteMailingCreacionDTO Remitente)
        {
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        var servicio = new RemitenteMailingService(unitOfWork);
                        var servicioAsesor = new RemitenteMailingAsesorService(unitOfWork);
                        var remitenteExistente = unitOfWork.RemitenteMailingRepository.FirstById(Remitente.formulario.Id);
                        if (remitenteExistente == null) 
                        {
                            throw new Exception("Entidad no encontrada!");
                        }
                        else
                        {
                            RemitenteMailing ActualRemitenteMailing = new RemitenteMailing
                            {
                                Id = remitenteExistente.Id,
                                Nombre = Remitente.formulario.Nombre,
                                Descripcion = Remitente.formulario.Descripcion,
                                Estado = true,
                                UsuarioCreacion = remitenteExistente.UsuarioCreacion,
                                UsuarioModificacion = Usuario,
                                FechaCreacion = remitenteExistente.FechaCreacion,
                                FechaModificacion = DateTime.Now
                            };
                            var remitenteUpdate = servicio.Update(ActualRemitenteMailing);
                            //Eliminamos asesores asociados existentes
                            var asesoresActuales = unitOfWork.RemitenteMailingAsesorRepository.GetBy(x => x.IdRemitenteMailing == remitenteUpdate.Id).ToList();
                            foreach (var item in asesoresActuales)
                            {
                                servicioAsesor.Delete(item.Id, Usuario);
                            }
                            SendingblueObtenerSenders senderAsesor = sendingblue.ObtenerSenders();
                            foreach (var asesor in Remitente.asesores)
                            {
                                int IdSenderAsesor = 0;
                                foreach (var sender in senderAsesor.senders)
                                {
                                    if (sender.email == asesor.CorreoElectronico)
                                    {
                                        IdSenderAsesor = sender.id;
                                    }
                                }
                                if (IdSenderAsesor == 0)
                                {
                                    SengindblueSenders DatosAsesor = new SengindblueSenders
                                    {
                                        name = asesor.NombreCompleto,
                                        email = asesor.CorreoElectronico,
                                    };
                                    var SenderNuevo = sendingblue.AgregarSender(DatosAsesor);
                                    IdSenderAsesor=(int)(SenderNuevo.id);
                                }
                                RemitenteMailingAsesor Asesor = new RemitenteMailingAsesor
                                {
                                    IdRemitenteMailing = remitenteUpdate.Id,
                                    IdPersonal = asesor.Id,
                                    NombreCompleto = asesor.NombreCompleto,
                                    CorreoElectronico = asesor.CorreoElectronico,
                                    Estado = true,
                                    IdSenderSendinBlue = IdSenderAsesor,
                                    UsuarioCreacion = Usuario,
                                    UsuarioModificacion = Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                servicioAsesor.Add(Asesor);
                            }
                            return Ok(remitenteUpdate);
                        }                        
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Eliminacion de registros de remitentes de mailing
        /// </summary>
        /// <returns>  </returns>
        [Route("[action]/{Id}")]
        [HttpDelete]
        public IActionResult EliminarRemitenteMailing(int Id)
        {
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        var servicio = new RemitenteMailingService(unitOfWork);
                        var servicioAsesor = new RemitenteMailingAsesorService(unitOfWork);
                        var remitentesMailing = unitOfWork.RemitenteMailingAsesorRepository.GetBy(x => x.IdRemitenteMailing == Id).ToList();
                        foreach (var item in remitentesMailing)
                        {
                            servicioAsesor.Delete(item.Id, Usuario);
                        }
                        servicio.Delete(Id, Usuario);
                        return Ok(true);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 22/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de remitentes senders
        /// </summary>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTodosRemitente()
        {
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
                        SendingblueObtenerSenders senderAsesor = sendingblue.ObtenerSenders();
                        return Ok(senderAsesor);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
    }
}
