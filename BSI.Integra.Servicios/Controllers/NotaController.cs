using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: NotaController
    /// Autor: Gilmer uispe.
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de Nota
    /// </summary>
    [Route("api/Operaciones/Nota")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class NotaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public NotaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// obtiene el listado de notas asociados al idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de matricula cabecera </param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ListadoNotaPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                var notaService = new NotaService(unitOfWork);
                var evaluacionEscalaCalificacionService = new EvaluacionEscalaCalificacionService(unitOfWork);
                var listadoNotasPorMatricula = notaService.ListadoNotaPorMatriculaCabecera(idMatriculaCabecera);
                foreach (var item in listadoNotasPorMatricula)
                {
                    item.NombreEvaluacion = item.NombreEvaluacion == null ? "" : item.NombreEvaluacion;
                    if (item.NombreEvaluacion.ToUpper().Contains("ASISTENCIA"))
                    {
                        var escalaCalificacion = evaluacionEscalaCalificacionService.ObtenerEscalaPorPEspecificoPresencial(item.IdPEspecifico);
                        item.Nota = item.Nota * Convert.ToInt32(escalaCalificacion?.EscalaCalificacion ?? 0);
                    }
                    if (item.IdEvaluacion == null)
                    {
                        item.Nota = null;
                    }
                }
                return Ok(listadoNotasPorMatricula);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
