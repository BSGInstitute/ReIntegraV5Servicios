using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Servicios.Controllers
{

    /// Controlador: EncuestaOnlineController
    /// Autor: Joseph LLanque
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EncuestaOnlineController : Controller
    {
        private IUnitOfWork unitOfWork;
        public EncuestaOnlineController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="encuestaOnlineEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: EncuestaOnline </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] EncuestaOnlineEntradaDTO encuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var existeRegistro = encuestaOnlineService.ExisteEncuestaOnlineTipoEncuestaVersion(encuestaOnlineEntradaDTO.IdTipoEncuesta, encuestaOnlineEntradaDTO.Version);
                if (existeRegistro)
                {
                    return BadRequest("Ya existe una encuesta registrada con el mismo tipo y versión.");
                }
                var encuestaOnline = new EncuestaOnline
                {
                    Nombre = encuestaOnlineEntradaDTO.Nombre,
                    Codigo = encuestaOnlineEntradaDTO.Codigo,
                    Descripcion = encuestaOnlineEntradaDTO.Descripcion,
                    UsuarioCreacion = encuestaOnlineEntradaDTO.Usuario,
                    UsuarioModificacion = encuestaOnlineEntradaDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                    Version = encuestaOnlineEntradaDTO.Version,
                    IdTipoEncuesta = encuestaOnlineEntradaDTO.IdTipoEncuesta,
                    IdModalidadCurso = encuestaOnlineEntradaDTO.IdModalidadCurso
                };
                var resultado = encuestaOnlineService.Add(encuestaOnline);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="encuestaOnlineEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<EncuestaOnline> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<EncuestaOnlineEntradaDTO> encuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var PreguntaEncuestaCategoriaLista = new List<EncuestaOnline>();
                foreach (var entidad in encuestaOnlineEntradaDTO)
                {
                    var encuestaOnline = new EncuestaOnline();
                    encuestaOnline.Nombre = entidad.Nombre;
                    encuestaOnline.Codigo = entidad.Codigo;
                    encuestaOnline.Descripcion = entidad.Descripcion;
                    encuestaOnline.UsuarioCreacion = entidad.Usuario;
                    encuestaOnline.UsuarioModificacion = entidad.Usuario;
                    encuestaOnline.FechaCreacion = DateTime.Now;
                    encuestaOnline.FechaModificacion = DateTime.Now;
                    encuestaOnline.Estado = true;
                    encuestaOnline.Version = entidad.Version;
                    encuestaOnline.IdTipoEncuesta = entidad.IdTipoEncuesta;
                    encuestaOnline.IdModalidadCurso = entidad.IdModalidadCurso;
                    PreguntaEncuestaCategoriaLista.Add(encuestaOnline);
                }

                var resultado = encuestaOnlineService.Add(PreguntaEncuestaCategoriaLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="encuestaOnlineEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: EncuestaOnline </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] EncuestaOnlineEntradaDTO encuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);

                var encuestaExistente = encuestaOnlineService.ObtenerPorId(encuestaOnlineEntradaDTO.Id.Value);
                if (encuestaExistente == null)
                    return NotFound("Encuesta no encontrada.");

                var existeRegistro = encuestaOnlineService.ExisteEncuestaOnlineTipoEncuestaVersion(
                    encuestaOnlineEntradaDTO.IdTipoEncuesta.Value,
                    encuestaOnlineEntradaDTO.Version.Value
                );

                if (existeRegistro && (encuestaExistente.IdTipoEncuesta != encuestaOnlineEntradaDTO.IdTipoEncuesta || encuestaExistente.Version != encuestaOnlineEntradaDTO.Version))
                {
                    return BadRequest("Ya existe una encuesta registrada con el mismo tipo y versión.");
                }

                encuestaExistente.Nombre = encuestaOnlineEntradaDTO.Nombre;
                encuestaExistente.Codigo = encuestaOnlineEntradaDTO.Codigo;
                encuestaExistente.Descripcion = encuestaOnlineEntradaDTO.Descripcion;
                encuestaExistente.UsuarioModificacion = encuestaOnlineEntradaDTO.Usuario;
                encuestaExistente.FechaModificacion = DateTime.Now;
                encuestaExistente.Version = encuestaOnlineEntradaDTO.Version;
                encuestaExistente.IdTipoEncuesta = encuestaOnlineEntradaDTO.IdTipoEncuesta;
                encuestaExistente.IdModalidadCurso = encuestaOnlineEntradaDTO.IdModalidadCurso;

                var resultado = encuestaOnlineService.Update(encuestaExistente);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="encuestaOnlineEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: EncuestaOnline </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<EncuestaOnlineEntradaDTO> encuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var encuestaOnlineLista = new List<EncuestaOnline>();
                foreach (var entidad in encuestaOnlineEntradaDTO)
                {
                    var encuestaOnline = new EncuestaOnline();
                    encuestaOnline = encuestaOnlineService.ObtenerPorId(entidad.Id.Value);
                    encuestaOnline.Nombre = entidad.Nombre;
                    encuestaOnline.Codigo = entidad.Codigo;
                    encuestaOnline.Descripcion = entidad.Descripcion;
                    encuestaOnline.UsuarioModificacion = entidad.Usuario;
                    encuestaOnline.FechaModificacion = DateTime.Now;
                    encuestaOnline.Version = entidad.Version;
                    encuestaOnline.IdTipoEncuesta = entidad.IdTipoEncuesta;
                    encuestaOnline.IdModalidadCurso = entidad.IdModalidadCurso;
                }
                var resultado = encuestaOnlineService.Update(encuestaOnlineLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
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

                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaOnlineService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
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
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaOnlineService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
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
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaOnlineService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEncuestaOnline()
        {
            try
            {
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaOnlineService.ObtenerEncuestaOnline();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 27/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las versiones actuales de encuestas sincrónicas
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerVersionEncuestaSincronico()
        {
            try
            {
                var encuestaOnlineService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaOnlineService.ObtenerVersionEncuestaSincronico();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 28/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos que permiten crear encuestas asincronicas
        /// </summary>
        /// <param name="IdPGeneral"> Id de la entidad </param>
        /// <returns> List<EncuestaEstructuraAsincronicaDTO> </returns>
        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerEncuestaAsincronicaAsignada(int IdPGeneral)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.ObtenerEncuestaAsincronicaAsignada(IdPGeneral);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de encuestas asincronica
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerEncuestaAsincronica()
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.ObtenerEncuestaAsincronica();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una encuesta a un curso asincronica
        /// </summary>
        /// <param name="encuesta"> Id de la entidad </param>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarEncuestaSesionProgramaAsincronica([FromBody] EncuestaAsincronicaDTO encuesta)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.InsertarEncuestaSesionProgramaAsincronica(encuesta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una encuesta a un curso asincronica
        /// </summary>
        /// <param name="id"> id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true o false </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public IActionResult EliminarEncuestaAsincronicaAsignada(int id, string usuario)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.EliminarEncuestaAsincronicaAsignada(id,usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronica"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarEncuestaAsincronica([FromBody] EncuestaAsincronicaEntradaDTO encuestaAsincronica)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.InsertarEncuestaAsincronica(encuestaAsincronica);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una lista preguntas a una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronicaEntradaDTO"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarListaPreguntaAsincronica([FromBody] List<PreguntaExamenAsincronicaDTO> encuestaAsincronicaEntradaDTO)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.InsertarListaPreguntaAsincronica(encuestaAsincronicaEntradaDTO);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una pregunta a una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronicaEntradaDTO"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarPreguntaEncuestaAsincronica([FromBody] PreguntaExamenAsincronicaDTO encuestaAsincronicaEntradaDTO)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.InsertarPreguntaEncuestaAsincronica(encuestaAsincronicaEntradaDTO);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una pregunta de una encuesta asincronica
        /// </summary>
        /// <param name="id"> id de la pregunta relacionada con la encuesta asincrona </param>
        /// <param name="usuario"> usuario modificacion </param>
        /// <returns> true o false </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public IActionResult DeletePreguntaEncuestaAsincronica(int id, string usuario)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.DeletePreguntaEncuestaAsincronica(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una encuesta asincronica
        /// </summary>
        /// <param name="id"> id de la encuesta asincrona </param>
        /// <param name="usuario"> usuario modificacion </param>
        /// <returns> true o false </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public IActionResult DeleteEncuestaAsincronica(int id, string usuario)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.DeleteEncuestaAsincronica(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronica"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult UpdateEncuestaAsincronica([FromBody] EncuestaAsincronicaEntradaDTO encuestaAsincronica)
        {
            try
            {
                var encuestaAsincronicaService = new EncuestaOnlineService(unitOfWork);
                var resultado = encuestaAsincronicaService.UpdateEncuestaAsincronica(encuestaAsincronica);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los subtipos de encuesta activos
        /// </summary>
        /// <returns> List<SubTipoEncuestaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerSubTipoEncuesta()
        {
            try
            {
                var service = new SubTipoEncuestaService(unitOfWork);
                var resultado = service.ObtenerTodo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo Id/Nombre de subtipos de encuesta activos
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboSubTipoEncuesta()
        {
            try
            {
                var service = new SubTipoEncuestaService(unitOfWork);
                var resultado = service.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo subtipo de encuesta
        /// </summary>
        /// <param name="dto"> Datos del subtipo a insertar </param>
        /// <returns> SubTipoEncuesta insertado </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarSubTipoEncuesta([FromBody] SubTipoEncuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var service = new SubTipoEncuestaService(unitOfWork);
                var entidad = new SubTipoEncuesta
                {
                    Nombre              = dto.Nombre,
                    Estado              = true,
                    UsuarioCreacion     = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion       = DateTime.Now,
                    FechaModificacion   = DateTime.Now
                };
                var resultado = service.Add(entidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un subtipo de encuesta existente
        /// </summary>
        /// <param name="dto"> Datos del subtipo a actualizar </param>
        /// <returns> SubTipoEncuesta actualizado </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarSubTipoEncuesta([FromBody] SubTipoEncuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var service = new SubTipoEncuestaService(unitOfWork);
                var existente = service.ObtenerPorId(dto.Id.Value);
                if (existente == null)
                    return NotFound("SubTipoEncuesta no encontrado.");

                existente.Nombre              = dto.Nombre;
                existente.UsuarioModificacion = dto.Usuario;
                existente.FechaModificacion   = DateTime.Now;

                var resultado = service.Update(existente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Eliminación lógica de un subtipo de encuesta (Estado = false)
        /// </summary>
        /// <param name="id"> Id del subtipo </param>
        /// <param name="usuario"> Usuario que realiza la acción </param>
        /// <returns> true o false </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public IActionResult EliminarSubTipoEncuesta(int id, string usuario)
        {
            try
            {
                var service = new SubTipoEncuestaService(unitOfWork);
                var existente = service.ObtenerPorId(id);
                if (existente == null)
                    return NotFound("SubTipoEncuesta no encontrado.");

                var resultado = service.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor:  Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los tipos de encuesta activos para el maestro
        /// </summary>
        /// <returns> List<TipoEncuestaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTodoTipoEncuesta()
        {
            try
            {
                var service = new TipoEncuestumService(unitOfWork);
                var resultado = service.ObtenerTodo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo tipo de encuesta
        /// </summary>
        /// <param name="dto"> Datos del tipo a insertar </param>
        /// <returns> TipoEncuesta insertado </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarTipoEncuesta([FromBody] TipoEncuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var service = new TipoEncuestumService(unitOfWork);
                var entidad = new TipoEncuesta
                {
                    Nombre              = dto.Nombre,
                    Estado              = true,
                    UsuarioCreacion     = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion       = DateTime.Now,
                    FechaModificacion   = DateTime.Now
                };
                var resultado = service.Add(entidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un tipo de encuesta existente
        /// </summary>
        /// <param name="dto"> Datos del tipo a actualizar </param>
        /// <returns> TipoEncuesta actualizado </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarTipoEncuesta([FromBody] TipoEncuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var service = new TipoEncuestumService(unitOfWork);
                var existente = service.ObtenerPorId(dto.Id.Value);
                if (existente == null)
                    return NotFound("TipoEncuesta no encontrado.");

                existente.Nombre              = dto.Nombre;
                existente.UsuarioModificacion = dto.Usuario;
                existente.FechaModificacion   = DateTime.Now;

                var resultado = service.Update(existente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Cuenta las asociaciones activas de un tipo de encuesta
        /// </summary>
        /// <param name="id"> Id del tipo </param>
        /// <returns> int </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ContarAsociacionesPorTipo(int id)
        {
            try
            {
                var service = new TipoSubTipoEncuestaService(unitOfWork);
                var count = service.ObtenerPorTipoEncuesta(id).Count;
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Cuenta las asociaciones activas de un subtipo de encuesta
        /// </summary>
        /// <param name="id"> Id del subtipo </param>
        /// <returns> int </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ContarAsociacionesPorSubTipo(int id)
        {
            try
            {
                var service = new TipoSubTipoEncuestaService(unitOfWork);
                var count = service.ObtenerTodo().Count(x => x.IdSubTipoEncuesta == id);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Eliminación lógica de un tipo de encuesta (Estado = false)
        /// </summary>
        /// <param name="id"> Id del tipo </param>
        /// <param name="usuario"> Usuario que realiza la acción </param>
        /// <returns> true o false </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public IActionResult EliminarTipoEncuesta(int id, string usuario)
        {
            try
            {
                var service = new TipoEncuestumService(unitOfWork);
                var existente = service.ObtenerPorId(id);
                if (existente == null)
                    return NotFound("TipoEncuesta no encontrado.");

                var resultado = service.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las asociaciones tipo-subtipo activas con nombres
        /// </summary>
        /// <returns> List<TipoSubTipoEncuestaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTipoSubTipoEncuesta()
        {
            try
            {
                var service = new TipoSubTipoEncuestaService(unitOfWork);
                var resultado = service.ObtenerTodo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los subtipos asociados a un tipo de encuesta especifico
        /// </summary>
        /// <param name="idTipoEncuesta"> Id del tipo de encuesta </param>
        /// <returns> List<TipoSubTipoEncuestaDTO> </returns>
        [Route("[action]/{idTipoEncuesta}")]
        [HttpGet]
        public IActionResult ObtenerSubTiposPorTipoEncuesta(int idTipoEncuesta)
        {
            try
            {
                var service = new TipoSubTipoEncuestaService(unitOfWork);
                var resultado = service.ObtenerPorTipoEncuesta(idTipoEncuesta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Asocia un subtipo a un tipo de encuesta. Valida que no exista la asociacion previamente.
        /// </summary>
        /// <param name="dto"> Datos de la asociacion </param>
        /// <returns> TipoSubTipoEncuesta insertado </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarTipoSubTipoEncuesta([FromBody] TipoSubTipoEncuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var service = new TipoSubTipoEncuestaService(unitOfWork);
                if (service.ExisteAsociacion(dto.IdTipoEncuesta, dto.IdSubTipoEncuesta))
                    return BadRequest("Ya existe una asociacion activa entre el tipo y subtipo de encuesta seleccionados.");

                var entidad = new TipoSubTipoEncuesta
                {
                    IdTipoEncuesta      = dto.IdTipoEncuesta,
                    IdSubTipoEncuesta   = dto.IdSubTipoEncuesta,
                    Estado              = true,
                    UsuarioCreacion     = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion       = DateTime.Now,
                    FechaModificacion   = DateTime.Now
                };
                var resultado = service.Add(entidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Versión: 1.0
        /// <summary>
        /// Eliminación lógica de una asociacion tipo-subtipo (Estado = false)
        /// </summary>
        /// <param name="id"> Id de la asociacion </param>
        /// <param name="usuario"> Usuario que realiza la acción </param>
        /// <returns> true o false </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public IActionResult EliminarTipoSubTipoEncuesta(int id, string usuario)
        {
            try
            {
                var service = new TipoSubTipoEncuestaService(unitOfWork);
                var resultado = service.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
