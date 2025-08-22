using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TipoDatoController
    /// Autor: Margiory Ramkirez.
    /// Fecha: 20/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class OtroMovimientoCajaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public OtroMovimientoCajaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        /// 

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarOtroMovimientoCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serpoOtroMovimientoCaja = new OtroMovimientoCajaService(unitOfWork);
                var OtroMovimientoCajaes = serpoOtroMovimientoCaja.ObtenerListaOtroMovimientoCaja();
                return Json(new { Records = OtroMovimientoCajaes });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TMoneds
        /// </summary>
        /// <returns> List<TMonedsa> </returns>
        

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaMoneda()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var serpoMoneda = new MonedaService(unitOfWork);
                var listaMoneda = serpoMoneda.ObtenerFiltroMoneda();
                return Ok(listaMoneda );
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TTipoMovimientoCaja
        /// </summary>
        /// <returns> List<TTipoMovimientoCaja> </returns>


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoMovimientoCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var serTipoMovimientoCaja = new TipoMovimientoCajaService(unitOfWork);
                var lista = serTipoMovimientoCaja.ObtenerListaTipoMovimientoCaja();
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TSubTipoMovimientoCaja
        /// </summary>
        /// <returns> List<TSubTipoMovimientoCaja> </returns>



        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSubTipoMovimientoCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var  serSubTipoMovimientoCaja = new SubTipoMovimientoCajaService(unitOfWork);
                var lista = serSubTipoMovimientoCaja.ObtenerListaSubTipoMovimientoCaja();
                return Ok(lista );
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TFormaPago
        /// </summary>
        /// <returns> List<TSubTipoMovimientoCaja> </returns>

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaFormaPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var sereFormaPago = new FormaPagoService(unitOfWork);
                var lista = sereFormaPago.ObtenerListaFormaPago();
                return Ok (lista );
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TCuentaCorriente
        /// </summary>
        /// <returns> List<TSubTipoMovimientoCaja> </returns>


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCuentaCorriente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var serCuentaCorriente = new CuentaCorrienteService(unitOfWork);
                var lista = serCuentaCorriente.ObtenerCuentaCorrienteConEntidad();
                return Ok( lista );
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TAlumno
        /// </summary>
        /// <returns> List<TSubTipoMovimientoCaja> </returns>


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAlumnoAutocomplete(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Ok(ListaVacia); ;
                }

                var serAlumno = new AlumnoService(unitOfWork);
                var lista = serAlumno.ObtenerTodoFiltroAutoComplete(NombreParcial);
                return Ok(lista );
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Mostrados en el Combo TCcentroCosto
        /// </summary>
        /// <returns> List<TSubTipoMovimientoCaja> </returns>

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCentroCosto(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Ok(ListaVacia);
                }
                var _repoCentroCosto = new CentroCostoService(unitOfWork);
                var lista = _repoCentroCosto.ObtenerListaCentrosCostoPorNombre(NombreParcial);
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }




        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlanContableAutoComplete(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Ok(ListaVacia);
                }
                var _repoPlanContable = new PlanContableService(unitOfWork);
                var lista = _repoPlanContable.ObtenerPlanContableAutoComplete(NombreParcial);
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarOtroMovimientoCaja([FromBody] OtroMovimientoCajaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OtroMovimientoCajaService(unitOfWork);
                var respuesta = servicio.InsertarOtroMovimientoCaja(ObjetoDTO);
                return Ok(respuesta);



            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarOtroMovimientoCaja([FromBody] OtroMovimientoCajaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OtroMovimientoCajaService(unitOfWork);
                var respuesta = servicio.ActualizarOtroMovimientoCaja(ObjetoDTO);
                return Ok(respuesta);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarOtroMovimientoCaja([FromBody] EliminarDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serOtroMovimientoCaja = new OtroMovimientoCajaService(unitOfWork);

                serOtroMovimientoCaja.Delete(ObjetoDTO.Id, ObjetoDTO.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }






    }
}
