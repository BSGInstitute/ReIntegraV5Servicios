using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FiltroSegmentoController
    /// Autor: Edson Daniel Mayta Escobedo.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión de FiltroSegmento
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
  
    [EnableCors("CorsVista")]
    public class FiltroSegmentoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public FiltroSegmentoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] FiltroSegmento entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<FiltroSegmento> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] FiltroSegmento entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<FiltroSegmento> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 12/08/2022
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
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 12/08/2022
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
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       

        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerCombo()
        {
            try
            {
                var res = new FiltroSegmentoService(unitOfWork);
                var respuesta = res.ObtenerCombo();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



        [Route("[action]")]
        [HttpPost]

        public IActionResult ObtenerSubAreas(string idAreas)
        {
            try
            {
                var res = new FiltroSegmentoService(unitOfWork);
                var respuesta = res.ObtenerSubArea(idAreas);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpPost]

        public IActionResult ObtenerProgramaSubAreas(string idAreas, string idSubAreas)
        {
            try
            {
                var res = new FiltroSegmentoService(unitOfWork);
                var respuesta = res.ObtenerProgramaSubArea(idAreas, idSubAreas);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpPost]

        public IActionResult ObtenerProgramaEspecifico(string IdProgramaGeneral)
        {
            try
            {
                var res = new FiltroSegmentoService(unitOfWork);
                var respuesta = res.ObtenerProgramaEspecifico(IdProgramaGeneral);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerFrecuenciaParaFiltroSegmento()
        {
            try
            {
                var res = new TiempoFrecuenciaService(unitOfWork);
                var respuesta = res.ObtenerListaParaFiltroSegmento();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [Route("[action]")]
        [HttpPost]

        public IActionResult ObtenerCriterioPorModalidad(ModalidadCriterioDTO datos)
        {
            try
            {
                var res = new CriterioDocService(unitOfWork);
                var respuesta = res.ObtenerCriterioModalidad(datos.Criterio);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerActividadCabecera()
        {
            try
            {
                var res = new ActividadCabeceraService(unitOfWork);
                var respuesta = res.ObtenerFiltro();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerTarifario()
        {
            try
            {
                var res = new TarifarioService(unitOfWork);
                var respuesta = res.ObtenerTodoFiltro();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [Route("[action]")]
        [HttpPost]

        public IActionResult ObtenerCategoriaOrigenPorTipo(string TipoDato)
        {
            try
            {
                var res = new CategoriaOrigenService(unitOfWork);
                var respuesta = res.ObtenerCategoriaPorTipoCategoria(TipoDato);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerListaTipoFormulario()
        {
            try
            {
                var res = new TipoFormularioService(unitOfWork);
                var respuesta = res.ObtenerListaTipoFormulario();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerListaTipoInteraccion()
        {
            try
            {
                var res = new TipoFormularioService(unitOfWork);
                var respuesta = res.ObtenerListaTipoFormulario();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerPorTipoInteraccionGeneralFormulario()
        {
            try
            {
                var res = new TipoInteraccionService(unitOfWork);
                var respuesta = res.ObtenerPorTipoInteraccionGeneralFormulario();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// Tipo Función: HttpPost
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 20/02/2023
        /// Versión: 1.0
        /// <summary>
        /// EjecutaFiltroSegmento
        /// </summary>
        /// <param name="Id">Recibe</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [AllowAnonymous]

        [HttpPost("EjecutarFiltro/{Id}/{usuario}")]
        public IActionResult EjecutarFiltro(int Id , String usuario)
        {
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.EjecutarFiltro(Id, usuario);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: HttpGet
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 20/02/2023
        /// Versión: 1.0
        /// <summary>
        /// ObtenerDetalleFiltroSegmento
        /// </summary>
        /// <param name="Id">Recibe</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Route("ObtenerDetalleFiltroSegmento/{IdFiltroSegmento}")]
        [HttpGet]
        public ActionResult ObtenerDetalleFiltroSegmento(int IdFiltroSegmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                return Ok(servicio.ObtenerDetalleFiltroSegmento(IdFiltroSegmento));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("ObtenerHistorialAudiencia/{IdFiltroSegmento}")]
        [HttpGet]
        public ActionResult ObtenerHistorialAudiencia(int IdFiltroSegmento)
        {
            try
            {
                var servicio = new FiltroSegmentoService(unitOfWork);
                return Ok(servicio.ObtenerHistorialPorIdFiltroSegmento(IdFiltroSegmento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: HttpPost
        /// Autor: Edson Daniel Mayta Escobedo.
        /// Fecha: 20/02/2023
        /// Versión: 1.0
        /// <summary>
        /// EjecutaFiltroSegmento
        /// </summary>
        /// <param name="Id">Recibe</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpPost("ObtenerResultadosFiltro/{Id}")]
        public IActionResult ObtenerResultadosFiltro(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var claimsIdentity = User.Identity as ClaimsIdentity;
                //var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                //var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new FiltroSegmentoService(unitOfWork);
                var respuesta = servicio.ObtenerResultadosFiltro(Id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("InsertarFiltro")]
        [HttpPost]
        public ActionResult InsertarFiltro([FromBody] FiltroSegmentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new FiltroSegmentoService(unitOfWork);
                return Ok(servicio.InsertarFiltro(Json , usuario)); //filtroSegmento.Insertar(Json)
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{IdFiltroSegmento}")]
        [HttpPost]
        public ActionResult EliminarFiltro(int IdFiltroSegmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new FiltroSegmentoService(unitOfWork);
                servicio.EliminarFiltro(IdFiltroSegmento, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdFiltroSegmento}")]
        [HttpPost]
        public ActionResult Duplicar(int IdFiltroSegmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new FiltroSegmentoService(unitOfWork);
                servicio.Duplicar(IdFiltroSegmento, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFiltro([FromBody] FiltroSegmentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new FiltroSegmentoService(unitOfWork);
                servicio.ActualizarFiltro(Json, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarAudiencia([FromBody] FacebookAudienciaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new FiltroSegmentoService(unitOfWork);
                servicio.InsertarAudiencia(Json, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult CrearOportunidadPorFiltroSegmento([FromBody] OportunidadFiltroSegmentoDTO OportunidadFiltroSegmento)
        {
            var servicio = new FiltroSegmentoService(unitOfWork);

            var resultado = servicio.CrearOportunidadPorFiltroSegmento(OportunidadFiltroSegmento);

            if (resultado.Errores != null && resultado.Errores.Any())
            {
                return BadRequest(resultado);  
            }

            return Ok(resultado); 
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                var servicio = new CentroCostoService(unitOfWork);
                if (Filtros != null)
                {
               
                    if (Filtros.ContainsKey("usuario") && Filtros["usuario"].ToString() == "AdminInst")
                    {
                        return Ok(servicio.ObtenerTodoFiltroAutoCompleteInstituto(Filtros["valor"].ToString()));
                    }
                    else
                    {
                        return Ok(servicio.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                    }
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarAudiencia([FromBody] FacebookAudienciaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
             

                var servicio = new FiltroSegmentoService(unitOfWork);
                servicio.ActualizarAudiencia(Json);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
