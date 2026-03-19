using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProveedorController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de Proveedor
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProveedorController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ProveedorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Proveedor entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<Proveedor> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Proveedor entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<Proveedor> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
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
        [HttpDelete("Eliminar")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
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
        [HttpDelete("EliminarListado")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene nombre del proveedor para comboBox.
        /// </summary>
        /// <param name="Filtros">Filtros del formulario</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerNombreProveedor")]
        public IActionResult ObtenerNombreProveedor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ObtenerNombreProveedorAutocomplete());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ruc y nombre del proveedor para comboBox.
        /// </summary>
        /// <param name="valor">Filtros del formulario</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProveedorRuc")]
        public IActionResult ObtenerProveedorRucAutocomplete()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ObtenerProveedorRucAutocomplete());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ruc y nombre del proveedor para comboBox v2..
        /// </summary>
        /// <param name="valor">Filtros del formulario</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerRucNombreProveedorAutocomplete2/{valor}")]
        public IActionResult ObtenerRucNombreProveedorAutocomplete2(string valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ObtenerProveedorPorRuc(valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipo de contribuyente.
        /// </summary>
        /// <param name="valor">Filtros del formulario</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerTipoContribuyente")]
        public IActionResult ObtenerTipoContribuyente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new TipoContribuyenteService(unitOfWork);
                return Ok(servicio.ObtenerTipoContribuyente());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene proveedores para la grilla.
        /// </summary>
        /// <param name="IdProveedor">Id del proveedor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProveedorGrilla/{IdProveedor}")]
        public IActionResult ObtenerProveedorGrilla(int? IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ObtenerProveedorGrilla(IdProveedor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene proveedores para la grilla.
        /// </summary>
        /// <param name="Usuario">Id del proveedor</param>
        /// <param name="Id">Id del proveedor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpDelete("EliminarProveedor/{Id}/{Usuario}")]
        public IActionResult EliminarProveedor(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.EliminarProveedor(Id, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene proveedores por subcriterio de calificacion.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProveedorSubCriterioCalificacion")]
        public IActionResult ObtenerProveedorSubCriterioCalificacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorSubCriterioCalificacionService(unitOfWork);
                return Ok(servicio.ObtenerSubCriterioCalificacion());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta cuenta de banco a un proveedor.
        /// </summary>
        /// <param name="proveedorCuenta">Cuenta del proveedor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("InsertarProveedorCuentaBanco")]
        public IActionResult InsertarProveedorCuentaBanco([FromBody] ProveedorCuentasDTO proveedorCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.InsertarProveedorCuentaBanco(proveedorCuenta.proveedor, proveedorCuenta.listaCuentaBanco));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza cuenta de banco a un proveedor.
        /// </summary>
        /// <param name="proveedorCuenta">Cuenta del proveedor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPut("ActualizarProveedorCuentaBanco")]
        public IActionResult ActualizarProveedorCuentaBanco([FromBody] ProveedorCuentasDTO proveedorCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ActualizarProveedorCuentaBanco(proveedorCuenta.proveedor, proveedorCuenta.listaCuentaBanco));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina cuenta de banco de un proveedor.
        /// </summary>
        /// <param name="Id">Identificador de Proveedor</param>
        /// /<param name="Usuario">Identificador de Proveedor</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpDelete("EliminarProveedorCuentaBanco/{Id}/{Usuario}")]
        public IActionResult EliminarProveedorCuentaBanco(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorCuentaBancoService(unitOfWork);
                return Ok(servicio.Delete(Id, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registro de prestaciones.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPrestacionRegistro")]
        public IActionResult ObtenerPrestacionRegistro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PrestacionRegistroService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta criterio de calificacion de proveedor.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("InsertarCriterioCalificacionAProveedor")]
        public IActionResult InsertarCriterioCalificacionAProveedor([FromBody] FiltroProveedorCalificacionDTO Calificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorCalificacionService(unitOfWork);
                return Ok(servicio.InsertarCriterioCalificacion(Calificacion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida existencia de un proveedor.
        /// </summary>
        /// <param name="DocidentidadEmail">Documento de identidad y Email</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ValidarExistenciaProveedor")]
        public IActionResult ValidarExistenciaProveedor([FromBody] CadenaStringDTO DocidentidadEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ValidarExistenciaProveedor(DocidentidadEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene impuesto de un proveedor.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerImpuestosProveedor")]
        public IActionResult ObtenerImpuestosProveedor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ObtenerImpuestosProveedor());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene nombre de un proveedor para honorario.
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerNombreProveedorParaHonorario()
        {
            var servicio = new ProveedorService(unitOfWork);
            return Ok(servicio.ObtenerNombreProveedorParaHonorario());
        }

        [HttpGet("ObtenerTodoCoordinadoresDocentes")]
        public IActionResult ObtenerTodoCoordinadoresDocentes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorService(unitOfWork);
                return Ok(servicio.ObtenerTodoCoordinadoresDocentes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{IdProveedor}")]
        public IActionResult ObtenerCuentaBancoProveedor(int IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProveedorCuentaBancoService(unitOfWork);
                return Ok(servicio.ObtenerCuentasProveedorById(IdProveedor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
		/// Autor: Jonathan Caipo
		/// Fecha: 03/05/2023
		/// Version: 1.0
		/// <summary>
		/// Función que trae data para llenar los combos de PEspecifico
		/// </summary>
		/// <returns>Retorna un objeto agrupado</returns>
		[Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerProveedor()
        {
            try
            {
                ProveedorService reporteControlTareaAlumnoService = new ProveedorService(unitOfWork);
                return Ok(reporteControlTareaAlumnoService.ObtenerProveedorFiltro());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Listar los nombres de los docentes para el filtro
        /// </summary>
        /// <returns>Lista de los nombres en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDocentes()
        {
            try
            {
                IProveedorService reporteEncuestaInicialService = new ProveedorService(unitOfWork);
                return Ok(reporteEncuestaInicialService.ObtenerListaDocentes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("[action]")]
        public IActionResult ObtenerDocentesActivos()
        {
            IProveedorService reporteEncuestaInicialService = new ProveedorService(unitOfWork);
            var resultado = reporteEncuestaInicialService.ObtenerDocentesActivos();
            return Ok(resultado);
        }

        
    }
}
