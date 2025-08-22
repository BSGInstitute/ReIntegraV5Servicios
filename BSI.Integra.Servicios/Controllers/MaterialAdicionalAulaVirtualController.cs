using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Servicios.Helpers;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaterialAdicionalAulaVirtualController
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 16/05/2023
    /// <summary>
    /// Gestión de T_MaterialAdicionalAulaVirtual,
    /// T_MaterialAdicionalAulaVirtualPespecifico,
    /// T_MaterialAdicionalAulaVirtualRegistro
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MaterialAdicionalAulaVirtualController: ControllerBase
    {
        private IMaterialAdicionalAulaVirtualService _materialAdicionalAulaVirtual;
        private IUnitOfWork _unitOfWork;
        public MaterialAdicionalAulaVirtualController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _materialAdicionalAulaVirtual = new MaterialAdicionalAulaVirtualService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _materialAdicionalAulaVirtual.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult ObtenerDetalle(int id)
        {
            try
            {
                var resultado = _materialAdicionalAulaVirtual.ObtenerDetalleMaterialAdicional(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] MaterialAdicionalAulaVirtualEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialAdicionalAulaVirtual.InsertarMaterialAdicional(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] MaterialAdicionalAulaVirtualEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialAdicionalAulaVirtual.ActualizarMaterialAdicional(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialAdicionalAulaVirtual.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
