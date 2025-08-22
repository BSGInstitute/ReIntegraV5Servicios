using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: FichaAlumno
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 18/02/2023
    /// <summary>
    /// Gestión de TFichaAlumno
    /// </summary>
    [Route("api/Comercial/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FichaAlumnoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FichaAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de oportunidades
        /// </summary>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult CrearOportunidadFicha([FromBody] OportunidadFichaDTO dto)
        {
            try
            {
                FichaAlumnoService service = new FichaAlumnoService(unitOfWork);
                AsignacionManualService serviceAsignacionManual = new AsignacionManualService(unitOfWork);
                OportunidadService serviceOportunidad = new OportunidadService(unitOfWork);


                var resultado = service.CrearOportunidadFicha(dto);
                try
                {
                    var resultado_contesto = service.ActualizarCreoOportunidadPredictivo(dto.IdOportunidadRN2, resultado.IdOportunidad);

                    serviceAsignacionManual.EnviarWhatsappPredictivas((int)resultado.IdOportunidad);
                }
                catch (Exception e) { }
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de oportunidades
        /// </summary>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idOportunidadRN2}")]
        public IActionResult ObtenerInformacionAlumnoPorIdOportunidadRN2(int idOportunidadRN2)
        {
            try
            {
                FichaAlumnoService service = new FichaAlumnoService(unitOfWork);
                try
                {
                    var resultado_contesto = service.ActualizarContestoPredictivo(idOportunidadRN2);
                }
                catch (Exception e) { }
                var resultado = service.ObtenerInformacionAlumnoPorIdOportunidadRN2(idOportunidadRN2);

                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de oportunidades
        /// </summary>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombos()
        {
            try
            {
                FichaAlumnoService service = new FichaAlumnoService(unitOfWork);
                var resultado = service.ObtenerCombos();
                return Ok(new
                {
                    Programas = resultado.programas,
                    FasesOportunidad = resultado.fasesOportunidad
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de oportunidades
        /// </summary>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idOportunidadRN2}")]
        public IActionResult ObtenerOportunidadPredictivo(int idOportunidadRN2)
        {
            try
            {
                FichaAlumnoService service = new FichaAlumnoService(unitOfWork);
                var resultado = service.ObtenerOportunidadPredictivo(idOportunidadRN2);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de oportunidades
        /// </summary>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idOportunidadRN2}")]
        public IActionResult ObtenerProgramaGeneralPredictivo(int idOportunidadRN2)
        {
            try
            {
                FichaAlumnoService service = new FichaAlumnoService(unitOfWork);
                var resultado = service.ObtenerProgramaGeneralPredictivo(idOportunidadRN2);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
