using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Servicios.Controllers.Comercial.AnalisisLlamadas
{
    /// Controlador: LineamientoCalificacionController
    /// Autor: Joseph Llanque.
    /// Fecha:07/03/2025
    /// <summary>
    /// Fase Calificacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LineamientoCalificacionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public LineamientoCalificacionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="lineamientoCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] LineamientoCalificacionEntradaDTO lineamientoCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var LineamientoCalificacion = new LineamientoCalificacion();
                LineamientoCalificacion.IdCriterioCalificacionLlamada = lineamientoCalificacionEntradaDTO.IdCriterioCalificacionLlamada;
                LineamientoCalificacion.IdCriticidadCalificacion = lineamientoCalificacionEntradaDTO.IdCriticidadCalificacion;
                LineamientoCalificacion.NombreLineamiento = lineamientoCalificacionEntradaDTO.NombreLineamiento;
                LineamientoCalificacion.Orden = lineamientoCalificacionEntradaDTO.Orden;
                LineamientoCalificacion.Descripcion = lineamientoCalificacionEntradaDTO.Descripcion;
                LineamientoCalificacion.HerramientaAnalisis = lineamientoCalificacionEntradaDTO.HerramientaAnalisis;
                LineamientoCalificacion.Version = lineamientoCalificacionEntradaDTO.Version;
                LineamientoCalificacion.EsVigente = lineamientoCalificacionEntradaDTO.EsVigente;
                LineamientoCalificacion.FechaVigenciaInicio = lineamientoCalificacionEntradaDTO.FechaVigenciaInicio;
                LineamientoCalificacion.FechaVigenciaFin = lineamientoCalificacionEntradaDTO.FechaVigenciaFin;
                LineamientoCalificacion.UsuarioCreacion = lineamientoCalificacionEntradaDTO.Usuario;
                LineamientoCalificacion.UsuarioModificacion = lineamientoCalificacionEntradaDTO.Usuario;
                LineamientoCalificacion.FechaCreacion = DateTime.Now;
                LineamientoCalificacion.FechaModificacion = DateTime.Now;
                LineamientoCalificacion.Estado = true;
                var resultado = lineamientoCalificacionService.Add(LineamientoCalificacion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="lineamientoCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<LineamientoCalificacion> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<LineamientoCalificacionEntradaDTO> lineamientoCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var lineamientoCalificacionLista = new List<LineamientoCalificacion>();
                foreach (var entidad in lineamientoCalificacionEntradaDTO)
                {
                    var LineamientoCalificacion = new LineamientoCalificacion();
                    LineamientoCalificacion.IdCriterioCalificacionLlamada = entidad.IdCriterioCalificacionLlamada;
                    LineamientoCalificacion.IdCriticidadCalificacion = entidad.IdCriticidadCalificacion;
                    LineamientoCalificacion.NombreLineamiento = entidad.NombreLineamiento;
                    LineamientoCalificacion.Orden = entidad.Orden;
                    LineamientoCalificacion.Descripcion = entidad.Descripcion;
                    LineamientoCalificacion.HerramientaAnalisis = entidad.HerramientaAnalisis;
                    LineamientoCalificacion.UsuarioCreacion = entidad.Usuario;
                    LineamientoCalificacion.UsuarioModificacion = entidad.Usuario;
                    LineamientoCalificacion.FechaCreacion = DateTime.Now;
                    LineamientoCalificacion.FechaModificacion = DateTime.Now;
                    LineamientoCalificacion.Estado = true;
                    lineamientoCalificacionLista.Add(LineamientoCalificacion);
                }
                var resultado = lineamientoCalificacionService.Add(lineamientoCalificacionLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="lineamientoCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] LineamientoCalificacionEntradaDTO lineamientoCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var LineamientoCalificacion = new LineamientoCalificacion();
                LineamientoCalificacion = lineamientoCalificacionService.ObtenerPorId(lineamientoCalificacionEntradaDTO.Id.Value);
                LineamientoCalificacion.IdCriterioCalificacionLlamada = lineamientoCalificacionEntradaDTO.IdCriterioCalificacionLlamada;
                LineamientoCalificacion.IdCriticidadCalificacion = lineamientoCalificacionEntradaDTO.IdCriticidadCalificacion;
                LineamientoCalificacion.NombreLineamiento = lineamientoCalificacionEntradaDTO.NombreLineamiento;
                LineamientoCalificacion.Orden = lineamientoCalificacionEntradaDTO.Orden;
                LineamientoCalificacion.Descripcion = lineamientoCalificacionEntradaDTO.Descripcion;
                LineamientoCalificacion.HerramientaAnalisis = lineamientoCalificacionEntradaDTO.HerramientaAnalisis;
                LineamientoCalificacion.Version = lineamientoCalificacionEntradaDTO.Version;
                LineamientoCalificacion.EsVigente = lineamientoCalificacionEntradaDTO.EsVigente;
                LineamientoCalificacion.FechaVigenciaInicio = lineamientoCalificacionEntradaDTO.FechaVigenciaInicio;
                LineamientoCalificacion.FechaVigenciaFin = lineamientoCalificacionEntradaDTO.FechaVigenciaFin;
                LineamientoCalificacion.UsuarioModificacion = lineamientoCalificacionEntradaDTO.Usuario;
                LineamientoCalificacion.FechaModificacion = DateTime.Now;
                var resultado = lineamientoCalificacionService.Update(LineamientoCalificacion);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="lineamientoCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<LineamientoCalificacionEntradaDTO> lineamientoCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var lineamientoCalificacionLista = new List<LineamientoCalificacion>();
                foreach (var entidad in lineamientoCalificacionEntradaDTO)
                {
                    var LineamientoCalificacion = new LineamientoCalificacion();
                    LineamientoCalificacion = lineamientoCalificacionService.ObtenerPorId(entidad.Id.Value);
                    LineamientoCalificacion.IdCriterioCalificacionLlamada = entidad.IdCriterioCalificacionLlamada;
                    LineamientoCalificacion.IdCriticidadCalificacion = entidad.IdCriticidadCalificacion;
                    LineamientoCalificacion.NombreLineamiento = entidad.NombreLineamiento;
                    LineamientoCalificacion.Orden = entidad.Orden;
                    LineamientoCalificacion.Descripcion = entidad.Descripcion;
                    LineamientoCalificacion.HerramientaAnalisis = entidad.HerramientaAnalisis;
                    LineamientoCalificacion.UsuarioModificacion = entidad.Usuario;
                    LineamientoCalificacion.FechaModificacion = DateTime.Now;
                }
                var resultado = lineamientoCalificacionService.Update(lineamientoCalificacionLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true or false </returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="listadoIds"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true or false </returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerLineamiento()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerLineamiento();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult EsquemaCalificacionConfigurado()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.EsquemaCalificacionConfigurado();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult CongelarConfiguracion([FromBody] CongelamientoConfiguracionDTO congelamientoConfiguracionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.CongelarConfiguracion(congelamientoConfiguracionDTO);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ActivarConfiguracion([FromBody] CongelamientoConfiguracionActivaDTO activarConfiguracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ActivarConfiguracion(activarConfiguracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult HistorialVersionCalificacionLlamada()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.HistorialVersionCalificacionLlamada();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la a por area trabajo
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{IdPersonalAreaTrabajo}")]
        [HttpGet]
        public ActionResult HistorialVersionCalificacionLlamadaPorAreaTrabajo(int IdPersonalAreaTrabajo)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.HistorialVersionCalificacionLlamadaPorAreaTrabajo(IdPersonalAreaTrabajo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 18/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el historial de versiones de configuración de calificación para un área de trabajo específica.
        /// </summary>
        /// <param name="idPersonalAreaTrabajo">Identificador del área de trabajo.</param>
        /// <returns> ActionResult con la lista de ConfiguracionEsquemaCalificacionLlamdaDTO </returns>
        [Route("[action]/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public ActionResult HistorialVersionCalificacionLlamadaV2(int idPersonalAreaTrabajo)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.HistorialVersionCalificacionLlamadaV2(idPersonalAreaTrabajo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Arnold Zaa Fernandez
        /// Fecha: 25/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene la versión vigente de configuración de calificación para un área de trabajo específica.
        /// Devuelve los datos de la tabla T_EvaluacionLlamadaConfiguracionVersion (id, fechaVersion, descripcionVersion,
        /// esVigente, comentario, etc.) junto con el configuracionJSON construido dinámicamente.
        /// El JSON se construye usando BuildConfiguracionLineamientosFromSp a partir del SP SP_EvaluacionLlamadaObtenerConfiguracionVigente.
        /// </summary>
        /// <param name="idPersonalAreaTrabajo">Identificador del área de trabajo.</param>
        /// <returns> ActionResult con ConfiguracionEsquemaCalificacionLlamdaDTO que incluye los datos de la versión vigente y el JSON serializado </returns>
        [Route("[action]/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public ActionResult HistorialVesionEvaluacionLlamada(int idPersonalAreaTrabajo)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerConfiguracionVigenteV3(idPersonalAreaTrabajo);

                if (resultado == null)
                {
                    return NotFound(new { mensaje = "No se encontró una versión vigente para el área de trabajo especificada." });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idLlamada}")]
        [HttpGet]
        public ActionResult ObtenerNotaCalificacionLineamiento(int idLlamada)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerNotaCalificacionLineamiento(idLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 25/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene calificaciones temporales en tiempo real desde T_EvaluacionLlamadaTemporal y T_EvaluacionDetalleManualTemporal.
        /// Utiliza IdActividadDetalle + NumeroLlamada como identificadores antes de que la llamada definitiva se registre.
        /// </summary>
        /// <param name="idActividadDetalle">ID de la actividad detalle</param>
        /// <param name="numeroLlamada">Número secuencial de la llamada dentro de la actividad</param>
        /// <returns> Lista de CalificacionLlamadaDTO con las calificaciones temporales </returns>
        [Route("[action]/{idActividadDetalle}/{numeroLlamada}")]
        [HttpGet]
        public ActionResult ObtenerNotaCalificacionLineamientoTemporal(int idActividadDetalle, int numeroLlamada)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerNotaCalificacionLineamientoTemporal(idActividadDetalle, numeroLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{IdOportunidad}/{IdLlamadaWebphoneCruceCentral3Cx}")]
        [HttpGet]
        public ActionResult ObtenerNotaCalificacionLineamientoHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerNotaCalificacionLineamientoHistorico(IdOportunidad, IdLlamadaWebphoneCruceCentral3Cx);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{IdOportunidad}/{IdLlamadaWebphoneCruceCentral3Cx}")]
        [HttpGet]
        public ActionResult ObtenerNotaCalificacionPuntoGeneralHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerNotaCalificacionPuntoGeneralHistorico(IdOportunidad, IdLlamadaWebphoneCruceCentral3Cx);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información de motivaciones
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionMotivaciones")]
        public async Task<IActionResult> ObtenerInformacionMotivaciones([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idPGeneralStr))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido." });
                }

                if (!int.TryParse(idPGeneralStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El valor de 'idPGeneral' debe ser un número entero válido." });
                }

                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var respuesta = await lineamientoCalificacionService.CargarInformacionMotivacionesAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información de objeciones de clientes
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionObjecionesClientes")]
        public async Task<IActionResult> ObtenerInformacionObjecionesClientes([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idPGeneralStr))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido." });
                }

                if (!int.TryParse(idPGeneralStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El valor de 'idPGeneral' debe ser un número entero válido." });
                }

                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var respuesta = await lineamientoCalificacionService.CargarInformacionObjecionesClientesAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idLlamada}")]
        [HttpGet]
        public ActionResult ObtenerNotaCalificacionLineamientoGeneral(int idLlamada)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerNotaCalificacionLineamientoGeneral(idLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idLlamada}")]
        [HttpGet]
        public ActionResult ObtenerNotaCalificacionAutomaticaLineamiento(int idLlamada)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerNotaCalificacionAutomaticaLineamiento(idLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult GuardarCalificacionLlamada([FromBody] CalificacionLlamadaManualDTO activarConfiguracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.GuardarCalificacionLlamada(activarConfiguracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 25/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Guarda calificación en tiempo real usando tablas temporales (T_EvaluacionLlamadaTemporal y T_EvaluacionDetalleManualTemporal).
        /// Permite calificar antes de que la llamada definitiva se registre, usando IdActividadDetalle + NumeroLlamada.
        /// </summary>
        /// <param name="calificacionTemporal"> Datos necesarios para la inserción temporal de calificación manual </param>
        /// <returns> bool indicando éxito de la operación </returns>
        [HttpPost("[Action]")]
        public IActionResult GuardarCalificacionLlamadaTemporal([FromBody] CalificacionLlamadaManualTemporalDTO calificacionTemporal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.GuardarCalificacionLlamadaTemporal(calificacionTemporal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult CalificarLlamadaAutomaticamente([FromBody] CalificacionLlamadaAutomaticaDTO informacionCalificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.CalificarLlamadaAutomaticamente(informacionCalificacion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ActualizarEstadoCalificacionLlamada([FromBody] EstadoLlamadaCalificadaDTO estadoLlamada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ActualizarEstadoCalificacionLlamada(estadoLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ConfigurarPanelAutomatico([FromBody] ConfiguracionTranscripcionDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ConfigurarPanelAutomatico(configuracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ConfigurarPanelAutomaticoCalificacion([FromBody] ConfiguracionTranscripcionDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ConfigurarPanelAutomaticoCalificacion(configuracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ConfigurarPanelCalificacionAuto([FromBody] ConfiguracionTranscripcionDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ConfigurarPanelCalificacionAuto(configuracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ConfigurarPanelTranscripcionAuto([FromBody] ConfiguracionTranscripcionDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ConfigurarPanelTranscripcionAuto(configuracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ActivarConfiguracionTranscripcionAuto([FromBody] ConfiguracionActivoProcesoDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ActivarConfiguracionTranscripcionAuto(configuracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="congelamientoConfiguracionDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult ActivarConfiguracionCalificacionAuto([FromBody] ConfiguracionActivoProcesoDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ActivarConfiguracionCalificacionAuto(configuracion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionMasivaActiva()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerConfiguracionMasivaActiva();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionCalificacionMasivaActiva()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerConfiguracionCalificacionMasivaActiva();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionCalificacionAuto()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerConfiguracionCalificacionAuto();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionTranscripcionAuto()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerConfiguracionTranscripcionAuto();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> TranscripcionAuto()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.TranscripcionAuto(1);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> TranscripcionAutoV2(int idPersonalAreaTrabajo)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.TranscripcionAutoV2(idPersonalAreaTrabajo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 24/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Procesa la transcripción manual de audio según los datos proporcionados
        /// </summary>
        /// <returns> List<TranscripcionManualDTO> </returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> TranscripcionManual([FromBody] TranscripcionManualDTO payload)
        {
            if (payload == null)
            {
                return BadRequest("Payload de transcripción inválido.");
            }

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.TranscripcionManual(payload);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la transcripción: {ex.Message}");
            }
        }



        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]   
        public async Task<ActionResult<List<bool>>> CalificacionAuto()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.CalificacionAuto(1);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> CalificacionAutoV2(int idPersonalAreaTrabajo)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.CalificacionAutoV2(idPersonalAreaTrabajo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta validación de matrícula para llamadas pendientes.
        /// Usa SP_ValidacioMatriculaObtenerInformacionPendiente que está fijo para área Ventas.
        /// </summary>
        /// <returns> List<bool> con resultados de validaciones </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> ValidacionMatricula()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.ValidacionMatricula();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Lolo Zaa
        /// Fecha: 03/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Transcribe llamadas sin transcripción para el proceso de validación de matrícula.
        /// Solo procesa llamadas del área de Ventas.
        /// </summary>
        /// <param name="llamadasSinTranscripcion">Lista de llamadas sin transcripción</param>
        /// <returns> List<bool> con resultados de transcripciones </returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> TranscripcionValidacionMatricula([FromBody] List<LlamadaProcesoAutoDTO> llamadasSinTranscripcion)
        {
            if (llamadasSinTranscripcion == null)
            {
                return BadRequest("La lista de llamadas no puede ser nula.");
            }

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.TranscripcionValidacionMatricula(llamadasSinTranscripcion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar las transcripciones: {ex.Message}");
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 05/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las observaciones de lineamientos evaluados por IA para una validación de matrícula.
        /// TipoValidacionMatricula: 1=Validación Proceso Enrollment, 2=Validación Contrato Voz
        /// </summary>
        /// <param name="idOportunidad">ID de la oportunidad</param>
        /// <param name="tipoValidacionMatricula">Tipo de validación (1 o 2)</param>
        /// <returns> List<ValidacionMatriculaLineamientoDTO> con las observaciones </returns>
        [Route("[action]/{idOportunidad}/{tipoValidacionMatricula}")]
        [HttpGet]
        public ActionResult<IEnumerable<ValidacionMatriculaLineamientoDTO>> ObtenerValidacionMatriculaLineamiento(int idOportunidad, int tipoValidacionMatricula)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerValidacionMatriculaLineamiento(idOportunidad, tipoValidacionMatricula);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: Post
        /// Autor: Lolo Zaa
        /// Fecha: 25/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Proxy para el servicio de calificacion ia
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpPost("CalificacionIndividual")]
        public async Task<IActionResult> CalificacionIndividual([FromBody] CalificacionIndividualRequestDTO dto)
        {
            if (dto == null)
                return BadRequest("El payload no puede ser nulo.");

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.CalificacionIndividual(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> TranscripcionMasiva()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.TranscripcionAuto(2);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> CalificacionMasiva()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.CalificacionAuto(2);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteCalificacionClientes([FromBody] ReporteCalificacionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerReporte(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Lolo Zaa
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteCalificacionClientesVentas([FromBody] ReporteCalificacionRequestV2 request)
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerReporteVentas(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Lolo Zaa
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteValidacionMatricula([FromBody] ReporteCalificacionRequestV2 request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ValidacionMatriculaReporte(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Lolo Zaa
        /// Fecha: 27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada) para atencion al cliente.
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteCalificacionClientesAtencionCliente([FromBody] ReporteCalificacionRequestV2 request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerReporteAtencionCliente(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteCalificacionClientePorArea([FromBody] ReporteCalificacionAreaRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerReportePorArea(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[action]")]
        public IActionResult ObtenerPuntoCriticoDiario(PuntoCriticoResumenEntradaDTO payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerPuntoCriticoDiario(payload);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteCalificacionFase([FromBody] ReporteCalificacionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerReporteFase(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Lolo Zaa
        /// Fecha: 02/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).
        /// </summary>
        [HttpPost("[Action]")]
        public IActionResult ReporteCalificacionFaseVentas([FromBody] ReporteCalificacionRequestV2 request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);

                var resultado = lineamientoCalificacionService.ObtenerReporteFaseVentas(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Obtiene el promedio global de calificaciones para el rango de fechas especificado.
        /// Calcula el promedio de todas las calificaciones válidas (excluyendo -1).
        /// </summary>
        /// <param name="request">Parámetros de filtrado</param>
        /// <returns>Promedio global, total de llamadas y total de calificaciones</returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerPromedioGlobal([FromBody] ReporteCalificacionGlobalRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerPromedioGlobal(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        /// <summary>
        /// Obtiene el promedio global de calificaciones para el rango de fechas especificado.
        /// Calcula el promedio de todas las calificaciones válidas (excluyendo -1).
        /// </summary>
        /// <param name="request">Parámetros de filtrado</param>
        /// <returns>Promedio global, total de llamadas y total de calificaciones</returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerPromedioGlobalVentas([FromBody] ReporteCalificacionGlobalRequestV2 request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerPromedioGlobalVentas(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

         /// Tipo Función: POST
        /// Autor: Lolo Zaa
        /// Fecha: 27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Reporte de calificación de clientes (agrupado por llamada).
        /// Calcula promedio excluyendo -1 y devuelve puntos críticos (3 peores).

        [HttpPost("[Action]")]
        public IActionResult ObtenerPromedioGlobalAtencionCliente([FromBody] ReporteCalificacionGlobalRequestV2 request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerPromedioGlobalAtencionCliente(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        [Route("[action]/{idLlamada}/{tipoCalificacion}")]
        [HttpGet]
        public ActionResult ObtenerCalificacionFase(int idLlamada, bool tipoCalificacion)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerCalificacionFase(idLlamada, tipoCalificacion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        [Route("[action]/{idLlamada}")]
        [HttpGet]
        public ActionResult ObtenerInformacionLlamada(int idLlamada)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = lineamientoCalificacionService.ObtenerInformacionLlamada(idLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 27/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza el procesamiento de transcripcion de llamadas
        /// </summary>
        /// <param name="TranscriptionWebhookPayload"> Datos necesarios para la insercion de datos </param>
        /// <returns> DTO: TranscriptionWebhookPayloadDTO </returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ProcesarCalificacionBatch([FromBody] ResultadoEvaluacionBatch payload)
        {

            if (payload == null)
            {
                return BadRequest("Payload de Calificacion inválido.");
            }

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                HubConnection signalRConnection = null;
                await lineamientoCalificacionService.ProcesarCalificacionBatch(payload);
                signalRConnection = new HubConnectionBuilder()
                    .WithUrl($"https://signalr-prototipo.bsginstitute.com/hubIntegraHub?idUsuario={payload.IdPersonal}&usuarioNombre={payload.UserName}&rooms=")
                    .WithAutomaticReconnect()
                    .ConfigureLogging(logging =>
                    {
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Debug);
                    })
                    .Build();

                await signalRConnection.StartAsync();

                // 4. Enviar notificación
                await signalRConnection.InvokeAsync("NotificarCalificacion",
                    payload.IdLlamada.ToString(),
                    "success",
                    payload.IdPersonal?.ToString(),
                    payload.contacto);

                return Ok("Calififacion registrada correctamente.");

            }
            catch (System.Exception ex)
            {
                // Registra el error según corresponda
                return StatusCode(500, $"Error al insertar la calificacion: {ex.Message}");
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 27/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza el procesamiento de transcripcion de llamadas
        /// </summary>
        /// <param name="TranscriptionWebhookPayload"> Datos necesarios para la insercion de datos </param>
        ///// <returns> DTO: TranscriptionWebhookPayloadDTO </returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ProcesarRecomendacionesBatch([FromBody] RecomendacionLlamadaDTO payload)
        {

            if (payload == null)
            {
                return BadRequest("Payload de recomenacion inválido.");
            }

            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                HubConnection signalRConnection = null;
                await lineamientoCalificacionService.ProcesarRecomendacionesBatch(payload);
                return Ok("Recomendacion registrada correctamente.");

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al insertar la calificacion: {ex.Message}");
            }
        }



        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<bool>>> ProcesarPuntoCriticoDiario()
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.ProcesarPuntoCriticoDiario();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idLlamada}")]
        [HttpGet]
        public async Task<IActionResult> GenerarCuerpoCalificacion(int idLlamada)
        {
            try
            {
                var lineamientoCalificacionService = new LineamientoCalificacionService(unitOfWork);
                var resultado = await lineamientoCalificacionService.GenerarCuerpoCalificacionv2(idLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
