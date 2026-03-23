using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class GestionDocenteFlujoService : IGestionDocenteFlujoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGestionDocenteActividadService _actividadService;

        public GestionDocenteFlujoService(IUnitOfWork unitOfWork, IGestionDocenteActividadService actividadService)
        {
            _unitOfWork = unitOfWork;
            _actividadService = actividadService;
        }

        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo flujo docente y opcionalmente asocia una actividad cabecera al flujo.
        /// </summary>
        /// <param name="dto">DTO con los datos del flujo a crear.</param>
        /// <returns>Id del flujo creado.</returns>
        public async Task<int> InsertarAsync(GestionDocenteFlujoDTO dto)
        {
            try
            {
                var entidad = new GestionDocenteFlujo
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteEstado = dto.IdGestionDocenteEstado,
                    IdGestionDocenteCategoria = dto.IdGestionDocenteCategoria,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var model = _unitOfWork.GestionDocenteFlujoRepository.Add(entidad);
                await _unitOfWork.CommitAsync();

                // Asociar actividad cabecera individual (uso legado)
                if (dto.IdGestionDocenteActividadCabecera.HasValue)
                {
                    var asociacion = new GestionDocenteActividadCabeceraFlujo
                    {
                        IdGestionDocenteFlujo = model.Id,
                        IdGestionDocenteActividadCabecera = dto.IdGestionDocenteActividadCabecera.Value,
                        Estado = true,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Add(asociacion);
                    await _unitOfWork.CommitAsync();
                }

                // Asociar lista de actividades al flujo si se proporcionan
                if (dto.ActividadesIds != null && dto.ActividadesIds.Count > 0)
                {
                    foreach (var actividadId in dto.ActividadesIds)
                    {
                        var asociacion = new GestionDocenteActividadCabeceraFlujo
                        {
                            IdGestionDocenteFlujo = model.Id,
                            IdGestionDocenteActividadCabecera = actividadId,
                            Estado = true,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Add(asociacion);
                    }
                    await _unitOfWork.CommitAsync();
                }

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos de un flujo docente existente.
        /// </summary>
        /// <param name="dto">DTO con los datos actualizados del flujo.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> ActualizarAsync(GestionDocenteFlujoDTO dto)
        {
            try
            {
                // Cargar la entidad existente para preservar FechaCreacion, UsuarioCreacion y Estado
                var existente = _unitOfWork.GestionDocenteFlujoRepository
                    .GetAll().FirstOrDefault(x => x.Id == dto.Id);

                var entidad = new GestionDocenteFlujo
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteEstado = dto.IdGestionDocenteEstado,
                    IdGestionDocenteCategoria = dto.IdGestionDocenteCategoria,
                    Estado = existente != null ? existente.Estado : dto.Estado,
                    UsuarioCreacion = existente != null ? existente.UsuarioCreacion : dto.Usuario,
                    FechaCreacion = existente != null ? existente.FechaCreacion : DateTime.Now,
                    UsuarioModificacion = dto.Usuario,
                    FechaModificacion = DateTime.Now
                };

                _unitOfWork.GestionDocenteFlujoRepository.Update(entidad);
                await _unitOfWork.CommitAsync();

                // Asociar actividades al flujo si se proporcionan
                if (dto.ActividadesIds != null && dto.ActividadesIds.Count > 0)
                {
                    foreach (var actividadId in dto.ActividadesIds)
                    {
                        var asociacion = new GestionDocenteActividadCabeceraFlujo
                        {
                            IdGestionDocenteFlujo = dto.Id,
                            IdGestionDocenteActividadCabecera = actividadId,
                            Estado = true,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Add(asociacion);
                    }
                    await _unitOfWork.CommitAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente un flujo docente.
        /// </summary>
        /// <param name="id">Identificador del flujo a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la operación.</param>
        /// <returns>True si la eliminación fue exitosa.</returns>
        public async Task<bool> EliminarAsync(int id, string usuario)
        {
            try
            {
                _unitOfWork.GestionDocenteFlujoRepository.Delete(id, usuario);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los flujos docentes activos y los mapea a DTOs.
        /// </summary>
        /// <returns>Lista de GestionDocenteFlujoDTO.</returns>
        public async Task<List<GestionDocenteFlujoDTO>> ObtenerTodoAsync()
        {
            try
            {
                var lista = _unitOfWork.GestionDocenteFlujoRepository.GetAll();
                return lista.Select(x => new GestionDocenteFlujoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    IdGestionDocenteEstado = x.IdGestionDocenteEstado,
                    IdGestionDocenteCategoria = x.IdGestionDocenteCategoria,
                    Estado = x.Estado
                }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el catálogo de estados de flujo docente.
        /// </summary>
        /// <returns>Colección de GestionDocenteEstadoDTO.</returns>
        public IEnumerable<GestionDocenteEstadoDTO> ObtenerEstadosFlujo()
        {
            try
            {
                return _unitOfWork.GestionDocenteFlujoRepository.ObtenerEstadosFlujo();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el catálogo de categorías de flujo docente.
        /// </summary>
        /// <returns>Colección de GestionDocenteCategoriaDTO.</returns>
        public IEnumerable<GestionDocenteCategoriaDTO> ObtenerCategorias()
        {
            try
            {
                return _unitOfWork.GestionDocenteFlujoRepository.ObtenerCategorias();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las actividades cabecera disponibles para asociar a flujos.
        /// </summary>
        /// <param name="idCategoria">Identificador opcional de la categoría docente para filtrar.</param>
        /// <returns>Colección de GestionDocenteActividadCabeceraListaDTO.</returns>
        public IEnumerable<GestionDocenteActividadCabeceraListaDTO> ObtenerActividadesCabecera(int? idCategoria = null)
        {
            try
            {
                return _unitOfWork.GestionDocenteFlujoRepository.ObtenerActividadesCabecera(idCategoria);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un flujo docente por su identificador con nombres de estado y categoría resueltos.
        /// </summary>
        /// <param name="id">Identificador del flujo.</param>
        /// <returns>GestionDocenteFlujoOutputDTO con los datos del flujo.</returns>
        public GestionDocenteFlujoOutputDTO ObtenerFlujoPorId(int id)
        {
            try
            {
                return _unitOfWork.GestionDocenteFlujoRepository.ObtenerFlujoPorId(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un flujo completo con todas sus actividades cabecera asociadas,
        /// incluyendo detalles, disparadores, reglas, sesiones, ocurrencias, configuración IA y ejemplos.
        /// </summary>
        /// <param name="id">Identificador del flujo.</param>
        /// <returns>FlujoCompletoDTO con el flujo y sus actividades completas.</returns>
        public async Task<FlujoCompletoDTO> ObtenerFlujoCompletoAsync(int id)
        {
            try
            {
                var flujo = _unitOfWork.GestionDocenteFlujoRepository.ObtenerFlujoPorId(id);
                if (flujo == null) return null;

                var asociaciones = _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository
                    .GetBy(x => x.IdGestionDocenteFlujo == id && x.Estado)
                    .ToList();

                var actividades = new List<ActividadCabeceraCompletaDTO>();
                foreach (var asociacion in asociaciones)
                {
                    var cabecera = await _actividadService.ObtenerActividadCabeceraCompletaAsync(asociacion.IdGestionDocenteActividadCabecera);
                    if (cabecera != null)
                    {
                        actividades.Add(cabecera);
                    }
                }

                return new FlujoCompletoDTO
                {
                    Flujo = flujo,
                    Actividades = actividades
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 26/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Duplica un flujo docente completo: crea un nuevo flujo con nuevo nombre y realiza una copia profunda
        /// de todas sus actividades cabecera, detalles, disparadores, reglas, sesiones, ocurrencias,
        /// configuración IA y ejemplos de entrenamiento. Mantiene el mapeo de IDs de ocurrencias para
        /// preservar las referencias entre disparadores tipo 2 (Ocurrencia Anterior).
        /// </summary>
        /// <param name="request">DTO con el ID del flujo original, nuevo nombre y usuario.</param>
        /// <returns>ID del nuevo flujo creado.</returns>
        public async Task<int> DuplicarFlujoAsync(DuplicarFlujoRequestDTO request)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flujoCompleto = await ObtenerFlujoCompletoAsync(request.IdFlujoOriginal);
                    if (flujoCompleto == null)
                        throw new Exception($"No se encontró el flujo con ID {request.IdFlujoOriginal}");

                    DateTime fechaActual = DateTime.Now;
                    string usuario = request.Usuario;

                    // 1. Crear nuevo flujo
                    var nuevoFlujo = new GestionDocenteFlujo
                    {
                        Nombre = request.NuevoNombre,
                        Descripcion = flujoCompleto.Flujo.Descripcion,
                        IdGestionDocenteEstado = flujoCompleto.Flujo.IdGestionDocenteEstado,
                        IdGestionDocenteCategoria = flujoCompleto.Flujo.IdGestionDocenteCategoria,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };
                    var nuevoFlujoModel = _unitOfWork.GestionDocenteFlujoRepository.Add(nuevoFlujo);
                    await _unitOfWork.CommitAsync();

                    // 2. Duplicar cada actividad cabecera y asociarla al nuevo flujo
                    foreach (var actividad in flujoCompleto.Actividades)
                    {
                        int idNuevaCabecera = await DuplicarActividadCabeceraAsync(actividad, nuevoFlujoModel.Id, usuario, fechaActual);

                        var asociacion = new GestionDocenteActividadCabeceraFlujo
                        {
                            IdGestionDocenteFlujo = nuevoFlujoModel.Id,
                            IdGestionDocenteActividadCabecera = idNuevaCabecera,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = fechaActual,
                            FechaModificacion = fechaActual
                        };
                        _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Add(asociacion);
                        await _unitOfWork.CommitAsync();
                    }

                    scope.Complete();
                    return nuevoFlujoModel.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Copia en profundidad una actividad cabecera con todos sus detalles, disparadores,
        /// reglas, sesiones, ocurrencias, configuración IA y ejemplos de entrenamiento.
        /// Mantiene un mapa de IDs de ocurrencias (viejo → nuevo) para resolver referencias
        /// cruzadas en disparadores tipo 2 (Basado en Ocurrencia Anterior).
        /// </summary>
        private async Task<int> DuplicarActividadCabeceraAsync(ActividadCabeceraCompletaDTO original, int idNuevoFlujo, string usuario, DateTime fechaActual)
        {
            // Mapa: ID ocurrencia original -> ID ocurrencia nueva
            var ocurrenciaIdMap = new Dictionary<int, int>();

            // 1. Crear nueva cabecera
            var nuevaCabecera = new GestionDocenteActividadCabecera
            {
                Nombre = original.Cabecera.Nombre,
                Descripcion = original.Cabecera.Descripcion,
                IdGestionDocenteEstado = original.Cabecera.IdGestionDocenteEstado,
                IdGestionDocenteCategoria = original.Cabecera.IdGestionDocenteCategoria,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };
            var nuevaCabeceraModel = _unitOfWork.GestionDocenteActividadCabeceraRepository.Add(nuevaCabecera);
            await _unitOfWork.CommitAsync();

            // 2. Procesar cada detalle secuencialmente para poder mapear IDs de ocurrencias
            foreach (var detalleCompleto in original.Detalles)
            {
                var detalle = detalleCompleto.Detalle;
                var disparador = detalleCompleto.Disparador;
                int idTipoDisparador = disparador.DisparadorDetalle.IdGestionDocenteDisparadorFlujoTipo;

                // 2a. Crear disparador
                var nuevoDisparador = new GestionDocenteDisparadorDetalle
                {
                    IdGestionDocenteDisparadorFlujoTipo = idTipoDisparador,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var nuevoDisparadorModel = _unitOfWork.GestionDocenteDisparadorDetalleRepository.Add(nuevoDisparador);
                await _unitOfWork.CommitAsync();

                // 2b. Crear regla según tipo de disparador
                switch (idTipoDisparador)
                {
                    case 1: // Primera Actividad (Tiempo Fijo)
                        if (disparador.ReglaTiempoFijo != null)
                        {
                            int idReglaTiempoFijo = _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.ObtenerIdReglaTiempoPorTipo("FIJO");
                            var reglaFija = new GestionDocenteDisparadorReglaTiempoFijo
                            {
                                IdGestionDocenteDisparadorReglaTiempo = idReglaTiempoFijo,
                                IdGestionDocenteDisparadorDetalle = nuevoDisparadorModel.Id,
                                Fecha = disparador.ReglaTiempoFijo.Fecha,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.Add(reglaFija);
                            await _unitOfWork.CommitAsync();
                        }
                        break;

                    case 2: // Basado en Ocurrencia Anterior
                        if (disparador.ReglaTiempoRelativo != null)
                        {
                            int idReglaTiempoRelativo = _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.ObtenerIdReglaTiempoPorTipo("RELATIVO");
                            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
                            {
                                IdGestionDocenteDisparadorReglaTiempo = idReglaTiempoRelativo,
                                IdGestionDocenteDisparadorDetalle = nuevoDisparadorModel.Id,
                                Cantidad = disparador.ReglaTiempoRelativo.Cantidad,
                                IdGestionDocenteUnidadTiempo = disparador.ReglaTiempoRelativo.IdGestionDocenteUnidadTiempo,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);

                            if (disparador.OcurrenciaDetalle != null)
                            {
                                int idOcurrenciaPrevia = ocurrenciaIdMap.TryGetValue(
                                    disparador.OcurrenciaDetalle.IdGestionDocenteOcurrenciaPrevia, out int nuevoIdOcurrencia)
                                    ? nuevoIdOcurrencia
                                    : disparador.OcurrenciaDetalle.IdGestionDocenteOcurrenciaPrevia;

                                var disparadorOcurrencia = new GestionDocenteDisparadorOcurrenciaDetalle
                                {
                                    IdGestionDocenteDisparadorDetalle = nuevoDisparadorModel.Id,
                                    IdGestionDocenteOcurrenciaPrevia = idOcurrenciaPrevia,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = fechaActual,
                                    FechaModificacion = fechaActual
                                };
                                _unitOfWork.GestionDocenteDisparadorOcurrenciaDetalleRepository.Add(disparadorOcurrencia);
                            }
                            await _unitOfWork.CommitAsync();
                        }
                        break;

                    case 3: // Basado en Cronograma
                        if (disparador.ReglaTiempoRelativo != null)
                        {
                            int idReglaTiempoRelativo = _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.ObtenerIdReglaTiempoPorTipo("RELATIVO");
                            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
                            {
                                IdGestionDocenteDisparadorReglaTiempo = idReglaTiempoRelativo,
                                IdGestionDocenteDisparadorDetalle = nuevoDisparadorModel.Id,
                                Cantidad = disparador.ReglaTiempoRelativo.Cantidad,
                                IdGestionDocenteUnidadTiempo = disparador.ReglaTiempoRelativo.IdGestionDocenteUnidadTiempo,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            var reglaRelativaModel = _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);
                            await _unitOfWork.CommitAsync();

                            if (disparador.ReferenciaRelativa != null)
                            {
                                var referenciaRelativa = new GestionDocenteDisparadorReglaTiempoRelativoReferencia
                                {
                                    IdGestionDocenteDisparadorReglaTiempoRelativo = reglaRelativaModel.Id,
                                    IdGestionDocenteReferenciaTiempo = disparador.ReferenciaRelativa.IdGestionDocenteReferenciaTiempo,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = fechaActual,
                                    FechaModificacion = fechaActual
                                };
                                _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoReferenciaRepository.Add(referenciaRelativa);
                                await _unitOfWork.CommitAsync();
                            }
                        }
                        break;
                }

                // 2c. Crear detalle
                var nuevoDetalle = new GestionDocenteActividadDetalle
                {
                    IdGestionDocenteActividadCabecera = nuevaCabeceraModel.Id,
                    IdGestionDocenteActividadDetalleTipo = detalle.IdGestionDocenteActividadDetalleTipo,
                    IdPlantillaMedioComunicacion = detalle.IdPlantillaMedioComunicacion,
                    IdGestionDocenteDisparadorDetalle = nuevoDisparadorModel.Id,
                    Nombre = detalle.NombreActividadDetalle,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var nuevoDetalleModel = _unitOfWork.GestionDocenteActividadDetalleRepository.Add(nuevoDetalle);
                await _unitOfWork.CommitAsync();

                // 2d. Sesión (sólo para disparador tipo 3 - Cronograma)
                if (idTipoDisparador == 3 && detalleCompleto.Sesion != null)
                {
                    var sesion = new GestionContactoActividadDetalleSesion
                    {
                        IdGestionDocenteActividadDetalle = nuevoDetalleModel.Id,
                        IdGestionDocenteSesion = detalleCompleto.Sesion.IdGestionDocenteSesion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };
                    _unitOfWork.GestionContactoActividadDetalleSesionRepository.Add(sesion);
                    await _unitOfWork.CommitAsync();
                }

                // 2e. Crear ocurrencias y registrar mapeo de IDs
                foreach (var ocurrenciaCompleta in detalleCompleto.Ocurrencias)
                {
                    var ocurrencia = ocurrenciaCompleta.Ocurrencia;

                    var nuevaOcurrencia = new GestionDocenteOcurrencia
                    {
                        Nombre = ocurrencia.Nombre,
                        Descripcion = ocurrencia.Descripcion,
                        IdGestionDocenteOcurrenciaTipo = ocurrencia.IdGestionDocenteOcurrenciaTipo,
                        IdGestionDocenteActividadDetalle = nuevoDetalleModel.Id,
                        IdGestionDocenteModoMarcado = ocurrencia.IdGestionDocenteModoMarcado,
                        RequiereComentario = ocurrencia.RequiereComentario,
                        RequiereFechaHora = ocurrencia.RequiereFechaHora,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };
                    var nuevaOcurrenciaModel = _unitOfWork.GestionDocenteOcurrenciaRepository.Add(nuevaOcurrencia);
                    await _unitOfWork.CommitAsync();

                    // Registrar mapeo viejo ID -> nuevo ID
                    ocurrenciaIdMap[ocurrencia.IdGestionDocenteOcurrencia] = nuevaOcurrenciaModel.Id;

                    // Configuración IA (modos Automático=2 y Warm=3)
                    if ((ocurrencia.IdGestionDocenteModoMarcado == 2 || ocurrencia.IdGestionDocenteModoMarcado == 3)
                        && ocurrenciaCompleta.IaConfiguracion != null)
                    {
                        var iaConfig = new GestionDocenteOcurrenciaIaConfiguracion
                        {
                            Prompt = ocurrenciaCompleta.IaConfiguracion.Prompt,
                            IdGestionDocenteConfianzaUmbralNivel = ocurrenciaCompleta.IaConfiguracion.IdGestionDocenteConfianzaUmbralNivel,
                            IdGestionDocenteOcurrencia = nuevaOcurrenciaModel.Id,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = fechaActual,
                            FechaModificacion = fechaActual
                        };
                        var iaConfigModel = _unitOfWork.GestionDocenteOcurrenciaIaConfiguracionRepository.Add(iaConfig);
                        await _unitOfWork.CommitAsync();

                        if (ocurrenciaCompleta.IaConfiguracion.EjemplosEntrenamiento?.Any() == true)
                        {
                            foreach (var ejemplo in ocurrenciaCompleta.IaConfiguracion.EjemplosEntrenamiento)
                            {
                                var nuevoEjemplo = new GestionDocenteIaEntrenamientoEjemplo
                                {
                                    IdGestionDocenteOcurrenciaIaConfiguracion = iaConfigModel.Id,
                                    IdGestionDocenteIaEntrenamientoClasificacionTipo = ejemplo.IdGestionDocenteIaEntrenamientoClasificacionTipo,
                                    TextoEjemplo = ejemplo.TextoEjemplo,
                                    EsPositivo = ejemplo.EsPositivo,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = fechaActual,
                                    FechaModificacion = fechaActual
                                };
                                _unitOfWork.GestionDocenteIaEntrenamientoEjemploRepository.Add(nuevoEjemplo);
                            }
                            await _unitOfWork.CommitAsync();
                        }
                    }
                }
            }

            return nuevaCabeceraModel.Id;
        }
    }
}