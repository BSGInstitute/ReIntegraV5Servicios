using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: HistoricoProductoProveedorController
    /// Autor: Griselberto Huaman
    /// Fecha: 13/07/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class HistoricoProductoProveedorController : Controller
    {
        private IUnitOfWork unitOfWork;
        public HistoricoProductoProveedorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman
        /// Fecha: 13/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] HistoricoProductoProveedor entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman
        /// Fecha: 13/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<HistoricoProductoProveedor> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman
        /// Fecha: 13/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] ActualizarHistoricoDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman
        /// Fecha: 13/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<HistoricoProductoProveedor> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
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
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{listadoIds}/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de FIN.V_ObtenerProductosPrecioHistorico o solo el que conincida con IdHistorico.
        /// </summary>
        /// <returns> List<HistoricoProductoProveedorDTO> </returns>
        /// <paramref name="IdHistorico"/> Identidificador de FIN.V_ObtenerProductosPrecioHistorico.
        [HttpGet("ObtenerTodoHistoricoUltimaVersion/{IdHistorico}")]
        public IActionResult ObtenerTodoHistoricoUltimaVersion(int? IdHistorico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.ObtenerHistoricoUltimaVersion(IdHistorico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de FIN.V_ObtenerNombreHistoricoPP que contengan el valor de la variable "valor".
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        /// <paramref name="valor"/> Identidificador de FIN.V_ObtenerProductosPrecioHistorico.
        [HttpGet("ObtenerNombreHistoricoAutocomplete")]
        public IActionResult ObtenerNombreHistoricoAutocomplete()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.ObtenerNombreHistoricoAutocomplete();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de productos con su precio asociados a un proveedor .
        /// </summary>
        /// <returns> List<ProductoPorProveedorUltimaVersionDTO> </returns>.
        [HttpGet("ObtenerListaProductoPorProveedorUltimaVersion")]
        public IActionResult ObtenerListaProductoPorProveedorUltimaVersion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.ObtenerListaProductoPorProveedorUltimaVersion();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,Nombre de fin.T_CondicionPago para llenado de combo.
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCondicionPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CondicionPagoService(unitOfWork);
                var respuesta = servicio.ObtenerCombo();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,Nombre de fin.T_CondicionTipoPago para llenado de combo.
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCondicionTipoPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CondicionTipoPagoService(unitOfWork);
                var respuesta = servicio.ObtenerCombo();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un Proveedor y los registra en Producto historico.
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> Mensaje result </returns>
        /// <param name="objetoProductoHistorico">una lista de Producto y PoductoHistorico</param>
        [HttpPost("InsertarProveedorAProductoEnHistorico")]
        public IActionResult InsertarProveedorAProductoEnHistorico([FromBody] ProductoHistoricoDTO objetoProductoHistorico )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var _repProductoRep = new ProductoService(unitOfWork);
                    if (objetoProductoHistorico.Productos.Id == 0)
                    {
                        objetoProductoHistorico.Productos = _repProductoRep.InsertarProducto(objetoProductoHistorico.Productos);
                        objetoProductoHistorico.Historico.IdProducto = objetoProductoHistorico.Productos.Id;
                        var respuesta = this.insertarHistoricoProducto(objetoProductoHistorico.Historico);
                        if (respuesta == false) return BadRequest("Ocurrio Un problema al guardar, no existe el Tipo de Cambio de Moneda para este dia.");
                    }
                    else
                    {
                        objetoProductoHistorico.Productos = _repProductoRep.ActualizarProducto(objetoProductoHistorico.Productos);
                        objetoProductoHistorico.Historico.IdProducto = objetoProductoHistorico.Productos.Id;
                        var respuesta = this.insertarHistoricoProducto(objetoProductoHistorico.Historico);
                        if (respuesta == false) return BadRequest("Ocurrio Un problema al guardar, no existe el Tipo de Cambio de Moneda para este dia.");
                    }

                    scope.Complete();


                    return Ok(true);
                }

            }
            catch (Exception ex)
            {
                string result = "Ocurrio Un problema al guardar";
                return BadRequest(result);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta Producto Historico.
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> Mensaje result </returns>
        /// <param name="objetoProductoHistorico">una lista de Producto y PoductoHistorico</param>
        private bool insertarHistoricoProducto(HistoricoProductoProveedorVersionDTO objetoHistorico)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {
                var servicio = new HistoricoProductoProveedorService(unitOfWork);
                var respuesta = servicio.insertarHistoricoProducto(objetoHistorico);
                return respuesta;
            }
            catch (Exception ex) { 
                return false;
            }
        }
    }
}
