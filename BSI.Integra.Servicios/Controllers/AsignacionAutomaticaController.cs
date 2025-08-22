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

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AsignacionAutomaticaController
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 02/22/2022
    /// <summary>
    /// Gestión de Asignación Automática
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AsignacionAutomaticaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public AsignacionAutomaticaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] AsignacionAutomatica entidad)
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<AsignacionAutomatica> listado)
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] AsignacionAutomatica entidad)
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<AsignacionAutomatica> listado)
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
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
        /// Fecha: 02/11/2022
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
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
        /// Fecha: 02/11/2022
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los filtros necesario para Asignación Automática
        /// </summary>
        /// <returns> AsignacionAutomaticaFiltroDTO </returns>
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
                        var centroCostoServicio = new CentroCostoService(unitOfWork);
                        var categoriaDatoServicio = new CategoriaOrigenService(unitOfWork);
                        var paisServicio = new PaisService(unitOfWork);
                        var ciudadServicio = new CiudadService(unitOfWork);
                        var probabilidadServicio = new ProbabilidadRegistroPwService(unitOfWork);
                        var industriaServicio = new IndustriaService(unitOfWork);
                        var cargoServicio = new CargoService(unitOfWork);
                        var areaTrabajoServicio = new AreaTrabajoService(unitOfWork);
                        var areaFormacionServicio = new AreaFormacionService(unitOfWork);
                        AsignacionAutomaticaFiltroDTO asignacionAutomaticaFiltro = new AsignacionAutomaticaFiltroDTO();
                        asignacionAutomaticaFiltro.listaCentroCosto = centroCostoServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaCategoriaDato = categoriaDatoServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaPais = paisServicio.ObtenerPaisCombo();
                        asignacionAutomaticaFiltro.listaCiudad = ciudadServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaProbabilidad = probabilidadServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaIndustria = industriaServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaCargo = cargoServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaAreaTrabajo = areaTrabajoServicio.ObtenerCombo();
                        asignacionAutomaticaFiltro.listaAreaFormacion = areaFormacionServicio.ObtenerCombo();
                        return Ok(asignacionAutomaticaFiltro);
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros importados según filtros para Asignación Automática
        /// </summary>
        /// <returns> AsignacionAutomaticaCompuestoImportadosDTO </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerRegistrosImportados([FromBody] FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador)
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
                        var RegistrosImportados = servicio.ObtenerRegistrosImportados(paginador);
                        var Total = RegistrosImportados.Count() == 0 ? 0 : RegistrosImportados.FirstOrDefault().TotalRegistros;
                        return Ok(new { data = RegistrosImportados, Total });
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros erroneos según filtros para Asignación Automática
        /// </summary>
        /// <returns> AsignacionAutomaticaCompuestoErroneosDTO </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerRegistrosErroneos([FromBody] FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador)
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
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
                        var servicioError = new AsignacionAutomaticaErrorService(unitOfWork);
                        var RegistrosErroneos = servicio.ObtenerRegistrosErroneos(paginador);
                        var Total = RegistrosErroneos.Count() == 0 ? 0 : RegistrosErroneos.FirstOrDefault().TotalRegistros;
                        foreach (var erroneo in RegistrosErroneos)
                        {
                            erroneo.Errores = servicioError.ObtenerError(erroneo.Id);
                        }
                        return Ok(new { data = RegistrosErroneos, Total });
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
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Élimina un registro erroneo de Asignación Automática
        /// </summary>
        /// <returns> bool </returns>
        [Route("[action]/{Id}")]
        [HttpDelete]
        public IActionResult EliminarAsignacionAutomaticaErroneo(int Id)
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
                        AsignacionAutomaticaService _repAsignacionAutomatica = new AsignacionAutomaticaService(unitOfWork);
                        AsignacionAutomaticaErrorService _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorService(unitOfWork);

                        if (_repAsignacionAutomatica.ExisteAsignacionAutomatica(Id))
                        {
                            var lista = _repAsignacionAutomaticaError.ObtenerErrorAsignacionAutomatica(Id);

                            foreach (var item in lista)
                            {
                                _repAsignacionAutomaticaError.Delete(item.Id, Usuario);
                            }
                            _repAsignacionAutomatica.Delete(Id, Usuario);
                        }

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
    }
}
