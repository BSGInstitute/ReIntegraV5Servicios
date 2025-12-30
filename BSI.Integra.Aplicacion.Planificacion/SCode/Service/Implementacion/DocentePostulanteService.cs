using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Autor: Lolo Zaa
    /// Fecha: 26/12/2025
    /// <summary>
    /// Implementación del servicio para el manejo de docentes postulantes
    /// </summary>
    public class DocentePostulanteService : IDocentePostulanteService
    {
        private IUnitOfWork _unitOfWork;
        private IGestionContactoService _gestionContactoService;
        private Mapper _mapper;

        public DocentePostulanteService(IUnitOfWork unitOfWork, IGestionContactoService gestionContactoService)
        {
            _unitOfWork = unitOfWork;
            _gestionContactoService = gestionContactoService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocentePostulante, DocentePostulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<DocentePostulante, TDocentePostulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDocentePostulante, DocentePostulanteDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<DocentePostulante, DocentePostulanteDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public List<DocentePostulanteDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.DocentePostulanteRepository.ObtenerDocentePostulante();
                return _mapper.Map<List<DocentePostulanteDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DocentePostulanteDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.DocentePostulanteRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<DocentePostulanteDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe el docente postulante con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DocentePostulanteDTO> InsertarAsync(DocentePostulanteDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                if (string.IsNullOrWhiteSpace(dto.Correo))
                    throw new BadRequestException("El correo electrónico es obligatorio para crear el docente postulante");

                var fechaActual = DateTime.Now;

                // 1. Crear DocentePostulante
                DocentePostulante entidad = new()
                {
                    Nombre1 = dto.Nombre1,
                    Nombre2 = dto.Nombre2,
                    ApellidoPaterno = dto.ApellidoPaterno,
                    ApellidoMaterno = dto.ApellidoMaterno,
                    NumeroDocumento = dto.NumeroDocumento,
                    FechaNacimiento = dto.FechaNacimiento,
                    Telefono = dto.Telefono,
                    Celular = dto.Celular,
                    Correo = dto.Correo,
                    IdCiudad = dto.IdCiudad,
                    Estado = true,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                };

                var docentePostulante = _unitOfWork.DocentePostulanteRepository.Add(entidad);
                _unitOfWork.Commit();

                // 2. Verificar si ya existe Persona con este email, si no, crear
                Persona personaCreada = _unitOfWork.PersonaRepository.ObtenerPorEmail(dto.Correo);
                int idPersona;

                if (personaCreada == null)
                {
                    Persona persona = new()
                    {
                        Email1 = dto.Correo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };

                    var tPersona = _unitOfWork.PersonaRepository.Add(persona);
                    _unitOfWork.Commit();
                    idPersona = tPersona.Id;
                }
                else
                {
                    idPersona = personaCreada.Id;
                }

                // 3. Crear ClasificacionPersona (conf.T_ClasificacionPersona)
                // Verificar si ya existe una clasificación para esta persona como DocentePostulante
                var clasificacionExistente = _unitOfWork.ClasificacionPersonaRepository
                    .ObtenerPorIdPersonaTipoPersona(idPersona, (BSI.Integra.Aplicacion.Base.Enums.Enums.TipoPersona)6);

                int idClasificacionPersona;

                if (clasificacionExistente == null)
                {
                    ClasificacionPersona clasificacion = new()
                    {
                        IdPersona = idPersona,
                        IdTipoPersona = 6, // 6 = DocentePostulante
                        IdTablaOriginal = docentePostulante.Id,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };

                    var tClasificacion = _unitOfWork.ClasificacionPersonaRepository.Add(clasificacion);
                    _unitOfWork.Commit();
                    idClasificacionPersona = tClasificacion.Id;
                }
                else
                {
                    idClasificacionPersona = clasificacionExistente.Id;
                }

                // 4. Crear GestionContacto (pla.T_GestionContacto)
                var gestionDTO = new CrearGestionContactoDTO
                {
                    IdCentroCosto = null, // NULL porque el docente postulante aún no está asignado a ningún curso
                    IdPersonal_Asignado = 6205,
                    IdClasificacionPersona = idClasificacionPersona,
                    IdFaseGestionContacto = 1, // Pre-Candidato
                    IdOrigen = 1124,
                    UsuarioCreacion = usuario,
                    Comentario = $"Registro automático desde DocentePostulante: {dto.Nombre1} {dto.ApellidoPaterno}"
                };

                try
                {
                    await _gestionContactoService.ProcesarInsercionGestionAsync(gestionDTO);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException($"Error al crear GestionContacto: {ex.Message}. InnerException: {ex.InnerException?.Message}");
                }

                return _mapper.Map<DocentePostulanteDTO>(docentePostulante);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error en InsertarAsync: {ex.Message}. InnerException: {ex.InnerException?.Message}");
            }
        }

        public DocentePostulanteDTO Actualizar(DocentePostulanteDTO dto, string usuario)
        {
            try
            {
                DocentePostulante entidad = new();
                if (dto != null && dto.Id != 0)
                {
                    entidad = _unitOfWork.DocentePostulanteRepository.ObtenerPorId(dto.Id);
                    if (entidad != null && entidad.Id != 0)
                    {
                        entidad.Nombre1 = dto.Nombre1;
                        entidad.Nombre2 = dto.Nombre2;
                        entidad.ApellidoPaterno = dto.ApellidoPaterno;
                        entidad.ApellidoMaterno = dto.ApellidoMaterno;
                        entidad.NumeroDocumento = dto.NumeroDocumento;
                        entidad.FechaNacimiento = dto.FechaNacimiento;
                        entidad.Telefono = dto.Telefono;
                        entidad.Celular = dto.Celular;
                        entidad.Correo = dto.Correo;
                        entidad.IdCiudad = dto.IdCiudad;
                        entidad.FechaModificacion = DateTime.Now;
                        entidad.UsuarioModificacion = usuario;
                    }
                    else
                        throw new BadRequestException("Docente postulante no encontrado");
                }
                else
                    throw new BadRequestException("Id Entidad 0");

                var respuesta = _unitOfWork.DocentePostulanteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocentePostulanteDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var docentePostulante = _unitOfWork.DocentePostulanteRepository.ObtenerPorId(id);
                if (docentePostulante != null && docentePostulante.Id != 0)
                {
                    var respuesta = _unitOfWork.DocentePostulanteRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontró el docente postulante con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
