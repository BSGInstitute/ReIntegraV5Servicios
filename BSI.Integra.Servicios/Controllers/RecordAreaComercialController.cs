using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RecordAreaComercialController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de RecordAreaComercial
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RecordAreaComercialController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public RecordAreaComercialController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Autor: Gilmer Quispe.
        /// Fecha: 29/12/2022
        /// Versión: 1.1
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="recordAreaComercialCrudDTO"> Datos entrantes nuevos de RecordAreaComercial </param>
        /// <returns> nueva entidad RecordAreaComercial </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] RecordAreaComercialCrudDTO recordAreaComercialCrudDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var recordAreaComercialService = new RecordAreaComercialService(unitOfWork);
                var recordAreaComercial = new RecordAreaComercial();
                recordAreaComercial.Nombre = recordAreaComercialCrudDTO.Nombre;
                recordAreaComercial.Bono = recordAreaComercialCrudDTO.Bono.Value;
                recordAreaComercial.Monto = recordAreaComercialCrudDTO.Monto.Value;
                recordAreaComercial.IdMonedaBono = recordAreaComercialCrudDTO.IdMonedaBono.Value;
                recordAreaComercial.IdMonedaRecord = recordAreaComercialCrudDTO.IdMonedaRecord.Value;
                recordAreaComercial.IdTableroComercialUnidad = recordAreaComercialCrudDTO.IdTableroComercialUnidad.Value;
                recordAreaComercial.VisualizarMonedaLocal= recordAreaComercialCrudDTO.VisualizarMonedaLocal.Value;
                recordAreaComercial.EsVigente = recordAreaComercialCrudDTO.EsVigente.Value;
                recordAreaComercial.UsuarioCreacion = recordAreaComercialCrudDTO.Usuario;
                recordAreaComercial.UsuarioModificacion = recordAreaComercialCrudDTO.Usuario;
                recordAreaComercial.FechaCreacion = DateTime.Now;
                recordAreaComercial.FechaModificacion =  DateTime.Now;
                recordAreaComercial.Estado = true;

                var respuesta = recordAreaComercialService.Add(recordAreaComercial);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<RecordAreaComercial> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Autor Modificacion: Gilmer Quispe.
        /// Fecha: 29/12/2022
        /// Versión: 1.1
        /// <summary>
        /// Realiza una actualización basica a la tabla
        /// </summary>
        /// <param name="recordAreaComercialCrudDTO"> Datos entrantes nuevos de RecordAreaComercial </param>
        /// <returns> entidad actualizada RecordAreaComercial </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] RecordAreaComercialCrudDTO recordAreaComercialCrudDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var recordAreaComercialService = new RecordAreaComercialService(unitOfWork);
                var recordAreaComercial = recordAreaComercialService.ObtenerPorId(recordAreaComercialCrudDTO.Id.Value);

                recordAreaComercial.Nombre = recordAreaComercialCrudDTO.Nombre;
                recordAreaComercial.Bono = recordAreaComercialCrudDTO.Bono.Value;
                recordAreaComercial.Monto = recordAreaComercialCrudDTO.Monto.Value;
                recordAreaComercial.IdMonedaBono = recordAreaComercialCrudDTO.IdMonedaBono.Value;
                recordAreaComercial.IdMonedaRecord = recordAreaComercialCrudDTO.IdMonedaRecord.Value;
                recordAreaComercial.IdTableroComercialUnidad = recordAreaComercialCrudDTO.IdTableroComercialUnidad.Value;
                recordAreaComercial.VisualizarMonedaLocal = recordAreaComercialCrudDTO.VisualizarMonedaLocal.Value;
                recordAreaComercial.EsVigente = recordAreaComercialCrudDTO.EsVigente.Value; 
                recordAreaComercial.UsuarioModificacion = recordAreaComercialCrudDTO.Usuario; 
                recordAreaComercial.FechaModificacion = DateTime.Now; 

                var respuesta = recordAreaComercialService.Update(recordAreaComercial);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<RecordAreaComercial> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_RecordAreaComercial
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerRecordAreaComercial")]
        public IActionResult ObtenerRecordAreaComercial()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                return Ok(servicio.ObtenerRecordAreaComercial());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_RecordAreaComercial para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de datos para (M)Record del area comercial
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerTodoRecordAreaComercial")]
        public IActionResult ObtenerTodoRecordAreaComercial()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RecordAreaComercialService(unitOfWork);
                return Ok(servicio.ObtenerRecordAreaComercialParaTabla());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos iniciales para Maestro Record Area Comercial
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCombosIniciales")]
        public IActionResult ObtenerCombosIniciales()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioMoneda = new MonedaService(unitOfWork);
                var servicioTableroUnidad = new TableroComercialUnidadService(unitOfWork);

                var monedas = servicioMoneda.ObtenerMonedaCodigoCambio();
                var unidades = servicioTableroUnidad.ObtenerTableroComercialUnidadSinAuditoria();

                return Ok(new
                {
                    monedas,
                    unidades
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
