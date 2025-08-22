using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: BeneficioAlumnoPEspecificoController
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión de T_BeneficioAlumnoPEspecifico
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class BeneficioAlumnoPEspecificoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public BeneficioAlumnoPEspecificoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta beneficios por medio de OportunidadCodigoMatriculaDTO
        /// </summary>
        /// <param name="oportunidadVerificada"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarBeneficios([FromBody] OportunidadCodigoMatriculaDTO oportunidadVerificada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IBeneficioAlumnoPEspecificoService servicioInsertarBeneficios = new BeneficioAlumnoPEspecificoService(unitOfWork);
                var entidad = servicioInsertarBeneficios.InsertarBeneficios(oportunidadVerificada);
                return Ok(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
