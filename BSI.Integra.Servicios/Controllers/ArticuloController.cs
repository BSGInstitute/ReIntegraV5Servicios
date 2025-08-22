using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ArticuloController
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión de Articulo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ArticuloController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public ArticuloController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Articulo entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var IdWeb = servicio.ObtenerMaximaIdWeb();
                entidad.IdWeb = IdWeb + 1;
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<Articulo> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Articulo entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<Articulo> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
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
                var servicio = new ArticuloService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
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
                var servicio = new ArticuloService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Articulo
        /// </summary>
        /// <returns> List<ArticuloCompuestoDTO> </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerArticulo([FromBody] filtroPrueba paginador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var FormularioArticulo = servicio.ObtenerTodo(paginador);
                //var Total = FormularioArticulo.Total;
                //return Ok(new { data = FormularioArticulo, Total });
                return Ok(FormularioArticulo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los combos para los filtros de T_Articulo
        /// </summary>
        /// <returns> List<ArticuloDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerFiltros()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioTipoArticulo = new TipoArticuloService(unitOfWork);
                var servicioArea = new AreaCapacitacionService(unitOfWork);
                var servicioSubArea = new SubAreaCapacitacionService(unitOfWork);
                var servicioCategoria = new CategoriaProgramaService(unitOfWork);
                var servicioParametroSeo = new ArticuloSeoService(unitOfWork);

                var filtroTipoArticulo = servicioTipoArticulo.ObtenerFiltroTipoArticulo();
                var filtroArea = servicioArea.ObtenerCombo();
                var filtroSubArea = servicioSubArea.ObtenerCombo();
                var filtroExpositor = unitOfWork.ExpositorRepository.ObtenerCombo();
                var filtroCategoria = servicioCategoria.ObtenerCombo();
                var filtroParametroSeo = servicioParametroSeo.ObtenerCombo();

                return Ok(new
                {
                    filtroTipoArticulo,
                    filtroArea,
                    filtroSubArea,
                    filtroExpositor,
                    filtroCategoria,
                    filtroParametroSeo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de parametroSeo para T_Articulo
        /// </summary>
        /// <returns> List<ParametroSeoContenidoArticuloDTO> </returns>
        [HttpGet("[Action]/{IdArticulo}")]
        public IActionResult ObtenerParametroSeoArticulo(int IdArticulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloSeoService(unitOfWork);
                return Ok(servicio.ObtenerArticuloSeoParametro(IdArticulo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de tag para T_Articulo
        /// </summary>
        /// <returns> List<TagComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerTag()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new TagService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Asocia los registros de tap para T_Articulo
        /// </summary>
        /// <returns> List<TagComboDTO> </returns>
        [HttpPost("[Action]")]
        public IActionResult AsociarTag([FromBody] ArticulosTagAsociadosDTO listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new ArticuloTagService(unitOfWork);
                var asociadosActualmente = servicio.ObtenerArticuloTagsAsociados(listado.IdArticulo);
                List<ArticuloTag> listaEliminar = new List<ArticuloTag>();
                List<ArticuloTag> listaInsertar = new List<ArticuloTag>();
                foreach (ArticuloTag item in asociadosActualmente)
                    if (!listado.IdsAsociados.Contains(item.IdTag))
                    {
                        item.Estado = false;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = listado.Usuario;
                        listaEliminar.Add(item);
                    }
                foreach (int IdTagItem in listado.IdsAsociados)
                    if (asociadosActualmente.SingleOrDefault(x => x.IdTag == IdTagItem) == null)
                        listaInsertar.Add(new ArticuloTag
                        {
                            IdArticulo = listado.IdArticulo,
                            IdTag = IdTagItem,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = listado.Usuario,
                            UsuarioModificacion = listado.Usuario
                        });

                if (listaEliminar.Count > 0)
                    servicio.Update(listaEliminar);

                if (listaInsertar.Count > 0)
                    servicio.Add(listaInsertar);
                return Ok(new { Mensaje = "Eliminados:" + listaEliminar.Count + "  Insertados:" + listaInsertar.Count, listaEliminar, listaInsertar });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de tag por T_Articulo
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        [HttpGet("[Action]/{IdArticulo}")]
        public IActionResult ObtenerTagsPorArticulo(int IdArticulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloTagService(unitOfWork);
                return Ok(servicio.ObtenerTagsAsociadosArticulo(IdArticulo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de programas no asociados para T_Articulo
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        [HttpGet("[Action]/{IdArticulo}")]
        public IActionResult ObtenerProgramasNoAsociadosArticulo(int IdArticulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                return Ok(servicio.ObtenerProgramasNoAsociadosArticulo(IdArticulo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de todos los programas asociados para para T_Articulo
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        [HttpGet("[Action]/{IdArticulo}")]
        public IActionResult ObtenerProgramasAsociadosArticulo(int IdArticulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                return Ok(servicio.ObtenerProgramasAsociadosArticulo(IdArticulo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Asocia los programas para T_Articulo
        /// </summary>
        /// <returns> List<ArticuloPGeneral> </returns>
        [HttpPost("[Action]")]
        public IActionResult AsociarProgramas([FromBody] ArticulosProgramasAsociadosDTO listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloPGeneralService(unitOfWork);
                var asociadosActualmente = servicio.ObtenerArticuloPGeneralAsociados(listado.IdArticulo);
                List<ArticuloPGeneral> listaEliminar = new List<ArticuloPGeneral>();
                List<ArticuloPGeneral> listaInsertar = new List<ArticuloPGeneral>();
                foreach (ArticuloPGeneral item in asociadosActualmente)
                    if (!listado.IdsAsociados.Contains(item.IdPgeneral))
                    {
                        item.Estado = false;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = listado.Usuario;
                        listaEliminar.Add(item);
                    }
                foreach (int IdPGeneralItem in listado.IdsAsociados)
                    if (asociadosActualmente.SingleOrDefault(x => x.IdPgeneral == IdPGeneralItem) == null)
                        listaInsertar.Add(new ArticuloPGeneral
                        {
                            IdArticulo = listado.IdArticulo,
                            IdPgeneral = IdPGeneralItem,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = listado.Usuario,
                            UsuarioModificacion = listado.Usuario
                        });

                if (listaEliminar.Count > 0)
                    servicio.Update(listaEliminar);

                if (listaInsertar.Count > 0)
                    servicio.Add(listaInsertar);
                return Ok(new { Mensaje = "Eliminados:" + listaEliminar.Count + "  Insertados:" + listaInsertar.Count, listaEliminar, listaInsertar });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta los registros de parametroSeo para T_Articulo
        /// </summary>
        /// <returns> Articulo </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarArticuloParametroSeo([FromBody] InsertarArticuloParametroSeoDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var IdWeb = servicio.ObtenerMaximaIdWeb();
                entidad.Formulario.IdWeb = IdWeb + 1;
                var respuesta = servicio.InsertarArticuloParametroSeo(entidad);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta los registros de parametroSeo para T_Articulo
        /// </summary>
        /// <returns> Articulo </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarArticuloParametroSeo([FromBody] InsertarArticuloParametroSeoDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ArticuloService(unitOfWork);
                var respuesta = servicio.ActualizarArticuloParametroSeo(entidad);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}
