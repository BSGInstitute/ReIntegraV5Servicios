using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: PgeneralAsubPgeneralVersionProgramaController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/07/2023
    /// <summary>
    /// Gestion de PgeneralAsubPgeneralVersionPrograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PgeneralAsubPgeneralVersionProgramaController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public PgeneralAsubPgeneralVersionProgramaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
