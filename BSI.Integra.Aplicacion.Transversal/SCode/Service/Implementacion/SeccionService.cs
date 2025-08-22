using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SeccionService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Seccion
    /// </summary>
    public class SeccionService : ISeccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SeccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSeccion, Seccion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public Seccion Add(SeccionEnvio data)
        {
            try
            {
                var repSeccion = _unitOfWork.SeccionRepository;
                Seccion entidad = new Seccion();
                entidad.Nombre = data.Nombre;
                entidad.EstadoTexto = data.EstadoTexto;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.SeccionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Seccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Seccion Update(SeccionEnvio data)
        {
            try
            {
                var repSeccion = _unitOfWork.SeccionRepository;
                Seccion entidad = new Seccion();
                entidad = _mapper.Map<Seccion>(repSeccion.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.EstadoTexto = data.EstadoTexto;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.SeccionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Seccion>(modelo);
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
                _unitOfWork.SeccionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Seccion> Add(List<Seccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SeccionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Seccion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Seccion> Update(List<Seccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SeccionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Seccion>>(modelo);
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
                _unitOfWork.SeccionRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_Seccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<SeccionCombo> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SeccionRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_Seccion
        /// </summary>
        /// <returns> List<SeccionDTO> </returns>
        public IEnumerable<Seccion> ObtenerSeccion()
        {
            try
            {
                return _unitOfWork.SeccionRepository.ObtenerSeccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
