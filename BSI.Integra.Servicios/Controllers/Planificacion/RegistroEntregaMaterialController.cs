using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: RegistroEntregaMaterialController
    /// Autor: Margiory Ramirez
    /// Fecha: 03/07/2023
    /// <summary>
    /// Registro Entrega Material
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegistroEntregaMaterialController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMaterialPespecificoService _materialPespecificoService;
       
        private IMaterialPespecificoDetalleService _materialPespecificoDetalleService;
 
        private ITokenManager _tokenManager;
        public RegistroEntregaMaterialController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _materialPespecificoService = new MaterialPespecificoService(_unitOfWork);
            _materialPespecificoDetalleService = new MaterialPespecificoDetalleService(_unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 06/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista MaterialPespecificoDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombosRegistroMaterial()
        {
            try
            {
                return Ok(_materialPespecificoService.ObtenerCombos());
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 06/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene MaterialPEspecifico por Id
        /// </summary>
        /// <returns> ListMaterialPespecifico </returns>
        //[AllowAnonymous]

        [HttpGet("[Action]/{IdMaterialPEspecificoDetalle}")]
        public IActionResult ObtenerDetalleFurMaterialPEspecifico(int IdMaterialPEspecificoDetalle)
        {
            try
            {
                var resultado = _materialPespecificoDetalleService.ObtenerDetalleFur(IdMaterialPEspecificoDetalle);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-06
        /// <summary>
        /// </summary>
        /// <returns></returns>

        [HttpPost("[Action]")]
        public async Task<IActionResult> AprobarRechazarRegistroEntrega(AprobarRechazarRegistroEntregaMaterialDTO Registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialEntrega = await _materialPespecificoService.AprobarRechazarRegistroEntrega(Registro);
                return Ok(materialEntrega);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }



        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-06
        /// <summary>
        /// </summary>
        /// <returns></returns>

        [HttpPost("[Action]")]
        public IActionResult ActualizarFurRegistroMaterial(FurRegistroMaterialDTO FurMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialfur =  _materialPespecificoService.ActualizarFurRegistroMaterial(FurMaterial);
                return Ok(materialfur);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }


    }



}
