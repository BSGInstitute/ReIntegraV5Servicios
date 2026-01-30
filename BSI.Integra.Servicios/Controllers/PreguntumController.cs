using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using CsvHelper;
using CsvHelper.Configuration;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Transactions;

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
    public class PreguntumController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IPreguntumService preguntumService;
        private ITokenManager tokenManager;

        public PreguntumController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            this.preguntumService = new PreguntumService(unitOfWork);
            this.tokenManager = tokenManager;
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

        [HttpPost("[action]")]
        public IActionResult InsertarPregunta([FromBody] CompuestoPreguntaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(preguntumService.InsertarPregunta(dto, tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerTipoRespuestaCategoria()
        {
            var resultado = preguntumService.ObtenerTipoRespuestaCategoria();
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = preguntumService.Obtener();
                var resultadoAgrupado = resultado.GroupBy(x => new
            {
                x.Id,
                x.Enunciado,
                x.IdTipoRespuesta,
                x.IdPreguntaTipo,
                x.MinutosPorPregunta,
                x.RespuestaAleatoria,
                x.ActivarFeedBackRespuestaCorrecta,
                x.ActivarFeedBackRespuestaIncorrecta,
                x.MostrarFeedbackInmediato,
                x.MostrarFeedbackPorPregunta,
                x.NumeroMaximoIntento,
                x.ActivarFeedbackMaximoIntento,
                x.MensajeFeedbackIntento,
                x.IdTipoRespuestaCalificacion,
                x.FactorRespuesta,
                x.IdPreguntaCategoria
            }).Select(x => new ListaPreguntaAgrupadaDTO
            {
                Id = x.Key.Id,
                Enunciado = x.Key.Enunciado,
                IdTipoRespuesta = x.Key.IdTipoRespuesta,
                IdPreguntaTipo = x.Key.IdPreguntaTipo,
                MinutosPorPregunta = x.Key.MinutosPorPregunta,
                RespuestaAleatoria = x.Key.RespuestaAleatoria,
                ActivarFeedBackRespuestaCorrecta = x.Key.ActivarFeedBackRespuestaCorrecta,
                ActivarFeedBackRespuestaIncorrecta = x.Key.ActivarFeedBackRespuestaIncorrecta,
                MostrarFeedbackInmediato = x.Key.MostrarFeedbackInmediato,
                MostrarFeedbackPorPregunta = x.Key.MostrarFeedbackPorPregunta,
                NumeroMaximoIntento = x.Key.NumeroMaximoIntento,
                ActivarFeedbackMaximoIntento = x.Key.ActivarFeedbackMaximoIntento,
                MensajeFeedback = x.Key.MensajeFeedbackIntento,
                IdTipoRespuestaCalificacion = x.Key.IdTipoRespuestaCalificacion,
                FactorRespuesta = x.Key.FactorRespuesta,
                IdPreguntaCategoria = x.Key.IdPreguntaCategoria,
                ComponenteExamen = x.GroupBy(y => y.ComponenteExamen).Select(y => y.Key).ToList(),
                ListaExamen = x.GroupBy(z => z.IdExamen).Select(z => z.Key).ToList()
            }).ToList();
            return Ok(resultadoAgrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult ObtenerID(int id)
        {
            var resultado = unitOfWork.PreguntumRepository.ObtenerPorId(id);
            return Ok(resultado);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult ObtenerRespuestaPregunta(int id) {
            try {
                var respuesta = preguntumService.ObtenerRespuestaPregunta(id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
        }
        [HttpGet("[action]")]
        public IActionResult ObtenerComboTipoPregunta()
        {
            var resultado = preguntumService.ObtenerComboTipoPregunta();
            return Ok(resultado);
        }

        [HttpPut("[action]")]
        public IActionResult ActualizarPregunta([FromBody] CompuestoPreguntaDTO dto) {
            try
            {
                var resultado = preguntumService.ActualizarPregunta(dto, tokenManager.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarPregunta(int id) {
            try {
                var resultado = preguntumService.EliminarPregunta(id, tokenManager.UserName);
                return Ok(resultado);
            }
            catch {
                throw;
            }
            
        }
        [HttpPost("[action]")]
        public IActionResult ImportarExcel([FromForm] RespuestaPreguntaImportadaDTO Dto)
        {
            CsvFile file = new CsvFile();
            List<string> listaErrores = new List<string>();
            try
            {
                int indexError = 0;
                int indexTotal = 0;
                var pregunta = unitOfWork.PreguntumRepository.FirstById(Dto.IdPregunta);
                using (var reader = new StreamReader(Dto.File.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    MissingFieldFound = null,   // ignora si falta un campo
                    HeaderValidated = null      // ignora si el header no coincide
                }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        int? puntajeTipoRespuesta = null;
                        int? puntaje = csv.GetField<int?>("Puntaje");
                        bool? respuestaCorrecta = csv.GetField<bool?>("RespuestaCorrecta");

                        if (pregunta.IdTipoRespuestaCalificacion.HasValue)
                        {
                            int tipoRes = pregunta.IdTipoRespuestaCalificacion.Value;

                            if (tipoRes == 1) // Directo
                            {
                                puntajeTipoRespuesta = puntaje;
                            }
                            else if (tipoRes == 2) // Inversa
                            {
                                if (pregunta.FactorRespuesta.HasValue)
                                {
                                    int factorRes = pregunta.FactorRespuesta.Value;
                                    puntajeTipoRespuesta = factorRes - puntaje;
                                }
                            }
                            else // Negativo
                            {
                                if (pregunta.FactorRespuesta.HasValue)
                                {
                                    int factorRes = pregunta.FactorRespuesta.Value;
                                    if (respuestaCorrecta.HasValue)
                                    {
                                        if (!respuestaCorrecta.Value)
                                            puntajeTipoRespuesta = puntaje - factorRes;
                                        else
                                            puntajeTipoRespuesta = puntaje;
                                    }
                                }
                            }
                        }

                        indexTotal++;

                        try
                        {
                            var respuestaPregunta = new RespuestaPregunta()
                            {
                                IdPregunta = Dto.IdPregunta,
                                RespuestaCorrecta = csv.GetField<bool?>("RespuestaCorrecta"),
                                NroOrden = csv.GetField<int>("NroOrden"),
                                EnunciadoRespuesta = csv.GetField<string>("EnunciadoRespuesta"),
                                NroOrdenRespuesta = csv.GetField<int?>("NroOrdenRespuesta"),
                                Puntaje = csv.GetField<int?>("Puntaje"),
                                FeedbackPositivo = csv.GetField<string>("FeedbackPositivo"),
                                FeedbackNegativo = csv.GetField<string>("FeedbackNegativo"),
                                Estado = true,
                                UsuarioCreacion = tokenManager.UserName,
                                UsuarioModificacion = tokenManager.UserName,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                PuntajeTipoRespuesta = puntajeTipoRespuesta
                            }; unitOfWork.RespuestaPreguntaRepository.Add(respuestaPregunta);

                            unitOfWork.Commit();
                        }
                        catch (Exception e)
                        {
                            indexError++;
                            listaErrores.Add("Error en: " + csv.GetField<string>("EnunciadoRespuesta") + " - " + e.Message);
                        }
                    }

                    
                }
                ImportarExcelRespuestaDTO resultadoImportarExcel = new()
                {
                    Total = indexTotal,
                    Correcto = (indexTotal - indexError),
                    Error = indexError,
                    Errores = listaErrores
                };
                return Ok(resultadoImportarExcel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); ;
            }
        }
    }
}
