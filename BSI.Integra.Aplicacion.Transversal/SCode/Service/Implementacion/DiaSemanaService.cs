using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DiaSemanaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class DiaSemanaService : IDiaSemanaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DiaSemanaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDiaSemana, DiaSemana>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DiaSemana Add(DiaSemana entidad)
        {
            try
            {
                var modelo = _unitOfWork.DiaSemanaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DiaSemana>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DiaSemana Update(DiaSemana entidad)
        {
            try
            {
                var modelo = _unitOfWork.DiaSemanaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DiaSemana>(modelo);
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
                _unitOfWork.DiaSemanaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DiaSemana> Add(List<DiaSemana> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DiaSemanaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DiaSemana>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DiaSemana> Update(List<DiaSemana> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DiaSemanaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DiaSemana>>(modelo);
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
                _unitOfWork.DiaSemanaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_TipoDato para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.DiaSemanaRepository.ObtenerCombo();
        }
    }
}
