using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Aplicacion.Interaccion.Service.Implementacion;
using BSI.Integra.Aplicacion.Interaccion.Service.Interface;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BSI.Integra.Servicios.Controllers.IntegraDBInteraccion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegistroInicioSesionController : Controller
    {
        private readonly IUnitOfWorkInteraccion unitOfWork;

        public RegistroInicioSesionController(IUnitOfWorkInteraccion unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 2024/05/31
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro Inicio Sesion
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                IRegistroInicioSesionService servicio = new RegistroInicioSesionService(unitOfWork);
                return Ok(servicio.Obtener());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: Post
        /// Autor: Max Mantilla R.
        /// Fecha: 28/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra la interacción de inicio de sesión en integra
        /// </summary>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult RegistrarInicioSesion([FromBody] RegistroInicioSesionLogueoDTO objetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroInicioSesionService(unitOfWork);
                var host = System.Net.Dns.GetHostName();
                var ipEntry = System.Net.Dns.GetHostEntry(host);
                var ipAddress = ipEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();

                var macAddress = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => string.Join("-", nic.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2"))))
                    .FirstOrDefault();
                objetoDTO.DireccionMac = macAddress;
                objetoDTO.IpLocal = ipAddress;
                var Registro = servicio.RegistrarInicioSesion(objetoDTO);

                return Ok(Registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}
