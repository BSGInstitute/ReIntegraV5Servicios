using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ChatController
    /// Autor: Jonathan Caipo
    /// Fecha: 17/10/2022
    /// <summary>
    /// Gestión del chat
    /// </summary>
    [Route("api/ChatController")]
    public class ChatController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ChatController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos filtrados de ContactoOportunidad
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosComboContactoOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nombre = "Chat";
                PaisService servicioPais = new PaisService(unitOfWork);
                CiudadService servicioCiudad = new CiudadService(unitOfWork);
                TipoDatoService servicioTipoDato = new TipoDatoService(unitOfWork);
                FaseOportunidadService servicioFaseOportunidad = new FaseOportunidadService(unitOfWork);
                CategoriaOrigenService servicioCategoriaOrigen = new CategoriaOrigenService(unitOfWork);
                CargoService servicioCargo = new CargoService(unitOfWork);
                AreaFormacionService servicioAreaFormacion = new AreaFormacionService(unitOfWork);
                AreaTrabajoService servicioAreaTrabajo = new AreaTrabajoService(unitOfWork);
                IndustriaService servicioIndustria = new IndustriaService(unitOfWork);
                OrigenService servicioOrigen = new OrigenService(unitOfWork);

                ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
                {
                    Paises = servicioPais.ObtenerPaisZonaHoraria().ToList(),
                    Ciudades = servicioCiudad.ObtenerCombo().ToList(),
                    TipoDatoChats = servicioTipoDato.CargarTipoDatoChat().ToList(),
                    FaseOportunidades = servicioFaseOportunidad.ObtenerCombo().ToList(),
                    CategoriaOrigenes = servicioCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombre),
                    Cargos = servicioCargo.ObtenerCombo().ToList(),
                    AreasFormacion = servicioAreaFormacion.ObtenerCombo().ToList(),
                    AreasTrabajo = servicioAreaTrabajo.ObtenerCombo().ToList(),
                    Industrias = servicioIndustria.ObtenerCombo().ToList(),
                    Origenes = servicioOrigen.ObtenerOrigenChat(nombre)
                };
                return Ok(ContactoOportunidad);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 19/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos contacto oportunidad
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosFiltroContactoOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nombre = "Chat";
                PaisService servicioPais = new PaisService(unitOfWork);
                CiudadService servicioCiudad = new CiudadService(unitOfWork);
                TipoDatoService servicioTipoDato = new TipoDatoService(unitOfWork);
                FaseOportunidadService servicioFaseOportunidad = new FaseOportunidadService(unitOfWork);
                CategoriaOrigenService servicioCategoriaOrigen = new CategoriaOrigenService(unitOfWork);
                CargoService servicioCargo = new CargoService(unitOfWork);
                AreaFormacionService servicioAreaFormacion = new AreaFormacionService(unitOfWork);
                AreaTrabajoService servicioAreaTrabajo = new AreaTrabajoService(unitOfWork);
                IndustriaService servicioIndustria = new IndustriaService(unitOfWork);
                OrigenService servicioOrigen = new OrigenService(unitOfWork);

                ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
                {
                    Paises = servicioPais.ObtenerPaisZonaHoraria().ToList(),
                    Ciudades = servicioCiudad.ObtenerCombo().ToList(),
                    TipoDatoChats = servicioTipoDato.CargarTipoDatoChat().ToList(),
                    FaseOportunidades = servicioFaseOportunidad.ObtenerCombo().ToList(),
                    CategoriaOrigenes = servicioCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombre),
                    Cargos = servicioCargo.ObtenerCombo().ToList(),
                    AreasFormacion = servicioAreaFormacion.ObtenerCombo().ToList(),
                    AreasTrabajo = servicioAreaTrabajo.ObtenerCombo().ToList(),
                    Industrias = servicioIndustria.ObtenerCombo().ToList(),
                    Origenes = servicioOrigen.ObtenerOrigenChat(nombre)
                };
                return Ok(ContactoOportunidad);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
