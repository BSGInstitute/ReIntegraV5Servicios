using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class NivelEstudioService : INivelEstudioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public NivelEstudioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNivelEstudio, NivelEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<NivelEstudio, NivelEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TNivelEstudio, NivelEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<NivelEstudio, NivelEstudioComboDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TNivelEstudio, NivelEstudioComboDTO>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de NivelEstudio
        /// </summary>
        /// <returns> Lista NivelEstudioDTO </returns>
        public IEnumerable<NivelEstudioDTO> Obtener()
        {
            return _unitOfWork.NivelEstudioRepository.Obtener();
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de NivelEstudio
        /// </summary>
        /// <returns> Lista NivelEstudioDTO </returns>
        public IEnumerable<TipoFormacionDTO> ObtenerFormacion()
        {
            return _unitOfWork.TipoFormacionRepository.Obtener();
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo NivelEstudio
        /// </summary>
        /// <param name="dto">NivelEstudioDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>NivelEstudioDTO</returns>
        public NivelEstudioComboDTO Insertar(NivelEstudioComboDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    NivelEstudio entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdTipoFormacion = dto.IdTipoFormacion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.NivelEstudioRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<NivelEstudioComboDTO>(respuesta);

                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="dto"> NivelEstudioDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public NivelEstudioComboDTO Actualizar(NivelEstudioComboDTO dto, string usuario)
        {
            try
            {
                NivelEstudio? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.NivelEstudioRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdTipoFormacion = dto.IdTipoFormacion;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.NivelEstudioRepository.Update(entidad);
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
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
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
                var entidad = _unitOfWork.NivelEstudioRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.NivelEstudioRepository.Delete(id, usuario);

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
