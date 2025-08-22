using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoLandingPageService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoLandingPage
    /// </summary>
    public class TipoLandingPageService : ITipoLandingPageService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoLandingPageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoLandingPage, TipoLandingPage>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public TipoLandingPage Add(TipoLandingPageEnvio data)
        {
            try
            {
                var repTipoLandingPage = _unitOfWork.TipoLandingPageRepository;
                TipoLandingPage entidad = new TipoLandingPage();
                entidad.Nombre = data.Nombre;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.TipoLandingPageRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoLandingPage>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoLandingPage Update(TipoLandingPageEnvio data)
        {
            try
            {
                var repTipoLandingPage = _unitOfWork.TipoLandingPageRepository;
                TipoLandingPage entidad = new TipoLandingPage();
                entidad = _mapper.Map<TipoLandingPage>(repTipoLandingPage.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.TipoLandingPageRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoLandingPage>(modelo);
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
                _unitOfWork.TipoLandingPageRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoLandingPage> Add(List<TipoLandingPage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoLandingPageRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoLandingPage>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoLandingPage> Update(List<TipoLandingPage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoLandingPageRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoLandingPage>>(modelo);
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
                _unitOfWork.TipoLandingPageRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_TipoLandingPage para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboTipoLandingPage> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoLandingPageRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_TipoLandingPage
        /// </summary>
        /// <returns> List<TipoLandingPageDTO> </returns>
        public IEnumerable<TipoLandingPage> ObtenerTipoLandingPage()
        {
            try
            {
                return _unitOfWork.TipoLandingPageRepository.ObtenerTipoLandingPage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
