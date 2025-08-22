using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReclamoController
    /// Autor: Gilmer Quispe.
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión de Agenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReclamoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReclamoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Busca reclamo de alumno especifico por el idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de matricula cabecera </param>
        /// <returns> Lista de Objeto DTO : List<ListarReclamosDTO> </returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerReclamosAlumno(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoService = new ReclamoService(unitOfWork);
                var resultado = new { ListaReclamo = reclamoService.ListarReclamosAlumno(idMatriculaCabecera) };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo filtro de reclamos
        /// </summary> 
        /// <returns> Lista de Objeto DTO : List<ComboFiltroDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboOrigenReclamo()
        {
            try
            {
                var origenService = new OrigenService(unitOfWork);
                var origen = new { Origen = origenService.ObtenerCombosOrigen() };
                return Ok(origen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// OBTIENE TODOS LOS RECLAMOS
        /// </summary> 
        /// <returns> Lista de Objeto DTO : List<ComboFiltroDTO> </returns> 
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerReclamos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoService = new ReclamoService(unitOfWork);
                var cmbCE = new { ListaReclamo = reclamoService.ListarReclamos() };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// OBTIENE TODOS LOS RECLAMOS
        /// </summary> 
        /// <returns> Lista de Objeto DTO : List<ComboFiltroDTO> </returns> 
        [Route("[Action]/{idMatricula}")]
        [HttpGet]
        public ActionResult ObtenerReclamosPorAlumno(int idMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoService = new ReclamoService(unitOfWork);
                var cmbCE = new { ListaReclamo = reclamoService.ObtenerReclamosPorAlumno(idMatricula) };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque.
        /// Fecha: 17/10/2023
        /// Versión: 1.0
        /// <summary>
        /// OBTIENE TODOS LOS RECLAMOS
        /// </summary> 
        /// <returns> Lista de Objeto DTO : List<ComboFiltroDTO> </returns> 
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerReclamosAreas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoService = new ReclamoService(unitOfWork);
                var cmbCE = new { ListaReclamo = reclamoService.ListarReclamosAreas() };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque.
        /// Fecha: 17/10/2023
        /// Versión: 1.0
        /// <summary>
        /// OBTIENE TODOS LOS RECLAMOS
        /// </summary> 
        /// <returns> Lista de Objeto DTO : List<ComboFiltroDTO> </returns> 
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoReclamoAlumno()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoService = new ReclamoService(unitOfWork);
                var cmbCE = new { ListaReclamo = reclamoService.ObtenerListaTipoReclamoAlumno() };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 28/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de las vista V_Reclamo filtrado por el CodigoMatricula y el DNI
        /// </summary>
        /// <param name="filtro"> Codigo de matricula </param> 
        /// <returns> List<FiltroMatriculaAlumnoDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnosMatriculadosFiltro([FromBody] FiltroMatriculaAlumnoDTO filtro)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                var resultado = matriculaCabeceraService.ObtenerAlumnosMatriculados(filtro.CodigoMatricula, filtro.DNI);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Jorge Quiñones
        /// Fecha: 31/01/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ConfirmarReclamo([FromBody] FiltroReclamoDTO filtro)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoRepositorio = new ReclamoService(unitOfWork);
                var resultado = reclamoRepositorio.ConfirmarReclamo(filtro.Id, filtro.Usuario,filtro.Comentario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Jorge Quiñones
        /// Fecha: 31/01/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpGet]
        public ActionResult ReclamoSinContacto(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var reclamoRepositorio = new ReclamoService(unitOfWork);
                //ReclamoBO reclamo = new ReclamoBO();
                var result = reclamoRepositorio.ReclamoSinContacto(id, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Joseph LLanque
        /// Fecha: 17/10/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarReclamo([FromBody] ReclamoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var repReclamo = new ReclamoService(unitOfWork);
                var reclamo = new Reclamo();
                reclamo.IdMatriculaCabecera = Json.IdMatricula;
                reclamo.Descripcion = Json.Descripcion;
                reclamo.IdReclamoEstado = Json.IdReclamoEstado;
                reclamo.IdOrigen = Json.IdOrigen;
                reclamo.IdTipoReclamoAlumno = Json.IdTipoReclamoAlumno;
                reclamo.UsuarioCreacion = Json.Usuario;
                reclamo.UsuarioModificacion = Json.Usuario;
                reclamo.FechaCreacion = DateTime.Now;
                reclamo.FechaModificacion = DateTime.Now;
                reclamo.Estado = true;
                var resultado = repReclamo.Add(reclamo);
                return Ok(resultado);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 17/10/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarReclamoAreas([FromBody] ReclamoAreasEntradaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var repReclamo = new ReclamoService(unitOfWork); 
                var reclamo = new ReclamoAreasDTO();
                reclamo.CodigoMatricula = Json.CodigoMatricula;
                reclamo.Descripcion = Json.Descripcion;
                reclamo.IdReclamoEstado = Json.IdReclamoEstado;
                reclamo.IdArea = Json.IdArea;
                reclamo.IdOrigen = Json.IdOrigen;
                reclamo.IdTipoReclamoAlumno = Json.IdTipoReclamoAlumno;
                reclamo.UsuarioCreacion = Json.Usuario;
                reclamo.UsuarioModificacion = Json.Usuario;
                reclamo.FechaCreacion = DateTime.Now;
                reclamo.FechaModificacion = DateTime.Now;
                reclamo.Estado = true;
                repReclamo.InsertarReclamosArea(reclamo);
                return Ok(reclamo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 17/10/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult GenerarReporteReclamo([FromBody] ReclamoFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var repReclamo = new ReclamoService(unitOfWork);
                var reclamo = new ReclamoFiltroDTO();
                reclamo.idMatricula = Json.idMatricula;
                reclamo.FechaInicio = Json.FechaInicio;
                reclamo.FechaFin = Json.FechaFin;
                var cmbCE = repReclamo.GenerarReporteReclamo(reclamo);
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 17/10/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult EnviarReclamo(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoRepositorio = new ReclamoService(unitOfWork);
                var resultado = reclamoRepositorio.EnviarReclamo(Id, Usuario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 17/10/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarReclamo(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reclamoRepositorio = new ReclamoService(unitOfWork);
                var resultado = reclamoRepositorio.EliminarReclamo(Id, Usuario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 19/10/2023
        /// Version: 1.0
        /// <summary>
        /// confirmar reclamo
        /// </summary>
        /// <param name="id"> id registro, name="usuario"> id usuario modificacion </param> 
        /// <returns> boolean </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarReclamo([FromBody] ReclamoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var repReclamo = new ReclamoService(unitOfWork);
                var reclamo = repReclamo.ObtenerPorId(Json.Id);

                reclamo.IdMatriculaCabecera = Json.IdMatricula;
                reclamo.Descripcion = Json.Descripcion;
                reclamo.IdReclamoEstado = Json.IdReclamoEstado;
                reclamo.IdOrigen = Json.IdOrigen;
                reclamo.IdTipoReclamoAlumno = Json.IdTipoReclamoAlumno;
                //reclamo.UsuarioCreacion = Json.Usuario;
                reclamo.UsuarioModificacion = Json.Usuario;
                reclamo.FechaModificacion = DateTime.Now;
                var respuesta = repReclamo.Update(reclamo);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new ReclamoService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 27/04/2023
        /// <summary>
        /// Inserta la fecha de reprogamacion
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        //Enviar Reclamo (Actualiza la tabla)
        [Route("[action]")]
        [HttpPost]
        public ActionResult ResolverReclamoAreas([FromBody] ReclamoSolucionDTO reclamo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var servicio = new ReclamoService(unitOfWork);
                var result = servicio.ResolverReclamoAreas(reclamo);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
