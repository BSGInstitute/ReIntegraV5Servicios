using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TagsEstiloService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TagsEstilo
    /// </summary>
    public class TagsEstiloService : ITagsEstiloService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TagsEstiloService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTagEstilo, TagsEstilo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public TagsEstilo Add(TagsEstiloEnvio data)
        {
            try
            {
                var repTagsEstilo = _unitOfWork.TagsEstiloRepository;
                TagsEstilo entidad = new TagsEstilo();
                entidad.IdTag = data.IdTag;
                entidad.IdEstilo = data.IdEstilo;
                entidad.Valor = data.Valor;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.TagsEstiloRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TagsEstilo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TagsEstilo Update(TagsEstiloEnvio data)
        {
            try
            {
                var repTagsEstilo = _unitOfWork.TagsEstiloRepository;
                TagsEstilo entidad = new TagsEstilo();
                entidad = _mapper.Map<TagsEstilo>(repTagsEstilo.FirstById(data.Id));
                entidad.IdTag = data.IdTag;
                entidad.IdEstilo = entidad.IdEstilo;
                entidad.Valor = data.Valor;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.TagsEstiloRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TagsEstilo>(modelo);
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
                _unitOfWork.TagsEstiloRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TagsEstilo> Add(List<TagsEstilo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TagsEstiloRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TagsEstilo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TagsEstilo> Update(List<TagsEstilo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TagsEstiloRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TagsEstilo>>(modelo);
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
                _unitOfWork.TagsEstiloRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_TagsEstilo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        //public IEnumerable<ObtenerFiltro> ObtenerFiltro()
        //{
        //    try
        //    {
        //        return _unitOfWork.TagsEstiloRepository.ObtenerFiltro();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TagsEstilo
        /// </summary>
        /// <returns> List<TagsEstiloDTO> </returns>
        public IEnumerable<TagsEstilo> ObtenerTagsEstilo()
        {
            try
            {
                return _unitOfWork.TagsEstiloRepository.ObtenerTagsEstilo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EstiloValor> ObtenerEstiloValor(int id)
        {
            try
            {
                return _unitOfWork.TagsEstiloRepository.ObtenerEstiloValor(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
