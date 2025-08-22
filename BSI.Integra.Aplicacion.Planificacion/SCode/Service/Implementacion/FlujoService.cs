
using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Base.Exceptions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: FlujoService
    /// Autor: Christian Quispe.
    /// Fecha: 04/09/2023
    /// <summary>
    /// Gestión general de ope.T_Flujo
    /// </summary>
    public class FlujoService : IFlujoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public FlujoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlujoFaseDTO, FlujoFase>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Christian Quispe
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro Partner PW
        /// </summary>
        /// <returns> Lista PartnerPwDTO </returns>
        public IEnumerable<FlujoDetalleDTO> Obtener()
        {
            return _unitOfWork.FlujoRepository.Obtener();
        }
        /// Autor: Christian Quispe
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos iniciales para el modulo
        /// </summary>
        /// <returns> FlujoCombosDTO </returns>
        public FlujoCombosDTO ObtenerCombos()
        {
            FlujoCombosDTO combos = new FlujoCombosDTO()
            {
                ComboClasificacionUbicacion = _unitOfWork.FlujoRepository.ObtenerComboClasificacionUbicacionDocente(),
                ComboModalidad = _unitOfWork.ModalidadCursoRepository.ObtenerCombo()
            };
            return combos;
        }
        /// Autor: Christian Quispe
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los hijos de Flujo
        /// </summary>
        /// <returns> Lista FlujoFaseDTO </returns>
        public IEnumerable<FlujoFaseDTO> ObtenerFlujoFasePorIdFlujo(int idFlujo)
        {
            try
            {
                if (idFlujo == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var FlujosFases = _unitOfWork.FlujoFaseRepository.ObtenerPorIdFlujo(idFlujo);
                return FlujosFases;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Quispe
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los hijos de FlujoFase
        /// </summary>
        /// <returns> Lista FlujoActividadDTO </returns>
        public IEnumerable<FlujoActividadDTO> ObtenerFlujoActividadPorIdFlujoFase(int idFlujoFase)
        {
            try
            {
                if (idFlujoFase == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var FlujosActividades = _unitOfWork.FlujoActividadRepository.ObtenerPorIdFlujoFase(idFlujoFase);
                return FlujosActividades;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Quispe
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los hijos de FlujoFase
        /// </summary>
        /// <returns> Lista FlujoOcurrenciaDetalleDTO </returns>
        public IEnumerable<FlujoOcurrenciaDetalleDTO> ObtenerFlujoOcurrenciaPorIdFlujoActividad(int idFlujoActividad)
        {
            try
            {
                if (idFlujoActividad == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var FlujosOcurrencias = _unitOfWork.FlujoOcurrenciaRepository.ObtenerPorIdFlujoActividad(idFlujoActividad);
                return FlujosOcurrencias;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Flujo
        /// </summary>
        /// <param name="dto">FlujoDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool Insertar(FlujoDTO dto, string usuario)
        {
            try
            {
                if (dto != null) {
                    Flujo entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdModalidadCurso = dto.IdModalidadCurso,
                        IdClasificacionUbicacionDocente = dto.IdClasificacionUbicacionDocente,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    _unitOfWork.FlujoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return true;
                } else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un Flujo
        /// </summary>
        /// <param name="dto">FlujoDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool Actualizar(FlujoDTO dto, string usuario)
        {
            try
            {
                Flujo flujo = _unitOfWork.FlujoRepository.ObtenerPorId(dto.Id);
                if (flujo.Id != null)
                {
                    flujo.Nombre = dto.Nombre;
                    flujo.IdModalidadCurso = dto.IdModalidadCurso;
                    flujo.IdClasificacionUbicacionDocente = dto.IdClasificacionUbicacionDocente;
                    flujo.UsuarioModificacion = usuario;
                    flujo.FechaModificacion = DateTime.Now;
                    _unitOfWork.FlujoRepository.Update(flujo);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Flujo no existente!");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un Flujo
        /// </summary>
        /// <param name="dto">id</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.FlujoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    _unitOfWork.FlujoRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                } else
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Flujo Fase
        /// </summary>
        /// <param name="dto">FlujoFaseDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool InsertarFase(FlujoFaseDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FlujoFase entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdFlujo = dto.IdFlujo,
                        Orden = dto.Orden,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    _unitOfWork.FlujoFaseRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualizar un Flujo Fase
        /// </summary>
        /// <param name="dto">FlujoFaseDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool ActualizarFase(FlujoFaseDTO dto, string usuario)
        {
            try
            {
                FlujoFase flujoFase = _unitOfWork.FlujoFaseRepository.ObtenerPorId(dto.Id);
                if (flujoFase.Id != null)
                {
                    flujoFase.Nombre = dto.Nombre;
                    flujoFase.Orden = dto.Orden;
                    flujoFase.UsuarioModificacion = usuario;
                    flujoFase.FechaModificacion = DateTime.Now;
                    _unitOfWork.FlujoFaseRepository.Update(flujoFase);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un Flujo Fase
        /// </summary>
        /// <param name="dto">id</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool EliminarFase(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.FlujoFaseRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    _unitOfWork.FlujoFaseRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Flujo Actividad
        /// </summary>
        /// <param name="dto">FlujoActividadDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool InsertarActividad(FlujoActividadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FlujoActividad entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdFlujoFase = dto.IdFlujoFase,
                        Orden = dto.Orden,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    _unitOfWork.FlujoActividadRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualizar un Flujo Actividad
        /// </summary>
        /// <param name="dto">FlujoActividadDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool ActualizarActividad(FlujoActividadDTO dto, string usuario)
        {
            try
            {
                FlujoActividad flujoActividad = _unitOfWork.FlujoActividadRepository.ObtenerPorId(dto.Id);
                if (flujoActividad.Id != null)
                {
                    flujoActividad.Nombre = dto.Nombre;
                    flujoActividad.Orden = dto.Orden;
                    flujoActividad.UsuarioModificacion = usuario;
                    flujoActividad.FechaModificacion = DateTime.Now;
                    _unitOfWork.FlujoActividadRepository.Update(flujoActividad);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un Flujo Actividad
        /// </summary>
        /// <param name="dto">id</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool EliminarActividad(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.FlujoActividadRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    _unitOfWork.FlujoActividadRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Flujo Ocurrencia
        /// </summary>
        /// <param name="dto">FlujoOcurrenciaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool InsertarOcurrencia(FlujoOcurrenciaDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FlujoOcurrencia entidad = new()
                    {
                        IdFlujoActividad = dto.IdFlujoActividad,
                        Nombre = dto.Nombre,
                        Orden = dto.Orden,
                        CerrarSeguimiento = dto.CerrarSeguimiento,
                        IdFaseDestino = dto.IdFaseDestino,
                        IdFlujoActividadSiguiente = dto.IdFlujoActividadSiguiente,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    _unitOfWork.FlujoOcurrenciaRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualizar un Flujo Ocurrencia
        /// </summary>
        /// <param name="dto">FlujoOcurrenciaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool ActualizarOcurrencia(FlujoOcurrenciaDTO dto, string usuario)
        {
            try
            {
                FlujoOcurrencia flujoOcurrencia = _unitOfWork.FlujoOcurrenciaRepository.ObtenerPorId(dto.Id);
                if (flujoOcurrencia.Id != null)
                {
                    flujoOcurrencia.Nombre = dto.Nombre;
                    flujoOcurrencia.Orden = dto.Orden;
                    flujoOcurrencia .CerrarSeguimiento = dto.CerrarSeguimiento;
                    flujoOcurrencia.IdFaseDestino = dto.IdFaseDestino;
                    flujoOcurrencia.IdFlujoActividadSiguiente = dto.IdFlujoActividadSiguiente;
                    flujoOcurrencia.UsuarioModificacion = usuario;
                    flujoOcurrencia.FechaModificacion = DateTime.Now;
                    _unitOfWork.FlujoOcurrenciaRepository.Update(flujoOcurrencia);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un Flujo Actividad
        /// </summary>
        /// <param name="id">idActividad</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>bool</returns>
        public bool EliminarOcurrencia(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.FlujoOcurrenciaRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    _unitOfWork.FlujoOcurrenciaRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
