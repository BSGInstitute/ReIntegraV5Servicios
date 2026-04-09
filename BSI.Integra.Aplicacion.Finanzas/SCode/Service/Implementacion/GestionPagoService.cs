using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Autor: Jose Vega
    /// Fecha: 30/03/2026
    /// Version: 1.0
    /// <summary>
    /// Servicio principal del módulo de pagos — Escenario B (Cuotas Dependientes)
    /// Gestiona el ciclo de vida de la gestión de pago por comprobante completo.
    /// </summary>
    public class GestionPagoService : IGestionPagoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GestionPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGestionPago, GestionPago>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGestionPagoCronograma, GestionPagoCronograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGestionPagoArchivo, GestionPagoArchivo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GestionPago Add(GestionPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.GestionPagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GestionPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GestionPago Update(GestionPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.GestionPagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GestionPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int idGestionPago, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // Soft-delete archivos asociados
                    var archivos = _unitOfWork.GestionPagoArchivoRepository
                        .GetBy(w => w.IdGestionPago == idGestionPago && w.Estado == true)
                        .Select(a => a.Id)
                        .ToList();

                    if (archivos.Any())
                    {
                        _unitOfWork.GestionPagoArchivoRepository.Delete(archivos, usuario);
                    }

                    // Soft-delete cronograma asociado
                    var cronogramas = _unitOfWork.GestionPagoCronogramaRepository
                        .GetBy(w => w.IdGestionPago == idGestionPago && w.Estado == true)
                        .Select(c => c.Id)
                        .ToList();

                    if (cronogramas.Any())
                    {
                        _unitOfWork.GestionPagoCronogramaRepository.Delete(cronogramas, usuario);
                    }

                    // Soft-delete cabecera
                    _unitOfWork.GestionPagoRepository.Delete(idGestionPago, usuario);

                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GestionPago> Add(List<GestionPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GestionPagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GestionPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GestionPago> Update(List<GestionPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GestionPagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GestionPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.GestionPagoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Consultas
        public IEnumerable<GestionPagoDTO> ObtenerGestionesPago(FiltroGestionPagoDTO filtro)
        {
            try
            {
                return _unitOfWork.GestionPagoRepository.ObtenerGestionesPago(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GestionPagoDTO? ObtenerGestionPagoPorId(int id)
        {
            try
            {
                return _unitOfWork.GestionPagoRepository.ObtenerGestionPagoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GestionPagoDTO? ObtenerGestionPagoPorComprobante(int idComprobantePago)
        {
            try
            {
                return _unitOfWork.GestionPagoRepository.ObtenerGestionPagoPorComprobante(idComprobantePago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionPagoCronogramaDTO> ObtenerCronogramaPorGestionPago(int idGestionPago)
        {
            try
            {
                return _unitOfWork.GestionPagoCronogramaRepository.ObtenerCronogramaPorGestionPago(idGestionPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosCabecera(int idGestionPago)
        {
            try
            {
                return _unitOfWork.GestionPagoArchivoRepository.ObtenerArchivosPorGestionPago(idGestionPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosPorCronograma(int idGestionPagoCronograma)
        {
            try
            {
                return _unitOfWork.GestionPagoArchivoRepository.ObtenerArchivosPorCronograma(idGestionPagoCronograma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Catálogos
        public IEnumerable<ModalidadPagoDTO> ObtenerModalidadesPago()
        {
            try
            {
                return _unitOfWork.ModalidadPagoRepository.ObtenerModalidadesPago();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PagoEstadoDTO> ObtenerPagoEstados()
        {
            try
            {
                return _unitOfWork.PagoEstadoRepository.ObtenerPagoEstados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Operaciones de Negocio

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta una gestión de pago completa: cabecera + cronograma + archivos
        /// </summary>
        public bool InsertarGestionPago(GestionPagoInsertarDTO dto)
        {
            try
            {
                var usuario = dto.Usuario;

                // Validar que no exista ya una gestión de pago para este comprobante
                var existeGestionPago = _unitOfWork.GestionPagoRepository.Exist(
                    w => w.IdComprobantePago == dto.IdComprobantePago && w.Estado == true);
                if (existeGestionPago)
                    throw new ConflictException($"Ya existe una gestión de pago para el comprobante {dto.IdComprobantePago}");

                using (TransactionScope scope = new TransactionScope())
                {
                    // 1. Insertar cabecera
                    var gestionPago = new GestionPago
                    {
                        IdComprobantePago = dto.IdComprobantePago,
                        ServicioValidado = dto.ServicioValidado,
                        FechaSolicitud = dto.FechaSolicitud,
                        ObservacionDocumentacion = dto.ObservacionDocumentacion,
                        LevantamientoObservacion = dto.LevantamientoObservacion,
                        ConformidadFinanzas = dto.ConformidadFinanzas,
                        ObservacionProgramacionPago = dto.ObservacionProgramacionPago,
                        IdModalidadPago = dto.IdModalidadPago,
                        IdPagoEstado = dto.IdPagoEstado,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };

                    var modeloCabecera = _unitOfWork.GestionPagoRepository.Add(gestionPago);
                    _unitOfWork.Commit();

                    int idGestionPago = modeloCabecera.Id;

                    // 2. Insertar cronograma (cuotas)
                    if (dto.Cronograma != null && dto.Cronograma.Count > 0)
                    {
                        var cronogramas = dto.Cronograma.Select(c => new GestionPagoCronograma
                        {
                            IdGestionPago = idGestionPago,
                            NumeroCuota = c.NumeroCuota,
                            MontoCuota = c.MontoCuota,
                            FechaVencimiento = c.FechaVencimiento,
                            FechaProbablePago = c.FechaProbablePago,
                            FechaRealPago = c.FechaRealPago,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        }).ToList();

                        _unitOfWork.GestionPagoCronogramaRepository.Add(cronogramas);
                    }

                    // 3. Insertar archivos de cabecera
                    if (dto.Archivos != null && dto.Archivos.Count > 0)
                    {
                        var archivos = dto.Archivos.Select(a => new GestionPagoArchivo
                        {
                            IdGestionPago = idGestionPago,
                            IdGestionPagoCronograma = a.IdGestionPagoCronograma,
                            NombreArchivo = a.NombreArchivo,
                            ContentTypeArchivo = a.ContentTypeArchivo,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        }).ToList();

                        _unitOfWork.GestionPagoArchivoRepository.Add(archivos);
                    }

                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza una gestión de pago existente incluyendo cronograma y archivos (upsert hijos)
        /// </summary>
        public bool ActualizarGestionPago(GestionPagoActualizarDTO dto)
        {
            try
            {
                var usuario = dto.Usuario;
                var modeloExistente = _unitOfWork.GestionPagoRepository.FirstBy(w => w.Id == dto.IdGestionPago && w.Estado == true);
                if (modeloExistente == null)
                    throw new NotFoundException("Gestión de pago no encontrada");

                using (TransactionScope scope = new TransactionScope())
                {
                    var gestionPago = _mapper.Map<GestionPago>(modeloExistente);
                    gestionPago.IdComprobantePago = dto.IdComprobantePago;
                    gestionPago.ServicioValidado = dto.ServicioValidado;
                    gestionPago.FechaSolicitud = dto.FechaSolicitud;
                    gestionPago.ObservacionDocumentacion = dto.ObservacionDocumentacion;
                    gestionPago.LevantamientoObservacion = dto.LevantamientoObservacion;
                    gestionPago.ConformidadFinanzas = dto.ConformidadFinanzas;
                    gestionPago.ObservacionProgramacionPago = dto.ObservacionProgramacionPago;
                    gestionPago.IdModalidadPago = dto.IdModalidadPago;
                    gestionPago.IdPagoEstado = dto.IdPagoEstado;
                    gestionPago.UsuarioModificacion = usuario;

                    _unitOfWork.GestionPagoRepository.Update(gestionPago);
                    _unitOfWork.Commit();

                    // Upsert cronograma
                    if (dto.Cronograma != null)
                    {
                        var cronogramasExistentes = _unitOfWork.GestionPagoCronogramaRepository
                            .GetBy(w => w.IdGestionPago == dto.IdGestionPago && w.Estado == true)
                            .ToList();

                        var idsEnviados = dto.Cronograma
                            .Where(c => c.Id.HasValue && c.Id.Value > 0)
                            .Select(c => c.Id!.Value)
                            .ToHashSet();

                        // Soft-delete cuotas que ya no están en el DTO
                        var idsAEliminar = cronogramasExistentes
                            .Where(e => !idsEnviados.Contains(e.Id))
                            .Select(e => e.Id)
                            .ToList();

                        if (idsAEliminar.Any())
                        {
                            _unitOfWork.GestionPagoCronogramaRepository.Delete(idsAEliminar, usuario);
                        }

                        // Insertar nuevas cuotas (Id == null o 0)
                        var nuevas = dto.Cronograma
                            .Where(c => !c.Id.HasValue || c.Id.Value == 0)
                            .Select(c => new GestionPagoCronograma
                            {
                                IdGestionPago = dto.IdGestionPago,
                                NumeroCuota = c.NumeroCuota,
                                MontoCuota = c.MontoCuota,
                                FechaVencimiento = c.FechaVencimiento,
                                FechaProbablePago = c.FechaProbablePago,
                                FechaRealPago = c.FechaRealPago,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario
                            }).ToList();

                        if (nuevas.Any())
                        {
                            _unitOfWork.GestionPagoCronogramaRepository.Add(nuevas);
                        }

                        // Actualizar cuotas existentes (Id > 0)
                        var aActualizar = dto.Cronograma
                            .Where(c => c.Id.HasValue && c.Id.Value > 0)
                            .ToList();

                        foreach (var cuotaDto in aActualizar)
                        {
                            var cuotaExistente = cronogramasExistentes.FirstOrDefault(e => e.Id == cuotaDto.Id!.Value);
                            if (cuotaExistente == null) continue;

                            var cuotaEntidad = _mapper.Map<GestionPagoCronograma>(cuotaExistente);
                            cuotaEntidad.NumeroCuota = cuotaDto.NumeroCuota;
                            cuotaEntidad.MontoCuota = cuotaDto.MontoCuota;
                            cuotaEntidad.FechaVencimiento = cuotaDto.FechaVencimiento;
                            cuotaEntidad.FechaProbablePago = cuotaDto.FechaProbablePago;
                            cuotaEntidad.FechaRealPago = cuotaDto.FechaRealPago;
                            cuotaEntidad.UsuarioModificacion = usuario;

                            _unitOfWork.GestionPagoCronogramaRepository.Update(cuotaEntidad);
                        }

                        _unitOfWork.Commit();
                    }

                    // Upsert archivos de cabecera
                    if (dto.Archivos != null)
                    {
                        var archivosExistentes = _unitOfWork.GestionPagoArchivoRepository
                            .GetBy(w => w.IdGestionPago == dto.IdGestionPago && w.IdGestionPagoCronograma == null && w.Estado == true)
                            .ToList();

                        var idsArchivosEnviados = dto.Archivos
                            .Where(a => a.Id.HasValue && a.Id.Value > 0)
                            .Select(a => a.Id!.Value)
                            .ToHashSet();

                        // Soft-delete archivos que ya no están en el DTO
                        var idsArchivosAEliminar = archivosExistentes
                            .Where(e => !idsArchivosEnviados.Contains(e.Id))
                            .Select(e => e.Id)
                            .ToList();

                        if (idsArchivosAEliminar.Any())
                        {
                            _unitOfWork.GestionPagoArchivoRepository.Delete(idsArchivosAEliminar, usuario);
                        }

                        // Insertar nuevos archivos
                        var archivosNuevos = dto.Archivos
                            .Where(a => !a.Id.HasValue || a.Id.Value == 0)
                            .Select(a => new GestionPagoArchivo
                            {
                                IdGestionPago = dto.IdGestionPago,
                                IdGestionPagoCronograma = a.IdGestionPagoCronograma,
                                NombreArchivo = a.NombreArchivo,
                                ContentTypeArchivo = a.ContentTypeArchivo,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario
                            }).ToList();

                        if (archivosNuevos.Any())
                        {
                            _unitOfWork.GestionPagoArchivoRepository.Add(archivosNuevos);
                        }

                        _unitOfWork.Commit();
                    }

                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Finanzas registra conformidad u observación — afecta todo el pago
        /// </summary>
        public bool RegistrarConformidad(GestionPagoConformidadDTO dto)
        {
            try
            {
                var modeloExistente = _unitOfWork.GestionPagoRepository.FirstBy(w => w.Id == dto.IdGestionPago && w.Estado == true);
                if (modeloExistente == null)
                    throw new NotFoundException("Gestión de pago no encontrada");
                var gestionPago = _mapper.Map<GestionPago>(modeloExistente);

                gestionPago.ConformidadFinanzas = dto.ConformidadFinanzas;
                gestionPago.ObservacionDocumentacion = dto.ObservacionDocumentacion ?? gestionPago.ObservacionDocumentacion;
                gestionPago.ObservacionProgramacionPago = dto.ObservacionProgramacionPago ?? gestionPago.ObservacionProgramacionPago;
                gestionPago.IdPagoEstado = dto.IdPagoEstado;
                gestionPago.UsuarioModificacion = dto.Usuario;

                _unitOfWork.GestionPagoRepository.Update(gestionPago);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Operaciones levanta observación
        /// </summary>
        public bool LevantarObservacion(GestionPagoLevantamientoDTO dto)
        {
            try
            {
                var modeloExistente = _unitOfWork.GestionPagoRepository.FirstBy(w => w.Id == dto.IdGestionPago && w.Estado == true);
                if (modeloExistente == null)
                    throw new NotFoundException("Gestión de pago no encontrada");
                var gestionPago = _mapper.Map<GestionPago>(modeloExistente);

                gestionPago.LevantamientoObservacion = dto.LevantamientoObservacion;
                gestionPago.IdPagoEstado = dto.IdPagoEstado;
                gestionPago.UsuarioModificacion = dto.Usuario;

                _unitOfWork.GestionPagoRepository.Update(gestionPago);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Registra la fecha real de pago de una cuota específica
        /// </summary>
        public bool RegistrarPagoCuota(GestionPagoCronogramaPagoDTO dto)
        {
            try
            {
                var usuario = dto.Usuario;

                _unitOfWork.GestionPagoCronogramaRepository.ActualizarFechaPago(dto.IdGestionPagoCronograma, dto.FechaRealPago, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Agrega un archivo adjunto a una gestión de pago
        /// </summary>
        public bool InsertarArchivo(int idGestionPago, GestionPagoArchivoInsertarDTO dto)
        {
            try
            {
                var usuario = dto.Usuario;
                var archivo = new GestionPagoArchivo
                {
                    IdGestionPago = idGestionPago,
                    IdGestionPagoCronograma = dto.IdGestionPagoCronograma,
                    NombreArchivo = dto.NombreArchivo,
                    ContentTypeArchivo = dto.ContentTypeArchivo,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario
                };

                _unitOfWork.GestionPagoArchivoRepository.Add(archivo);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina lógicamente un archivo adjunto
        /// </summary>
        public bool EliminarArchivo(int idArchivo, string usuario)
        {
            try
            {
                _unitOfWork.GestionPagoArchivoRepository.Delete(idArchivo, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
