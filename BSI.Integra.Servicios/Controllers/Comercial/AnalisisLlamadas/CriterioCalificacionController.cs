using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial.AnalisisLlamadas
{

    /// Controlador: CriterioCalificacionController
    /// Autor: Joseph Llanque.
    /// Fecha:07/03/2025
    /// <summary>
    /// Fase Calificacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriterioCalificacionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CriterioCalificacionController(IUnitOfWork unitOfWork)
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
        /// <param name="criterioCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: CriterioCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] CriterioCalificacionEntradaDTO criterioCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var CriterioCalificacion = new CriterioCalificacionLlamada();
                CriterioCalificacion.IdFaseCalificacion = criterioCalificacionEntradaDTO.IdFaseCalificacion;
                CriterioCalificacion.NombreCriterio = criterioCalificacionEntradaDTO.NombreCriterio;
                CriterioCalificacion.Orden = criterioCalificacionEntradaDTO.Orden;
                CriterioCalificacion.Descripcion = criterioCalificacionEntradaDTO.Descripcion;
                CriterioCalificacion.UsuarioCreacion = criterioCalificacionEntradaDTO.Usuario;
                CriterioCalificacion.UsuarioModificacion = criterioCalificacionEntradaDTO.Usuario;
                CriterioCalificacion.FechaCreacion = DateTime.Now;
                CriterioCalificacion.FechaModificacion = DateTime.Now;
                CriterioCalificacion.Estado = true;
                var resultado = criterioCalificacionService.Add(CriterioCalificacion);
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
        /// <param name="criterioCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<CriterioCalificacion> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<CriterioCalificacionEntradaDTO> criterioCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var criterioCalificacionLista = new List<CriterioCalificacionLlamada>();
                foreach (var entidad in criterioCalificacionEntradaDTO)
                {
                    var CriterioCalificacion = new CriterioCalificacionLlamada();
                    CriterioCalificacion.IdFaseCalificacion = entidad.IdFaseCalificacion;
                    CriterioCalificacion.NombreCriterio = entidad.NombreCriterio;
                    CriterioCalificacion.Orden = entidad.Orden;
                    CriterioCalificacion.Descripcion = entidad.Descripcion;
                    CriterioCalificacion.UsuarioCreacion = entidad.Usuario;
                    CriterioCalificacion.UsuarioModificacion = entidad.Usuario;
                    CriterioCalificacion.FechaCreacion = DateTime.Now;
                    CriterioCalificacion.FechaModificacion = DateTime.Now;
                    CriterioCalificacion.Estado = true;
                    criterioCalificacionLista.Add(CriterioCalificacion);
                }
                var resultado = criterioCalificacionService.Add(criterioCalificacionLista);
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
        /// <param name="criterioCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: CriterioCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CriterioCalificacionEntradaDTO criterioCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var CriterioCalificacion = new CriterioCalificacionLlamada();
                CriterioCalificacion = criterioCalificacionService.ObtenerPorId(criterioCalificacionEntradaDTO.Id.Value);
                CriterioCalificacion.IdFaseCalificacion = criterioCalificacionEntradaDTO.IdFaseCalificacion;
                CriterioCalificacion.NombreCriterio = criterioCalificacionEntradaDTO.NombreCriterio;
                CriterioCalificacion.Orden = criterioCalificacionEntradaDTO.Orden;
                CriterioCalificacion.Descripcion = criterioCalificacionEntradaDTO.Descripcion;
                CriterioCalificacion.UsuarioModificacion = criterioCalificacionEntradaDTO.Usuario;
                CriterioCalificacion.FechaModificacion = DateTime.Now;
                var resultado = criterioCalificacionService.Update(CriterioCalificacion);
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
        /// <param name="criterioCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: CriterioCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<CriterioCalificacionEntradaDTO> criterioCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var criterioCalificacionLista = new List<CriterioCalificacionLlamada>();
                foreach (var entidad in criterioCalificacionEntradaDTO)
                {
                    var CriterioCalificacion = new CriterioCalificacionLlamada();
                    CriterioCalificacion = criterioCalificacionService.ObtenerPorId(entidad.Id.Value);
                    CriterioCalificacion.IdFaseCalificacion = entidad.IdFaseCalificacion;
                    CriterioCalificacion.NombreCriterio = entidad.NombreCriterio;
                    CriterioCalificacion.Orden = entidad.Orden;
                    CriterioCalificacion.Descripcion = entidad.Descripcion;
                    CriterioCalificacion.UsuarioModificacion = entidad.Usuario;
                    CriterioCalificacion.FechaModificacion = DateTime.Now;
                }
                var resultado = criterioCalificacionService.Update(criterioCalificacionLista);
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

                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var resultado = criterioCalificacionService.Delete(id, usuario);
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
                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var resultado = criterioCalificacionService.Delete(listadoIds, usuario);
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
        public ActionResult ObtenerCriterios()
        {
            try
            {
                var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
                var resultado = criterioCalificacionService.ObtenerCriterios();
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
        //[Route("[action]")]
        //[HttpGet]
        //public ActionResult ObtenerCombo()
        //{
        //    try
        //    {
        //        var criterioCalificacionService = new CriterioCalificacionLlamadaService(unitOfWork);
        //        var resultado = criterioCalificacionService.ObtenerCombo();
        //        return Ok(resultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
