using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: IntegraAspNetUserController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión de Usuarios y Accesos al sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class IntegraAspNetUserController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ITokenManager _tokenManager;
        public IntegraAspNetUserController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nombre por usuario
        /// </summary>
        /// <param name="usuario"> Nombre de usuario </param>
        /// <returns> ValorStringDTO </returns>
        [HttpGet("[Action]/{usuario}")]
        public IActionResult ObtenerNombre(string usuario)
        {
            try
            {
                var servicio = new PersonalService(unitOfWork);
                return Ok(servicio.ObtenerPrimerNombreApellidoPaternoPorUserName(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida el acceso por ip
        /// </summary>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult ValidarAcceso([FromBody] IpPublicaDTO objetoDTO)
        {
            IIntegraAspNetUserService servicio = new IntegraAspNetUserService(unitOfWork);
            if (objetoDTO.usuario != "AdminInst")
            {
                servicio.ValidarAcceso(objetoDTO.ipIntegra);
            }
            return Ok(true);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Validar el relogin por usuario
        /// </summary>
        /// <returns> StringDTO </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult ValidarReLogin([FromBody] string ipPublica)
        {
            IIntegraAspNetUserService servicio = new IntegraAspNetUserService(unitOfWork);
            if (ipPublica != "-1" && _tokenManager.UserName != "AdminInst")
            {
                servicio.ValidarAcceso(ipPublica);
            }
            var resultado = servicio.ValidarReLogin(_tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Validar el relogin por usuario
        /// </summary>
        /// <returns> StringDTO </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpGet("[action]")]
        public IActionResult ActualizarReLogin()
        {
            IIntegraAspNetUserService servicio = new IntegraAspNetUserService(unitOfWork);
            var resultado = servicio.ActualizarReLogin(_tokenManager.UserName);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/11/2022
        /// Autor Modificacion: Flavio Rodrigo Mamani Fabian.
        /// Fecha Modificacion: 09/11/2022
        /// Versión: 1.1
        /// <summary>
        /// Obtiene la lista de modulos por usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Retorna Lista de modulos <returns>
        [Route("[Action]/{nombreUsuario}")]
        [HttpGet]
        public ActionResult ObtenerModuloporUsuario(string nombreUsuario)
        {
            try
            {
                var _repRegistrosIntegra = new IntegraAspNetUserService(unitOfWork);
                var _repUsuario = new UsuarioService(unitOfWork);
                var validacion = _repUsuario.ObtenerPorNombreUsuario(nombreUsuario);
                if (string.IsNullOrEmpty(nombreUsuario))
                {
                    return Ok("");
                }
                if (validacion.IdPersonal == 0 || validacion.Equals("[]"))
                {
                    return Ok("");
                }
                var modulo = _repRegistrosIntegra.ObtenerDatosParaModuloAgrupado(nombreUsuario).GroupBy(x => new
                {
                    x.IdGrupo,
                    x.NombreGrupo,
                }).Select(x => new GrupoModuloUsuarioDTO
                {
                    IdGrupo = x.Key.IdGrupo,
                    NombreGrupo = x.Key.NombreGrupo,
                    SubGrupoModulo = x.GroupBy(y => new
                    {
                        y.IdModuloSistemaTipo,
                        y.NombreModuloSistemaTipo,
                    }).Select(y => new SubGrupoModuloUsuarioDTO
                    {
                        IdGrupo = x.Key.IdGrupo,
                        IdModuloSistemaTipo = y.Key.IdModuloSistemaTipo,
                        NombreModuloSistemaTipo = y.Key.NombreModuloSistemaTipo,
                        Modulos = y.Select(z => new ModuloUsuarioDTO
                        {
                            IdGrupo = z.IdGrupo,
                            IdModuloSistemaTipo = z.IdModuloSistemaTipo,
                            IdModulo = z.IdModulo,
                            NombreModulo = z.NombreModulo,
                            URL = z.URL,
                            Etiqueta = z.Etiqueta,
                            Icono = z.Icono,
                        }).ToList()
                    }).ToList()
                });
                return Ok(modulo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
