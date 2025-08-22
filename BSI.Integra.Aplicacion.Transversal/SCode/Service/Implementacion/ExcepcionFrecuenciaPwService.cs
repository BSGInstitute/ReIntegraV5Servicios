using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ExcepcionFrecuenciaPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_ExcepcionFrecuenciaPw
    /// </summary>
    public class ExcepcionFrecuenciaPwService : IExcepcionFrecuenciaPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ExcepcionFrecuenciaPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TExcepcionFrecuenciaPw, ExcepcionFrecuenciaPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ExcepcionFrecuenciaPw Add(ExcepcionFrecuenciaPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.ExcepcionFrecuenciaPwRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ExcepcionFrecuenciaPw>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExcepcionFrecuenciaPw Update(ExcepcionFrecuenciaPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.ExcepcionFrecuenciaPwRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ExcepcionFrecuenciaPw>(modelo);
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
                _unitOfWork.ExcepcionFrecuenciaPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExcepcionFrecuenciaPw> Add(List<ExcepcionFrecuenciaPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ExcepcionFrecuenciaPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ExcepcionFrecuenciaPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExcepcionFrecuenciaPw> Update(List<ExcepcionFrecuenciaPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ExcepcionFrecuenciaPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ExcepcionFrecuenciaPw>>(modelo);
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
                _unitOfWork.ExcepcionFrecuenciaPwRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ExcepcionFrecuenciaPw
        /// </summary>
        /// <returns> List<ExcepcionFrecuenciaPwDTO> </returns>
        public IEnumerable<ExcepcionFrecuenciaPwDTO> ObtenerExcepcionFrecuenciaPw()
        {
            try
            {
                return _unitOfWork.ExcepcionFrecuenciaPwRepository.ObtenerExcepcionFrecuenciaPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda las excepciones de programas generales.
        /// </summary>
        /// <returns> Lista Excepciones Programa generales: List<ExcepcionFrecuenciaPGeneralDTO></returns> 
        public IEnumerable<ExcepcionFrecuenciaPGeneralDTO> ObtenerTodoProgramaGeneral()
        {
            try
            {
                return _unitOfWork.ExcepcionFrecuenciaPwRepository.ObtenerTodoProgramaGeneral();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
