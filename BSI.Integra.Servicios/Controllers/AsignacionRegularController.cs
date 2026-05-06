using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AsignacionRegularController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión de AsignacionRegular
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class AsignacionRegularController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly IServiceScopeFactory _scopeFactory;

        public AsignacionRegularController(IUnitOfWork unitOfWork, IServiceScopeFactory scopeFactory)
        {
            this.unitOfWork = unitOfWork;

            _scopeFactory = scopeFactory;
        }


        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] AsignacionRegular entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
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
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<AsignacionRegular> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
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
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] AsignacionRegular entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
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
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<AsignacionRegular> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
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
        /// Fecha: 12/08/2022
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
                var servicio = new AsignacionRegularService(unitOfWork);
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
        /// Fecha: 12/08/2022
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
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("RegularizarCambiosConfiguraciones")]
        public IActionResult RegularizarCambiosConfiguraciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.RegularizarConfiguracionAsignacionRegular();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerConfiguracionAsignacionRegular/{IdGrupoFiltroProgramaCritico}")]
        public IActionResult ObtenerConfiguracionAsignacionRegular(int IdGrupoFiltroProgramaCritico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerConfiguracionAsignacionRegular(IdGrupoFiltroProgramaCritico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerBloquePorProgramaCritico")]
        public IActionResult ObtenerBloquePorProgramaCritico()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerBloquePorProgramaCritico();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerCategoriaOrigen")]
        public IActionResult ObtenerCategoriaOrigen()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerCategoriaOrigen();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerCategoriaOrigenPorSector/{Id}")]
        public IActionResult ObtenerCategoriaOrigenPorSector(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerCategoriaOrigenPorSector(Id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para el tab de programas otras areas
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerConfiguracionProgramasOtrasAreas/{IdGrupoFiltroProgramaCritico}")]
        public IActionResult ObtenerConfiguracionProgramasOtrasAreas(int IdGrupoFiltroProgramaCritico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerConfiguracionProgramasOtrasAreas(IdGrupoFiltroProgramaCritico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Programas Generales habilitados
        /// </summary>
        /// <returns>Retorna 200 mas la lista de programas generales</returns>
        [HttpGet("ObtenerComboListaProgramasGenerales")]
        public IActionResult ObtenerComboListaProgramasGenerales()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerComboListaProgramasGenerales();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarProgramasOtrasAreas")]
        public IActionResult ActualizarProgramasOtrasAreas([FromBody] List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);

                var respuesta = servicio.ActualizarProgramasOtrasAreas(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarAsignacionRegular")]
        public IActionResult ActualizarAsignacionRegular([FromBody] List<ConfiguracionPrincipalDTO> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarAsignacionRegular(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para el tab de programas otras areas
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerComboBusqueda")]
        public IActionResult ObtenerComboBusqueda()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerComboBusqueda();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para el tab de programas otras areas
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("BuscarPorComboSeleccionadosProgramasOtrasAreas/{IdProgramasGeneral}/{IdGrupoFiltroProgramaCritico}/{IdAsesor}/{IdCoordinador}")]
        public IActionResult BuscarPorComboSeleccionadosProgramasOtrasAreas(int IdProgramasGeneral, int IdGrupoFiltroProgramaCritico, int IdAsesor, int IdCoordinador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.BuscarPorComboSeleccionadosProgramasOtrasAreas(IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para el tab de programas otras areas
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("BuscarPorComboSeleccionadosProgramasCriticos/{IdProgramasGeneral}/{IdGrupoFiltroProgramaCritico}/{IdAsesor}/{IdCoordinador}")]
        public IActionResult BuscarPorComboSeleccionadosProgramasCriticos(int IdProgramasGeneral, int IdGrupoFiltroProgramaCritico, int IdAsesor, int IdCoordinador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.BuscarPorComboSeleccionadosProgramasCriticos(IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para el tab de programas otras areas
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("AsignacionAutomatizada")]
        public IActionResult AsignacionAutomatizadaDeAsesor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerComboBusqueda();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10s/2022
        /// Versión: 1.0
        /// <summary>
        /// Inicia el proceso de asignacion automatizada de asesores
        /// </summary>
        /// <returns>Retorna 200</returns>
        [HttpGet("AsignacionAutomatizadaAsesorV1")]
        public IActionResult AsignacionAutomatizadaAsesorV1()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                //var servicio = new AsignacionRegularService(unitOfWork);
                var servicio = new AsignacionRegularService(unitOfWork);

                bool exitoPrimerProceso = servicio.AsignacionAutomatizadaAsesorWhatsapp(usuario);
                bool exitoSegundaProceso = servicio.AsignacionAutomatizadaAsesor(usuario);
                return Ok("Asignaciones automatizadas ejecutadas correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AsignacionAutomatizadaAsesor")]
        public IActionResult AsignacionAutomatizadaAsesor()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                Task.Run(async () =>

                {

                    using (var scope = _scopeFactory.CreateScope())
                    {

                        var unitOfWorkEnScope = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var servicioEnScope = new AsignacionRegularAutomaticaService(unitOfWorkEnScope);

                        try
                        {

                            bool resultado = await servicioEnScope.EjecutarAsignacionDatosAutomatizada(usuario);

                            if (resultado)
                                servicioEnScope.EnvioCorreoValidado(" La asignación de datos finalizó correctamente.");
                            else
                                servicioEnScope.EnvioCorreoValidado(" La asignación terminó con errores o no pudo ejecutarse.");
                        }
                        catch (Exception ex)
                        {

                            servicioEnScope.EnvioCorreoValidado($" Ocurrió un error interno en segundo plano: {ex.Message}");
                        }

                    }
                });


                return Ok(new { mensaje = "La asignación fue iniciada y se está ejecutando en segundo plano." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("AsignacionAutoAsesor")]
        public IActionResult AsignacionAutoAsesor()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                Task.Run(async () =>
                {

                    using (var scope = _scopeFactory.CreateScope())
                    {

                        var unitOfWorkEnScope = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var servicioEnScope = new AsignacionRegularAutomaticaService(unitOfWorkEnScope);

                        try
                        {

                            bool resultado = await servicioEnScope.EjecutarAsignacionDatosAuto(usuario);

                            if (resultado)
                                servicioEnScope.EnvioCorreoValidado(" La asignación de datos finalizó correctamente.");
                            else
                                servicioEnScope.EnvioCorreoValidado(" La asignación terminó con errores o no pudo ejecutarse.");
                        }
                        catch (Exception ex)
                        {

                            servicioEnScope.EnvioCorreoValidado($" Ocurrió un error interno en segundo plano: {ex.Message}");
                        }

                    }
                });


                return Ok(new { mensaje = "La asignación fue iniciada y se está ejecutando en segundo plano." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        /// <summary>
        /// Inicia el proceso de asignación automática de asesores.
        /// Este endpoint está destinado exclusivamente para ser llamado por un servicio automático
        /// y no debe ser utilizado para otros fines.
        /// </summary>
        /// <remarks>
        /// Autor: Joseph Llanque.
        /// Fecha: 25-07-2025.
        /// </remarks>
        [AllowAnonymous]
        [HttpGet("AsignacionAutomaticaWorker")]
        public IActionResult AsignacionAutomaticaWorker()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string usuario = "AsignacionAutomatica-Service";
                Task.Run(async () =>
                {

                    using (var scope = _scopeFactory.CreateScope())
                    {

                        var unitOfWorkEnScope = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var servicioEnScope = new AsignacionRegularAutomaticaService(unitOfWorkEnScope);

                        try
                        {

                            bool resultado = await servicioEnScope.EjecutarAsignacionDatosAuto(usuario);

                            if (resultado)
                                servicioEnScope.EnvioCorreoValidado(" La asignación de datos finalizó correctamente.");
                            else
                                servicioEnScope.EnvioCorreoValidado(" La asignación terminó con errores o no pudo ejecutarse.");
                        }
                        catch (Exception ex)
                        {

                            servicioEnScope.EnvioCorreoValidado($" Ocurrió un error interno en segundo plano: {ex.Message}");
                        }

                    }
                });


                return Ok(new { mensaje = "La asignación fue iniciada y se está ejecutando en segundo plano." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }









        [HttpGet("AsignacionAutomatizadaAsesorWhats")]
        public IActionResult AsignacionAutomatizadaAsesorWhats()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                Task.Run(async () =>
                {
                    try
                    {
                        var usuario = "SYSTEMV5";
                        var servicio = new AsignacionRegularAutomaticaService(unitOfWork);
                        await servicio.EjecutarAsignacionDatosAutomatizada(usuario);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($" Error en ejecución automática: {ex.Message}");
                    }
                });

                return Ok(new { mensaje = "Proceso de asignación iniciado en segundo plano." });
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Error al iniciar asignación: {ex.Message}");
            }
        }



        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerListaAsesor")]
        public IActionResult ObtenerListaAsesor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerListaAsesor();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerAsesorConfiguracion/{id}")]
        public IActionResult ObtenerAsesorConfiguracion(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerAsesorConfiguracion(id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerAsesorConfiguracionPorPais/{id}")]
        public IActionResult ObtenerAsesorConfiguracionPorPais(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerAsesorConfiguracionPorPais(id);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones para la asignacion regular 
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerComboAsesores")]
        public IActionResult ObtenerComboAsesores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerComboAsesores();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarAsignacionRegular")]
        public IActionResult InsertarAsignacionRegular([FromBody] List<ConfiguracionPrincipalDTO> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarAsignacionRegular(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Insertar categoria origen por sector
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarCategoriaOrigenPorSector/{Id}")]
        public IActionResult InsertarCategoriaOrigenPorSector([FromBody] ListaCategoriaOrigenDTO listado, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.InsertarCategoriaOrigenPorSector(Id, listado, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarAsesorAsignacionRegular")]
        public IActionResult InsertarAsesorAsignacionRegular([FromBody] InsertarAsignacionRegularDTO listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.InsertarAsignacionRegular(listado, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarOrigenSector")]
        public IActionResult InsertarOrigenSector([FromBody] InsertarOrigenSectorDTO OrigenSector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.InsertarOrigenSector(OrigenSector, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("EliminarOrigenSectorPorParametro/{Id}")]
        public IActionResult EliminarOrigenSectorPorParametro(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.EliminarOrigenSector(Id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarConfiguracionAsignacionRegular/{id}")]
        public IActionResult InsertarConfiguracionAsignacionRegular(int id, [FromBody] InsertarProgramaGeneralAsignacionRegularDTO ListaIdAsignacionRegular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.InsertarConfiguracionAsignacionRegular(id, ListaIdAsignacionRegular, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarConfiguracionAsignacionRegular")]
        public IActionResult ActualizarConfiguracionAsignacionRegular([FromBody] List<ObtenerAsesorConfiguracionPorPaisDTO> ListaConfiguracionAsignacionRegular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarConfiguracionAsignacionRegular(ListaConfiguracionAsignacionRegular, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminar la asignación regular seleccionada 
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("EliminarAsignacionRegular/{id}")]
        public IActionResult EliminarAsignacionRegular(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.EliminarAsignacionRegular(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminar la asignación regular seleccionada 
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("EliminarPaisConfiguracionAsignacionRegular/{id}")]
        public IActionResult EliminarPaisConfiguracionAsignacionRegular(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.EliminarPaisConfiguracionAsignacionRegular(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminar la asignación regular seleccionada 
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActivarAsignacionAutomatica/{id}/{activarAsignacionAutomatica}")]
        public IActionResult ActivarAsignacionAutomatica(int id, bool activarAsignacionAutomatica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActivarAsignacionAutomatica(id, activarAsignacionAutomatica, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminar la asignación regular seleccionada 
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("AgruparCategoriaOrigen/{id}/{agruparCategoriaOrigen}")]
        public IActionResult AgruparCategoriaOrigen(int id, bool agruparCategoriaOrigen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.AgruparCategoriaOrigen(agruparCategoriaOrigen, id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminar la asignación regular seleccionada 
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("EliminarConfiguracionCategoriaOrigen/{id}")]
        public IActionResult EliminarConfiguracionCategoriaOrigen(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.EliminarConfiguracionCategoriaOrigen(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminar la asignación regular seleccionada 
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarTopeOportunidad/{id}/{TopeOportunidad}")]
        public IActionResult ActualizarTopeOportunidad(int id, int TopeOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarTopeOportunidad(id, TopeOportunidad, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Autor: Miguel Valdivia
        /// Fecha: 27/08/2025
        /// Version: 1.0
        /// Actualiza el tope de asignación diaria
        /// </summary>
        /// <param name="id">ID de la asignación regular</param>
        /// <param name="TopeAsignacionDiaria">Nuevo tope de asignación diaria</param>
        /// <returns>IActionResult</returns>
        [HttpPost("ActualizarTopeAsignacionDiaria/{id}/{TopeAsignacionDiaria}")]
        public IActionResult ActualizarTopeAsignacionDiaria(int id, int TopeAsignacionDiaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarTopeAsignacionDiaria(id, TopeAsignacionDiaria, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualizar prioridad de asignación
        /// </summary>
        /// <param name="listado">Id Asignacion Regular para eliminar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarPrioridad/{id}/{Prioridad}")]
        public IActionResult ActualizarPrioridad(int id, int Prioridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarPrioridad(id, Prioridad, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 29/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el valor de AsignacionPais del asesor a partir del IdAsignacionRegular
        /// </summary>
        /// <param name="IdAsignacionRegular">Id del registro en T_AsignacionRegular</param>
        /// <returns>Retorna 200 con el AsignacionPais o 400 y mensaje de error</returns>
        [HttpGet("ObtenerAsignacionPaisAsesor/{IdAsignacionRegular}")]
        public IActionResult ObtenerAsignacionPaisAsesor(int IdAsignacionRegular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ObtenerAsignacionPaisAsesor(IdAsignacionRegular);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 29/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el valor de asingacionPais para un asesor en asignación regular
        /// </summary>
        /// <param name="request">Objeto con IdAsignacionRegular y AsignacionPais</param>
        /// <returns>Retorna 200 con resultado o 400 y mensaje de error</returns>
        [HttpPost("ActualizarAsignacionPaisAsesor")]
        public IActionResult ActualizarAsignacionPaisAsesor([FromBody] ActualizarAsignacionPaisAsesorDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new AsignacionRegularService(unitOfWork);
                var respuesta = servicio.ActualizarAsignacionPaisAsesor(request, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

