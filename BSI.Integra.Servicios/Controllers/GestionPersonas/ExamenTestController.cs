using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Cors;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using DocumentFormat.OpenXml.Office2010.Excel;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;


namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: ExamenTestController
    /// Autor: Victor Hinojosa
    /// Fecha: 23/10/2024
    /// <summary>
    /// Gestión de ExamentTest
    /// Interactura con la tabla ''
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class ExamenTestController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IExamenTestService _examenTestService;
        private ITokenManager _tokenManager;
        public ExamenTestController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _examenTestService = new ExamenTestService(unitOfWork);
            _tokenManager = tokenManager;
            this.unitOfWork = unitOfWork;
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 24/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de evaluaciones
        /// </summary>
        /// <returns> List<Obtener> </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener()
        {
            try
            {
                var listaEvaluacion = _examenTestService.Obtener();
                return Ok(listaEvaluacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 24/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de  formula , sexo y evaluacionCategoria
        /// </summary>
        /// <returns> List<ObtenerCombosExamenTest> </returns>
        [HttpGet("ObtenerCombosExamenTest")]
        public IActionResult ObtenerCombosExamenTest()
        {
            try
            {
                var resultado = _examenTestService.ObtenerCombosExamenTest();

                return Ok(new { ListaFormula = resultado.Item1, listaSexo = resultado.Item2, EvaluacionCategoria = resultado.Item3 });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 25/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de Examenes , grupo y componentes
        /// </summary>
        /// <returns> List<ObtenerEvaluacionEditar> </returns>
        [HttpGet("ObtenerEvaluacionEditar")]
        public IActionResult ObtenerEvaluacionEditar(int IdEvaluacion)
        {
            try
            {
                var resultado = _examenTestService.ObtenerEvaluacionEditar(IdEvaluacion);

                return Ok(new
                {
                    ListaExamenes = resultado.Item1,
                    ListaGrupo = resultado.Item2,
                    ListaComponentes = resultado.Item3
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ObtenerCentilesEvaluacion")]
        public IActionResult ObtenerCentilesEvaluacion(int idExamenTest)
        {
            try
            {
                var listaCentiles = _examenTestService.ObtenerCentilEvaluacion(idExamenTest);
                return Ok(listaCentiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
