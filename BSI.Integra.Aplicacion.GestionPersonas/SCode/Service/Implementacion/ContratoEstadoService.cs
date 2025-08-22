using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;



namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: ContratoEstadoService
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 09/04/2024
    /// <summary>
    /// Contrato Estado Service
    /// </summary>
    public class ContratoEstadoService : IContratoEstadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ContratoEstadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContratoEstado, ContratoEstado>(MemberList.None).ReverseMap();
                cfg.CreateMap<ContratoEstado, ContratoEstadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ContratoEstadoDTO, ContratoEstado>(MemberList.None).ReverseMap();
                cfg.CreateMap<TContratoEstado, ContratoEstadoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 10/04/2024
        /// <summary>
        /// Categoria Evaluacion Servicio
        /// </summary>
        /// <returns> Lista ContratoEstadoDTO </returns>
        public IEnumerable<ContratoEstadoDTO> Obtener()
        {
            return _unitOfWork.ContratoEstadoRepository.Obtener();
        }
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo ContratoEstado
        /// </summary>
        /// <param name="dto">CategoriaPreguntaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>ContratoEstado</returns>
        public ContratoEstadoDTO Insertar(ContratoEstadoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ContratoEstado entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.ContratoEstadoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<ContratoEstadoDTO>(respuesta);


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

        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="ContratoEstadoDTO"> parametros de la nueva ContratoEstado y sus detalles </param>

        public ContratoEstadoDTO Actualizar(ContratoEstadoDTO dto, string usuario)
        {
            try
            {
                ContratoEstado? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ContratoEstadoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioCreacion = usuario;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaCreacion = DateTime.Now;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.ContratoEstadoRepository.Update(entidad);
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

        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 10/04/2024
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
                var entidad = _unitOfWork.ContratoEstadoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.ContratoEstadoRepository.Delete(id, usuario);

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

