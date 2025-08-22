using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: AreaTrabajoService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_AreaTrabajo
    /// </summary>
    public class AreaTrabajoService : IAreaTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AreaTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAreaTrabajo, AreaTrabajo>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TAreaTrabajo, AreaTrabajoDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<AreaTrabajo, AreaTrabajoDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }


        //#region Metodos Base
        //public AreaTrabajo Add(AreaTrabajo entidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaTrabajoRepository.Add(entidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<AreaTrabajo>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public AreaTrabajo Update(AreaTrabajo entidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaTrabajoRepository.Update(entidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<AreaTrabajo>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Delete(int id, string usuario)
        //{
        //    try
        //    {
        //        _unitOfWork.AreaTrabajoRepository.Delete(id, usuario);
        //        _unitOfWork.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<AreaTrabajo> Add(List<AreaTrabajo> listadoEntidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaTrabajoRepository.Add(listadoEntidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<List<AreaTrabajo>>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<AreaTrabajo> Update(List<AreaTrabajo> listadoEntidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaTrabajoRepository.Update(listadoEntidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<List<AreaTrabajo>>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Delete(List<int> listadoIds, string usuario)
        //{
        //    try
        //    {
        //        _unitOfWork.AreaTrabajoRepository.Delete(listadoIds, usuario);
        //        _unitOfWork.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id Area de Trabajo 
        /// </summary>
        /// <param name="idPGeneral">Id del area de trabajo</param>

        /// <returns> AreaTrabajo </returns>
        public AreaTrabajoDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AreaTrabajoRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<AreaTrabajoDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Area de trabajo
        /// </summary>
        /// <param name="dto">Area de trabajo</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>AreaTrabajo</returns>
        public AreaTrabajoDTO Insertar(AreaTrabajoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    AreaTrabajo entidad = new()
                    {
                        Nombre = dto.Nombre,

                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.AreaTrabajoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<AreaTrabajoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Area de trabajo
        /// </summary>
        /// <param name="dtos">Lista de Area de trabajo</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista MaterialAccionDTO</returns>
        public List<AreaTrabajoDTO> InsertarLista(List<AreaTrabajoDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<AreaTrabajo> entidades = new();
                    foreach (var item in dtos)
                    {
                        AreaTrabajo entidad = new()
                        {
                            Nombre = item.Nombre,

                            Estado = true,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.AreaTrabajoRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<AreaTrabajoDTO>>(respuesta);
                }
                return new List<AreaTrabajoDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Area de trabajo
        /// </summary>
        /// <param name="dto">Are de trabajo</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>AreaTrabajo</returns>
        public AreaTrabajoDTO Actualizar(AreaTrabajoDTO dto, string usuario)
        {
            try
            {
                AreaTrabajo entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.AreaTrabajoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;

                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
                var respuesta = _unitOfWork.AreaTrabajoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AreaTrabajoDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// elimina Un area de trabajo
        /// </summary>
        /// <param name="id">Id del area de trabajo</param>
        /// <returns> AreaTrabajo </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var areaTrabajo = _unitOfWork.AreaTrabajoRepository.ObtenerPorId(id);
                if (areaTrabajo != null && areaTrabajo.Id != 0)
                {
                    var respuesta = _unitOfWork.AreaTrabajoRepository.Delete(id, usuario);
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
        /// Autor Modificacion: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<AreaTrabajoComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.AreaTrabajoRepository.ObtenerCombo()!;
        }
        /// Autor Modificacion: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<AreaTrabajoComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAreaAgenda()
        {
            return _unitOfWork.AreaTrabajoRepository.ObtenerAreaAgenda();
        }
    }
}
