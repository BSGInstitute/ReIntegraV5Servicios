using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class FlujoDocenteService : IFlujoDocenteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGestionContactoService _gestionContactoService;

        public FlujoDocenteService(IUnitOfWork unitOfWork, IGestionContactoService gestionContactoService)
        {
            _unitOfWork = unitOfWork;
            _gestionContactoService = gestionContactoService;
        }

        public async Task<bool> RegistrarOportunidad(DocentePostulanteDTO dto, string usuario)
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
                    IdCentroCosto = null,
                    IdPersonal_Asignado = 6205,
                    IdClasificacionPersona = idClasificacionPersona,
                    IdFaseGestionContacto = 1,
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

                return true;
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);
            }
        }
    }
}
