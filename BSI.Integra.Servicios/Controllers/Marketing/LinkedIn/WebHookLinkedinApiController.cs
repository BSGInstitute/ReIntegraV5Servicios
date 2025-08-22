using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;
using System.Text;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;

namespace BSI.Integra.Servicios.Controllers.Marketing.LinkedIn
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WebHookLinkedinApiController : ControllerBase
    {
        



    }
}