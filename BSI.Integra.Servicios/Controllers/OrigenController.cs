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
    /// Controlador: OrigenController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Origen
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class OrigenController : Controller
    {
        private IUnitOfWork unitOfWork;
        public OrigenController(IUnitOfWork unitOfWork)
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
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] OrigenDTO entidad)
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
                var servicio = new OrigenService(unitOfWork);
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<Origen> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenService(unitOfWork);
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
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] OrigenDTO entidad)
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
                var servicio = new OrigenService(unitOfWork);
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<Origen> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenService(unitOfWork);
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
        /// Fecha: 22/08/2022
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
                var servicio = new OrigenService(unitOfWork);
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
        /// Fecha: 22/08/2022
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
                var servicio = new OrigenService(unitOfWork);
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
        /// Obtiene todos los registros guardados en T_Origen para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new OrigenService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Origen
        /// </summary>
        /// <returns> List<OrigenDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerOrigen()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenService(unitOfWork);
                return Ok(servicio.ObtenerOrigen());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Tarifarios
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTarifarios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                var lista = servicioOrigen.ObtenerTarifarios();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Tarifarios detallados
        /// </summary>
        /// <param name="idTarifario"></param>
        /// <returns></returns>
        [Route("[action]/{idTarifario}")]
        [HttpGet]
        public IActionResult ObtenerTarifariosDetalles(int idTarifario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                var lista = servicioOrigen.ObtenerTarifariosDetalles(idTarifario);
                var agrupado = lista.GroupBy(x => new { x.IdTarifario, x.Concepto, x.Descripcion, x.Estados, x.SubEstados, x.TipoCantidad }).Select(g => new ConfiguracionTarifarioDetalleDTO
                {
                    IdTarifario = g.Key.IdTarifario,
                    Concepto = g.Key.Concepto,
                    Descripcion = g.Key.Descripcion,
                    Estados = g.Key.Estados,
                    SubEstados = g.Key.SubEstados,
                    TipoCantidad = g.Key.TipoCantidad,
                    DetallePais = g.GroupBy(y => new { y.Id, y.IdPais, y.NombrePais, y.IdMoneda, y.NombrePlural, y.Monto, y.Simbolo }).Select(y => new ConfiguracionTarifarioDetallePorPaisDTO
                    {
                        Id = y.Key.Id,
                        IdPais = y.Key.IdPais,
                        NombrePais = y.Key.NombrePais,
                        IdMoneda = y.Key.IdMoneda,
                        NombrePlural = y.Key.NombrePlural,
                        Monto = y.Key.Monto,
                        Simbolo = y.Key.Simbolo
                    }).ToList(),

                }).ToList();
                return Ok(agrupado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta un tarifario
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTarifario([FromBody] TarifarioNuevoDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                TarifarioDetalleAlternoService servicioTarifarioDetalleAlterno = new TarifarioDetalleAlternoService(unitOfWork);
                var lista = servicioOrigen.InsertarTarifario(json);

                foreach (var item in json.Detalles)
                {
                    foreach (var tmp in item.ListaIdPaises)
                    {
                        TarifarioDetalleAlterno tarifarioDetalle = new TarifarioDetalleAlterno();
                        item.IdTarifario = lista.FirstOrDefault().Id;
                        tarifarioDetalle.IdTarifario = item.IdTarifario;
                        tarifarioDetalle.Concepto = item.Concepto;
                        tarifarioDetalle.IdPais = tmp.IdPais;
                        tarifarioDetalle.Monto = tmp.Monto;
                        tarifarioDetalle.IdMoneda = tmp.IdMoneda;
                        tarifarioDetalle.Descripcion = item.Descripcion;
                        tarifarioDetalle.TipoCantidad = item.TipoCantidad;
                        tarifarioDetalle.Estados = item.Estados;
                        tarifarioDetalle.SubEstados = item.SubEstados;
                        tarifarioDetalle.Estado = true;
                        tarifarioDetalle.UsuarioCreacion = json.Usuario;
                        tarifarioDetalle.UsuarioModificacion = json.Usuario;
                        tarifarioDetalle.FechaCreacion = DateTime.Now;
                        tarifarioDetalle.FechaModificacion = DateTime.Now;
                        tarifarioDetalle.VisualizarPortalWeb = true;
                        servicioTarifarioDetalleAlterno.Add(tarifarioDetalle);
                    }
                }
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Tarifario
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarTarifario([FromBody] TarifarioNuevoDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int idTarifarioTemp = json.Id;
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                TarifarioDetalleAlternoService servicioTarifarioDetalleAlterno = new TarifarioDetalleAlternoService(unitOfWork);
                var lista = servicioOrigen.ActualizarTarifario(json);

                foreach (var item in json.Detalles)
                {
                    foreach (var tmp in item.ListaIdPaises)
                    {
                        if (tmp.Id > 0)
                        {
                            var tarifarioDetalle = servicioTarifarioDetalleAlterno.ObtenerPorId(tmp.Id);
                            tarifarioDetalle.IdTarifario = item.IdTarifario;
                            tarifarioDetalle.Concepto = item.Concepto;
                            tarifarioDetalle.IdPais = tmp.IdPais;
                            tarifarioDetalle.Monto = tmp.Monto;
                            tarifarioDetalle.IdMoneda = tmp.IdMoneda;
                            tarifarioDetalle.Descripcion = item.Descripcion;
                            tarifarioDetalle.TipoCantidad = item.TipoCantidad;
                            tarifarioDetalle.Estados = item.Estados;
                            tarifarioDetalle.SubEstados = item.SubEstados;
                            tarifarioDetalle.UsuarioModificacion = json.Usuario;
                            tarifarioDetalle.FechaModificacion = DateTime.Now;
                            servicioTarifarioDetalleAlterno.Update(tarifarioDetalle);
                        }
                        else
                        {
                            TarifarioDetalleAlterno tarifarioDetalle = new TarifarioDetalleAlterno();
                            tarifarioDetalle.IdTarifario = idTarifarioTemp;
                            tarifarioDetalle.Concepto = item.Concepto;
                            tarifarioDetalle.IdPais = tmp.IdPais;
                            tarifarioDetalle.Monto = tmp.Monto;
                            tarifarioDetalle.IdMoneda = tmp.IdMoneda;
                            tarifarioDetalle.Descripcion = item.Descripcion;
                            tarifarioDetalle.TipoCantidad = item.TipoCantidad;
                            tarifarioDetalle.Estados = item.Estados;
                            tarifarioDetalle.SubEstados = item.SubEstados;
                            tarifarioDetalle.Estado = true;
                            tarifarioDetalle.UsuarioCreacion = json.Usuario;
                            tarifarioDetalle.UsuarioModificacion = json.Usuario;
                            tarifarioDetalle.FechaCreacion = DateTime.Now;
                            tarifarioDetalle.FechaModificacion = DateTime.Now;
                            tarifarioDetalle.VisualizarPortalWeb = true;
                            servicioTarifarioDetalleAlterno.Add(tarifarioDetalle);
                        }
                    }
                }
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina Tarifario
        /// </summary>
        /// <param name="idTarifario"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Route("[action]/{idTarifario}/{usuario}")]
        [HttpPost]
        public IActionResult EliminarTarifario(int idTarifario, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                var lista = servicioOrigen.EliminarTarifario(idTarifario, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Tarifario Detalle País
        /// </summary>
        /// <param name="id">uparam>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpPost]
        public IActionResult EliminarTarifarioDetallePais(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                var lista = servicioOrigen.EliminarTarifarioDetallePais(id, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Tarifario Detalle
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Route("[action]/{Concepto}/{Usuario}")]
        [HttpPost]
        public IActionResult EliminarTarifarioDetalle(string concepto, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenService servicioOrigen = new OrigenService(unitOfWork);
                var lista = servicioOrigen.EliminarTarifarioDetalle(concepto, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
