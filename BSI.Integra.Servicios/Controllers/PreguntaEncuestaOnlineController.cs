using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PreguntaEncuestaOnlineController
    /// Autor:Joseph Llanque
    /// Fecha: 21/12/2024
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PreguntaEncuestaOnlineController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PreguntaEncuestaOnlineController(IUnitOfWork unitOfWork)
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
        /// <param name="preguntaEncuestaCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PreguntaEncuestaOnline </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] PreguntaEncuestaOnlineEntradaDTO preguntaEncuestaCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new PreguntaEncuestaOnlineService(unitOfWork);
                var preguntaEncuestaOnline = new PreguntaEncuestaOnline();
                preguntaEncuestaOnline.IdPreguntaEncuesta = preguntaEncuestaCategoriaEntradaDTO.IdPreguntaEncuesta;
                preguntaEncuestaOnline.IdEncuestaOnline = preguntaEncuestaCategoriaEntradaDTO.IdEncuestaOnline;
                preguntaEncuestaOnline.UsuarioCreacion = preguntaEncuestaCategoriaEntradaDTO.Usuario;
                preguntaEncuestaOnline.UsuarioModificacion = preguntaEncuestaCategoriaEntradaDTO.Usuario;
                preguntaEncuestaOnline.FechaCreacion = DateTime.Now;
                preguntaEncuestaOnline.FechaModificacion = DateTime.Now;
                preguntaEncuestaOnline.Estado = true;
                var resultado = preguntaEncuestaCategoriaService.Add(preguntaEncuestaOnline);
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
        /// <param name="preguntaEncuestaCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<PreguntaEncuestaOnline> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<PreguntaEncuestaOnlineEntradaDTO> preguntaEncuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new PreguntaEncuestaOnlineService(unitOfWork);
                var PreguntaEncuestaOnlineLista = new List<PreguntaEncuestaOnline>();
                foreach (var entidad in preguntaEncuestaOnlineEntradaDTO)
                {
                    var preguntaEncuestaOnline = new PreguntaEncuestaOnline();
                    preguntaEncuestaOnline.IdPreguntaEncuesta = entidad.IdPreguntaEncuesta;
                    preguntaEncuestaOnline.IdEncuestaOnline = entidad.IdEncuestaOnline;
                    preguntaEncuestaOnline.UsuarioCreacion = entidad.Usuario;
                    preguntaEncuestaOnline.UsuarioModificacion = entidad.Usuario;
                    preguntaEncuestaOnline.FechaCreacion = DateTime.Now;
                    preguntaEncuestaOnline.FechaModificacion = DateTime.Now;
                    preguntaEncuestaOnline.Estado = true;
                    PreguntaEncuestaOnlineLista.Add(preguntaEncuestaOnline);
                }
                var resultado = preguntaEncuestaCategoriaService.Add(PreguntaEncuestaOnlineLista);
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
        /// <param name="preguntaEncuestaCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PreguntaEncuestaOnline </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] PreguntaEncuestaOnlineEntradaDTO preguntaEncuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntaEncuestaOnlineService = new PreguntaEncuestaOnlineService(unitOfWork);
                var preguntaEncuestaOnline = new PreguntaEncuestaOnline();
                preguntaEncuestaOnline = PreguntaEncuestaOnlineService.ObtenerPorId(preguntaEncuestaOnlineEntradaDTO.Id.Value);
                preguntaEncuestaOnline.IdPreguntaEncuesta = preguntaEncuestaOnlineEntradaDTO.IdPreguntaEncuesta;
                preguntaEncuestaOnline.IdEncuestaOnline = preguntaEncuestaOnlineEntradaDTO.IdEncuestaOnline;
                preguntaEncuestaOnline.UsuarioModificacion = preguntaEncuestaOnlineEntradaDTO.Usuario;
                preguntaEncuestaOnline.FechaModificacion = DateTime.Now;
                var resultado = PreguntaEncuestaOnlineService.Update(preguntaEncuestaOnline);
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
        /// <param name="preguntaEncuestaCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PreguntaEncuestaOnline </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<PreguntaEncuestaOnlineEntradaDTO> preguntaEncuestaOnlineEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PreguntaEncuestaOnlineService = new PreguntaEncuestaOnlineService(unitOfWork);
                var solicitudTipoReporteLista = new List<PreguntaEncuestaOnline>();
                foreach (var entidad in preguntaEncuestaOnlineEntradaDTO)
                {
                    var solicitudTipoReporte = new PreguntaEncuestaOnline();
                    solicitudTipoReporte = PreguntaEncuestaOnlineService.ObtenerPorId(entidad.Id.Value);
                    solicitudTipoReporte.IdPreguntaEncuesta = entidad.IdPreguntaEncuesta;
                    solicitudTipoReporte.IdEncuestaOnline = entidad.IdEncuestaOnline;
                    solicitudTipoReporte.UsuarioModificacion = entidad.Usuario;
                    solicitudTipoReporte.FechaModificacion = DateTime.Now;
                }
                var resultado = PreguntaEncuestaOnlineService.Update(solicitudTipoReporteLista);
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

                var PreguntaEncuestaOnlineService = new PreguntaEncuestaOnlineService(unitOfWork);
                var resultado = PreguntaEncuestaOnlineService.Delete(id, usuario);
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
                var PreguntaEncuestaOnlineService = new PreguntaEncuestaOnlineService(unitOfWork);
                var resultado = PreguntaEncuestaOnlineService.Delete(listadoIds, usuario);
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
                var PreguntaEncuestaOnlineService = new PreguntaEncuestaOnlineService(unitOfWork);
                var resultado = PreguntaEncuestaOnlineService.ObtenerCombo();
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
        [Route("[action]/{idEncuestaOnline}")]
        [HttpGet]
        public ActionResult ObtenerPreguntaEncuestaOnline(int idEncuestaOnline)
        {
            try
            {
                var PreguntaEncuestaOnlineService = new PreguntaEncuestaOnlineService(unitOfWork);
                var resultado = PreguntaEncuestaOnlineService.ObtenerPreguntaEncuestaOnline(idEncuestaOnline);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }


}
