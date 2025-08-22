using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CriterioDocService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_CriterioDoc
    /// </summary>
    public class CriterioDocService : ICriterioDocService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CriterioDocService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, CriterioDoc>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CriterioDoc Add(CriterioDoc entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioDocRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioDoc>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CriterioDoc Update(CriterioDoc entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioDocRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioDoc>(modelo);
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
                _unitOfWork.CriterioDocRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CriterioDoc> Add(List<CriterioDoc> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioDocRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CriterioDoc>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CriterioDoc> Update(List<CriterioDoc> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioDocRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CriterioDoc>>(modelo);
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
                _unitOfWork.CriterioDocRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       public List<DTO.ComboDTO> ObtenerTodoSeleccionar()
        {
            try
            {
                return _unitOfWork.CriterioDocRepository.ObtenerTodoSeleccionar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO.ComboDTO> ObtenerCriterioModalidad(List<int> idModalidades)
        {
            try
            {
                return _unitOfWork.CriterioDocRepository.ObtenerCriterioModalidad(idModalidades);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
