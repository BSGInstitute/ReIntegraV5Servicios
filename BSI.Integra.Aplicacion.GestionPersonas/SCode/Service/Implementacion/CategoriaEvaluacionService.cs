using AutoMapper;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: CategoriaEvaluacionService
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 03/04/2024
    /// <summary>
    /// Categoria Evaluacion Servicio
    /// </summary>
    public class CategoriaEvaluacionService : ICategoriaEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CategoriaEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEvaluacionCategorium, CategoriaEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<CategoriaEvaluacion, CategoriaEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CategoriaEvaluacionDTO, CategoriaEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEvaluacionCategorium, CategoriaEvaluacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 03/04/2024
        /// <summary>
        /// Categoria Evaluacion Servicio
        /// </summary>
        /// <returns> Lista PartnerPwDTO </returns>
        public IEnumerable<CategoriaEvaluacionDTO> Obtener()
        {
            return _unitOfWork.CategoriaEvaluacionRepository.Obtener();
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 03/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaEvaluacion
        /// </summary>
        /// <param name="dto">CategoriaEvaluacionDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PartnerPwDTO</returns>
        public CategoriaEvaluacionDTO Insertar(CategoriaEvaluacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CategoriaEvaluacion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.CategoriaEvaluacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<CategoriaEvaluacionDTO>(respuesta);

                   
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
        /// Fecha: 03/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CategoriaEvaluacionDTO"> parametros de la nueva CategoriaEvaluacion y sus detalles </param>

        public CategoriaEvaluacionDTO Actualizar(CategoriaEvaluacionDTO dto, string usuario)
        {
            try
            {
                CategoriaEvaluacion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CategoriaEvaluacionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.CategoriaEvaluacionRepository.Update(entidad);
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
        /// Fecha: 03/04/2024
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
                var entidad = _unitOfWork.CategoriaEvaluacionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.CategoriaEvaluacionRepository.Delete(id, usuario);

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
