using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: AreaFormacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_AreaFormacion
    /// </summary>
    public class AreaFormacionService : IAreaFormacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AreaFormacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAreaFormacion, AreaFormacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AreaFormacion Add(AreaFormacion dto , string usuario)
        {
            try
            {
                if (dto != null)
                {
                    AreaFormacion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.AreaFormacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<AreaFormacion>(respuesta);


                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AreaFormacion Update(AreaFormacion dto, string usuario)
        {
            try
            {
                AreaFormacion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.AreaFormacionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.AreaFormacionRepository.Update(entidad);
                            _unitOfWork.Commit();


                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.AreaFormacionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.AreaFormacionRepository.Delete(id, usuario);

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AreaFormacion> Add(List<AreaFormacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AreaFormacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AreaFormacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AreaFormacion> Update(List<AreaFormacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AreaFormacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AreaFormacion>>(modelo);
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
                _unitOfWork.AreaFormacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaFormacion
        /// </summary>
        /// <returns> List<AreaFormacionDTO> </returns>
        public IEnumerable<AreaFormacionDTO> ObtenerAreaFormacion()
        {
            try
            {
                return _unitOfWork.AreaFormacionRepository.ObtenerAreaFormacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaFormacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.AreaFormacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> Obtener()
        {
            return _unitOfWork.AreaFormacionRepository.ObtenerAreaFormacionFiltro();
        }
    }
}
