using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Aplicacion.Interaccion.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BSI.Integra.Servicios.Controllers.IntegraDBInteraccion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class InteraccionModuloController : ControllerBase
    {
        private readonly IUnitOfWorkInteraccion unitOfWork;
        private ITokenManager _tokenManager;
        public InteraccionModuloController(IUnitOfWorkInteraccion unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }
        /// Tipo Función: Post
        /// Autor: Max Mantilla R.
        /// Fecha: 28/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra la interacción de inicio de sesión en integra
        /// </summary>
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult InteraccionModuloInsertar([FromBody] RegistroInteraccionModuloDTO objetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                var servicio = new InteraccionModuloService(unitOfWork);
                var host = System.Net.Dns.GetHostName();
                var ipEntry = System.Net.Dns.GetHostEntry(host);
                var ipAddress = ipEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();

                var macAddress = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => string.Join("-", nic.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2"))))
                    .FirstOrDefault();
                objetoDTO.IdUsuario = _tokenManager.IdPersonal;
                objetoDTO.Usuario = _tokenManager.UserName;
                objetoDTO.DireccionMac = macAddress;
                objetoDTO.IpLocal = ipAddress;
                var Registro = servicio.RegistrarInteraccionModulo(objetoDTO);

                return Ok(Registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
