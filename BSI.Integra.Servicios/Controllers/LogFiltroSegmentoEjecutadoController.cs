using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: LogFiltroSegmentoEjecutadoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/07/2022
    /// <summary>
    /// Gestión de Informacion de Actividades para Agenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LogFiltroSegmentoEjecutadoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public LogFiltroSegmentoEjecutadoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [Route("[action]")]
        [HttpPost]

        public IActionResult ObtenerPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                var res = new LogFiltroSegmentoEjecutadoService(unitOfWork);
                var respuesta = res.ObtenerPorIdFiltroSegmento(idFiltroSegmento);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}