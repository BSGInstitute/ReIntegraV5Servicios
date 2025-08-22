using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TagService
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_Tag
    /// </summary>
    public class TagService : ITagService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTag, Tag>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Tag Add(Tag entidad)
        {
            try
            {
                var modelo = _unitOfWork.TagRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Tag>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tag Update(Tag entidad)
        {
            try
            {
                var modelo = _unitOfWork.TagRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Tag>(modelo);
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
                _unitOfWork.TagRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tag> Add(List<Tag> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TagRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Tag>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tag> Update(List<Tag> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TagRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Tag>>(modelo);
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
                _unitOfWork.TagRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_Tag
        /// </summary>
        /// <returns> IEnumerable<TagComboDTO> </returns>
        public IEnumerable<TagComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TagRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
