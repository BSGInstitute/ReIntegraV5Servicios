using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FormularioSolicitudController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión de Categoria Origen
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioSolicitudController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioSolicitudController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] FormularioSolicitud entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<FormularioSolicitud> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FormularioSolicitud entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<FormularioSolicitud> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
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
        /// Fecha: 11/08/2022
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
                var servicio = new FormularioSolicitudService(unitOfWork);
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
                var servicio = new FormularioSolicitudService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FormularioSolicitud para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new FormularioSolicitudService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }

        [HttpPost("ObtenerComboFs")]
        public IActionResult ObtenerComboFs(InsertarFormulario2DTO nombre)
        {
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
            return Ok(servicio.ObtenerComboFs(nombre));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>
        //[HttpGet("[Action]")]
        //public IActionResult ObtenerFormularioSolicitud()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var servicio = new FormularioSolicitudService(unitOfWork);
        //        return Ok(servicio.ObtenerFormularioSolicitud());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}






        [HttpPost("[Action]")]
        public IActionResult ObtenerFormularioSolicitud([FromBody] FiltroCompuestroGrillaDTO paginador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
                var FormularioSolicitud = servicio.ObtenerTodo(paginador);
                var Total = FormularioSolicitud.Count() == 0 ? 0 : FormularioSolicitud.FirstOrDefault().Total;
                return Ok(new { data = FormularioSolicitud, Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("[Action]")]
        public IActionResult ObtenerFiltros()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioCampoContacto = new CampoContactoService(unitOfWork);
                var servicioCampoContactoTodo = new CampoContactoService(unitOfWork);
                var servicioCategoriaOrigen = new CategoriaOrigenService(unitOfWork);
                var servicioFormularioRespuesta = new FormularioRespuestaService(unitOfWork);
                var servicioFormularioSolicitudTextoBoton = new FormularioSolicitudTextoBotonService(unitOfWork);

                var campoContacto = servicioCampoContacto.ObtenerFiltroCampoContacto();
                var campoContactoTodo = servicioCampoContactoTodo.ObtenerFiltroCampoContactoTodo();
                var categoriaOrigen = servicioCategoriaOrigen.ObtenerFiltroCategoriaOrigen();
                var formularioRespueta = servicioFormularioRespuesta.ObtenerFiltroFormularioRespuestum();
                var textoBoton = servicioFormularioSolicitudTextoBoton.ObtenerFiltroFormularioSolicitudTextoBoton();

                return Ok(new
                {
                    textoBoton,
                    campoContacto,
                    categoriaOrigen,
                    formularioRespueta,
                    campoContactoTodo,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarFormularioSolicitud([FromBody] InsertarFormularioSolicitudCampoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);

                var respuesta = servicio.InsertarFormularioSolicitud(obj);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdFormulario}")]
        [HttpGet]
        public ActionResult ObtenerCampoFormularioPorIdFormularioSolicitud(int IdFormulario)
        {
            try
            {
                var servicio = new CampoFormularioService(unitOfWork);
                return Ok(servicio.ObtenerCampoFormularioPorIdFormularioSolicitud(IdFormulario));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConjuntoAnuncioFiltro([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                if (Filtro != null)
                {
                    var servicio = new FormularioSolicitudService(unitOfWork);
                    return Ok(servicio.ObtenerConjuntoAnunciosFiltro(Filtro["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerFormularioRespuestaFiltro([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                if (Filtro != null)
                {
                    var servicio = new FormularioSolicitudService(unitOfWork);
                    return Ok(servicio.FormularioRespuestaFiltro(Filtro["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]/{IdFormulario}/{Usuario}")]
        [HttpPost]
        public ActionResult EliminarFormularioSolicitud(int IdFormulario, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
                return Ok(servicio.EliminarFormularioSolicitud(IdFormulario, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("[Action]")]
        public IActionResult ActualizarFormularioSolicitud([FromBody] InsertarFormularioSolicitudCampoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new FormularioSolicitudService(unitOfWork);
                var respuesta = servicio.ActualizarFormularioSolicitud(obj);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}






//[HttpPost("[Action]")]
//public IActionResult InsertarConjuntoAnuncio([FromBody] CompuestoConjuntoAnuncioDTO obj)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(ModelState);
//    }
//    try

//    {
//        var ConjuntoAnuncioRepositorio = new FormularioSolicitudRepository(unitOfWork);
//        var respuesta = ConjuntoAnuncioRepositorio.InsertarConjuntoAnuncio(object);

//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}












