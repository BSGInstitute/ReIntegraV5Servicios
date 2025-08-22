using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EmbudoNivelService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_EmbudoNivel
    /// </summary>
    public class EmbudoNivelService : IEmbudoNivelService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EmbudoNivelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, EmbudoNivel>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EmbudoNivel Add(EmbudoNivel entidad)
        {
            try
            {
                var modelo = _unitOfWork.EmbudoNivelRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EmbudoNivel>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmbudoNivel Update(EmbudoNivel entidad)
        {
            try
            {
                var modelo = _unitOfWork.EmbudoNivelRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EmbudoNivel>(modelo);
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
                _unitOfWork.EmbudoNivelRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmbudoNivel> Add(List<EmbudoNivel> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EmbudoNivelRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EmbudoNivel>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmbudoNivel> Update(List<EmbudoNivel> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EmbudoNivelRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EmbudoNivel>>(modelo);
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
                _unitOfWork.EmbudoNivelRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       public List<DTO.ComboDTO> ObtenerEmbudoNivel()
        {
            try
            {
                return _unitOfWork.EmbudoNivelRepository.ObtenerEmbudoNivel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
