using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class TipoSangreService : ITipoSangreService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TipoSangreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoSangre, TipoSangre>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoSangre, TipoSangreDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoSangre, TipoSangreDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 07/05/2024
        /// <summary>
        /// Obtiene todos los registros de TipoSangre
        /// </summary>
        /// <returns> Lista TipoSangreDTO </returns>
        public IEnumerable<TipoSangreDTO> Obtener()
        {
            return _unitOfWork.TipoSangreRepository.Obtener();
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 07/05/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo TipoSangre
        /// </summary>
        /// <param name="dto">TipoSangreDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>TipoSangreDTO</returns>
        public TipoSangreDTO Insertar(TipoSangreDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    TipoSangre entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.TipoSangreRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<TipoSangreDTO>(respuesta);
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 07/05/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="dto"> TipoSangreDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public TipoSangreDTO Actualizar(TipoSangreDTO dto, string usuario)
        {
            try
            {
                TipoSangre? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.TipoSangreRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.TipoSangreRepository.Update(entidad);
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 07/05/2024
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
                var entidad = _unitOfWork.TipoSangreRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.TipoSangreRepository.Delete(id, usuario);
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
