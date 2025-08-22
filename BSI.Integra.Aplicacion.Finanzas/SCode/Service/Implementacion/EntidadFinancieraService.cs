using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: EntidadFinancieraService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_EntidadFinanciera
    /// </summary>
    public class EntidadFinancieraService : IEntidadFinancieraService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EntidadFinancieraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntidadFinanciera, EntidadFinanciera>(MemberList.None).ReverseMap();
                cfg.CreateMap<EntidadFinancieraRecibidoDTO, EntidadFinanciera>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EntidadFinanciera Add(EntidadFinancieraRecibidoDTO data,string Usuario)
        {
            try
            {
                var entidad = _mapper.Map<EntidadFinanciera>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                var modelo = _unitOfWork.EntidadFinancieraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EntidadFinanciera>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntidadFinanciera Update(EntidadFinancieraRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorio = _unitOfWork.EntidadFinancieraRepository;
                var antiguo = repositorio.FirstById(data.Id);
                EntidadFinanciera entidadNueva = _mapper.Map<EntidadFinanciera>(data);
                entidadNueva.UsuarioCreacion = antiguo.UsuarioCreacion;
                entidadNueva.UsuarioModificacion = Usuario;
                entidadNueva.FechaModificacion = DateTime.Now;
                entidadNueva.FechaCreacion = antiguo.FechaCreacion;
                entidadNueva.Estado = antiguo.Estado;
                var modelo = repositorio.Update(entidadNueva);
                _unitOfWork.Commit();
                return _mapper.Map<EntidadFinanciera>(modelo);
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
                _unitOfWork.EntidadFinancieraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntidadFinanciera> Add(List<EntidadFinanciera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EntidadFinancieraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EntidadFinanciera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntidadFinanciera> Update(List<EntidadFinanciera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EntidadFinancieraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EntidadFinanciera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.EntidadFinancieraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EntidadFinanciera
        /// </summary>
        /// <returns> List<EntidadFinancieraDTO> </returns>
        public IEnumerable<EntidadFinancieraDTO> ObtenerEntidadFinanciera()
        {
            try
            {
                return _unitOfWork.EntidadFinancieraRepository.ObtenerEntidadFinanciera();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EntidadFinanciera para mostrarse en combo.
        /// </summary>
        /// <returns> List<EntidadFinancieraComboDTO> </returns>
        public IEnumerable<EntidadFinancieraComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EntidadFinancieraRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
