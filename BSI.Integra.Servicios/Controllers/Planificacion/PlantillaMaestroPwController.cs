using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PlantillaMaestroPwController : ControllerBase
    {
        private IPlantillaMaestroPwService _plantillaMaestroPwService;
        private IUnitOfWork _unitOfWork;
        public PlantillaMaestroPwController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _plantillaMaestroPwService = new PlantillaMaestroPwService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todo DocumentoSeccion para Editar
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns> List<DocumentoSeccionPwFiltroAgrupadoDTO> </returns>
        [Route("[Action]/{idPlantilla}")]
        [HttpGet]
        public IActionResult ObtenerPlantillaSeccionMaestraPorIdPlantilla(int idPlantilla)
        {
            try
            {
                return Ok(_plantillaMaestroPwService.ObtenerPlantillaSeccionMaestraPorIdPlantilla(idPlantilla));
            }
            catch
            {
                throw;
            }
        }
    }
}
