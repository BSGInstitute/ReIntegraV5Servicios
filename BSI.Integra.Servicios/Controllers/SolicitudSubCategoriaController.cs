using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudSubCategoriaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 22/12/2022
    /// <summary>
    /// Gestión de Solicitud Sub Categoria
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudSubCategoriaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public SolicitudSubCategoriaController(IUnitOfWork unitOfWork)
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
        /// <param name="solicitudCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudSubCategoria </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] SolicitudSubCategoriaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var solicitudCategoria = new SolicitudSubCategoria();
                solicitudCategoria.Nombre = solicitudCategoriaEntradaDTO.Nombre;
                solicitudCategoria.IdSolicitudCategoria = solicitudCategoriaEntradaDTO.IdSolicitudCategoria;
                solicitudCategoria.UsuarioCreacion = solicitudCategoriaEntradaDTO.Usuario;
                solicitudCategoria.UsuarioModificacion = solicitudCategoriaEntradaDTO.Usuario;
                solicitudCategoria.FechaCreacion = DateTime.Now;
                solicitudCategoria.FechaModificacion = DateTime.Now;
                solicitudCategoria.Estado = true;
                var resultado = solicitudCategoriaService.Add(solicitudCategoria);
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
        /// <param name="solicitudCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<SolicitudSubCategoria> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<SolicitudSubCategoriaEntradaDTO> solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var solicitudCategoriaLista = new List<SolicitudSubCategoria>();
                foreach (var entidad in solicitudCategoriaEntradaDTO)
                {
                    var solicitudCategoria = new SolicitudSubCategoria();
                    solicitudCategoria.Nombre = entidad.Nombre;
                    solicitudCategoria.IdSolicitudCategoria = entidad.IdSolicitudCategoria;
                    solicitudCategoria.UsuarioCreacion = entidad.Usuario;
                    solicitudCategoria.UsuarioModificacion = entidad.Usuario;
                    solicitudCategoria.FechaCreacion = DateTime.Now;
                    solicitudCategoria.FechaModificacion = DateTime.Now;
                    solicitudCategoria.Estado = true;
                    solicitudCategoriaLista.Add(solicitudCategoria);
                }
                var resultado = solicitudCategoriaService.Add(solicitudCategoriaLista);
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
        /// <param name="solicitudCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudSubCategoria </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] SolicitudSubCategoriaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var solicitudCategoria = new SolicitudSubCategoria();
                solicitudCategoria = solicitudCategoriaService.ObtenerPorId(solicitudCategoriaEntradaDTO.Id.Value);
                solicitudCategoria.Nombre = solicitudCategoriaEntradaDTO.Nombre;
                solicitudCategoria.IdSolicitudCategoria = solicitudCategoriaEntradaDTO.IdSolicitudCategoria;
                solicitudCategoria.UsuarioModificacion = solicitudCategoriaEntradaDTO.Usuario;
                solicitudCategoria.FechaModificacion = DateTime.Now;
                var resultado = solicitudCategoriaService.Update(solicitudCategoria);
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
        /// <param name="solicitudCategoriaEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudSubCategoria </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<SolicitudSubCategoriaEntradaDTO> solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var solicitudCategoriaLista = new List<SolicitudSubCategoria>();
                foreach (var entidad in solicitudCategoriaEntradaDTO)
                {
                    var solicitudCategoria = new SolicitudSubCategoria();
                    solicitudCategoria = solicitudCategoriaService.ObtenerPorId(entidad.Id.Value);
                    solicitudCategoria.IdSolicitudCategoria = entidad.IdSolicitudCategoria;
                    solicitudCategoria.Nombre = entidad.Nombre;
                    solicitudCategoria.UsuarioModificacion = entidad.Usuario;
                    solicitudCategoria.FechaModificacion = DateTime.Now;
                }
                var resultado = solicitudCategoriaService.Update(solicitudCategoriaLista);
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

                var solicitudCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var resultado = solicitudCategoriaService.Delete(id, usuario);
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
                var solicitudCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var resultado = solicitudCategoriaService.Delete(listadoIds, usuario);
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
                var solicitudSubCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var resultado = solicitudSubCategoriaService.ObtenerCombo();
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
        [HttpPost]
        public ActionResult InsertarProblema([FromBody] SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            try
            {
                var solicitudSubCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var resultado = solicitudSubCategoriaService.InsertarProblema(solicitudCategoriaEntradaDTO);
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
        [HttpPost]
        public ActionResult ActualizarProblema([FromBody] SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            try
            {
                var solicitudSubCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var resultado = solicitudSubCategoriaService.ActualizarProblema(solicitudCategoriaEntradaDTO);
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
        [HttpPost]
        public ActionResult EliminarProblema([FromBody] SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            try
            {
                var solicitudSubCategoriaService = new SolicitudSubCategoriaService(unitOfWork);
                var resultado = solicitudSubCategoriaService.EliminarProblema(solicitudCategoriaEntradaDTO);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
