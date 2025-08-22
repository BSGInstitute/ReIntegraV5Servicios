using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudTipoReporteController
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PreguntaEncuestaRespuestaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PreguntaEncuestaRespuestaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="preguntaEncuestaRespuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PreguntaEncuestaRespuesta </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] PreguntaEncuestaRespuestaEntradaDTO preguntaEncuestaRespuestaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var preguntaEncuestaCategoria = new PreguntaEncuestaRespuesta();
                preguntaEncuestaCategoria.IdPreguntaEncuesta = preguntaEncuestaRespuestaEntradaDTO.IdPreguntaEncuesta;
                preguntaEncuestaCategoria.Respuesta = preguntaEncuestaRespuestaEntradaDTO.Respuesta;
                preguntaEncuestaCategoria.Orden = preguntaEncuestaRespuestaEntradaDTO.Orden;
                preguntaEncuestaCategoria.Puntaje = preguntaEncuestaRespuestaEntradaDTO.Puntaje;
                preguntaEncuestaCategoria.UsuarioCreacion = preguntaEncuestaRespuestaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.UsuarioModificacion = preguntaEncuestaRespuestaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.FechaCreacion = DateTime.Now;
                preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                preguntaEncuestaCategoria.Estado = true;
                var resultado = preguntaEncuestaCategoriaService.Add(preguntaEncuestaCategoria);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="preguntaEncuestaRespuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<PreguntaEncuestaRespuesta> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<PreguntaEncuestaRespuestaEntradaDTO> preguntaEncuestaRespuestaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var PreguntaEncuestaCategoriaLista = new List<PreguntaEncuestaRespuesta>();
                foreach (var entidad in preguntaEncuestaRespuestaEntradaDTO)
                {
                    var preguntaEncuestaCategoria = new PreguntaEncuestaRespuesta();
                    preguntaEncuestaCategoria.IdPreguntaEncuesta = entidad.IdPreguntaEncuesta;
                    preguntaEncuestaCategoria.Respuesta = entidad.Respuesta;
                    preguntaEncuestaCategoria.Orden = entidad.Orden;
                    preguntaEncuestaCategoria.Puntaje = entidad.Puntaje;
                    preguntaEncuestaCategoria.UsuarioCreacion = entidad.Usuario;
                    preguntaEncuestaCategoria.UsuarioModificacion = entidad.Usuario;
                    preguntaEncuestaCategoria.FechaCreacion = DateTime.Now;
                    preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                    preguntaEncuestaCategoria.Estado = true;
                    PreguntaEncuestaCategoriaLista.Add(preguntaEncuestaCategoria);
                }
                var resultado = preguntaEncuestaCategoriaService.Add(PreguntaEncuestaCategoriaLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="preguntaEncuestaRespuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PreguntaEncuestaRespuesta </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] PreguntaEncuestaRespuestaEntradaDTO preguntaEncuestaRespuestaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var preguntaEncuestaCategoria = new PreguntaEncuestaRespuesta();
                preguntaEncuestaCategoria = PreguntaEncuestaRespuestaService.ObtenerPorId(preguntaEncuestaRespuestaEntradaDTO.Id.Value);
                preguntaEncuestaCategoria.IdPreguntaEncuesta = preguntaEncuestaRespuestaEntradaDTO.IdPreguntaEncuesta;
                preguntaEncuestaCategoria.Respuesta = preguntaEncuestaRespuestaEntradaDTO.Respuesta;
                preguntaEncuestaCategoria.Orden = preguntaEncuestaRespuestaEntradaDTO.Orden;
                preguntaEncuestaCategoria.Puntaje = preguntaEncuestaRespuestaEntradaDTO.Puntaje;
                preguntaEncuestaCategoria.UsuarioModificacion = preguntaEncuestaRespuestaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                var resultado = PreguntaEncuestaRespuestaService.Update(preguntaEncuestaCategoria);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="preguntaEncuestaRespuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PreguntaEncuestaRespuesta </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<PreguntaEncuestaRespuestaEntradaDTO> preguntaEncuestaRespuestaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var solicitudTipoReporteLista = new List<PreguntaEncuestaRespuesta>();
                foreach (var entidad in preguntaEncuestaRespuestaEntradaDTO)
                {
                    var preguntaEncuestaRespuesta = new PreguntaEncuestaRespuesta();
                    preguntaEncuestaRespuesta = PreguntaEncuestaRespuestaService.ObtenerPorId(entidad.Id.Value);
                    preguntaEncuestaRespuesta.IdPreguntaEncuesta = entidad.IdPreguntaEncuesta;
                    preguntaEncuestaRespuesta.Respuesta = entidad.Respuesta;
                    preguntaEncuestaRespuesta.Orden = entidad.Orden;
                    preguntaEncuestaRespuesta.Puntaje = entidad.Puntaje;
                    preguntaEncuestaRespuesta.UsuarioModificacion = entidad.Usuario;
                    preguntaEncuestaRespuesta.FechaModificacion = DateTime.Now;
                }
                var resultado = PreguntaEncuestaRespuestaService.Update(solicitudTipoReporteLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true or false </returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var resultado = PreguntaEncuestaRespuestaService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="listadoIds"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true or false </returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var resultado = PreguntaEncuestaRespuestaService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var resultado = PreguntaEncuestaRespuestaService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCategoriaEncuesta()
        {
            try
            {
                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var resultado = PreguntaEncuestaRespuestaService.ObtenerCategoriaEncuesta();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 12/08/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos las respuestas asociadas a preguntas
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idPregunta}")]
        [HttpGet]
        public ActionResult ObtenerRespuestaPregunta(int idPregunta)
        {
            try
            {
                var PreguntaEncuestaRespuestaService = new PreguntaEncuestaRespuestaService(unitOfWork);
                var resultado = PreguntaEncuestaRespuestaService.ObtenerRespuestaPregunta(idPregunta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
