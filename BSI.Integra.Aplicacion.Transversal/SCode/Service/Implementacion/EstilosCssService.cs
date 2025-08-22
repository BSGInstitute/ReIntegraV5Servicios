using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstilosCssService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_EstilosCss
    /// </summary>
    public class EstilosCssService : IEstilosCssService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstilosCssService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEstilo, EstilosCss>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public EstilosCss Add(EstilosCssEnvio data)
        {
            try
            {
                var repEstilosCss = _unitOfWork.EstilosCssRepository;
                EstilosCss entidad = new EstilosCss();
                entidad.Nombre = data.Nombre;
                entidad.CodigoCss = data.CodigoCss;
                entidad.NombreTipo = data.NombreTipo;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.EstilosCssRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstilosCss>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstilosCss Update(EstilosCssEnvio data)
        {
            try
            {
                var repEstilosCss = _unitOfWork.EstilosCssRepository;
                EstilosCss entidad = new EstilosCss();
                entidad = _mapper.Map<EstilosCss>(repEstilosCss.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.CodigoCss = data.CodigoCss;
                entidad.NombreTipo = data.NombreTipo;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.EstilosCssRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstilosCss>(modelo);
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
                _unitOfWork.EstilosCssRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstilosCss> Add(List<EstilosCss> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstilosCssRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstilosCss>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstilosCss> Update(List<EstilosCss> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstilosCssRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstilosCss>>(modelo);
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
                _unitOfWork.EstilosCssRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_EstilosCss para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<EstiloCombo> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EstilosCssRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_EstilosCss
        /// </summary>
        /// <returns> List<EstilosCssDTO> </returns>
        public IEnumerable<EstilosCss> ObtenerEstilosCss()
        {
            try
            {
                return _unitOfWork.EstilosCssRepository.ObtenerEstilosCss();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EstiloCombo> ObtenerComboTagEstilo(int id)
        {
            try
            {
                return _unitOfWork.EstilosCssRepository.ObtenerComboTagEstilo(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
