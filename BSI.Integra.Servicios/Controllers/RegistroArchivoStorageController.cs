using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RegistroArchivoStorageController
    /// Autor: Margiory Ramirez Neyra
    /// Fecha: 28/09/2022
    /// <summary>
    /// Gestión de Categoria Origen
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegistroArchivoStorageController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ITokenManager _tokenManager;
        private string urlimg;

        public RegistroArchivoStorageController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] RegistroArchivoStorage entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<RegistroArchivoStorage> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] RegistroArchivoStorage entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<RegistroArchivoStorage> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
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
        /// Fecha: 11/08/2022
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
                var servicio = new RegistroArchivoStorageService(unitOfWork);
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
        /// Fecha: 22/08/2022
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
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_RegistroArchivoStorage para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new RegistroArchivoStorageService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_RegistroArchivoStorage
        /// </summary>
        /// <returns> List<RegistroArchivoStorageDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult insertsr()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                return Ok(servicio.ObtenerRegistroArchivoStorage());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_RegistroArchivoStorage
        /// </summary>
        /// <param name="IdPersonal">Lista de Id de la entidad a obtener</param>
        /// <param name="usuario">Nombre del usuario que realiza el obtener la data</param>
        /// <param name="NombreArchivo">Nombre del archivo registrado/param>
        /// <returns> List<RegistroArchivoStorageDTO> </returns>
        [HttpGet("[Action]/{IdPersonal}/{IdUrlBlockStorage}/{NombreArchivo}")]

        public IActionResult ObtenerTodoPorPermisosRegistroArchivoStorage(int IdPersonal, int IdUrlBlockStorage, string NombreArchivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new RegistroArchivoStorageService(unitOfWork);

                return Ok(servicio.ObtenerTodoPorPermisosRegistroArchivoStorage(new RegistroArchivoMostrarFiltroDTO
                {
                    IdPersonal = IdPersonal,
                    IdUrlBlockStorage = IdUrlBlockStorage,
                    NombreArchivo = NombreArchivo
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Margiory Rammirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_RegistroArchivoStorage para Combo por Id.
        /// </summary>
        /// <param name="IdPersonal">Lista de Id de la entidad a Obtener</param>
        /// <returns> List<T_RegistroArchivoStorage> </returns>

        [HttpGet("[Action]/{IdPersonal}")]
        public IActionResult ObtenerCombos(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                var servicioRegistroArchivoStorage = new RegistroArchivoStorageService(unitOfWork);


                var listadoContenedores = servicioRegistroArchivoStorage.ObtenerContenedores(IdPersonal);
                var listadoSubContenedores = servicioRegistroArchivoStorage.ObtenerSubcontenedores(IdPersonal);
                var listadoTipoSubContenedores = servicioRegistroArchivoStorage.ObtenerTipoSubcontenedores(IdPersonal);


                return Ok(new
                {
                    listadoContenedores,
                    listadoSubContenedores,
                    listadoTipoSubContenedores


                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>



        [HttpPost("[Action]")]
        public IActionResult InsertarNuevoRegistro([FromBody] RegistroArchivoStorage entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion   de url en el repsositorioweb
        /// </summary>
        /// <returns>Retorna 200  de objetos ingresados o 400 y mensaje de error </returns>


        [Route("[action]")]
        [HttpPost]

        public IActionResult RegistroArchivoStorageSubirArchivo([FromForm] RegistroArchivoStorageSubirArchivoDTO registroSubirArchivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.RegistroArchivoStorageSubirArchivo(registroSubirArchivo);
                //var respuesta = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/programas/Captura de pantalla 2022-09-06 154214.png";
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 28/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage para mostrarse en combo.
        /// </summary>
        /// <returns>Retorna 200  de objetos ingresados o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpGet]

        public IActionResult ObtenerComboFirma()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                var respuesta = servicio.ObtenerComboFirma();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 28/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage filtrados por idUrlSubContenedor
        /// </summary>
        /// <param name="idUrlSubContenedor">Lista de Id de la entidad a obtener</param>
        /// <returns> List<RegistroArchivoObtenerUrlComboDTO> </returns>
        [HttpGet("[Action]/{idUrlSubContenedor}")]

        public IActionResult ObtenerRegistroArchivoStoragePorIdUrlSubContenedor(int idUrlSubContenedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new RegistroArchivoStorageService(unitOfWork);
                return Ok(servicio.ObtenerRegistroArchivoStoragePorIdUrlSubContenedor(idUrlSubContenedor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}



