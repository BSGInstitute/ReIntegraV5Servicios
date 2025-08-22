using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

using DocumentFormat.OpenXml.Office2010.Excel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    public class ModuloSistemaPaqueteService : IModuloSistemaPaqueteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ModuloSistemaPaqueteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ModuloSistemaPaqueteV5DTO, TModuloSistemaPaqueteV5>(MemberList.None).ReverseMap();
                cfg.CreateMap<TModuloSistemaPaqueteV5, ModuloSistemaPaqueteV5DTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">ModuloSistemaPaqueteV5DTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MaterialAccionDTO</returns>
        public bool Insertar(ModuloSistemaPaqueteV5DTO dto, string usuario)
        {
            try
            {
                var entidad = new TModuloSistemaPaqueteV5();
                entidad.Id = 0;
                entidad.Nombre = dto.Nombre;
                entidad.Descripcion = dto.Descripcion;
                entidad.IdModuloSistema = dto.IdModuloSistema;
                entidad.Estado = true;
                entidad.UsuarioCreacion = usuario;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                _unitOfWork.ModuloSistemaPaqueteRepository.Add(entidad);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">ModuloSistemaPaqueteV5DTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MaterialAccionDTO</returns>
        public bool Actualizar(ModuloSistemaPaqueteV5DTO dto, string usuario)
        {
            try
            {
                var entidad = _unitOfWork.ModuloSistemaPaqueteRepository.ObtenerPorId(dto.Id);
                if (entidad != null)
                {
                    entidad.Id = dto.Id;
                    entidad.Nombre = dto.Nombre;
                    entidad.Descripcion = dto.Descripcion;
                    entidad.IdModuloSistema = dto.IdModuloSistema;
                    entidad.Estado = true;
                    entidad.UsuarioModificacion = usuario;
                    entidad.FechaModificacion = DateTime.Now;
                    _unitOfWork.ModuloSistemaPaqueteRepository.Update(entidad);
                    _unitOfWork.Commit();
                    return true;
                } else {
                    throw new BadRequestException("Entidad Nula");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">ModuloSistemaPaqueteV5DTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MaterialAccionDTO</returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var modulo = _unitOfWork.ModuloSistemaPaqueteRepository.ObtenerPorId(id);
                if (modulo != null && modulo.Id != 0)
                {
                    var respuesta = _unitOfWork.ModuloSistemaPaqueteRepository.Delete(id, usuario);
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
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <returns>MaterialAccionDTO</returns>
        public IEnumerable<ModuloSistemaPaqueteV5DTO> Obtener()
        {
            return _unitOfWork.ModuloSistemaPaqueteRepository.Obtener();
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <returns>MaterialAccionDTO</returns>
        public IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerModulos(int idPaquete)
        {
            return _unitOfWork.ModuloSistemaPaqueteRepository.ObtenerModulos(idPaquete);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <returns>MaterialAccionDTO</returns>
        public IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerListaModulos(int idPaquete)
        {
            return _unitOfWork.ModuloSistemaPaqueteRepository.ObtenerListaModulos(idPaquete);
        }
    }
}
