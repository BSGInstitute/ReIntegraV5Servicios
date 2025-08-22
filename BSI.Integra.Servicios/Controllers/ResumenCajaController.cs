using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ResumenCajaController
    /// Autor: Griselberto Huaman
    /// Fecha: 04/10/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ResumenCajaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ResumenCajaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros NIC entre las fechas .
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCajaIngresoByFecha/{FechaInicial}/{FechaFinal}/{IdCaja}")]
        public IActionResult ObtenerCajaIngresoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new NotaIngresoCajaService(unitOfWork);
                return Ok(servicio.ObtenerCajaIngresoByFecha(FechaInicial, FechaFinal, IdCaja));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros REC entre las fechas .
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCajaEgresoAprobadoByFecha/{FechaInicial}/{FechaFinal}/{IdCaja}")]
        public IActionResult ObtenerCajaEgresoAprobadoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                return Ok(servicio.ObtenerCajaEgresoAprobadoByFecha(FechaInicial, FechaFinal, IdCaja));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros PR entre las fechas .
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCajaPorRendirByFecha/{FechaInicial}/{FechaFinal}/{IdCaja}")]
        public IActionResult ObtenerCajaPorRendirByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaPorRendirService(unitOfWork);
                return Ok(servicio.ObtenerCajaPorRendirByFecha(FechaInicial, FechaFinal, IdCaja));
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
        /// Obtiene Lista de Byte segun los Nic's solicitados .
        /// </summary>
        /// <returns> IEnumerable<byte[]> </returns>
        [HttpPost("ObtenerDocumentosNIC")]
        public IActionResult ObtenerDocumentosNIC([FromBody] List<int> listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new NotaIngresoCajaService(unitOfWork);
                return Ok(servicio.ObtenerDocumentosNIC(listaEnteros));
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
        /// Obtiene Lista de Byte segun los REC's solicitados .
        /// </summary>
        /// <returns> IEnumerable<byte[]> </returns>
        [HttpPost("ObtenerDocumentosEgresoCaja")]
        public IActionResult ObtenerDocumentosEgresoCaja(List<int> listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaEgresoService(unitOfWork);
                return Ok(servicio.ObtenerDocumentosEgresoCaja(listaEnteros));
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
        /// Obtiene Lista de Byte segun los PR's solicitados .
        /// </summary>
        /// <returns> IEnumerable<byte[]> </returns>
        [HttpPost("ObtenerDocumentosCajaPorRendir")]
        public IActionResult ObtenerDocumentosCajaPorRendir(List<int> listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CajaPorRendirService(unitOfWork);
                return Ok(servicio.ObtenerDocumentosCajaPorRendir(listaEnteros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
