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

                // Asociar actividad cabecera al flujo
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

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(GestionDocenteFlujoDTO dto)
        {
            try
            {
                var entidad = new GestionDocenteFlujo
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteEstado = dto.IdGestionDocenteEstado,
                    IdGestionDocenteCategoria = dto.IdGestionDocenteCategoria,
                    UsuarioModificacion = dto.Usuario,
                    FechaModificacion = DateTime.Now
                };

                _unitOfWork.GestionDocenteFlujoRepository.Update(entidad);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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

        public FlujoCompletoDTO ObtenerFlujoCompleto(int id)
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
                    var cabecera = _actividadService.ObtenerActividadCabeceraCompleta(asociacion.IdGestionDocenteActividadCabecera);
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