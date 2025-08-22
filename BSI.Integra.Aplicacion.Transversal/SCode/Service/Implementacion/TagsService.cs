using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TagsService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Tags
    /// </summary>
    public class TagsService : ITagsService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TagsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTag, Tags>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public Tags Add(TagsEnvio data)
        {
            try
            {
                var repTags = _unitOfWork.TagsRepository;
                Tags entidad = new Tags();
                entidad.Nombre = data.Nombre;
                entidad.Texto = data.Texto;
                entidad.NombreTipo = data.NombreTipo;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.TagsRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Tags>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tags Update(TagsEnvio data)
        {
            try
            {
                var repTags = _unitOfWork.TagsRepository;
                Tags entidad = new Tags();
                entidad = _mapper.Map<Tags>(repTags.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.Texto = data.Texto;
                entidad.NombreTipo = data.NombreTipo;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.TagsRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Tags>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #region Metodos Base

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.TagsRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tags> Add(List<Tags> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TagsRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Tags>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tags> Update(List<Tags> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TagsRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Tags>>(modelo);
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
                _unitOfWork.TagsRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Tags para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboTag> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TagsRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Tags
        /// </summary>
        /// <returns> List<TagsDTO> </returns>
        public IEnumerable<Tags> ObtenerTags()
        {
            try
            {
                return _unitOfWork.TagsRepository.ObtenerTags();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
