using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EncuestaSesionProgramaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EncuestaSesionProgramaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public EncuestaSesionProgramaController(IUnitOfWork unitOfWork)
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
        /// <param name="encuestaSesionProgramaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: EncuestaSesionPrograma </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] EncuestaSesionProgramaEntradaDTO encuestaSesionProgramaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new EncuestaSesionProgramaService(unitOfWork);
                var preguntaEncuestaCategoria = new EncuestaSesionPrograma();
                preguntaEncuestaCategoria.IdPgeneral = encuestaSesionProgramaEntradaDTO.IdPgeneral;
                preguntaEncuestaCategoria.IdPespecifico = encuestaSesionProgramaEntradaDTO.IdPespecifico;
                preguntaEncuestaCategoria.IdPespecificoSesion = encuestaSesionProgramaEntradaDTO.IdPespecificoSesion;
                preguntaEncuestaCategoria.IdEncuestaOnline = encuestaSesionProgramaEntradaDTO.IdEncuestaOnline;
                preguntaEncuestaCategoria.EncuestaObligatoria = encuestaSesionProgramaEntradaDTO.EncuestaObligatoria;
                preguntaEncuestaCategoria.EncuestaActiva = encuestaSesionProgramaEntradaDTO.EncuestaActiva;
                preguntaEncuestaCategoria.AsignadoPara = encuestaSesionProgramaEntradaDTO.AsignadoPara;
                preguntaEncuestaCategoria.UsuarioCreacion = encuestaSesionProgramaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.UsuarioModificacion = encuestaSesionProgramaEntradaDTO.Usuario;
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
        /// <param name="encuestaSesionProgramaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<EncuestaSesionPrograma> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<EncuestaSesionProgramaEntradaDTO> encuestaSesionProgramaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var preguntaEncuestaCategoriaService = new EncuestaSesionProgramaService(unitOfWork);
                var PreguntaEncuestaCategoriaLista = new List<EncuestaSesionPrograma>();
                foreach (var entidad in encuestaSesionProgramaEntradaDTO)
                {

                    var preguntaEncuestaCategoria = new EncuestaSesionPrograma();
                    preguntaEncuestaCategoria.IdPgeneral = entidad.IdPgeneral;
                    preguntaEncuestaCategoria.IdPespecifico = entidad.IdPespecifico;
                    preguntaEncuestaCategoria.IdPespecificoSesion = entidad.IdPespecificoSesion;
                    preguntaEncuestaCategoria.IdEncuestaOnline = entidad.IdEncuestaOnline;
                    preguntaEncuestaCategoria.EncuestaObligatoria = entidad.EncuestaObligatoria;
                    preguntaEncuestaCategoria.EncuestaActiva = entidad.EncuestaActiva;
                    preguntaEncuestaCategoria.AsignadoPara = entidad.AsignadoPara;
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
        /// <param name="encuestaSesionProgramaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: EncuestaSesionPrograma </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] EncuestaSesionProgramaEntradaDTO encuestaSesionProgramaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var preguntaEncuestaCategoria = new EncuestaSesionPrograma();
                
                preguntaEncuestaCategoria = EncuestaSesionProgramaService.ObtenerPorId(encuestaSesionProgramaEntradaDTO.Id.Value);
                preguntaEncuestaCategoria.IdPgeneral = encuestaSesionProgramaEntradaDTO.IdPgeneral;
                preguntaEncuestaCategoria.IdPespecifico = encuestaSesionProgramaEntradaDTO.IdPespecifico;
                preguntaEncuestaCategoria.IdPespecificoSesion = encuestaSesionProgramaEntradaDTO.IdPespecificoSesion;
                preguntaEncuestaCategoria.IdEncuestaOnline = encuestaSesionProgramaEntradaDTO.IdEncuestaOnline;
                preguntaEncuestaCategoria.EncuestaObligatoria = encuestaSesionProgramaEntradaDTO.EncuestaObligatoria;
                preguntaEncuestaCategoria.EncuestaActiva = encuestaSesionProgramaEntradaDTO.EncuestaActiva;
                preguntaEncuestaCategoria.AsignadoPara = encuestaSesionProgramaEntradaDTO.AsignadoPara;
                preguntaEncuestaCategoria.UsuarioModificacion = encuestaSesionProgramaEntradaDTO.Usuario;
                preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                var resultado = EncuestaSesionProgramaService.Update(preguntaEncuestaCategoria);
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
        /// <param name="encuestaSesionProgramaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: EncuestaSesionPrograma </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<EncuestaSesionProgramaEntradaDTO> encuestaSesionProgramaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var solicitudTipoReporteLista = new List<EncuestaSesionPrograma>();
                foreach (var entidad in encuestaSesionProgramaEntradaDTO)
                {
                    var preguntaEncuestaCategoria = new EncuestaSesionPrograma();
                    preguntaEncuestaCategoria = EncuestaSesionProgramaService.ObtenerPorId(entidad.Id.Value);
                    preguntaEncuestaCategoria.IdPgeneral = entidad.IdPgeneral;
                    preguntaEncuestaCategoria.IdPespecifico = entidad.IdPespecifico;
                    preguntaEncuestaCategoria.IdPespecificoSesion = entidad.IdPespecificoSesion;
                    preguntaEncuestaCategoria.IdEncuestaOnline = entidad.IdEncuestaOnline;
                    preguntaEncuestaCategoria.EncuestaObligatoria = entidad.EncuestaObligatoria;
                    preguntaEncuestaCategoria.EncuestaActiva = entidad.EncuestaActiva;
                    preguntaEncuestaCategoria.AsignadoPara = entidad.AsignadoPara;
                    preguntaEncuestaCategoria.UsuarioModificacion = entidad.Usuario;
                    preguntaEncuestaCategoria.FechaModificacion = DateTime.Now;
                }
                var resultado = EncuestaSesionProgramaService.Update(solicitudTipoReporteLista);
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

                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var resultado = EncuestaSesionProgramaService.Delete(id, usuario);
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
                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var resultado = EncuestaSesionProgramaService.Delete(listadoIds, usuario);
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
                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var resultado = EncuestaSesionProgramaService.ObtenerCombo();
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
        [Route("[action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerEncuestasPrograma(int idPespecifico)
        {
            try
            {
                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var resultado = EncuestaSesionProgramaService.ObtenerEncuestasPrograma(idPespecifico);
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
        [Route("[action]/{idPespecificoSesion}")]
        [HttpGet]
        public ActionResult ObtenerEncuestaAsignada(int idPespecificoSesion)
        {
            try
            {
                var EncuestaSesionProgramaService = new EncuestaSesionProgramaService(unitOfWork);
                var resultado = EncuestaSesionProgramaService.ObtenerEncuestaAsignada(idPespecificoSesion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
