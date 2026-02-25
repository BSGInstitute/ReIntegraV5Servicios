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
        /// <returns>Colección de GestionDocenteActividadCabeceraListaDTO.</returns>
        public IEnumerable<GestionDocenteActividadCabeceraListaDTO> ObtenerActividadesCabecera()
        {
            try
            {
                return _unitOfWork.GestionDocenteFlujoRepository.ObtenerActividadesCabecera();
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
    }
}