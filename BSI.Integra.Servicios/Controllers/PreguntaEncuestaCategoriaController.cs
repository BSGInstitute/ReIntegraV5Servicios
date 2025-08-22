using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PreguntaEncuestaCategoriaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
        [ApiController]
        [EnableCors("CorsVista")]
        public class PreguntaEncuestaCategoriaController : Controller
        {
            private IUnitOfWork unitOfWork;
            public PreguntaEncuestaCategoriaController(IUnitOfWork unitOfWork)
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
            /// <returns> Entidad: PreguntaEncuestaCategoria </returns>
            [HttpPost("[Action]")]
            public IActionResult Insertar([FromBody] PreguntaEncuestaCategoriaEntradaDTO preguntaEncuestaCategoriaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var preguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var preguntaEncuestaCategoria = new PreguntaEncuestaCategoria();
                        preguntaEncuestaCategoria.Nombre = preguntaEncuestaCategoriaEntradaDTO.Nombre;
                        preguntaEncuestaCategoria.Descripcion = preguntaEncuestaCategoriaEntradaDTO.Descripcion;
                        preguntaEncuestaCategoria.UsuarioCreacion = preguntaEncuestaCategoriaEntradaDTO.Usuario;
                        preguntaEncuestaCategoria.UsuarioModificacion = preguntaEncuestaCategoriaEntradaDTO.Usuario;
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
            /// <param name="preguntaEncuestaCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
            /// <returns> List<PreguntaEncuestaCategoria> </returns>
            [HttpPost("[Action]")]
            public IActionResult InsertarLista([FromBody] List<PreguntaEncuestaCategoriaEntradaDTO> preguntaEncuestaCategoriaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var preguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var PreguntaEncuestaCategoriaLista = new List<PreguntaEncuestaCategoria>();
                    foreach (var entidad in preguntaEncuestaCategoriaEntradaDTO)
                    {
                        var preguntaEncuestaCategoria = new PreguntaEncuestaCategoria();
                            preguntaEncuestaCategoria.Nombre = entidad.Nombre;
                            preguntaEncuestaCategoria.Descripcion = entidad.Descripcion;
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
            /// <param name="preguntaEncuestaCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
            /// <returns> Entidad: PreguntaEncuestaCategoria </returns>
            [HttpPut("[Action]")]
            public IActionResult Actualizar([FromBody] PreguntaEncuestaCategoriaEntradaDTO preguntaEncuestaCategoriaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var preguntaEncuestaCategoria = new PreguntaEncuestaCategoria();
                        preguntaEncuestaCategoria = PreguntaEncuestaCategoriaService.ObtenerPorId(preguntaEncuestaCategoriaEntradaDTO.Id.Value);
                        preguntaEncuestaCategoria.Nombre = preguntaEncuestaCategoriaEntradaDTO.Nombre;
                        preguntaEncuestaCategoria.Descripcion = preguntaEncuestaCategoriaEntradaDTO.Descripcion;
                        preguntaEncuestaCategoria.UsuarioModificacion = preguntaEncuestaCategoriaEntradaDTO.Usuario;
                        preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                    var resultado = PreguntaEncuestaCategoriaService.Update(preguntaEncuestaCategoria);
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
            /// <returns> Entidad: PreguntaEncuestaCategoria </returns>
            [HttpPut("[Action]")]
            public IActionResult ActualizarLista([FromBody] List<PreguntaEncuestaCategoriaEntradaDTO> preguntaEncuestaCategoriaEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var solicitudTipoReporteLista = new List<PreguntaEncuestaCategoria>();
                    foreach (var entidad in preguntaEncuestaCategoriaEntradaDTO)
                    {
                        var solicitudTipoReporte = new PreguntaEncuestaCategoria();
                        solicitudTipoReporte = PreguntaEncuestaCategoriaService.ObtenerPorId(entidad.Id.Value);
                        solicitudTipoReporte.Nombre = entidad.Nombre;
                        solicitudTipoReporte.UsuarioModificacion = entidad.Usuario;
                        solicitudTipoReporte.FechaModificacion = DateTime.Now;
                    }
                    var resultado = PreguntaEncuestaCategoriaService.Update(solicitudTipoReporteLista);
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

                    var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var resultado = PreguntaEncuestaCategoriaService.Delete(id, usuario);
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
                    var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var resultado = PreguntaEncuestaCategoriaService.Delete(listadoIds, usuario);
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
                    var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                    var resultado = PreguntaEncuestaCategoriaService.ObtenerCombo();
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
                var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                var resultado = PreguntaEncuestaCategoriaService.ObtenerCategoriaEncuesta();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]

        public ActionResult ObtenerPreguntaCategoriaAsincronica()
        {
            try
            {
                var PreguntaEncuestaCategoriaService = new PreguntaEncuestaCategoriaService(unitOfWork);
                var resultado = PreguntaEncuestaCategoriaService.ObtenerPreguntaCategoriaAsincronica();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
