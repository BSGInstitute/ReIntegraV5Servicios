using AutoMapper;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: CategoriaPreguntaService
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 03/04/2024
    /// <summary>
    /// Categoria Evaluacion Servicio
    /// </summary>
    public class CategoriaPreguntaService : ICategoriaPreguntaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CategoriaPreguntaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaCategorium, CategoriaPregunta>(MemberList.None).ReverseMap();
                cfg.CreateMap<CategoriaPregunta, CategoriaPreguntaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CategoriaPreguntaDTO, CategoriaPregunta>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaCategorium, CategoriaPreguntaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 04/04/2024
        /// <summary>
        /// Categoria Evaluacion Servicio
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<CategoriaPreguntaDTO> Obtener()
        {
            return _unitOfWork.CategoriaPreguntaRepository.Obtener();
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaPregunta
        /// </summary>
        /// <param name="dto">CategoriaPreguntaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CategoriaPreguntaDTO</returns>
        public CategoriaPreguntaDTO Insertar(CategoriaPreguntaDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CategoriaPregunta entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.CategoriaPreguntaRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<CategoriaPreguntaDTO>(respuesta);


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
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva CategoriaPregunta y sus detalles </param>

        public CategoriaPreguntaDTO Actualizar(CategoriaPreguntaDTO dto, string usuario)
        {
            try
            {
                CategoriaPregunta? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CategoriaPreguntaRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;             
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.CategoriaPreguntaRepository.Update(entidad);
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
        /// Fecha: 04/04/2024
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
                var entidad = _unitOfWork.CategoriaPreguntaRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.CategoriaPreguntaRepository.Delete(id, usuario);

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
