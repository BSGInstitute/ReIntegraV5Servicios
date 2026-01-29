using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial.AnalisisLlamadas
{
    /// Controlador: CriticidadCalificacionController
    /// Autor: Joseph Llanque.
    /// Fecha:07/03/2025
    /// <summary>
    /// Fase Calificacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriticidadCalificacionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CriticidadCalificacionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="criticidadCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: CriticidadCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] CriticidadCalificacionEntradaDTO criticidadCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var CriticidadCalificacion = new CriticidadCalificacion();
                CriticidadCalificacion.NombreCriticidad = criticidadCalificacionEntradaDTO.NombreCriticidad;
                CriticidadCalificacion.Descripcion = criticidadCalificacionEntradaDTO.Descripcion;
                CriticidadCalificacion.UsuarioCreacion = criticidadCalificacionEntradaDTO.Usuario;
                CriticidadCalificacion.UsuarioModificacion = criticidadCalificacionEntradaDTO.Usuario;
                CriticidadCalificacion.FechaCreacion = DateTime.Now;
                CriticidadCalificacion.FechaModificacion = DateTime.Now;
                CriticidadCalificacion.Estado = true;
                var resultado = criticidadCalificacionService.Add(CriticidadCalificacion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="criticidadCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<CriticidadCalificacion> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<CriticidadCalificacionEntradaDTO> criticidadCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var criticidadCalificacionLista = new List<CriticidadCalificacion>();
                foreach (var entidad in criticidadCalificacionEntradaDTO)
                {
                    var CriticidadCalificacion = new CriticidadCalificacion();
                    CriticidadCalificacion.NombreCriticidad = entidad.NombreCriticidad;
                    CriticidadCalificacion.Descripcion = entidad.Descripcion;
                    CriticidadCalificacion.UsuarioCreacion = entidad.Usuario;
                    CriticidadCalificacion.UsuarioModificacion = entidad.Usuario;
                    CriticidadCalificacion.FechaCreacion = DateTime.Now;
                    CriticidadCalificacion.FechaModificacion = DateTime.Now;
                    CriticidadCalificacion.Estado = true;
                    criticidadCalificacionLista.Add(CriticidadCalificacion);
                }
                var resultado = criticidadCalificacionService.Add(criticidadCalificacionLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="criticidadCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: CriticidadCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CriticidadCalificacionEntradaDTO criticidadCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var CriticidadCalificacion = new CriticidadCalificacion();
                CriticidadCalificacion = criticidadCalificacionService.ObtenerPorId(criticidadCalificacionEntradaDTO.Id.Value);
                CriticidadCalificacion.NombreCriticidad = criticidadCalificacionEntradaDTO.NombreCriticidad;
                CriticidadCalificacion.Descripcion = criticidadCalificacionEntradaDTO.Descripcion;
                CriticidadCalificacion.UsuarioModificacion = criticidadCalificacionEntradaDTO.Usuario;
                CriticidadCalificacion.FechaModificacion = DateTime.Now;
                var resultado = criticidadCalificacionService.Update(CriticidadCalificacion);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="criticidadCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: CriticidadCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<CriticidadCalificacionEntradaDTO> criticidadCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var criticidadCalificacionLista = new List<CriticidadCalificacion>();
                foreach (var entidad in criticidadCalificacionEntradaDTO)
                {
                    var CriticidadCalificacion = new CriticidadCalificacion();
                    CriticidadCalificacion = criticidadCalificacionService.ObtenerPorId(entidad.Id.Value);
                    CriticidadCalificacion.NombreCriticidad = entidad.NombreCriticidad;
                    CriticidadCalificacion.Descripcion = entidad.Descripcion;
                    CriticidadCalificacion.UsuarioModificacion = entidad.Usuario;
                    CriticidadCalificacion.FechaModificacion = DateTime.Now;
                }
                var resultado = criticidadCalificacionService.Update(criticidadCalificacionLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
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

                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var resultado = criticidadCalificacionService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
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
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var resultado = criticidadCalificacionService.Delete(listadoIds, usuario);
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
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var resultado = criticidadCalificacionService.ObtenerCombo();
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
        public ActionResult ObtenerCriticidad()
        {
            try
            {
                var criticidadCalificacionService = new CriticidadCalificacionService(unitOfWork);
                var resultado = criticidadCalificacionService.ObtenerCriticidad();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
