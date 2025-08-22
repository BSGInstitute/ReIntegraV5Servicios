using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: DocumentoAgendaController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 03/08/2022
    /// <summary>
    /// Gestión de DocumentoAgenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class DocumentoAgendaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public DocumentoAgendaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
