using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudCategoriaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 22/12/2022
    /// <summary>
    /// Gestión de Solicitud Categoria
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudCategoriaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public SolicitudCategoriaController(IUnitOfWork unitOfWork)
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
        /// <returns> Entidad: SolicitudCategoria </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] SolicitudCategoriaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var solicitudCategoria = new SolicitudCategoria();
                solicitudCategoria.Nombre = solicitudCategoriaEntradaDTO.Nombre;
                solicitudCategoria.IdSolicitudTipoReporte = solicitudCategoriaEntradaDTO.IdSolicitudTipoReporte;
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
        /// <returns> List<SolicitudCategoria> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<SolicitudCategoriaEntradaDTO> solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var solicitudCategoriaLista = new List<SolicitudCategoria>();
                foreach (var entidad in solicitudCategoriaEntradaDTO)
                {
                    var solicitudCategoria = new SolicitudCategoria();
                    solicitudCategoria.Nombre = entidad.Nombre;
                    solicitudCategoria.IdSolicitudTipoReporte = entidad.IdSolicitudTipoReporte;
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
        /// <returns> Entidad: SolicitudCategoria </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] SolicitudCategoriaEntradaDTO solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var solicitudCategoria = new SolicitudCategoria();
                solicitudCategoria = solicitudCategoriaService.ObtenerPorId(solicitudCategoriaEntradaDTO.Id.Value);
                solicitudCategoria.Nombre = solicitudCategoriaEntradaDTO.Nombre;
                solicitudCategoria.IdSolicitudTipoReporte = solicitudCategoriaEntradaDTO.IdSolicitudTipoReporte;
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
        /// <returns> Entidad: SolicitudCategoria </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<SolicitudCategoriaEntradaDTO> solicitudCategoriaEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var solicitudCategoriaLista = new List<SolicitudCategoria>();
                foreach (var entidad in solicitudCategoriaEntradaDTO)
                {
                    var solicitudCategoria = new SolicitudCategoria();
                    solicitudCategoria = solicitudCategoriaService.ObtenerPorId(entidad.Id.Value);
                    solicitudCategoria.IdSolicitudTipoReporte = entidad.IdSolicitudTipoReporte;
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

                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
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
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
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
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var resultado = solicitudCategoriaService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 01/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipos de reporte y  categorias
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoReporteCategoria()
        {
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var resultado = solicitudCategoriaService.ObtenerTipoReporteCategoria();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipos de reporte y  categorias
        /// </summary>
        /// <returns> List<TipoReporteSubCategoriaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoReporteSubCategoria()
        {
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var resultado = solicitudCategoriaService.ObtenerTipoReporteSubCategoria();
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
        /// Obtiene los registros de la tabla asociados al IdTipoReporte
        /// </summary>
        /// <param name="idTipoReporte"> Id de T_SolicitudTipoReporte </param>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idTipoReporte}")]
        [HttpGet]
        public ActionResult ObtenerComboPorTipoReporte(int idTipoReporte)
        {
            try
            {
                var solicitudCategoriaService = new SolicitudCategoriaService(unitOfWork);
                var resultado = solicitudCategoriaService.ObtenerComboPorTipoReporte(idTipoReporte);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
