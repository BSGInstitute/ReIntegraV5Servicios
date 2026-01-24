using Azure.Storage.Blobs;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: OportunidadMasivaController
    /// Autor: Margiory Ramirez.
    /// Fecha: 30/01/2025
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class OportunidadMasivaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public OportunidadMasivaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            var servicio = new OportunidadMasivaService(unitOfWork);

            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se seleccionó un archivo.");
            string urlArchivo = await servicio.SubirArchivoAsync(archivo);

            if (urlArchivo == null)
                return StatusCode(500, "Error al subir el archivo a Azure.");

            string nombreArchivo = Path.GetFileName(archivo.FileName);

            string newId = servicio.InsertarArchivo(nombreArchivo, "OportunidadMasiva");


            if (string.IsNullOrEmpty(newId))
                return StatusCode(500, "El archivo se subió a Azure pero no se guardó en la BD.");

            return Ok(new { mensaje = "✅ Archivo subido y registrado correctamente", url = urlArchivo, id = newId });
        }




        [HttpPost("DescargarArchivo")]
        public IActionResult DescargarArchivo([FromBody] ArchivoMasivoDTO request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.NombreArchivo))
                    return BadRequest(new { error = "El nombre del archivo es requerido." });

                string nombreArchivoLimpio = Uri.EscapeDataString(request.NombreArchivo.Trim());

                string baseUrl = "https://repositorioweb.blob.core.windows.net/oportunidades-masivas/";

                string sasToken = "sp=rwd&st=2025-01-30T16:21:32Z&se=2025-05-01T02:21:32Z&spr=https&sv=2022-11-02&sr=c&sig=J0eaByE3p9aEAyKDNy4HtmeNzUnkWnn5ZQ5bY3i8hUY%3D";

                string urlArchivo = $"{baseUrl}{nombreArchivoLimpio}?{sasToken}";

                Console.WriteLine($"🔍 URL Generada con SAS: {urlArchivo}");

                return Ok(new { mensaje = "✅ Archivo encontrado", url = urlArchivo });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Error en el servidor: {ex.Message}" });
            }
        }



        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerArchivosOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadService(unitOfWork);


                var respuesta = servicio.ObtenerArchivosOportunidad();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarOportunidadedMasiva([FromForm] IFormFile file, [FromForm] string usuario)
        {
            try
            {
                var servicio = new OportunidadMasivaService(unitOfWork);
                var resultado = servicio.ProcesarOportunidadMasiva(file, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMasivoHistorial(HistorialOportunidaMasivodDTO datos)
        {
            try
            {
                var servicio = new OportunidadMasivaService(unitOfWork);
                servicio.InsertarHistorialOportunidad(datos.IdOportunidad, datos.Usuario);
                return Ok(new { message = "Historial de oportunidad insertado correctamente." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerOportunidadesMasivas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadMasivaService(unitOfWork);

                var respuesta = servicio.ObtenerOportunidadesMasivas();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
