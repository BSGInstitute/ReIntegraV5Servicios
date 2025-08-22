using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstadoProyeccionFurService
    /// Autor: Griselberto Huaman.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_EstadoProyeccionFur
    /// </summary>
    public class EstadoProyeccionFurService  
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoProyeccionFurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoProyeccionFur, EstadoProyeccionFur>(MemberList.None).ReverseMap();
            }
           );

         
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EstadoProyeccionFur Add(EstadoProyeccionFurDTO data)
        {
            try
            {
                var rep = _unitOfWork.EstadoProyeccionFurRepository;
                EstadoProyeccionFur entidad = new EstadoProyeccionFur();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = rep.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoProyeccionFur>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoProyeccionFur Update(EstadoProyeccionFurDTO data)
        {
            try
            {
                var rep = _unitOfWork.EstadoProyeccionFurRepository;
                var entidad = _mapper.Map<EstadoProyeccionFur>(rep.ObtenerEstadoProyeccionFurById(data.Id));
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Nombre = data.Nombre;

                var modelo = _unitOfWork.EstadoProyeccionFurRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoProyeccionFur>(modelo);
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
                _unitOfWork.EstadoProyeccionFurRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoProyeccionFur> Add(List<EstadoProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoProyeccionFurRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoProyeccionFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoProyeccionFur> Update(List<EstadoProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoProyeccionFurRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoProyeccionFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// elimina un registro
        /// </summary>
        /// <returns> bool </returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.EstadoProyeccionFurRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por id
        /// </summary>
        /// <returns> EstadoProyeccionFur </returns>
        public IEnumerable<EstadoProyeccionFurDTO> ObtenerComboEstadoProyeccionFur()
        {
            try
            {
                return _unitOfWork.EstadoProyeccionFurRepository.ObtenerComboEstadoProyeccionFur();
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
