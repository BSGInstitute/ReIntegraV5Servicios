using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionCoordinadores
	/// Autor: Jonathan Caipo
	/// Fecha: 04/11/2022    
	/// <summary>
	/// Controlador de Configuracion de Coordinadores
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionCoordinadoresController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ConfiguracionCoordinadoresController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 04/11/2022        
		/// Versión: 1.0
		/// <summary>
		/// Obtiene combos de Configuracion de Coordinadores
		/// </summary>
		/// <returns> Lista de Personal : List<FiltroDTO>/returns>
		/// <returns> Lista de CentroCosto : List<CentroCostoPadreCentroCostoIndividualDTO>/returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCombosConfiguracionCoordinadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalService servicioPersonal = new PersonalService(unitOfWork);
                CentroCostoService servicioCentroCosto = new CentroCostoService(unitOfWork);
                EstadoMatriculaService servicioEstadoMatricula = new EstadoMatriculaService(unitOfWork);
                MatriculaCabeceraService servicioMatriculaCabecera = new MatriculaCabeceraService(unitOfWork);

                var personal = servicioPersonal.ObtenerCoordinadoresParaFiltro();
                var centroCosto = servicioCentroCosto.ObtenerCentroCostoPadreCentroCostoIndividual();
                var estado = servicioEstadoMatricula.ObtenerTodoFiltroConfiguracionCoordinadora();
                var subestado = servicioMatriculaCabecera.ObtenerSubEstadoMatriculaConfiguracionCoordinadora();

                return Ok(new { Personal = personal, CentroCosto = centroCosto, EstadoMatricula = estado, SubEstadoMatricula = subestado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 04/11/2022         
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Informacion de Configuracion de Coordinadores
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCentroCostoCoordinadorDTO>/returns>
		/// <returns> Lista de CentroCosto sin Asignacion : List<ConfiguracionCentroCostoCoordinadorDTO>/returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionCoordinadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionAsignacionCoordinadorOportunidadOperacionService servicioCoordinadora = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(unitOfWork);
                var configuracion = servicioCoordinadora.ObtenerConfiguracionCoordinadores();
                var agrupado = configuracion.GroupBy(x => new { x.IdPersonal, x.Personal }).Select(g => new ConfiguracionCoordinadorPorPersonalDTO
                {
                    IdPersonal = g.Key.IdPersonal.Value,
                    Personal = g.Key.Personal,
                    DetalleCentroCosto = g.GroupBy(y => new { y.IdCentroCosto, y.CentroCosto }).Select(y => new ConfiguracionCoordinadorPorPersonalDetalleCentroCosto
                    {
                        IdCentroCosto = y.Key.IdCentroCosto,
                        CentroCosto = y.Key.CentroCosto
                    }).ToList(),
                    DetalleEstadoMatricula = g.GroupBy(y => new { y.IdEstadoMatricula, y.EstadoMatricula }).Select(y => new ConfiguracionCoordinadorPorPersonalDetalleEstadoMatricula
                    {
                        IdEstadoMatricula = y.Key.IdEstadoMatricula,
                        EstadoMatricula = y.Key.EstadoMatricula
                    }).ToList(),
                    DetalleSubEstadoMatricula = g.GroupBy(y => new { y.IdSubEstadoMatricula, y.SubEstadoMatricula }).Select(y => new ConfiguracionCoordinadorPorPersonalDetalleSubEstadoMatricula
                    {
                        IdSubEstadoMatricula = y.Key.IdSubEstadoMatricula,
                        SubEstadoMatricula = y.Key.SubEstadoMatricula
                    }).ToList()

                }).ToList();
                var centroCostoSinAsignacion = servicioCoordinadora.ObtenerCentroCostoSigAsignacion();
                return Ok(new { CentroCostoAsignado = agrupado, CentroCostoNoAsignado = centroCostoSinAsignacion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 04/11/2022         
		/// Versión: 1.0
		/// <summary>
		/// Actualizar Configuracion
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCoordinadorDTO>/returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConfiguracion([FromBody] List<ConfiguracionCoordinadorDTO> configuracionCoordinador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionAsignacionCoordinadorOportunidadOperacionService servicioConfiguracionCoordinadora = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(unitOfWork);

                foreach (var configuracion in configuracionCoordinador)
                {
                    foreach (var personal in configuracion.ListaPersonal)
                    {
                        var listaEliminar = servicioConfiguracionCoordinadora.ObtenerPorIdPersonal(personal).ToList();

                        foreach (var conf in listaEliminar)
                        {
                            servicioConfiguracionCoordinadora.Delete(conf.Id, configuracion.Usuario);
                        }
                    }
                }
                //ConfiguracionAsignacionCoordinadorOportunidadOperacionService configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(unitOfWork);
                var estado = true;
                if (!servicioConfiguracionCoordinadora.InsertarActualizarConfiguracionCoordinador(configuracionCoordinador))
                {
                    estado = false;
                }
                return Ok(estado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 05/11/2021        
		/// Versión: 1.0
		/// <summary>
		/// Insertar Configuracion
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCoordinadorDTO>/returns>		
		[Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarConfiguracion([FromBody] List<ConfiguracionCoordinadorDTO> configuracionCoordinadorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionAsignacionCoordinadorOportunidadOperacionService configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(unitOfWork);
                var estado = true;
                if (!configuracionCoordinador.InsertarActualizarConfiguracionCoordinador(configuracionCoordinadorDTO))
                {
                    estado = false;
                }
                return Ok(estado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 07/11/2021        
		/// Versión: 1.0
		/// <summary>
		/// Eliminar Configuracion
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCoordinadorDTO>/returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracion([FromBody] AprobarMaterialVersionDTO configuracionEliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionAsignacionCoordinadorOportunidadOperacionService servicioConfiguracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(unitOfWork);
                bool estado = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    var configuracionDetalle = servicioConfiguracionCoordinador.ObtenerPorIdPersonal(configuracionEliminar.Id);
                    if (configuracionDetalle != null)
                    {
                        foreach (var detalle in configuracionDetalle)
                        {
                            estado = servicioConfiguracionCoordinador.Delete(detalle.Id, configuracionEliminar.NombreUsuario);
                        }
                    }
                    //estado = _repConfiguracionCoordinador.Delete(ConfiguracionEliminar.Id, ConfiguracionEliminar.NombreUsuario);
                    scope.Complete();
                    return Ok(estado);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
