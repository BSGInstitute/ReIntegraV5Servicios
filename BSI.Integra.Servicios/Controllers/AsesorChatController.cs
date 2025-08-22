using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AsesorChatController
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 17/22/2022
    /// <summary>
    /// Gestión de Asesor Chat
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AsesorChatController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public AsesorChatController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] AsesorChat entidad)
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
                        var servicio = new AsesorChatMktService(unitOfWork);
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
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<AsesorChat> listado)
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
                        var servicio = new AsesorChatMktService(unitOfWork);
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
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] AsesorChat entidad)
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
                        var servicio = new AsesorChatMktService(unitOfWork);
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
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<AsesorChat> listado)
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
                        var servicio = new AsesorChatMktService(unitOfWork);
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
        /// Fecha: 17/11/2022
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        var servicio = new AsesorChatMktService(unitOfWork);
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
        /// Fecha: 17/11/2022
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
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        var servicio = new AsesorChatMktService(unitOfWork);
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
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los filtros necesario para AsesorChat
        /// </summary>
        /// <returns> AsesorChatFiltroDTO </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerFiltroCombos()
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
                        var personal = new PersonalService(unitOfWork);
                        var areaCapacitacion = new AreaCapacitacionService(unitOfWork);
                        var subAreaCapacitacion = new SubAreaCapacitacionService(unitOfWork);
                        var programasServicio = new PGeneralService(unitOfWork);
                        var paisServicio = new PaisService(unitOfWork);
                        AsesorChatFiltroDTO asesorChatFiltro = new AsesorChatFiltroDTO();
                        asesorChatFiltro.listaAsesores = personal.ObtenerTodoAsesorCoordinadorVentas();
                        asesorChatFiltro.listaAreas = areaCapacitacion.ObtenerCombo();
                        asesorChatFiltro.listaSubAreas = subAreaCapacitacion.ObtenerCombo();
                        asesorChatFiltro.listaProgramas = programasServicio.ObtenerTodoFiltro();
                        asesorChatFiltro.listaPaises = paisServicio.ObtenerPaisCombo();
                        return Ok(asesorChatFiltro);
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
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros para ChatAsignadosNoAsignados
        /// </summary>
        /// <returns> AsesorChatFiltroDTO </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerChatAsignadosNoAsignados([FromBody] FiltroCompuestroGrillaDTO paginador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsesorChatMktService(unitOfWork);
                var AsesorChat = servicio.ObtenerChatAsignadosNoAsignados(paginador);
                var Total = AsesorChat.Total == 0 ? 0 : AsesorChat.Total;
                return Ok(new { data = AsesorChat.Registros, Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[Action]")]
        public IActionResult ObtenerTodoChatAsignados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsesorChatService(unitOfWork);
                var AsesorChat = servicio.ObtenerTodoChatAsignados();
                return Ok(new { AsesorChat });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros para ChatListaAsesores
        /// </summary>
        /// <returns> AsesorChatFiltroDTO </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerChatListaAsesores([FromBody] FiltroCompuestroGrillaDTO paginador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsesorChatMktService(unitOfWork);
                var ListaAsesor = servicio.ObtenerChatListaAsesores(paginador);
                var Total = ListaAsesor.Total == 0 ? 0 : ListaAsesor.Total;
                return Ok(new { data = ListaAsesor.Registros, Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de Chat Detalle por asesor
        /// </summary>
        /// <returns> AsesorChatFiltroDTO </returns>
        [HttpGet("[Action]/{IdAsesorChat}")]
        public IActionResult ObtenerDetalleChatAsesor(int IdAsesorChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsesorChatDetalleService(unitOfWork);
                AsesorChatDetalleDetalleDTO asesorChatDetalleDetalle = new AsesorChatDetalleDetalleDTO()
                {
                    listadoIdsPais = servicio.ObtenerPaisesPorIdAsesorChat(IdAsesorChat),
                    listadoIdsProgramaGeneral = servicio.ObtenerProgramasGeneralesPorIdAsesorChat(IdAsesorChat),
                    listadoIdsAreaCapacitacion = servicio.ObtenerAreasCapacitacionPorIdAsesorChat(IdAsesorChat),
                    listadoIdsSubAreaCapacitacion = servicio.ObtenerSubAreasCapacitacionPorIdAsesorChat(IdAsesorChat)
                };
                return Ok(asesorChatDetalleDetalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los detalles del AsesorChat (grillas)
        /// </summary>
        /// <param name="DTO">Objeto de clase CompuestoInsertarAsesorChatDTO</param>
        /// <returns>Response 200 con el objeto de clase AsesorChatDetalleDetalleDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDetalles([FromBody] CompuestoInsertarAsesorChatDTO DTO)
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
                        var servicio = new AsesorChatMktService(unitOfWork);

                        var servicioPGeneral = new PGeneralService(unitOfWork);
                        var servicioPais = new PaisService(unitOfWork);
                        var servicioAsesorChatDetalle = new AsesorChatDetalleService(unitOfWork);
                        var servicioAsesorChat = new AsesorChatMktService(unitOfWork);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            AsesorChat asesorChat = new AsesorChat()
                            {
                                IdPersonal = DTO.IdPersonal,
                                NombreAsesor = DTO.NombreAsesor,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario
                            };

                            if (DTO.Programas == null || DTO.Programas.Count == 0)
                            {
                                if (DTO.SubArea == null || DTO.SubArea.Count == 0)
                                {
                                    if (DTO.Area == null || DTO.Area.Count == 0)
                                    {
                                        //si no selecciono ninguna area /subarea/programa
                                        DTO.Programas = unitOfWork.PGeneralRepository.GetBy(x => x.Estado, x => new IdDTO { Id = x.Id }).Select(x => x.Id).ToList();
                                    }
                                    else//selecciono una area
                                    {
                                        DTO.Programas = servicioPGeneral.ObtenerTodosPorIdArea(DTO.Programas).Select(x => x.Id).ToList();
                                    }
                                }
                                else
                                {
                                    DTO.Programas = servicioPGeneral.ObtenerTodosPorIdSubArea(DTO.SubArea).Select(x => x.Id).ToList();
                                }
                            }
                            if (DTO.Paises == null || DTO.Paises.Count == 0)
                            {
                                DTO.Paises = servicioPais.ObtenerTodoCodigoPais();
                            }
                            //Insertamos el asesor chat
                            var IdAsesorNuevo = servicioAsesorChat.Add(asesorChat).Id;
                            //Insertamos los detalles
                            servicioAsesorChatDetalle.ActualizarAsesorChaDetalleYLog(IdAsesorNuevo, asesorChat.IdPersonal.Value, Usuario, String.Join(",", DTO.Programas), String.Join(",", DTO.Paises));
                            scope.Complete();
                            return Ok(asesorChat);
                        }
                        return Ok(null);
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

        /// Autor: Max Mantilla Rodríguez
        /// Fecha: 17/11/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta los detalles del AsesorChat (grillas)
        /// </summary>
        /// <param name="DTO">Objeto de clase CompuestoInsertarAsesorChatDTO</param>
        /// <returns>Response 200 con el objeto de clase AsesorChatDetalleDetalleDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDetalles([FromBody] CompuestoInsertarAsesorChatDTO DTO)
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
                        var servicio = new AsesorChatMktService(unitOfWork);

                        var servicioPGeneral = new PGeneralService(unitOfWork);
                        var servicioPais = new PaisService(unitOfWork);
                        var servicioAsesorChatDetalle = new AsesorChatDetalleService(unitOfWork);
                        var servicioAsesorChat = new AsesorChatMktService(unitOfWork);

                        if (unitOfWork.AsesorChatRepository.Exist(DTO.Id))
                        {
                            var asesorChat = unitOfWork.AsesorChatRepository.FirstById(DTO.Id);
                            AsesorChat asesorChatActual = new AsesorChat()
                            {
                                Id = asesorChat.Id,
                                IdPersonal = DTO.IdPersonal,
                                NombreAsesor = DTO.NombreAsesor,
                                Estado = true,
                                FechaCreacion = asesorChat.FechaCreacion,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = asesorChat.UsuarioCreacion,
                                UsuarioModificacion = Usuario
                            };
                            if (DTO.Programas == null || DTO.Programas.Count == 0)
                            {
                                if (DTO.SubArea == null || DTO.SubArea.Count == 0)
                                {
                                    if (DTO.Area == null || DTO.Area.Count == 0)
                                    {
                                        //si no selecciono ninguna area /subarea/programa
                                        DTO.Programas = unitOfWork.PGeneralRepository.GetBy(x => x.Estado, x => new IdDTO { Id = x.Id }).Select(x => x.Id).ToList();
                                    }
                                    else//selecciono una area
                                    {
                                        DTO.Programas = servicioPGeneral.ObtenerTodosPorIdArea(DTO.Area).Select(x => x.Id).ToList();
                                    }
                                }
                                else
                                {
                                    DTO.Programas = servicioPGeneral.ObtenerTodosPorIdSubArea(DTO.SubArea).Select(x => x.Id).ToList();
                                }
                            }
                            if (DTO.Paises == null || DTO.Paises.Count == 0)
                            {
                                DTO.Paises = servicioPais.ObtenerTodoCodigoPais();
                            }

                            servicioAsesorChatDetalle.EliminarAsesorChatDetalle(asesorChatActual.Id, Usuario);
                            //Actualizamos el asesor chat
                            servicioAsesorChat.Update(asesorChatActual);
                            //Insertamos los detalles
                            servicioAsesorChatDetalle.ActualizarAsesorChaDetalleYLog(asesorChatActual.Id, asesorChatActual.IdPersonal.Value, Usuario, String.Join(",", DTO.Programas), String.Join(",", DTO.Paises));
                            return Ok(asesorChatActual);
                        }
                        else
                        {
                            return BadRequest("No existe el asesor chat");
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
        /// Tipo Función: DELETE
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica de registro de la tabla AsesorChat y AsesorChatDetalle
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarDetalles(int Id)
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
                        var servicioAsesorChat = new AsesorChatMktService(unitOfWork);
                        var servicioAsesorChatDetalle = new AsesorChatDetalleService(unitOfWork);
                        if (unitOfWork.AsesorChatRepository.Exist(Id))
                        {
                            servicioAsesorChatDetalle.EliminarAsesorChatDetalle(Id, Usuario);
                            servicioAsesorChat.Delete(Id, Usuario);
                            return Ok(true);
                        }
                        else
                        {
                            return BadRequest("No existe el asesor chat!");
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


        [HttpGet("[Action]")]
        public IActionResult ObtenerChatListaAsesores2()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsesorChatMktService(unitOfWork);
                var AsesorChat = servicio.ObtenerChatListaAsesores2();
                return Ok(new { AsesorChat });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
