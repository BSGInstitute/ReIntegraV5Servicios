using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
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
    /// Controlador: CriteriosEvaluacionProgramasEspecificosController
    /// Autor:  Max Mantilla Rodríguez.
    /// Fecha: 2023-07-20
    /// <summary>
    /// Gestión de Esquemas y  Criterios de Evaluación para Programas Específicos
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriteriosEvaluacionProgramasEspecificosController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CriteriosEvaluacionProgramasEspecificosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los filtros necesario para Esquemas de Criterios de Evaluación por programa específico
        /// </summary>
        /// <returns> CriteriosEvaluacionProgramasEspecificosDTO </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerFiltroCombos()
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var areaCapacitacionRepositorio = new AreaCapacitacionService(unitOfWork);
                        var subAreaCapacitacionRepositorio = new SubAreaCapacitacionService(unitOfWork);
                        var programaGeneralService = new PGeneralService(unitOfWork);
                        var programaEspecificoService = new PEspecificoService(unitOfWork);
                        var centroCostoService = new CentroCostoService(unitOfWork);
                        var estadoProgramaEspecificoService = new EstadoProgramaEspecificoService(unitOfWork);
                        var ciudadService = new CiudadService(unitOfWork);


                        CriteriosEvaluacionProgramasEspecificosDTO ListaComboFiltros = new CriteriosEvaluacionProgramasEspecificosDTO();
                        ListaComboFiltros.listaArea = areaCapacitacionRepositorio.ObtenerFiltro();
                        ListaComboFiltros.listaSubArea = subAreaCapacitacionRepositorio.ObtenerCombo();
                        ListaComboFiltros.listaProgramaGeneral = programaGeneralService.ObtenerProgramaGeneralPadre(null);
                        ListaComboFiltros.listaProgramaEspecifico = programaEspecificoService.ObtenerProgramasEspecificosPadres(null);
                        ListaComboFiltros.listaCentroCosto = centroCostoService.ObtenerCentroCostoPadres(null);
                        ListaComboFiltros.listaCentroCostoPersonalizado = centroCostoService.ObtenerCombo();
                        ListaComboFiltros.listaEstadoProgramaEspecifico = estadoProgramaEspecificoService.ObtenerEstadoPespecificoParaCombo();
                        ListaComboFiltros.listaCiudad = ciudadService.ObtenerListaCiudadesBs();
                        return Ok(ListaComboFiltros);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el filtros necesario para Esquemas de evaluación.
        /// </summary>
        /// <returns> List<EsquemaEvaluacionComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboEsquemaEvaluacion()
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var esquemaEvaluacionService = new EsquemaEvaluacionService(unitOfWork);
                        var ComboEsquemaEvaluacion = esquemaEvaluacionService.ObtenerComboEsquemaEvaluacion();
                        return Ok(ComboEsquemaEvaluacion);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de los registros de programas específicos para asociación con esquemas
        /// </summary>
        /// <returns> IEnumerable<DatosListaPespecificoEsquemaDTO> </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerProgramasEspecificoEsquemasFiltrosPadre([FromBody] FiltroProgramaEspecificoEsquemaFiltroCompuestoDTO Filtro)
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var servicio = new CriteriosEvaluacionProgramasEspecificosService(unitOfWork);
                        var Registros = servicio.ObtenerProgramasEspecificoEsquemasFiltrosPadreIndividual(Filtro);
                        var Total = Registros.Count() == 0 ? 0 : Registros.FirstOrDefault().TotalRegistros;
                        return Ok(new { data = Registros, Total });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los criterios de evaluación por IdEsquemaEvaluación
        /// </summary>
        /// <returns> List<EsquemaCriterioEvaluacionDTO> </returns>
        [HttpGet("[Action]/{idEsquemaEvaluacion}")]
        public IActionResult ObtenerCriterioEvaluacionPorIdEsquema(int idEsquemaEvaluacion)
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var servicio = new EsquemaEvaluacionService(unitOfWork);
                        var Registros = servicio.ObtenerCriterioEvaluacionPorIdEsquema(idEsquemaEvaluacion);
                        return Ok(Registros);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene esquema de evaluación por IdProgramaEspecifico
        /// </summary>
        /// <returns> List<EsquemaCriterioEvaluacionDTO> </returns>
        [HttpGet("[Action]/{idProgramaEspecifico}")]
        public IActionResult ObtenerEsquemaPorIdPEspecifico(int idProgramaEspecifico)
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var servicio = new CriteriosEvaluacionProgramasEspecificosService(unitOfWork);
                        var Registros = servicio.ObtenerEsquemaPorIdPEspecifico(idProgramaEspecifico);
                        return Ok(Registros);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Creacion de registros de PEspecificoEsquema
        /// </summary>
        /// <returns>  </returns>
        [HttpPost("[Action]")]
        public IActionResult CrearEsquemaEvaluacionProgramaEspecifico([FromBody] EsquemaeEvaluacionProgramaEspecificoCreacionDTO EsquemaProgramaEspecifico)
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        var servicioEsquemaEvaluacion = new CriteriosEvaluacionProgramasEspecificosService(unitOfWork);
                        var servicioCriterioEvaluacion = new PEspecificoCriterioEvaluacionService(unitOfWork);
                        PEspecificoEsquema NuevaAsociacion = new PEspecificoEsquema
                        {
                            IdPEspecifico = EsquemaProgramaEspecifico.asociacionEsquema.IdProgramaEspecifico,
                            IdEsquemaEvaluacion = EsquemaProgramaEspecifico.asociacionEsquema.IdEsquemaEvaluacion,
                            Estado = true,
                            UsuarioCreacion = Usuario,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        var EsquemaEspecificoInsert = servicioEsquemaEvaluacion.Add(NuevaAsociacion);
                        foreach (var criterio in EsquemaProgramaEspecifico.criterios)
                        {
                            PEspecificoCriterioEvaluacion CriterioEvaluacion = new PEspecificoCriterioEvaluacion
                            {
                                IdPEspecificoEsquema = EsquemaEspecificoInsert.Id,
                                IdPEspecifico = EsquemaProgramaEspecifico.asociacionEsquema.IdProgramaEspecifico,
                                IdCriterioEvaluacion = criterio.IdCriterioEvaluacion,
                                Ponderacion = criterio.Ponderacion,
                                Estado = true,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            servicioCriterioEvaluacion.Add(CriterioEvaluacion);
                        }
                        return Ok(EsquemaEspecificoInsert);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualización de registros de PEspecificoEsquema
        /// </summary>
        /// <returns>  </returns>
        [HttpPost("[Action]")]
        public IActionResult ActualizarEsquemaEvaluacionProgramaEspecifico([FromBody] EsquemaeEvaluacionProgramaEspecificoCreacionDTO EsquemaProgramaEspecifico)
        {
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    try
                    {
                        var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        var servicioEsquemaEvaluacion = new CriteriosEvaluacionProgramasEspecificosService(unitOfWork);
                        var servicioCriterioEvaluacion = new PEspecificoCriterioEvaluacionService(unitOfWork);
                        var EsquemaAsociadoExistente = unitOfWork.CriteriosEvaluacionProgramasEspecificosRepository.FirstById(EsquemaProgramaEspecifico.asociacionEsquema.Id);
                        if (EsquemaAsociadoExistente == null)
                        {
                            throw new Exception("Entidad no encontrada!");
                        }
                        else
                        {
                            //Eliminamos esquemas asociados existentes
                            servicioEsquemaEvaluacion.Delete(EsquemaAsociadoExistente.Id, Usuario);
                            PEspecificoEsquema EsquemaActual = new PEspecificoEsquema
                            {
                                IdPEspecifico = EsquemaProgramaEspecifico.asociacionEsquema.IdProgramaEspecifico,
                                IdEsquemaEvaluacion = EsquemaProgramaEspecifico.asociacionEsquema.IdEsquemaEvaluacion,
                                Estado = true,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            var esquemaUpdate = servicioEsquemaEvaluacion.Add(EsquemaActual);
                            //Eliminamos criterios asociados existentes
                            var criteriosActuales = unitOfWork.PEspecificoCriterioEvaluacionRepository.GetBy(x => x.IdPespecificoEsquema == EsquemaAsociadoExistente.Id).ToList();
                            foreach (var item in criteriosActuales)
                            {
                                servicioCriterioEvaluacion.Delete(item.Id, Usuario);
                            }
                            foreach (var criterio in EsquemaProgramaEspecifico.criterios)
                            {
                                PEspecificoCriterioEvaluacion CriterioEvaluacion = new PEspecificoCriterioEvaluacion
                                {
                                    IdPEspecificoEsquema = esquemaUpdate.Id,
                                    IdPEspecifico = EsquemaProgramaEspecifico.asociacionEsquema.IdProgramaEspecifico,
                                    IdCriterioEvaluacion = criterio.IdCriterioEvaluacion,
                                    Ponderacion = criterio.Ponderacion,
                                    Estado = true,
                                    UsuarioCreacion = Usuario,
                                    UsuarioModificacion = Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                servicioCriterioEvaluacion.Add(CriterioEvaluacion);
                            }
                            return Ok(esquemaUpdate);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

    }
}
