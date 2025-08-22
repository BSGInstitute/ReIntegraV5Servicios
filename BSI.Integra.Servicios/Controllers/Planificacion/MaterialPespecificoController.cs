using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: MaterialPespecificoController
    /// Autor: Jonathan Caipo
    /// Fecha: 03/07/2023
    /// <summary>
    /// Gestion de Material de PEspecifico
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MaterialPespecificoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMaterialPespecificoService _materialPespecificoService;
        private IAsociarTagProgramaService _asociarProgramaTagService;
        private ITokenManager _tokenManager;
        public MaterialPespecificoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _materialPespecificoService = new MaterialPespecificoService(_unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista MaterialPespecificoDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombos()
        {
            try
            {
                return Ok(_materialPespecificoService.ObtenerCombos());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los materiales por grupo de programa especifico a revisar
        /// </summary>
        /// <param name="dto">Objeto de clase FiltroMaterialDTO</param>
        /// <returns>Lista de objetos de clase ResultadoMaterialPEspecificoDetalleDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult ObtenerMaterialesPorProgramaEspecificoGrupoRevisar([FromBody] FiltroMaterialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_materialPespecificoService.ObtenerMateriales(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la tabla de T_MaterialPespecificoDetalle el campo de IdMaterialEstado
        /// </summary>
        /// <param name="MaterialPespecificoDetalle">Objeto de clase AprobarMaterialVersionDTO</param>
        /// <returns> Bool </returns>
        [Route("[Action]/{idMaterialPespecificoDetalle}")]
        [Authorize]
        [HttpPut]
        public IActionResult AprobarMaterialVersion(int idMaterialPespecificoDetalle)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_materialPespecificoService.AprobarMaterialVersion(idMaterialPespecificoDetalle, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la tabla de T_MaterialPespecificoDetalle el campo de IdMaterialEstado
        /// </summary>
        /// <param name="MaterialPespecificoDetalle">Objeto de clase AprobarMaterialVersionDTO</param>
        /// <returns>Bool</returns>
        [Route("[Action]/{idMaterialPespecificoDetalle}")]
        [HttpPut]
        public IActionResult DesaprobarMaterialVersion(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_materialPespecificoService.DesaprobarMaterialVersion(id, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Edmundo A. Llaza M.
        /// Fecha: 24/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene MaterialPEspecifico por Id
        /// </summary>
        /// <returns> ListMaterialPespecifico </returns>
        //[AllowAnonymous]

        [HttpGet("[Action]/{idPEspecifico}")]
        public IActionResult ObtenerPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                var resultado = _materialPespecificoService.ObtenerPorIdPEspecifico(idPEspecifico);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edmundo A. Llaza M.
        /// Fecha: 24/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene MaterialPEspecifico por Id
        /// </summary>
        /// <returns> ListMaterialPespecifico </returns>  
        [HttpPost("[Action]")]
        public IActionResult Insertar(MaterialPespecificoDTO materialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialPespecificoService.Insertar(materialPespecifico, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo: Post
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-02
        /// <summary>
        /// Obtiene el combo para material
        /// </summary>
        /// <returns>ComboMaterialPespecificoDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerComboMaterial()
        {
            try
            {
                var combo = _materialPespecificoService.ObtenerComboMaterial();
                return Ok(combo);
            }
            catch { return BadRequest(); }
        }
        /// Tipo: HttpPut
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-02
        /// <summary>
        /// Realiza actulizacion de materialpespecifico
        /// </summary>
        /// <param name="materialPespecificoDTO"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPut]
        public IActionResult Actualizar(MaterialPespecificoDTO materialPespecificoDTO)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_materialPespecificoService.Actualizar(materialPespecificoDTO, registroClaimToken.UserName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-03-08
        /// <summary>
        /// elimina de material pespecifico
        /// </summary>
        /// <param name="id">es el campo id del usuario</param>
        /// <returns>bool</returns>
        [Route("[Action]/{id}")]
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_materialPespecificoService.Eliminar(id, registroClaimToken.UserName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo: POST
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-04
        /// <summary>
        /// Obtendra los materiales que estan con estado aprobado, todos los materiales del grupo de edicion deben estar aprobados
        /// </summary>
        /// <param name="filtroMaterial"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ObtenerMaterialesGestionEnvio(FiltroMaterialDTO filtroMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materiales =await _materialPespecificoService.ObtenerMaterialesGestionEnvioAsync(filtroMaterial);
                return Ok(materiales);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-05
        /// <summary>
        /// Envia correo de confirmacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult NotificarMaterialVersionAlumnoPorCorreo(List<int> idMaterialPEspecificoDetalle)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_materialPespecificoService.NotificarMaterialVersionAlumnoPorCorreo(idMaterialPEspecificoDetalle, registroClaimToken.UserName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-13
        /// <summary>
        /// Notifica a proveedor con impresion de material
        /// </summary>F
        /// <param name="idMaterialPespecifico"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [Authorize]
        [HttpPost("[Action]/{idMaterialPespecifico}")]
        public IActionResult NotificarMaterialVersionAlumnoImpresoPorCorreoAProveedor(int idMaterialPespecifico)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_materialPespecificoService.NotificarMaterialVersionAlumnoImpresoPorCorreoAProveedor(idMaterialPespecifico, registroClaimToken.UserName));
            }
            catch (Exception e)
            {
                throw new BadRequestException($"No se encontro el id {e.Message}");
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene de T_MaterialPespecificoDetalle filtrado por mateial pespecifico version y estado
        /// </summary>
        /// <param name="idMaterialPEspecifico"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [HttpGet("[Action]/{idMaterialPEspecifico}")]
        public IActionResult ObtenerMaterialesAlumnoDigital(int idMaterialPEspecifico)
        {
            try
            {
                var materialesAlumno = _materialPespecificoService.ObtenerMaterialesAlumnoDigital(idMaterialPEspecifico);
                return Ok(materialesAlumno);
            }
            catch (Exception ex) { throw new BadRequestException($"No se encontro el id{ex.Message}"); }
        }
        /// <summary>
        /// Obtiene valores de T_Fur por el campo PEspecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        [HttpGet("[Action]/{idPEspecifico}")]
        public IActionResult ObtenerFursAsociadosPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                var furAsociado = _materialPespecificoService.ObtenerFursAsociadosPorIdPEspecifico(idPEspecifico);
                return Ok(furAsociado);
            }
            catch(Exception e) { throw new BadRequestException($"Nose encontro el id{e.Message}"); }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Actualiza T_Fur y T_MaterialPEspecificoDetalle
        /// </summary>
        /// <param name="furMaterial"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult AsociarActualizarFur(AsociarActualizarFurMaterialVersionDTO furMaterial)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var furActual = _materialPespecificoService.AsociarActualizarFur(furMaterial, registroClaimToken.UserName);
                return Ok(furActual);
            }
            catch(Exception e) { throw new BadRequestException($"Nose encontro {e.Message}"); }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene Fur por MaterialPEspecifico
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [HttpGet("[Action]/{idMaterialPEspecificoDetalle}")]
        public IActionResult ObtenerFurAsociadoPorIdPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                var retorno = _materialPespecificoService.ObtenerFurAsociadoPorIdPEspecificoDetalle(idMaterialPEspecificoDetalle);
                return Ok(retorno);
            }
            catch (Exception e) { throw new BadRequestException($"Nose encontro el id"); }
        }



        /// Autor: Margioryr Ramirez
        /// Fecha: 2023-11-03
        /// <summary>
        /// </summary>
        /// <returns></returns>

        [HttpPost("[Action]")]
        public async Task<IActionResult>ObtenerCriteriosMaterialesProgramaEspecifico(FiltroMaterialDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materiales = await _materialPespecificoService.ObtenerCriteriosMaterialesProgramaEspecifico(Filtro);
                return Ok(materiales);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult NotificarListaMaterialVersionAlumnoPorCorreo([FromBody]List<int> listaIdMaterialPEspecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try

            {
          
                var materialVersion = _materialPespecificoService.NotificarListaMaterialVersionAlumnoPorCorreo(listaIdMaterialPEspecificoDetalle, _tokenManager.UserName);
                return Ok(materialVersion);
            }
            catch (Exception e) { throw new BadRequestException($"Nose encontro {e.Message}"); }
        }

    }





}
