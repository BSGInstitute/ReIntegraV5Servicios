using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
  
    /// Controlador: SolicitudTipoReporteController
    /// Autor: Jorge Gamero
    /// Fecha: 05/05/2025
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PreguntumController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PreguntumController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 05/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="preguntumEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: Preguntum </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] PreguntumEntradaDTO preguntumEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntumService = new PreguntumService(unitOfWork);
                var preguntum = new Preguntum();
                preguntum.IdTipoRespuesta = preguntumEntradaDTO.IdTipoRespuesta;
                preguntum.IdPreguntaEscalaValor = preguntumEntradaDTO.IdPreguntaEscalaValor;
                preguntum.EnunciadoPregunta = preguntumEntradaDTO.EnunciadoPregunta;
                preguntum.ConparacionValor = preguntumEntradaDTO.ConparacionValor;
                preguntum.RequiereTiempo = preguntumEntradaDTO.RequiereTiempo;
                preguntum.MinutosPorPregunta = preguntumEntradaDTO.MinutosPorPregunta;
                preguntum.RespuestaAleatoria = preguntumEntradaDTO.RespuestaAleatoria;
                preguntum.ActivarFeedBackRespuestaCorrecta = preguntumEntradaDTO.ActivarFeedBackRespuestaCorrecta;
                preguntum.ActivarFeedBackRespuestaIncorrecta = preguntumEntradaDTO.ActivarFeedBackRespuestaIncorrecta;
                preguntum.MostrarFeedbackInmediato = preguntumEntradaDTO.MostrarFeedbackInmediato;
                preguntum.MostrarFeedbackPorPregunta = preguntumEntradaDTO.MostrarFeedbackPorPregunta;
                preguntum.IdPreguntaIntento = 130378;
                preguntum.IdPreguntaTipo = preguntumEntradaDTO.IdPreguntaTipo;
                preguntum.IdTipoRespuestaCalificacion = 1;
                preguntum.FactorRespuesta = preguntumEntradaDTO.FactorRespuesta;
                preguntum.IdPreguntaCategoria = 3;
                preguntum.UsuarioCreacion = preguntumEntradaDTO.Usuario;
                preguntum.UsuarioModificacion = preguntumEntradaDTO.Usuario;
                preguntum.FechaCreacion = DateTime.Now;
                preguntum.FechaModificacion = DateTime.Now;
                preguntum.Estado = true;
                preguntum.ActivarDescripcion = preguntumEntradaDTO.ActivarDescripcion;
                preguntum.Descripcion = preguntumEntradaDTO.Descripcion;
                preguntum.PreguntaObligatoria = preguntumEntradaDTO.PreguntaObligatoria;
                preguntum.PreguntaActiva = preguntumEntradaDTO.PreguntaActiva;
                var resultado = preguntumService.Add(preguntum);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 05/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="preguntumEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<Preguntum> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<PreguntumEntradaDTO> preguntumEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntumService = new PreguntumService(unitOfWork);
                var PreguntumCategoriaLista = new List<Preguntum>();
                foreach (var entidad in preguntumEntradaDTO)
                {
                    var preguntum = new Preguntum();
                    preguntum.IdTipoRespuesta = entidad.IdTipoRespuesta;
                    preguntum.IdPreguntaEscalaValor = entidad.IdPreguntaEscalaValor;
                    preguntum.EnunciadoPregunta = entidad.EnunciadoPregunta;
                    preguntum.ConparacionValor = entidad.ConparacionValor;
                    preguntum.RequiereTiempo = entidad.RequiereTiempo;
                    preguntum.MinutosPorPregunta = entidad.MinutosPorPregunta;
                    preguntum.RespuestaAleatoria = entidad.RespuestaAleatoria;
                    preguntum.ActivarFeedBackRespuestaCorrecta = entidad.ActivarFeedBackRespuestaCorrecta;
                    preguntum.ActivarFeedBackRespuestaIncorrecta = entidad.ActivarFeedBackRespuestaIncorrecta;
                    preguntum.MostrarFeedbackInmediato = entidad.MostrarFeedbackInmediato;
                    preguntum.MostrarFeedbackPorPregunta = entidad.MostrarFeedbackPorPregunta;
                    preguntum.IdPreguntaIntento = 130378;
                    preguntum.IdPreguntaTipo = entidad.IdPreguntaTipo;
                    preguntum.IdTipoRespuestaCalificacion = 1;
                    preguntum.FactorRespuesta = entidad.FactorRespuesta;
                    preguntum.IdPreguntaCategoria = 3;
                    preguntum.UsuarioCreacion = entidad.Usuario;
                    preguntum.UsuarioModificacion = entidad.Usuario;
                    preguntum.FechaCreacion = DateTime.Now;
                    preguntum.FechaModificacion = DateTime.Now;
                    preguntum.Estado = true;
                    preguntum.ActivarDescripcion = entidad.ActivarDescripcion;
                    preguntum.Descripcion = entidad.Descripcion;
                    preguntum.PreguntaObligatoria = entidad.PreguntaObligatoria;
                    preguntum.PreguntaActiva = entidad.PreguntaActiva;
                    PreguntumCategoriaLista.Add(preguntum);
                }

                var resultado = preguntumService.Add(PreguntumCategoriaLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="preguntumEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: Preguntum </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] PreguntumEntradaDTO preguntumEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntumService = new PreguntumService(unitOfWork);
                var preguntum = new Preguntum();
                preguntum = PreguntumService.ObtenerPorId(preguntumEntradaDTO.Id.Value);
                preguntum.IdTipoRespuesta = preguntumEntradaDTO.IdTipoRespuesta;
                preguntum.IdPreguntaEscalaValor = preguntumEntradaDTO.IdPreguntaEscalaValor;
                preguntum.EnunciadoPregunta = preguntumEntradaDTO.EnunciadoPregunta;
                preguntum.ConparacionValor = preguntumEntradaDTO.ConparacionValor;
                preguntum.RequiereTiempo = preguntumEntradaDTO.RequiereTiempo;
                preguntum.MinutosPorPregunta = preguntumEntradaDTO.MinutosPorPregunta;
                preguntum.RespuestaAleatoria = preguntumEntradaDTO.RespuestaAleatoria;
                preguntum.ActivarFeedBackRespuestaCorrecta = preguntumEntradaDTO.ActivarFeedBackRespuestaCorrecta;
                preguntum.ActivarFeedBackRespuestaIncorrecta = preguntumEntradaDTO.ActivarFeedBackRespuestaIncorrecta;
                preguntum.MostrarFeedbackInmediato = preguntumEntradaDTO.MostrarFeedbackInmediato;
                preguntum.MostrarFeedbackPorPregunta = preguntumEntradaDTO.MostrarFeedbackPorPregunta;
                preguntum.IdPreguntaIntento = 130378;
                preguntum.IdPreguntaTipo = preguntumEntradaDTO.IdPreguntaTipo;
                preguntum.IdTipoRespuestaCalificacion = 1;
                preguntum.FactorRespuesta = preguntumEntradaDTO.FactorRespuesta;
                preguntum.IdPreguntaCategoria = 3;
                preguntum.UsuarioModificacion = preguntumEntradaDTO.Usuario;
                preguntum.FechaModificacion = DateTime.Now;
                preguntum.ActivarDescripcion = preguntumEntradaDTO.ActivarDescripcion;
                preguntum.Descripcion = preguntumEntradaDTO.Descripcion;
                preguntum.PreguntaObligatoria = preguntumEntradaDTO.PreguntaObligatoria;
                preguntum.PreguntaActiva = preguntumEntradaDTO.PreguntaActiva;
                var resultado = PreguntumService.Update(preguntum);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="preguntumEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: Preguntum </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<PreguntumEntradaDTO> preguntumEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntumService = new PreguntumService(unitOfWork);
                var solicitudTipoReporteLista = new List<Preguntum>();
                foreach (var entidad in preguntumEntradaDTO)
                {
                    var preguntum = new Preguntum();
                    preguntum = PreguntumService.ObtenerPorId(entidad.Id.Value);
                    preguntum.IdTipoRespuesta = entidad.IdTipoRespuesta;
                    preguntum.IdPreguntaEscalaValor = entidad.IdPreguntaEscalaValor;
                    preguntum.EnunciadoPregunta = entidad.EnunciadoPregunta;
                    preguntum.ConparacionValor = entidad.ConparacionValor;
                    preguntum.RequiereTiempo = entidad.RequiereTiempo;
                    preguntum.MinutosPorPregunta = entidad.MinutosPorPregunta;
                    preguntum.RespuestaAleatoria = entidad.RespuestaAleatoria;
                    preguntum.ActivarFeedBackRespuestaCorrecta = entidad.ActivarFeedBackRespuestaCorrecta;
                    preguntum.ActivarFeedBackRespuestaIncorrecta = entidad.ActivarFeedBackRespuestaIncorrecta;
                    preguntum.MostrarFeedbackInmediato = entidad.MostrarFeedbackInmediato;
                    preguntum.MostrarFeedbackPorPregunta = entidad.MostrarFeedbackPorPregunta;
                    preguntum.IdPreguntaIntento = 130378;
                    preguntum.IdPreguntaTipo = entidad.IdPreguntaTipo;
                    preguntum.IdTipoRespuestaCalificacion = 1;
                    preguntum.FactorRespuesta = entidad.FactorRespuesta;
                    preguntum.IdPreguntaCategoria = 3;
                    preguntum.UsuarioModificacion = entidad.Usuario;
                    preguntum.FechaModificacion = DateTime.Now;
                    preguntum.ActivarDescripcion = entidad.ActivarDescripcion;
                    preguntum.Descripcion = entidad.Descripcion;
                    preguntum.PreguntaObligatoria = entidad.PreguntaObligatoria;
                    preguntum.PreguntaActiva = entidad.PreguntaActiva;
                }
                var resultado = PreguntumService.Update(solicitudTipoReporteLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
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
                var PreguntumService = new PreguntumService(unitOfWork);
                var resultado = PreguntumService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
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
                var PreguntumService = new PreguntumService(unitOfWork);
                var resultado = PreguntumService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                var PreguntumService = new PreguntumService(unitOfWork);
                var resultado = PreguntumService.ObtenerPreguntaEncuestaAsincronica();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
