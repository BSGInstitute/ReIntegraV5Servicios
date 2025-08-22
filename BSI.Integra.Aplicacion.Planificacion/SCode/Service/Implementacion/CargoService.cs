using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: CargoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_Cargo
    /// </summary>
    public class CargoService : ICargoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CargoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCargo, Cargo>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TCargo, CargoDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<Cargo, CargoDTO>(MemberList.None).ReverseMap();

                }   
              );
            _mapper = new Mapper(config);
        }

        //#region Metodos Base
        //public Cargo Add(Cargo entidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.CargoRepository.Add(entidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<Cargo>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Cargo Update(Cargo entidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.CargoRepository.Update(entidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<Cargo>(modelo);
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
        //        _unitOfWork.CargoRepository.Delete(id, usuario);
        //        _unitOfWork.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<Cargo> Add(List<Cargo> listadoEntidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.CargoRepository.Add(listadoEntidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<List<Cargo>>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<Cargo> Update(List<Cargo> listadoEntidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.CargoRepository.Update(listadoEntidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<List<Cargo>>(modelo);
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
        //        _unitOfWork.CargoRepository.Delete(listadoIds, usuario);
        //        _unitOfWork.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion
        /// Autor:  Klebert Layme.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Cargo
        /// </summary>
        /// <returns> List<CargoDTO> </returns>
        public  List<CargoDTO> Obtener()
        {
            try
            {

                var respuesta = _unitOfWork.CargoRepository.ObtenerTodo();
                return _mapper.Map<List<CargoDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id Cargo
        /// </summary>
        /// <param name="idPGeneral">Id del Cargo</param>

        /// <returns> Cargo </returns>
        public CargoDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.CargoRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<CargoDTO>(respuesta);
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

        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Cargo
        /// </summary>
        /// <param name="dto">Cargo</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CargoDTO</returns>
        public CargoDTO Insertar(CargoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    Cargo entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Orden = dto.Orden,
                        Estado = dto.Estado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.CargoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<CargoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Cargos
        /// </summary>
        /// <param name="dtos">Lista de Cargos</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista CargoDTO</returns>
        public List<CargoDTO> InsertarLista(List<CargoDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<Cargo> entidades = new();
                    foreach (var item in dtos)
                    {
                        Cargo entidad = new()
                        {
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Orden = item.Orden,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.CargoRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<CargoDTO>>(respuesta);
                }
                return new List<CargoDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Cargo
        /// </summary>
        /// <param name="dto">Cargo</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>CargoDTO</returns>
        public CargoDTO Actualizar(CargoDTO dto, string usuario)
        {
            try
            {
                Cargo entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CargoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.Orden =  dto.Orden;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.Estado = dto.Estado;
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
                var respuesta = _unitOfWork.CargoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CargoDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de Cargo
        /// </summary>
        /// <param name="idPGeneral">Id del Cargol</param>
        /// <returns> CargoDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialAccion = _unitOfWork.CargoRepository.ObtenerPorId(id);
                if (materialAccion != null && materialAccion.Id != 0)
                {
                    var respuesta = _unitOfWork.CargoRepository.Delete(id, usuario);
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



        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Cargo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CargoRepository.ObtenerCombo();
            }
            catch 
            {
                throw;
            }
        }  
    }
}
