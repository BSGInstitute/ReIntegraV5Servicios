using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
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
    public class RespuestaPreguntaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public RespuestaPreguntaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="respuestaPreguntaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: RespuestaPregunta </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] RespuestaPreguntaEntradaDTO respuestaPreguntaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuestaPreguntaService = new RespuestaPreguntaService(unitOfWork);
                var preguntaEncuestaCategoria = new RespuestaPregunta();
                preguntaEncuestaCategoria.IdPregunta = respuestaPreguntaEntradaDTO.IdPregunta;
                preguntaEncuestaCategoria.RespuestaCorrecta = false;
                preguntaEncuestaCategoria.EnunciadoRespuesta = respuestaPreguntaEntradaDTO.EnunciadoRespuesta;
                preguntaEncuestaCategoria.NroOrden = respuestaPreguntaEntradaDTO.NroOrden;
                preguntaEncuestaCategoria.Puntaje = respuestaPreguntaEntradaDTO.Puntaje;
                preguntaEncuestaCategoria.UsuarioCreacion = respuestaPreguntaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.UsuarioModificacion = respuestaPreguntaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.FechaCreacion = DateTime.Now;
                preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                preguntaEncuestaCategoria.Estado = true;
                preguntaEncuestaCategoria.NroOrdenRespuesta = 0;
                preguntaEncuestaCategoria.FeedbackPositivo = "";
                preguntaEncuestaCategoria.FeedbackNegativo = "";
                preguntaEncuestaCategoria.MostrarFeedBack = false;
                preguntaEncuestaCategoria.PuntajeTipoRespuesta = 0;
                var resultado = respuestaPreguntaService.Add(preguntaEncuestaCategoria);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="respuestaPreguntaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<RespuestaPregunta> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<RespuestaPreguntaEntradaDTO> respuestaPreguntaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new RespuestaPreguntaService(unitOfWork);
                var PreguntaEncuestaCategoriaLista = new List<RespuestaPregunta>();
                foreach (var entidad in respuestaPreguntaEntradaDTO)
                {
                    var preguntaEncuestaCategoria = new RespuestaPregunta();
                    preguntaEncuestaCategoria.IdPregunta = entidad.IdPregunta;
                    preguntaEncuestaCategoria.RespuestaCorrecta = false;
                    preguntaEncuestaCategoria.EnunciadoRespuesta = entidad.EnunciadoRespuesta;
                    preguntaEncuestaCategoria.NroOrden = entidad.NroOrden;
                    preguntaEncuestaCategoria.Puntaje = entidad.Puntaje;
                    preguntaEncuestaCategoria.UsuarioCreacion = entidad.Usuario;
                    preguntaEncuestaCategoria.UsuarioModificacion = entidad.Usuario;
                    preguntaEncuestaCategoria.FechaCreacion = DateTime.Now;
                    preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                    preguntaEncuestaCategoria.Estado = true;
                    preguntaEncuestaCategoria.NroOrdenRespuesta = 0;
                    preguntaEncuestaCategoria.FeedbackPositivo = "";
                    preguntaEncuestaCategoria.FeedbackNegativo = "";
                    preguntaEncuestaCategoria.MostrarFeedBack = false;
                    preguntaEncuestaCategoria.PuntajeTipoRespuesta = 0;
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
        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="respuestaPreguntaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: RespuestaPregunta </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] RespuestaPreguntaEntradaDTO respuestaPreguntaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var RespuestaPreguntaService = new RespuestaPreguntaService(unitOfWork);
                var preguntaEncuestaCategoria = new RespuestaPregunta();
                preguntaEncuestaCategoria = RespuestaPreguntaService.ObtenerPorId(respuestaPreguntaEntradaDTO.Id.Value);
                preguntaEncuestaCategoria.IdPregunta = respuestaPreguntaEntradaDTO.IdPregunta;
                preguntaEncuestaCategoria.RespuestaCorrecta = false;
                preguntaEncuestaCategoria.EnunciadoRespuesta = respuestaPreguntaEntradaDTO.EnunciadoRespuesta;
                preguntaEncuestaCategoria.NroOrden = respuestaPreguntaEntradaDTO.NroOrden;
                preguntaEncuestaCategoria.Puntaje = respuestaPreguntaEntradaDTO.Puntaje;
                preguntaEncuestaCategoria.UsuarioModificacion = respuestaPreguntaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                preguntaEncuestaCategoria.NroOrdenRespuesta = 0;
                preguntaEncuestaCategoria.FeedbackPositivo = "";
                preguntaEncuestaCategoria.FeedbackNegativo = "";
                preguntaEncuestaCategoria.MostrarFeedBack = false;
                preguntaEncuestaCategoria.PuntajeTipoRespuesta = 0;
                var resultado = RespuestaPreguntaService.Update(preguntaEncuestaCategoria);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="respuestaPreguntaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: RespuestaPregunta </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<RespuestaPreguntaEntradaDTO> respuestaPreguntaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var RespuestaPreguntaService = new RespuestaPreguntaService(unitOfWork);
                var solicitudTipoReporteLista = new List<RespuestaPregunta>();
                foreach (var entidad in respuestaPreguntaEntradaDTO)
                {
                    var respuestaPregunta = new RespuestaPregunta();
                    respuestaPregunta = RespuestaPreguntaService.ObtenerPorId(entidad.Id.Value);
                    respuestaPregunta.IdPregunta = entidad.IdPregunta;
                    respuestaPregunta.RespuestaCorrecta = false;
                    respuestaPregunta.EnunciadoRespuesta = entidad.EnunciadoRespuesta;
                    respuestaPregunta.NroOrden = entidad.NroOrden;
                    respuestaPregunta.Puntaje = entidad.Puntaje;
                    respuestaPregunta.UsuarioModificacion = entidad.Usuario;
                    respuestaPregunta.FechaModificacion = DateTime.Now;
                    respuestaPregunta.NroOrdenRespuesta = 0;
                    respuestaPregunta.FeedbackPositivo = "";
                    respuestaPregunta.FeedbackNegativo = "";
                    respuestaPregunta.MostrarFeedBack = false;
                    respuestaPregunta.PuntajeTipoRespuesta = 0;
                }
                var resultado = RespuestaPreguntaService.Update(solicitudTipoReporteLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
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

                var RespuestaPreguntaService = new RespuestaPreguntaService(unitOfWork);
                var resultado = RespuestaPreguntaService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
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
                var RespuestaPreguntaService = new RespuestaPreguntaService(unitOfWork);
                var resultado = RespuestaPreguntaService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
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
                var RespuestaPreguntaService = new RespuestaPreguntaService(unitOfWork);
                var resultado = RespuestaPreguntaService.ObtenerRespuestaPregunta(idPregunta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
