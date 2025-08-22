
using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

using System;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class CategoriaCiudadService : ICategoriaCiudadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CategoriaCiudadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCategoriaCiudad, TroncalEntidadDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<CategoriaCiudad, TroncalEntidadDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaCiudad
        /// </summary>
        /// <param name="dto">TroncalEntidadDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>FeedbackTipoDTO</returns>
        public TroncalEntidadDTO InsertarTroncal(TroncalEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    var validarCiudadCategoria = _unitOfWork.CategoriaCiudadRepository.ValidarPorCiudadCategoria(dto.IdCategoriaPrograma, dto.IdRegionCiudad);
                    var validarTroncal = _unitOfWork.CategoriaCiudadRepository.ValidarTroncal(dto.TroncalCompleto);
                    if (validarTroncal != null)
                    {
                        if (dto.TroncalCompleto.Equals(validarTroncal.TroncalCompleto))
                            throw new BadRequestException("Troncal Existente");
                    }
                    if (validarCiudadCategoria != null)
                    {
                        if (dto.IdCategoriaPrograma == validarCiudadCategoria.IdCategoriaPrograma && dto.IdRegionCiudad == validarCiudadCategoria.IdRegionCiudad)
                            throw new BadRequestException("Troncal configurado programa y region");
                    }

                    CategoriaCiudad entidad = new()
                    {
                        Id = dto.Id,
                        IdCategoriaPrograma = dto.IdCategoriaPrograma,
                        IdRegionCiudad = dto.IdRegionCiudad,
                        TroncalCompleto = dto.TroncalCompleto,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var resultado = _unitOfWork.CategoriaCiudadRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<TroncalEntidadDTO>(resultado);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaCiudad
        /// </summary>
        /// <param name="dto">TroncalEntidadDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>FeedbackTipoDTO</returns>
        public TroncalEntidadDTO ActualizarTroncal(TroncalEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CategoriaCiudad entidad = _unitOfWork.CategoriaCiudadRepository.ObtenerPorId(dto.Id);
                    if (dto.TroncalCompleto == entidad.TroncalCompleto)
                    {
                        if (dto.IdCategoriaPrograma == entidad.IdCategoriaPrograma && dto.IdRegionCiudad == entidad.IdRegionCiudad)
                        {
                            throw new BadRequestException("Troncal ya Existe");
                        }
                        else
                        {
                            entidad.IdCategoriaPrograma = dto.IdCategoriaPrograma;
                            entidad.IdRegionCiudad = dto.IdRegionCiudad;
                            entidad.TroncalCompleto = dto.TroncalCompleto;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                            _unitOfWork.CategoriaCiudadRepository.Update(entidad);
                        }
                    }
                    if (dto.IdCategoriaPrograma == entidad.IdCategoriaPrograma && dto.IdRegionCiudad == entidad.IdRegionCiudad)
                    {
                        if (dto.TroncalCompleto == entidad.TroncalCompleto)
                        {
                            throw new BadRequestException("Troncal ya Existe");
                        }
                        else
                        {
                            entidad.IdCategoriaPrograma = dto.IdCategoriaPrograma;
                            entidad.IdRegionCiudad = dto.IdRegionCiudad;
                            entidad.TroncalCompleto = dto.TroncalCompleto;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                            _unitOfWork.CategoriaCiudadRepository.Update(entidad);
                        }
                    }
                    _unitOfWork.Commit();
                    return _mapper.Map<TroncalEntidadDTO>(entidad);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaCiudad
        /// </summary>
        /// <returns>FeedbackTipoDTO</returns>
        public IEnumerable<TroncalDTO> ObtenerTroncales()
        {
            try
            {
                var respuesta = _unitOfWork.CategoriaCiudadRepository.ObtenerTroncales();
                return _mapper.Map<IEnumerable<TroncalDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaCiudad
        /// </summary>
        /// <returns>FeedbackTipoDTO</returns>
        public IEnumerable<ComboDTO> ObtenerCategoriaCombo()
        {
            try
            {
                return _unitOfWork.CategoriaProgramaRepository.ObtenerCombo();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaCiudad
        /// </summary>
        /// <returns>FeedbackTipoDTO</returns>
        public IEnumerable<ComboDTO> ObtenerCiudadBsCombo()
        {
            try
            {
                var respuesta = _unitOfWork.RegionCiudadRepository.ObtenerCiudadBsCombo();
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
