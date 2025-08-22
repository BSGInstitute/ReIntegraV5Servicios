using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: UrlSubContenedorService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_UrlSubContenedor
    /// </summary>
    public class UrlSubContenedorService : IUrlSubContenedorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public UrlSubContenedorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TUrlSubContenedor, UrlSubContenedor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public UrlSubContenedor Add(UrlSubContenedor entidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlSubContenedorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<UrlSubContenedor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UrlSubContenedor Update(UrlSubContenedor entidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlSubContenedorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<UrlSubContenedor>(modelo);
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
                _unitOfWork.UrlSubContenedorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UrlSubContenedor> Add(List<UrlSubContenedor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlSubContenedorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<UrlSubContenedor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UrlSubContenedor> Update(List<UrlSubContenedor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlSubContenedorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<UrlSubContenedor>>(modelo);
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
                _unitOfWork.UrlSubContenedorRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_UrlSubContenedor
        /// </summary>
        /// <returns> List<UrlSubContenedorDTO> </returns>
        public IEnumerable<UrlSubContenedorDTO> ObtenerRutaSubContenedor(int idSubContenedor)
        {
            try
            {
                return _unitOfWork.UrlSubContenedorRepository.ObtenerRutaSubContenedor(idSubContenedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}


