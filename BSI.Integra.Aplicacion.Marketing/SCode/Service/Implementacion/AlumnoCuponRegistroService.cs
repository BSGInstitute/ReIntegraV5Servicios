using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: AlumnoCuponRegistroService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_AlumnoCuponRegistro
    /// </summary>
    public class AlumnoCuponRegistroService : IAlumnoCuponRegistroService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AlumnoCuponRegistroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAlumnoCuponRegistro, AlumnoCuponRegistro>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AlumnoCuponRegistro Add(AlumnoCuponRegistro entidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoCuponRegistroRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AlumnoCuponRegistro>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AlumnoCuponRegistro Update(AlumnoCuponRegistro entidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoCuponRegistroRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AlumnoCuponRegistro>(modelo);
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
                _unitOfWork.AlumnoCuponRegistroRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AlumnoCuponRegistro> Add(List<AlumnoCuponRegistro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoCuponRegistroRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AlumnoCuponRegistro>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AlumnoCuponRegistro> Update(List<AlumnoCuponRegistro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoCuponRegistroRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AlumnoCuponRegistro>>(modelo);
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
                _unitOfWork.AlumnoCuponRegistroRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AlumnoCuponRegistro
        /// </summary>
        /// <returns> List<AlumnoCuponRegistroDTO> </returns>
        public IEnumerable<AlumnoCuponRegistroDTO> ObtenerAlumnoCuponRegistro()
        {
            try
            {
                return _unitOfWork.AlumnoCuponRegistroRepository.ObtenerAlumnoCuponRegistro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AlumnoCuponRegistro para mostrarse en combo.
        /// </summary>
        /// <returns> List<AlumnoCuponRegistroComboDTO> </returns>
        public IEnumerable<AlumnoCuponRegistroComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.AlumnoCuponRegistroRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
