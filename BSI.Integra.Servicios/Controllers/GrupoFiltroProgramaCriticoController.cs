using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GrupoFiltroProgramaCriticoController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 07/10/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GrupoFiltroProgramaCriticoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public GrupoFiltroProgramaCriticoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] GrupoFiltroProgramaCritico entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<GrupoFiltroProgramaCritico> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] GrupoFiltroProgramaCritico entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<GrupoFiltroProgramaCritico> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
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
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_GrupoFiltroProgramaCritico para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de los grupos de filtro programa critico
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase DatosPersonalAsesorDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerComboGrupoFiltroProgramaCritico()
        {
            try
            {


                var _repPersonal = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var personalAsesores = new List<DatosPersonalAsesorDTO>();
                personalAsesores = _repPersonal.ObtenerTodoPersonalAsesoresFiltro();

                return Ok(personalAsesores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra
        /// Fecha:  07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la grilla inicial para el modulo
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase GrupoFiltroProgramaCriticoDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                var _repGrupoFiltroProgramaCritico = new GrupoFiltroProgramaCriticoService(unitOfWork);
                return Ok(_repGrupoFiltroProgramaCritico.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha:  07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de areas y subareas de capacitacion
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase GrupoFiltroProgramaCriticoCombosDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosAreaSubArea()
        {
            try
            {

                var _repAreaCapacitacion = new AreaCapacitacionService(unitOfWork);
                var _repSubAreaCapacitacion = new SubAreaCapacitacionService(unitOfWork);
                var combos = new GrupoFiltroProgramaCriticoCombosDTO()
                {
                    ListaAreaCapacitacion = _repAreaCapacitacion.ObtenerCombo(),
                    ListaSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerCombo().ToList()
                };

                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 09/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los asesores por grupo de programa critico
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase DatosPersonalAsesorPorGrupoIdDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]/{IdGrupo}")]
        [HttpGet]
        public ActionResult ObtenerAsesoresPorGrupoId(int IdGrupo)
        {
            try
            {
                var _repPersonal = new PersonalService(unitOfWork);
                List<DatosPersonalAsesorPorGrupoIdDTO> listaPersonalAsesoresGrupoId = new List<DatosPersonalAsesorPorGrupoIdDTO>();
                listaPersonalAsesoresGrupoId = _repPersonal.ObtenerAsesoresPorGrupoId(IdGrupo);

                return Ok(listaPersonalAsesoresGrupoId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        /// Tipo Función: GET
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los programas generales asociados segun el id de grupo de programa critico
        /// </summary>
        /// <param name="IdGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Response 200 con el objeto anonimo (Lista de objetos de clase PGeneralSubAreaDTO, Lista de objetos de clase PGeneralSubAreaDTO), caso contrario response 400 con el mensaje de excepcion</returns>
        [Route("[Action]/{IdGrupo}")]
        [HttpGet]
        public ActionResult ObtenerPGeneralAsociado(int IdGrupo)
        {
            try
            {
                var _repPGeneral = new PGeneralService(unitOfWork);
                var _repGrupoFiltroProgramaCriticoPGeneral = new GrupoFiltroProgramaCriticoService(unitOfWork);
                // GrupoFiltroProgramaCriticoPgeneralRepositorio _repGrupoFiltroProgramaCriticoPGeneral = new GrupoFiltroProgramaCriticoPgeneralRepositorio(_integraDBContext);
                var listaPGeneral = _repPGeneral.ObtenerPGeneralProgramaCriticoPorSubArea();
                var listaPGeneralPorGrupo = _repGrupoFiltroProgramaCriticoPGeneral.ObtenerPorIdGrupo(IdGrupo);

                return Ok(new { ListaPGeneral = listaPGeneral, ListaPGeneralPorGrupo = listaPGeneralPorGrupo });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarGrupoFiltro(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);

                var respuesta = servicio.Insertar(Json);

                return Ok(respuesta);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[Action]")]
        public IActionResult ActualizarGrupo(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Actualizar(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[Action]")]
        public IActionResult EliminarGrupo(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                var respuesta = servicio.Eliminar(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GuardarAsociacion(AsociacionGrupoFiltroPGeneralDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoPgeneralService(unitOfWork);
                var respuesta = servicio.GuardarAsociacion(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}









