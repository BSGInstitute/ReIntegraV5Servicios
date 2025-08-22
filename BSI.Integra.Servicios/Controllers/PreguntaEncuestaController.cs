using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
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
        public class PreguntaEncuestaController : Controller
        {
            private IUnitOfWork unitOfWork;
            public PreguntaEncuestaController(IUnitOfWork unitOfWork)
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
            /// <param name="preguntaEncuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
            /// <returns> Entidad: PreguntaEncuesta </returns>
            [HttpPost("[Action]")]
            public IActionResult Insertar([FromBody] PreguntaEncuestaEntradaDTO preguntaEncuestaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var preguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var preguntaEncuesta = new PreguntaEncuesta();
                    preguntaEncuesta.Pregunta = preguntaEncuestaEntradaDTO.Pregunta;
                    preguntaEncuesta.IdPreguntaEncuestaCategoria = preguntaEncuestaEntradaDTO.IdPreguntaEncuestaCategoria;
                    preguntaEncuesta.IdPreguntaEncuestaTipo = preguntaEncuestaEntradaDTO.IdPreguntaEncuestaTipo;
                    preguntaEncuesta.ActivarDescripcion = preguntaEncuestaEntradaDTO.ActivarDescripcion;
                    preguntaEncuesta.Descripcion = preguntaEncuestaEntradaDTO.Descripcion;
                    preguntaEncuesta.PreguntaObligatoria = preguntaEncuestaEntradaDTO.PreguntaObligatoria;
                    preguntaEncuesta.PreguntaActiva = preguntaEncuestaEntradaDTO.PreguntaActiva;
                    preguntaEncuesta.UsuarioCreacion = preguntaEncuestaEntradaDTO.Usuario;
                    preguntaEncuesta.UsuarioModificacion = preguntaEncuestaEntradaDTO.Usuario;
                    preguntaEncuesta.FechaCreacion = DateTime.Now;
                    preguntaEncuesta.FechaModificacion = DateTime.Now;
                    preguntaEncuesta.Estado = true;
                    var resultado = preguntaEncuestaService.Add(preguntaEncuesta);
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
            /// <param name="preguntaEncuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
            /// <returns> List<PreguntaEncuesta> </returns>
            [HttpPost("[Action]")]
            public IActionResult InsertarLista([FromBody] List<PreguntaEncuestaEntradaDTO> preguntaEncuestaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var preguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var PreguntaEncuestaCategoriaLista = new List<PreguntaEncuesta>();
                    foreach (var entidad in preguntaEncuestaEntradaDTO)
                    {
                        var preguntaEncuesta = new PreguntaEncuesta();
                            preguntaEncuesta.Pregunta = entidad.Pregunta;
                            preguntaEncuesta.IdPreguntaEncuestaCategoria = entidad.IdPreguntaEncuestaCategoria;
                            preguntaEncuesta.IdPreguntaEncuestaTipo = entidad.IdPreguntaEncuestaTipo;
                            preguntaEncuesta.ActivarDescripcion = entidad.ActivarDescripcion;
                            preguntaEncuesta.Descripcion = entidad.Descripcion;
                            preguntaEncuesta.PreguntaObligatoria = entidad.PreguntaObligatoria;
                            preguntaEncuesta.PreguntaActiva = entidad.PreguntaActiva;
                            preguntaEncuesta.UsuarioCreacion = entidad.Usuario;
                            preguntaEncuesta.UsuarioModificacion = entidad.Usuario;
                            preguntaEncuesta.FechaCreacion = DateTime.Now;
                            preguntaEncuesta.FechaModificacion = DateTime.Now;
                            preguntaEncuesta.Estado = true;
                    PreguntaEncuestaCategoriaLista.Add(preguntaEncuesta);
                    }

            

                var resultado = preguntaEncuestaService.Add(PreguntaEncuestaCategoriaLista);
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
            /// <param name="preguntaEncuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
            /// <returns> Entidad: PreguntaEncuesta </returns>
            [HttpPut("[Action]")]
            public IActionResult Actualizar([FromBody] PreguntaEncuestaEntradaDTO preguntaEncuestaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var preguntaEncuesta = new PreguntaEncuesta();
                    preguntaEncuesta = PreguntaEncuestaService.ObtenerPorId(preguntaEncuestaEntradaDTO.Id.Value);
                       preguntaEncuesta.Pregunta = preguntaEncuestaEntradaDTO.Pregunta;
                       preguntaEncuesta.IdPreguntaEncuestaCategoria = preguntaEncuestaEntradaDTO.IdPreguntaEncuestaCategoria;
                       preguntaEncuesta.IdPreguntaEncuestaTipo = preguntaEncuestaEntradaDTO.IdPreguntaEncuestaTipo;
                       preguntaEncuesta.ActivarDescripcion = preguntaEncuestaEntradaDTO.ActivarDescripcion;
                       preguntaEncuesta.Descripcion = preguntaEncuestaEntradaDTO.Descripcion;
                       preguntaEncuesta.PreguntaObligatoria = preguntaEncuestaEntradaDTO.PreguntaObligatoria;
                       preguntaEncuesta.PreguntaActiva = preguntaEncuestaEntradaDTO.PreguntaActiva;
                       preguntaEncuesta.UsuarioModificacion = preguntaEncuestaEntradaDTO.Usuario;
                       preguntaEncuesta.FechaModificacion = DateTime.Now;
                    var resultado = PreguntaEncuestaService.Update(preguntaEncuesta);
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
            /// <param name="preguntaEncuestaEntradaDTO"> Datos necesarios para la insercion de datos </param>
            /// <returns> Entidad: PreguntaEncuesta </returns>
            [HttpPut("[Action]")]
            public IActionResult ActualizarLista([FromBody] List<PreguntaEncuestaEntradaDTO> preguntaEncuestaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var solicitudTipoReporteLista = new List<PreguntaEncuesta>();
                    foreach (var entidad in preguntaEncuestaEntradaDTO)
                    {
                        var preguntaEncuesta = new PreguntaEncuesta();
                        preguntaEncuesta = PreguntaEncuestaService.ObtenerPorId(entidad.Id.Value);
                        preguntaEncuesta.Pregunta = entidad.Pregunta;
                        preguntaEncuesta.IdPreguntaEncuestaCategoria = entidad.IdPreguntaEncuestaCategoria;
                        preguntaEncuesta.IdPreguntaEncuestaTipo = entidad.IdPreguntaEncuestaTipo;
                        preguntaEncuesta.ActivarDescripcion = entidad.ActivarDescripcion;
                        preguntaEncuesta.Descripcion = entidad.Descripcion;
                        preguntaEncuesta.PreguntaObligatoria = entidad.PreguntaObligatoria;
                        preguntaEncuesta.PreguntaActiva = entidad.PreguntaActiva;
                        preguntaEncuesta.UsuarioModificacion = entidad.Usuario;
                        preguntaEncuesta.FechaModificacion = DateTime.Now;
                }
                    var resultado = PreguntaEncuestaService.Update(solicitudTipoReporteLista);
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

                    var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var resultado = PreguntaEncuestaService.Delete(id, usuario);
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
                    var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var resultado = PreguntaEncuestaService.Delete(listadoIds, usuario);
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
                    var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var resultado = PreguntaEncuestaService.ObtenerCombo();
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
            public ActionResult ObtenerPreguntaEncuesta()
            {
                try
                {
                    var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                    var resultado = PreguntaEncuestaService.ObtenerPreguntaEncuesta();
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        /// Autor: Jeremy Pacheco
        /// Fecha: 07/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos las preguntas asincronicas
        /// </summary>
        /// <returns> List<PreguntaEncuestaAsincronicaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                var resultado = PreguntaEncuestaService.ObtenerPreguntaEncuestaAsincronica();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 07/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos las preguntas asincronicas asociadas a una encuesta
        /// </summary>
        /// <returns> List<BancoPreguntaEncuestaAsincronicaDTO> </returns>
        [Route("[action]/{idEncuesta}")]
        [HttpGet]
        public IActionResult ObtenerPreguntaEncuestaAsincronicaPorId(int idEncuesta)
        {
            try
            {
                var PreguntaEncuestaService = new PreguntaEncuestaService(unitOfWork);
                var resultado = PreguntaEncuestaService.ObtenerPreguntaEncuestaAsincronicaPorId(idEncuesta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
