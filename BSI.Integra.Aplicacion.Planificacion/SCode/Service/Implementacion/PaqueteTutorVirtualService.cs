using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class PaqueteTutorVirtualService : IPaqueteTutorVirtualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PaqueteTutorVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPaqueteTutorVirtual, PaqueteTutorVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtual, PaqueteTutorVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtualDTO, PaqueteTutorVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPaqueteTutorVirtual, PaqueteTutorVirtualDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha:  27/11/2025
        /// <summary>
        /// Tipo Formacion Service
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<PaqueteTutorVirtualDTO> Obtener()
        {
            return _unitOfWork.PaqueteTutorVirtualRepository.Obtener();
        }

        /// Autor: Christopher Sandy D' Paris
        /// Fecha:  27/11/2025
        /// <summary>
        /// Obtiene el detalle completo de paquetes con países y beneficios
        /// </summary>
        /// <returns> Lista PaqueteTutorVirtualDetalleDTO </returns>
        public IEnumerable<PaqueteTutorVirtualDetalleDTO> ObtenerDetalle()
        {
            try
            {
                // Obtener todos los paquetes
                var paquetes = _unitOfWork.PaqueteTutorVirtualRepository.GetAll().Where(p => p.Estado == true);
                
                List<PaqueteTutorVirtualDetalleDTO> resultado = new List<PaqueteTutorVirtualDetalleDTO>();

                foreach (var paquete in paquetes)
                {
                    var paqueteDetalle = new PaqueteTutorVirtualDetalleDTO
                    {
                        Id = paquete.Id,
                        Nombre = paquete.Nombre,
                        CantidadCreditos = paquete.CantidadCredito,
                        Paises = new List<PaquetePaisDetalleDTO>()
                    };

                    // Obtener países asociados al paquete
                    var paises = _unitOfWork.PaqueteTutorVirtualPaisRepository.ObtenerPorIdPaquete(paquete.Id);

                    foreach (var pais in paises)
                    {
                        var paisDetalle = new PaquetePaisDetalleDTO
                        {
                            Id = pais.Id,
                            IdPais = pais.IdPais,
                            IdMoneda = pais.IdMoneda,
                            CostoIndividual = pais.CostoIndividual,
                            CostoPrograma = pais.CostoPaquete,
                            Beneficios = new List<PaqueteTutorVirtualBeneficioDetalleDTO>()
                        };

                        // Obtener beneficios asociados al país
                        var beneficios = _unitOfWork.PaqueteTutorVirtualBeneficioRepository.ObtenerPorIdPaquetePais(pais.Id);

                        foreach (var beneficio in beneficios)
                        {
                            paisDetalle.Beneficios.Add(new PaqueteTutorVirtualBeneficioDetalleDTO
                            {
                                Id = beneficio.Id,
                                Nombre = beneficio.Nombre
                            });
                        }

                        paqueteDetalle.Paises.Add(paisDetalle);
                    }

                    resultado.Add(paqueteDetalle);
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Christopher Sandy D' Paris
        /// Fecha:  27/11/2025
        /// Version: 1.0
        /// <summary>
        /// Inserta un PaqueteTutorVirtual completo con países y beneficios
        /// </summary>
        /// <param name="dto">PaqueteTutorVirtualGuardarDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PaqueteTutorVirtualGuardarDTO</returns>
        public PaqueteTutorVirtualGuardarDTO Insertar(PaqueteTutorVirtualGuardarDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                // 1. Insertar Paquete
                PaqueteTutorVirtual paqueteEntidad = new()
                {
                    Nombre = dto.Nombre,
                    CantidadCredito = dto.CantidadCredito,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };
                var paqueteRespuesta = _unitOfWork.PaqueteTutorVirtualRepository.Add(paqueteEntidad);
                _unitOfWork.Commit();
                dto.Id = paqueteRespuesta.Id;

                // 2. Insertar Países
                if (dto.Paises != null && dto.Paises.Any())
                {
                    foreach (var paisDto in dto.Paises)
                    {
                        PaqueteTutorVirtualPais paisEntidad = new()
                        {
                            IdPaqueteTutorVirtual = paqueteRespuesta.Id,
                            IdPais = paisDto.IdPais,
                            IdMoneda = paisDto.IdMoneda,
                            CostoIndividual = paisDto.CostoIndividual,
                            CostoPaquete = paisDto.CostoPaquete,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            ListadoBeneficios = new List<PaqueteTutorVirtualBeneficio>()
                        };

                        // 3. Agregar Beneficios al país
                        if (paisDto.Beneficios != null && paisDto.Beneficios.Any())
                        {
                            foreach (var beneficioDto in paisDto.Beneficios)
                            {
                                PaqueteTutorVirtualBeneficio beneficioEntidad = new()
                                {
                                    Nombre = beneficioDto.Nombre,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                paisEntidad.ListadoBeneficios.Add(beneficioEntidad);
                            }
                        }

                        var paisRespuesta = _unitOfWork.PaqueteTutorVirtualPaisRepository.Add(paisEntidad);
                        _unitOfWork.Commit();
                        paisDto.Id = paisRespuesta.Id;

                        // Actualizar IDs de beneficios en el DTO
                        if (paisDto.Beneficios != null && paisRespuesta.TPaqueteTutorVirtualPaisBeneficios != null)
                        {
                            for (int i = 0; i < paisDto.Beneficios.Count; i++)
                            {
                                paisDto.Beneficios[i].Id = paisRespuesta.TPaqueteTutorVirtualPaisBeneficios.ElementAt(i).Id;
                            }
                        }
                    }
                }

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Christopher Sandy D' Paris
        /// Fecha:  27/11/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza un PaqueteTutorVirtual completo con países y beneficios
        /// </summary>
        /// <param name="dto">PaqueteTutorVirtualGuardarDTO</param>
        /// <param name="usuario">Usuario Modificación</param>
        /// <returns>PaqueteTutorVirtualGuardarDTO</returns>
        public PaqueteTutorVirtualGuardarDTO Actualizar(PaqueteTutorVirtualGuardarDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                if (dto.Id == 0)
                    throw new BadRequestException("Id Entidad 0");

                // 1. Actualizar Paquete
                var paqueteExistente = _unitOfWork.PaqueteTutorVirtualRepository.ObtenerPorId(dto.Id);
                if (paqueteExistente == null || paqueteExistente.Id == 0)
                    throw new BadRequestException("Paquete no encontrado");

                paqueteExistente.Nombre = dto.Nombre;
                paqueteExistente.CantidadCredito = dto.CantidadCredito;
                paqueteExistente.UsuarioModificacion = usuario;
                paqueteExistente.FechaModificacion = DateTime.Now;
                _unitOfWork.PaqueteTutorVirtualRepository.Update(paqueteExistente);
                _unitOfWork.Commit();

                // 2. Obtener países existentes
                var paisesExistentes = _unitOfWork.PaqueteTutorVirtualPaisRepository.ObtenerPorIdPaquete(dto.Id).ToList();
                var idsExistentes = paisesExistentes.Select(p => p.Id).ToList();
                var idsRecibidos = dto.Paises.Where(p => p.Id > 0).Select(p => p.Id).ToList();

                // 3. Eliminar países que ya no existen
                var paisesEliminar = idsExistentes.Except(idsRecibidos).ToList();
                foreach (var idEliminar in paisesEliminar)
                {
                    // Primero eliminar beneficios del país
                    var beneficiosEliminar = _unitOfWork.PaqueteTutorVirtualBeneficioRepository
                        .ObtenerPorIdPaquetePais(idEliminar)
                        .Select(b => b.Id)
                        .ToList();
                    if (beneficiosEliminar.Any())
                    {
                        _unitOfWork.PaqueteTutorVirtualBeneficioRepository.Delete(beneficiosEliminar, usuario);
                    }
                    _unitOfWork.PaqueteTutorVirtualPaisRepository.Delete(idEliminar, usuario);
                }
                _unitOfWork.Commit();

                // 4. Insertar/Actualizar países
                foreach (var paisDto in dto.Paises)
                {
                    if (paisDto.Id > 0)
                    {
                        // Actualizar país existente
                        var paisExistente = _unitOfWork.PaqueteTutorVirtualPaisRepository.ObtenerPorId(paisDto.Id);
                        if (paisExistente != null)
                        {
                            paisExistente.IdPais = paisDto.IdPais;
                            paisExistente.IdMoneda = paisDto.IdMoneda;
                            paisExistente.CostoIndividual = paisDto.CostoIndividual;
                            paisExistente.CostoPaquete = paisDto.CostoPaquete;
                            paisExistente.UsuarioModificacion = usuario;
                            paisExistente.FechaModificacion = DateTime.Now;
                            _unitOfWork.PaqueteTutorVirtualPaisRepository.Update(paisExistente);
                            _unitOfWork.Commit();

                            // Manejar beneficios
                            var beneficiosExistentes = _unitOfWork.PaqueteTutorVirtualBeneficioRepository
                                .ObtenerPorIdPaquetePais(paisDto.Id).ToList();
                            var idsBeneficiosExistentes = beneficiosExistentes.Select(b => b.Id).ToList();
                            var idsBeneficiosRecibidos = paisDto.Beneficios.Where(b => b.Id > 0).Select(b => b.Id).ToList();

                            // Eliminar beneficios que ya no existen
                            var beneficiosEliminar = idsBeneficiosExistentes.Except(idsBeneficiosRecibidos).ToList();
                            if (beneficiosEliminar.Any())
                            {
                                _unitOfWork.PaqueteTutorVirtualBeneficioRepository.Delete(beneficiosEliminar, usuario);
                                _unitOfWork.Commit();
                            }

                            // Insertar/Actualizar beneficios
                            foreach (var beneficioDto in paisDto.Beneficios)
                            {
                                if (beneficioDto.Id > 0)
                                {
                                    // Actualizar
                                    var beneficioExistente = _unitOfWork.PaqueteTutorVirtualBeneficioRepository.ObtenerPorId(beneficioDto.Id);
                                    if (beneficioExistente != null)
                                    {
                                        beneficioExistente.Nombre = beneficioDto.Nombre;
                                        beneficioExistente.UsuarioModificacion = usuario;
                                        beneficioExistente.FechaModificacion = DateTime.Now;
                                        _unitOfWork.PaqueteTutorVirtualBeneficioRepository.Update(beneficioExistente);
                                    }
                                }
                                else
                                {
                                    // Insertar nuevo beneficio
                                    PaqueteTutorVirtualBeneficio nuevoBeneficio = new()
                                    {
                                        IdPaqueteTutorVirtualPais = paisDto.Id,
                                        Nombre = beneficioDto.Nombre,
                                        Estado = true,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    };
                                    var beneficioRespuesta = _unitOfWork.PaqueteTutorVirtualBeneficioRepository.Add(nuevoBeneficio);
                                    beneficioDto.Id = beneficioRespuesta.Id;
                                }
                            }
                            _unitOfWork.Commit();
                        }
                    }
                    else
                    {
                        // Insertar nuevo país
                        PaqueteTutorVirtualPais nuevoPais = new()
                        {
                            IdPaqueteTutorVirtual = dto.Id,
                            IdPais = paisDto.IdPais,
                            IdMoneda = paisDto.IdMoneda,
                            CostoIndividual = paisDto.CostoIndividual,
                            CostoPaquete = paisDto.CostoPaquete,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            ListadoBeneficios = new List<PaqueteTutorVirtualBeneficio>()
                        };

                        // Agregar beneficios
                        if (paisDto.Beneficios != null && paisDto.Beneficios.Any())
                        {
                            foreach (var beneficioDto in paisDto.Beneficios)
                            {
                                PaqueteTutorVirtualBeneficio beneficioEntidad = new()
                                {
                                    Nombre = beneficioDto.Nombre,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                nuevoPais.ListadoBeneficios.Add(beneficioEntidad);
                            }
                        }

                        var paisRespuesta = _unitOfWork.PaqueteTutorVirtualPaisRepository.Add(nuevoPais);
                        _unitOfWork.Commit();
                        paisDto.Id = paisRespuesta.Id;

                        // Actualizar IDs de beneficios
                        if (paisDto.Beneficios != null && paisRespuesta.TPaqueteTutorVirtualPaisBeneficios != null)
                        {
                            for (int i = 0; i < paisDto.Beneficios.Count; i++)
                            {
                                paisDto.Beneficios[i].Id = paisRespuesta.TPaqueteTutorVirtualPaisBeneficios.ElementAt(i).Id;
                            }
                        }
                    }
                }

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Metodo Eliminar.
        /// Autor: Christopher Sandy D' Paris
        /// Fecha:  27/11/2025
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key en cascada
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.PaqueteTutorVirtualRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    // 1. Obtener y eliminar países asociados
                    var paises = _unitOfWork.PaqueteTutorVirtualPaisRepository.ObtenerPorIdPaquete(id);
                    foreach (var pais in paises)
                    {
                        // 2. Obtener y eliminar beneficios asociados al país
                        var beneficios = _unitOfWork.PaqueteTutorVirtualBeneficioRepository.ObtenerPorIdPaquetePais(pais.Id);
                        if (beneficios.Any())
                        {
                            var idsBeneficios = beneficios.Select(b => b.Id).ToList();
                            _unitOfWork.PaqueteTutorVirtualBeneficioRepository.Delete(idsBeneficios, usuario);
                        }
                        // Eliminar el país
                        _unitOfWork.PaqueteTutorVirtualPaisRepository.Delete(pais.Id, usuario);
                    }

                    // 3. Eliminar el paquete principal
                    var respuesta = _unitOfWork.PaqueteTutorVirtualRepository.Delete(id, usuario);

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
