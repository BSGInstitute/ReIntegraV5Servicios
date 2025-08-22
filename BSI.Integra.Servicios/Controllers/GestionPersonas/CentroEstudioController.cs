using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: CentroEstudioController
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 08/04/2024
    /// <summary>
    /// Gestión de Centro de Estudio para el Modulo (M) Centro Estudio
    /// Interactua con la tabla 'gp.T_CentroEstudio'
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class CentroEstudioController : ControllerBase
    {
        private ICentroEstudioService _centroEstudioService;
        private IPaisService _paisService;
        private ICiudadService _ciudadService;
        private ITipoCentroEstudioService _tipoCentroEstudioService;
        private ITokenManager _tokenManager;

        public CentroEstudioController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _centroEstudioService = new CentroEstudioService(unitOfWork);
            _paisService = new PaisService(unitOfWork);
            _ciudadService = new CiudadService(unitOfWork);
            _tipoCentroEstudioService = new TipoCentroEstudioService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Método HTTP: GET
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los centros de estudio.
        /// </summary>
        /// <returns>Retorna 200 con la lista de objetos o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<CentroEstudioDTO>> Obtener()
        {
            return Ok(_centroEstudioService.Obtener());
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            var listaPais = _paisService.ObtenerPaisCombo();
            var listaCiudad = _ciudadService.ObtenerCombo();
            var listaTipoCentroEstudio = _tipoCentroEstudioService.Obtener();
            return Ok(new
            {
                ListaPais = listaPais,
                ListaCiudad = listaCiudad,
                ListaTipoCentroEstudio = listaTipoCentroEstudio
            });
        }

        /// Método HTTP: POST
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla.
        /// </summary>
        /// <param name="dto">Entidad CentroEstudioDTO a insertar</param>
        /// <returns>Retorna 200 y el objeto ingresado o 400 y un mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<CentroEstudioDTO> Insertar([FromBody] CentroEstudioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_centroEstudioService.Insertar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: PUT
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="dto">Entidad CentroEstudioDTO a actualizar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult<CentroEstudioDTO> Actualizar([FromBody] CentroEstudioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_centroEstudioService.Actualizar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: DELETE
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación básica a la tabla
        /// </summary>
        /// <param name="idCentroEstudio">Id del centro de estudio a eliminar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]/{idCentroEstudio}")]
        [HttpDelete]
        public ActionResult<bool> Eliminar(int idCentroEstudio)
        {
            return Ok(_centroEstudioService.Eliminar(idCentroEstudio, _tokenManager.UserName));
        }

    }
}
