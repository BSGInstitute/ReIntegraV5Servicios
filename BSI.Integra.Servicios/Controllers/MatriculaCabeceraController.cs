using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MatriculaCabeceraController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión de MatriculaCabecera
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MatriculaCabeceraController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public MatriculaCabeceraController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] MatriculaCabecera entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<MatriculaCabecera> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] MatriculaCabecera entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<MatriculaCabecera> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
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
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
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
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MatriculaCabecera
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerMatriculaCabecera")]
        public IActionResult ObtenerMatriculaCabecera()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                return Ok(servicio.ObtenerMatriculaCabecera());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MatriculaCabecera para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Id de la Matricula Cabecera asociado al Alumno y al Centro de Costo.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerIdMatriculaPorAlumnoCentroCosto/{idAlumno}/{idCentroCosto}")]
        public IActionResult ObtenerIdMatriculaPorAlumnoCentroCosto(int idAlumno, int idCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                return Ok(servicio.ObtenerIdMatriculaCabeceraPorAlumnoCentroCosto(idAlumno, idCentroCosto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Estado Matricula
        /// </summary>
        /// <returns> Retorna booleano: true o false</returns>
        [Route("[action]/{idMatriculaCabecera}/{idEstadoMatriculado}/{codigoMatricula}")]
        [HttpGet]
        public ActionResult ActualizarEstadoMatriculado(int idMatriculaCabecera, int idEstadoMatriculado, string codigoMatricula)
        {
            try
            {
                var servicioMatriculaCabecera = new MatriculaCabeceraService(unitOfWork);
                var matricula = servicioMatriculaCabecera.ActualizarEstadoMatricula(idMatriculaCabecera, idEstadoMatriculado, codigoMatricula);
                return Ok(matricula);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cursos de moodle por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de la tabla T_MatriculaCabecera </param>
        /// <returns> Retorna Objeto recibido: List</returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerComboCursosMoodlePorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var moodleCronogramaEvaluacionService = new MoodleCronogramaEvaluacionService(unitOfWork);
                var rpta = moodleCronogramaEvaluacionService.ObtenerComboCursosMoodlePorMatricula(idMatriculaCabecera);
                //selecciona el ultimo curso que tiene actividad pendiente o sino el ultimo curso
                int? idCursoSeleccionado = moodleCronogramaEvaluacionService.ObtenerIdCursoMoodlePrimeraActividadPendiente(idMatriculaCabecera);
                if (idCursoSeleccionado == null)
                {
                    idCursoSeleccionado = (rpta != null && rpta.Any())
                        ? rpta.OrderByDescending(o => o.NombreCurso).FirstOrDefault().IdCursoMoodle
                        : idCursoSeleccionado;
                }
                return Ok(new { ComboCursos = rpta.OrderBy(o => o.NombreCurso), IdCursoSeleccionado = idCursoSeleccionado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Codigo de matricula y programa por idAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno </param>
        /// <returns> Retorna Objeto recibido: CodigoMatriculaPEspecificoDTO</returns>
        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCodigoMatriculaPEspecificoPorAlumno(int idAlumno)
        {
            try
            {
                MatriculaCabeceraService servicioMatriculaCabecera = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicioMatriculaCabecera.ObtenerCodigoMatriculaPEspecificoPorAlumno(idAlumno);
                var resultado = respuesta.Select(w => new { w.CodigoMatricula, PEspecifico = w.PEspecifico + " - " + w.CodigoMatricula, w.VersionPrograma });
                return Ok(resultado);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Codigo de matricula y programa por idAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno </param>
        /// <returns> Retorna Objeto recibido: CodigoMatriculaPEspecificoDTO</returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCursosMatriculados(int idMatriculaCabecera)
        {
            try
            {
                MatriculaCabeceraService servicioMatriculaCabecera = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicioMatriculaCabecera.ObtenerCursosMatriculados(idMatriculaCabecera);
                //var resultado = respuesta.Select(w => new { w.CodigoMatricula, PEspecifico = w.PEspecifico + " - " + w.CodigoMatricula, w.VersionPrograma });
                return Ok(respuesta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Miguel Quiñones
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Codigo de matricula y programa por idAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno </param>
        /// <returns> Retorna Objeto recibido: CodigoMatriculaPEspecificoDTO</returns>
        [Route("[action]/{idPespecificoMatriculaAlumno}/{usuario}")]
        [HttpGet]
        public ActionResult DesmatricularCurso(int idPespecificoMatriculaAlumno, string usuario)
        {
            try
            {
                MatriculaCabeceraService servicioMatriculaCabecera = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicioMatriculaCabecera.DesmatricularCurso(idPespecificoMatriculaAlumno, usuario);
                //var resultado = respuesta.Select(w => new { w.CodigoMatricula, PEspecifico = w.PEspecifico + " - " + w.CodigoMatricula, w.VersionPrograma });
                return Ok(respuesta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve los identificadores importantes por matricula de alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna Objeto recibido: IdentificadorMatriculaComboDTO</returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerIdentificadoresMatriculaComboPorAlumno(int idAlumno)
        {
            try
            {
                MatriculaCabeceraService servicioMartriculaCabecera = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicioMartriculaCabecera.ObtenerIdentificadoresMatriculaComboPorAlumno(idAlumno);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 12/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Guarda Fecha de Compromiso 
        /// </summary>
        /// <param name="nuevoCompromisoAlumnoDTO"> datos para la nueva fecha de comprimiso </param>
        /// <returns> Retorna Objeto recibido: MatriculaCabeceraFiltroDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GuardarFechaCompromiso([FromBody] NuevoCompromisoAlumnoDTO nuevoCompromisoAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                var compromisoAlumnoService = new CompromisoAlumnoService(unitOfWork);
                var compromisoAlumno = new CompromisoAlumno();
                var fecha = Convert.ToDateTime(nuevoCompromisoAlumnoDTO.FechaCompromiso);
                //var fechaActualizada = fecha.AddHours(-5);
                DateTime fechaActual = DateTime.Now;
                compromisoAlumno.IdCronogramaPagoDetalleFinal = nuevoCompromisoAlumnoDTO.Id;
                compromisoAlumno.FechaCompromiso = fecha;
                compromisoAlumno.FechaGeneracionCompromiso = fechaActual;
                compromisoAlumno.Monto = nuevoCompromisoAlumnoDTO.MontoCompromiso.Value;
                compromisoAlumno.IdMoneda = nuevoCompromisoAlumnoDTO.IdMoneda;
                compromisoAlumno.Version = nuevoCompromisoAlumnoDTO.Version.Value;
                compromisoAlumno.Estado = true;
                compromisoAlumno.UsuarioCreacion = nuevoCompromisoAlumnoDTO.Usuario;
                compromisoAlumno.UsuarioModificacion = nuevoCompromisoAlumnoDTO.Usuario;
                compromisoAlumno.FechaCreacion = fechaActual;
                compromisoAlumno.FechaModificacion = fechaActual;
                var nuevoCompromisoAlumno = compromisoAlumnoService.Add(compromisoAlumno);
                if (nuevoCompromisoAlumno.Id > 0)
                {
                    nuevoCompromisoAlumnoDTO.Flag = true;
                }
                else
                {
                    nuevoCompromisoAlumnoDTO.Flag = false;
                }
                return Ok(nuevoCompromisoAlumnoDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 02/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener versiones de Fecha de Compromiso
        /// </summary>
        /// <param name="idCuota"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        [Route("[action]/{idCuota}")]
        [HttpGet]
        public ActionResult ObtenerVersionesFechaCompromiso(int idCuota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var cronogramaPagoDetalleFinalService = new CronogramaPagoDetalleFinalService(unitOfWork);
                var resultadoFechaCompromiso = cronogramaPagoDetalleFinalService.ObtenerVersionesFechaCompromiso(idCuota);

                return Ok(resultadoFechaCompromiso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 03/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtener compromisos de una cuota
        /// </summary>
        /// <param name="idCuota"> Id de la Cuota </param>
        /// <returns> List AgendaAtcCompromiso </returns>
        [Route("[action]/{idCuota}")]
        [HttpGet]
        public ActionResult ObtenerAgendaAtcCompromiso(int idCuota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var cronogramaPagoDetalleFinalService = new CronogramaPagoDetalleFinalService(unitOfWork);
                var resultadoFechaCompromiso = cronogramaPagoDetalleFinalService.ObtenerAgendaAtcCompromiso(idCuota);

                return Ok(resultadoFechaCompromiso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor:Margiory Ramirez
        /// Fecha: 31/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cronograma Detalle Pago
        /// </summary>
        /// <returns> Retorna Objeto recibido: CronogramaPagoDetalleFinal</returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaDetallePagoFinal(string CodigoMatricula)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _moneda = new MatriculaCabeceraService(unitOfWork);
                return Ok(_moneda.ObtenerCronogramaDetallePagoFinal(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 08/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener id matricula por DNI Alumno
        /// </summary>
        /// <param name="correoAlumno"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        [Route("[action]/{DNI}")]
        [HttpGet]
        public ActionResult obtenerIdMatriculaPorDNI(string DNI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                var matriculaDNIService = matriculaCabeceraService.obtenerIdMatriculaporDni(DNI);

                return Ok(matriculaDNIService);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 08/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener id matricula por DNI Alumno
        /// </summary>
        /// <param name="correoAlumno"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        [Route("[action]/{correo}")]
        [HttpGet]
        public ActionResult obtenerIdMatriculaPorCorreo(string correo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                var matriculaCorreoService = matriculaCabeceraService.obtenerIdMatriculaporCorreo(correo);

                return Ok(matriculaCorreoService);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 08/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener id matricula por codigo Alumno
        /// </summary>
        /// <param name="correoAlumno"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        [Route("[action]/{codigo}")]
        [HttpGet]
        public ActionResult obtenerIdMatriculaPorCodigo(string codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                var matriculaCodigoService = matriculaCabeceraService.obtenerIdMatriculaporCodigo(codigo);

                return Ok(matriculaCodigoService);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatriculaAutocomplete([FromBody] StringDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                return Ok(matriculaCabeceraService.ObtenerCodigoMatriculaAutocompleto(filtro.Valor));
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerProgramaGeneralPorIdMatricula(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                return Ok(matriculaCabeceraService.ObtenerProgramaGeneralPorIdMatricula(IdMatriculaCabecera));
            }
            catch
            {
                throw;
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 25/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtener id matricula por celular Alumno
        /// </summary>
        /// <param name="correoAlumno"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        /// 
        [Route("[Action]")]
        [HttpPost]
        public ActionResult obtenerIdMatriculaPorCelular([FromBody] StringDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                return Ok(matriculaCabeceraService.ObtenerIdMatriculaPorCelular(filtro.Valor));
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 25/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtener id matricula por celular Alumno
        /// </summary>
        /// <param name="correoAlumno"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        /// 
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult obtenerVersionMatricula(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                return Ok(matriculaCabeceraService.ObtenerVersionMatricula(idMatriculaCabecera));
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 25/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtener id matricula por celular Alumno
        /// </summary>
        /// <param name="correoAlumno"> Id de la Cuota </param>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso </returns>
        /// 
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult obtenerVersionDisponibleMatricula(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                return Ok(matriculaCabeceraService.ObtenerVersionDisponibleMatricula(idMatriculaCabecera));
            }
            catch
            {
                throw;
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jorge Gamero
        /// Fecha: 17/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene version por idmatricula
        /// </summary>
        /// <param> idMatriculaCabecera </param>
        /// <returns> VersionMatriculaDTO </returns>
        /// 
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPaisMatricula(string idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                return Ok(matriculaCabeceraService.ObtenerPaisMatricula(idMatriculaCabecera));
            }
            catch
            {
                throw;
            }
        }

    }
}