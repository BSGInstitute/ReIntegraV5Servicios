using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionDePersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;


namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class PuestoTrabajoNivelService : INivelPuestoTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PuestoTrabajoNivelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoNivel, PuestoTrabajoNivel>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoNivel, PuestoTrabajoNivelDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPuestoTrabajoNivel, PuestoTrabajoNivelDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);

        }

        /// Autor: Marco jose Villanueva Torres 
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PuestoTrabajoNivel
        /// </summary>
        /// <returns> List<PuestoTrabajoNivelDTO> </returns>
        public IEnumerable<PuestoTrabajoNivelDTO> Obtener()
        {
            return _unitOfWork.MaestroNivelPuestoTrabajoRepository.Obtener();
        }
        /// Autor: Marco jose Villanueva Torres 
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo T_PuestoTrabajoNivel
        /// </summary>
        /// <param name="dto">PuestoTrabajoNivelDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PuestoTrabajoNivelDTO</returns>
        public PuestoTrabajoNivelDTO Insertar(PuestoTrabajoNivelDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PuestoTrabajoNivel entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.MaestroNivelPuestoTrabajoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PuestoTrabajoNivelDTO>(respuesta);
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
        /// Autor: Marco jose Villanueva Torres 
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el registro de T_PuestoTrabajoNivel
        /// </summary>
        /// <returns> List<PuestoTrabajoNivelDTO> </returns>
        public PuestoTrabajoNivelDTO Actualizar(PuestoTrabajoNivelDTO dto, string usuario)
        {
            try
            {
                PuestoTrabajoNivel? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.MaestroNivelPuestoTrabajoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.UsuarioModificacion = usuario;              
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.MaestroNivelPuestoTrabajoRepository.Update(entidad);
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
        /// Autor: Marco jose Villanueva Torres 
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina el valor con el id seleccionado
        /// </summary>
        /// <returns> boolean(true/false)</returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.MaestroNivelPuestoTrabajoRepository.ObtenerPorId(id)
;
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.MaestroNivelPuestoTrabajoRepository.Delete(id, usuario);

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
