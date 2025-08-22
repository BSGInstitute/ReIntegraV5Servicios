using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FormularioProgresivoController
    /// Autor: Jorge Gamero.
    /// Fecha: 04/11/2024
    /// <summary>
    /// Gestión de Formulario Progresivo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioProgresivoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioProgresivoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 05/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="FormularioProgresivoEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: FormularioProgresivo </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioProgresivoEntradaDTO formularioProgresivoEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var formularioProgresivo = new FormularioProgresivo();
                var formularioProgresivoConfiguracionBotonService = new FormularioProgresivoConfiguracionBotonService(unitOfWork);
                //var formularioProgresivoConfiguracionBoton = new FormularioProgresivoConfiguracionBoton();
                formularioProgresivo.Nombre = formularioProgresivoEntradaDTO.Nombre;
                formularioProgresivo.Descripcion = formularioProgresivoEntradaDTO.Descripcion;
                formularioProgresivo.Tipo = formularioProgresivoEntradaDTO.Tipo;
                formularioProgresivo.Activado = formularioProgresivoEntradaDTO.Activado;
                formularioProgresivo.IdFormularioProgresivoInicial = formularioProgresivoEntradaDTO.IdFormularioProgresivoInicial;
                formularioProgresivo.CondicionMostrar = formularioProgresivoEntradaDTO.CondicionMostrar;
                formularioProgresivo.TiempoProgramasPublicidad = formularioProgresivoEntradaDTO.TiempoProgramasPublicidad;
                formularioProgresivo.Titulo = formularioProgresivoEntradaDTO.Titulo;
                formularioProgresivo.TituloTexto = formularioProgresivoEntradaDTO.TituloTexto;
                formularioProgresivo.CabeceraMensajeSup = formularioProgresivoEntradaDTO.CabeceraMensajeSup;
                formularioProgresivo.CabeceraMensajeSupTexto = formularioProgresivoEntradaDTO.CabeceraMensajeSupTexto;
                formularioProgresivo.CabeceraMensaje = formularioProgresivoEntradaDTO.CabeceraMensaje;
                formularioProgresivo.CabeceraMensajeIndexCurso = formularioProgresivoEntradaDTO.CabeceraMensajeIndexCurso;
                formularioProgresivo.CabeceraMensajeTexto = formularioProgresivoEntradaDTO.CabeceraMensajeTexto;
                formularioProgresivo.CabeceraMensajeTextoCurso = formularioProgresivoEntradaDTO.CabeceraMensajeTextoCurso;
                formularioProgresivo.CabeceraMensajeBordes = formularioProgresivoEntradaDTO.CabeceraMensajeBordes;
                formularioProgresivo.CabeceraMensajeInf = formularioProgresivoEntradaDTO.CabeceraMensajeInf;
                formularioProgresivo.CabeceraMensajeInfIndexCurso = formularioProgresivoEntradaDTO.CabeceraMensajeInfIndexCurso;
                formularioProgresivo.CabeceraMensajeInfTexto = formularioProgresivoEntradaDTO.CabeceraMensajeInfTexto;
                formularioProgresivo.CabeceraMensajeInfTextoCurso = formularioProgresivoEntradaDTO.CabeceraMensajeInfTextoCurso;
                formularioProgresivo.CabeceraBoton = formularioProgresivoEntradaDTO.CabeceraBoton;
                formularioProgresivo.CabeceraBotonTexto = formularioProgresivoEntradaDTO.CabeceraBotonTexto;
                formularioProgresivo.CabeceraBotonAccion = formularioProgresivoEntradaDTO.CabeceraBotonAccion;
                formularioProgresivo.CuerpoMensajeSup = formularioProgresivoEntradaDTO.CuerpoMensajeSup;
                formularioProgresivo.CuerpoMensajeSupTexto = formularioProgresivoEntradaDTO.CuerpoMensajeSupTexto;
                formularioProgresivo.CuerpoCorreo = formularioProgresivoEntradaDTO.CuerpoCorreo;
                formularioProgresivo.CuerpoCorreoOrden = formularioProgresivoEntradaDTO.CuerpoCorreoOrden;
                formularioProgresivo.CuerpoCorreoObl = formularioProgresivoEntradaDTO.CuerpoCorreoObl;
                formularioProgresivo.CuerpoNombres = formularioProgresivoEntradaDTO.CuerpoNombres;
                formularioProgresivo.CuerpoNombresOrden = formularioProgresivoEntradaDTO.CuerpoNombresOrden;
                formularioProgresivo.CuerpoNombresObl = formularioProgresivoEntradaDTO.CuerpoNombresObl;
                formularioProgresivo.CuerpoApellidos = formularioProgresivoEntradaDTO.CuerpoApellidos;
                formularioProgresivo.CuerpoApellidosOrden = formularioProgresivoEntradaDTO.CuerpoApellidosOrden;
                formularioProgresivo.CuerpoApellidosObl = formularioProgresivoEntradaDTO.CuerpoApellidosObl;
                formularioProgresivo.CuerpoPais = formularioProgresivoEntradaDTO.CuerpoPais;
                formularioProgresivo.CuerpoPaisOrden = formularioProgresivoEntradaDTO.CuerpoPaisOrden;
                formularioProgresivo.CuerpoPaisObl = formularioProgresivoEntradaDTO.CuerpoPaisObl;
                formularioProgresivo.CuerpoTelefono = formularioProgresivoEntradaDTO.CuerpoTelefono;
                formularioProgresivo.CuerpoTelefonoOrden = formularioProgresivoEntradaDTO.CuerpoTelefonoOrden;
                formularioProgresivo.CuerpoTelefonoObl = formularioProgresivoEntradaDTO.CuerpoTelefonoObl;
                formularioProgresivo.CuerpoCargo = formularioProgresivoEntradaDTO.CuerpoCargo;
                formularioProgresivo.CuerpoCargoOrden = formularioProgresivoEntradaDTO.CuerpoCargoOrden;
                formularioProgresivo.CuerpoCargoObl = formularioProgresivoEntradaDTO.CuerpoCargoObl;
                formularioProgresivo.CuerpoAreaFormacion = formularioProgresivoEntradaDTO.CuerpoAreaFormacion;
                formularioProgresivo.CuerpoAreaFormacionOrden = formularioProgresivoEntradaDTO.CuerpoAreaFormacionOrden;
                formularioProgresivo.CuerpoAreaFormacionObl = formularioProgresivoEntradaDTO.CuerpoAreaFormacionObl;
                formularioProgresivo.CuerpoAreaTrabajo = formularioProgresivoEntradaDTO.CuerpoAreaTrabajo;
                formularioProgresivo.CuerpoAreaTrabajoOrden = formularioProgresivoEntradaDTO.CuerpoAreaTrabajoOrden;
                formularioProgresivo.CuerpoAreaTrabajoObl = formularioProgresivoEntradaDTO.CuerpoAreaTrabajoObl;
                formularioProgresivo.CuerpoIndustria = formularioProgresivoEntradaDTO.CuerpoIndustria;
                formularioProgresivo.CuerpoIndustriaOrden = formularioProgresivoEntradaDTO.CuerpoIndustriaOrden;
                formularioProgresivo.CuerpoIndustriaObl = formularioProgresivoEntradaDTO.CuerpoIndustriaObl;
                formularioProgresivo.CuerpoMensajeInf = formularioProgresivoEntradaDTO.CuerpoMensajeInf;
                formularioProgresivo.CuerpoMensajeInfTexto = formularioProgresivoEntradaDTO.CuerpoMensajeInfTexto;
                formularioProgresivo.CuerpoBoton = formularioProgresivoEntradaDTO.CuerpoBoton;
                formularioProgresivo.CuerpoBotonTexto = formularioProgresivoEntradaDTO.CuerpoBotonTexto;
                formularioProgresivo.CuerpoBotonAccion = formularioProgresivoEntradaDTO.CuerpoBotonAccion;
                formularioProgresivo.Pie = formularioProgresivoEntradaDTO.Pie;
                formularioProgresivo.PieTexto = formularioProgresivoEntradaDTO.PieTexto;
                formularioProgresivo.Boton = formularioProgresivoEntradaDTO.Boton;
                formularioProgresivo.BotonTexto = formularioProgresivoEntradaDTO.BotonTexto;
                formularioProgresivo.BotonAccion = formularioProgresivoEntradaDTO.BotonAccion;
                formularioProgresivo.Estado = true;
                formularioProgresivo.UsuarioCreacion = formularioProgresivoEntradaDTO.Usuario;
                formularioProgresivo.UsuarioModificacion = formularioProgresivoEntradaDTO.Usuario;
                formularioProgresivo.FechaCreacion = DateTime.Now;
                formularioProgresivo.FechaModificacion = DateTime.Now;
                formularioProgresivo.TiempoProgramasOrganico = formularioProgresivoEntradaDTO.TiempoProgramasOrganico;
                formularioProgresivo.TiempoBlogsWhite = formularioProgresivoEntradaDTO.TiempoBlogsWhite;
                formularioProgresivo.TiempoIndexTags = formularioProgresivoEntradaDTO.TiempoIndexTags;
                formularioProgresivo.CabeceraMensajeTextoWhitepaper = formularioProgresivoEntradaDTO.CabeceraMensajeTextoWhitepaper;
                formularioProgresivo.CabeceraMensajeInfTextoWhitepaper = formularioProgresivoEntradaDTO.CabeceraMensajeInfTextoWhitepaper;
                formularioProgresivo.CabeceraMensajeSupIndexCurso = formularioProgresivoEntradaDTO.CabeceraMensajeSupIndexCurso;
                formularioProgresivo.CabeceraMensajeSupTextoCurso = formularioProgresivoEntradaDTO.CabeceraMensajeSupTextoCurso;
                formularioProgresivo.CabeceraMensajeSupTextoWhitepaper = formularioProgresivoEntradaDTO.CabeceraMensajeSupTextoWhitepaper;
                formularioProgresivo.CuerpoMensajeSupIndexCurso = formularioProgresivoEntradaDTO.CuerpoMensajeSupIndexCurso;
                formularioProgresivo.CuerpoMensajeSupTextoCurso = formularioProgresivoEntradaDTO.CuerpoMensajeSupTextoCurso;
                formularioProgresivo.CuerpoMensajeSupTextoWhitepaper = formularioProgresivoEntradaDTO.CuerpoMensajeSupTextoWhitepaper;
                var resultado = formularioProgresivoService.Add(formularioProgresivo);
                if (resultado != null && resultado.Any())
                {
                    var idFormularioProgresivo = resultado.First().Id;

                    foreach (var boton in formularioProgresivoEntradaDTO.ConfiguracionBoton)
                    {
                        var formularioProgresivoConfiguracionBoton = new FormularioProgresivoConfiguracionBoton
                        {
                            IdentificadorFilaGrilla = boton.IdentificadorFilaGrilla,
                            IdFormularioProgresivo = idFormularioProgresivo,
                            IdFormularioProgresivoSeccionPortal = boton.IdFormularioProgresivoSeccionPortal,
                            IdFormularioProgresivoAccionBoton = boton.IdFormularioProgresivoAccionBoton,
                            IdRegistroArchivoStorage = boton.IdRegistroArchivoStorage,
                            TextoBoton = boton.TextoBoton,
                            UsuarioCreacion = formularioProgresivoEntradaDTO.Usuario,
                            UsuarioModificacion = formularioProgresivoEntradaDTO.Usuario
                        };
                        var resultado2 = formularioProgresivoConfiguracionBotonService.Add(formularioProgresivoConfiguracionBoton);
                    }
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jorge Gamero.
        /// Fecha: 04/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="FormularioProgresivoEntradaDTO"> Datos necesarios para la actualización de datos </param>
        /// <returns> Entidad: FormularioProgresivo </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FormularioProgresivoEntradaDTO formularioProgresivoEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var formularioProgresivo = new FormularioProgresivo();
                var formularioProgresivoConfiguracionBotonService = new FormularioProgresivoConfiguracionBotonService(unitOfWork);
                formularioProgresivo = formularioProgresivoService.ObtenerPorId(formularioProgresivoEntradaDTO.Id.Value);
                formularioProgresivo.Nombre = formularioProgresivoEntradaDTO.Nombre;
                formularioProgresivo.Descripcion = formularioProgresivoEntradaDTO.Descripcion;
                formularioProgresivo.Tipo = formularioProgresivoEntradaDTO.Tipo;
                formularioProgresivo.Activado = formularioProgresivoEntradaDTO.Activado;
                formularioProgresivo.IdFormularioProgresivoInicial = formularioProgresivoEntradaDTO.IdFormularioProgresivoInicial;
                formularioProgresivo.CondicionMostrar = formularioProgresivoEntradaDTO.CondicionMostrar;
                formularioProgresivo.TiempoProgramasPublicidad = formularioProgresivoEntradaDTO.TiempoProgramasPublicidad;
                formularioProgresivo.Titulo = formularioProgresivoEntradaDTO.Titulo;
                formularioProgresivo.TituloTexto = formularioProgresivoEntradaDTO.TituloTexto;
                formularioProgresivo.CabeceraMensajeSup = formularioProgresivoEntradaDTO.CabeceraMensajeSup;
                formularioProgresivo.CabeceraMensajeSupTexto = formularioProgresivoEntradaDTO.CabeceraMensajeSupTexto;
                formularioProgresivo.CabeceraMensaje = formularioProgresivoEntradaDTO.CabeceraMensaje;
                formularioProgresivo.CabeceraMensajeIndexCurso = formularioProgresivoEntradaDTO.CabeceraMensajeIndexCurso;
                formularioProgresivo.CabeceraMensajeTexto = formularioProgresivoEntradaDTO.CabeceraMensajeTexto;
                formularioProgresivo.CabeceraMensajeTextoCurso = formularioProgresivoEntradaDTO.CabeceraMensajeTextoCurso;
                formularioProgresivo.CabeceraMensajeBordes = formularioProgresivoEntradaDTO.CabeceraMensajeBordes;
                formularioProgresivo.CabeceraMensajeInf = formularioProgresivoEntradaDTO.CabeceraMensajeInf;
                formularioProgresivo.CabeceraMensajeInfIndexCurso = formularioProgresivoEntradaDTO.CabeceraMensajeInfIndexCurso;
                formularioProgresivo.CabeceraMensajeInfTexto = formularioProgresivoEntradaDTO.CabeceraMensajeInfTexto;
                formularioProgresivo.CabeceraMensajeInfTextoCurso = formularioProgresivoEntradaDTO.CabeceraMensajeInfTextoCurso;
                formularioProgresivo.CabeceraBoton = formularioProgresivoEntradaDTO.CabeceraBoton;
                formularioProgresivo.CabeceraBotonTexto = formularioProgresivoEntradaDTO.CabeceraBotonTexto;
                formularioProgresivo.CabeceraBotonAccion = formularioProgresivoEntradaDTO.CabeceraBotonAccion;
                formularioProgresivo.CuerpoMensajeSup = formularioProgresivoEntradaDTO.CuerpoMensajeSup;
                formularioProgresivo.CuerpoMensajeSupTexto = formularioProgresivoEntradaDTO.CuerpoMensajeSupTexto;
                formularioProgresivo.CuerpoCorreo = formularioProgresivoEntradaDTO.CuerpoCorreo;
                formularioProgresivo.CuerpoCorreoOrden = formularioProgresivoEntradaDTO.CuerpoCorreoOrden;
                formularioProgresivo.CuerpoCorreoObl = formularioProgresivoEntradaDTO.CuerpoCorreoObl;
                formularioProgresivo.CuerpoNombres = formularioProgresivoEntradaDTO.CuerpoNombres;
                formularioProgresivo.CuerpoNombresOrden = formularioProgresivoEntradaDTO.CuerpoNombresOrden;
                formularioProgresivo.CuerpoNombresObl = formularioProgresivoEntradaDTO.CuerpoNombresObl;
                formularioProgresivo.CuerpoApellidos = formularioProgresivoEntradaDTO.CuerpoApellidos;
                formularioProgresivo.CuerpoApellidosOrden = formularioProgresivoEntradaDTO.CuerpoApellidosOrden;
                formularioProgresivo.CuerpoApellidosObl = formularioProgresivoEntradaDTO.CuerpoApellidosObl;
                formularioProgresivo.CuerpoPais = formularioProgresivoEntradaDTO.CuerpoPais;
                formularioProgresivo.CuerpoPaisOrden = formularioProgresivoEntradaDTO.CuerpoPaisOrden;
                formularioProgresivo.CuerpoPaisObl = formularioProgresivoEntradaDTO.CuerpoPaisObl;
                formularioProgresivo.CuerpoTelefono = formularioProgresivoEntradaDTO.CuerpoTelefono;
                formularioProgresivo.CuerpoTelefonoOrden = formularioProgresivoEntradaDTO.CuerpoTelefonoOrden;
                formularioProgresivo.CuerpoTelefonoObl = formularioProgresivoEntradaDTO.CuerpoTelefonoObl;
                formularioProgresivo.CuerpoCargo = formularioProgresivoEntradaDTO.CuerpoCargo;
                formularioProgresivo.CuerpoCargoOrden = formularioProgresivoEntradaDTO.CuerpoCargoOrden;
                formularioProgresivo.CuerpoCargoObl = formularioProgresivoEntradaDTO.CuerpoCargoObl;
                formularioProgresivo.CuerpoAreaFormacion = formularioProgresivoEntradaDTO.CuerpoAreaFormacion;
                formularioProgresivo.CuerpoAreaFormacionOrden = formularioProgresivoEntradaDTO.CuerpoAreaFormacionOrden;
                formularioProgresivo.CuerpoAreaFormacionObl = formularioProgresivoEntradaDTO.CuerpoAreaFormacionObl;
                formularioProgresivo.CuerpoAreaTrabajo = formularioProgresivoEntradaDTO.CuerpoAreaTrabajo;
                formularioProgresivo.CuerpoAreaTrabajoOrden = formularioProgresivoEntradaDTO.CuerpoAreaTrabajoOrden;
                formularioProgresivo.CuerpoAreaTrabajoObl = formularioProgresivoEntradaDTO.CuerpoAreaTrabajoObl;
                formularioProgresivo.CuerpoIndustria = formularioProgresivoEntradaDTO.CuerpoIndustria;
                formularioProgresivo.CuerpoIndustriaOrden = formularioProgresivoEntradaDTO.CuerpoIndustriaOrden;
                formularioProgresivo.CuerpoIndustriaObl = formularioProgresivoEntradaDTO.CuerpoIndustriaObl;
                formularioProgresivo.CuerpoMensajeInf = formularioProgresivoEntradaDTO.CuerpoMensajeInf;
                formularioProgresivo.CuerpoMensajeInfTexto = formularioProgresivoEntradaDTO.CuerpoMensajeInfTexto;
                formularioProgresivo.CuerpoBoton = formularioProgresivoEntradaDTO.CuerpoBoton;
                formularioProgresivo.CuerpoBotonTexto = formularioProgresivoEntradaDTO.CuerpoBotonTexto;
                formularioProgresivo.CuerpoBotonAccion = formularioProgresivoEntradaDTO.CuerpoBotonAccion;
                formularioProgresivo.Pie = formularioProgresivoEntradaDTO.Pie;
                formularioProgresivo.PieTexto = formularioProgresivoEntradaDTO.PieTexto;
                formularioProgresivo.Boton = formularioProgresivoEntradaDTO.Boton;
                formularioProgresivo.BotonTexto = formularioProgresivoEntradaDTO.BotonTexto;
                formularioProgresivo.BotonAccion = formularioProgresivoEntradaDTO.BotonAccion;
                formularioProgresivo.UsuarioModificacion = formularioProgresivoEntradaDTO.Usuario;
                formularioProgresivo.FechaModificacion = DateTime.Now;
                formularioProgresivo.TiempoProgramasOrganico = formularioProgresivoEntradaDTO.TiempoProgramasOrganico;
                formularioProgresivo.TiempoBlogsWhite = formularioProgresivoEntradaDTO.TiempoBlogsWhite;
                formularioProgresivo.TiempoIndexTags = formularioProgresivoEntradaDTO.TiempoIndexTags;
                formularioProgresivo.CabeceraMensajeTextoWhitepaper = formularioProgresivoEntradaDTO.CabeceraMensajeTextoWhitepaper;
                formularioProgresivo.CabeceraMensajeInfTextoWhitepaper = formularioProgresivoEntradaDTO.CabeceraMensajeInfTextoWhitepaper;
                formularioProgresivo.CabeceraMensajeSupIndexCurso = formularioProgresivoEntradaDTO.CabeceraMensajeSupIndexCurso;
                formularioProgresivo.CabeceraMensajeSupTextoCurso = formularioProgresivoEntradaDTO.CabeceraMensajeSupTextoCurso;
                formularioProgresivo.CabeceraMensajeSupTextoWhitepaper = formularioProgresivoEntradaDTO.CabeceraMensajeSupTextoWhitepaper;
                formularioProgresivo.CuerpoMensajeSupIndexCurso = formularioProgresivoEntradaDTO.CuerpoMensajeSupIndexCurso;
                formularioProgresivo.CuerpoMensajeSupTextoCurso = formularioProgresivoEntradaDTO.CuerpoMensajeSupTextoCurso;
                formularioProgresivo.CuerpoMensajeSupTextoWhitepaper = formularioProgresivoEntradaDTO.CuerpoMensajeSupTextoWhitepaper;
                var resultado = formularioProgresivoService.Update(formularioProgresivo);
                if (resultado != null && resultado.Any())
                {
                    var idFormularioProgresivo = resultado.First().Id;

                    var existingItems = formularioProgresivoConfiguracionBotonService.ObtenerPorIdFormularioProgresivo(idFormularioProgresivo).ToDictionary(x => x.IdentificadorFilaGrilla, x => x.Id);

                    var receivedIds = formularioProgresivoEntradaDTO.ConfiguracionBoton.Select(b => b.IdentificadorFilaGrilla).ToList();

                    foreach (var boton in formularioProgresivoEntradaDTO.ConfiguracionBoton)
                    {
                        var formularioProgresivoConfiguracionBoton = new FormularioProgresivoConfiguracionBoton
                        {
                            IdentificadorFilaGrilla = boton.IdentificadorFilaGrilla,
                            IdFormularioProgresivo = idFormularioProgresivo,
                            IdFormularioProgresivoSeccionPortal = boton.IdFormularioProgresivoSeccionPortal,
                            IdFormularioProgresivoAccionBoton = boton.IdFormularioProgresivoAccionBoton,
                            IdRegistroArchivoStorage = boton.IdRegistroArchivoStorage,
                            TextoBoton = boton.TextoBoton,
                            UsuarioCreacion = formularioProgresivoEntradaDTO.Usuario,
                            UsuarioModificacion = formularioProgresivoEntradaDTO.Usuario
                        };

                        if (existingItems.ContainsKey(boton.IdentificadorFilaGrilla))
                        {
                            var existingId = existingItems[boton.IdentificadorFilaGrilla];
                            formularioProgresivoConfiguracionBoton.Id = (int)existingId;
                            formularioProgresivoConfiguracionBotonService.Update(formularioProgresivoConfiguracionBoton);
                        }
                        else
                        {
                            formularioProgresivoConfiguracionBotonService.Add(formularioProgresivoConfiguracionBoton);
                        }

                    }

                    var idsToDelete = existingItems.Where(item => !receivedIds.Contains(item.Key)).Select(item => item.Value).ToList();
                    foreach (var idToDelete in idsToDelete)
                    {
                        formularioProgresivoConfiguracionBotonService.Delete(idToDelete, formularioProgresivoEntradaDTO.Usuario);
                    }
                }
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jorge Gamero.
        /// Fecha: 10/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza únicamente el estado de activado de un registro.
        /// </summary>
        /// <param name="id">ID del registro a actualizar</param>
        /// <param name="activado">Nuevo estado del campo "Activado"</param>
        /// <returns>Mensaje de éxito o error</returns>
        [HttpPut("ActualizarActivado")]
        public IActionResult ActualizarActivado([FromBody] ActualizaActivadoDTO dato)
        {
            if (dato.Id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido.");
            }
            try
            {
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var formularioProgresivo = formularioProgresivoService.ObtenerPorId(dato.Id);
                if (formularioProgresivo == null)
                {
                    return NotFound("No se encontró el registro con el ID proporcionado.");
                }
                formularioProgresivo.Activado = dato.Activado;
                formularioProgresivo.UsuarioModificacion = dato.Usuario;
                formularioProgresivo.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoService.Update(formularioProgresivo);
                if (resultado != null && resultado.Count > 0)
                {
                    return Ok("El estado 'Activado' se actualizó correctamente.");
                }
                else
                {
                    return StatusCode(500, "Ocurrió un error al guardar los cambios.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jorge Gamero.
        /// Fecha: 04/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de la tabla
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificación </param>
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
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var resultado = formularioProgresivoService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 04/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<FormularioProgresivo> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRegistros()
        {
            try
            {
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var resultado = formularioProgresivoService.ObtenerRegistros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 06/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene id y nombre de todos los formularios iniciales (tipo 1)
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFormulariosIniciales()
        {
            try
            {
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var resultado = formularioProgresivoService.ObtenerFormulariosIniciales();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene id y nombre de todos los formularios iniciales (tipo 1) sin formulario de respuesta
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFormulariosInicialesSinFormularioRespuesta()
        {
            try
            {
                var formularioProgresivoService = new FormularioProgresivoService(unitOfWork);
                var resultado = formularioProgresivoService.ObtenerFormulariosInicialesSinFormularioRespuesta();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
