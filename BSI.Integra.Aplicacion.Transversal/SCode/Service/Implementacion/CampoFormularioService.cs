using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CampoFormularioService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CampoFormulario
    /// </summary>
    public class CampoFormularioService : ICampoFormularioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampoFormularioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampoFormulario, CampoFormulario>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CampoFormulario Add(CampoFormulario entidad)
        {
            try
            {
                var modelo = _unitOfWork.CampoFormularioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampoFormulario>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampoFormulario Update(CampoFormulario entidad)
        {
            try
            {
                var modelo = _unitOfWork.CampoFormularioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampoFormulario>(modelo);
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
                _unitOfWork.CampoFormularioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampoFormulario> Add(List<CampoFormulario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampoFormularioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampoFormulario>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampoFormulario> Update(List<CampoFormulario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampoFormularioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampoFormulario>>(modelo);
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
                _unitOfWork.CampoFormularioRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_CampoFormulario para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CampoFormularioRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_CampoFormulario
        /// </summary>
        /// <returns> List<CampoFormularioDTO> </returns>
        public IEnumerable<CampoFormularioDTO> ObtenerCampoFormulario()
        {
            try
            {
                return _unitOfWork.CampoFormularioRepository.ObtenerCampoFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CampoFormularioSeleccionadoDTO> ObtenerCampoFormularioPorIdFormularioSolicitud(int idFormularioSolicitud)
        {
            try
            {
                return _unitOfWork.CampoFormularioRepository.ObtenerCampoFormularioPorIdFormularioSolicitud(idFormularioSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
