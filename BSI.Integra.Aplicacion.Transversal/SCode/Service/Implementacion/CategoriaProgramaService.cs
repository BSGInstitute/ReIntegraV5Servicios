using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CategoriaProgramaService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_CategoriaPrograma
    /// </summary>
    public class CategoriaProgramaService : ICategoriaProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CategoriaProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCategoriaPrograma, CategoriaPrograma>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CategoriaPrograma Add(CategoriaPrograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.CategoriaProgramaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CategoriaPrograma>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoriaPrograma Update(CategoriaPrograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.CategoriaProgramaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CategoriaPrograma>(modelo);
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
                _unitOfWork.CategoriaProgramaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaPrograma> Add(List<CategoriaPrograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CategoriaProgramaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CategoriaPrograma>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaPrograma> Update(List<CategoriaPrograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CategoriaProgramaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CategoriaPrograma>>(modelo);
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
                _unitOfWork.CategoriaProgramaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CategoriaPrograma
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CategoriaProgramaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
