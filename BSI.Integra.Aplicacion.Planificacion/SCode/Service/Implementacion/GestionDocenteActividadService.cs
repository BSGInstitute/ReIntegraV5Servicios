using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class GestionDocenteActividadService : IGestionDocenteActividadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> InsertarCabeceraAsync(GestionDocenteActividadCabeceraDTO dto)
        {
            try
            {
                var gestionDocenteActividadCabecera = new GestionDocenteActividadCabecera
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

                var model = _unitOfWork.GestionDocenteActividadCabeceraRepository.Add(gestionDocenteActividadCabecera);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertarDetalleAsync(InsertarActividadDetalleRequestDTO request)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;
                string usuario = request.Detalle.Usuario;

                // 1. Crear el Disparador Detalle
                var gestionDocenteDisparadorDetalle = new GestionDocenteDisparadorDetalle
                {
                    IdGestionDocenteDisparadorFlujoTipo = request.Disparador.IdGestionDocenteDisparadorFlujoTipo,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var disparadorModel = _unitOfWork.GestionDocenteDisparadorDetalleRepository.Add(gestionDocenteDisparadorDetalle);
                await _unitOfWork.CommitAsync();

                // 2. Procesar según el tipo de disparador
                await ProcesarTipoDisparadorAsync(request, disparadorModel.Id, usuario, fechaActual);

                // 3. Crear Detalle de Actividad
                var gestionDocenteActividadDetalle = new GestionDocenteActividadDetalle
                {
                    IdGestionDocenteActividadCabecera = request.Detalle.IdGestionDocenteActividadCabecera,
                    IdGestionDocenteActividadDetalleTipo = request.Detalle.IdGestionDocenteActividadDetalleTipo,
                    IdPlantillaMedioComunicacion = request.Detalle.IdPlantillaMedioComunicacion,
                    IdGestionDocenteDisparadorDetalle = disparadorModel.Id,
                    Nombre = request.Detalle.Nombre,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                var model = _unitOfWork.GestionDocenteActividadDetalleRepository.Add(gestionDocenteActividadDetalle);
                await _unitOfWork.CommitAsync();

                // 4. Si es tipo disparador 3 (Basado en Cronograma) insertar en T_GestionContactoActividadDetalleSesion
                if (request.Disparador.IdGestionDocenteDisparadorFlujoTipo == 3 && request.IdGestionDocenteSesion.HasValue)
                {
                    var actividadDetalleSesion = new GestionContactoActividadDetalleSesion
                    {
                        IdGestionDocenteActividadDetalle = model.Id,
                        IdGestionDocenteSesion = request.IdGestionDocenteSesion.Value,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };

                    _unitOfWork.GestionContactoActividadDetalleSesionRepository.Add(actividadDetalleSesion);
                    await _unitOfWork.CommitAsync();
                }

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Procesa el tipo de disparador y crea los registros correspondientes según el tipo:
        /// 1 = Primera Actividad (Tiempo Fijo)
        /// 2 = Basado en Ocurrencia Anterior
        /// 3 = Basado en Cronograma
        /// </summary>
        private async Task ProcesarTipoDisparadorAsync(InsertarActividadDetalleRequestDTO request, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            switch (request.Disparador.IdGestionDocenteDisparadorFlujoTipo)
            {
                case 1: // Primera Actividad (Tiempo Fijo)
                    await ProcesarDisparadorTiempoFijoAsync(request.ReglaTiempoFijo, idDisparadorDetalle, usuario, fechaActual);
                    break;

                case 2: // Basado en Ocurrencia Anterior
                    await ProcesarDisparadorOcurrenciaAnteriorAsync(request.ReglaTiempoRelativo, request.OcurrenciaDetalle, idDisparadorDetalle, usuario, fechaActual);
                    break;

                case 3: // Basado en Cronograma
                    await ProcesarDisparadorCronogramaAsync(request.ReglaTiempoRelativo, request.ReferenciaRelativa, idDisparadorDetalle, usuario, fechaActual);
                    break;

                default:
                    throw new ArgumentException($"Tipo de disparador no válido: {request.Disparador.IdGestionDocenteDisparadorFlujoTipo}");
            }
        }

        /// <summary>
        /// Tipo 1: Primera Actividad - Tiempo Fijo
        /// Guarda fecha específica en T_GestionDocenteDisparadorReglaTiempoFijo
        /// </summary>
        private async Task ProcesarDisparadorTiempoFijoAsync(GestionDocenteDisparadorReglaTiempoFijoDTO dto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (dto == null)
                throw new ArgumentException("La regla de tiempo fijo es requerida para el disparador de tipo Primera Actividad");

            var reglaFija = new GestionDocenteDisparadorReglaTiempoFijo
            {
                IdGestionDocenteDisparadorReglaTiempo = dto.IdGestionDocenteDisparadorReglaTiempo,
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Fecha = dto.Fecha,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.Add(reglaFija);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Tipo 2: Basado en Ocurrencia Anterior
        /// Guarda cantidad y unidad de tiempo en T_GestionDocenteDisparadorReglaTiempoRelativo
        /// y la ocurrencia anterior en T_GestionDocenteDisparadorOcurrenciaDetalle
        /// </summary>
        private async Task ProcesarDisparadorOcurrenciaAnteriorAsync(GestionDocenteDisparadorReglaTiempoRelativoDTO reglaDto, GestionDocenteDisparadorOcurrenciaDetalleDTO ocurrenciaDto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (reglaDto == null)
                throw new ArgumentException("La regla de tiempo relativo es requerida para el disparador Basado en Ocurrencia Anterior");

            if (ocurrenciaDto == null)
                throw new ArgumentException("La ocurrencia anterior es requerida para el disparador Basado en Ocurrencia Anterior");

            // Crear regla de tiempo relativo
            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
            {
                IdGestionDocenteDisparadorReglaTiempo = reglaDto.IdGestionDocenteDisparadorReglaTiempo,
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Cantidad = reglaDto.Cantidad,
                IdGestionDocenteUnidadTiempo = reglaDto.IdGestionDocenteUnidadTiempo,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);

            // Crear referencia a ocurrencia anterior
            var disparadorOcurrencia = new GestionDocenteDisparadorOcurrenciaDetalle
            {
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                IdGestionDocenteOcurrenciaPrevia = ocurrenciaDto.IdGestionDocenteOcurrenciaPrevia,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorOcurrenciaDetalleRepository.Add(disparadorOcurrencia);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Tipo 3: Basado en Cronograma
        /// Guarda cantidad y unidad de tiempo en T_GestionDocenteDisparadorReglaTiempoRelativo
        /// y la referencia de tiempo (antes/después de sesión) en T_GestionDocenteDisparadorReglaTiempoRelativoReferencia
        /// </summary>
        private async Task ProcesarDisparadorCronogramaAsync(GestionDocenteDisparadorReglaTiempoRelativoDTO reglaDto, GestionDocenteDisparadorReglaTiempoRelativoReferenciaDTO referenciaDto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (reglaDto == null)
                throw new ArgumentException("La regla de tiempo relativo es requerida para el disparador Basado en Cronograma");

            if (referenciaDto == null)
                throw new ArgumentException("La referencia de tiempo (antes/después) es requerida para el disparador Basado en Cronograma");

            // Crear regla de tiempo relativo
            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
            {
                IdGestionDocenteDisparadorReglaTiempo = reglaDto.IdGestionDocenteDisparadorReglaTiempo,
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Cantidad = reglaDto.Cantidad,
                IdGestionDocenteUnidadTiempo = reglaDto.IdGestionDocenteUnidadTiempo,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            var reglaRelativaModel = _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);
            await _unitOfWork.CommitAsync();

            // Crear referencia de tiempo (antes/después de sesión)
            var referenciaRelativa = new GestionDocenteDisparadorReglaTiempoRelativoReferencia
            {
                IdGestionDocenteDisparadorReglaTiempoRelativo = reglaRelativaModel.Id,
                IdGestionDocenteReferenciaTiempo = referenciaDto.IdGestionDocenteReferenciaTiempo,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoReferenciaRepository.Add(referenciaRelativa);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> InsertarOcurrenciaAsync(InsertarOcurrenciaRequestDTO request)
        {
            try
            {
                var dto = request.Ocurrencia;
                DateTime fechaActual = DateTime.Now;
                string usuario = dto.Usuario;

                // 1. Insertar la ocurrencia
                var gestionDocenteOcurrencia = new GestionDocenteOcurrencia
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteOcurrenciaTipo = dto.IdGestionDocenteOcurrenciaTipo,
                    IdGestionDocenteActividadDetalle = dto.IdGestionDocenteActividadDetalle,
                    IdGestionDocenteModoMarcado = dto.IdGestionDocenteModoMarcado,
                    RequiereComentario = dto.RequiereComentario,
                    RequiereFechaHora = dto.RequiereFechaHora,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                var model = _unitOfWork.GestionDocenteOcurrenciaRepository.Add(gestionDocenteOcurrencia);
                await _unitOfWork.CommitAsync();

                // 2. Para Automatico (2) y Warm (3), insertar configuración IA y ejemplos de entrenamiento
                if (dto.IdGestionDocenteModoMarcado == 2 || dto.IdGestionDocenteModoMarcado == 3)
                {
                    await ProcesarConfiguracionIaAsync(request, model.Id, usuario, fechaActual);
                }

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Procesa la configuración de IA para modos de marcado Automatico (2) y Warm (3).
        /// Inserta en T_GestionDocenteOcurrenciaIaConfiguracion y T_GestionDocenteIaEntrenamientoEjemplo.
        /// </summary>
        private async Task ProcesarConfiguracionIaAsync(InsertarOcurrenciaRequestDTO request, int idOcurrencia, string usuario, DateTime fechaActual)
        {
            if (request.IaConfiguracion == null)
                throw new ArgumentException("La configuración de IA es requerida para los modos de marcado Automático y Warm");

            // Insertar configuración IA
            var iaConfiguracion = new GestionDocenteOcurrenciaIaConfiguracion
            {
                Prompt = request.IaConfiguracion.Prompt,
                IdGestionDocenteConfianzaUmbralNivel = request.IaConfiguracion.IdGestionDocenteConfianzaUmbralNivel,
                IdGestionDocenteOcurrencia = idOcurrencia,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            var iaConfigModel = _unitOfWork.GestionDocenteOcurrenciaIaConfiguracionRepository.Add(iaConfiguracion);
            await _unitOfWork.CommitAsync();

            // Insertar ejemplos de entrenamiento
            if (request.EjemplosEntrenamiento != null && request.EjemplosEntrenamiento.Count > 0)
            {
                foreach (var ejemploDto in request.EjemplosEntrenamiento)
                {
                    var ejemplo = new GestionDocenteIaEntrenamientoEjemplo
                    {
                        IdGestionDocenteOcurrenciaIaConfiguracion = iaConfigModel.Id,
                        IdGestionDocenteIaEntrenamientoClasificacionTipo = ejemploDto.IdGestionDocenteIaEntrenamientoClasificacionTipo,
                        TextoEjemplo = ejemploDto.TextoEjemplo,
                        EsPositivo = ejemploDto.EsPositivo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };

                    _unitOfWork.GestionDocenteIaEntrenamientoEjemploRepository.Add(ejemplo);
                }
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto)
        {
            try
            {
                // 1. Insertar Cabecera
                int idCabecera = await InsertarCabeceraAsync(dto.Cabecera);

                // 2. Insertar Detalles
                foreach (var detRequest in dto.Detalles)
                {
                    detRequest.Detalle.IdGestionDocenteActividadCabecera = idCabecera;
                    int idDetalle = await InsertarDetalleAsync(detRequest);

                    // 3. Insertar Ocurrencias asociadas a este detalle
                    var ocurrenciasAsociadas = dto.Ocurrencias.Where(o => o.Ocurrencia.IdGestionDocenteActividadDetalle == detRequest.Detalle.Id);
                    foreach (var ocuRequest in ocurrenciasAsociadas)
                    {
                        ocuRequest.Ocurrencia.IdGestionDocenteActividadDetalle = idDetalle;
                        await InsertarOcurrenciaAsync(ocuRequest);
                    }
                }

                return idCabecera;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AsociarActividadAFlujoAsync(GestionDocenteActividadCabeceraFlujoDTO dto)
        {
            try
            {
                var entidad = new GestionDocenteActividadCabeceraFlujo
                {
                    IdGestionDocenteFlujo = dto.IdGestionDocenteFlujo,
                    IdGestionDocenteActividadCabecera = dto.IdGestionDocenteActividadCabecera,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var model = _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Add(entidad);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DesasociarActividadDeFlujoAsync(int id, string usuario)
        {
            try
            {
                _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Delete(id, usuario);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GestionDocenteActividadCabeceraFlujoDTO>> ObtenerActividadesPorFlujoAsync(int idFlujo)
        {
            try
            {
                var lista = _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository
                    .GetAll()
                    .Where(x => x.IdGestionDocenteFlujo == idFlujo && x.Estado)
                    .ToList();

                return lista.Select(x => new GestionDocenteActividadCabeceraFlujoDTO
                {
                    Id = x.Id,
                    IdGestionDocenteFlujo = x.IdGestionDocenteFlujo,
                    IdGestionDocenteActividadCabecera = x.IdGestionDocenteActividadCabecera,
                    Estado = x.Estado
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteSesionDTO> ObtenerSesiones()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerSesiones();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteOcurrenciaDTO> ObtenerOcurrencias()
        {
            try
            {
                return _unitOfWork.GestionDocenteOcurrenciaRepository.ObtenerOcurrencias();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteConfianzaUmbralNivelDTO> ObtenerConfianzaUmbralNiveles()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerConfianzaUmbralNiveles();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteOcurrenciaTipoDTO> ObtenerOcurrenciaTipos()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerOcurrenciaTipos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteReferenciaTiempoDTO> ObtenerReferenciasTiempo()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReferenciasTiempo();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteActividadDetalleTipoDTO> ObtenerActividadDetalleTipos()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerActividadDetalleTipos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteDisparadorFlujoTipoDTO> ObtenerDisparadorFlujoTipos()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDisparadorFlujoTipos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteUnidadTiempoDTO> ObtenerUnidadesTiempo()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerUnidadesTiempo();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object> ObtenerDisparadorFlujoTiposConfiguracion()
        {
            try
            {
                var tipos = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDisparadorFlujoTipos().ToList();
                if (!tipos.Any()) return new List<object>();

                var unidadesTiempo = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerUnidadesTiempo().ToList();
                var referenciasTiempo = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReferenciasTiempo().ToList();
                var ocurrencias = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerOcurrenciasReferencia().ToList();

                return tipos.Select<GestionDocenteDisparadorFlujoTipoDTO, object>(t =>
                {
                    switch (t.Id)
                    {
                        case 1:
                            return new DisparadorFlujoTipoPrimeraActividadConfigDTO
                            {
                                Id = t.Id,
                                Nombre = t.Nombre
                            };
                        case 2:
                            return new DisparadorFlujoTipoOcurrenciaConfigDTO
                            {
                                Id = t.Id,
                                Nombre = t.Nombre,
                                Tiempo = unidadesTiempo,
                                Ocurrencias = ocurrencias
                            };
                        case 3:
                            return new DisparadorFlujoTipoCronogramaConfigDTO
                            {
                                Id = t.Id,
                                Nombre = t.Nombre,
                                Momento = referenciasTiempo,
                                Tiempo = unidadesTiempo
                            };
                        default:
                            return new DisparadorFlujoTipoPrimeraActividadConfigDTO
                            {
                                Id = t.Id,
                                Nombre = t.Nombre
                            };
                    }
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteModoMarcadoDTO> ObtenerModosMarcado()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerModosMarcado();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocenteMedioComunicacionDTO> ObtenerMediosComunicacion()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerMediosComunicacion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GestionDocentePlantillaMedioComunicacionDTO> ObtenerPlantillasMedioComunicacion()
        {
            try
            {
                return _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerPlantillasMedioComunicacion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object> ObtenerDisparadorReglaTiempo()
        {
            try
            {
                var disparadores = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDisparadorReglaTiempo().ToList();
                if (!disparadores.Any()) return new List<object>();

                var disparadorIds = string.Join(",", disparadores.Select(d => d.IdGestionDocenteDisparadorDetalle));

                var reglasFijo = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReglasTiempoFijoPorDisparadores(disparadorIds).ToList();
                var reglasRelativo = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReglasTiempoRelativoPorDisparadores(disparadorIds).ToList();

                var referenciasRelativas = new List<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO>();
                if (reglasRelativo.Any())
                {
                    var reglasRelativoIds = string.Join(",", reglasRelativo.Select(r => r.IdGestionDocenteDisparadorReglaTiempoRelativo));
                    referenciasRelativas = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReferenciasRelativasPorReglas(reglasRelativoIds).ToList();
                }

                var disparadoresOcurrencia = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDisparadoresOcurrenciaPorIds(disparadorIds).ToList();

                return disparadores.Select<GestionDocenteDisparadorDetalleOutputDTO, object>(d =>
                {
                    switch (d.IdGestionDocenteDisparadorFlujoTipo)
                    {
                        case 1:
                            return new DisparadorTiempoFijoDTO
                            {
                                DisparadorDetalle = d,
                                ReglaTiempoFijo = reglasFijo.FirstOrDefault(r => r.IdGestionDocenteDisparadorDetalle == d.IdGestionDocenteDisparadorDetalle)
                            };
                        case 2:
                            return new DisparadorOcurrenciaAnteriorDTO
                            {
                                DisparadorDetalle = d,
                                ReglaTiempoRelativo = reglasRelativo.FirstOrDefault(r => r.IdGestionDocenteDisparadorDetalle == d.IdGestionDocenteDisparadorDetalle),
                                OcurrenciaDetalle = disparadoresOcurrencia.FirstOrDefault(o => o.IdGestionDocenteDisparadorDetalle == d.IdGestionDocenteDisparadorDetalle)
                            };
                        case 3:
                            var reglaRelativo = reglasRelativo.FirstOrDefault(r => r.IdGestionDocenteDisparadorDetalle == d.IdGestionDocenteDisparadorDetalle);
                            return new DisparadorCronogramaDTO
                            {
                                DisparadorDetalle = d,
                                ReglaTiempoRelativo = reglaRelativo,
                                ReferenciaRelativa = reglaRelativo != null
                                    ? referenciasRelativas.FirstOrDefault(r => r.IdGestionDocenteDisparadorReglaTiempoRelativo == reglaRelativo.IdGestionDocenteDisparadorReglaTiempoRelativo)
                                    : null
                            };
                        default:
                            return new DisparadorDetalleCompletoDTO { DisparadorDetalle = d };
                    }
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActividadCabeceraCompletaDTO ObtenerActividadCabeceraCompleta(int id)
        {
            try
            {
                // 1. Obtener cabecera
                var cabecera = _unitOfWork.GestionDocenteActividadCabeceraRepository.ObtenerCabeceraPorId(id);
                if (cabecera == null) return null;

                // 2. Obtener detalles de la cabecera
                var detalles = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDetallesPorCabecera(id).ToList();
                if (!detalles.Any())
                {
                    return new ActividadCabeceraCompletaDTO { Cabecera = cabecera, Detalles = new List<ActividadDetalleCompletoDTO>() };
                }

                // 3. Obtener IDs para consultas en lote
                var detalleIds = string.Join(",", detalles.Select(d => d.IdGestionDocenteActividadDetalle));
                var disparadorIds = string.Join(",", detalles.Select(d => d.IdGestionDocenteDisparadorDetalle));

                // 4. Obtener disparadores
                var disparadores = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDisparadoresPorIds(disparadorIds).ToList();

                // 5. Obtener reglas de tiempo fijo
                var reglasFijo = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReglasTiempoFijoPorDisparadores(disparadorIds).ToList();

                // 6. Obtener reglas de tiempo relativo
                var reglasRelativo = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReglasTiempoRelativoPorDisparadores(disparadorIds).ToList();

                // 7. Obtener referencias relativas (si hay reglas relativas)
                var referenciasRelativas = new List<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO>();
                if (reglasRelativo.Any())
                {
                    var reglasRelativoIds = string.Join(",", reglasRelativo.Select(r => r.IdGestionDocenteDisparadorReglaTiempoRelativo));
                    referenciasRelativas = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerReferenciasRelativasPorReglas(reglasRelativoIds).ToList();
                }

                // 8. Obtener disparadores de ocurrencia
                var disparadoresOcurrencia = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerDisparadoresOcurrenciaPorIds(disparadorIds).ToList();

                // 9. Obtener sesiones
                var sesiones = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerSesionesPorDetalles(detalleIds).ToList();

                // 10. Obtener ocurrencias
                var ocurrencias = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerOcurrenciasPorDetalles(detalleIds).ToList();

                // 11. Obtener configuraciones IA (si hay ocurrencias)
                var iaConfiguraciones = new List<OcurrenciaIaConfiguracionCompletaDTO>();
                var ejemplosEntrenamiento = new List<GestionDocenteIaEntrenamientoEjemploOutputDTO>();
                if (ocurrencias.Any())
                {
                    var ocurrenciaIds = string.Join(",", ocurrencias.Select(o => o.IdGestionDocenteOcurrencia));
                    iaConfiguraciones = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerIaConfiguracionesPorOcurrencias(ocurrenciaIds).ToList();

                    // 12. Obtener ejemplos de entrenamiento (si hay configuraciones IA)
                    if (iaConfiguraciones.Any())
                    {
                        var iaConfigIds = string.Join(",", iaConfiguraciones.Select(c => c.IdGestionDocenteOcurrenciaIaConfiguracion));
                        ejemplosEntrenamiento = _unitOfWork.GestionDocenteActividadDetalleRepository.ObtenerEjemplosEntrenamientoPorConfiguraciones(iaConfigIds).ToList();
                    }
                }

                // Ensamblar: asignar ejemplos a configuraciones IA
                foreach (var config in iaConfiguraciones)
                {
                    config.EjemplosEntrenamiento = ejemplosEntrenamiento
                        .Where(e => e.IdGestionDocenteOcurrenciaIaConfiguracion == config.IdGestionDocenteOcurrenciaIaConfiguracion)
                        .ToList();
                }

                // Ensamblar: crear ocurrencias completas
                var ocurrenciasCompletas = ocurrencias.Select(o => new OcurrenciaCompletaDTO
                {
                    Ocurrencia = o,
                    IaConfiguracion = iaConfiguraciones.FirstOrDefault(c => c.IdGestionDocenteOcurrencia == o.IdGestionDocenteOcurrencia)
                }).ToList();

                // Ensamblar: crear detalles completos
                var detallesCompletos = detalles.Select(d =>
                {
                    var idDisparador = d.IdGestionDocenteDisparadorDetalle;
                    var disparador = disparadores.FirstOrDefault(dp => dp.IdGestionDocenteDisparadorDetalle == idDisparador);
                    var reglaFijo = reglasFijo.FirstOrDefault(r => r.IdGestionDocenteDisparadorDetalle == idDisparador);
                    var reglaRelativo = reglasRelativo.FirstOrDefault(r => r.IdGestionDocenteDisparadorDetalle == idDisparador);
                    var referenciaRelativa = reglaRelativo != null
                        ? referenciasRelativas.FirstOrDefault(r => r.IdGestionDocenteDisparadorReglaTiempoRelativo == reglaRelativo.IdGestionDocenteDisparadorReglaTiempoRelativo)
                        : null;
                    var ocurrenciaDetalle = disparadoresOcurrencia.FirstOrDefault(o => o.IdGestionDocenteDisparadorDetalle == idDisparador);

                    return new ActividadDetalleCompletoDTO
                    {
                        Detalle = d,
                        Disparador = new DisparadorDetalleCompletoDTO
                        {
                            DisparadorDetalle = disparador,
                            ReglaTiempoFijo = reglaFijo,
                            ReglaTiempoRelativo = reglaRelativo,
                            ReferenciaRelativa = referenciaRelativa,
                            OcurrenciaDetalle = ocurrenciaDetalle
                        },
                        Sesion = sesiones.FirstOrDefault(s => s.IdGestionDocenteActividadDetalle == d.IdGestionDocenteActividadDetalle),
                        Ocurrencias = ocurrenciasCompletas.Where(o => o.Ocurrencia.IdGestionDocenteActividadDetalle == d.IdGestionDocenteActividadDetalle).ToList()
                    };
                }).ToList();

                return new ActividadCabeceraCompletaDTO
                {
                    Cabecera = cabecera,
                    Detalles = detallesCompletos
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
