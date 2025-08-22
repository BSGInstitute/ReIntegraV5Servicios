using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RegistrarOportunidadController
    /// Autor: Jonathan Caipo
    /// Fecha: 07/10/2022
    /// <summary>
    /// Gestión del registro de oportunidades
    /// </summary>
    [Route("api/RegistrarOportunidad")]
    [EnableCors("CorsVista")]
    public class RegistrarOportunidadController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        public RegistrarOportunidadController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el registro filtrado de las oportunidades
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerDatosFiltroRegistrarOportunidad(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisService servicioPais = new PaisService(_unitOfWork);
                CiudadService servicioCiudad = new CiudadService(_unitOfWork);
                TipoDatoService servicioTipoDato = new TipoDatoService(_unitOfWork);
                FaseOportunidadService servicioFaseOportunidad = new FaseOportunidadService(_unitOfWork);
                CategoriaOrigenService servicioCategoriaOrigen = new CategoriaOrigenService(_unitOfWork);
                OrigenService servicioOrigen = new OrigenService(_unitOfWork);
                CargoService servicioCargo = new CargoService(_unitOfWork);
                AreaFormacionService servicioAreaFormacion = new AreaFormacionService(_unitOfWork);
                AreaTrabajoService servicioAreaTrabajo = new AreaTrabajoService(_unitOfWork);
                IndustriaService servicioIndustria = new IndustriaService(_unitOfWork);
                PersonalService servicioPersonal = new PersonalService(_unitOfWork);

                var area = servicioPersonal.ObtenerPersonalPorId(idPersonal);
                string variable = "";
                if (area.AreaAbrev != null && area != null)
                {
                    variable = area.AreaAbrev.ToString();
                }
                if (idPersonal == 5447 || idPersonal == 6114 || idPersonal == 259 || idPersonal == 4094 || idPersonal == 5891 || idPersonal == 5268 || idPersonal == 6089 || idPersonal == 5305)//5447:Maria Idme,6114_Karyme Muniz,259:Edita Alcalde,4094:Jackeline Villodas,5891:Giovani Montejo,5268:Demnisse Santillan,6089:Jessica Ochoa,5305:Jhoselin davila
                {
                    variable = "MKT";
                }

                ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
                {
                    Paises = servicioPais.ObtenerPaisZonaHoraria().ToList(),
                    Ciudades = servicioCiudad.ObtenerCombo().ToList(),
                    TipoDatoChats = servicioTipoDato.ObtenerCombo().ToList(),
                    FaseOportunidades = servicioFaseOportunidad.ObtenerCombo().ToList(),
                    CategoriaOrigenes = servicioOrigen.ObtenerOrigeneParaRegistrarOportunidad(variable),
                    Cargos = servicioCargo.ObtenerCombo().ToList(),
                    AreasFormacion = servicioAreaFormacion.ObtenerCombo().ToList(),
                    AreasTrabajo = servicioAreaTrabajo.ObtenerCombo().ToList(),
                    Industrias = servicioIndustria.ObtenerCombo().ToList()
                };

                return Ok(ContactoOportunidad);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 10/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las oportunidades regristradas
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>RegistrarOportunidadFitroGrillaDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidad([FromBody] RegistrarOportunidadFitroGrillaDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IOportunidadService servicioOportunidad = new OportunidadService(_unitOfWork);
                var oportunidadManual = servicioOportunidad.ObtenerPorFiltroRegistrarOportunidad(obj.filtro, obj.paginador);
                return Ok(oportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/03/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las oportunidades regristradas
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>RegistrarOportunidadFitroGrillaDTO</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidadV2([FromBody] RegistrarOportunidadFitroGrillaDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IOportunidadService servicioOportunidad = new OportunidadService(_unitOfWork);
                var resultado = servicioOportunidad.ObtenerPorFiltroRegistrarOportunidadV2(obj.filtro, obj.paginador, _tokenManager.UserName, _tokenManager.AreaTrabajo, _tokenManager.TipoPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
