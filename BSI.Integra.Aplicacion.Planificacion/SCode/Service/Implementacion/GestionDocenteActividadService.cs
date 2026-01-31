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

        public async Task<int> InsertarDetalleAsync(GestionDocenteActividadDetalleDTO dto)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                // 1. Procesar Disparador
                var gestionDocenteDetalleDisparador = new GestionDocenteDetalleDisparador
                {
                    IdGestionDocenteTipoDisparadorFlujo = dto.Disparador.IdGestionDocenteTipoDisparadorFlujo,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var disparadorModel = _unitOfWork.GestionDocenteDetalleDisparadorRepository.Add(gestionDocenteDetalleDisparador);
                await _unitOfWork.CommitAsync();

                // 2. Ocurrencias Previas del Disparador
                if (dto.Disparador.IdsOcurrenciasPrevias != null)
                {
                    if (dto.Disparador.IdGestionDocenteTipoDisparadorFlujo == 1) // FIJO
                    {
                        if (dto.Disparador.Fecha.HasValue)
                        {
                            var fechaFinal = dto.Disparador.Fecha.Value.Date + (dto.Disparador.Hora ?? TimeSpan.Zero);
                            var reglaFija = new GestionDocenteDisparadorReglaTiempoFijo
                            {
                                IdGestionDocenteDisparadorReglaTiempo = 1, // ID Fijo existente
                                IdGestionDocenteDisparadorDetalle = disparadorModel.Id,
                                Fecha = fechaFinal,
                                Estado = true,
                                UsuarioCreacion = dto.Usuario,
                                UsuarioModificacion = dto.Usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            // TODO: Crear y registrar GestionDocenteDisparadorReglaTiempoFijoRepository en UnitOfWork
                            _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.Add(reglaFija);
                        }
                    }
                    else if (dto.Disparador.IdGestionDocenteTipoDisparadorFlujo == 2) // RELATIVO
                    {
                        if (dto.Disparador.CantidadTiempo.HasValue && dto.Disparador.IdGestionDocenteUnidadTiempo.HasValue)
                        {
                            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
                            {
                                IdGestionDocenteDisparadorReglaTiempo = 2, // ID Relativo existente
                                IdGestionDocenteDisparadorDetalle = disparadorModel.Id,
                                Cantidad = dto.Disparador.CantidadTiempo.Value,
                                IdGestionDocenteUnidadTiempo = dto.Disparador.IdGestionDocenteUnidadTiempo.Value,
                                Estado = true,
                                UsuarioCreacion = dto.Usuario,
                                UsuarioModificacion = dto.Usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            // TODO: Crear y registrar GestionDocenteDisparadorReglaTiempoRelativoRepository en UnitOfWork
                            _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);
                        }

                        if (dto.Disparador.IdOcurrenciaActividadAnterior.HasValue)
                        {
                            var gestionDocenteDetalleDisparadorOcurrencia = new GestionDocenteDetalleDisparadorOcurrencia
                            {
                                IdGestionDocenteDetalleDisparador = disparadorModel.Id,
                                IdGestionDocenteOcurrenciaPrevia = dto.Disparador.IdOcurrenciaActividadAnterior.Value,
                                Estado = true,
                                UsuarioCreacion = dto.Usuario,
                                UsuarioModificacion = dto.Usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            _unitOfWork.GestionDocenteDetalleDisparadorOcurrenciaRepository.Add(gestionDocenteDetalleDisparadorOcurrencia);
                        }
                    }
                    else if (dto.Disparador.IdsOcurrenciasPrevias != null && dto.Disparador.IdsOcurrenciasPrevias.Any())
                    {
                        foreach (var idOcuPrevia in dto.Disparador.IdsOcurrenciasPrevias)
                        {
                            var gestionDocenteDetalleDisparadorOcurrencia = new GestionDocenteDetalleDisparadorOcurrencia
                            {
                                IdGestionDocenteDetalleDisparador = disparadorModel.Id,
                                IdGestionDocenteOcurrenciaPrevia = idOcuPrevia,
                                Estado = true,
                                UsuarioCreacion = dto.Usuario,
                                UsuarioModificacion = dto.Usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            _unitOfWork.GestionDocenteDetalleDisparadorOcurrenciaRepository.Add(gestionDocenteDetalleDisparadorOcurrencia);
                        }
                    }
                }

                // 3. Crear Detalle
                var gestionDocenteActividadDetalle = new GestionDocenteActividadDetalle
                {
                    IdGestionDocenteActividadCabecera = dto.IdGestionDocenteActividadCabecera,
                    IdGestionDocenteTipoActividadDetalle = dto.IdGestionDocenteTipoActividadDetalle,
                    IdPlantillaMediaComunicacion = dto.IdPlantillaMediaComunicacion,
                    IdGestionDocenteDetalleDisparador = disparadorModel.Id,
                    Nombre = dto.Nombre,
                    EstadoActividad = dto.Estado,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                var model = _unitOfWork.GestionDocenteActividadDetalleRepository.Add(gestionDocenteActividadDetalle);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertarOcurrenciaAsync(int idDetalle, GestionDocenteOcurrenciaDTO dto)
        {
            try
            {
                var gestionDocenteOcurrencia = new GestionDocenteOcurrencia
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteOcurrenciaTipo = dto.IdGestionDocenteTipoOcurrencia,
                    IdGestionDocenteActividadDetalle = idDetalle,
                    IdGestionDocenteModoMarcado = dto.IdGestionDocenteModoMarcado,
                    RequiereComentario = dto.RequiereComentario,
                    RequiereFechaHora = dto.RequiereFechaHora,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var model = _unitOfWork.GestionDocenteOcurrenciaRepository.Add(gestionDocenteOcurrencia);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto)
        {
            try
            {
                // 1. Insertar Cabecera
                int idCabecera = await InsertarCabeceraAsync(dto.Cabecera);

                // 2. Insertar Detalles
                foreach (var detDto in dto.Detalles)
                {
                    detDto.IdGestionDocenteActividadCabecera = idCabecera;
                    int idDetalle = await InsertarDetalleAsync(detDto);

                    // 3. Insertar Ocurrencias asociadas a este detalle
                    var ocurrenciasAsociadas = dto.Ocurrencias.Where(o => o.IdGestionDocenteActividadDetalle == detDto.Id);
                    foreach (var ocuDto in ocurrenciasAsociadas)
                    {
                        await InsertarOcurrenciaAsync(idDetalle, ocuDto);
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
    }
}
